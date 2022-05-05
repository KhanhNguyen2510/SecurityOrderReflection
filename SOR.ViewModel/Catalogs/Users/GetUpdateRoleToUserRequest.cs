using SOR.Data.Enum;

namespace SOR.ViewModel.Catalogs.Users
{
    public class GetUpdateRoleToUserRequest : CreateUserRequest
    {
        public string userUpdate { get; set; }
        public IsAdmin IsAdmin { get; set; }
    }
}
