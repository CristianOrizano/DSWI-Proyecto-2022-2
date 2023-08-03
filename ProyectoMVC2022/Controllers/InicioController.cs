using Microsoft.AspNetCore.Mvc;

namespace ProyectoMVC2022.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult inicio()
        {
            
            ViewBag.mensaje = HttpContext.Session.GetString("Bien");
            return View();
        }



    }
}
