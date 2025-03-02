using AutoMapper;
using DTO;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        IMemoryCache _cache;


        public ProductsController(IProductService productService,  IMapper mapper, IMemoryCache cache)
        {
            _productService = productService;
            _mapper = mapper;
            _cache = cache;
        }
        [HttpGet]
        // GET: api/<ProductsController>
        public async Task<ActionResult<List<Product>>> Get([FromQuery]int position, [FromQuery] int skip, [FromQuery] string? nameSearch, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoryIds)
        {
            if (!_cache.TryGetValue("products", out List<Product> products))
            {
                products = await _productService.Get(position, skip, nameSearch, minPrice, maxPrice, categoryIds);
                _cache.Set("products", products, TimeSpan.FromMinutes(10));
            }

            List<ProductDTO> productsDTOs = _mapper.Map<List<Product>, List<ProductDTO>>(products);
            return products != null ? Ok(productsDTOs) : NoContent();
        }
    }
}
