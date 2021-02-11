using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CryptoHelper;
using Microsoft.IdentityModel.Tokens;

namespace Blog.API.Services
{
    public class AuthService: IAuthService
    {
        private readonly string _jwtSecret;
        private readonly int _jwtLifeSpan;

        public AuthService(string jwtSecret, int jwtLifeSpan)
        {
            _jwtSecret = jwtSecret;
            _jwtLifeSpan = jwtLifeSpan;
        }

        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public bool VerifyPassword(string actualPassword, string hashedPassword)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, actualPassword);
        }

        public AuthData GetAuthData(string id)
        {
            var expirationTime = DateTime.UtcNow.AddSeconds(_jwtLifeSpan);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, id),

                }),
                Expires = expirationTime,
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret)),
                        SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return new AuthData
            {
                Token = token,
                TokenExpirationTime = ((DateTimeOffset) expirationTime).ToUnixTimeSeconds(),
                Id = id
            };
        }
    }
}
