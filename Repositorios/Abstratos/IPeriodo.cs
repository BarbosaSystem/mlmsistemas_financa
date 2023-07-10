using System.Collections.Generic;
using app.Models.Financa;
using app.Repositorios.Base;

namespace app.Repositorios.Abstratos
{
    public interface IPeriodo : IRepositorioBase<Periodo>
    {
         Periodo PesquisaReferencia(string referencia);

         Periodo EncerrarPeriodo(int codigo);
         Periodo UltimoPeriodo();
    }
}