using Entity;
using Repository;
using Repository.CategoryRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _CategoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _CategoryRepository = categoryRepository;
        }
        public Task<List<Category>> GetAllCategories()
        {
            return _CategoryRepository.GetAllCategories();
        }
        
    }
}
