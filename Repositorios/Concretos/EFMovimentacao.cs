using System.Collections.Generic;
using System.Linq;
using app.Contexto;
using app.Models.Financa;
using app.Models.ViewModel;
using app.Repositorios.Abstratos;

namespace app.Repositorios.Concretos
{
    public class EFMovimentacao : EFRepositorio<Movimentacao>, IMovimentacao
    {
        public EFMovimentacao(DemoDbContext context) : base(context)
        {
        }
        public bool PesquisaByObjeto(Movimentacao movimento)
        {
            var moviment = PesquisarTodos()
            .Where(x => x.Descricao.Equals(movimento.Descricao))
            .Where(x => x.Valor.Equals(movimento.Valor))
            .Where(x => x.Data.Equals(movimento.Data)).FirstOrDefault();

            /* .Where(x => x.Categoria.Equals(movimento.Categoria)) */
            if(moviment != null)
                return false;
            else
                return true;
        }
        public IEnumerable<MovimentoPeriodo> PesquisaDatas(DataViewmodel datas)
        {
            var movimentos = (from peri in PesquisarTodos()
                .Where(x => x.Data >= datas.DataInicio && x.Data <= datas.DataFim)
                              let categoria = _context.Categorias.Where(t => t.Id.Equals(peri.CategoriaId)).FirstOrDefault()
                              let p = new MovimentoPeriodo(
                                    _context.Categorias.Where(x => x.Id.Equals(peri.CategoriaId)).FirstOrDefault(),
                                    peri.Id,
                                    peri.Status,
                                    peri.Valor,
                                    peri.Descricao,
                                    peri.Data.ToLocalTime(),
                                    peri.CategoriaId
                              )
                              select p);
            return movimentos;
        }
        public IEnumerable<MovimentoPeriodo> PesquisaEntreDatas(DataViewmodel datas)
        {
            var __movimentos = PesquisarTodos().Where(x =>
                x.Data >= datas.DataInicio && x.Data <= datas.DataFim)
                .Select(s =>
                    s);

            var teste = (from movi in __movimentos.Where(x =>
                x.Data >= datas.DataInicio && x.Data <= datas.DataFim)
                select new {
                    movi.Categoria.Nome,
                    movi.Valor,
                    Data = movi.Data.ToString("dd/MM/yyyy"),
                    movi.Descricao,
                    movi.Id,
                    movi.Status

                });
            return new List<MovimentoPeriodo>();
        }
        public decimal SomaPeriodo(IEnumerable<MovimentoPeriodo> movimento)
        {
            return movimento.Sum(x=>x.Valor);
        }
        public IEnumerable<Movimentacao> PesquisaPendentes()
        {
            var movimento = PesquisarTodos().Where(x => x.Categoria == null);
            
            return movimento.ToList();
        }
        public IEnumerable<object> UltimosLancamentos()
        {
            var movimento = (from movi in PesquisarTodos().OrderByDescending(x =>x.Data).Take(5)
                select new {
                    Categoria = _context.Categorias
                        .Where(x => x.Id.Equals(movi.CategoriaId))
                        .Select(x => x.Nome).FirstOrDefault(),
                    Data = movi.Data.ToShortDateString(),
                    movi.Descricao,
                    movi.Id,
                    valor = movi.Valor
                });
            
            return movimento.ToList();
        }
    }
}