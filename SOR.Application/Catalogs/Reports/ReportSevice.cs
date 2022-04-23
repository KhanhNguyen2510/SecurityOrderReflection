using Microsoft.EntityFrameworkCore;
using SOR.Data.EFs;
using SOR.Data.Enum;
using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Reports;
using SOR.ViewModel.Catalogs.Reports.Proof;
using SOR.ViewModel.Catalogs.Reports.Report;
using SOR.ViewModel.Catalogs.Reports.Result;
using SOR.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Reports
{
    public class ReportSevice : IReportSevice
    {
        private readonly SORDbContext _context;
        private SystemBase<string> checkValue = new SystemBase<string>();

        public ReportSevice(SORDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Check
        /// </summary>
        /// <param name="Kiểm tra thông tin"></param>
        /// <returns></returns>

        public async Task<Data.Entitis.Report> FindIdExistence(string Id)
        {
            var findId = await _context.Reports.FirstOrDefaultAsync(x => x.Id == Id && x.IsDelete == true);
            return findId;
        }
        public async Task<Data.Entitis.ReportProof> FindIdProofExistence(int Id)
        {
            var findId = await _context.ReportProofs.FirstOrDefaultAsync(x => x.Id == Id && x.IsDelete == true);
            return findId;
        }
        public async Task<Data.Entitis.ReportResult> FindIdResultExistence(int Id)
        {
            var findId = await _context.ReportResults.FirstOrDefaultAsync(x => x.Id == Id && x.IsDelete == true);
            return findId;
        }

        public async Task<bool> NewsLableExistence(string Id)
        {
            var cNewsLable = await _context.NewsLabels.FirstOrDefaultAsync(x => x.Id == Id);
            if(cNewsLable == null) return false;
            return true;
        }

        public async Task<bool> ReportIdExistence(string Id)
        {
            var cNewsLable = await _context.Reports.FirstOrDefaultAsync(x => x.Id == Id);
            if (cNewsLable == null) return false;
            return true;
        }



        public async Task<string> GetKeyInLocationReport(string location)
        {
            var fCode = await _context.Codes.Where(x => x.IsDelete == true).Select(x => new { x.Name, x.Key }).ToListAsync();

            if(!fCode.Any()) return MessageBase.KEY_UN_EXISTENCE;

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

        public StringBuilder GenerateIdRandom(int number)
        {
            var random = new Random();
            var rString = new StringBuilder();

            for (int i = 1; i <= number; i++)
            {
                int rNumber = random.Next(0, 3);
                var r = ((char)(random.Next(1, 26) + 64)).ToString();

                switch (rNumber)
                {
                    case 0:
                        rString.Append(r.ToUpper());
                        break;
                    case 1:
                        rString.Append(r.ToLower());
                        break;
                    case 2:
                        rString.Append(random.Next(0, 9));
                        break;
                }
            }
            return rString;
        }

        public async Task<string> RandomID(string location)
        {
            var key = await GetKeyInLocationReport(location);
            var generate = GenerateIdRandom(3);
            Thread.Sleep(5);
          
            return $"{key}{generate}-{DateTime.Now.ToString("ddMMyyyyHHmm")}";///Mã gồm key và mả random 3 chữ và ngày tháng năm giờ phút của mã
        }
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Tạo thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> CreateToReport(GetCreateToReportRequest request)
        {
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            bool cContent = checkValue.CheckNullValue(request.content);
            if (!cContent) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            bool cLocation = checkValue.CheckNullValue(request.locationReport);
            if (!cLocation) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            bool cNewsLable = checkValue.CheckNullValue(request.newsLabelId);
            if (!cNewsLable) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            request.newsLabelId = request.newsLabelId.Trim();
            bool cNewsLableById = await NewsLableExistence(request.newsLabelId);
            if(!cNewsLableById) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            request.content = request.content.Trim();
            request.locationReport = request.locationReport.Trim();

            var IdRandom = await RandomID(request.locationReport);

            var data = new Data.Entitis.Report()
            {
                Id = IdRandom,
                Content= request.content,
                LocationReport= request.locationReport,
                LocationUser = request.locationUser,
                NewsLabelId = request.newsLabelId,
                IP = request.iP,
                UserAngel = request.userAngel,
                
                CreateUser = request.userId,
                UpdateUser = request.userId
            };

            await _context.Reports.AddAsync(data);
            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        public async Task<ApiResponse> CreateToReportProof(GetCreateToReportProofRequest request)
        {
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            bool cProof = checkValue.CheckNullValue(request.proof);
            if (!cProof) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            bool cReportId = checkValue.CheckNullValue(request.reportId);
            if (!cReportId) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            request.reportId = request.reportId.Trim();

            bool cReportById = await ReportIdExistence(request.reportId);
            if (cReportById) return new ApiResponse(MessageBase.NAME_EXISTENCE, 400);

            var data = new Data.Entitis.ReportProof()
            {
                ReportId = request.reportId,
                Proof = request.proof,

                CreateUser = request.userId,
                UpdateUser = request.userId
            };

            await _context.ReportProofs.AddAsync(data);
            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        public async Task<ApiResponse> CreateToReportResult(GetCreateToReportResultRequest request)
        {
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            bool cProof = checkValue.CheckNullValue(request.content);
            if (!cProof) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            bool cReportId = checkValue.CheckNullValue(request.reportId);
            if (!cReportId) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            request.reportId = request.reportId.Trim();

            bool cReportById = await ReportIdExistence(request.reportId);
            if (cReportById) return new ApiResponse(MessageBase.NAME_EXISTENCE, 400);

            var data = new Data.Entitis.ReportResult()
            {
                Content = request.content,
                ReportId = request.reportId,
                
                CreateUser = request.userId,
                UpdateUser = request.userId
            };

            await _context.ReportResults.AddAsync(data);
            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Cập nhật thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> UpdateToReport(string Id, GetUpdateToReportRequest request)
        {

            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var findId = await FindIdExistence(Id);

            if (findId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            bool cContent = checkValue.CheckNullValue(request.content);

            if (cContent)
            {
                request.content = request.content.Trim();
                findId.Content = request.content;
            }

            bool cNewsLable = checkValue.CheckNullValue(request.newsLabelId);

            if (cNewsLable)
            {
                request.newsLabelId = request.newsLabelId.Trim();

                bool cNewsLableById = await NewsLableExistence(request.newsLabelId);
                if (cNewsLableById) return new ApiResponse(MessageBase.NAME_EXISTENCE, 400);

                findId.NewsLabelId = request.newsLabelId;
            }

            findId.UpdateUser = request.userId;
            findId.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        public async Task<ApiResponse> UpdateToReportResult(int Id, GetUpdateToReportResultRequest request)
        {

            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var findId = await FindIdResultExistence(Id);

            if (findId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            bool cContent = checkValue.CheckNullValue(request.content);

            if (cContent)
            {
                request.content = request.content.Trim();
                findId.Content = request.content;

                findId.UpdateUser = request.userId;
                findId.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Xóa thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> DeleteToReport(string Id, CreateUserRequest request)
        {
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var finfId = await FindIdExistence(Id);

            if (finfId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            finfId.UpdateUser = request.userId;
            finfId.IsDelete = false;

            finfId.TimeDelete = finfId.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            await GetDeleteReportProof(Id, request);

            await GetDeleteReportResult(Id, request);

            return new ApiResponse(MessageBase.SUCCCESS);
        }


        public async Task GetDeleteReportProof(string Id, CreateUserRequest request)
        {
            var gReportProof = await _context.ReportProofs.Where(x => x.ReportId == Id && x.IsDelete == true).Select(x => x.Id).ToListAsync();
            Parallel.ForEach(gReportProof, async id =>
            {
                await DeleteToReportProof(id, request);
            });
        }
        public async Task GetDeleteReportResult(string Id, CreateUserRequest request)
        {
            var gReportProof = await _context.ReportResults.Where(x => x.ReportId == Id && x.IsDelete == true).Select(x => x.Id).ToListAsync();
            Parallel.ForEach(gReportProof, async id =>
            {
                await DeleteToReportResult(id, request);
            });
        }

        public async Task<ApiResponse> DeleteToReportProof(int Id, CreateUserRequest request)
        {
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var finfId = await FindIdProofExistence(Id);

            if (finfId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            finfId.UpdateUser = request.userId;
            finfId.IsDelete = false;

            finfId.TimeDelete = finfId.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        public async Task<ApiResponse> DeleteToReportResult(int Id, CreateUserRequest request)
        {
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var finfId = await FindIdResultExistence(Id);

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

        public async Task<GetReportViewModel> GetReportById(string Id)
        {
            var query = await (from r in _context.Reports
                               join rs in _context.ReportResults
                               on r.Id equals rs.ReportId
                               join rp in _context.ReportProofs
                               on r.Id equals rp.ReportId
                               join n in _context.NewsLabels
                               on r.NewsLabelId equals n.Id
                               where r.IsDelete == true && n.IsDelete == true
                               && rs.IsDelete == true && rp.IsDelete == true
                               && r.Id == Id
                               select new
                               {
                                   r.Id,
                                   r.Content,
                                   Proof = rp.Proof,
                                   ProofId = rp.Id,
                                   Result = rs.Content,
                                   ResultId = rs.Id,
                                   NewsLabelId = n.Id,
                                   NewsLabelName = n.Name,
                                   r.IP,
                                   r.UserAngel,
                                   r.LocationReport,
                                   r.LocationUser,
                                   r.Views,
                                   r.DateSolve,
                                   r.IsStatus,
                                   r.CreateDate,
                                   r.UpdateDate,
                                   r.CreateUser,
                                   r.UpdateUser
                               }).ToListAsync();
            if (!query.Any())
                return null;

            return query.Select(x => new GetReportViewModel()
            {
                Content = x.Content,
                DateSolve = x.DateSolve,
                NewsLabelId = x.NewsLabelId,
                IsStatus = x.IsStatus,
                IP = x.IP,
                LocationReport = x.LocationReport,
                LocationUser = x.LocationUser,
                NewsLabelName = x.NewsLabelName,
                Proof = x.Proof,
                ProofId = x.ProofId,
                ResultId = x.ResultId,
                Result = x.Result,
                UserAngel = x.UserAngel,
                Views = x.Views,
                CreateDate = x.CreateDate,
                CreateUser = x.CreateUser,
                Id = x.Id,
                UpdateDate = x.UpdateDate,
                UpdateUser = x.UpdateUser
            }).FirstOrDefault();
        }

        public async Task<IEnumerable<GetReportViewModel>> GetListToReport(GetMangagerToReportRequest request)
        {
            var query = await (from r in _context.Reports
                               join rs in _context.ReportResults
                               on r.Id equals rs.ReportId
                               join rp in _context.ReportProofs
                               on r.Id equals rp.ReportId
                               join n in _context.NewsLabels
                               on r.NewsLabelId equals n.Id
                               where r.IsDelete == true && n.IsDelete == true
                               && rs.IsDelete == true && rp.IsDelete == true
                               select new
                               {
                                   r.Id,
                                   r.Content,
                                   Proof = rp.Proof,
                                   ProofId = rp.Id,
                                   Result = rs.Content,
                                   ResultId = rs.Id,
                                   NewsLabelId = n.Id,
                                   NewsLabelName = n.Name,
                                   r.IP,
                                   r.UserAngel,
                                   r.LocationReport,
                                   r.LocationUser,
                                   r.Views,
                                   r.DateSolve,
                                   r.IsStatus,
                                   r.CreateDate,
                                   r.UpdateDate,
                                   r.CreateUser,
                                   r.UpdateUser
                               }).ToListAsync();


            if (!query.Any()) return null;

            bool cKeyWord = checkValue.CheckNullValue(request.keyWord);

            if (cKeyWord)
            {
                query = query.Where(x => x.Id.ToString().Contains(request.keyWord) || x.Content.Contains(request.keyWord)).ToList();
            }
            if (request.IsStatus != null)
            {
                query = query.Where(x => x.IsStatus == request.IsStatus).ToList();
            }
            if (request.NewslableId != null)
            {
                query = query.Where(x => x.NewsLabelId == request.NewslableId).ToList();
            }


            return query.Select(x => new GetReportViewModel()
            {
                Content = x.Content,
                DateSolve = x.DateSolve,
                NewsLabelId = x.NewsLabelId,
                IsStatus = x.IsStatus,
                IP = x.IP,
                LocationReport = x.LocationReport,
                LocationUser = x.LocationUser,
                NewsLabelName = x.NewsLabelName,
                Proof = x.Proof,
                ProofId = x.ProofId,
                ResultId = x.ResultId,
                Result = x.Result,
                UserAngel = x.UserAngel,
                Views = x.Views,
                CreateDate = x.CreateDate,
                CreateUser = x.CreateUser,
                Id = x.Id,
                UpdateDate = x.UpdateDate,
                UpdateUser = x.UpdateUser
            });
        }

        public async Task<PagedResult<GetReportViewModel>> GetListPagingToReport(GetMangagerReportRequest request)
        {
            var query = await (from r in _context.Reports
                               join rs in _context.ReportResults
                               on r.Id equals rs.ReportId
                               join rp in _context.ReportProofs
                               on r.Id equals rp.ReportId
                               join n in _context.NewsLabels
                               on r.NewsLabelId equals n.Id
                               where r.IsDelete == true && n.IsDelete == true
                               && rs.IsDelete == true && rp.IsDelete == true
                               select new
                               {
                                   r.Id,
                                   r.Content,
                                   Proof = rp.Proof,
                                   ProofId = rp.Id,
                                   Result = rs.Content,
                                   ResultId = rs.Id,
                                   NewsLabelId = n.Id,
                                   NewsLabelName = n.Name,
                                   r.IP,
                                   r.UserAngel,
                                   r.LocationReport,
                                   r.LocationUser,
                                   r.Views,
                                   r.DateSolve,
                                   r.IsStatus,
                                   r.CreateDate,
                                   r.UpdateDate,
                                   r.CreateUser,
                                   r.UpdateUser
                               }).ToListAsync();


            if (!query.Any()) return null;

            bool cKeyWord = checkValue.CheckNullValue(request.keyWord);

            if (cKeyWord)
            {
                query = query.Where(x => x.Id.ToString().Contains(request.keyWord) || x.Content.Contains(request.keyWord)).ToList();
            }
            if (request.IsStatus != null)
            {
                query = query.Where(x => x.IsStatus == request.IsStatus).ToList();
            }
            if (request.NewslableId != null)
            {
                query = query.Where(x => x.NewsLabelId == request.NewslableId).ToList();
            }

            int totalRow = query.Count();

            var data = query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new GetReportViewModel()
                {
                    Content = x.Content,
                    DateSolve = x.DateSolve,
                    NewsLabelId = x.NewsLabelId,
                    IsStatus = x.IsStatus,
                    IP = x.IP,
                    LocationReport = x.LocationReport,
                    LocationUser = x.LocationUser,
                    NewsLabelName = x.NewsLabelName,
                    Proof = x.Proof,
                    ProofId = x.ProofId,
                    ResultId = x.ResultId,
                    Result = x.Result,
                    UserAngel = x.UserAngel,
                    Views = x.Views,
                    CreateDate = x.CreateDate,
                    CreateUser = x.CreateUser,
                    Id = x.Id,
                    UpdateDate = x.UpdateDate,
                    UpdateUser = x.UpdateUser
                }).ToList();
            return new PagedResult<GetReportViewModel>()
            {
                Items = data,
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };
        }
    }
}
