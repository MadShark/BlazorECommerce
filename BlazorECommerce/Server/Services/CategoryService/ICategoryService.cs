namespace BlazorECommerce.Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetAllCategoriesAsync();

        Task<ServiceResponse<Category>> GetCategoryByIdAsync(int CategoryId);
    }
}
