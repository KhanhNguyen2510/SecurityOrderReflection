using SOR.Application.Catalogs.Users;
using SOR.Data.EFs;
using SOR.Data.SystemBase;
using SOR.IntergrationAPI.Catalogs.User;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Mobile;
using System;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Mobiles
{
    public class MobileSevice : IMobileSevice
    {
        private readonly SORDbContext _context;
        private readonly IUserSevice _userSevice;
        private readonly IUserApiClient _userApiClient;

        public MobileSevice(SORDbContext context , IUserSevice userSevice, IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
            _userSevice = userSevice;
            _context = context;
        }

        public async Task<ApiResponse> RemoveTokenInMobile(CreateUserRequest request)
        {
            var fUser = await _userSevice.CheckUser(request.userId);
            if (fUser == null) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            fUser.Token = null;
            fUser.UpdateUser = request.userId;
            fUser.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }
        public async Task<ApiResponse> AddTokenInMobile(GetAddToKenRequest request)
        {
            var fUser = await _userSevice.CheckUser(request.userId);
            if (fUser == null) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            fUser.Token = request.Token;
            fUser.UpdateUser = request.userId;
            fUser.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        public async Task<ApiResponse> ShowNotificationInMobile(string userName,Notification request)
        {
            var gUser = await _userSevice.CheckUser(userName);
            if (gUser == null)
                return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            var dNotification = new GetNotificationRequest()
            {
                registration_ids = new string[] { $"{gUser.Token}" },
                notification = new Notification()
                {
                    body = request.body,
                    tille = request.tille
                }
            };

            var gNotification = await _userApiClient.NotificationInMobile(dNotification);
            if(gNotification == null)
                return new ApiResponse(MessageBase.NO_CONECTION, 400);

            return new ApiResponse(MessageBase.SUCCCESS);
        }


    }
}
