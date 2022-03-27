using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BlazorECommerce.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


        public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));


        public async Task<ServiceResponse<int>> RegisterAsync(User User, string Password)
        {
            if (await UserExistsAsync(User.Email))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User Already Exists"
                };
            }

            CreatePasswordHash(Password, out byte[] PasswordHash, out byte[] PasswordSalt);

            User.PasswordHash = PasswordHash;
            User.PasswordSalt = PasswordSalt;

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = User.Id, Message = "Registration Successful!" };
        }


        public async Task<bool> UserExistsAsync(string Email)
        {
            if (await _context.Users.AnyAsync(user => user.Email.ToLower().Equals(Email.ToLower())))
                return true;

            return false;
        }

        public async Task<ServiceResponse<string>> LoginAsync(string Email, string Password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(Email.ToLower()));

            if (user == null)
            {
                response.Success = false;
                response.Message = "User Not Found";
            }
            else if (!VerifyPasswordHash(Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Incorrect Password";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> ChangePasswordAsync(int UserId, string NewPassword)
        {
            var user = await _context.Users.FindAsync(UserId);
            if (user == null)
            {
                return new ServiceResponse<bool>
                { 
                    Success = false,
                    Message = "User Not Found"
                };
            }

            CreatePasswordHash(NewPassword, out byte[] PasswordHash, out byte[] PasswordSalt);

            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "Password Changed Successfully!" };
        }

        private void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }

        private bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

                return ComputedHash.SequenceEqual(PasswordHash);
            }
        }

        private string CreateToken(User User)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                new Claim(ClaimTypes.Name, User.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
