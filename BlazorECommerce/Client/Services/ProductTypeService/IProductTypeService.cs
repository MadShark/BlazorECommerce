namespace BlazorECommerce.Client.Services.ProductTypeService
{
    public interface IProductTypeService
    {
        event Action OnChangeProductType;
        List<ProductType> ProductTypes { get; set; }


        Task GetProductTypes();
        Task AddProductType(ProductType ProductType);
        Task UpdateProductType(ProductType ProductType);
        ProductType CreateNewProductType();
    }
}
