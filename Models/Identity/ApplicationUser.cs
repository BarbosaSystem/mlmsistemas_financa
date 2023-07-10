using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace app.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName {get;set;}
        public string photoUrl {get;set;}
        public byte[] UserPhoto { get; set; }
        public string RefreshToken {get;set;}
    }
}