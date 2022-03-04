namespace BlazorECommerce.Client.Services.ProductService
{
    public interface IProductService
    {
        // Properties
        List<Product> listProducts { get; set; }

        // Methods
        Task GetAllProducts();
        Task<ServiceResponse<Product>> GetProductById(int ProductId);
    }
}
