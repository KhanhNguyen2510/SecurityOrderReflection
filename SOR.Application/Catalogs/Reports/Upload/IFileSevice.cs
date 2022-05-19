using Microsoft.AspNetCore.Http;
using SOR.ViewModel.Catalogs.Reports.Proof;
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
        List<FileSystemViewModel> UploadImage(List<IFormFile> files);
    }
}
