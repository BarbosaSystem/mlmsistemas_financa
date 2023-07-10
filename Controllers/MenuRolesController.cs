using System.Security.Principal;
using System.Linq;
using app.Models.Settings;
using app.Repositorios.Abstratos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuRolesController : ControllerBase
    {
        private readonly IMenuRoles _menuroles;
        private readonly RoleManager<IdentityRole> _roleManager;
        public MenuRolesController(IMenuRoles menuRoles, RoleManager<IdentityRole> roleManager)
        {
            _menuroles = menuRoles;
            _roleManager = roleManager;
        }
        
        [HttpPost]
        public IActionResult PostMenuRules(MenuRoles menuRoles)
        {
            if (ModelState.IsValid)
            {
                _menuroles.Inserir(menuRoles);
                _menuroles.SaveChanges();
            }

            return Ok();
        }

        [HttpGet]
        [Route("GetMenuRoleById")]
        public IActionResult GetMenuRole(string roleId){
            var list = _menuroles.PesquisaMenuByRoles(roleId);

            return Ok(list);
        }

        [HttpGet]
        [Route("GetMenuRole")]
        public IActionResult GetMenuRoleFromRole(){
            
            var menuRole = _menuroles.PesquisarTodos();
            var role = _roleManager.Roles;

            var result = new List<object>() ;

            foreach (var rol in role)
            {
                var menuroles = _menuroles.PesquisarTodos()
                    .Where(x => x.RolesId.Equals(rol.Id));

                var teste = new {
                    RoleName = rol.Name,
                    IdsMenu = menuroles,
                };

                result.Add(teste);
            }

            return Ok(result);
        }

    }

}

