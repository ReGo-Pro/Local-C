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
            var viewModel = new DownloadDirectoryModel() {
                RootDir = RootDir,
                SubDirectories = _direcotryAnalyzer.GetSubDirectories(RootDir),
                Files = _direcotryAnalyzer.GetFiles(RootDir)
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Serve(string dir, string file) {
            if (!string.IsNullOrEmpty(file)) {
                var fileFullName = Path.Combine(RootDir, file);
                if (!System.IO.File.Exists(fileFullName)) {
                    return NotFound();
                }

                var fileBytes = await System.IO.File.ReadAllBytesAsync(fileFullName);
                var mimeType = MimeTypes.GetMimeType(fileFullName);
                return File(fileBytes, mimeType);
            }

            return NotFound();
        }

        private string RootDir => _config.GetSection("RootDir").Value;
    }
}
