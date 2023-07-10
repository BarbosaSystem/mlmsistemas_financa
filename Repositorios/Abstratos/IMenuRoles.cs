using System.Collections.Generic;
using app.Models.Settings;
using app.Repositorios.Base;

namespace app.Repositorios.Abstratos
{
    public interface IMenuRoles : IRepositorioBase<MenuRoles>
    {
         ICollection<MenuRoles> PesquisaRolesByMenuId(string MenuId);
         List<Menu> PesquisaMenuByRolesMenu(string RoleId);
         ICollection<MenuRoles> PesquisaMenuByRoles(string RoleId);
         void UpdateMenuRoles(MenuRoles menuRoles);

         IEnumerable<MenuRoles> GetMenuRolesTrueByRoleName(string roleNameId);
    }
}