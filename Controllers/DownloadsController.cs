using Microsoft.AspNetCore.Mvc;
using Local_C.Models;
using Local_C.Core;

namespace Local_C.Controllers {
    public class DownloadsController : Controller {
        private readonly IConfiguration _config;
        private readonly IDirectoryAnalyzer _direcotryAnalyzer;

        public DownloadsController(IConfiguration config, IDirectoryAnalyzer direcotryAnalyzer) {
            _config = config;   
            _direcotryAnalyzer = direcotryAnalyzer;
        }


        public IActionResult Index() {
            var rootDir = _config.GetSection("RootDir").Value;
            var viewModel = new DownloadDirectoryModel() {
                RootDir = rootDir,
                SubDirectories = _direcotryAnalyzer.GetSubDirectories(rootDir)
            };

            return View(viewModel);
        }
    }
}
