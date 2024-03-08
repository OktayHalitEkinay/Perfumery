using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.Brand;
using Entities.Dtos.Order;
using Entities.Dtos.Perfume;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _orderService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("getorderbyuserdetailid")]
        public IActionResult GetOrderByUserDetailId(int userDetailId)
        {
            var result = _orderService.GetOrderByUserDetailId(userDetailId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPost("createorder")]
        public IActionResult CreateOrder(CreateOrderDto createOrderDto)
        {
            var result = _orderService.CreateOrder(_mapper.Map<Order>(createOrderDto));
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
