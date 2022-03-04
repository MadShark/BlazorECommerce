﻿namespace BlazorECommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }   

        public List<Product> listProducts { get; set; } = new List<Product>();

        public async Task GetAllProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product");
            if (result != null && result.Data != null)
                listProducts = result.Data;
        }

        public async Task<ServiceResponse<Product>> GetProductById(int ProductId)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{ProductId}");
            return result;
        }
    }
}