using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SOR.Data.EFs;
using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Agencies;
using SOR.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;



namespace SOR.Application.Catalogs.Agencies
{
    public class AgenciesSevice : IAgenciesSevice
    {
        private readonly SORDbContext _context;
        private SystemBase<string> checkValue = new SystemBase<string>();

        private IHttpContextAccessor _accessor;

        public AgenciesSevice(SORDbContext context , IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
        }

        public IEnumerable<string> Get()
        {
            var ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            return new string[] { ip, "value2" };
        }




























        /// <summary>
        /// Check
        /// </summary>
        /// <param name="Kiểm tra thông tin"></param>
        /// <returns></returns>

        public async Task<bool> NameAgenciesExistence(string name)
        {
            var checkName = await _context.Agencies.FirstOrDefaultAsync(x => x.Name == name && x.IsDelete == true);
            if (checkName == null) return false;
            return true;
        }

        public async Task<Data.Entitis.Agencies> FindIdExistence(int Id)
        {
            var findId = await _context.Agencies.FirstOrDefaultAsync(x => x.Id == Id && x.IsDelete == true);
            return findId;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Tạo thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> CreateToAgencies(GetCreateToAgenciesRequest request)
        {
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            bool cName = checkValue.CheckNullValue(request.name);
            if (!cName) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            request.name = request.name.Trim();

            bool checkName = await NameAgenciesExistence(request.name);

            if (checkName) return new ApiResponse(MessageBase.NAME_EXISTENCE, 400);

            var data = new Data.Entitis.Agencies()
            {
                Name = request.name,
                NumberPhone = request.numberPhone,
                Office = request.office,
                CreateUser = request.userId,
                UpdateUser = request.userId
            };

            await _context.Agencies.AddAsync(data);
            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Cập nhật thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> UpdateToAgencies(int Id, GetUpdateToAgenciesRequest request)
        {

            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var findId = await FindIdExistence(Id);

            if (findId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            bool cName = checkValue.CheckNullValue(request.name);

            if (cName)
            {
                request.name = request.name.Trim();
                bool checkName = await NameAgenciesExistence(request.name);
                if (checkName) return new ApiResponse(MessageBase.NAME_EXISTENCE, 400);
                findId.Name = request.name;
            }

            findId.Office = !string.IsNullOrEmpty(request.office) ? request.office : findId.Office;
            findId.NumberPhone = !string.IsNullOrEmpty(request.numberPhone) ? request.numberPhone : findId.NumberPhone;
            findId.UpdateUser = request.userId;
            findId.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Xóa thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> DeleteToAgencies(int Id, CreateUserRequest request)
        {

            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var finfId = await FindIdExistence(Id);

            if (finfId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            finfId.UpdateUser = request.userId;
            finfId.IsDelete = false;

            finfId.TimeDelete = finfId.UpdateDate =  DateTime.Now;

            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        /// <summary>
        /// List
        /// </summary>
        /// <param name="Hiển thị thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<GetAgenciesViewModel> GetAgenciesById(int Id)
        {
            var query = await _context.Agencies.Where(x => x.IsDelete == true && x.Id == Id).ToListAsync();
            if (!query.Any())
                return null;

            return query.Select(x => new GetAgenciesViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                UpdateUser = x.UpdateUser,
                CreateDate = x.CreateDate,
                CreateUser = x.CreateUser,
                NumberPhone = x.NumberPhone,
                Office = x.Office,
            }).FirstOrDefault();
        }

        public async Task<IEnumerable<GetAgenciesViewModel>> GetListToAgencies(GetMangagerToAgenciesRequest request)
        {
            var query = await _context.Agencies.Where(x => x.IsDelete == true).ToListAsync();

            if (!query.Any()) return null;

            bool cKeyWord = checkValue.CheckNullValue(request.keyWord);

            if (cKeyWord)
            {
                query = query.Where(x => x.Id.ToString().Contains(request.keyWord)
                || x.Name.Contains(request.keyWord) || x.Office.Contains(request.keyWord)
                || x.NumberPhone.Contains(request.keyWord)).ToList();
            }

            return query.Select(x => new GetAgenciesViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                UpdateUser = x.UpdateUser,
                CreateDate = x.CreateDate,
                CreateUser = x.CreateUser,
                NumberPhone = x.NumberPhone,
                Office = x.Office,
            });
        }

        public async Task<PagedResult<GetAgenciesViewModel>> GetListPagingToAgencies(GetMangagerAgenciesRequest request)
        {
            var query = await _context.Agencies.Where(x => x.IsDelete == true).ToListAsync();

            if (!query.Any()) return null;

            bool cKeyWord = checkValue.CheckNullValue(request.keyWord);

            if (cKeyWord)
            {
                query = query.Where(x => x.Id.ToString().Contains(request.keyWord)
                || x.Name.Contains(request.keyWord) || x.Office.Contains(request.keyWord)
                || x.NumberPhone.Contains(request.keyWord)).ToList();
            }

            int totalRow =  query.Count();

            var data =  query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new GetAgenciesViewModel()
                {
                  NumberPhone = x.NumberPhone,
                  Office = x.Office,    
                  Name = x.Name,    
                  CreateDate = x.CreateDate,
                  CreateUser = x.CreateUser,
                  Id = x.Id,
                  UpdateDate = x.UpdateDate,
                  UpdateUser = x.UpdateUser
                }).ToList();
            return new PagedResult<GetAgenciesViewModel>()
            {
                Items = data,
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };
        }

    }
}
