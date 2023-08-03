namespace ProyectoMVC2022.Models.ClienteDI
{
    public interface ICLiente
    {
        //definen los metodos

        IEnumerable<Cliente> listaCli();
        string agregar(Cliente cli);
        string actualizar(Cliente cli);
        string eliminar(int cod);
        Cliente buscar(int codele);
    }
}
