using Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.CategoryRepo;
using Services;
using Services.CategoryService;
using AutoMapper;
using DTO;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        ICategoryService _categoryService;
        IMapper _mapper;
        IMemoryCache _cache;
        
        public CategoriesController(ICategoryService categoryService, IMapper mapper, IMemoryCache cache)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _cache = cache;
        }
        // GET: api/<CategoriesController>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            if (!_cache.TryGetValue("categories", out List<Category> categories))
            {
                categories = await _categoryService.GetAllCategories();
                _cache.Set("categories", categories, TimeSpan.FromMinutes(10));
            }
            List<CategoryDTO> categoryDTOs = _mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            return categories != null ? Ok(categoryDTOs) : NoContent();
        }
    }
}
