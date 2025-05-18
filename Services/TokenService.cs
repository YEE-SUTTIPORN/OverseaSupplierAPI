using Microsoft.IdentityModel.Tokens;
using OverseaSupplierAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace OverseaSupplierAPI.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        private static Dictionary<string, string> _refreshTokens = new(); // <refreshToken, username>

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public TokenResponse GenerateTokens(string username)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: new[] { new Claim(ClaimTypes.Name, username) },
                expires: DateTime.UtcNow.AddSeconds(30),
                signingCredentials: creds
            );

            var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            _refreshTokens[refreshToken] = username;

            return new TokenResponse
            {
                AccessToken = accessTokenString,
                RefreshToken = refreshToken
            };
        }

        public TokenResponse? Refresh(string refreshToken)
        {
            if (_refreshTokens.TryGetValue(refreshToken, out var username))
            {
                _refreshTokens.Remove(refreshToken); // ใช้ได้ครั้งเดียว
                return GenerateTokens(username);
            }

            return null;
        }

    }
}
