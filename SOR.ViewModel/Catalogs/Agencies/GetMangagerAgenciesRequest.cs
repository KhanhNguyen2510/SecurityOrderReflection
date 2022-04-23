using SOR.ViewModel.Common;

namespace SOR.ViewModel.Catalogs.Agencies
{
    public class GetMangagerAgenciesRequest : PagingRequestBase
    {
        public string keyWord { get; set; }
    }
}
