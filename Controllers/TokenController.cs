using System.Linq;
using System.Threading.Tasks;
using app.Models.Identity;
using app.Models.ViewModel;
using app.Repositorios.Abstratos;
using app.Repositorios.Concretos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenFactory _tokenFactory;
        private UserManager<ApplicationUser> _userManager;

        public TokenController(ITokenFactory tokenFactory, UserManager<ApplicationUser> userManager)
        {
            _tokenFactory = tokenFactory;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("VerifyToken/{token}")]
        public IActionResult VerifyToken(string token)
        {
            if ((token != null) && (token != ""))
            {
                var resultToken = _tokenFactory.GetPrincipalFromExpiredToken(token);
                if (resultToken != null)
                    return Ok(RespostaToken.Result("Token Válido", true));
                else
                    return Unauthorized(RespostaToken.Result("Token Inválido", false));
            }
            else
            {
                return BadRequest(RespostaToken.Result("Token Nulo", false));
            }
        }


        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshAsync(UpdateToken _updateToken)
        {
            var principal = _tokenFactory.GetPrincipalFromExpiredToken(_updateToken.Token).Identities.FirstOrDefault();

            var userId = principal.Claims.FirstOrDefault().Value;

            var appUser =  await _userManager.FindByIdAsync(userId.ToString());
            var userRefreshToken = appUser.RefreshToken;

            if(userRefreshToken == _updateToken.RefreshToken ){
                return Ok( await _tokenFactory.GenToken(_userManager, appUser));
            }   
            else{
                return Unauthorized("Não permitido");
            }        
        }
    }
}