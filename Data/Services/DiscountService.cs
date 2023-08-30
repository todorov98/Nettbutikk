using Nettbutikk.Data.DTO;
using Nettbutikk.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nettbutikk.Data.Services
{
    public class DiscountService
    {
        private readonly WebStoreContext _webStoreContext;
        private readonly DtoMapperService _dtoMapperService;

        public DiscountService(WebStoreContext webStoreContext, DtoMapperService dtoMapperService)
        {
            _webStoreContext = webStoreContext;
            _dtoMapperService = dtoMapperService;
        }

        public async Task AddDiscount(DiscountDTO discountDTO)
        {
            var discountEntity = await _dtoMapperService.MapFromDTO<Discount, DiscountDTO>(discountDTO);
            discountEntity.Active = true;

            _webStoreContext.Discounts.Add(discountEntity);
            await _webStoreContext.SaveChangesAsync();
        }

        public bool CheckIfHasDiscount(Guid productId)
        {
            return _webStoreContext.Discounts.Any(dc => dc.ProductId == productId && dc.ExpirationDate > DateTime.Now);
        }

        public async Task<DiscountDTO> GetDiscountForProduct(Guid productId)
        {
            var discount = _webStoreContext.Discounts.FirstOrDefault(dc => dc.ProductId == productId)
                        ?? throw new Exception("Discount not found");

            return await _dtoMapperService.MapToDTO<Discount, DiscountDTO>(discount);
        }
    }
}
