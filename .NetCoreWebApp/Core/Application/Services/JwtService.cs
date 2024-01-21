using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Github.NetCoreWebApp.Core.Application.Services
{
    public class JwtService
    {
        public string GenerateJwtToken(string[] roles)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(".NetCoreOnionArchitectur"));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityToken token = new JwtSecurityToken(issuer: "https://localhost", audience: "https://localhost", claims: claims, notBefore: DateTime.Now, expires: DateTime.Now.AddHours(8), signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
