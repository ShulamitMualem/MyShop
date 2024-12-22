using Entity;
using Repository.CategoryRepo;
using Repository.ProductsRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductService
{
    public class ProductService : IProductService
    {
        IProductsRepository _productsRepository;
        public ProductService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await _productsRepository.GetProducts();
        }
    }
}
