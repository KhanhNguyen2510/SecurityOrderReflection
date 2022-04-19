using Microsoft.EntityFrameworkCore;
using SOR.Data.EFs;
using SOR.Data.Enum;
using SOR.Data.SystemBase;
using SOR.ViewModel.Catalogs.Agencies;
using SOR.ViewModel.Catalogs.Historys;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Historys
{
    public class HistorySevice : IHistorySevice
    {
        private readonly SORDbContext _context;

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
            var findId = await _context.Histories.FirstOrDefaultAsync(x => x.Id == Id);
            return findId;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Tạo thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> CreateToHistory(GetCreateToHistoryRequest request, int userId)
        {
            var data = new Data.Entitis.History()
            {
                HistoryOperation = request.HistoryOperation,
                Operation = request.Operation == null? IsOperation.Orther : request.Operation,
                CreateUser = userId,
                UpdateUser = userId
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

        public async Task<ApiResponse> UpdateToHistory(int Id, GetUpdateToHistoryRequest request, int userId)
        {
            var findId = await FindIdExistence(Id);

            if (findId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            findId.Operation = request.Operation != null ? request.Operation : findId.Operation;
            findId.HistoryOperation = !string.IsNullOrEmpty(request.HistoryOperation) ? request.HistoryOperation : findId.HistoryOperation;
            findId.UpdateUser = userId;
            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Xóa thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> DeleteToAgencies(int Id, int userId)
        {
            var finfId = await FindIdExistence(Id);

            if (finfId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            finfId.UpdateUser = userId;
            finfId.IsDelete = true;
            finfId.TimeDelete = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);

        }
    }
}
