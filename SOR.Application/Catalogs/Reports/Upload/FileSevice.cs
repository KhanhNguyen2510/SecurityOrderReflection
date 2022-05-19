using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SOR.Data.Enum;
using SOR.ViewModel.Catalogs.Reports.Proof;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Reports.Upload
{
    public class FileSevice :  IFileSevice
    {
        private readonly  IHostingEnvironment _hostingEnvironment;

        public FileSevice(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public List<FileSystemViewModel> UploadImage(List<IFormFile> files)
        {
            var gFiles = new List<FileSystemViewModel>();

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
                    var cFile = CheckFile(files[item].FileName);
                    gFiles.Add(new FileSystemViewModel() { url = $"/Uploads/{files[item].FileName}", type = cFile} );
                };
            } return gFiles;
        }

        public IsFile CheckFile(string type)
        {

           var Images = new List<string>() { ".apng", ".avif", ".gif", ".jpg", ".jpeg", ".jfif", ".pjpeg", ".pjp", ".png", ".svg", ".webp", ".bmp", ".ico", ".cur", ".tif", ".tiff" };
            var Videos = new List<string>() { ".mp4", ".mov", ".wmv", ".flv", "mp3" };

            IsFile cfile = IsFile.None;

            Parallel.ForEach(Images, file =>
            {
                if (type.Contains(file))
                {
                    cfile = IsFile.Image;
                    return;
                }
            });
            Parallel.ForEach(Videos, file =>
            {
                if (type.Contains(file))
                {
                    cfile = IsFile.Video;
                    return;
                }
            });

            return cfile;
        }
    }
}
