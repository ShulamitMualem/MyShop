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
        MyShop328264650Context _dbcontext;
        public CategoryRepository(MyShop328264650Context context)
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
