namespace app.Models.ViewModel
{
    public class MovimentoViewModel
    {
        public string Data { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public int CategoriaId { get; set; }
        public bool Status {get;set;}
    }
}