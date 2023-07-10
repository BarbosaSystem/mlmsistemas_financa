using System.Collections.Generic;
using app.Models.Financa;
using app.Repositorios.Base;

namespace app.Repositorios.Abstratos
{
    public interface ICategoria : IRepositorioBase<Categoria>
    {
         IList<Categoria> PesquisarPorNome(string nome);
    }
}