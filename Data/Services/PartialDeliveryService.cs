using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Nettbutikk.Data.DTO;
using Nettbutikk.Data.Events;
using Nettbutikk.Factories;
using Nettbutikk.Models;
using Nettbutikk.Models.EventModels;
using Nettbutikk.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Nettbutikk.Data.Services
{
    public class PartialDeliveryService
    {
        private readonly WebStoreContext _webStoreContext;
        private readonly PartialDeliveryFactory _partialDeliveryFactory;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHubContext<PartialDeliveryHub> _partialDeliveryHub;
        private PartialDeliveryHubUserManagerSingleton _connectedUserManager;

        public PartialDeliveryService(WebStoreContext webStoreContext, PartialDeliveryFactory partialDeliveryFactory, UserManager<UserEntity> userManager, IHubContext<PartialDeliveryHub> partialDeliveryHub
            , PartialDeliveryHubUserManagerSingleton connectedUserManager)
        {
            _webStoreContext = webStoreContext;
            _partialDeliveryFactory = partialDeliveryFactory;
            _userManager = userManager;
            _partialDeliveryHub = partialDeliveryHub;
            _connectedUserManager = connectedUserManager;
        }

        public Task<List<PartialDelivery>> CheckIfUserHasPartialDeliveries(string userId)
        {
            if (userId is not null)
            {
                if (_webStoreContext.PartialDeliveries.Any(pd => pd.UserId.Equals(userId)))
                {
                    var partials = _webStoreContext.PartialDeliveries.Where(pd => pd.UserId.Equals(userId)).ToList();
                    return Task.FromResult(partials);
                }

                return null;
            }

            throw new ArgumentNullException($"{nameof(userId)} can not be null when checking for partial deliveries");
        }

#nullable enable
        public async Task HandleProductsReceivedForPartialDeliveryEvents(List<Event> events)
        {
            if (events is null) throw new ArgumentNullException($"{nameof(events)} list argument is null");

            if (!events.Any()) throw new Exception($"{nameof(events)}s list argument does not contain any events");

            foreach (var evt in events)
            {
                if (!evt.EventName.Equals(EventTypes.DeliveryLateEvent)) continue;

                var lateEvt = JsonSerializer.Deserialize<DeliveryLateEvent>(evt.JsonData) ?? throw new Exception($"{nameof(evt.JsonData)} event data can not be null");

                var queryableProducts = lateEvt.Products.AsQueryable().ToList();
                var partials = new List<PartialDelivery>();
                queryableProducts.ForEach(p =>
                {
                    var partialsWithProduct = _webStoreContext.PartialDeliveries
                        .Include(partial => partial.PartialDeliveryProductRelations)
                        .ThenInclude(pdr => pdr.Product)
                        .Where(partial => partial.PartialDeliveryProductRelations
                        .Any(pdr => pdr.ProductId.ToString().Equals(p.Id.ToString())) && !partial.IsFulfilled).ToList();

                    if (partialsWithProduct.Any()) partials.AddRange(partialsWithProduct);
                });

                foreach (var partial in partials)
                {
                    try
                    {
                        var order = _webStoreContext.Orders
                        .Include(o => o.PartialDelivery)
                        .Where(o => o.PartialDeliveryId.ToString().Equals(partial.Id.ToString())).Single();

                        await NotifyUserOfSentPartialWithHub(order);
                        order.PartialDelivery.IsFulfilled = true;

                        await _webStoreContext.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }
#nullable disable

        public async Task NotifyUserOfSentPartialWithHub(Order order)
        {
            var user = _connectedUserManager._connectedUsers.Where(e => e.Key.Id.Equals(order.PartialDelivery.UserId.ToString())).Single().Key
                ?? throw new Exception("user in partial delivery argument is not connected to this hub.");

            if (user.Id.Equals(order.PartialDelivery.UserId))
            {
                var queryable = order.PartialDelivery.PartialDeliveryProductRelations.AsQueryable();
                var productDTOs = queryable.Where(pd => pd.PartialDeliveryId.ToString().Equals(order.PartialDelivery.Id))
                    .Include(pdpr => pdpr.Product)
                    .Select(o => new ProductDTO
                    {
                        Price = o.Product.Price,
                        Count = o.ProductCount,
                        Category = o.Product.Category,
                        Description = o.Product.Description,
                        Name = o.Product.Name,
                        Id = o.Product.Id
                    }).ToList();

                var response = new
                {
                    Id = order.PartialDelivery.Id,
                    Products = productDTOs,
                    orderId = order.Id,
                    DateCreated = order.PartialDelivery.DateCreated,
                    Expected = order.PartialDelivery.Expected
                };

                var clientProxy = _partialDeliveryHub.Clients.User(order.PartialDelivery.UserId);
                await clientProxy.SendAsync("ReceivePartialDeliveryNotification", response);
            }

            else throw new Exception("Incongruent users in NotifyUserOfSentPartial method when comparing user of order and user of partial delivery");
        }
    }
}
