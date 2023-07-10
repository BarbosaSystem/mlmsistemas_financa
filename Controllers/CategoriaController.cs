using System.Linq;
using System.Collections.Generic;
using app.Models.Financa;
using app.Repositorios.Abstratos;
using Microsoft.AspNetCore.Mvc;
using app.Models.ViewModel;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoria _categoria;
        private readonly IPeriodo _periodo;
        private readonly IMovimentacao _movimentacao;

        public CategoriaController(ICategoria categoria, IPeriodo periodo, IMovimentacao movimentacao)
        {
            _movimentacao = movimentacao;
            _periodo = periodo;
            _categoria = categoria;
        }

        [HttpPost]
        public IActionResult PostCategoriaAsync(Categoria categoria)
        {
            if (categoria.Id == 0)
            {
                return Ok(PostarCategoria(categoria));
            }
            else
            {
                _categoria.Atualizar(categoria);
                return Ok("Atualizado");
            }
        }

        private string PostarCategoria(Categoria categoria)
        {
            var categoria_existente = _categoria.PesquisarPorNome(categoria.Nome);
            if (categoria_existente.Count == 0)
            {
                _categoria.Inserir(categoria);
                return "Categoria: '" + categoria.Nome + "', cadastrado com sucesso";
            }
            else
            {
                return categoria.Nome + " já está registrado no sistema";
            }
        }

        [HttpPost]
        [Route("PostListCategorias")]
        public IActionResult PostListCategorias(IEnumerable<Categoria> categorias)
        {

            if (ModelState.IsValid)
            {
                List<string> mensagem = new List<string>();
                foreach (var categoria in categorias)
                {
                    mensagem.Add(PostarCategoria(categoria));
                }
                return Ok(mensagem);
            }
            else
            {
                return BadRequest("Não foi possível executar esta opereação");
            }
        }

        [HttpGet]
        [Route("RelacaoCategorias")]
        public IActionResult GetCategoria()
        {
            var categorias = _categoria.PesquisarTodos();
            return Ok(categorias);
        }

        private List<ListaMovimentosPeriodoViewModel> GetMovimentacaoPorCategoria(int categoria_id)
        {
            var peri = _periodo.PesquisarTodos();
            var movi = _movimentacao.PesquisarTodos().Where(x => x.CategoriaId.Equals(categoria_id));
            var list_movimentos = new List<ListaMovimentosPeriodoViewModel>();
            foreach (var item in peri)
            {
                var valor = movi.Where(x => x.Data >= item.DataInicio && x.Data <= item.DataFinal).Sum(x => x.Valor);
                if (valor > 0.0M)
                {
                    list_movimentos.Add(new ListaMovimentosPeriodoViewModel
                    {
                        Referencia = item.referencia,
                        Lista_Movimentos = movi.Where(x => x.Data >= item.DataInicio && x.Data <= item.DataFinal).Sum(x => x.Valor)
                    });
                }

            }
            return list_movimentos;
        }

        [HttpGet]
        [Route("CategoriaPorPeriodo")]
        public IActionResult GetCategoriaPeriodo()
        {
            var catego = _categoria.PesquisarTodos().ToList();

            var lista = new List<object>();

            foreach (var item in catego)
            {
                lista.Add(new
                {
                    Categoria_Nome = item.Nome,
                    lista = GetMovimentacaoPorCategoria(item.Id)
                });
            }
            return Ok(lista);
        }

        [HttpGet]
        [Route("CategoriaPorPeriodoByCatId/{id}")]
        public IActionResult GetCategoriaPeriodoByCat_Id(int id)
        {
            var catego = _categoria.PesquisarPorId(x => x.Id.Equals(id));

            return Ok(new
            {
                Categoria_Nome = catego.Nome,
                Lista = GetMovimentacaoPorCategoria(catego.Id)
            });
        }
    }
}