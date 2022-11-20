using Marval.HelperInterface;
using Microsoft.Extensions.FileProviders;

namespace Marval.HelperSevices
{
    public class FileHelper : IFileHelper
    {
        private readonly IHostEnvironment _hostingEnvironment;

        public FileHelper(IHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<string> UploadCSV(IFormFile file, string userName)
        {
            var csvUrl = "";
            if (file != null)
            {
                FileInfo fi = new FileInfo(file.FileName);
                string fileName = userName + Guid.NewGuid().ToString() + fi.Extension;

                var webPath = _hostingEnvironment.ContentRootPath;
                var path = Path.Combine("", webPath + @"\wwwroot\files\" + fileName);
                csvUrl = path;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return csvUrl;
        }
    }
}
