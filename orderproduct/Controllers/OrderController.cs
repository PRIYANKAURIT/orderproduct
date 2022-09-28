using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using orderproduct.Model;
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

        [HttpGet("/id")]
        public async Task<IActionResult> Getorderbyid(int id)
        {
            try
            {
                var Order = await _orderRepository.Getorderbyid(id);
                return Ok(Order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insertorder(Order order)
        {
            try
            {
                var res = await _orderRepository.Insertorder(order);
                return StatusCode(200, "Inserted Succefully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Updateorder(Order order)
        {
            try
            {
                var res = await _orderRepository.Updateorder(order);
                return StatusCode(200, "updated Succefully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
          
        [HttpDelete]
        public async Task<IActionResult> Deleteorder(int id)
        {
            try
            {
                var res = await _orderRepository.Deleteorder(id);
                return StatusCode(200, "deleted Succefully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
/*{
    "oid": 0,
      "pid": 1,
      "pname": "pen",
      "qty": 5,
      "price": 10,
      "totalamt": 0
    },
{
    "oid": 0,
      "pid": 2,
      "pname": "chocolate",
      "qty": 5,
      "price": 100,
      "totalamt": 0
    },
{
    "oid": 0,
      "pid": 3,
      "pname": "bag",
      "qty": 2,
      "price": 30,
      "totalamt": 0
    }*/