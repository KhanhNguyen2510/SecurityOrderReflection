using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace SOR.Application.Catalogs.Reports.Upload
{
    public interface IFileSevice
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<string> UploadImage(List<IFormFile> files);
    }
}
