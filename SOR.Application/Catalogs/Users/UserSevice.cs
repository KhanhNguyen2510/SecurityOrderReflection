using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SOR.Application.Catalogs.Historys;
using SOR.Data.EFs;
using SOR.Data.Enum;
using SOR.Data.SystemBase;
using SOR.IntergrationAPI.Catalogs.User;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Historys;
using SOR.ViewModel.Catalogs.Mobile;
using SOR.ViewModel.Catalogs.Users;
using SOR.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Users
{
    public class UserSevice : IUserSevice
    {
        private readonly SORDbContext _context;
        private readonly IConfiguration _config;
        private readonly IHistorySevice _historySevice;
        private SystemBase<string> checkValueTypeString = new SystemBase<string>();

        public UserSevice(SORDbContext context, IConfiguration configuration, IHistorySevice historySevice )
        {
            _context = context;
            _config = configuration;
            _historySevice = historySevice;
        }

        /// <summary>
        /// Check
        /// </summary>
        /// <param name="Kiểm tra thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<bool> UserNameExistence(string userName)
        {
            var cUserName = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.IsDelete == true);
            if (cUserName == null) return false;
            return true;
        }

        public async Task<Data.Entitis.User> CheckUser(string userName)
        {
            var gUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName && x.IsDelete == true);
            if (gUser == null) return null;
            return gUser;
        }

        public async Task<bool> AgenciesExistence(string Id)
        {
            var cAgencies = await _context.Agencies.FirstOrDefaultAsync(x => x.Id == Id);
            if (cAgencies == null) return false;
            return true;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="Các cách đăng nhập"></param>
        /// <returns></returns>
        /// 

        public async Task<Data.Entitis.User> Login(GetLoginRequest request)
        {
            var cUser = await _context.Users.FirstOrDefaultAsync(x => x.IsDelete == true && x.UserName == request.userName && x.PassWord == ShareContantsSytem.MD5(request.passWord));
            if (cUser == null) return null;

            var dHistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Tài Khoản {request.userName} đăng nhập vào lúc {DateTime.Now}",
                IsOperation = IsOperation.Login,
                userId = request.userName
            };
            await _historySevice.CreateToHistory(dHistory);

            return cUser;
        }

        public async Task<string> LoginInWed(GetLoginRequest request)
        {
            var fUser = await Login(request);
            if (fUser != null)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, fUser.UserName));
                claims.Add(new Claim(ClaimTypes.Role, fUser.IsAdmin.ToString()));
                claims.Add(new Claim(ClaimTypes.Actor, fUser.FullName));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(200),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return null;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Tạo tài khoản"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> CreateToUser(GetCreateToUserRequest request)
        {
            #region Check Value
            bool cName = checkValueTypeString.CheckNullValue(request.userName);
            if (!cName) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            //bool cAgenciesExisten = false;

            //bool cAgencies = checkValueTypeString.CheckNullValue(request.agenciesId);
            //if (cAgencies)
            //{
            //    request.agenciesId = request.agenciesId.Trim();

            //    cAgenciesExisten = await AgenciesExistence(request.agenciesId);
            //    if (!cAgenciesExisten) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);
            //}

            request.userName = request.userName.Trim();
            bool cNameExistence = await UserNameExistence(request.userName);
            if (cNameExistence)
                return new ApiResponse(MessageBase.NAME_EXISTENCE, 400);
            #endregion

            var dUser = new Data.Entitis.User()
            {
                UserName = request.userName,
                Email = request.email,
                IsAdmin =/* cAgenciesExisten == true ?*/ IsAdmin.Police /*: IsAdmin.People*/,
                Gender = request.gender != null ? request.gender : IsGender.Orther,
                FullName = request.fullName,
                Identification = request.userName,
                NumberPhone = request.numberPhone,
                PassWord = ShareContantsSytem.MD5(request.password),
                IPCreate = request.iPCreate,
                //AgenciesId = cAgenciesExisten == true ? request.agenciesId: null,
                CreateUser = request.userName,
                UpdateUser = request.userName 
            };

            await _context.Users.AddAsync(dUser);
            await _context.SaveChangesAsync();

            var dHistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Tạo tài khoản {request.userName} đăng nhập vào lúc {DateTime.Now}",
                IsOperation = IsOperation.Create,
                userId = request.userName
            };
            await _historySevice.CreateToHistory(dHistory);

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Cập nhật tài khoản"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> UpdatePassWordToUser(string userName)
        {
            bool cName = checkValueTypeString.CheckNullValue(userName);
            if (!cName) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);
            userName = userName.Trim();
            var fUserName = await CheckUser(userName);
            if (fUserName == null)
                return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            StringBuilder generatePassWord = AutoGenerate.GenerateIdRandom(6);

            fUserName.PassWord = ShareContantsSytem.MD5(generatePassWord.ToString());
            fUserName.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            #region AddHistoy
            var dHistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Cập nhật mật khẩu của tài khoản {userName} vào lúc {DateTime.Now}",
                IsOperation = IsOperation.Update,
                userId = userName
            };
            await _historySevice.CreateToHistory(dHistory);
            #endregion

            return new ApiResponse(MessageBase.SUCCCESS,generatePassWord.ToString());
        }

        public async Task<ApiResponse> UpdateToUser(string userName , GetUpdateToUserRequest request)
        {

            #region CheckValue
            bool cName = checkValueTypeString.CheckNullValue(userName);
            if (!cName) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);
            userName = userName.Trim();
            var cNameExistence = await CheckUser(userName);
            if (cNameExistence == null)
                return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            bool cAgenciesExisten = false;

            bool cAgencies = checkValueTypeString.CheckNullValue(request.AgenciesId);
            if (cAgencies)
            {
                request.AgenciesId = request.AgenciesId.Trim();

                cAgenciesExisten = await AgenciesExistence(request.AgenciesId);
                if (!cAgenciesExisten) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);
            }
            bool cPassword = checkValueTypeString.CheckNullValue(request.PassWord);
            if (cPassword)
            {
                var dLogin = new GetLoginRequest()
                {
                    userName = userName,
                    passWord = request.PassWord
                };
                var cLogin = await Login(dLogin);
                if (cLogin == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

                cNameExistence.PassWord = ShareContantsSytem.MD5(request.NewPassWord);
            }
            #endregion

            cNameExistence.Email = !string.IsNullOrEmpty(request.Email) ? request.Email : cNameExistence.Email;
            cNameExistence.FullName = !string.IsNullOrEmpty(request.FullName) ? request.FullName : cNameExistence.FullName;
            cNameExistence.Identification = !string.IsNullOrEmpty(request.Identification) ? request.Identification : cNameExistence.Identification;
            cNameExistence.AgenciesId = cAgenciesExisten == true ? request.AgenciesId : cNameExistence.AgenciesId;
            cNameExistence.Gender = request.Gender != null ? request.Gender : cNameExistence.Gender;
            cNameExistence.NumberPhone = !string.IsNullOrEmpty(request.NumberPhone) ? request.NumberPhone : cNameExistence.NumberPhone;
            cNameExistence.UpdateDate = DateTime.Now;
            cNameExistence.UpdateUser = userName;


            await _context.SaveChangesAsync();

            #region AddHistoy
            var dHistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Cập nhật tài khoản {userName} đăng nhập vào lúc {DateTime.Now}",
                IsOperation = IsOperation.Update,
                userId = userName
            };
            await _historySevice.CreateToHistory(dHistory);
            #endregion

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        public async Task<ApiResponse> RoleUser(GetUpdateRoleToUserRequest request )
        {
            bool cName = checkValueTypeString.CheckNullValue(request.userUpdate);
            if (!cName) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);
            request.userUpdate = request.userUpdate.Trim();

            var cNameExistence = await CheckUser(request.userUpdate);
            if (cNameExistence == null)
                return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            cNameExistence.IsAdmin = request.IsAdmin;
            cNameExistence.UpdateUser = request.userId;
            cNameExistence.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();


            #region AddHistoy
            var dHistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Cập nhật quyền của {request.userUpdate} đăng nhập vào lúc {DateTime.Now}",
                IsOperation = IsOperation.Update,
                userId = request.userId
            };
            await _historySevice.CreateToHistory(dHistory);
            #endregion

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Xóa tài khoản "></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> DeleteToUse(string userName, CreateUserRequest request)
        {
            bool cUser = checkValueTypeString.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var cUserExistence = await CheckUser(userName);

            if (cUserExistence == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            cUserExistence.UpdateUser = request.userId;
            cUserExistence.IsDelete = false;

            cUserExistence.TimeDelete = DateTime.Now;

            await _context.SaveChangesAsync();

            #region Add History
            var dhistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Xóa tài khoản với tên là {userName} vào ngày {DateTime.Now}",
                IsOperation = IsOperation.Delete,
                userId = request.userId
            };

            await _historySevice.CreateToHistory(dhistory);
            #endregion

            return new ApiResponse(MessageBase.SUCCCESS);

        }

        /// <summary>
        /// List
        /// </summary>
        /// <param name="Hiển thị thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<Data.Entitis.User> GetUserById(string userName)
        {
            var gUser = await _context.Users.Where(x => x.IsDelete == true && x.UserName == userName.Trim()).FirstOrDefaultAsync();

            if (gUser == null) return null;

            return new Data.Entitis.User()
            {
               UserName = gUser.UserName,
               AgenciesId = gUser.AgenciesId,
               Email = gUser.Email,
               EndLogin = gUser.EndLogin,
               FistLogin = gUser.FistLogin,
               FullName = gUser.FullName,
               Gender = gUser.Gender,
               Id = gUser.Id,
               Identification = gUser.Identification,
               IPCreate = gUser.IPCreate,
               IsAdmin = gUser.IsAdmin,
               NumberPhone = gUser.NumberPhone,
               PassWord = gUser.PassWord
            };
        }

        public async Task<IEnumerable<Data.Entitis.User>> GetListToUser(GetMangagerToUserRequest request)
        {
            var gUsers = await _context.Users.Where(x => x.IsDelete == true).ToListAsync();

            if (!gUsers.Any()) return null;

            bool cKeyWord = checkValueTypeString.CheckNullValue(request.keyWord);

            if (cKeyWord)
            {
                gUsers = gUsers.Where(x => x.Id.ToString().Contains(request.keyWord) 
                || x.Email.Contains(request.keyWord) || x.FullName.Contains(request.keyWord) ||
                x.Identification.Contains(request.keyWord)|| x.IPCreate.Contains(request.keyWord) 
                || x.NumberPhone.Contains(request.keyWord) || x.UserName.Contains(request.keyWord))
                .ToList();
            }
            if (request.isAdmin != null)
            {
                gUsers = gUsers.Where(x => x.IsAdmin == request.isAdmin).ToList();
            }
            if (request.isGender != null)
            {
                gUsers = gUsers.Where(x => x.Gender == request.isGender).ToList();                
            }
            if (!string.IsNullOrEmpty(request.agenciesId))
            {
                gUsers = gUsers.Where(x => x.AgenciesId == request.agenciesId).ToList();
            }

            return gUsers.Select(x => new Data.Entitis.User()
            {
                UserName = x.UserName,
                AgenciesId = x.AgenciesId,
                Email = x.Email,
                EndLogin = x.EndLogin,
                FistLogin = x.FistLogin,
                FullName = x.FullName,
                Gender = x.Gender,
                Id = x.Id,
                Identification = x.Identification,
                IPCreate = x.IPCreate,
                IsAdmin = x.IsAdmin,
                NumberPhone = x.NumberPhone,
                PassWord = x.PassWord
            });
        }

        public async Task<PagedResult<Data.Entitis.User>> GetListPagingToUser(GetMangagerUserRequest request)
        {
            var gUsers = await _context.Users.Where(x => x.IsDelete == true).ToListAsync();

            if (!gUsers.Any()) return null;

            bool cKeyWord = checkValueTypeString.CheckNullValue(request.keyWord);

            if (cKeyWord)
            {
                gUsers = gUsers.Where(x => x.Id.ToString().Contains(request.keyWord)
                || x.Email.Contains(request.keyWord) || x.FullName.Contains(request.keyWord) ||
                x.Identification.Contains(request.keyWord) || x.IPCreate.Contains(request.keyWord)
                || x.NumberPhone.Contains(request.keyWord) || x.UserName.Contains(request.keyWord))
                .ToList();
            }
            if (request.isAdmin != null)
            {
                gUsers = gUsers.Where(x => x.IsAdmin == request.isAdmin).ToList();
            }
            if (request.isGender != null)
            {
                gUsers = gUsers.Where(x => x.Gender == request.isGender).ToList();
            }
            if (!string.IsNullOrEmpty(request.agenciesId))
            {
                gUsers = gUsers.Where(x => x.AgenciesId == request.agenciesId).ToList();
            }

            int totalRow = gUsers.Count();

            var data = gUsers.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new Data.Entitis.User()
                {
                    UserName = x.UserName,
                    AgenciesId = x.AgenciesId,
                    Email = x.Email,
                    EndLogin = x.EndLogin,
                    FistLogin = x.FistLogin,
                    FullName = x.FullName,
                    Gender = x.Gender,
                    Id = x.Id,
                    Identification = x.Identification,
                    IPCreate = x.IPCreate,
                    IsAdmin = x.IsAdmin,
                    NumberPhone = x.NumberPhone,
                    PassWord = x.PassWord
                }).ToList();
            return new PagedResult<Data.Entitis.User>()
            {
                Items = data,
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };
        }
    }
}
