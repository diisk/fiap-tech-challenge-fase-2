using Application.DTOs;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class TokenService : ITokenService
    {

        private const string CLAIM_TYPE_ID = "Identificador";
        private const string CONFIG_KEY_SECRET = "SecretJwt";
        private const string CONFIG_KEY_CRYPTO = "ChaveCrypto";

        private readonly IConfiguration configuration;
        private readonly ICryptoService cryptoService;
        private readonly ICacheService cacheService;

        public TokenService(IConfiguration configuration, ICryptoService cryptoService, ICacheService cacheService)
        {
            this.configuration = configuration;
            this.cryptoService = cryptoService;
            this.cacheService = cacheService;
        }

        public string GetToken(TokenData data)
        {
            var chaveCache = $"TOKEN_{data.Identifier}";
            var tokenString = cacheService.GetCache(chaveCache);

            if (tokenString != null)
            {
                return (string)tokenString;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(configuration.GetValue<string>(CONFIG_KEY_SECRET)!);
            var chaveCriptografia = configuration.GetValue<string>(CONFIG_KEY_CRYPTO)!;

            var tokenPropriedades = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(CLAIM_TYPE_ID,cryptoService.CriptografarString(data.Identifier,chaveCriptografia))
                }),

                Expires = DateTime.UtcNow.AddHours(9),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenPropriedades);
            tokenString = tokenHandler.WriteToken(token);
            cacheService.SetCache(chaveCache, tokenString, TimeSpan.FromHours(8));
            return (string)tokenString;
        }

        public string GetIdentifierFromClaimsPrincipal(ClaimsPrincipal user)
        {
            var chaveCriptografia = configuration.GetValue<string>(CONFIG_KEY_CRYPTO)!;

            var idEncryptado = user.FindFirst(CLAIM_TYPE_ID)!.Value;

            var id = cryptoService.DescriptografarString(idEncryptado, chaveCriptografia);

            return id;
        }
    }
}
