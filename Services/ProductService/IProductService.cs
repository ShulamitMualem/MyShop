using Entity;

namespace Services.ProductService
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
    }
}