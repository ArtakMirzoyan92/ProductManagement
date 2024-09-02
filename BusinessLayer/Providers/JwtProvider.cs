using BusinessLayer.IProviders;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Providers
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(User user)
        {
            IList<Claim> claims = new List<Claim>()
            {
               new Claim("sub", user.Id.ToString()),
               new Claim("given_name", user.FirstName),
               new Claim("city", user.City)
            };

            SigningCredentials signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHourse));

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }

    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpiresHourse { get; set; }
    }
}
