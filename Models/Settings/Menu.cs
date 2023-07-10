using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using app.Models.Financa;
using app.Models.Identity;
using app.Models.ViewModel;

namespace app.Models.Settings
{
    public class Menu : GenericModel
    {
        public string Titulo { get; set; }
        public string Rota { get; set; }
        public string Icone { get; set; }
        public bool Status { get; set; }
        public string Perfil { get; set; }
        [NotMapped]
        public List<TagsViewModel> Tags { get; set; }
        public Menu()
        {
            Tags = new List<TagsViewModel>();
        }
    }
}