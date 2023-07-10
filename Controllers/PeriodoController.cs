using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using app.Models;
using app.Models.Financa;
using app.Models.ViewModel;
using app.Repositorios.Abstratos;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodoController : ControllerBase
    {
        private readonly IPeriodo _periodo;
        private readonly IMovimentacao _movimentacao;
        public PeriodoController(IPeriodo periodo, IMovimentacao movimentacao)
        {
            _periodo = periodo;
            _movimentacao = movimentacao;
        }

        [HttpGet]
        public IActionResult GetPeriodo()
        {
            var periodo = _periodo.PesquisarTodos();
            return Ok(periodo);
        }

        [HttpGet]
        [Route("BuscarPeriodo/{id}")] 
        public IActionResult GetPeriodoById(int id)
        {
            var periodo = (from perio in _periodo.PesquisarTodos()
                .Where(x => x.Id.Equals(id))
                           select new
                           {
                               perio.Id,
                               DataFinal = perio.DataFinal.ToShortDateString(),
                               DataInicio = perio.DataInicio.ToShortDateString(),
                               valor = perio.Valor.ToString("N2"),
                               perio.referencia,
                               perio.status,
                               Movimentacao = NewMethod(perio),
                           }).FirstOrDefault();
            
            if (periodo != null)
            {
                return Ok(periodo);
            }
            else
            {
                return Ok("Periodo não encontrado");
            }
        }

        private IEnumerable<MovimentoPeriodo> NewMethod(Periodo perio)
        {
            return _movimentacao.PesquisaDatas(new DataViewmodel(perio.DataInicio.ToShortDateString(), perio.DataFinal.ToShortDateString())).OrderBy(x => x.Data);
            /* return _movimentacao.PesquisaDatas(new DataViewmodel(perio.DataInicio.ToShortDateString(), perio.DataFinal.ToShortDateString())); */
        }
        [HttpGet]
        [Route("UltimoPeriodo")]
        public IActionResult UltimoPeriodo()
        {
            var periodo = _periodo.UltimoPeriodo();
            Random r = new Random();
            var categoria = NewMethod(periodo).GroupBy(x => x.Categoria)
                                .Select(g => new 
										  {
                                            Nome_Categoria = g.First().Categoria,
                                            Valor = g.Sum(s => s.Valor),
                                            Cor = String.Format("#{0:X6}", r.Next(0x1000000))
										  });
            var resultado = new {
                periodo.Id,
                Fim = periodo.DataFinal.ToShortDateString(),
                Inicio = periodo.DataInicio.ToShortDateString(),
                periodo.referencia,
                periodo.Valor,
                categoria
            };

            return Ok(resultado);
        }
        [HttpGet]
        [Route("EncerrarPeriodo/{id}")] 
        public IActionResult EncerrarPeriodo(int id)
        {
            try
            {
                var periodo = _periodo.PesquisarTodos().Where(x => 
                x.Id.Equals(id)).FirstOrDefault();

                if (periodo.DataFinal >= DateTime.Now)
                {
                    return Ok("Encerramento do período não permitida!! \n A data atual é menor do que a data de encerramento do período");
                }
                else
                {
                    periodo.Valor = _movimentacao.SomaPeriodo(NewMethod(periodo));

                    if (periodo.Valor > 0)
                        periodo.status = true;

                    _periodo.Atualizar(periodo);

                    var atualizado = _periodo.PesquisarTodos().Where(x => x.status.Equals(true));

                    if (atualizado != null)
                        return Ok("Periodo encerrado com sucesso");
                    else
                        return Ok("Não foi possivel processar sua solicitação");
                }

            }
            catch (System.Exception)
            {
                return Ok("Não foi possivel processar sua solicitação");
            }
        }
        [HttpPost]
        public IActionResult PostPeriodoAsync(PeriodoViewModel periodo)
        {
            if(ModelState.IsValid){
                var referencia = _periodo.PesquisaReferencia(periodo.referencia);

                if(referencia != null){
                    return Ok("Periodo já cadastrado");
                }
                else{
                    var _period = new Periodo(){
                        referencia = periodo.referencia,
                        DataFinal = Convert.ToDateTime(periodo.DataFinal),
                        DataInicio = Convert.ToDateTime(periodo.DataInicial),
                        status = false,
                        Valor = 0
                    };
                    _periodo.Inserir(_period);
                    return Ok("Periodo Cadastrado com sucesso");
                }
            }
            else{
                return BadRequest("Não foi possivel executar esta operação.");
            }
        }
    }
}