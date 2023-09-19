using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nettbutikk.CustomExceptions;
using Nettbutikk.Data.DTO;
using Nettbutikk.Factories;
using Nettbutikk.Models;
using Nettbutikk.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nettbutikk.Data.Services
{
    /// <summary>
    /// This service is used to work with order related data.
    /// </summary>
    public class OrderService
    {
        private readonly WebStoreContext _webStoreContext;
        private readonly DtoMapperService _dtoMapperService;
        private readonly ProductOrderRelationFactory _productOrderRelationFactory;
        private readonly OrderReceiptFactory _orderReceiptFactory;
        private readonly OrderFactory _orderFactory;
        private readonly UserManager<UserEntity> _userManager;
        private readonly PartialDeliveryFactory _partialDeliveryFactory;

        public OrderService
        (
            WebStoreContext webStoreContext, 
            DtoMapperService dtoMapperService, 
            ProductOrderRelationFactory productOrderRelationFactory, 
            OrderReceiptFactory orderReceiptFactory,
            OrderFactory orderFactory,
            UserManager<UserEntity> userManager,
            PartialDeliveryFactory partialDeliveryFactory
        )

        {
            _webStoreContext = webStoreContext;
            _dtoMapperService = dtoMapperService;
            _productOrderRelationFactory = productOrderRelationFactory;
            _orderReceiptFactory = orderReceiptFactory;
            _orderFactory = orderFactory;
            _userManager = userManager;
            _partialDeliveryFactory = partialDeliveryFactory;
        }
        
        /// <summary>
        /// Gets all the orders for a specific customer.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>All orders placed by the customer.</returns>
        public Task<List<Order>> GetAllOrdersOnUserId(string userId)
        {
            var orders = _webStoreContext.Orders
                .Where(order => order.UserId.Equals(userId))
                .Include(order => order.ProductOrderRelations)
                .ThenInclude(por => por.Product)
                .Select(o => new Order
                {
                    Id = o.Id,
                    DateFulfilled = o.DateFulfilled,
                    DatePlaced = o.DatePlaced,
                    Price = o.Price,
                    Stage = o.Stage,
                    ProductOrderRelations = o.ProductOrderRelations,
                    UserId = o.UserId
                }).ToList();

            return Task.FromResult(orders);
        }

        /// <summary>
        /// Checks if a specific product was ordered before by the specific customer.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns>All orders placed by customer that contain the product.</returns>
        public Task<List<Order>> CheckIfProductWasOrderedBefore(string userId, Guid productId)
        {
            var orders = _webStoreContext.Orders
                .Include(order => order.ProductOrderRelations)
                .ThenInclude(por => por.Product)
                .Where(order => order.UserId.Equals(userId) && order.ProductOrderRelations
                .Any(por => por.ProductId == productId))
                .Select(o => new Order
                {
                    Id = o.Id,
                    DateFulfilled = o.DateFulfilled,
                    DatePlaced = o.DatePlaced,
                    Price = o.Price,
                    Stage = o.Stage,
                    ProductOrderRelations = o.ProductOrderRelations
                }).ToList();

            return Task.FromResult(orders);
        }

        public async Task<CancelOrderConfirmation> CancelOrder(string orderId, UserEntity user)
        {
            var order = _webStoreContext.Orders.Include(o => o.PartialDelivery).FirstOrDefault(o => o.Id.ToString().Equals(orderId))
                ?? throw new Exception("Order not found.");

            var cancelConfirmation = order.CancelOrder();

            cancelConfirmation.CancelledByAdmin = await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin);
            order.PartialDelivery.StageOfOrder = OrderStages.Cancelled;

            await _webStoreContext.SaveChangesAsync();
            return cancelConfirmation;
        }

        public Task<Order> GetOrderOnId(string id)
        {
            var order = _webStoreContext.Orders
                .FirstOrDefault(o => o.Id.ToString().Equals(id)) ?? throw new Exception("Order not found.");

            return Task.FromResult(order);
        }

        /// <summary>
        /// Gets all completed orders for a specific customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>All completed orders for the customer.</returns>
        public Task<List<Order>> GetAllCompletedOrdersOnCustomerId(string userId)
        {
            var orders = _webStoreContext.Orders
                .Where(order => order.UserId.Equals(userId) && order.Stage.Equals(OrderStages.Delivered))
                .Include(order => order.ProductOrderRelations)
                .ThenInclude(por => por.Product)
                .Select(o => new Order
                {
                    Id = o.Id,
                    DatePlaced = o.DatePlaced,
                    DateFulfilled = o.DateFulfilled,
                    Price = o.Price,
                    Stage = o.Stage,
                    ProductOrderRelations = o.ProductOrderRelations
                }).ToList();

            return Task.FromResult(orders);
        }

        /// <summary>
        /// Handles and places an order that is received from the controller.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="dto"></param>
        /// <returns>Order that was placed.</returns>
        public async Task<OrderReceipt> PlaceOrder(OrderDTO dto, UserEntity user)
        {
            var orderEntity = await _orderFactory.CreateAndInitializeOrderFromDTO(dto, user.Id);
            var partialDelivery = _partialDeliveryFactory.CreatePartialDelivery();

            if (orderEntity is not null)
            {
                foreach (var product in dto.Products)
                {
                    if (product.Count <= 0)
                        throw new Exception("Invalid product count.");

                    var retrievedProduct = _webStoreContext.Products.FirstOrDefault(p => p.Name.Equals(product.Name))
                        ?? throw new Exception("Product not found.");

                    if (retrievedProduct.Count == 0 || (retrievedProduct.Count - product.Count < 0))
                    {
                        if (orderEntity.WantsPartialDelivery)
                        {
                            partialDelivery.PartialDeliveryProductRelations.Add(PartialDeliveryProductRelationFactory
                                .CreatePartialDeliveryProductRelation(partialDelivery, retrievedProduct, 
                                retrievedProduct.Count == 0 ? product.Count : product.Count - retrievedProduct.Count));

                            retrievedProduct.Count = 0;
                        }

                        else throw new SoldOutException("Product sold out.", orderEntity.Id.ToString(), retrievedProduct.Id.ToString());
                    }

                    else
                    {
                        retrievedProduct.Count -= product.Count;
                        orderEntity.ProductOrderRelations.Add(_productOrderRelationFactory
                            .CreateRelation(retrievedProduct, orderEntity, product.Count));
                    } 
                }

                if (orderEntity.WantsPartialDelivery && partialDelivery.PartialDeliveryProductRelations.Any())
                {
                    orderEntity.PartialDelivery = partialDelivery;
                    orderEntity.PartialDeliveryId = partialDelivery.Id;
                    partialDelivery.User = user;
                    partialDelivery.UserId = user.Id;

                    _webStoreContext.PartialDeliveries.Add(partialDelivery);
                }

                orderEntity.CalculatePrize();
                _webStoreContext.Orders.Add(orderEntity);

                foreach (var relation in orderEntity.ProductOrderRelations)
                    _webStoreContext.ProductOrderRelations.Add(relation);

                await _webStoreContext.SaveChangesAsync();
                return _orderReceiptFactory.CreateOrderReceipt(orderEntity.Id, dto.Products);
            }

            else throw new Exception("Order object is null.");
        }

        public async Task<List<OrderDTO>> ConvertOrderListToOrderDTOs(List<Order> orders)
        {
            var orderDTOs = new List<OrderDTO>();
            foreach (var order in orders)
            {
                var productsDtos = new List<ProductDTO>();

                foreach (var relation in order.ProductOrderRelations)
                    productsDtos.Add(await _dtoMapperService.MapToDTO<Product, ProductDTO>(relation.Product));

                var orderDTO = await _dtoMapperService.MapToDTO<Order, OrderDTO>(order);
                orderDTO.Products = productsDtos;
                orderDTOs.Add(orderDTO);
            }

            return orderDTOs;
        }

        public async Task<AdvanceOrderStageConfirmation> AdvanceOrderStage(string orderId, UserEntity user)
        {
            var order = _webStoreContext.Orders.FirstOrDefault(o => o.Id.ToString().Equals(orderId))
                ?? throw new Exception("Order not found.");

            var advancedByAdmin = await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin);

            var advancedStageConfirmation = order.AdvanceStage(advancedByAdmin);

            await _webStoreContext.SaveChangesAsync();
            return advancedStageConfirmation;
        }
    }
}
