using CookBook.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CookBook.Application.Services.Token
{
    public class TokenService
    {
        private const string EmailAlias = "usuarioEmail";
        private const string NameAlias = "nome";
        private readonly double _lifeTimeInMinutes;
        private readonly string _secureKey;

        public TokenService(double lifeTimeInMinutes, string secureKey)
        {
            _lifeTimeInMinutes = lifeTimeInMinutes;
            _secureKey = secureKey;
        }

        public dynamic GenerateToken(Usuario user)
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

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            var cookieOptions = new CookieOptions()
            {
                Path = "/",
                HttpOnly = false,
                IsEssential = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddMinutes(_lifeTimeInMinutes),
            };
            dynamic dynamicResponse = new System.Dynamic.ExpandoObject();
            dynamicResponse.Token = tokenHandler.WriteToken(securityToken);
            dynamicResponse.LifeTimeInMinutes = DateTime.UtcNow.AddMinutes(_lifeTimeInMinutes);
            return dynamicResponse;
        }

        public ClaimsPrincipal ValidateToken(string token)
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

            var claims = tokenHandler.ValidateToken(token, validationParams, out _);

            return claims;
        }

        private SymmetricSecurityKey SimmetricKey()
        {
            var symmetricKey = Convert.FromBase64String(_secureKey);
            return new SymmetricSecurityKey(symmetricKey);
        }

        public string GetEmailBySession(string token)
        {
            var claims = ValidateToken(token);

            return claims.FindFirst(EmailAlias).Value;
        }
    }
}
