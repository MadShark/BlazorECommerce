namespace BlazorECommerce.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public List<Category> listCategories { get; set; } = new List<Category>();


        public async Task GetAllCategories()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category");
            if (response != null && response.Data != null)
                listCategories = response.Data;
        }


        public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string CategoryUrl)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/{CategoryUrl}");
            return response;
        }
    }
}
