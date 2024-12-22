using Entity;

namespace Repository.CategoryRepo
{
    public interface ICategoryRepository
    {

        Task<List<Category>> GetAllCategories();
    }
}