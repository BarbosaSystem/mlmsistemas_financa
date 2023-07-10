using System;
using System.Linq;
using System.Threading.Tasks;
using app.Models.Identity;
using app.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using app.Models;
using app.Services;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public static IHostingEnvironment _environment;
        public UserProfileController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            var photFile = new byte[1];
            var roleUser = _userManager.GetRolesAsync(user);

            if (System.IO.File.Exists("Upload/" + user.Id + ".jpg"))
            {
                photFile = System.IO.File.ReadAllBytes("Upload/" + user.Id + ".jpg");
            }
            else
            {
                photFile = System.IO.File.ReadAllBytes("Upload/user.png");
            }
            var _options = new IdentityOptions();

            return new
            {
                Codigo = user.Id,
                user.FullName,
                user.Email,
                user.UserName,
                user.PhoneNumber,
                user.RefreshToken,
                role = roleUser.Result,
                Photo = "data:image/jpeg;base64," + Convert.ToBase64String(photFile)
            };
        }

        [HttpPost]
        [Authorize]
        [Route("LoadImageProfile")]
        public async Task<IActionResult> ImageUpload([FromForm] FileUploadApi objFile)
        {
            if (ModelState.IsValid)
            {
                if (objFile.files.Length > 0)
                {
                    string[] formatExt = { ".jpg" };

                    string ext = System.IO.Path.GetExtension(objFile.files.FileName);
                    System.IO.Path.ChangeExtension(objFile.files.FileName, ".jpg");

                    if (Array.IndexOf(formatExt, ext) > -1)
                    {
                        if (!Directory.Exists("Upload"))
                        {
                            Directory.CreateDirectory("Upload");
                        }

                        if (System.IO.File.Exists("Upload/" + objFile.UserId + ext))
                        {
                            System.IO.File.Delete("Upload/" + objFile.UserId + ext);
                        }

                        using (FileStream fileStream = System.IO.File.Create("Upload/" + objFile.files.FileName.Replace(objFile.files.FileName, objFile.UserId) + ext))
                        {
                            await objFile.files.CopyToAsync(fileStream);
                            await fileStream.FlushAsync();

                            var usuario = await _userManager.FindByIdAsync(objFile.UserId);
                            if (usuario != null)
                            {
                                usuario.photoUrl = Path.GetFileName(fileStream.Name);
                                await _userManager.UpdateAsync(usuario);
                            }
                        }

                        var usrPhotoString = await PhotoUser.GetPhotoAsync(objFile.UserId);

                        return Ok(new NewClass(true, usrPhotoString));
                    }
                    else
                    {
                        return Ok(new NewClass(false, "Extensão de arquivo inválido. Extensões aceitas: " + string.Join("", formatExt)));
                    }
                }
                else
                {
                    return BadRequest("Nenhum arquivo carregado");
                }
            }
            else
            {
                return BadRequest("Erro ao processar a sua solicitação");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Usuário")]
        [Route("ForAdmin")]
        public string GetForAdmin()
        {
            return "Web method for Admin";
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> GenerateTokenPasswordAsync(ForgotPassword Email)
        {
            var user = await _userManager.FindByEmailAsync(Email.Email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                return Ok(token);
            }
            else
            {
                return Ok("Não foi possível");
            }
        }


        [HttpGet]
        [Route("GetUserById/{Id}")]
        public async Task<IActionResult> GetProfileByIdAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                var role = await _userManager.GetRolesAsync(user);
                var usuario = new
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Usuario = user.UserName,
                    Email = user.Email,
                    Role = role.Select(x => x).FirstOrDefault(),
                    photoString = await PhotoUser.GetPhotoAsync(user.Id)
                };
                return Ok(usuario);
            }
            else{
                return BadRequest("Usuário não encontrado");
            }
        }

        [HttpPost]
        [Route("ChangeEmailAndRole")]
        public async Task<IActionResult> ChangeEmailAndRoleAsync(ChangeEmailAndRoleView modelo){

            var user = await _userManager.FindByIdAsync(modelo.Id);
            var user_Token = await _userManager.GenerateChangeEmailTokenAsync(user, modelo.Email);
            try
            {
                if(user != null)
                {
                    if(user.Email != modelo.Email)
                    {
                        var result = await _userManager.ChangeEmailAsync(user, modelo.Email, user_Token);
                    }
                    
                    var roleUser = await _userManager.GetRolesAsync(user);
                    if(roleUser.FirstOrDefault() != modelo.RoleName){
                        await _userManager.RemoveFromRoleAsync(user, roleUser.FirstOrDefault());
                        await _userManager.AddToRoleAsync(user, modelo.RoleName);
                    }

                }
                return Ok("Dados atualizados com sucesso.");
            }
            catch (System.Exception)
            {
                return Ok("Não foi possível alterar as informações");
            }
            
        }
    }

    internal class NewClass
    {
        public bool Messagem { get; }
        public string Photo { get; }

        public NewClass(bool messagem, string photo)
        {
            Messagem = messagem;
            Photo = photo;
        }

        public override bool Equals(object obj)
        {
            return obj is NewClass other &&
                   Messagem == other.Messagem &&
                   Photo == other.Photo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Messagem, Photo);
        }
    }
}