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
                SubDirectories = _direcotryAnalyzer.GetSubDirectories(rootDir),
                Files = _direcotryAnalyzer.GetFiles(rootDir)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Serve(string dir, string file) {
            if (!string.IsNullOrEmpty(file)) {
                var fileFullName = Path.Combine(_config.GetSection("RootDir").Value, file);
                if (!System.IO.File.Exists(fileFullName)) {
                    return NotFound();
                }

                var theFile = await System.IO.File.ReadAllBytesAsync(Path.Combine(_config.GetSection("RootDir").Value, file));
                return File(theFile, "application/pdf");
            }

            return NotFound();
        }
    }
}
