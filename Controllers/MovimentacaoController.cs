using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using app.Models;
using app.Models.Financa;
using app.Models.ViewModel;
using app.Repositorios.Abstratos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMovimentacao _movimento;
        private readonly ICategoria _categoria;
        public MovimentacaoController(IMovimentacao movimento, ICategoria categoria)
        {
            _movimento = movimento;
            _categoria = categoria;
        }

        [HttpPost]
        [Route("CarregarListaMovimentos")]
        public IActionResult ListPostMovimentacaoAsync([FromBody] IEnumerable<MovimentoViewModel> movimentacao)
        {
            if (ModelState.IsValid)
            {
                var contSucess = 0;
                var constError = 0;
                foreach (var item in movimentacao)
                {
                    var movimento = new Movimentacao
                    {
                        Data = Convert.ToDateTime(item.Data),
                        Valor = item.Valor,
                        Descricao = item.Descricao,
                        CategoriaId = item.CategoriaId,
                        Status = item.Status
                    };

                    if (_movimento.PesquisaByObjeto(movimento))
                    {
                        _movimento.Inserir(movimento);
                        contSucess++;
                    }
                    else
                    {
                        constError++;
                    }
                }
                return Ok(contSucess + " cadastrados com sucesso! " + constError + " cadastros já existentes");
            }
            else
            {
                return BadRequest("Não foi possível executar. Verifique e tente novamente");
            }
        }


        [HttpPost]
        public IActionResult PostMovimentacaoAsync(MovimentoViewModel movimentacao)
        {
            if (ModelState.IsValid)
            {
                var movimento = new Movimentacao
                {
                    Data = Convert.ToDateTime(movimentacao.Data),
                    Valor = movimentacao.Valor,
                    Descricao = movimentacao.Descricao,
                    CategoriaId = movimentacao.CategoriaId,
                    Status = movimentacao.Status
                };
                if (_movimento.PesquisaByObjeto(movimento))
                {
                    _movimento.Inserir(movimento);
                    return Ok("Registro cadastrado com sucesso");
                }
                else
                {
                    return Ok("Movimentação já cadastrada");
                }
            }
            else
            {
                return BadRequest("Favor verificar");
            }
        }

        [HttpPut]
        public IActionResult PutMovimentacao(Movimentacao _movimentacao)
        {
            if (ModelState.IsValid)
            {
                var movimento = _movimento.PesquisarTodos().Where(x => x.Id.Equals(_movimentacao.Id)).FirstOrDefault();

                if (!movimento.Equals(null))
                {
                    movimento.Data = _movimentacao.Data;
                    movimento.Descricao = _movimentacao.Descricao;
                    movimento.CategoriaId = _movimentacao.CategoriaId;

                    _movimento.Atualizar(movimento);
                    return Ok("Atualizado");
                }
                else
                {
                    return Ok("Registro não localizado.");
                }
            }
            else
            {
                return BadRequest("Erro encontrado");
            }
        }

        [HttpGet]
        public IActionResult GetMovimentacao()
        {
            var movimento = (from movi in _movimento.PesquisarTodos().OrderByDescending(x => x.Data).Take(200)
                             select new
                             {
                                 Categoria = _categoria.PesquisarTodos()
                                     .Where(x => x.Id.Equals(movi.CategoriaId))
                                     .Select(x => x.Nome)
                                     .FirstOrDefault(),
                                 Data = movi.Data,
                                 movi.Descricao,
                                 movi.Id,
                                 valor = movi.Valor.ToString("N2"),
                                 Status = movi.Status
                             }
            );
            return Ok(movimento);
        }
        [HttpGet]
        [Route("UltimosLancamentos")]
        public IActionResult UltimosLancamentos()
        {
            var movimento = _movimento.UltimosLancamentos();
            if (movimento != null)
                return Ok(movimento);
            else
                return Ok("Movimentos não encontrados");
        }
        [HttpGet]
        [Route("PesquisaPendentes")]
        public IActionResult GetPesquisaPendente()
        {
            return Ok(_movimento.PesquisaPendentes());
        }

        [HttpDelete]
        [Route("DeleteMovimento/{id}")]
        public IActionResult DeleteMovimento(int id)
        {

            var movimento = _movimento.PesquisarTodos().Where(x => x.Id.Equals(id)).FirstOrDefault();
            _movimento.Excluir(movimento);

            var movimento_excluido = _movimento.PesquisaByObjeto(movimento);
            if (movimento_excluido)
                return Ok("Registro Excluído com sucesso");
            else
            {
                return Ok("Não foi possível executar esta operação.");
            }

        }

       /*  [HttpGet]
        [Route("BuscaMovimento/{id}")]
        public IActionResult BuscarMovimento(int id)
        {
            var movimento = (from mov in _movimento.PesquisarTodos().Where(x => x.Id.Equals(id))
                            select new {
                                id = mov.Id,
                                descricao = mov.Descricao,
                                valor = mov.Valor,
                                categoria = _categoria.PesquisarPorId(x => x.Id.Equals(mov.CategoriaId)).Nome,
                                data = mov.Data
                            });
            if (movimento != null)
                return Ok(movimento.FirstOrDefault());
            else
                return Ok("Movimento não encontrado");

        } */
    }
}