namespace BlazorECommerce.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<List<Product>>> GetAllProductsAsync()
        {
            var response = new ServiceResponse<List<Product>> {
                Data = await _context.Products
                                  .Where(p => p.Visible && !p.Deleted)
                                  .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                                  .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetAdminProductsAsync()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                                  .Where(p => !p.Deleted)
                                  .Include(p => p.Variants.Where(v => !v.Deleted))
                                  .ThenInclude(v => v.ProductType)
                                  .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductByIdAsync(int ProductId)
        {
            var response = new ServiceResponse<Product>();
            Product product = new Product();

            if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
            {
                product = await _context.Products
                                    .Include(p => p.Variants.Where(v => !v.Deleted))
                                    .ThenInclude(v => v.ProductType)
                                    .FirstOrDefaultAsync(p => p.Id == ProductId && !p.Deleted);
            }
            else 
            {
                product = await _context.Products
                                    .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                                    .ThenInclude(v => v.ProductType)
                                    .FirstOrDefaultAsync(p => p.Id == ProductId && !p.Deleted && p.Visible);
            }

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
                                        .Where(x => x.Category.Url.Equals(CategoryUrl) && x.Visible && !x.Deleted)
                                        .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
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
                                        .Where(p => p.Title.ToLower().Contains(FilterSearch) ||
                                               p.Description.ToLower().Contains(FilterSearch) &&
                                               p.Visible && !p.Deleted)
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
                                    .Where(p => p.Featured && p.Visible && !p.Deleted)
                                    .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
                                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> CreateProductAsync(Product Product)
        {
            foreach (var variant in Product.Variants)
            {
                variant.ProductType = null;
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return new ServiceResponse<Product> { Data = Product };
        }

        public async Task<ServiceResponse<Product>> UpdateProductAsync(Product Product)
        {
            var dbProduct = await _context.Products.FindAsync(Product.Id);
            if (dbProduct == null)
            {
                return new ServiceResponse<Product>
                {
                    Success = false,
                    Message = "Product not found."
                };
            }

            dbProduct.Title = Product.Title;
            dbProduct.Description = Product.Description;
            dbProduct.ImageUrl = Product.ImageUrl;
            dbProduct.CategoryId = Product.CategoryId;
            dbProduct.Visible = Product.Visible;
            dbProduct.Featured = Product.Featured;

            foreach (var variant in Product.Variants)
            {
                var dbVariant = await _context.ProductVariants
                                    .SingleOrDefaultAsync(v => v.ProductId == variant.ProductId &&
                                                          v.ProductTypeId == variant.ProductTypeId);
                if (dbVariant == null)
                {
                    variant.ProductType = null;
                    _context.ProductVariants.Add(variant);
                }
                else
                {
                    dbVariant.ProductTypeId = variant.ProductTypeId;
                    dbVariant.Price = variant.Price;
                    dbVariant.OriginalPrice = variant.OriginalPrice;
                    dbVariant.Visible = variant.Visible;
                    dbVariant.Deleted = variant.Deleted;
                }
            }

            await _context.SaveChangesAsync();
            return new ServiceResponse<Product> { Data = Product };
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync(int ProductId)
        {
            var dbProduct = await _context.Products.FindAsync(ProductId);
            if (dbProduct == null)
            { 
                return new ServiceResponse<bool> 
                { 
                    Data = false,
                    Success = false,
                    Message = "Product not found."
                };
            }

            dbProduct.Deleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        private async Task<List<Product>> FindProductBySearchText(string FilterSearch)
        {
            var filteredProducts = await _context.Products
                                               .Where(p => p.Title.ToLower().Contains(FilterSearch) ||
                                                      p.Description.ToLower().Contains(FilterSearch) &&
                                                      p.Visible && !p.Deleted)
                                               .Include(p => p.Variants)
                                               .ThenInclude(v => v.ProductType)
                                               .ToListAsync();

            return filteredProducts;
        }
    }
}
