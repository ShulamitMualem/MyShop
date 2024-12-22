using AutoMapper;
using DTO;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Services.ProductService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        IMapper _mapper;
        public ProductsController(IProductService productService,  IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet]
        // GET: api/<ProductsController>
        public async Task<ActionResult<List<ProductDTO>>> GetProducts()
        {
            List<Product> products = await _productService.GetAllProducts();
            List<ProductDTO> productsDTOs = _mapper.Map<List<Product>, List<ProductDTO>>(products);
            return products != null ? Ok(productsDTOs) : NoContent();
        }
    }
}
