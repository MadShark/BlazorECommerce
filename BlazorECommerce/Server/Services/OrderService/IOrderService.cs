namespace BlazorECommerce.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResponse<bool>> PlaceOrderAsync(int UserId);
        Task<ServiceResponse<List<OrderOverviewResponseDTO>>> GetOrdersAsync();
        Task<ServiceResponse<OrderDetailsResponseDTO>> GetOrderDetailsAsync(int OrderId);
    }
}
