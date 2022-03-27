namespace BlazorECommerce.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductResponseDTO>>> GetCartProductsAsync(List<CartItem> ListCartItems);
        Task<ServiceResponse<List<CartProductResponseDTO>>> StoreCartItemsAsync(List<CartItem> CartItems);
        Task<ServiceResponse<int>> GetCartItemsCountAsync();
        Task<ServiceResponse<List<CartProductResponseDTO>>> GetDbCartProductsAsync();
        Task<ServiceResponse<bool>> AddToCartAsync(CartItem CartItem);
        Task<ServiceResponse<bool>> UpdateQuantityAsync(CartItem CartItem);
        Task<ServiceResponse<bool>> RemoveItemFromCartAsync(int ProductId, int ProductTypeId);
    }
}
