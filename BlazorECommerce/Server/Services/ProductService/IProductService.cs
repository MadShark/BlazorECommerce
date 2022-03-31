namespace BlazorECommerce.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetAllProductsAsync();
        Task<ServiceResponse<List<Product>>> GetAdminProductsAsync();
        Task<ServiceResponse<Product>> GetProductByIdAsync(int ProductId);
        Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string CategoryUrl);
        Task<ServiceResponse<ProductSearchResultDTO>> SearchProductsByFilterAsync(string FilterSearch, int PageNumber);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestionsAsync(string FilterSearch);
        Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync();
        Task<ServiceResponse<Product>> CreateProductAsync(Product Product);
        Task<ServiceResponse<Product>> UpdateProductAsync(Product Product);
        Task<ServiceResponse<bool>> DeleteProductAsync(int ProductId);
    }
}
