using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SOR.Application.Catalogs.Reports.Upload
{
    public interface IFileSevice
    {
        List<string> UploadImage(List<IFormFile> files);
    }
}
