using Nettbutikk.Data.DTO;
using Nettbutikk.Data.Services;
using Nettbutikk.Models;
using Nettbutikk.State;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nettbutikk.Factories
{
    public class OrderFactory
    {
        private readonly DtoMapperService _dtoMapperService;

        public OrderFactory(DtoMapperService dtoMapperService)
        {
            _dtoMapperService = dtoMapperService;
        }

        public Order CreateEmptyOrder()
        {
            return new Order();
        }

        public async Task<Order> CreateAndInitializeOrderFromDTO(OrderDTO dto, string userId)
        {
            var orderEntity = await _dtoMapperService.MapFromDTO<Order, OrderDTO>(dto);

            orderEntity.DatePlaced = DateTime.Now.AddMilliseconds(-5.0);
            orderEntity.DateFulfilled = null;
            orderEntity.Stage = OrderStages.Received;
            orderEntity.UserId = userId;
            orderEntity.Id = Guid.NewGuid();
            orderEntity.ProductOrderRelations = new List<ProductOrderRelation>();

            return orderEntity;
        }
    }
}
