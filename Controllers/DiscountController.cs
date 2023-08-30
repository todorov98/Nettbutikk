using Microsoft.AspNetCore.Mvc;
using Nettbutikk.Data.DTO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Nettbutikk.Data.Services;

namespace Nettbutikk.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DiscountController : ControllerBase
    {
        private readonly DiscountService _discountService;

        public DiscountController(DiscountService discountService)
        {
            _discountService = discountService;
        }

        public async Task<IActionResult> AddDiscount(DiscountDTO discountDTO)
        {
            try
            {
                await _discountService.AddDiscount(discountDTO);
                return Ok();
            }

            catch(Exception e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}
