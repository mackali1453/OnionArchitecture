using Application.Interfaces;
using Domain.Entities.Aggregates;
using Github.NetCoreWebApp.Core.Applications.Dto;
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

        public AuthService(IWebApiIuow iUow, IUtility utility)
        {
            _iUow = iUow;
            _utility = utility;
        }

        public async Task<LoginResponseDto> Authenticate(string password, string username)
        {
            try
            {
                var userRepository = _iUow.GetRepository<AppUser>();
                Expression<Func<AppUser, bool>> condition = person => person.UserName == username && _utility.VerifyPassword(password, person.Password);

                var user = await userRepository.GetByFilterEager(condition);

                if (user == null)
                {
                    return new LoginResponseDto(false, "User not found", null);
                }

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(".NetCoreOnionArchitectur"));

                SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();

                JwtSecurityToken token = new JwtSecurityToken(issuer: "https://localhost", audience: "https://localhost", claims: claims, notBefore: DateTime.Now, expires: DateTime.Now.AddHours(8), signingCredentials: credentials);

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                return new LoginResponseDto(true, "", new LoginResponse(handler.WriteToken(token)));
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
    }
}
