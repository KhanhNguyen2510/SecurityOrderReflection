using SOR.Data.Enum;

namespace SOR.ViewModel.Catalogs.Users
{
    public class GetMangagerToUserRequest
    {
        public string keyWord { get; set; }
        public IsAdmin? isAdmin { get; set; }
        public IsGender? isGender { get; set; }
        public string agenciesId { get; set; }
    }
}
