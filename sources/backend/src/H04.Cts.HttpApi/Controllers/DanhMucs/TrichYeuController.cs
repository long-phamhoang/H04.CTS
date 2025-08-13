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

[Route("api/app/trich-yeu")]
[ApiController]
[Authorize]
public class TrichYeuController : AbpController
{
    private ITrichYeuAppService _service;

    public TrichYeuController(ITrichYeuAppService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(CtsPermissions.DanhMucs.TrichYeuCreate)]
    public Task<TrichYeuDto> CreateAsync(CreateUpdateTrichYeuDto input)
    {
        return _service.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    [Authorize(CtsPermissions.DanhMucs.TrichYeuDelete)]
    public Task DeleteAsync(long id)
    {
        return _service.DeleteAsync(id);
    }

    [HttpPut("check-ma/{MaTrichYeu}")]
    public Task<bool> EnsureExistsByMaTrichYeuAsync(string MaTrichYeu)
    {
        return _service.EnsureExistsByMaTrichYeuAsync(MaTrichYeu);
    }

    [HttpGet("{id}")]
    public Task<TrichYeuDto> GetAsync(long id)
    {
        return _service.GetAsync(id);
    }

    [HttpGet]
    public Task<PagedResultDto<TrichYeuDto>> GetListAsync([FromQuery] GetListTrichYeuDto input)
    {
        return _service.GetListAsync(input);
    }

    [HttpPut("{id}")]
    [Authorize(CtsPermissions.DanhMucs.TrichYeuEdit)]
    public Task<TrichYeuDto> UpdateAsync(long id, CreateUpdateTrichYeuDto input)
    {
        return _service.UpdateAsync(id, input);
    }
}
