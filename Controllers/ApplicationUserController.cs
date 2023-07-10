using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using app.Models;
using app.Models.Identity;
using app.Models.Settings;
using app.Models.ViewModel;
using app.Repositorios.Abstratos;
using app.Repositorios.Concretos;
using app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _singInManager;
        private readonly ApplicationSettings _appSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMenuRoles _menuRoles;
        private readonly IMenu _menu;
        private readonly ITokenFactory _itokenFactory;
        
        public ApplicationUserController
            (UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<ApplicationSettings> appSettings,
            RoleManager<IdentityRole> roleManager,
            TokenValidationParameters tokenValidationParameters,
            IMenuRoles menuRoles,
            ITokenFactory itokenFactory,
            IMenu menu)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _appSettings = appSettings.Value;
            _roleManager = roleManager;
            _tokenValidationParameters = tokenValidationParameters;
            _menuRoles = menuRoles;
            _menu = menu;
            _itokenFactory = itokenFactory;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<Object> PostApplicationUser(ApplicationUserModel model)
        {
            //1 - Criar no ApplicationUserModel o atributo ROLE
            //model.Role = "Usuário";
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
                //
            };
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    return BadRequest(new { message = "O E-mail: " + model.Email + " se encontra em uso. Favor verificar?" });
                }
                else
                {
                    applicationUser.photoUrl = "user.png";
                    var result = await _userManager.CreateAsync(applicationUser, model.Password);
                    var usuario = await _userManager.FindByEmailAsync(model.Email);
                    //2 - Metodo para adicionar a Role no banco de dados vinculado ao usuário
                    var applicationRole = await _roleManager.FindByNameAsync("Admin");
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(usuario, applicationRole.Name);
                    }
                    //await _userManager.AddToRoleAsync(usuario, model.Role);
                    if (result.Succeeded)
                    {
                        return Ok(new { message = result.Succeeded });
                    }
                    else
                    {
                        return BadRequest(new { message = "Erro ao criar registro" });
                    }

                }

            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Erro ao criar registro" });
                throw;
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var photoString = await PhotoUser.GetPhotoAsync(user.Id);
                    var role = await _userManager.GetRolesAsync(user);
                    

                    var roleId = await _roleManager.FindByNameAsync(role[0]);

                    var menuRoles = _menuRoles.GetMenuRolesTrueByRoleName(roleId.Id).Select( x => x.MenuId);
                    var listMenu = new List<Menu>();
                    foreach (var item in menuRoles)
                    {
                        listMenu.Add(_menu.PesquisarTodos().Where(x => x.Id.Equals(item)).FirstOrDefault());
                    }

                    var usuario = new {
                        user.Id,
                        user.FullName,
                        user.UserName,
                        user.Email,
                        user.PhoneNumber,
                        role,
                        listMenu = listMenu.Where(s => s.Status.Equals(true)),
                        photoString
                        
                        
                    };
                    var token = await _itokenFactory.GenToken(_userManager, user);
                    return Ok(new { token, usuario });
                    //https://fullstackmark.com/post/19/jwt-authentication-flow-with-refresh-tokens-in-aspnet-core-web-api
                }
                else
                {
                    return Unauthorized(new { message = "Nome de usuário ou senha está incorreta!" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro na solicitação: " + ex.ToString());
            }
        }

        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordViewModel ChangePassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(ChangePassword.Id);
                if (user == null)
                {
                    return Ok("Não foi possivel");
                }
                var result = await _userManager.ChangePasswordAsync(user, ChangePassword.OldPassword, ChangePassword.NewPassword);

                if (result.Succeeded)
                {
                    return Ok("Senha alterada com sucesso");
                }
                else
                {
                    var resultado = new List<object>();
                    foreach (var item in result.Errors)
                    {
                        resultado.Add(item);
                    }
                    return Ok(value: resultado.ToList());
                }
            }
            else
            {
                return Ok("Não foi possivel");
            }
        }

        [HttpGet]
        [Route("getUsuario")]
        public ActionResult GetUser()
        {
            var usuario = _userManager.Users.ToList();
            return Ok(usuario);
        }
        private async Task<string> GetRefreshToken(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            return usuario.RefreshToken;
        }

        /* [HttpPost]
        [Route("FindUserByEmail")]
        public async Task<IActionResult> FindUserByEmailAsync(ForgotPassword password)
        {
            //Selecionar usuário baseado no e-mail
            var user = await _userManager.FindByEmailAsync(password.Email);
            if (user != null)
            {
                try
                {
                    var resultado = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var newPassword = GenerateRandomPassword();
                    var res = _userManager.ResetPasswordAsync(user, resultado, newPassword);
                    var envioEmail = _emailServices.SendMail(user.Email, user.UserName, newPassword);

                    if(envioEmail.IsCompleted){
                        return Ok(new {Sinal = true, Mensagem="Favor verificar a sua Caixa de Entrada. Foi enviado um e-mail contendo a nova senha."});
                    }
                    else{
                        return Ok(new {Sinal = false, Mensagem= "Não foi possível processar a sua solicitação."});
                    }
                   
                }
                catch (System.Exception)
                {
                    return BadRequest("Erro ao processar sua solicitação");
                }
            }
            else
            {
                return Ok(new {Sinal = false, Mensagem= "Email não cadastrado"});
            }
        } */

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            //Método para randomizar baseado no string acima, com no máximo 10 caracteres.
            var randomPassword = new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return randomPassword;
        }

        private async Task<bool> SendEmailAsync(Email email)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://mlmsistemas.somee.com/api/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service using HttpClient    
                HttpResponseMessage Res = await client.PostAsJsonAsync("email/send-email", email);
                //Checking the response is successful or not which is sent using HttpClient    
                if (Res.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}