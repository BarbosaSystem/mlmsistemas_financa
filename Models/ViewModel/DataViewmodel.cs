using System;

namespace app.Models.ViewModel
{
    public class DataViewmodel
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public DataViewmodel(string _datainicio, string _datafim)
        {
            DataInicio = Convert.ToDateTime(_datainicio);
            DataFim = Convert.ToDateTime(_datafim);
        }
    }

}