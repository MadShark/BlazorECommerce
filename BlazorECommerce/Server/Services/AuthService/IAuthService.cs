namespace BlazorECommerce.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> RegisterAsync(User User, string Password);
        Task<bool> UserExistsAsync(string Email);
        Task<ServiceResponse<string>> LoginAsync(string Email, string Password);
        Task<ServiceResponse<bool>> ChangePasswordAsync(int UserId, string NewPassword);
        int GetUserId();
    }
}
