using System;

namespace app.Models.Financa
{
    public class Movimentacao : GenericModel
    {
        public Movimentacao() 
        {
            
        }
        /* public Movimentacao(int id, DateTime data, string descricao, decimal valor, int categoriaId, bool status)
        {
            this.Id = id;
            this.Data = data;
            this.Descricao = descricao;
            this.Valor = valor;
            this.CategoriaId = categoriaId;
            this.Status = status;

        } */
        /* public int Id { get; set; } */
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
        public bool Status { get; set; }

        
    }
}