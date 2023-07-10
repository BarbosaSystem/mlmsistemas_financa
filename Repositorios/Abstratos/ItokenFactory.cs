using System.Security.Claims;
using System.Threading.Tasks;
using app.Models;
using app.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace app.Repositorios.Abstratos
{
    public interface ITokenFactory
    {
        string GenerateToken(int size = 32);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<UserToken> GenToken(UserManager<ApplicationUser> userManager, ApplicationUser user);
        Task<bool> SetRefreshTokenAsync(ApplicationUser user, string refreshToken, UserManager<ApplicationUser> userManager);
    }
}