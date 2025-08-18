using H04.Cts.DanhMucs.Minio;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Dtos.DanhMucs;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;
using H04.Cts.Utilities;

namespace H04.Cts.Application.DanhMucs.LucLuongs.Jobs
{
    public class ImportLucLuongJob : AsyncBackgroundJob<ImportLucLuongJobArgs>, ITransientDependency
    {
        private readonly MinioService _minio;
        private readonly IRepository<LucLuong, long> _repo;
        private readonly IUnitOfWorkManager _uowManager;
        private readonly ILogger<ImportLucLuongJob> _logger;
        private readonly IDistributedCache<ImportProgressDto> _progressCache;

        public ImportLucLuongJob(
            MinioService minio,
            IRepository<LucLuong, long> repo,
            IUnitOfWorkManager uowManager,
            ILogger<ImportLucLuongJob> logger,
            IDistributedCache<ImportProgressDto> progressCache)
        {
            _minio = minio;
            _repo = repo;
            _uowManager = uowManager;
            _logger = logger;
            _progressCache = progressCache;
        }

        public override async Task ExecuteAsync(ImportLucLuongJobArgs args)
        {
            _logger.LogInformation("Start import LucLuong. BatchId={BatchId}, Object={Object}", args.BatchId, args.ObjectName);

            var key = $"import:luc-luong:{args.BatchId}";
            // Init progress as Running
            await _progressCache.SetAsync(key, new ImportProgressDto
            {
                BatchId = args.BatchId!,
                Status = "Running",
                StartedAt = DateTime.UtcNow,
                TotalRows = 0,
                ProcessedRows = 0
            });

            await using var stream = await _minio.GetObjectStreamAsync(args.ObjectName);
            using IWorkbook workbook = new XSSFWorkbook(stream);
            var sheet = workbook.GetSheetAt(0);
            if (sheet == null)
            {
                _logger.LogWarning("Excel has no sheet");
                await _progressCache.SetAsync(key, new ImportProgressDto
                {
                    BatchId = args.BatchId!,
                    Status = "Failed",
                    Message = "Excel không có sheet",
                    StartedAt = DateTime.UtcNow,
                    FinishedAt = DateTime.UtcNow
                });
                return;
            }

            var firstDataRow = sheet.FirstRowNum + 1;
            var totalRows = Math.Max(0, sheet.LastRowNum - firstDataRow + 1);
            await _progressCache.SetAsync(key, new ImportProgressDto
            {
                BatchId = args.BatchId!,
                Status = "Running",
                StartedAt = DateTime.UtcNow,
                TotalRows = totalRows,
                ProcessedRows = 0
            });

            var batchSize = 100;
            var buffer = new List<LucLuong>(batchSize);
            int processed = 0;
            int lastPercent = -1;

            // Assume first row is header
            for (int i = firstDataRow; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                if (row == null) continue;

                var ma = row.GetCell(0)?.ToString()?.Trim();
                var ten = row.GetCell(1)?.ToString()?.Trim();
                var ghiChu = row.GetCell(2)?.ToString()?.Trim();
                var trangThaiStr = row.GetCell(3)?.ToString()?.Trim();

                if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten))
                {
                    continue;
                }

                var entity = new LucLuong
                {
                    MaLucLuong = ma,
                    TenLucLuong = ten,
                    GhiChu = ghiChu,
                };

                if (int.TryParse(trangThaiStr, out var tt))
                {
                    try { entity.TrangThai = (TrangThai)tt; } catch { }
                }

                buffer.Add(entity);
                processed++;

                // update progress per row (only when percent changed)
                if (totalRows > 0)
                {
                    var percent = (int)Math.Floor(processed * 100.0 / totalRows);
                    if (percent != lastPercent)
                    {
                        lastPercent = percent;
                        await _progressCache.SetAsync(key, new ImportProgressDto
                        {
                            BatchId = args.BatchId!,
                            Status = "Running",
                            StartedAt = DateTime.UtcNow,
                            TotalRows = totalRows,
                            ProcessedRows = processed
                        });
                    }
                }

                if (buffer.Count >= batchSize)
                {
                    await SaveBatchAsync(buffer);
                    buffer.Clear();
                }
            }

            if (buffer.Count > 0)
            {
                await SaveBatchAsync(buffer);

                await _progressCache.SetAsync(key, new ImportProgressDto
                {
                    BatchId = args.BatchId!,
                    Status = "Running",
                    StartedAt = DateTime.UtcNow,
                    TotalRows = totalRows,
                    ProcessedRows = processed
                });
            }

            await _progressCache.SetAsync(key, new ImportProgressDto
            {
                BatchId = args.BatchId!,
                Status = "Completed",
                StartedAt = DateTime.UtcNow,
                FinishedAt = DateTime.UtcNow,
                TotalRows = totalRows,
                ProcessedRows = processed
            });

            _logger.LogInformation("Import LucLuong done. BatchId={BatchId}, Processed={Processed}", args.BatchId, processed);
        }

        private async Task SaveBatchAsync(List<LucLuong> entities)
        {
            using var uow = _uowManager.Begin(requiresNew: true, isTransactional: true);
            foreach (var e in entities)
            {
        
                var exist = await _repo.FirstOrDefaultAsync(x => x.MaLucLuong == e.MaLucLuong);
                if (exist != null)
                {
                    exist.TenLucLuong = e.TenLucLuong;
                    exist.TrangThai = e.TrangThai;
                    exist.GhiChu = e.GhiChu;
                    await _repo.UpdateAsync(exist, autoSave: false);
                }
                else
                {
                    await _repo.InsertAsync(e, autoSave: false);
                }
            }
            await uow.CompleteAsync();
        }
    }
}
