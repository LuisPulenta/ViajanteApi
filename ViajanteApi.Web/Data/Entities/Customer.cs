using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViajanteApi.Web.Data.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cliente")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        public ICollection<Bill>? Bills { get; set; }

       
    }
}


