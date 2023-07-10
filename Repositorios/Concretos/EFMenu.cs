using System.Collections.Generic;
using System.Linq;
using app.Contexto;
using app.Models.Settings;
using app.Repositorios.Abstratos;

namespace app.Repositorios.Concretos
{
    public class EFMenu : EFRepositorio<Menu>, IMenu
    {
        public EFMenu(DemoDbContext context) : base(context)
        {
        }

        public IEnumerable<Menu> PesquisaPorAcesso(string regra)
        {
            return _context.Menus.Where(x => x.Perfil.Equals(regra)).ToList();
        }

        public IList<Menu> PesquisarPorNome(string nome)
        {
            return _context.Menus.Where(x => x.Titulo.Equals(nome)).ToList();
        }
    }
}