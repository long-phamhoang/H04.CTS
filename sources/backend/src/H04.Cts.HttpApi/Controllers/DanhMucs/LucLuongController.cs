using H04.Cts.Blob;
using H04.Cts.DanhMucs.Minio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.Controllers.DanhMucs;

[RemoteService(Name = "Default")]
[Area("app")]
[Route("api/app/luc-luong")]
public class LucLuongController : CtsController
{
    private readonly MinioService _minioService;


    public LucLuongController(MinioService minioService)
    {
        _minioService = minioService;
    }

    [AllowAnonymous]
    [HttpGet("template/{fileName}")]

    public async Task<IActionResult> DownloadTemplateAsync( string fileName)
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
}
