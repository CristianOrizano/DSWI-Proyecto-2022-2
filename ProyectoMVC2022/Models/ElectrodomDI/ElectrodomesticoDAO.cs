
using Microsoft.Data.SqlClient;
using ProyectoMVC2022.Models;
using System.Data;

namespace ProyectoMVC2022.Models.ElectrodomDI
{
    public class ElectrodomesticoDAO : ElectrodomesticoIFace
    {
        string cadena =
     @"server = DESKTOP-UI3J76O; database = CyberElec; Trusted_Connection = True;" +
     "MultipleActiveResultSets = True; TrustServerCertificate = False;Encrypt = False";

        public string actualizar(Electrodomestico ele)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("PA_ACTUALIZAR_ELEC", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codi", ele.codigo);
                    cmd.Parameters.AddWithValue("@idcate", ele.ide_cate);
                    cmd.Parameters.AddWithValue("@descrip", ele.descripcion);
                    cmd.Parameters.AddWithValue("@stock", ele.stock);
                    cmd.Parameters.AddWithValue("@precio", ele.precio);
                    cmd.Parameters.AddWithValue("@marca", ele.marca);
                    cmd.ExecuteNonQuery();
                    mensaje = "Se ha Actualizado : " + ele.descripcion;
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                }
                finally { cn.Close(); }
            }
            return mensaje;
        }

        public string agregar(Electrodomestico ele)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("PA_INSERTAR_ELEC", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codi", ele.codigo);
                    cmd.Parameters.AddWithValue("@idcate", ele.ide_cate);
                    cmd.Parameters.AddWithValue("@descrip", ele.descripcion);
                    cmd.Parameters.AddWithValue("@stock", ele.stock);
                    cmd.Parameters.AddWithValue("@precio", ele.precio);
                    cmd.Parameters.AddWithValue("@marca", ele.marca);
                    cmd.ExecuteNonQuery();
                    mensaje = "Se ha Agregado : " + ele.descripcion;
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                }
                finally { cn.Close(); }
            }
            return mensaje;
        }

        public Electrodomestico buscar(string codele)
        {
            Electrodomestico em = null;
            //
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            //
            SqlCommand cmd = new SqlCommand("PA_BUSCAR_ELE", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codi", codele);
            //
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                em = new Electrodomestico();
                {
                    em.codigo = dr.GetString(0);
                    em.ide_cate = dr.GetInt32(1);
                    em.descripcion = dr.GetString(2);
                    em.stock = dr.GetInt32(3);
                    em.precio = dr.GetDecimal(4);
                    em.marca = dr.GetString(5);
                };

            }
            dr.Close();
            //
            cn.Close();
            //
            return em;
        }

        public string eliminar(string cod)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("PA_ELIMINAR_ELEC", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codi", cod);
                    
                    cmd.ExecuteNonQuery();
                    mensaje = "Se ha Eliminado : " + cod;
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                }
                finally { cn.Close(); }
            }
            return mensaje;
        }

        public string GenerCOdi()
        {
            string cod = "0";
            //
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            //
            SqlCommand cmd = new SqlCommand("SP_genercod", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cod = dr.GetString(0);
            }
            dr.Close();
            cn.Close();
            //
            return cod;
        }

        public IEnumerable<Categoria> listaCategorias()
        {
            //ejecutar el procedure donde liste y retorne los registros tb_accesorios
            List<Categoria> acce = new List<Categoria>();
            using (SqlConnection con = new SqlConnection(cadena))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PA_LISTAR_CATE", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Categoria cate = new Categoria()
                    {
                        idcategoria = dr.GetInt32(0),
                        descripcion = dr.GetString(1),
                      
                    };
                    acce.Add(cate);
                }
                con.Close();
            }
            return acce;
        }

        public IEnumerable<Electrodomestico> listaDetalle(string bole)
        {
            //ejecutar el procedure donde liste y retorne los registros tb_accesorios
            List<Electrodomestico> acce = new List<Electrodomestico>();
            using (SqlConnection con = new SqlConnection(cadena))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_buscarDetalle", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fact", bole);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Electrodomestico cate = new Electrodomestico()
                    {
                        codigo = dr.GetString(0),
                        descripcion = dr.GetString(1),
                        stock = dr.GetInt32(2),
                        precio=dr.GetDecimal(3),

                    };
                    acce.Add(cate);
                }
                con.Close();
            }
            return acce;



        }

        public IEnumerable<Electrodomestico> listaElec()
        {
            //ejecutar el procedure donde liste y retorne los registros tb_accesorios
            List<Electrodomestico> acce = new List<Electrodomestico>();
            using (SqlConnection con = new SqlConnection(cadena))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PA_LISTAR_ELEC_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Electrodomestico ele = new Electrodomestico()
                    {
                        codigo = dr.GetString(0),
                        nombrecate = dr.GetString(1),
                        descripcion = dr.GetString(2),
                        stock = dr.GetInt32(3),
                        precio = dr.GetDecimal(4),
                         marca = dr.GetString(5)
                    };
                    acce.Add(ele);
                }
                con.Close();
            }
            return acce;
        }

        //lista repor

        public IEnumerable<Reporte> listaReporte()
        {
            //ejecutar el procedure donde liste y retorne los registros tb_accesorios
            List<Reporte> acce = new List<Reporte>();
            using (SqlConnection con = new SqlConnection(cadena))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_listar_reporte", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Reporte ele = new Reporte()
                    {
                        numbol = dr.GetString(0),
                        fecha = dr.GetDateTime(1),
                        tranom = dr.GetString(2),
                        clinom = dr.GetString(3),
                        total = dr.GetDecimal(4),

                    };
                    acce.Add(ele);
                }
                con.Close();
            }
            return acce;
        }
    }
}
