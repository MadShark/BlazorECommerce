using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorECommerce.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;
        public CartService(ILocalStorageService localStorage, HttpClient httpClient, IAuthService authService)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
            _authService = authService;
        }


        public event Action OnChange;


        public async Task AddToCart(CartItem CartItem)
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _httpClient.PostAsJsonAsync("api/cart/add", CartItem);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

                if (cart == null)
                    cart = new List<CartItem>();


                var sameItem = cart.Find(x => x.ProductId == CartItem.ProductId && x.ProductTypeId == CartItem.ProductTypeId);

                if (sameItem == null)
                    cart.Add(CartItem);
                else
                    sameItem.Quantity += CartItem.Quantity;


                await _localStorage.SetItemAsync("cart", cart);
            }
            await GetCartItemsCount();
        }

        public async Task<List<CartProductResponseDTO>> GetCartProducts()
        {
            if (await _authService.IsUserAuthenticated())
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<CartProductResponseDTO>>>("api/cart");
                return response.Data;
            }
            else
            {
                var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");

                if (cartItems == null)
                    return new List<CartProductResponseDTO>();

                var response = await _httpClient.PostAsJsonAsync("api/cart/products", cartItems);
                var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponseDTO>>>();

                return cartProducts.Data;
            }
        }

        public async Task RemoveProductFromCart(int ProductId, int ProductTypeId) 
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _httpClient.DeleteAsync($"api/cart/{ProductId}/{ProductTypeId}");
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cart == null)
                    return;


                var cartItem = cart.Find(x => x.ProductId == ProductId && x.ProductTypeId == ProductTypeId);

                if (cartItem != null)
                {
                    cart.Remove(cartItem);
                    await _localStorage.SetItemAsync("cart", cart);
                }
            }
        }

        public async Task UpdateQuantity(CartProductResponseDTO product)
        {
            if (await _authService.IsUserAuthenticated())
            {
                var request = new CartItem
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    ProductTypeId = product.ProductTypeId
                };

                await _httpClient.PutAsJsonAsync("api/cart/update-quantity", request);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cart == null)
                    return;


                var cartItem = cart.Find(x => x.ProductId == product.ProductId && x.ProductTypeId == product.ProductTypeId);

                if (cartItem != null)
                {
                    cartItem.Quantity = product.Quantity;
                    await _localStorage.SetItemAsync("cart", cart);
                }
            }
        }

        public async Task StoreCartItems(bool EmptyLocalCart = false)
        {
            var localCart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (localCart == null)
                return;

            await _httpClient.PostAsJsonAsync("api/cart", localCart);

            if (EmptyLocalCart)
                await _localStorage.RemoveItemAsync("cart");
        }

        public async Task GetCartItemsCount()
        {
            if (await _authService.IsUserAuthenticated())
            {
                var result = await _httpClient.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");
                var count = result.Data;

                await _localStorage.SetItemAsync<int>("CartItemsCount", count);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                await _localStorage.SetItemAsync<int>("CartItemsCount", cart != null ? cart.Count : 0);
            }

            OnChange.Invoke();
        }
    }
}
