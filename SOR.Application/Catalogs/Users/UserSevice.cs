using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SOR.Data.EFs;
using SOR.Data.Enum;
using SOR.Data.SystemBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Users
{
    public class UserSevice : IUserSevice
    {
        private readonly SORDbContext _context;
        private readonly IConfiguration _config;

        public UserSevice(SORDbContext context , IConfiguration configuration)
        {
            _context = context;
            _config = configuration;    
        }

        /// <summary>
        /// Check
        /// </summary>
        /// <param name="Kiểm tra thông tin"></param>
        /// <returns></returns>
        /// 

        public class GetLoginRequest
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn cần nhập tên tài khoản")]
            public string userName { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn cần phải nhập mật khẩu")]
            public string passWord { get; set; }
        }

        public async Task<bool> CheckLoginExistence(GetLoginRequest request)
        {
            var cUser = await _context.Users.FirstOrDefaultAsync(x => x.IsDelete == true &&  x.UserName == request.userName && x.PassWord == ShareContantsSytem.MD5(request.passWord));
            if (cUser == null) return false;
            return true;
        }

        public async Task<bool> Login(GetLoginRequest request)
        {
            bool cUser = await CheckLoginExistence(request);
            if (cUser)
                return true;
            return false;
        }

        public async Task<string> LoginInWed(GetLoginRequest request)
        {
            var user = await Login(request);
            if (user)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, request.userName));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(2000),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return null;
        }

       

        public async Task<string> GetKeyByLocationReport(string location)
        {
            var fCode = await _context.Codes.Where(x => x.IsDelete == true).Select(x => new { x.Name, x.Key }).ToListAsync();

            if (!fCode.Any()) return MessageBase.KEY_UN_EXISTENCE;

            int cnCode = fCode.Count;

            for (int item = 0; item < cnCode; item++)
            {
                var cLocationReport = location.ToLower().Contains(fCode[item].Name.ToLower());
                if (cLocationReport)
                {
                    return fCode[item].Key;
                }
            }
            return MessageBase.KEY_UN_EXISTENCE;
        }

        public async Task<bool> UserNameExistence(string userName)
        {
            var cUserName = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (cUserName == null) return false;
            return true;
        }
        public class GetCreateToUserRequest
        {
            [Required(AllowEmptyStrings = false, ErrorMessage = "Cần nhập tên tài khoản đăng nhập")]
            public string UserName { get; set; }
            [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]*", ErrorMessage = "Email không đúng định dạng")]
            public string Email { get; set; }
            public IsGender? Gender { get; set; } = IsGender.Orther;
            public string FullName { get; set; }
            public string Identification { get; set; }
            public int? AgenciesId { get; set; }
            [Display(Name = "Số điện thoại")]
            [MinLength(9, ErrorMessage = "Số điện thoại không hợp lệ")]
            [MaxLength(15, ErrorMessage = "Số điện thoại không hợp lệ")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn cần nhập số diện thoại liên hệ")]
            public string NumberPhone { get; set; }
            [Display(Name = "Mật khẩu")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Cần nhập mật khẩu")]
            public string Password { get; set; }
            [Display(Name = "Nhập lại mật khẩu")]
            [Compare(otherProperty: "Password", ErrorMessage = "Mật khẩu không khớp")]
            public string ConfirmPassword { get; set; }
        }

        //public async Task<ApiResponse> CreateToUser(GetCreateToUserRequest request)
        //{
        //    bool cUserName = await UserNameExistence(request.UserName);
        //    if(cUserName)
        //        return new ApiResponse(MessageBase.NON_EXISTENCE, 400);
        //    request.UserName = request.UserName.Trim();

        //    var data = new Data.Entitis.User()
        //    {
        //        UserName = request.UserName,
        //        Email = request.Email,
        //        IsAdmin = IsAdmin.People,
        //        Gender = request.Gender != null ? request.Gender : IsGender.Orther,
        //        FullName = request.FullName,
        //        Identification = request.Identification,
        //        AgenciesId = request.AgenciesId,
        //        NumberPhone = request.NumberPhone,
        //        PassWord = request.Password,
                
        //    }


        //}



    }
}
