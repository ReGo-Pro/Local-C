using Microsoft.AspNetCore.Mvc;
using Local_C.Models;
using Local_C.Core;

namespace Local_C.Controllers {
    /* A controller should only act as a gateway, connecting different pieces of the application together, 
       Especially it should not contain the following:
       - Business logic
       - Data access logic
       - Composing different parts of the ViewModel (we may use Adapter pattern or a tool like AutoMapper)
    */
    public class DownloadsController : Controller {
        private readonly IConfiguration _config;
        private DirectoryInfo _dirInfo;

        public DownloadsController(IConfiguration config) {
            _config = config;
        }

        // This view can be reused for root directory and all subdirectories because they share a common interface and hence a similar ViewModel
        public IActionResult Index() {
            // Define a modelbuilder here and 
            // Duplication not good
            // Violates single responsibility (this should belong to the business layer, not controller)
            _dirInfo = new DirectoryInfo(RootDir);
            var viewModel = new DirectoryModel() {
                Name = "Root directory",
                SubDirectories = _dirInfo.GetDirectories()
                    .Select(d => new DirectoryModel() {
                        Name = d.Name,
                        Path = d.FullName.Replace($"{RootDir}\\", "")
                }).ToList(),
                Files = _dirInfo.GetFiles().Select(f => new FileModel() {
                    Path = f.FullName,
                    Name = f.Name,
                    Extension = f.Extension
                })
            };

            return View(viewModel);
        }

        // it's a better approach to get rid of ServableType and just have two separate action methods
        // This will further simplify the code and the input parameters could be better named that just saying "fullName"
        public async Task<IActionResult> Serve(string fullName, ServableType type) {
            switch (type) {
                case ServableType.Directory:
                    return ServeDirectory(fullName);
                case ServableType.File:
                    return await ServeFile(fullName);
                default:
                    throw new ArgumentException("Invalid servable type");
                    // The user should be redirected to an error page or just an alert should appear
            }
        }

        private async Task<IActionResult> ServeFile(string fileName) {
            // TODO: Better input validation
            if (!string.IsNullOrEmpty(fileName)) {
                var fullName = Path.Combine(RootDir, fileName);
                if (!System.IO.File.Exists(fullName)) {
                    return NotFound();
                }

                var fileBytes = await System.IO.File.ReadAllBytesAsync(fullName);
                var mimeType = MimeTypes.GetMimeType(fullName);
                return File(fileBytes, mimeType);
            }

            return NotFound();
        }

        private IActionResult ServeDirectory(string path) {
            // TODO: Better input validation
            // Bad: duplication
            // Logic: should be moved out of the controller to business layer
            _dirInfo = new DirectoryInfo(Path.Combine(RootDir, path));
            var viewModel = new DirectoryModel() {
                Name = Path.GetFileName(path),
                SubDirectories = _dirInfo.GetDirectories()
                    .Select(d => new DirectoryModel() {
                        Name = d.Name,
                        Path = d.FullName.Replace($"{RootDir}\\", "")
                    }).ToList(),
                Files = _dirInfo.GetFiles().Select(f => new FileModel() {
                    Path = f.FullName,
                    Name = f.Name,
                    Extension = f.Extension
                })
            };

            return View("Index", viewModel);
        }

        private string RootDir => _config.GetSection("RootDir").Value;
    }
}