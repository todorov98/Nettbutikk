using Nettbutikk.Data.DTO;
using Nettbutikk.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nettbutikk.Data.Services
{
    public class ProductService
    {
        private readonly WebStoreContext _webStoreContext;
        private readonly DtoMapperService _dtoMapperService;

        public ProductService(WebStoreContext webStoreContext, DtoMapperService dtoMapperService)
        {
            _webStoreContext = webStoreContext;
            _dtoMapperService = dtoMapperService;
        }

        /// <summary>
        /// Gets all the products from the stores database.
        /// </summary>
        /// <returns>All the products as a list.</returns>
        public Task<List<Product>> GetAllProducts()
        {
            return Task.FromResult(_webStoreContext.Products.Select(p => p).ToList());
        }

        public Task<Product> GetProductOnId(string id)
        {
            return Task.FromResult(_webStoreContext.Products.FirstOrDefault(p => p.Id.ToString().Equals(id)));
        }

        /// <summary>
        /// Adds a product to the store database. This metod is only used by admin users.
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns>Task.</returns>
        public async Task AddProduct(ProductDTO productDTO)
        {
            var productEntity = await _dtoMapperService.MapFromDTO<Product, ProductDTO>(productDTO);
            _webStoreContext.Products.Add(productEntity);
            await _webStoreContext.SaveChangesAsync();
        }
    }
}
