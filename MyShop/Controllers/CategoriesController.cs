using Entity;
using Microsoft.AspNetCore.Mvc;
using Repository.CategoryRepo;
using Services;
using Services.CategoryService;
using AutoMapper;
using DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService _categoryService;
        IMapper _mapper;
        
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            List<Category> category = await _categoryService.GetAllCategories();

            List<CategoryDTO> categoryDTOs = _mapper.Map<List<Category>, List<CategoryDTO>>(category);
            return category != null ? Ok(categoryDTOs) : NoContent();
        }
    }
}
