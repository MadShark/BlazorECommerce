namespace BlazorECommerce.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Category>>> GetAllCategoriesAsync()
        {
            var categoryList = await _context.Categories.ToListAsync();
            var response = new ServiceResponse<List<Category>>() {
                Data = categoryList
            };

            return response;
        }

        public async Task<ServiceResponse<Category>> GetCategoryByIdAsync(int CategoryId)
        {
            var response = new ServiceResponse<Category>();
            var category = await _context.Categories.FindAsync(CategoryId);

            if (category == null)
            {
                response.Success = false;
                response.Message = "The Category was not found";
            }
            else
                response.Data = category;

            return response;
        }
    }
}
