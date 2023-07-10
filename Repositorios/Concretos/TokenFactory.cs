using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using app.Models;
using app.Models.Identity;
using app.Repositorios.Abstratos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace app.Repositorios.Concretos
{
    public class TokenFactory : ITokenFactory
    {
        public IConfiguration Configuration { get; }
        public string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<UserToken> GenToken(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var role = await userManager.GetRolesAsync(user);
            var tokenFactory = new TokenFactory();
            var refresh_token = tokenFactory.GenerateToken();
            IdentityOptions _options = new IdentityOptions();
            var expiration = DateTime.UtcNow.ToUniversalTime().AddMinutes(60);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault()),
                        new Claim("RefreshToken",refresh_token),
                }),
                Expires = expiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
            };
            //PROCEDIMENTO PARA SALVAR O REFRESHTOKEN

            if (await SetRefreshTokenAsync(user, refresh_token, userManager))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return new UserToken() {
                    
                    Token = tokenHandler.WriteToken(securityToken),
                    RefreshToken = refresh_token,
                    Expiration = (long)Math.Round((expiration.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds)
                };
            }else{
                return null;
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.UTF8.GetBytes("1234567890123456");
            var tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch (System.Exception)
            {
                return null;
            }

        }

        public async Task<bool> SetRefreshTokenAsync(ApplicationUser user, string refreshToken, UserManager<ApplicationUser> userManager)
        {
            var usuario = await userManager.FindByIdAsync(user.Id);
            usuario.RefreshToken = refreshToken;

            var result = await userManager.UpdateAsync(usuario);
            if (result.Succeeded)
                return true;
            else
                return false;
        }
    }
}