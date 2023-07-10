
using System.Collections.Generic;
using app.Models.Settings;
using app.Repositorios.Base;

namespace app.Repositorios.Abstratos
{
    public interface IMenu : IRepositorioBase<Menu>
    {
        IList<Menu> PesquisarPorNome(string nome);
        IEnumerable<Menu> PesquisaPorAcesso(string regra);
    }
}