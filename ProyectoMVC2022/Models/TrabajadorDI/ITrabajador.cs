namespace ProyectoMVC2022.Models.TrabajadorDI
{
    public interface ITrabajador
    {
        //definen los metodos
        IEnumerable<Trabajador> listatra();
        string agregar(Trabajador tra);
        string actualizar(Trabajador tra);
        string eliminar(int cod);
        Trabajador buscar(string usu,string clave);
        Trabajador buscartra(int cod);

    }
}
