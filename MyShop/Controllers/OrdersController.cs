using AutoMapper;
using DTO;
using Entity;
using Microsoft.AspNetCore.Mvc;
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
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get(int id)
        {
            Order order = await _orderService.GetOrderById(id);
            OrderDTO orderDTO= _mapper.Map<Order, OrderDTO>(order);
            return orderDTO == null ? NoContent() : Ok(orderDTO);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post(CreateOrderDTO newOrderDTO)
        {
            Order newOrder = _mapper.Map<CreateOrderDTO, Order>(newOrderDTO);
            Order order = await _orderService.CreateOrder(newOrder);
            OrderDTO orderDTO = _mapper.Map<Order, OrderDTO>(order);
            return orderDTO == null ? NoContent() : Ok(orderDTO);
        }
    }
}
