using System;
using System.Collections.Generic;
using System.Linq;
using app.Models.Settings;
using app.Models.ViewModel;
using app.Repositorios.Abstratos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenu _menu;
        private readonly IMenuRoles _menuRoles;
        private readonly RoleManager<IdentityRole> _roleManager;
        public MenuController(IMenu menu,
        IMenuRoles menuRoles, RoleManager<IdentityRole> roleManagger)
        {
            _menu = menu;
            _menuRoles = menuRoles;
            _roleManager = roleManagger;
        }

        [HttpPost]
        [Route("SetMenu")]
        public IActionResult CriarMenuAndRoleMenu(Menu _frontMenu)
        {
            if (ModelState.IsValid)
            {
                var resultMenuId = PostarMenu(_frontMenu);
                var menuTags = _frontMenu.Tags;

                if (resultMenuId != 0)
                {
                    var roles = _roleManager.Roles;
                    foreach (var menuTag in menuTags)
                    {
                        try
                        {
                            _menuRoles.Inserir(new MenuRoles { RolesId = menuTag.RoleId, MenuId = resultMenuId, Status = menuTag.Status });
                        }
                        catch (System.Exception ex)
                        {
                            return NotFound("Não foi possível: " + ex.ToString());
                        }
                    }
                    return Ok("Item Cadastrado com sucesso");

                }
                else
                {
                    return Ok("Título existente");
                }
            }
            else
            {
                return BadRequest("Não foi possível");
            }
        }


        [HttpPost]
        [Route("UpdateMenu")]
        public IActionResult EditMenu(Menu _menuEdit)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _menu.Atualizar(_menuEdit);
                    var tags = _menuEdit.Tags;

                    foreach (var item in tags)
                    {
                        _menuRoles.UpdateMenuRoles(new MenuRoles { MenuId = _menuEdit.Id, RolesId = item.RoleId, Status = item.Status });
                    }
                    return Ok("Menu atualizado com sucesso");
                }
                catch (System.Exception)
                {
                    return Ok("Não foi possível processar a sua solicitação");
                }
            }
            else
            {
                return BadRequest("Error.");
            }
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> GetMenuAsync()
        {
            var roleMenu = new List<object>();
            var mennu = _menu.PesquisarTodos();
            var menusRoles = _menu.PesquisarTodos()
                             .GroupJoin(_menuRoles.PesquisarTodos(),
                             m => m.Id,
                             r => r.MenuId,
                             (menu, rol) => new
                             {
                                 Menu = menu,
                                 Roles = rol
                             });

            foreach (var item in menusRoles)
            {
                foreach (var itens in item.Roles)
                {
                    var result = await _roleManager.FindByIdAsync(itens.RolesId);

                    item.Menu.Tags.Add(new TagsViewModel { RoleId = result.Id, Role = result.Name, Status = itens.Status });

                }
                roleMenu.Add(item.Menu);
            }

            return Ok(roleMenu);
            /* var menus = _menu.PesquisarTodos();
            var roleManager = _roleManager.Roles;
            
            foreach (var menu in menus)
            {
                var menuRole = _menuRoles.PesquisaRolesByMenuId(menu.Id.ToString());
                roleMenu.Add(from menRole in menuRole
                           join role in roleManager on menRole.RolesId equals role.Id.ToString()
                           select new { menRole.MenuId, role.Name, menRole.Status});

                menu.Tags.Add( new TagsViewModel{Role = m})
            } */

            /* for (int i = 0; i < menu.Count(); i++)
            {
                var itens = _menuRoles.PesquisaRolesByMenuId(menu[i].Id.ToString());

                foreach (var item in itens)
                {
                    var role = _roleManager.FindByIdAsync(item.RolesId);
                    menu[i].Tags.Add(new TagsViewModel
                    {
                        Role = role.Result.Name,
                        Status = true
                    });
                }
            } */
        }

        [HttpPost]
        [Route("GetMenuByRole")]
        public async System.Threading.Tasks.Task<IActionResult> GetMenuByRoleAsync(RoleViewModel roleName)
        {

            //retornar as roles com base no Nome - Retorna o id da role
            var menuItens = new List<Menu>();
            var teste = await _roleManager.FindByNameAsync(roleName.RoleName);
            var teste2 = _menuRoles.GetMenuRolesTrueByRoleName(teste.Id);

            foreach (var item in teste2)
            {
                menuItens.Add(_menu.PesquisarTodos().Where(x => x.Id.Equals(item.MenuId)).FirstOrDefault());
            }

            return Ok(menuItens.Where(x => x.Status.Equals(true)));

        }

        [HttpGet]
        [Route("RelMenu")]
        [Authorize(Roles = "Admin")]
        public IActionResult RelMenu()
        {
            var categorias = _menu.PesquisarTodos();
            return Ok(categorias);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public IActionResult PostMenu(MenuPostViewModel menu)
        {
            return Ok();
            /* if (ModelState.IsValid)
            {
                var menuItem = new Menu()
                {
                    Id = menu.Id,
                    Icone = menu.Icone,
                    Tags = menu.Perfil,
                    Status = menu.Status,
                    Titulo = menu.Titulo,
                    Perfil = "",
                    Rota = menu.Rota
                };
                var listaPerfil = menu.Perfil;
                
                if (menuItem.Id == 0)
                {
                    var result = PostarMenu(menuItem);
                    if (result != 0)
                    {
                        foreach (var item in listaPerfil)
                        {
                            var obterRole = _roleManager.FindByNameAsync(item);
                            _menuRoles.Inserir(new MenuRoles()
                            {
                                MenuId = result,
                                RolesId = obterRole.Result.Id
                            });
                        }
                    }
                    return Ok("Item cadastrado com sucesso");
                }
                else
                {
                    var rolesMenu = _menuRoles.PesquisaRolesByMenuId(menu.Id.ToString());
                    var listItem = new List<string>();
                    //salvar as rolesNames no banco em listItem
                    foreach (var item in rolesMenu)
                    {
                        listItem.Add(_roleManager.FindByIdAsync(item.RolesId).Result.Name);
                    }
                    if (menu.Perfil != listItem)
                    {
                        //excluir caso não tenha na model enviada, irá verificar com a informação do banco de dados
                        foreach (var item in listItem)
                        {
                            if (menu.Perfil.Contains(item))
                            {

                            }
                            else
                            {
                                _menuRoles.Excluir(new MenuRoles() { MenuId = menu.Id, RolesId = _roleManager.FindByNameAsync(item).Result.Id });
                            }
                        }

                        foreach (var item in menu.Perfil)
                        {
                            if (!listItem.Contains(item))
                            {
                                _menuRoles.Inserir(new MenuRoles() { MenuId = menu.Id, RolesId = _roleManager.FindByNameAsync(item).Result.Id });
                            }
                        }
                    }

                    _menu.Atualizar(menuItem);

                    return Ok("Dados alterados com sucesso");
                }
            }
            else
            {
                return Ok("Não foi possível realizar esta operação");
            } */
        }

        [HttpGet]
        [Route("GetMenuById/{id}")]
        public IActionResult GetMenuById(int id)
        {
            if (id != 0)
            {
                var menu = _menu.PesquisarTodos().Where(x => x.Id.Equals(id)).FirstOrDefault();
                if (menu != null)
                {
                    var menu_roles = _menuRoles.PesquisaRolesByMenuId(menu.Id.ToString());
                    foreach (var item in menu_roles)
                    {
                        var roleName = _roleManager.FindByIdAsync(item.RolesId);
                        menu.Tags.Add(new TagsViewModel
                        {
                            Role = roleName.Result.Name,
                            Status = true
                        });
                    }
                }
                var menuView = new MenuPostViewModel()
                {
                    Id = menu.Id,
                    Icone = menu.Icone,
                    Tags = menu.Tags,
                    Rota = menu.Rota,
                    Status = menu.Status,
                    Titulo = menu.Titulo
                };
                return Ok(menuView);
            }
            else
            {
                return Ok("Item não encontrado");
            }
        }


        private int PostarMenu(Menu menu)
        {
            var categoria_existente = _menu.PesquisarPorNome(menu.Titulo);
            if (categoria_existente.Count == 0)
            {
                _menu.Inserir(menu);
                return menu.Id;
            }
            else
            {
                return 0;
            }
        }
    }
}