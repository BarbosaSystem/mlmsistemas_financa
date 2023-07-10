using System;
using app.Models.Financa;

namespace app.Models.ViewModel
{
    public class MovimentoPeriodo
    {
        public MovimentoPeriodo(Categoria categoria, int id, bool status, decimal valor, string descricao, DateTime data, int categoriaId)
        {
            Id = id;
            Status = status;
            Valor = valor;
            Descricao = descricao;
            Data = data;
            Categoria = categoria.Nome;
        }

        public int Id {get; set;}
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Categoria { get; set; }
        public bool Status { get; set; }
    }
}