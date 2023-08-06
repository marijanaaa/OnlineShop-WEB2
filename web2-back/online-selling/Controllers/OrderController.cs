using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using online_selling.Dto;
using online_selling.Interfaces.Orders;
using online_selling.Models;

namespace online_selling.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                var result = await _orderService.AddOrder(orderDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //[HttpPut("updateStatus")]
        //public async Task<IActionResult> UpdateStatus([FromBody] int status)
        //{
        //    try
        //    {
        //        var result = await _orderService.UpdateStatus(status);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
