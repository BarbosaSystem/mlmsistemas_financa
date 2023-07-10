using System.ComponentModel.DataAnnotations;

namespace app.Models.ViewModel
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}