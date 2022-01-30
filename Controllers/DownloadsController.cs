using Microsoft.AspNetCore.Mvc;

namespace Local_C.Controllers {
    public class DownloadsController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
