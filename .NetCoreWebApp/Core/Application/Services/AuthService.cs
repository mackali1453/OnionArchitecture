using Application.Interfaces;
using Common.Interfaces;
using Domain.Entities;
using Domain.Entities.Aggregates;
using Github.NetCoreWebApp.Core.Applications.Dto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace Github.NetCoreWebApp.Core.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IWebApiIuow _iUow;
        private readonly IUtility _utility;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secretKey;
        public AuthService(IWebApiIuow iUow, IUtility utility, IOptions<AppSettings> jwtSettings)
        {
            _iUow = iUow;
            _utility = utility;
            _issuer = jwtSettings.Value.JwtSettings.Issuer;
            _audience = jwtSettings.Value.JwtSettings.Audience;
            _secretKey = jwtSettings.Value.JwtSettings.SecretKey;
        }

        public async Task<LoginResponseDto> Authenticate(string password, string username)
        {
            try
            {
                var userRepository = _iUow.GetRepository<AppUser>();
                Expression<Func<AppUser, bool>> condition = person => person.UserName == username;

                var user = await userRepository.GetByFilterEager(condition);

                if (user == null)
                {
                    return new LoginResponseDto(false, "User not found", null);
                }
                else if (!_utility.VerifyPassword(password, user.Password))
                    return new LoginResponseDto(false, "Invalid Password", null);

                var token = GenerateJwtToken(username);

                return new LoginResponseDto(true, "", new LoginResponse { AccessToken = token });
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                claims: new[] { new Claim(ClaimTypes.Name, username) },
                audience: _audience,
                expires: DateTime.UtcNow.AddHours(1), // Token expiration time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
