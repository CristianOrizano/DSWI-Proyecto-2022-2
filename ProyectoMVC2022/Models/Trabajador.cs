using System.ComponentModel.DataAnnotations;

namespace ProyectoMVC2022.Models
{
    public class Trabajador
    {
        public int codtra { get; set; }
        [Display(Name = "Nombre")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Min:3  Max:15 caracteres")]
        [Required]
        public string nombre { get; set; }
        [Display(Name = "Apellido")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Min:3  Max:15 caracteres")]
        [Required]
        public string apellido { get; set; }
        [Display(Name = "Direccion")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Min:3  Max:15 caracteres")]
        [Required]
        public string direccion { get; set; }
        [Display(Name = "Fecha Nacimiento")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true,
                      DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime fecha { get; set; }
    }
}
