using Microsoft.AspNetCore.Mvc;

//para el selecList
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Win32;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using System.Data;
using ProyectoMVC2022.Models;
using ProyectoMVC2022.Models.ClienteDI;
using ProyectoMVC2022.Models.ElectrodomDI;
using Microsoft.Data.SqlClient;


namespace ProyectoMVC2022.Controllers
{
    public class ElectroController : Controller
    {
        string cadena =
              @"server = DESKTOP-UI3J76O; database = CyberElec; Trusted_Connection = True;" +
              "MultipleActiveResultSets = True; TrustServerCertificate = False;Encrypt = False";

        //definir la interfaz
        ElectrodomesticoIFace _inyector;
        //definir la interfaz
        ICLiente _inyectorr;

        //constructor
        public ElectroController()
        {
            _inyector = new ElectrodomesticoDAO(); //la definicion del inyector
            _inyectorr = new ClienteDAO(); //la definicion del inyector
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListaClientes()
        {
            //envio la lista de accesorios vista parcial
            var lis = _inyectorr.listaCli();
            return View(lis);
        }



        public IActionResult Create()
        {
            //utilizando el link de Details, visualizo el Accesorio por su codigo
            Electrodomestico reg = new Electrodomestico();
            ViewBag.cli=HttpContext.Session.GetInt32("mugi");

            reg.codigo = _inyector.GenerCOdi();
            //envio la lista de categoria
            ViewBag.categorias = new SelectList(_inyector.listaCategorias(), "idcategoria", "descripcion");
            //envio la lista de electr vista parcial
            ViewBag.electro = _inyector.listaElec();
            return View(reg);
        }

        [HttpPost]
        public IActionResult Create(Electrodomestico reg)
        {
            if (ModelState.IsValid) //validacion de las notaciones
            {
                ViewBag.mensaje = _inyector.agregar(reg);
            }      
            //refrescar la pagina
            ViewBag.categorias = new SelectList(_inyector.listaCategorias(), "idcategoria", "descripcion");
            ViewBag.electro = _inyector.listaElec();
            return View();
        }

        public IActionResult ActualizarEle(string id)
        {
            Electrodomestico reg = _inyector.buscar(id);
            //envio la lista de categoria
            ViewBag.categorias = new SelectList(_inyector.listaCategorias(), "idcategoria", "descripcion");
            //envio la lista de accesorios vista parcial
            ViewBag.electro = _inyector.listaElec();
            return View(reg);
        }

        [HttpPost]
        public IActionResult ActualizarEle(Electrodomestico reg)
        {
            if (ModelState.IsValid) //validacion de las notaciones
            {
                ViewBag.mens = _inyector.actualizar(reg);           
            }
            //refrescar la pagina
            ViewBag.categorias = new SelectList(_inyector.listaCategorias(), "idcategoria", "descripcion");
            ViewBag.electro = _inyector.listaElec();
            return View();
        }

        public ActionResult EliminarElec(string id)
        {
            Electrodomestico reg = _inyector.buscar(id);
            //envio la lista de categoria
            ViewBag.categorias = new SelectList(_inyector.listaCategorias(), "idcategoria", "descripcion");
            //envio la lista de accesorios vista parcial
            ViewBag.electro = _inyector.listaElec();
            return View(reg);
        }

        // POST: Empleado/Delete/5
        [HttpPost]
        public ActionResult EliminarElec(string id,int ori=0)
        {

            if (ModelState.IsValid) //validacion de las notaciones
            {
                ViewBag.mens = _inyector.eliminar(id);
            }
            //refrescar la pagina
            ViewBag.categorias = new SelectList(_inyector.listaCategorias(), "idcategoria", "descripcion");
            ViewBag.electro = _inyector.listaElec();
            return View();
        }


        public IActionResult CatalogoElec()
        {
            //espacio para definir el Session canasta, no no existe definir
            if (HttpContext.Session.GetString("canasta") == null)
            {
                List<Carro> ca = new List<Carro>();
                HttpContext.Session.SetString("canasta", JsonConvert.SerializeObject(ca));

            }
            var lista = _inyector.listaElec();
            return View(lista);
        }

        public IActionResult seleccionarDeta(string id ="")
        {
            //buscar por idproducto
            Electrodomestico reg = _inyector.listaElec().FirstOrDefault(p => p.codigo == id);

            if (reg == null)
                return RedirectToAction("CatalogoElec");
            else
                return View(reg);
        }

        [HttpPost]
        public IActionResult seleccionarDeta(string idproducto, int cantidad)
        {
            //buscar al registro de producto por idproducto
            Electrodomestico reg = _inyector.listaElec().FirstOrDefault(p => p.codigo == idproducto);
            //instanciar Registro y pasar sus datos
            Carro it = new Carro()
            {
                idproducto = idproducto,
                descripcion = reg.descripcion,
                categoria = reg.nombrecate,
                precio = reg.precio,
                cantidad = cantidad,
            };

            //deserializar el Sesion canasta y lo almaceno en temporal
            List<Carro> temporal = JsonConvert.DeserializeObject<List<Carro>>(
           HttpContext.Session.GetString("canasta"));


            temporal.Add(it);
            //volver a serializar almacenando el Session
            HttpContext.Session.SetString("canasta", JsonConvert.SerializeObject(temporal));

            return RedirectToAction("Comprar");
            
        }

        public IActionResult Resumen()
        {
           
            //enviar el contenido del Sesion canasta a la vista
            return View(
            JsonConvert.DeserializeObject<List<Carro>>(HttpContext.Session.GetString("canasta")));

        }
        public IActionResult Delete(string id)
        {
            //eliminar el registro del Session canasta por idproducto y cantidad
            //deserializa
            List<Carro> registros =
              JsonConvert.DeserializeObject<List<Carro>>(HttpContext.Session.GetString("canasta"));

            Carro car = registros.Where(c => c.idproducto.Equals(id)).FirstOrDefault();
            registros.Remove(car);

            HttpContext.Session.SetString("canasta", JsonConvert.SerializeObject(registros));
            //redireccionar hacia el Resumen
            return RedirectToAction("Comprar");

        }
        public IActionResult Comprar(int id=0)
        {
            Cliente reg = _inyectorr.buscar(id);
            if(reg == null)
            {
                reg = new Cliente();
            }

            //espacio para definir el Session canasta, no no existe definir
            if (HttpContext.Session.GetString("canasta") == null)
            {
                HttpContext.Session.SetString("canasta", JsonConvert.SerializeObject(new List<Carro>()));
              
            }
            else
            {
             
                int can = 0;
                decimal pre = 0;
                List<Carro> temporal = JsonConvert.DeserializeObject<List<Carro>>(
                         HttpContext.Session.GetString("canasta"));

                foreach (Carro it in temporal)//leer cada registro de temporal
                {
                    pre += it.monto;
                    can += it.cantidad;

                }
                ViewBag.pre = pre;
                ViewBag.can = can;
                HttpContext.Session.SetString("total", pre + "");
            }
            var carr = JsonConvert.DeserializeObject<List<Carro>>(HttpContext.Session.GetString("canasta"));
            ViewBag.deta = carr;
            //para comprar necesito enviar un formulario para ingresar datos
            return View(reg);
        }
        
        [HttpPost]
        public IActionResult Comprar(Cliente reg)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                //como vas a guardar, cabecera,detalle y actualizar stock, definir transaccion
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    HttpContext.Session.SetString("Nombrecli",reg.nombre);
                    HttpContext.Session.SetString("apelcli", reg.apellido);
                    HttpContext.Session.SetString("dni", reg.dni);

                    string total = HttpContext.Session.GetString("total");
                    double totalfi = double.Parse(total);
                    //tb_pedidos
                    SqlCommand cmd = new SqlCommand("usp_RegisFac_Cab", cn, tr);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@num_fact", SqlDbType.VarChar,50).Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@cod_tra", HttpContext.Session.GetInt32("codiTra"));
                    cmd.Parameters.AddWithValue("@cod_cli",reg.codigo);
                    cmd.Parameters.AddWithValue("@total", totalfi);
                    cmd.ExecuteNonQuery();
                    string idpedido = (string)cmd.Parameters["@num_fact"].Value; //recupero el valor de @idpedido

