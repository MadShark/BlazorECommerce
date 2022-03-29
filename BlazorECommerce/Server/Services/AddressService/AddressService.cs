namespace BlazorECommerce.Server.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public AddressService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<Address>> GetAddressAsync()
        {
            int UserId = _authService.GetUserId();
            var Address = await _context.Addresses.FirstOrDefaultAsync(a => a.UserId == UserId);

            return new ServiceResponse<Address> { Data = Address };
        }

        public async Task<ServiceResponse<Address>> AddOrUpdateAddressAsync(Address Address)
        {
            var response = new ServiceResponse<Address>();
            var dbAddress = (await GetAddressAsync()).Data;
            if (dbAddress == null)
            {
                Address.UserId = _authService.GetUserId();
                _context.Addresses.Add(Address);
                response.Data = Address;
            }
            else
            {
                dbAddress.FirstName = Address.FirstName;
                dbAddress.LastName = Address.LastName;
                dbAddress.State = Address.State;
                dbAddress.Country = Address.Country;
                dbAddress.City = Address.City;
                dbAddress.Zip = Address.Zip;
                dbAddress.Street = Address.Street;
                response.Data = dbAddress;
            }

            await _context.SaveChangesAsync();

            return response;
        }
    }
}
