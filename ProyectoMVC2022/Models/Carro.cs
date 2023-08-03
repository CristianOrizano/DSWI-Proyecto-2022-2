namespace ProyectoMVC2022.Models
{
    public class Carro
    {
        public string idproducto { get; set; }
        public string descripcion { get; set; }
        public string categoria { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal monto { get { return precio * cantidad; } }

        public Carro()
        {
            idproducto= "";
            descripcion = "";
            categoria = "";

        }
    }
}
