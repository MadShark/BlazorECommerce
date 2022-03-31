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
        public List<Product> adminProducts { get; set; } = new List<Product>();
        public string Message { get; set; } = "Loading Products...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;

        public event Action OnProductsChanged;


        public async Task GetAllProducts(string? CategoryUrl = null)
        {
            var result = CategoryUrl == null 
                        ? await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/featured") 
                        : await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{CategoryUrl}");
            if (result != null && result.Data != null)
                listProducts = result.Data;

            CurrentPage = 1;
            PageCount = 0;

            if (listProducts.Count == 0)
                Message = "No products found";

            OnProductsChanged.Invoke();
        }

        public async Task GetAdminProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/admin");
            adminProducts = result.Data;
            CurrentPage = 1;
            PageCount = 0;
            if (adminProducts.Count == 0)
                Message = "No products found.";
        }

        public async Task<ServiceResponse<Product>> GetProductById(int ProductId)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{ProductId}");
            return result;
        }

        public async Task SearchProductsByFilter(string FilterSearch, int PageNumber)
        {
            LastSearchText = FilterSearch;
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<ProductSearchResultDTO>>($"api/product/search/{FilterSearch}/{PageNumber}");

            if (result != null && result.Data != null)
            {
                listProducts = result.Data.ListProducts;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            if (listProducts.Count == 0)
                Message = "No products found";

            OnProductsChanged.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string SuggestionSearch)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/suggestions/{SuggestionSearch}");
            return result.Data;
        }

        public async Task<Product> CreateProduct(Product Product)
        {
            var result = await _httpClient.PostAsJsonAsync("api/product", Product);
            var newProduct = (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>()).Data;
            return newProduct;
        }

        public async Task<Product> UpdateProduct(Product Product)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/product", Product);
            return (await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>()).Data;
        }

        public async Task DeleteProduct(Product Product)
        {
            var result = await _httpClient.DeleteAsync($"api/product/{Product.Id}");
        }


    }
}
