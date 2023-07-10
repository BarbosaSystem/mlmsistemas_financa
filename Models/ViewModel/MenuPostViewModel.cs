using System.Collections.Generic;

namespace app.Models.ViewModel
{
    public class MenuPostViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Rota { get; set; }
        public string Icone { get; set; }
        public bool Status { get; set; }
        public List<TagsViewModel> Tags { get; set; }
    }
}