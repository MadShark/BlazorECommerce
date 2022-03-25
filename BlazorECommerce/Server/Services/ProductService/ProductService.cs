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
                Data = await _context.Products
                                  .Include(p => p.Variants)
                                  .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductByIdAsync(int ProductId)
        {
            var response = new ServiceResponse<Product>();
            var product = await _context.Products
                                    .Include(p => p.Variants)
                                    .ThenInclude(v => v.ProductType)
                                    .FirstOrDefaultAsync(p => p.Id == ProductId);

            if (product == null)
            {
                response.Success = false;
                response.Message = "The product couldn't be found";
            }
            else
                response.Data = product;

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string CategoryUrl)
        {
            var response = new ServiceResponse<List<Product>>() { 
                Data = await _context.Products
                                        .Where(x => x.Category.Url.Equals(CategoryUrl))
                                        .Include(p => p.Variants)
                                        .ToListAsync(),
                Success = true
            };

            return response;
        }

        public async Task<ServiceResponse<ProductSearchResultDTO>> SearchProductsByFilterAsync(string FilterSearch, int PageNumber)
        {
            FilterSearch = FilterSearch.ToLower();
            var pageResults = 2f;
            var pageCount = Math.Ceiling((await FindProductBySearchText(FilterSearch)).Count / pageResults);
            var products = await _context.Products
                                        .Where(p => p.Title.ToLower().Contains(FilterSearch) || p.Description.ToLower().Contains(FilterSearch))
                                        .Include(p => p.Variants)
                                        .Skip((PageNumber - 1) * (int)pageResults)
                                        .Take((int)pageResults)
                                        .ToListAsync();

            var response = new ServiceResponse<ProductSearchResultDTO> { 
                Data = new ProductSearchResultDTO { 
                    ListProducts = products,
                    CurrentPage = PageNumber,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestionsAsync(string SuggestionSearch)
        {
            var products = await FindProductBySearchText(SuggestionSearch);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(SuggestionSearch, StringComparison.OrdinalIgnoreCase))
                    result.Add(product.Title);

                if (product.Description != null)
                {
                    var punctuation = product.Description
                                           .Where(char.IsPunctuation)
                                           .Distinct()
                                           .ToArray();

                    var words = product.Description.Split().Select(s => s.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(SuggestionSearch, StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = result };
        }

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync()
        {
            var response = new ServiceResponse<List<Product>> {
                Data = await _context.Products
                                    .Where(p => p.Featured)
                                    .Include(p => p.Variants)
                                    .ToListAsync()
            };

            return response;
        }

        private async Task<List<Product>> FindProductBySearchText(string FilterSearch)
        {
            var filteredProducts = await _context.Products
                                               .Where(p => p.Title.ToLower().Contains(FilterSearch) || p.Description.ToLower().Contains(FilterSearch))
                                               .Include(p => p.Variants)
                                               .ThenInclude(v => v.ProductType)
                                               .ToListAsync();

            return filteredProducts;
        }
    }
}
