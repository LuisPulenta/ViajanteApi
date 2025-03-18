using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViajanteApi.Web.Data.Entities
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cliente")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Customer { get; set; }

        [Display(Name = "Tipo")]
        [MaxLength(10, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Type { get; set; }

        [Display(Name = "N°")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Number { get; set; }

        [Display(Name = "Fecha Creación")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime BillDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Monto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Amount { get; set; }

        [Display(Name = "Foto")]
        public string Photo { get; set; }

        [Display(Name = "Fecha Entrega")]
        public DateTime? ChargeDate { get; set; }

        [Display(Name = "Entregado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool Charge { get; set; }

        [Display(Name = "Fecha Cobro")]
        public DateTime? DeliverDate { get; set; }

        [Display(Name = "Cobrado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public bool Deliver { get; set; }

        public string PhotoFullPath => string.IsNullOrEmpty(Photo)
        ? $"https://keypress.serveftp.net/ViajanteApi/images/logos/noimage.png"
        : $"https://keypress.serveftp.net/ViajanteApi{Photo.Substring(1)}";

    }
}


