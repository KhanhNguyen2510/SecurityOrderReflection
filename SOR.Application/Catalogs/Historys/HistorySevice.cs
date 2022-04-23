using Microsoft.EntityFrameworkCore;
using SOR.Data.EFs;
using SOR.Data.Enum;
using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Historys;
using SOR.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Historys
{
    public class HistorySevice : IHistorySevice
    {
        private readonly SORDbContext _context;
        private SystemBase<string> checkValue = new SystemBase<string>();

        public HistorySevice(SORDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Check
        /// </summary>
        /// <param name="Kiểm tra thông tin"></param>
        /// <returns></returns>

        public async Task<Data.Entitis.History> FindIdExistence(int Id)
        {
            var findId = await _context.Histories.FirstOrDefaultAsync(x => x.Id == Id && x.IsDelete == true);
            return findId;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Tạo thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> CreateToHistory(GetCreateToHistoryRequest request)
        {
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var data = new Data.Entitis.History()
            {
                HistoryOperation = request.HistoryOperation,
                IsOperation = request.IsOperation == null ? IsOperation.Orther : request.IsOperation,
                CreateUser = request.userId,
                UpdateUser = request.userId
            };

            await _context.Histories.AddAsync(data);
            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Cập nhật thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> UpdateToHistory(int Id, GetUpdateToHistoryRequest request)
        {
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var findId = await FindIdExistence(Id);

            if (findId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            findId.HistoryOperation = !string.IsNullOrEmpty(request.historyOperation) ? request.historyOperation : findId.HistoryOperation;
            findId.IsOperation = request.isOperation != null ? request.isOperation : findId.IsOperation;
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

        public async Task<ApiResponse> DeleteToHistory(int Id, CreateUserRequest request)
        {

            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var finfId = await FindIdExistence(Id);

            if (finfId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            finfId.UpdateUser = request.userId;
            finfId.IsDelete = false;

            finfId.TimeDelete = finfId.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        /// <summary>
        /// List
        /// </summary>
        /// <param name="Hiển thị thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<GetHistoryViewModel> GetHistoryById(int Id)
        {
            var query = await _context.Histories.Where(x => x.IsDelete == true && x.Id == Id).ToListAsync();
            if (!query.Any())
                return null;

            return query.Select(x => new GetHistoryViewModel()
            {
                Id = x.Id,
                HistoryOperation = x.HistoryOperation,
                IsOperation = x.IsOperation,
                TimeOperation = x.TimeOperation,
                UpdateDate = x.UpdateDate,
                UpdateUser = x.UpdateUser,
                CreateDate = x.CreateDate,
                CreateUser = x.CreateUser
            }).FirstOrDefault();
        }

        public async Task<IEnumerable<GetHistoryViewModel>> GetListToHistory(GetMangagerToHistoryRequest request)
        {
            var query = await _context.Histories.Where(x => x.IsDelete == true).ToListAsync();

            if (!query.Any()) return null;

            bool cKeyWord = checkValue.CheckNullValue(request.keyWord);

            if (cKeyWord)
            {
                query = query.Where(x => x.Id.ToString().Contains(request.keyWord)
                || x.HistoryOperation.Contains(request.keyWord)).ToList();
            }
            if (request.isOperation != null)
            {
                query = query.Where(x => x.IsOperation == request.isOperation).ToList();
            }

            return query.Select(x => new GetHistoryViewModel()
            {
                Id = x.Id,
                UpdateUser = x.UpdateUser,
                CreateDate = x.CreateDate,
                CreateUser = x.CreateUser,
                UpdateDate = x.UpdateDate,
                HistoryOperation = x.HistoryOperation,
                IsOperation = x.IsOperation,
                TimeOperation = x.TimeOperation
            });
        }

        public async Task<PagedResult<GetHistoryViewModel>> GetListPagingToHistory(GetMangagerHistoryRequest request)
        {
            var query = await _context.Histories.Where(x => x.IsDelete == true).ToListAsync();

            if (!query.Any()) return null;

            bool cKeyWord = checkValue.CheckNullValue(request.keyWord);

            if (cKeyWord)
            {
                query = query.Where(x => x.Id.ToString().Contains(request.keyWord)
                || x.HistoryOperation.Contains(request.keyWord)).ToList();
            }
            if (request.isOperation != null)
            {
                query = query.Where(x => x.IsOperation == request.isOperation).ToList();
            }

            int totalRow = query.Count();

            var data = query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new GetHistoryViewModel()
                {
                    HistoryOperation= x.HistoryOperation,
                    IsOperation = x.IsOperation,
                    TimeOperation = x.TimeOperation,
                    CreateDate = x.CreateDate,
                    CreateUser = x.CreateUser,
                    Id = x.Id,
                    UpdateDate = x.UpdateDate,
                    UpdateUser = x.UpdateUser
                }).ToList();
            return new PagedResult<GetHistoryViewModel>()
            {
                Items = data,
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };
        }
    }
}
