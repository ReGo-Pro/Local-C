using Microsoft.AspNetCore.Mvc;

namespace Local_C.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() {
            return View();
        }

        public IActionResult Error() {
            return View();
        }

        public IActionResult Contact() {
            return View();
        }
    }
}
