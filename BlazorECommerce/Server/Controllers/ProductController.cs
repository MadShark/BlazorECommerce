using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorECommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetAllProducts()
        {
            var result = await _productService.GetAllProductsAsync();
            return Ok(result);
        }

        [HttpGet("{ProductId}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductByIdAsync(int ProductId)
        {
            var result = await _productService.GetProductByIdAsync(ProductId);

            if (result == null)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
