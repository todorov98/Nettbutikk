﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Nettbutikk.Data.DTO;
using Nettbutikk.Data.Services;
using Nettbutikk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nettbutikk.SignalR
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PartialDeliveryHub : Hub
    {
        private readonly UserContextService _userContextService;
        private Dictionary<UserEntity, string> _connectedUsers;

        public PartialDeliveryHub(UserContextService userContextService)
        {
            _userContextService = userContextService;
            _connectedUsers = new Dictionary<UserEntity, string>();
        }

        [Authorize]
        public override Task OnConnectedAsync()
        {
            var user = GetCurrentConnectingUser();
            _connectedUsers.Add(user, Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        [Authorize]
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = GetCurrentConnectingUser();
            _connectedUsers.Remove(user);

            return base.OnDisconnectedAsync(exception);
        }

        private UserEntity GetCurrentConnectingUser()
        {
            var httpContext = Context.GetHttpContext()
               ?? throw new Exception("Result is null. Connection not associated with HTTP request.");

            return _userContextService.GetCurrentUserOnHttpContext(httpContext).Result;
        }

        public async Task NotifyUserOfSentPartial(Order order)
        {
            if (!order.PartialDelivery.IsFulfilled)
                throw new Exception("Can not notify user of sent partial delivery when partial delivery is not fulfilled");

            var user = _connectedUsers.Where(e => e.Key.Id.Equals(order.PartialDelivery.Id.ToString())).FirstOrDefault().Key
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

                var clientProxy = Clients.User(order.PartialDelivery.UserId);
                await clientProxy.SendAsync("ReceivePartialDeliveryNotification", response);
            }

            else throw new Exception("Incongruent users retrived in NotifyUserOfSentPartial method");
        }
    }
}
