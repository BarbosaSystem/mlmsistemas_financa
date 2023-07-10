using System.Linq;
using app.Contexto;
using app.Models.Financa;
using app.Repositorios.Abstratos;

namespace app.Repositorios.Concretos
{
    public class EFPeriodo : EFRepositorio<Periodo>, IPeriodo
    {
        public EFPeriodo(DemoDbContext context) : base(context)
        {
        }

        public Periodo EncerrarPeriodo(int codigo)
        {
            var periodo = PesquisarTodos().Where(
                x => x.Id.Equals(codigo)).FirstOrDefault();

            return periodo;
        }

        public Periodo PesquisaReferencia(string referencia)
        {
            var periodo = PesquisarTodos().Where(x => x.referencia.Equals(referencia)).FirstOrDefault();
            return periodo;
        }

        public Periodo UltimoPeriodo()
        {
            return PesquisarTodos().Where(x => x.status.Equals(true)).OrderByDescending(x => x.Id).First();
        }
    }
}