using System;
using System.Collections.Generic;

namespace app.Models.Financa 
{
    public class Periodo : GenericModel
    {
        /* public int Id { get; set; } */
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
        public decimal  Valor {get;set;}
        public string referencia {get;set;}
        public bool status {get;set;}

    }
}