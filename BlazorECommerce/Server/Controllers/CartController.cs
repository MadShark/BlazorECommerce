using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorECommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("products")]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponseDTO>>>> GetCartProductsAsync(List<CartItem> cartItems)
        {
            var result = await _cartService.GetCartProductsAsync(cartItems);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponseDTO>>>> StoreCartItemssAsync(List<CartItem> CartItems)
        {
            var result = await _cartService.StoreCartItemsAsync(CartItems);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddToCartAsync(CartItem CartItem)
        {
            var result = await _cartService.AddToCartAsync(CartItem);
            return Ok(result);
        }

        [HttpPut("update-quantity")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateQuantityAsync(CartItem CartItem)
        {
            var result = await _cartService.UpdateQuantityAsync(CartItem);
            return Ok(result);
        }

        [HttpDelete("{ProductId}/{ProductTypeId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveItemFromCartAsync(int ProductId, int ProductTypeId)
        {
            var result = await _cartService.RemoveItemFromCartAsync(ProductId, ProductTypeId);
            return Ok(result);
        }

        [HttpGet("count")]
        public async Task<ActionResult<ServiceResponse<int>>> GetCartItemsCountAsync()
        {
            return Ok(await _cartService.GetCartItemsCountAsync());
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponseDTO>>>> GetDbCartProducts()
        {
            var result = await _cartService.GetDbCartProductsAsync();
            return Ok(result);
        }
    }
}
