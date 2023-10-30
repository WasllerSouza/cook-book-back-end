using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CookBook.Application.Services.Token
{
    public class TokenController
    {
        private const string EmailAlias = "email";
        private readonly double _lifeTimeInMinutes;
        private readonly string _secureKey;

        public TokenController(double lifeTimeInMinutes, string secureKey)
        {
            _lifeTimeInMinutes = lifeTimeInMinutes;
            _secureKey = secureKey;
        }

        public string GenerateToken(string userEmail)
        {
            var claims = new List<Claim> { new Claim(EmailAlias, userEmail) };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_lifeTimeInMinutes),
                SigningCredentials = new SigningCredentials(SimmetricKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            var secureToken = tokenHandler.CreateToken(tokenDescription); 
            return tokenHandler.WriteToken(secureToken);
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
            var symmetricKey = Convert.FromBase64String( _secureKey );
            return new SymmetricSecurityKey( symmetricKey );
        }
    }
}
