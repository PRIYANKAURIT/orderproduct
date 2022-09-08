using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using orderproduct.Repository;
using orderproduct.Repository.Interface;

namespace orderproduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]

        public async Task<IActionResult> Getorders()
        {
            try
            {
                var Order = await _orderRepository.Getorders();
                return Ok(Order);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
