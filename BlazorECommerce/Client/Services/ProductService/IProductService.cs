namespace BlazorECommerce.Client.Services.ProductService
{
    public interface IProductService
    {
        List<Product> ListProducts { get; set; }
        Task GetAllProducts();
    }
}
