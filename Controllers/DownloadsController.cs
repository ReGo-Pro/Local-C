using Microsoft.AspNetCore.Mvc;
using Local_C.Models;

namespace Local_C.Controllers {
    public class DownloadsController : Controller {
        private readonly IConfiguration _config;

        public DownloadsController(IConfiguration config) {
            _config = config;   
        }


        public IActionResult Index() {
            var viewModel = new DownloadDirectoryModel() {
                RootDir = _config.GetSection("RootDir").Value
            };

            return View(viewModel);
        }
    }
}
