using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using app.Models;
using app.Models.Identity;
using app.Models.ViewModel;
using app.Repositorios.Abstratos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private readonly IMenuRoles menuRoles;
        private readonly IMenu menu;
        public RoleController(
            RoleManager<IdentityRole> _roleManager, 
            UserManager<ApplicationUser> _userManager,
            IMenu _menu,
            IMenuRoles _menuRoles
        )
        {
            roleManager = _roleManager;
            userManager = _userManager;
            menuRoles = _menuRoles;
            menu = _menu;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Create Role
                if (model.Id == "")
                {
                    var res = await roleManager.CreateAsync(new IdentityRole { Name = model.RoleName });
                    if (res.Succeeded)
                    {
                        var roleId = await roleManager.FindByNameAsync(model.RoleName);
                        if(GenerateMenuRoles(roleId.Id)){
                            return Ok(Utils.CreateSucesso);
                        }
                        return Ok("Nível cadastrado com sucesso, porém não foi vinculado aos menus");
                    }
                    else
                    {
                        return Ok(Utils.Error);
                    }
                }
                else
                {
                    return Ok(Utils.Error);
                }
            }
            else
            {
                return BadRequest(Utils.Error);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateRoleAsync(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != null)
                {
                    var res = await roleManager.FindByIdAsync(model.Id);
                    if (res != null)
                    {
                        res.Name = model.RoleName;
                        var result = await roleManager.UpdateAsync(res);
                        if (result.Succeeded)
                        {
                            return Ok(Utils.UpdateSucesso);
                        }
                        else
                        {
                            return Ok(Utils.Error);
                        }
                    }
                    else
                    {
                        return Ok(Utils.Vazio);
                    }
                }
                else
                {
                    return Ok(Utils.Error);
                }
            }
            else
            {
                return StatusCode(500, Utils.Error);
            }
        }
        [HttpDelete]
        public async Task<Object> DeleteRole(RoleViewModel model)
        {

            var role = await roleManager.FindByNameAsync(model.RoleName);
            var result = await roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return Ok("Função removida com sucesso");
            }
            else
            {
                return Ok("Não foi possivel executar esta ação");
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ListRoles()
        {
            if (ModelState.IsValid)
            {
                var roles = roleManager.Roles;

                var resultRoles = new List<RoleViewModel>();

                foreach (var item in roles)
                {
                    resultRoles.Add(new RoleViewModel
                    {
                        Id = item.Id,
                        RoleName = item.Name
                    });
                }
                return Ok(resultRoles);
            }
            else{
                return BadRequest("Não foi possível");
            }



        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        [Route("ListRoleInUsersAsync")]
        public async Task<IActionResult> ListRoleInUsersAsync()
        {
            var roles = roleManager.Roles;
            var roleUsers = new List<object>();

            foreach (var role in roles)
            {
                var roleUser = await userManager.GetUsersInRoleAsync(role.Name);

                if (roleUser.Count > 0)
                {
                    roleUsers.Add(roleUser);
                }
            }
            if (roleUsers.Count > 0)
            {
                return Ok(roleUsers);
            }
            else
            {
                return Ok("Nenhum dado encontrado");
            }

        }

        private bool GenerateMenuRoles(string roleId)
        {
            var menus = menu.PesquisarTodos().Select( x => x.Id);

            try
            {
                foreach (var item in menus)
                {
                    menuRoles.Inserir(new Models.Settings.MenuRoles { RolesId = roleId, MenuId = item, Status = false});
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }


}