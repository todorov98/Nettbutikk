using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nettbutikk.Data.DTO;
using Nettbutikk.Data.Services;
using Nettbutikk.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nettbutikk.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly DtoMapperService _dtoMapperService;
        private readonly DiscountService _discountService;

        public ProductController(ProductService productService, DtoMapperService dtoMapperService,
            DiscountService discountService)
        {
            _productService = productService;
            _dtoMapperService = dtoMapperService;
            _discountService = discountService;
        }

        [HttpGet]
        [Route("Products/GetProduct")]
        [Authorize]
        public async Task<IActionResult> GetProductOnId([FromBody] string id)
        {
            try
            {
                var product = await _productService.GetProductOnId(id);
                var dto = await _dtoMapperService.MapToDTO<Product, ProductDTO>(product);
                return Ok(dto);
            }

            catch(Exception)
            {
                return StatusCode(404);
            }
        }

        [HttpGet]
        [Route("Products/GetProducts")]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProducts();
                var productDTOs = new List<ProductDTO>();
                var discountDTOs = new List<DiscountDTO>();

                foreach (var product in products)
                {
                    productDTOs.Add(await _dtoMapperService.MapToDTO<Product, ProductDTO>(product));
                    var hasDiscount = _discountService.CheckIfHasDiscount(product.Id);

                    if (hasDiscount)
                    {
                        var dc = await _discountService.GetDiscountForProduct(product.Id);
                        discountDTOs.Add(dc);
                    }
                }

                var response = new
                {
                    Products = productDTOs,
                    Discounts = discountDTOs
                };

                return Ok(response);
            }

            catch(ArgumentException e)
            {
                if (e.Message.Equals("Discount not found"))
                    return StatusCode(404, "Could not find discount for a product.");

                return StatusCode(500, e.Message);
            }

            catch(Exception e)
            {
                return StatusCode(404, e.Message);
            }       
        }

        [HttpPost]
        [Route("Products/AddProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProductToDatabase([FromBody] ProductDTO productDTO)
        {
            try
            {
                await _productService.AddProduct(productDTO);

                return Ok();
            }

            catch(Exception)
            {
                return StatusCode(404, "Something went wrong. Contact support.");
            }
        }
    }
}