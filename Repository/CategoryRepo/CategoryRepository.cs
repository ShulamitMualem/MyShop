using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CategoryRepo
{
    public class CategoryRepository : ICategoryRepository
    {
        MyShopContext _dbcontext;
        public CategoryRepository(MyShopContext context)
        {
            _dbcontext = context;
        }
        public async Task<List<Category>> GetAllCategories()
        {
            List<Category> categories = await _dbcontext.Categories.ToListAsync();
            return categories;
        }
        
    }
}
