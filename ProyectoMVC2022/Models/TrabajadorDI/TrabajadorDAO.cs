using Microsoft.Data.SqlClient;
using ProyectoMVC2022.Models.ElectrodomDI;
using System.Data;

namespace ProyectoMVC2022.Models.TrabajadorDI
{
    public class TrabajadorDAO : ITrabajador
    {
        string cadena =
   @"server = DESKTOP-UI3J76O; database = CyberElec; Trusted_Connection = True;" +
   "MultipleActiveResultSets = True; TrustServerCertificate = False;Encrypt = False";
        public string actualizar(Trabajador tra)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("PA_ACTUALIZAR_TRABA", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cod_tra", tra.codtra);
                    cmd.Parameters.AddWithValue("@nom_tra", tra.nombre);
                    cmd.Parameters.AddWithValue("@apell", tra.apellido);
                    cmd.Parameters.AddWithValue("@direccion", tra.direccion);
                    cmd.Parameters.AddWithValue("@fecha_nac", tra.fecha);
                   

                    cmd.ExecuteNonQuery();
                    mensaje = "Se ha Actualizado : " + tra.nombre;
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                }
                finally { cn.Close(); }
            }
            return mensaje;
        }

        public string agregar(Trabajador tra)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("PA_INSERTAR_TRAB", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nom_tra", tra.nombre);
                    cmd.Parameters.AddWithValue("@apell", tra.apellido);
                    cmd.Parameters.AddWithValue("@direccion", tra.direccion);
                    cmd.Parameters.AddWithValue("@fecha_nac", tra.fecha);

                    cmd.ExecuteNonQuery();
                    mensaje = "Se ha Agregado : " + tra.nombre;
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                }
                finally { cn.Close(); }
            }
            return mensaje;
        }

   

        public Trabajador buscar(string usu, string clave)
        {
            Trabajador cli = null;
            //
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            //
            SqlCommand cmd = new SqlCommand("PA_BUSCAR_Tra", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@usu", usu);
            cmd.Parameters.AddWithValue("@cla", clave);
            //
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cli = new Trabajador();
                {
                    cli.codtra = dr.GetInt32(0);
                    cli.nombre = dr.GetString(1);
                    cli.apellido = dr.GetString(2);
                    cli.direccion = dr.GetString(3);
                    cli.fecha = dr.GetDateTime(4);
                   
                };

            }
            dr.Close();
            //
            cn.Close();
            //
            return cli;
        }

        public Trabajador buscartra(int cod)
        {
            Trabajador cli = null;
            //
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            //
            SqlCommand cmd = new SqlCommand("PA_BUSCAR_TRABApara", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codi", cod);
           
            //
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cli = new Trabajador();
                {
                    cli.codtra = dr.GetInt32(0);
                    cli.nombre = dr.GetString(1);
                    cli.apellido = dr.GetString(2);
                    cli.direccion = dr.GetString(3);
                    cli.fecha = dr.GetDateTime(4);

                };

            }
            dr.Close();
            //
            cn.Close();
            //
            return cli;
        }

        public string eliminar(int cod)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("PA_ELIMINAR_TRABA", cn);
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

        public IEnumerable<Trabajador> listatra()
        {
            //ejecutar el procedure donde liste y retorne los registros tb_accesorios
            List<Trabajador> acce = new List<Trabajador>();
            using (SqlConnection con = new SqlConnection(cadena))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PA_LISTAR_TRABA_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Trabajador ele = new Trabajador()
                    {
                        codtra = dr.GetInt32(0),
                        nombre = dr.GetString(1),
                        apellido = dr.GetString(2),
                        direccion = dr.GetString(3),
                        fecha = dr.GetDateTime(4),
                       
                    };
                    acce.Add(ele);
                }
                con.Close();
            }
            return acce;
        }
    }
}
