using Microsoft.AspNetCore.Mvc;
using Nettbutikk.Models;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nettbutikk.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Nettbutikk.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Nettbutikk.CustomExceptions;

namespace Nettbutikk.Controllers
{
    /// <summary>
    /// Controller that controls the creation and interaction with orders.
    /// </summary>
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly UserContextService _userContextService;
        private readonly ProductService _productService;
        private readonly DtoMapperService _dtoMapperService;

        public OrderController(OrderService orderService, UserContextService userContextService, ProductService productService,
            DtoMapperService dtoMapperService)
        {
            _orderService = orderService;
            _userContextService = userContextService;
            _productService = productService;
            _dtoMapperService = dtoMapperService;
        }

        [Route("Orders/GetOrdersOnProduct")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrdersForCustomerOnProduct([FromBody] string id)
        {
            try
            {
                var user = await _userContextService.GetCurrentUserOnHttpContext(HttpContext)
                    ?? throw new Exception("User not found");

                var orders = await _orderService.CheckIfProductWasOrderedBefore(user.Id, Guid.Parse(id));
                var orderDTOs = await _orderService.ConvertOrderListToOrderDTOs(orders);

                return Ok(orderDTOs);
            }

            catch(Exception e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [Route("Orders/GetCompletedOrders")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCompletedOrders()
        {
            try
            {
                var user = await _userContextService.GetCurrentUserOnHttpContext(HttpContext)
                    ?? throw new Exception("User not found");

                var orders = await _orderService.GetAllCompletedOrdersOnCustomerId(user.Id);
                var orderDTOs = await _orderService.ConvertOrderListToOrderDTOs(orders);

                return Ok(orderDTOs);
            }

            catch(Exception e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [Route("Orders/GetAllOrders")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var user = await _userContextService.GetCurrentUserOnHttpContext(HttpContext)
                    ?? throw new Exception("User not found");

                var orders = await _orderService.GetAllOrdersOnUserId(user.Id);
                var orderDTOs = await _orderService.ConvertOrderListToOrderDTOs(orders);

                return Ok(orderDTOs);
            }

            catch(Exception e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [Route("Orders/GetOrder")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrder([FromBody] string id)
        {
            try
            {
                var user = await _userContextService.GetCurrentUserOnHttpContext(HttpContext)
                    ?? throw new Exception("User not found");

                var order = await _orderService.GetOrderOnId(id);
                var orderDTO = await _dtoMapperService.MapToDTO<Order, OrderDTO>(order);

                return Ok(orderDTO);
            }

            catch(Exception e)
            {
                if (e.Message.Equals("Order not found."))
                    return StatusCode(404, "Order not found.");

                else return StatusCode(404, e.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [Route("Orders/PlaceOrder")]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderDTO orderDTO)
        {
            try
            {
                var user = await _userContextService.GetCurrentUserOnHttpContext(HttpContext)
                    ?? throw new Exception("User not found");

                var orderReceipt = await _orderService.PlaceOrder(orderDTO, user);
                return Ok(orderReceipt);
            }

            catch (SoldOutException e)
            {
                if (e.Message.Equals("Product sold out."))
                {
                    var soldOutProduct = await _productService.GetProductOnId(e.ProductId);

                    var response = new
                    {
                        Message = "Product you attempted to order is sold out. Order was cancelled.",
                        ProductName = soldOutProduct.Name,
                        ProductId = soldOutProduct.Id,
                        Date = DateTime.Now
                    };

                    return StatusCode(200, response);
                }

                else return StatusCode(404, "Something went wrong.");
            }

            catch (Exception e)
            {
                if (e.Message.Equals("User not found"))
                    return StatusCode(404, "User could not be found. Check if authenticated and try again or contact customer service.");

                if (e.Message.Equals("Order object is null."))
                    return StatusCode(404, "Order object is null. Try again.");

                if (e.Message.Equals("Invalid product count."))
                    return StatusCode(422, "Invalid product count in order. Enter valid number of products.");

                else return StatusCode(404, e.Message);
            }
        }

        [Authorize(Roles = "Customer, Admin")]
        [Route("Orders/CancelOrder")]
        [HttpPut]
        public async Task<IActionResult> CancelOrder([FromBody] string orderId)
        {
            try
            {
                var user = await _userContextService.GetCurrentUserOnHttpContext(HttpContext)
                    ?? throw new Exception("User not found");

                var cancelConfirmation = await _orderService.CancelOrder(orderId, user);

                return Ok(cancelConfirmation);
            }

            catch(Exception e)
            {
                return StatusCode(404, e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("Orders/AdvanceStage")]
        [HttpPut]
        public async Task<IActionResult> AdvanceOrderStage([FromBody] string orderId)
        {
            try
            {
                var user = await _userContextService.GetCurrentUserOnHttpContext(HttpContext)
                    ?? throw new Exception("User not found");

                var advanceOrderConfirmation = await _orderService.AdvanceOrderStage(orderId, user);
                return Ok(advanceOrderConfirmation);
            }

            catch(Exception e)
            {
                return StatusCode(404, e.Message);
            }
        }
    }
}