                    HttpContext.Session.SetString("codpro", idpedido);
                    //tb_pedidos_deta
                    List<Carro> temporal = JsonConvert.DeserializeObject<List<Carro>>
                        (HttpContext.Session.GetString("canasta"));

                    foreach (Carro it in temporal)//leer cada registro de temporal
                    {
                        cmd = new SqlCommand("usp_RegisDeta_detalle", cn, tr);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@num_fact", idpedido);
                        cmd.Parameters.AddWithValue("@cod_ele", it.idproducto);
                        cmd.Parameters.AddWithValue("@cantidad", it.cantidad);
                        cmd.Parameters.AddWithValue("@preciovta", it.precio);
                        cmd.ExecuteNonQuery(); //ejecutar
                        //actualizar stock
                        cmd = new SqlCommand("usp_actualiza_stock", cn, tr);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idelec", it.idproducto);
                        cmd.Parameters.AddWithValue("@cant", it.cantidad);
                        cmd.ExecuteNonQuery();
                    }

                    tr.Commit(); //si todo esta OK
                    mensaje = $"La Transaccion se ha completado con exito";

                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                    tr.Rollback();
                }
                finally { cn.Close(); }
            }
            HttpContext.Session.SetString("mensa", mensaje);
            //HttpContext.Session.SetString("canasta", JsonConvert.SerializeObject(new List<Carro>()));
            //ViewBag.deta = JsonConvert.DeserializeObject<List<Carro>>(HttpContext.Session.GetString("canasta"));
            HttpContext.Session.Remove("canasta");

            return RedirectToAction("ReporteGene");


        }

        //vista del reporte gene
        public IActionResult ReporteGene()
        {
            //mensaje
            ViewBag.mensaje = HttpContext.Session.GetString("mensa");
            //cliente
            ViewBag.nomcli = HttpContext.Session.GetString("Nombrecli");
            ViewBag.apecli = HttpContext.Session.GetString("apelcli");
            ViewBag.dnicli = HttpContext.Session.GetString("dni");

            //vendedor
            ViewBag.nomtra = HttpContext.Session.GetString("nomtra");
            ViewBag.apetra = HttpContext.Session.GetString("apetra");

            ViewBag.total=HttpContext.Session.GetString("total");
            string cod = HttpContext.Session.GetString("codpro");
            var lista = _inyector.listaDetalle(cod);
            ViewBag.deta = lista; 
            return View(); 
        }

        //vista detalle
        public IActionResult Detalleventa()
        {
            return View();
        }

        //Reporte del menu
        public IActionResult ListaReporteVenta()
        {
            //envio la lista de accesorios vista parcial
            var lis = _inyector.listaReporte();
            return View(lis);
        }

    }
}
