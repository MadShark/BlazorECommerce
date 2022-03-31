namespace BlazorECommerce.Server.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetAllCategoriesAsync();
        Task<ServiceResponse<List<Category>>> GetAdminCategoriesAsync();
        Task<ServiceResponse<List<Category>>> AddCategoryAsync(Category Category);
        Task<ServiceResponse<List<Category>>> UpdateCategoryAsync(Category Category);
        Task<ServiceResponse<List<Category>>> DeleteCategoryAsync(int CategoryId);
    }
}
