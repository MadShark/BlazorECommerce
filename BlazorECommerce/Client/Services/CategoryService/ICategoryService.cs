namespace BlazorECommerce.Client.Services.CategoryService
{
    public interface ICategoryService
    {
        event Action OnChangeCategory;
        public List<Category> listCategories { get; set; }
        public List<Category> adminCategories { get; set; }



        Task GetAllCategories();
        Task GetAdminCategories();
        Task AddCategory(Category Category);
        Task UpdateCategory(Category Category);
        Task DeleteCategory(int CategoryId);
        Category CreateNewCategory();
    }
}
