using System.Collections.Generic;

namespace app.Models.Financa
{
    public class Categoria : GenericModel
    {
        public string Nome { get; set; }
        public virtual List<Movimentacao> Movimentacao { get; set; }
    }
}