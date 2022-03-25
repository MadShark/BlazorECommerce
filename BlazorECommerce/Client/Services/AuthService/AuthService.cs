namespace BlazorECommerce.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<int>> Register(UserRegisterDTO RequestUser)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register", RequestUser);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task<ServiceResponse<string>> Login(UserLoginDTO RequestUser)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/login", RequestUser);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePasswordDTO RequestUser)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/change-password", RequestUser.Password);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }
}
