namespace BlazorECommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }   


        public List<Product> listProducts { get; set; } = new List<Product>();
        public string Message { get; set; } = "Loading Products...";

        public event Action OnProductsChanged;


        public async Task GetAllProducts(string? CategoryUrl = null)
        {
            var result = CategoryUrl == null 
                        ? await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product") 
                        : await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{CategoryUrl}");
            if (result != null && result.Data != null)
                listProducts = result.Data;

            OnProductsChanged.Invoke();
        }

        public async Task<ServiceResponse<Product>> GetProductById(int ProductId)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{ProductId}");
            return result;
        }

        public async Task SearchProductsByFilter(string FilterSearch)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/search/{FilterSearch}");

            if (result != null && result.Data != null)
                listProducts = result.Data;

            if (listProducts.Count == 0)
                Message = "No products found";

            OnProductsChanged.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string SuggestionSearch)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/suggestions/{SuggestionSearch}");
            return result.Data;
        }
    }
}
