using SOR.Data.Enum;
using SOR.ViewModel.Common;

namespace SOR.ViewModel.Catalogs.Users
{
    public class GetMangagerUserRequest : PagingRequestBase
    {
        public string keyWord { get; set; }
        public IsAdmin? isAdmin { get; set; }
        public IsGender? isGender { get; set; }
        public string agenciesId { get; set; }
    }
}
