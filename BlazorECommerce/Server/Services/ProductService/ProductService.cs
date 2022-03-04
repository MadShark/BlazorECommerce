namespace BlazorECommerce.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        public ProductService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<Product>>> GetAllProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>() {
                Data = await _context.Products.ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductByIdAsync(int ProductId)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products.FindAsync(ProductId);

            if (product == null)
            {
                response.Success = false;
                response.Message = "The product couldn't be found";
            }
            else
                response.Data = product;

            return response;
        }
    }
}
