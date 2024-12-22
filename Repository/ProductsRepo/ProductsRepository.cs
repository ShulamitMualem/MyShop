using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ProductsRepo
{
    public class ProductsRepository : IProductsRepository
    {
        MyShop328264650Context _dbcontext;
        public ProductsRepository(MyShop328264650Context context)
        {
            _dbcontext = context;
        }
        public async Task<List<Product>> GetProducts()
        {
            List<Product> categories = await _dbcontext.Products.Include(product=>product.Caregory).ToListAsync();
            return categories == null ? null : categories;
        }
    }
}
