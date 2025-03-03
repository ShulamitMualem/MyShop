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



        public ProductsController(IProductService productService, IMapper mapper, IMemoryCache cache)
        {
            _productService = productService;
            _mapper = mapper;
            _cache = cache;
        }
        [HttpGet]
        // GET: api/<ProductsController>
        public async Task<ActionResult<List<Product>>> Get([FromQuery]int position, [FromQuery] int skip, [FromQuery] string? nameSearch, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoryIds)
        {
            string cacheKey = $"products_{position}_{skip}_{nameSearch}_{minPrice}_{maxPrice}_{string.Join(",", categoryIds)}";

            if (!_cache.TryGetValue(cacheKey, out List<ProductDTO> productsDTOs))
            {
                List<Product> products = await _productService.Get(position, skip, nameSearch, minPrice, maxPrice, categoryIds);
                productsDTOs = _mapper.Map<List<Product>, List<ProductDTO>>(products);
                _cache.Set(cacheKey, productsDTOs, TimeSpan.FromMinutes(10));
            }

            return productsDTOs == null ? NoContent() : Ok(productsDTOs);
        }
    }
}
