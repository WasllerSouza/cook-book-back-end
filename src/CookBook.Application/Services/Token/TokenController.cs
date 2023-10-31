using CookBook.Communication.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CookBook.Application.Services.Token
{
    public class TokenController
    {
        private const string EmailAlias = "email";
        private const string NameAlias = "nome";
        private readonly double _lifeTimeInMinutes;
        private readonly string _secureKey;

        public TokenController(double lifeTimeInMinutes, string secureKey)
        {
            _lifeTimeInMinutes = lifeTimeInMinutes;
            _secureKey = secureKey;
        }

        public void GenerateToken(UserRegisterRequest user, IResponseCookies cookies)
        {
            var claims = new List<Claim>
            {
                new Claim(EmailAlias, user.Email),
                new Claim(NameAlias, user.Nome),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_lifeTimeInMinutes),
                SigningCredentials = new SigningCredentials(SimmetricKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            var secureToken = tokenHandler.CreateToken(tokenDescription);

            var cookieOptions = new CookieOptions()
            {
                Path = "/",
                HttpOnly = false,
                IsEssential = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddMinutes(_lifeTimeInMinutes),
            };

            cookies.Append("token", tokenHandler.WriteToken(secureToken), cookieOptions);
        }

        public void ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParams = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                IssuerSigningKey = SimmetricKey(),
                ClockSkew = new TimeSpan(0),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            tokenHandler.ValidateToken(token, validationParams, out _);
        }

        private SymmetricSecurityKey SimmetricKey()
        {
            var symmetricKey = Convert.FromBase64String(_secureKey);
            return new SymmetricSecurityKey(symmetricKey);
        }
    }
}
