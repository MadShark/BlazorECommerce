namespace BlazorECommerce.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        public List<Category> listCategories { get; set; }

        Task GetAllCategories();
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string CategoryUrl);
    }
}
