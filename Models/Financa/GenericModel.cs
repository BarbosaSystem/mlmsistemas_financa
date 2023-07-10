using System.ComponentModel.DataAnnotations.Schema;

namespace app.Models.Financa
{
    public class GenericModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}