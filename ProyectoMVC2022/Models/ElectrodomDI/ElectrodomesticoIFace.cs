namespace ProyectoMVC2022.Models.ElectrodomDI
{
    public interface ElectrodomesticoIFace
    {
        //definen los metodos

        //metodo para generar codigo
        IEnumerable<Categoria> listaCategorias();
        string GenerCOdi();
        IEnumerable<Electrodomestico> listaElec();
        IEnumerable<Reporte> listaReporte();
        IEnumerable<Electrodomestico> listaDetalle(string bole);
        string agregar(Electrodomestico ele);
        string actualizar(Electrodomestico ele);
        string eliminar(string cod);
        Electrodomestico buscar(string codele);

    }
}
