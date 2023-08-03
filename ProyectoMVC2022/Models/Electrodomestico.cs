// DataAnnotations
using System.ComponentModel.DataAnnotations;

namespace ProyectoMVC2022.Models
{
    public class Electrodomestico
    {
        [Required(ErrorMessage ="Codigo requerido")]
        [Display(Name = "Codigo")]
        public string codigo { get; set; }

        [Display(Name = "Categoria")]
        [Required]
        public int ide_cate { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Descripcion requerido")]
        [RegularExpression("[a-zA-Z ]{3,30}", ErrorMessage = "Min 3 a 10 caracteres")]
        //[StringLength(maximumLength: 15, MinimumLength = 3, ErrorMessage = "Min:3  Max:15 caracteres")]
        public string descripcion { get; set; }

        [Display(Name = "Stock")]
        [Required(ErrorMessage = "Stock requerido")]
        [Range(10, 50, ErrorMessage = "Min:10  Max:50 ")]
        public int stock { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Precio requerido")]
        [Range(100, 6000, ErrorMessage = "precio Min100 Max:6000")]
        public decimal precio { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Marca requerido")]
        [RegularExpression("[a-zA-Z ]{3,30}", ErrorMessage = "Min 3 a 10 caracteres")]
        public string marca { get; set; }

        public int estado { get; set; }

        public string nombrecate { get; set; }

        public Electrodomestico()
        {
            codigo = "";
            marca = "";
            descripcion = "";
            nombrecate = "";         
        }

    }
}
