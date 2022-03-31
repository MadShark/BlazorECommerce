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
            var categoryList = await _context.Categories.Where(c => !c.Deleted && c.Visible).ToListAsync();
            var response = new ServiceResponse<List<Category>>() {
                Data = categoryList
            };

            return response;
        }

        public async Task<ServiceResponse<List<Category>>> GetAdminCategoriesAsync()
        {
            var categoryList = await _context.Categories.Where(c => !c.Deleted).ToListAsync();
            var response = new ServiceResponse<List<Category>>()
            {
                Data = categoryList
            };

            return response;
        }

        public async Task<ServiceResponse<List<Category>>> AddCategoryAsync(Category Category)
        {
            Category.Editing = Category.IsNew = false;
            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();
            return await GetAdminCategoriesAsync();
        }

        public async Task<ServiceResponse<List<Category>>> UpdateCategoryAsync(Category Category)
        {
            Category dbCategory = await GetCategoryById(Category.Id);
            if (dbCategory == null)
            {
                return new ServiceResponse<List<Category>>
                {
                    Success = false,
                    Message = "Category not found"
                };
            }

            dbCategory.Name = Category.Name;
            dbCategory.Url = Category.Url;
            dbCategory.Visible = Category.Visible;

            await _context.SaveChangesAsync();

            return await GetAdminCategoriesAsync();
        }

        public async Task<ServiceResponse<List<Category>>> DeleteCategoryAsync(int CategoryId)
        {
            Category category = await GetCategoryById(CategoryId);
            if (category == null)
            {
                return new ServiceResponse<List<Category>>
                {
                    Success = false,
                    Message = "Category not found"
                };
            }

            category.Deleted = true;
            await _context.SaveChangesAsync();

            return await GetAdminCategoriesAsync();
        }

        private async Task<Category> GetCategoryById(int CategoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == CategoryId);
        }
    }
}
