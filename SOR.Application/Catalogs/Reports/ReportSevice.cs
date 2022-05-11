using Microsoft.EntityFrameworkCore;
using SOR.Application.Catalogs.Historys;
using SOR.Application.Catalogs.Mobiles;
using SOR.Application.Catalogs.NewsLabels;
using SOR.Application.Catalogs.Reports.Upload;
using SOR.Data.EFs;
using SOR.Data.Enum;
using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Historys;
using SOR.ViewModel.Catalogs.Mobile;
using SOR.ViewModel.Catalogs.Reports;
using SOR.ViewModel.Catalogs.Reports.Proof;
using SOR.ViewModel.Catalogs.Reports.Report;
using SOR.ViewModel.Catalogs.Reports.Result;
using SOR.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SOR.Application.Catalogs.Reports
{
    public class ReportSevice : IReportSevice
    {
        private readonly SORDbContext _context;
        private readonly IFileSevice _fileSevice;
        private readonly IHistorySevice _historySevice;
        private SystemBase<string> checkValue = new SystemBase<string>();
        private readonly IMobileSevice _mobileSevice;
        private readonly INewsLabelSevice _newsLabelSevice;
   
        public ReportSevice(SORDbContext context, IFileSevice fileSevice , IHistorySevice historySevice , IMobileSevice mobileSevice , INewsLabelSevice newsLabelSevice)
        {
            _context = context;
            _fileSevice = fileSevice;
            _historySevice = historySevice;
            _mobileSevice = mobileSevice;
            _newsLabelSevice = newsLabelSevice;
        }

        /// <summary>
        /// Generreate
        /// </summary>
        /// <param name="Kiểm tra thông tin"></param>
        /// <returns></returns>
        /// 
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

        public async Task<string> RandomID(string location)
        {
            var key = await GetKeyByLocationReport(location);
            var generate = AutoGenerate.GenerateIdRandom(4);
            Thread.Sleep(5);

            return $"{key}{generate}-{DateTime.Now.ToString("ddMMyyyyHHmm")}";///Mã gồm key và mả random 3 chữ và ngày tháng năm giờ phút của mã
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
        public async Task<bool> ReportIdExistence(string Id)
        {
            var cNewsLable = await _context.Reports.FirstOrDefaultAsync(x => x.Id == Id && x.IsDelete == true);
            if (cNewsLable == null) return false;
            return true;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Tạo thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> CreateToReport(GetCreateToReportRequest request)
        {
            #region Check Value
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            bool cContent = checkValue.CheckNullValue(request.content);
            if (!cContent) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            bool cLocation = checkValue.CheckNullValue(request.locationReport);
            if (!cLocation) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            bool cNewsLable = checkValue.CheckNullValue(request.newsLabelId);
            if (!cNewsLable) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            request.newsLabelId = request.newsLabelId.Trim();
            var cNewsLableById = await _newsLabelSevice.FindIdExistence(request.newsLabelId);
            if (cNewsLableById == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);
            #endregion

            request.content = request.content.Trim();
            request.locationReport = request.locationReport.Trim();

            var IdRandom = await RandomID(request.locationReport);

            #region Add Report
            var dReport = new Data.Entitis.Report()
            {
                Id = IdRandom,
                Content = request.content,
                LocationReport = request.locationReport,
                LocationUser = request.locationUser,
                NewsLabelId = request.newsLabelId,
                IP = request.iP,
                UserAngel = request.userAngel,

                CreateUser = request.userId,
                UpdateUser = request.userId
            };

            await _context.Reports.AddAsync(dReport);
            await _context.SaveChangesAsync();
            #endregion

            #region Add Proof
            var dProof = new GetCreateToReportProofRequest()
            {
                files = request.files,
                reportId = IdRandom,
                userId = request.userId
            };

            await CreateToReportProof(dProof);
            #endregion

            #region Add History
            var dhistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Thêm bài báo cáo với ID {IdRandom}",
                IsOperation = IsOperation.Create,
                userId = request.userId
            };

            await _historySevice.CreateToHistory(dhistory);
            #endregion

            #region Add Notification
            var dNotification = new Notification()
            {
                tille = "Phản ánh trật tự an ninh",
                body = @$"Có một báo cáo với nội dung : {request.content} . Tại địa chỉ là {request.locationUser}"
            };
            await _mobileSevice.ShowNotificationInMobile(request.userId, dNotification);
            #endregion

            return new ApiResponse(MessageBase.SUCCCESS);
        } ///Show

        public async Task<ApiResponse> CreateToReportProof(GetCreateToReportProofRequest request)
        {
            var gFiles = _fileSevice.UploadImage(request.files);

            var proofs = new List<Data.Entitis.ReportProof>();

            Parallel.ForEach(gFiles, file =>
            {
                var data = new Data.Entitis.ReportProof()
                {
                    ReportId = request.reportId,
                    Proof = file,
                    CreateUser = request.userId,
                    UpdateUser = request.userId
                };
                proofs.Add(data);
            });

            await _context.ReportProofs.AddRangeAsync(proofs);
            await _context.SaveChangesAsync();

            return new ApiResponse(MessageBase.SUCCCESS);
        }

        public async Task<ApiResponse> CreateToReportResult(GetCreateToReportResultRequest request)
        {
            #region  Check Value
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            bool cProof = checkValue.CheckNullValue(request.content);
            if (!cProof) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            bool cReportId = checkValue.CheckNullValue(request.reportId);
            if (!cReportId) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            request.reportId = request.reportId.Trim();

            bool cReportById = await ReportIdExistence(request.reportId);
            if (cReportById) return new ApiResponse(MessageBase.NAME_EXISTENCE, 400);
            #endregion

            #region Add Result
            var dResult = new Data.Entitis.ReportResult()
            {
                Content = request.content,
                ReportId = request.reportId,

                CreateUser = request.userId,
                UpdateUser = request.userId
            };

            await _context.ReportResults.AddAsync(dResult);
            await _context.SaveChangesAsync();
            #endregion

            #region Add History
            var dhistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Thêm thông tin kết quả cho bài báo cáo với ID {request.reportId}",
                IsOperation = IsOperation.Create,
                userId = request.userId
            };

            await _historySevice.CreateToHistory(dhistory);
            #endregion

            return new ApiResponse(MessageBase.SUCCCESS);
        } ///Show

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="Cập nhật thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<ApiResponse> UpdateStatus(string Id, GetUpdateStatusInReportRequest request)
        {
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var findId = await FindIdExistence(Id);

            if (findId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            findId.IsStatus = (IsStatus)(request.IsStatus != null? request.IsStatus : findId.IsStatus);
            findId.UpdateUser = request.userId;
            findId.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            #region Add History
            var dhistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Cập nhật trang thái giải quyết từ {findId.IsStatus} thành {request.IsStatus} với ID {Id}",
                IsOperation = IsOperation.Update,
                userId = request.userId
            };

            await _historySevice.CreateToHistory(dhistory);
            #endregion

            return new ApiResponse(MessageBase.SUCCCESS);
        } ///Show

        public async Task<ApiResponse> UpdateIsReport(string Id, GetUpdateReportInReportRequest request)
        {
            bool cUser = checkValue.CheckNullValue(request.userId);
            if (!cUser) return new ApiResponse(MessageBase.USER_EXISTENCE, 400);

            var findId = await FindIdExistence(Id);

            if (findId == null) return new ApiResponse(MessageBase.NON_EXISTENCE, 400);

            findId.IsReport = (IsReport)(request.IsReport != null ? request.IsReport : findId.IsReport);
            findId.UpdateUser = request.userId;
            findId.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            #region Add History
            var dhistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Cập nhật trang thái bài đăng từ {findId.IsDelete} thành {request.IsReport} với ID {Id}",
                IsOperation = IsOperation.Update,
                userId = request.userId
            };

            await _historySevice.CreateToHistory(dhistory);
            #endregion

            return new ApiResponse(MessageBase.SUCCCESS);
        } ///Show

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

                var cNewsLableById = await _newsLabelSevice.FindIdExistence(request.newsLabelId);
                if (cNewsLableById != null) return new ApiResponse(MessageBase.NAME_EXISTENCE, 400);

                findId.NewsLabelId = request.newsLabelId;
            }

            findId.UpdateUser = request.userId;
            findId.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();

            #region Add History
            var dhistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Cập nhật thông tin với ID {Id}",
                IsOperation = IsOperation.Update,
                userId = request.userId
            };

            await _historySevice.CreateToHistory(dhistory);
            #endregion

            return new ApiResponse(MessageBase.SUCCCESS);
        } ///Show

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

                #region Add History
                var dhistory = new GetCreateToHistoryRequest()
                {
                    HistoryOperation = $"Cập nhật kết quả bài báo cáo với ID {Id}",
                    IsOperation = IsOperation.Update,
                    userId = request.userId
                };

                await _historySevice.CreateToHistory(dhistory);
                #endregion
            }

            return new ApiResponse(MessageBase.SUCCCESS);
        } ///Show

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

            #region Add History
            var dhistory = new GetCreateToHistoryRequest()
            {
                HistoryOperation = $"Xóa bài báo cáo với ID {Id}",
                IsOperation = IsOperation.Delete,
                userId = request.userId
            };

            await _historySevice.CreateToHistory(dhistory);
            #endregion

            return new ApiResponse(MessageBase.SUCCCESS);
        } ///Show

        public async Task GetDeleteReportProof(string Id, CreateUserRequest request)
        {
            var gReportProof = await _context.ReportProofs.Where(x => x.ReportId == Id && x.IsDelete == true).Select(x => x.Id).ToListAsync();

            int cnReportProof = gReportProof.Count;

            for (int item = 0; item < cnReportProof; item++)
            {
                await DeleteToReportProof(gReportProof[item], request);
            }
        }
        
        public async Task GetDeleteReportResult(string Id, CreateUserRequest request)
        {
            var gReportResult = await _context.ReportResults.Where(x => x.ReportId == Id && x.IsDelete == true).Select(x => x.Id).ToListAsync();

            int cnReportResult = gReportResult.Count;

            for (int item = 0; item < cnReportResult; item++)
            {
                await DeleteToReportResult(gReportResult[item], request);
            }
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
        }  ///Show

        /// <summary>
        /// List
        /// </summary>
        /// <param name="Hiển thị thông tin"></param>
        /// <returns></returns>
        /// 

        public async Task<GetReportViewModel> GetReportById(string Id)
        {
            var gReport = await _context.Reports.Where(x => x.IsDelete == true && x.Id == Id)
                .Include(x => x.NewsLabel)
                .Include(x => x.ReportProofs)
                .Include(x=>x.ReportResults)
                .FirstOrDefaultAsync();

            if (gReport == null)
                return null;

            return new GetReportViewModel()
            {
                Content = gReport.Content,
                DateSolve = gReport.DateSolve,
                NewsLabelId = gReport.NewsLabelId,
                IsStatus = gReport.IsStatus,
                IP = gReport.IP,
                LocationReport = gReport.LocationReport,
                LocationUser = gReport.LocationUser,
                NewsLabelName = gReport.Content,

                rProofs = gReport.ReportProofs == null
                ? null : gReport.ReportProofs.Select(x => new Proofs { Id = x.Id, Name = x.Proof }).ToList(),

                rResults = gReport.ReportResults == null
                ? null : gReport.ReportResults.Select(x => new ViewModel.Catalogs.Reports.Results { Name = x.Content, Id = x.Id }).ToList(),

                UserAngel = gReport.UserAngel,
                Views = gReport.Views,
                CreateDate = gReport.CreateDate,
                CreateUser = gReport.CreateUser,
                Id = gReport.Id,
                UpdateDate = gReport.UpdateDate,
                UpdateUser = gReport.UpdateUser
            };
        }

        public async Task<IEnumerable<GetReportViewModel>> GetListToReport(GetMangagerToReportRequest request)
        {
            var gReport = await _context.Reports.Where(x => x.IsDelete == true)
                .Include(x => x.NewsLabel)
                .Include(x => x.ReportProofs)
                .Include(x => x.ReportResults)
                .ToListAsync();

            if (!gReport.Any()) return null;

            bool cKeyWord = checkValue.CheckNullValue(request.keyWord);

            if (cKeyWord)
            {
                gReport = gReport.Where(x => x.Id.ToString().Contains(request.keyWord) || x.Content.Contains(request.keyWord)).ToList();
            }
            if (request.IsStatus != null)
            {
                gReport = gReport.Where(x => x.IsStatus == request.IsStatus).ToList();
            }
            if (request.NewslableId != null)
            {
                gReport = gReport.Where(x => x.NewsLabelId == request.NewslableId).ToList();
            }


            return gReport.Select(x => new GetReportViewModel()
            {
                Content = x.Content,
                DateSolve = x.DateSolve,
                NewsLabelId = x.NewsLabelId,
                IsStatus = x.IsStatus,
                IP = x.IP,
                LocationReport = x.LocationReport,
                LocationUser = x.LocationUser,
                NewsLabelName = x.Content,

                rProofs = x.ReportProofs == null
            ? null : x.ReportProofs.Select(x => new Proofs { Id = x.Id, Name = x.Proof }).ToList(),

                rResults = x.ReportResults == null
            ? null : x.ReportResults.Select(x => new ViewModel.Catalogs.Reports.Results { Name = x.Content, Id = x.Id }).ToList(),

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
            var gReport = await _context.Reports.Where(x => x.IsDelete == true)
                        .Include(x => x.NewsLabel)
                        .Include(x => x.ReportProofs)
                        .Include(x => x.ReportResults)
                        .ToListAsync();


            if (!gReport.Any()) return null;

            bool cKeyWord = checkValue.CheckNullValue(request.keyWord);

            if (cKeyWord)
            {
                gReport = gReport.Where(x => x.Id.ToString().Contains(request.keyWord) || x.Content.Contains(request.keyWord)).ToList();
            }
            if (request.IsStatus != null)
            {
                gReport = gReport.Where(x => x.IsStatus == request.IsStatus).ToList();
            }
            if (request.NewslableId != null)
            {
                gReport = gReport.Where(x => x.NewsLabelId == request.NewslableId).ToList();
            }

            int totalRow = gReport.Count();

            var data = gReport.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize).Select(x => new GetReportViewModel()
                {
                    Content = x.Content,
                    DateSolve = x.DateSolve,
                    NewsLabelId = x.NewsLabelId,
                    IsStatus = x.IsStatus,
                    IP = x.IP,
                    LocationReport = x.LocationReport,
                    LocationUser = x.LocationUser,
                    NewsLabelName = x.Content,

                    rProofs = x.ReportProofs == null
                ? null : x.ReportProofs.Select(x => new Proofs { Id = x.Id, Name = x.Proof }).ToList(),

                    rResults = x.ReportResults == null
                ? null : x.ReportResults.Select(x => new ViewModel.Catalogs.Reports.Results { Name = x.Content, Id = x.Id }).ToList(),

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
