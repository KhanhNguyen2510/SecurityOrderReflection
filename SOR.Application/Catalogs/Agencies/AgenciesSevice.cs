using Microsoft.EntityFrameworkCore;
using SOR.Application.Catalogs.Historys;
using SOR.Data.EFs;
using SOR.Data.Enum;
using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Agencies;
using SOR.ViewModel.Catalogs.Historys;
using SOR.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace SOR.Application.Catalogs.Agencies
{
    public class AgenciesSevice : IAgenciesSevice
    {
        private readonly SORDbContext _context;
        private SystemBase<string> checkValue = new SystemBase<string>();

        private readonly IHistorySevice _historySevice;

        public AgenciesSevice(SORDbContext context , IHistorySevice historySevice)
        {
            _historySevice = historySevice;
            _context = context;
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

        public async Task<Data.Entitis.TA_Agencies> FindIdExistence(string Id)
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
            #region CheckValue
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            bool cId = checkValue.CheckNullValue(request.id);
            if (!cId) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            request.id = request.id.Trim();
            var findId = await FindIdExistence(request.id);
            if (findId != null) return new ApiResponse(MessageBase.NAME_EXISTENCE, 400);

            bool cName = checkValue.CheckNullValue(request.name);
            if (!cName) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            request.name = request.name.Trim();

            bool checkName = await NameAgenciesExistence(request.name);

            if (checkName) return new ApiResponse(MessageBase.NAME_EXISTENCE, 400);
            #endregion

            var dAgencies = new Data.Entitis.TA_Agencies()
            {
                Id = request.id,
                Name = request.name,
                NumberPhone = request.numberPhone,
                Office = request.office,
                CreateUser = request.userId,
                UpdateUser = request.userId
            };

            await _context.Agencies.AddAsync(dAgencies);
            await _context.SaveChangesAsync();

            #region Add History
            var dhistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Thêm thông tin cơ quan",
                IsOperation = IsOperation.Create,
                userId = request.userId
            };

            await _historySevice.CreateToHistory(dhistory);
            #endregion

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Cập nhật thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> UpdateToAgencies(string Id, GetUpdateToAgenciesRequest request)
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

            #region Add History
            var dhistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Cập nhật thông tin cơ quan với ID {Id}",
                IsOperation = IsOperation.Update,
                userId = request.userId
            };

            await _historySevice.CreateToHistory(dhistory);
            #endregion

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Xóa thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> DeleteToAgencies(string Id, CreateUserRequest request)
        {

            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var finfId = await FindIdExistence(Id);

            if (finfId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            finfId.UpdateUser = request.userId;
            finfId.IsDelete = false;

            finfId.TimeDelete = DateTime.Now;

            await _context.SaveChangesAsync();

            #region Add History
            var dhistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Xóa thông tin cơ quan với ID {Id}",
                IsOperation = IsOperation.Create,
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

        public async Task<GetAgenciesViewModel> GetAgenciesById(string Id)
        {
            var gAgencies = await _context.Agencies.Where(x => x.IsDelete == true && x.Id == Id).FirstOrDefaultAsync();

            if (gAgencies == null) return null;

            return  new GetAgenciesViewModel()
            {
                Id = gAgencies.Id,
                Name = gAgencies.Name,
                UpdateUser = gAgencies.UpdateUser,
                CreateDate = gAgencies.CreateDate,
                CreateUser = gAgencies.CreateUser,
                NumberPhone = gAgencies.NumberPhone,
                Office = gAgencies.Office,
            };
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
