using Microsoft.EntityFrameworkCore;
using SOR.Data.EFs;
using SOR.Data.SystemBase;
using SOR.ViewModel.Catalogs.Agencies;
using System;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Agencies
{
    public class AgenciesSevice : IAgenciesSevice
    {
        private readonly SORDbContext _context;

        public AgenciesSevice(SORDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Check
        /// </summary>
        /// <param name="Kiểm tra thông tin"></param>
        /// <returns></returns>

        public async Task<bool> NameAgenciesExistence(string name)
        {
            var checkName = await _context.Agencies.FirstOrDefaultAsync(x => x.Name == name);
            if (checkName == null) return false;
            return true;
        }

        public async Task<Data.Entitis.Agencies> FindIdExistence(int Id)
        {
            var findId = await _context.Agencies.FirstOrDefaultAsync(x => x.Id == Id);
            return findId;
        }


        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Tạo thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> CreateToAgencies(GetCreateToAgenciesRequest request, int userId)
        {
            bool checkName = await NameAgenciesExistence(request.name);

            if (!checkName) return new ApiResponse(MessageBase.NAME_EXISTENCE, 400);

            var data = new Data.Entitis.Agencies()
            {
                Name = request.name.Trim(),
                NumberPhone = request.numberPhone,
                Office = request.office,
                CreateUser = userId,
                UpdateUser = userId
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

        public async Task<ApiResponse> UpdateToAgencies(int Id, GetUpdateToAgenciesRequest request, int userId)
        {
            var findId = await FindIdExistence(Id);

            if (findId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            findId.Name = !string.IsNullOrEmpty(request.name) ? request.name : findId.Name;
            findId.Office = !string.IsNullOrEmpty(request.office) ? request.office : findId.Office;
            findId.NumberPhone = !string.IsNullOrEmpty(request.numberPhone) ? request.numberPhone : findId.NumberPhone;
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

            if(finfId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            finfId.UpdateUser = userId;
            finfId.IsDelete = true;
            finfId.TimeDelete = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);

        }

    }
}
