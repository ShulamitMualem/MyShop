using Entity;

namespace Repository.ProductsRepo
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetProducts();
    }
}