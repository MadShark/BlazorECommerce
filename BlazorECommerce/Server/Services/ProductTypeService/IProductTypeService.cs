namespace BlazorECommerce.Server.Services.ProductTypeService
{
    public interface IProductTypeService
    {
        Task<ServiceResponse<List<ProductType>>> GetProductTypesAsync();
        Task<ServiceResponse<List<ProductType>>> AddProductTypeAsync(ProductType ProductType);
        Task<ServiceResponse<List<ProductType>>> UpdateProductTypeAsync(ProductType ProductType);
    }
}
