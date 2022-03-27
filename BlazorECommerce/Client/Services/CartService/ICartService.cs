namespace BlazorECommerce.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;


        Task AddToCart(CartItem CartItem);
        Task<List<CartProductResponseDTO>> GetCartProducts();
        Task RemoveProductFromCart(int ProductId, int ProductTypeId);
        Task UpdateQuantity(CartProductResponseDTO product);
        Task StoreCartItems(bool EmptyLocalCart);
        Task GetCartItemsCount();
    }
}
