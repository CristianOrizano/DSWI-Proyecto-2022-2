
using Microsoft.Data.SqlClient;
using System.Data;
using ProyectoMVC2022.Models;
using System.Net;

namespace ProyectoMVC2022.Models.ClienteDI
{
    public class ClienteDAO : ICLiente
    {
        string cadena =
     @"server = DESKTOP-UI3J76O; database = CyberElec; Trusted_Connection = True;" +
     "MultipleActiveResultSets = True; TrustServerCertificate = False;Encrypt = False";
        public string actualizar(Cliente cli)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("PA_ACTUALIZAR_Cli", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codi", cli.codigo);
                    cmd.Parameters.AddWithValue("@nom_cli", cli.nombre);
                    cmd.Parameters.AddWithValue("@apell", cli.apellido);
                    cmd.Parameters.AddWithValue("@dni", cli.dni);
                    cmd.Parameters.AddWithValue("@sexo", cli.sexo);
                    cmd.Parameters.AddWithValue("@fecha_nac", cli.fecha);

                    cmd.ExecuteNonQuery();
                    mensaje = "Se ha Actualizado : " + cli.nombre;
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                }
                finally { cn.Close(); }
            }
            return mensaje;

        }

        public string agregar(Cliente cli)
        {
            string mensaje = "";

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("PA_INSERTAR_CLI", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nom_cli", cli.nombre);
                    cmd.Parameters.AddWithValue("@apell", cli.apellido);
                    cmd.Parameters.AddWithValue("@dni", cli.dni);
                    cmd.Parameters.AddWithValue("@sexo", cli.sexo);
                    cmd.Parameters.AddWithValue("@fecha_nac",cli.fecha );
                    
                    cmd.ExecuteNonQuery();
                    mensaje = "Se ha Agregado : " + cli.nombre;
                }
                catch (SqlException ex)
                {
                    mensaje =ex.Message;
                }
                finally { cn.Close(); }
            }
            return mensaje;
        }

        public Cliente buscar(int codele)
        {
            Cliente cli = null;
            //
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            //
            SqlCommand cmd = new SqlCommand("PA_BUSCAR_CLI", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codi", codele);
            //
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                cli = new Cliente();
                {
                    cli.codigo = dr.GetInt32(0);
                    cli.nombre = dr.GetString(1);
                    cli.apellido = dr.GetString(2);
                    cli.dni = dr.GetString(3);
                    cli.sexo = dr.GetString(4);
                    cli.fecha = dr.GetDateTime(5);
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
                    SqlCommand cmd = new SqlCommand("PA_ELIMINAR_CLI", cn);
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

        public IEnumerable<Cliente> listaCli()
        {
            //ejecutar el procedure donde liste y retorne los registros tb_accesorios
            List<Cliente> acce = new List<Cliente>();
            using (SqlConnection con = new SqlConnection(cadena))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("PA_LISTAR_CLI_list", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Cliente ele = new Cliente()
                    {
                        codigo = dr.GetInt32(0),
                        nombre = dr.GetString(1),
                        apellido = dr.GetString(2),
                        dni = dr.GetString(3),
                        sexo = dr.GetString(4),
                        fecha = dr.GetDateTime(5)
                    };
                    acce.Add(ele);
                }
                con.Close();
            }
            return acce;
        }
    }
}
