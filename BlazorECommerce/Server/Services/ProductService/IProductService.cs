namespace BlazorECommerce.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetAllProductsAsync();
        Task<ServiceResponse<Product>> GetProductByIdAsync(int ProductId);
        Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string CategoryUrl);
        Task<ServiceResponse<List<Product>>> SearchProductsByFilterAsync(string FilterSearch);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestionsAsync(string FilterSearch);
    }
}
