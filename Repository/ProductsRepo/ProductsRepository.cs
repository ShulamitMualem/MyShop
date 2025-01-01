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
        public async Task<List<Product>> GetProducts(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            var query = _dbcontext.Products.Where(product =>
            (desc == null ? (true) : (product.Description.Contains(desc)))
            && (minPrice == null ? (true) : (product.Price >= minPrice))
            && (maxPrice == null ? (true) : (product.Price <= maxPrice))
            && (categoryIds.Length == 0 ? (true) : (categoryIds.Contains(product.CategoryId)))
            .OrderBy(product => product.price);

            List<Product> products
        }
    }
}
