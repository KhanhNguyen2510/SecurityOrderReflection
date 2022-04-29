using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace SOR.Application.Catalogs.Reports.Upload
{
    public class FileSevice :  IFileSevice
    {
        private IHostingEnvironment _hostingEnvironment;

        public FileSevice(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public List<string> UploadImage(List<IFormFile> files)
        {
            var listUrlImages = new List<string>();

            int cnFiles = files.Count;

            for (int item = 0; item < cnFiles; item++)
            {
                if (!Directory.Exists(_hostingEnvironment.WebRootPath + "\\Uploads\\"))
                {
                    Directory.CreateDirectory(_hostingEnvironment.WebRootPath + "\\Uploads\\");
                }

                using (var stream = File.Create(_hostingEnvironment.WebRootPath + "\\Uploads\\" + files[item].FileName))
                {
                    files[item].CopyTo(stream);
                    stream.Flush();
                    listUrlImages.Add($"/Uploads/{files[item].FileName}");
                };
            }
            return listUrlImages;
        }
    }
}
