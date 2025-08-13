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

[Route("api/app/mang-cts")]
[ApiController]
[Authorize]
public class MangCTSController : AbpController
{
    private readonly IMangCTSAppService _service;

    public MangCTSController(IMangCTSAppService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(CtsPermissions.DanhMucs.MangCTSCreate)]
    public Task<MangCTSDto> CreateAsync(CreateUpdateMangCTSDto input)
    {
        return _service.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    [Authorize(CtsPermissions.DanhMucs.MangCTSDelete)]
    public Task DeleteAsync(long id)
    {
        return _service.DeleteAsync(id);
    }

    [HttpPut("check-ma/{MaMangCTS}")]
    public Task<bool> EnsureExistsByMaMangCTSAsync(string MaMangCTS)
    {
        return _service.EnsureExistsByMaMangCTSAsync(MaMangCTS);
    }

    [HttpGet("{id}")]
    public Task<MangCTSDto> GetAsync(long id)
    {
        return _service.GetAsync(id);
    }

    [HttpGet]
    public Task<PagedResultDto<MangCTSDto>> GetListAsync([FromQuery] GetListMangCTSDto input)
    {
        return _service.GetListAsync(input);
    }

    [HttpPut("{id}")]
    [Authorize(CtsPermissions.DanhMucs.MangCTSEdit)]
    public Task<MangCTSDto> UpdateAsync(long id, CreateUpdateMangCTSDto input)
    {
        return _service.UpdateAsync(id, input);
    }
}
