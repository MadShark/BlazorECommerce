namespace BlazorECommerce.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public event Action OnChangeCategory;
        public List<Category> listCategories { get; set; } = new List<Category>();
        public List<Category> adminCategories { get; set; } = new List<Category>();


        public async Task GetAllCategories()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category");
            if (response != null && response.Data != null)
                listCategories = response.Data;
        }

        public async Task GetAdminCategories()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category/admin");
            if (response != null && response.Data != null)
                adminCategories = response.Data;
        }

        public async Task AddCategory(Category Category)
        {
            var response = await _httpClient.PostAsJsonAsync("api/category/admin", Category);
            adminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;

            await GetAllCategories();
            OnChangeCategory.Invoke();
        }

        public async Task UpdateCategory(Category Category)
        {
            var response = await _httpClient.PutAsJsonAsync("api/category/admin", Category);
            adminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;

            await GetAllCategories();
            OnChangeCategory.Invoke();
        }

        public async Task DeleteCategory(int CategoryId)
        {
            var response = await _httpClient.DeleteAsync($"api/category/admin/{CategoryId}");
            adminCategories = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;

            await GetAllCategories();
            OnChangeCategory.Invoke();
        }

        public Category CreateNewCategory()
        {
            var newCategory = new Category { IsNew = true, Editing = true };
            adminCategories.Add(newCategory);

            OnChangeCategory.Invoke();
            return newCategory;
        }
    }
}
