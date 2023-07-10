using System.Collections.Generic;
using app.Models.Financa;
using app.Models.ViewModel;
using app.Repositorios.Base;

namespace app.Repositorios.Abstratos
{
    public interface IMovimentacao : IRepositorioBase<Movimentacao>
    {
         bool PesquisaByObjeto(Movimentacao movimento);
         IEnumerable<Movimentacao> PesquisaPendentes();
         IEnumerable<MovimentoPeriodo> PesquisaEntreDatas(DataViewmodel datas);
         IEnumerable<MovimentoPeriodo> PesquisaDatas(DataViewmodel datas);
         decimal SomaPeriodo(IEnumerable<MovimentoPeriodo> movimento);
         IEnumerable<object> UltimosLancamentos();
    }
}