using System;
using System.Collections.Generic;
using System.Linq;
using app.Contexto;
using app.Models.Settings;
using app.Repositorios.Abstratos;

namespace app.Repositorios.Concretos
{
    public class EFMenuRoles : EFRepositorio<MenuRoles>, IMenuRoles
    {
        public EFMenuRoles(DemoDbContext context) : base(context)
        {
            
        }

        public IEnumerable<MenuRoles> GetMenuRolesTrueByRoleName(string roleNameId)
        {
            var menuRoles= PesquisaMenuByRoles(roleNameId);
            return menuRoles.Where(x => x.Status.Equals(true));
        }

        public ICollection<MenuRoles> PesquisaMenuByRoles(string RoleId)
        {
            var item = PesquisarTodos().Where(x => x.RolesId.Equals(RoleId));
            return item.ToList();
        }

        public List<Menu> PesquisaMenuByRolesMenu(string RoleId)
        {
            var item = PesquisarTodos().Where(x => x.RolesId.Equals(RoleId));
            List<Menu> retorno = new List<Menu>();
            foreach (var itens in item)
            {   
                var result = _context.Menus.Where(x => x.Id.Equals(itens.MenuId) && x.Status).FirstOrDefault();
                if(result != null){
                    retorno.Add(result);
                } 
                
            }
            return retorno.ToList();
        }

        public ICollection<MenuRoles> PesquisaRolesByMenuId(string MenuId)
        {
            var item = PesquisarTodos().Where(x => x.MenuId.Equals(Convert.ToInt32(MenuId)));
            return item.ToList();
        }


        public void UpdateMenuRoles(MenuRoles menuRoles)
        {
            Atualizar(menuRoles);
        }

        
    }
}