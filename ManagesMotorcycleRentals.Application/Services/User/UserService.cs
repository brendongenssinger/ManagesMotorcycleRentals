using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Application.Services.Validator;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManagesMotorcycleRentals.Application.Services.User
{
    public class UserService : IUserService
    {
        private UserServiceValidator _userServiceValidator;

        public UserService(UserServiceValidator userServiceValidator)
        {
            _userServiceValidator = userServiceValidator;
        }
        public async Task<string> GenerateTokenAsync(int userId, string role, CancellationToken cancellationToken)
        {
            _userServiceValidator
                    .ValidRole(role)
                    .ValidUserId(userId);
            

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role) 
            };

            const string hash = "b7bb4e29f59e62c716a9f17986d1a861bf8b4689a0233ef31b0e77d5a78f4935";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(hash));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://api-brendon.com",
                audience: "http://teste-brendon.api",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds);

            return await Task.Run(()=> new JwtSecurityTokenHandler().WriteToken(token));
        }


    }
}
