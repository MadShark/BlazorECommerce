namespace BlazorECommerce.Client.Services.ProductService
{
    public interface IProductService
    {
        // Properties
        event Action OnProductsChanged;
        List<Product> listProducts { get; set; }
        string Message { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }


        // Methods
        Task GetAllProducts(string? CategoryUrl = null);
        Task<ServiceResponse<Product>> GetProductById(int ProductId);
        Task SearchProductsByFilter(string FilterSearch, int PageNumber);
        Task<List<string>> GetProductSearchSuggestions(string SuggestionSearch);
    }
}
