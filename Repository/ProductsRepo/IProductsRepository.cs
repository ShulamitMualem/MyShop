using Entity;

namespace Repository.ProductsRepo
{
    public interface IProductsRepository
    {
        Task<List<Product>> Get(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}