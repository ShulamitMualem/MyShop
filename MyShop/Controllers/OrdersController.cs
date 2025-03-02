using AutoMapper;
using DTO;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Services.OrderService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _orderService;
        IMapper _mapper;
        IMemoryCache _cache;

        public OrdersController(IOrderService orderService, IMapper mapper,IMemoryCache cache)
        {
            _orderService = orderService;
            _mapper = mapper;
            _cache = cache;
        }
        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            if (!_cache.TryGetValue("order", out Order order))
            {
                order = await _orderService.GetOrderById(id);
                _cache.Set("order", order, TimeSpan.FromMinutes(10));
            }
            OrderDTO orderDTO= _mapper.Map<Order, OrderDTO>(order);
            return orderDTO == null ? NoContent() : Ok(orderDTO);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post(CreateOrderDTO newOrderDTO)
        {
            Order newOrder = _mapper.Map<CreateOrderDTO, Order>(newOrderDTO);
            Order order = await _orderService.CreateOrder(newOrder);
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO == null ? BadRequest() : CreatedAtAction(nameof(Get),new { id = newOrder.OrderId },newOrder);
        }
    }
}
