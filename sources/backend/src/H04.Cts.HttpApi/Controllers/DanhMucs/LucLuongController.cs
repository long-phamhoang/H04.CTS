using H04.Cts.DanhMucs.Minio;
using H04.Cts.Application.DanhMucs;
using H04.Cts.Dtos.DanhMucs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;

namespace H04.Cts.Controllers.DanhMucs;

[RemoteService(Name = "Default")]
[Area("app")]
[Route("api/app/luc-luong")]
public class LucLuongController : CtsController
{
    private readonly MinioService _minioService;
    private readonly ILucLuongAppService _lucLuongAppService;

    public LucLuongController(MinioService minioService, ILucLuongAppService lucLuongAppService)
    {
        _minioService = minioService;
        _lucLuongAppService = lucLuongAppService;
    }

    [AllowAnonymous]
    [HttpGet("template/{fileName}")]
    public async Task<IActionResult> DownloadTemplateAsync(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return BadRequest(new { success = false, message = "Tên file không được để trống" });

        try
        {
            var url = await _minioService.DownloadFileAsStreamAsync(fileName);
            return Ok(new { success = true, data = new { downloadUrl = url } });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Lỗi khi tạo download URL: {ex.Message}" });
        }
    }

    [Authorize]
    [HttpPost("import")]
    [RequestSizeLimit(52428800)] // 50 MB
    public async Task<IActionResult> ImportAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { success = false, message = "File không hợp lệ" });
        }
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (ext != ".xlsx")
        {
            return BadRequest(new { success = false, message = "Chỉ hỗ trợ file .xlsx" });
        }

        var objectName = $"imports/luc-luong/{Guid.NewGuid():N}{ext}";
        await using var stream = file.OpenReadStream();
        await _minioService.UploadAsync(stream, objectName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        var batchId = await _lucLuongAppService.EnqueueImportAsync(new ImportLucLuongRequestDto
        {
            ObjectName = objectName,
            OriginalFileName = file.FileName
        });

        return Ok(new { success = true, data = new { batchId } });
    }

    [Authorize]
    [HttpGet("import-progress")]
    public async Task<IActionResult> GetImportProgress([FromQuery] string batchId)
    {
        if (string.IsNullOrWhiteSpace(batchId))
        {
            return BadRequest(new { success = false, message = "Thiếu batchId" });
        }
        var progress = await _lucLuongAppService.GetImportProgressAsync(batchId);
        return Ok(new { success = true, data = progress });
    }
}
