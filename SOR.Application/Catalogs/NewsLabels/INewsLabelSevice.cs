using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.NewLables;
using SOR.ViewModel.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.NewsLabels
{
    public interface INewsLabelSevice
    {
        Task<ApiResponse> CreateToNewsLable(GetCreateToNewsLableRequest request);
        Task<ApiResponse> UpdateToNewsLable(string Id, GetUpdateToNewsLableRequest request);
        Task<ApiResponse> DeleteToNewsLable(string Id, CreateUserRequest request);
        Task<GetNewsLableViewModel> GetNewsLableById(string Id);
        Task<IEnumerable<GetNewsLableViewModel>> GetListToNewsLable(GetMangagerToNewsLableRequest request);
        Task<PagedResult<GetNewsLableViewModel>> GetListPagingToNewsLable(GetMangagerNewsLableRequest request);
    }
}
