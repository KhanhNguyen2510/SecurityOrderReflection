using Microsoft.EntityFrameworkCore;
using SOR.Application.Catalogs.Historys;
using SOR.Data.EFs;
using SOR.Data.Enum;
using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Historys;
using SOR.ViewModel.Catalogs.NewLables;
using SOR.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.NewsLabels
{
    public class NewsLabelSevice : INewsLabelSevice
    {
        private readonly SORDbContext _context;
        private SystemBase<string> checkValue = new SystemBase<string>();
        private readonly IHistorySevice _historySevice;


        public NewsLabelSevice(SORDbContext context, IHistorySevice historySevice )
        {
            _context = context;
            _historySevice = historySevice;
        }

        public async Task<bool> NameNewsLableExistence(string name)
        {
            var checkName = await _context.NewsLabels.FirstOrDefaultAsync(x => x.Name == name && x.IsDelete == true);
            if (checkName == null) return false;
            return true;
        }

        public async Task<Data.Entitis.NewsLabel> FindIdExistence(string Id)
        {
            var findId = await _context.NewsLabels.FirstOrDefaultAsync(x => x.Id == Id && x.IsDelete == true);
            return findId;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Tạo thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> CreateToNewsLable(GetCreateToNewsLableRequest request)
        {
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            bool cName = checkValue.CheckNullValue(request.name);
            if (!cName) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            request.name = request.name.Trim();

            bool checkName = await NameNewsLableExistence(request.name);

            if (checkName) return new ApiResponse(MessageBase.NAME_EXISTENCE, 400);

            bool cId = checkValue.CheckNullValue(request.id);
            if (!cId) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            request.id = request.id.Trim();

            var findId = await FindIdExistence(request.id);

            if (findId != null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            var gHistory = new Data.Entitis.NewsLabel()
            {
                Id = request.id,
                Name = request.name,
                CreateUser = request.userId,
                UpdateUser = request.userId
            };

            await _context.NewsLabels.AddAsync(gHistory);
            await _context.SaveChangesAsync();

            #region Add History
            var dhistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Thêm thông tin nhản bài báo cáo",
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

        public async Task<ApiResponse> UpdateToNewsLable(string Id, GetUpdateToNewsLableRequest request)
        {

            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var findId = await FindIdExistence(Id);

            if (findId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            bool cName = checkValue.CheckNullValue(request.name);

            if (cName)
            {
                request.name = request.name.Trim();
                bool checkName = await NameNewsLableExistence(request.name);
                if (checkName) return new ApiResponse(MessageBase.NAME_EXISTENCE, 400);

                findId.Name = request.name;
                findId.UpdateUser = request.userId;
                findId.UpdateDate = DateTime.Now;

                await _context.SaveChangesAsync();

                #region Add History
                var dhistory = new GetCreateToHistoryRequest()
                {
                    HistoryOperation = $"Cập nhật thông tin nhản bài báo cáo với ID là  { Id}",
                    IsOperation = IsOperation.Update,
                    userId = request.userId
                };

                await _historySevice.CreateToHistory(dhistory);
                #endregion
            }

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Xóa thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> DeleteToNewsLable(string Id, CreateUserRequest request)
        {

            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var finfId = await FindIdExistence(Id);

            if (finfId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            finfId.UpdateUser = request.userId;
            finfId.IsDelete = false;

            finfId.TimeDelete = finfId.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            #region Add History
            var dhistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Xóa thông tin nhản bài báo cáo với ID là { Id} ",
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

        public async Task<GetNewsLableViewModel> GetNewsLableById(string Id)
        {
            var query = await _context.NewsLabels.Where(x => x.IsDelete == true && x.Id == Id).ToListAsync();
            if (!query.Any())
                return null;

            return query.Select(x => new GetNewsLableViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                UpdateUser = x.UpdateUser,
                CreateDate = x.CreateDate,
                CreateUser = x.CreateUser,
                UpdateDate = x.UpdateDate
            }).FirstOrDefault();
        }

        public async Task<IEnumerable<GetNewsLableViewModel>> GetListToNewsLable(GetMangagerToNewsLableRequest request)
        {
            var query = await _context.NewsLabels.Where(x => x.IsDelete == true).ToListAsync();

            if (!query.Any()) return null;

            bool cKeyWord = checkValue.CheckNullValue(request.keyWord);

            if (cKeyWord)
            {
                query = query.Where(x => x.Id.ToString().Contains(request.keyWord)
                || x.Name.Contains(request.keyWord)).ToList();
            }

            return query.Select(x => new GetNewsLableViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                UpdateUser = x.UpdateUser,
                CreateDate = x.CreateDate,
                CreateUser = x.CreateUser,
                UpdateDate = x.UpdateDate
            });
        }

        public async Task<PagedResult<GetNewsLableViewModel>> GetListPagingToNewsLable(GetMangagerNewsLableRequest request)
        {
            var query = await _context.NewsLabels.Where(x => x.IsDelete == true).ToListAsync();

            if (!query.Any()) return null;

            bool cKeyWord = checkValue.CheckNullValue(request.keyWord);

            if (cKeyWord)
            {
                query = query.Where(x => x.Id.ToString().Contains(request.keyWord)
                || x.Name.Contains(request.keyWord)).ToList();
            }

            int totalRow = query.Count();

            var data = query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new GetNewsLableViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UpdateUser = x.UpdateUser,
                    CreateDate = x.CreateDate,
                    CreateUser = x.CreateUser,
                    UpdateDate = x.UpdateDate
                }).ToList();
            return new PagedResult<GetNewsLableViewModel>()
            {
                Items = data,
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };
        }

    }
}
