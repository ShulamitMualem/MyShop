using Entity;

namespace Services.CategoryService
{
    public interface ICategoryService
    {

        Task<List<Category>> GetAllCategories();
    }
}