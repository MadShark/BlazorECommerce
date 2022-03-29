using System.Security.Claims;

namespace BlazorECommerce.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public CartService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }


        public async Task<ServiceResponse<List<CartProductResponseDTO>>> GetCartProductsAsync(List<CartItem> ListCartItems)
        {
            var result = new ServiceResponse<List<CartProductResponseDTO>> {
                Data = new List<CartProductResponseDTO>()
            };

            foreach (var cart in ListCartItems)
            {
                var product = await _context.Products.Where(p => p.Id == cart.ProductId).FirstOrDefaultAsync();

                if (product == null)
                    continue;

                var productVariant = await _context.ProductVariants
                                                .Where(v => v.ProductId == cart.ProductId && v.ProductTypeId == cart.ProductTypeId)
                                                .Include(v => v.ProductType)
                                                .FirstOrDefaultAsync();

                if (productVariant == null)
                    continue;

                var cartProduct = new CartProductResponseDTO 
                { 
                    ProductId = product.Id,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductType = productVariant.ProductType.Name,
                    ProductTypeId = productVariant.ProductTypeId,
                    Quantity = cart.Quantity
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }

        public async Task<ServiceResponse<List<CartProductResponseDTO>>> StoreCartItemsAsync(List<CartItem> CartItems)
        {
            CartItems.ForEach(cartItem => cartItem.UserId = _authService.GetUserId());
            _context.CartItems.AddRange(CartItems);
            await _context.SaveChangesAsync();

            return await GetDbCartProductsAsync(null);
        }

        public async Task<ServiceResponse<int>> GetCartItemsCountAsync()
        {
            var count = (await _context.CartItems.Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync()).Count;
            return new ServiceResponse<int> { Data = count };
        }

        public async Task<ServiceResponse<List<CartProductResponseDTO>>> GetDbCartProductsAsync(int? UserId)
        {
            if (UserId == null)
                UserId = _authService.GetUserId();

            return await GetCartProductsAsync(await _context.CartItems.Where(ci => ci.UserId == UserId).ToListAsync());
        }

        public async Task<ServiceResponse<bool>> AddToCartAsync(CartItem CartItem)
        {
            CartItem.UserId = _authService.GetUserId();

            var sameItem = await _context.CartItems
                                    .FirstOrDefaultAsync(ci => ci.ProductId == CartItem.ProductId && 
                                                         ci.ProductTypeId == CartItem.ProductTypeId &&
                                                         ci.UserId == CartItem.UserId);

            if (sameItem == null)
                _context.CartItems.Add(CartItem);
            else
                sameItem.Quantity += CartItem.Quantity;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true};
        }

        public async Task<ServiceResponse<bool>> UpdateQuantityAsync(CartItem CartItem)
        {
            var dbCartItem = await _context.CartItems
                                    .FirstOrDefaultAsync(ci => ci.ProductId == CartItem.ProductId &&
                                                         ci.ProductTypeId == CartItem.ProductTypeId &&
                                                         ci.UserId == _authService.GetUserId());

            if (dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart Item Doesn't Exist"
                };
            }

            dbCartItem.Quantity = CartItem.Quantity;
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> RemoveItemFromCartAsync(int ProductId, int ProductTypeId)
        {
            var dbCartItem = await _context.CartItems
                                    .FirstOrDefaultAsync(ci => ci.ProductId == ProductId &&
                                                         ci.ProductTypeId == ProductTypeId &&
                                                         ci.UserId == _authService.GetUserId());

            if (dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart Item Doesn't Exist"
                };
            }

            _context.CartItems.Remove(dbCartItem);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }
    }
}
