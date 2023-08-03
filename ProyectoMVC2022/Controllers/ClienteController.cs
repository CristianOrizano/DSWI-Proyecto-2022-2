using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//para el selecList
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoMVC2022.Models;
using ProyectoMVC2022.Models.ClienteDI;
using ProyectoMVC2022.Models.ElectrodomDI;

namespace ProyectoMVC2022.Controllers
{
    public class ClienteController : Controller
    {

        //definir la interfaz
        ICLiente _inyector;

        //constructor
        public ClienteController()
        {
            _inyector = new ClienteDAO(); //la definicion del inyector
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateCliente()
        {
            //utilizando el link de Details, visualizo el Accesorio por su codigo
            Cliente reg = new Cliente();
            //envio la lista de accesorios vista parcial
            ViewBag.cliente = _inyector.listaCli();
            return View(reg);
        }
      

        [HttpPost]
        public IActionResult CreateCliente(Cliente reg)
        {
            if (ModelState.IsValid) //validacion de las notaciones
            {
                ViewBag.mensaje = _inyector.agregar(reg);
            }
           

            ViewBag.cliente = _inyector.listaCli();
            return View(reg);
        }

        public IActionResult ActualizarEle(int cod)
        {
            Cliente reg = _inyector.buscar(cod);
            HttpContext.Session.SetInt32("mugi", reg.codigo);
            ViewBag.cliente = _inyector.listaCli();
            return View(reg);
        }

        [HttpPost]
        public IActionResult ActualizarEle(Cliente reg)
        {
            if (ModelState.IsValid) //validacion de las notaciones
            {
                ViewBag.mens = _inyector.actualizar(reg);
            }
           
            ViewBag.cliente = _inyector.listaCli();
            return View(reg);
        }

        public ActionResult EliminarCli(int id)
        {
            Cliente reg = _inyector.buscar(id);
           
            ViewBag.cliente = _inyector.listaCli();
            return View(reg);
        }

        // POST: Empleado/Delete/5
        [HttpPost]
        public ActionResult EliminarCli(int id, int ori = 0)
        {

            if (ModelState.IsValid) //validacion de las notaciones
            {
                ViewBag.mens = _inyector.eliminar(id);
            }
            
            ViewBag.cliente = _inyector.listaCli();
            return View();
        }




    }
}
