﻿using Microsoft.AspNetCore.Http;
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

        [HttpGet("category/{CategoryUrl}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategoryAsync(string CategoryUrl)
        {
            var result = await _productService.GetProductsByCategoryAsync(CategoryUrl);
            return Ok(result);
        }

        [HttpGet("search/{FilterSearch}/{PageNumber}")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResultDTO>>> SearchProductsByFilterAsync(string FilterSearch, int PageNumber = 1)
        {
            var result = await _productService.SearchProductsByFilterAsync(FilterSearch, PageNumber);
            return Ok(result);
        }

        [HttpGet("suggestions/{SuggestionSearch}")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetProductSearchSuggestionsAsync(string SuggestionSearch)
        {
            var response = await _productService.GetProductSearchSuggestionsAsync(SuggestionSearch);
            return Ok(response);
        }

        [HttpGet("featured")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetFeaturedProductsAsync()
        {
            var result = await _productService.GetFeaturedProductsAsync();
            return Ok(result);
        }
    }
}
