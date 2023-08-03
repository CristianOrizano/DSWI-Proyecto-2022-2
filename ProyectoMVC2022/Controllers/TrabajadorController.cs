using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoMVC2022.Models;
using ProyectoMVC2022.Models.ClienteDI;
using ProyectoMVC2022.Models.ElectrodomDI;
using ProyectoMVC2022.Models.TrabajadorDI;

namespace ProyectoMVC2022.Controllers
{
    public class TrabajadorController : Controller
    {
       
        //definir la interfaz
        ITrabajador _inyector;

        //constructor
        public TrabajadorController()
        {
            _inyector = new TrabajadorDAO(); //la definicion del inyector
        }
        public IActionResult Login(string usu="",string cla="")
        {
            Trabajador tr = _inyector.buscar(usu,cla);
            if(tr == null)
            {
                ViewBag.mensa = "Usuario y Clave incorrecto";
            }
            else
            {
                HttpContext.Session.SetInt32("codiTra",tr.codtra);
                HttpContext.Session.SetString("Bien",tr.nombre+tr.apellido);
                HttpContext.Session.SetString("nomtra",tr.nombre);
                HttpContext.Session.SetString("apetra",tr.apellido);
                return RedirectToAction("inicio", "Inicio");
            }

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoginCierra()
        {
            //para cerrar
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult CreateTrabaj()
        {
            //utilizando el link de Details, visualizo el Accesorio por su codigo
            Trabajador reg = new Trabajador();
            //envio la lista de accesorios vista parcial
            ViewBag.cliente = _inyector.listatra();
            return View(reg);
        }


        [HttpPost]
        public IActionResult CreateTrabaj(Trabajador reg)
        {
            if (ModelState.IsValid) //validacion de las notaciones
            {
                ViewBag.mensaje = _inyector.agregar(reg);
            }


            ViewBag.cliente = _inyector.listatra();
            return View(reg);
        }

        public IActionResult ActualizarTRAB(int cod)
        {
            Trabajador reg = _inyector.buscartra(cod);
            HttpContext.Session.SetInt32("mugi", reg.codtra);
            ViewBag.cliente = _inyector.listatra();
            return View(reg);
        }

        [HttpPost]
        public IActionResult ActualizarTRAB(Trabajador reg)
        {
            if (ModelState.IsValid) //validacion de las notaciones
            {
                ViewBag.mensaje = _inyector.actualizar(reg);
            }

            ViewBag.cliente = _inyector.listatra();
            return View(reg);
        }

        public ActionResult EliminarTRA(int id)
        {
            Trabajador reg = _inyector.buscartra(id);

            ViewBag.cliente = _inyector.listatra();
            return View(reg);
        }

        // POST: Empleado/Delete/5
        [HttpPost]
        public ActionResult EliminarTRA(int id, int ori = 0)
        {

            if (ModelState.IsValid) //validacion de las notaciones
            {
                ViewBag.mensaje = _inyector.eliminar(id);
            }

            ViewBag.cliente = _inyector.listatra();
            return View();
        }




    }
}
