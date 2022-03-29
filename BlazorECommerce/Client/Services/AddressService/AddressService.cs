namespace BlazorECommerce.Client.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;

        public AddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Address> GetAddress()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<Address>>("api/address");
            return response.Data;
        }

        public async Task<Address> AddOrUpdateAddress(Address Address)
        {
            var response = await _httpClient.PostAsJsonAsync("api/address", Address);
            return response.Content.ReadFromJsonAsync<ServiceResponse<Address>>().Result.Data;
        }
    }
}
