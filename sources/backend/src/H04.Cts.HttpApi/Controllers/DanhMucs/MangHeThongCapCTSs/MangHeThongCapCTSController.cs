using System;
using System.Threading.Tasks;
using H04.Cts.Application.DanhMucs;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace H04.Cts.Controllers.DanhMucs;

[Route("api/app/mang-he-thong-cap-cts")]
[ApiController]
[Authorize]
public class MangHeThongCapCTSController : AbpController
{
    private readonly IMangHeThongCapCTSAppService _service;

    public MangHeThongCapCTSController(IMangHeThongCapCTSAppService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(CtsPermissions.DanhMucs.MangHeThongCapCTSCreate)]
    public Task<MangHeThongCapCTSDto> CreateAsync(CreateUpdateMangHeThongCapCTSDto input)
    {
        return _service.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    [Authorize(CtsPermissions.DanhMucs.MangHeThongCapCTSDelete)]
    public Task DeleteAsync(long id)
    {
        return _service.DeleteAsync(id);
    }

    [HttpDelete("{id}/soft-delete")]
    [Authorize(CtsPermissions.DanhMucs.MangHeThongCapCTSDelete)]
    public Task SoftDeleteAsync(long id)
    {
        return _service.SoftDeleteAsync(id);
    }

    [HttpPut("check-ma/{MaMangCTS}")]
    public Task<bool> EnsureExistsByMaMangHeThongCapCTSAsync(string MaMangCTS)
    {
        return _service.EnsureExistsByMaMangHeThongCapCTSAsync(MaMangCTS);
    }

    [HttpGet("{id}")]
    public Task<MangHeThongCapCTSDto> GetAsync(long id)
    {
        return _service.GetAsync(id);
    }

    [HttpGet]
    public Task<PagedResultDto<MangHeThongCapCTSDto>> GetListAsync([FromQuery] GetListMangHeThongCapCTSDto input)
    {
        return _service.GetListAsync(input);
    }

    [HttpPut("{id}")]
    [Authorize(CtsPermissions.DanhMucs.MangHeThongCapCTSEdit)]
    public Task<MangHeThongCapCTSDto> UpdateAsync(long id, CreateUpdateMangHeThongCapCTSDto input)
    {
        return _service.UpdateAsync(id, input);
    }
}
