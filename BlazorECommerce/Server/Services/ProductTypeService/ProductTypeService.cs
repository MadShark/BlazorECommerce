namespace BlazorECommerce.Server.Services.ProductTypeService
{
    public class ProductTypeService : IProductTypeService
    {
         private readonly DataContext _context;

        public ProductTypeService(DataContext context)
        {
            _context = context;
        }


        public async Task<ServiceResponse<List<ProductType>>> GetProductTypesAsync()
        {
            var productTypes = await _context.ProductTypes.ToListAsync();
            return new ServiceResponse<List<ProductType>> { Data = productTypes };
        }

        public async Task<ServiceResponse<List<ProductType>>> AddProductTypeAsync(ProductType ProductType)
        {
            ProductType.Editing = ProductType.IsNew = false;
            _context.ProductTypes.Add(ProductType);
            await _context.SaveChangesAsync();

            return await GetProductTypesAsync();
        }

        public async Task<ServiceResponse<List<ProductType>>> UpdateProductTypeAsync(ProductType ProductType)
        {
            var dbProductType = await _context.ProductTypes.FindAsync(ProductType.Id);
            if (dbProductType == null)
            {
                return new ServiceResponse<List<ProductType>>
                {
                    Success = false,
                    Message = "Product type not found."
                };
            }
            dbProductType.Name = ProductType.Name;
            await _context.SaveChangesAsync();

            return await GetProductTypesAsync();
        }
    }
}
