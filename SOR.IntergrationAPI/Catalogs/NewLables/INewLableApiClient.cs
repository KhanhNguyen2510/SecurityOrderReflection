using SOR.ViewModel.Catalogs.NewLables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SOR.IntergrationAPI.Catalogs.NewLables
{
    public interface INewLableApiClient
    {
        Task<IEnumerable<GetNewsLableViewModel>> GetListToNewsLable(GetMangagerToNewsLableRequest request);
    }
}
