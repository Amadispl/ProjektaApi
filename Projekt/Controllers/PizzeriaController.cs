using Microsoft.AspNetCore.Mvc;

namespace Projekt.Controllers
{
    public class PizzeriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
