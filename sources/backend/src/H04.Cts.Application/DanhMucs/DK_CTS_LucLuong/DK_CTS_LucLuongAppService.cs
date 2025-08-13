using H04.Cts.DanhMucs.DieuKienCapCTSTheoLL.Dto;
using H04.Cts.DanhMucs.DieuKienCapCTSTheoLL.Interfaces;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.DanhMucs.DieuKienCapCTSTheoLL;

[Authorize(CtsPermissions.DanhMucs.DK_CTS_LucLuong)]
public class DkCtsLucLuongAppService : ApplicationService, IDkCtsLucLuongAppService
{
    private readonly IRepository<Entities.DanhMucs.DieuKienCapCTSTheoLL, long> _repository;
    public DkCtsLucLuongAppService(IRepository<Entities.DanhMucs.DieuKienCapCTSTheoLL, long> repository)
    {
        _repository = repository;
    }


    public async Task<DieuKienCapCTSTheoLLDto> GetAsync(long id)
    {
        var entity = await _repository.GetAsync(id);
        return ObjectMapper.Map<Entities.DanhMucs.DieuKienCapCTSTheoLL, DieuKienCapCTSTheoLLDto>(entity);
    }


    public async Task<PagedResultDto<DieuKienCapCTSTheoLLDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = queryable
            .OrderBy(string.IsNullOrWhiteSpace(input.Sorting) ? "TenDieuKien" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var items = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<DieuKienCapCTSTheoLLDto>(
            totalCount,
            ObjectMapper.Map<List<Entities.DanhMucs.DieuKienCapCTSTheoLL>, List<DieuKienCapCTSTheoLLDto>>(items)
        );
    }

    [Authorize(CtsPermissions.DanhMucs.DK_CTS_LucLuongCreate)]

    public async Task<DieuKienCapCTSTheoLLDto> CreateAsync(DieuKienCapCTSTheoLL_CreateUpdateDto input)
    {
        var exists = await _repository.AnyAsync(x => x.MaDieuKien == input.MaDieuKien);
        if (exists)
        {
            throw new Volo.Abp.UserFriendlyException("Mã điều kiện đã tồn tại");
        }

        var entity = ObjectMapper.Map<DieuKienCapCTSTheoLL_CreateUpdateDto, Entities.DanhMucs.DieuKienCapCTSTheoLL>(input);
        await _repository.InsertAsync(entity);
        return ObjectMapper.Map<Entities.DanhMucs.DieuKienCapCTSTheoLL, DieuKienCapCTSTheoLLDto>(entity);
    }

    [Authorize(CtsPermissions.DanhMucs.DK_CTS_LucLuongEdit)]

    public async Task<DieuKienCapCTSTheoLLDto> UpdateAsync(long id, DieuKienCapCTSTheoLL_CreateUpdateDto input)
    {
        var exists = await _repository.AnyAsync(x => x.Id != id && x.MaDieuKien == input.MaDieuKien);
        if (exists)
        {
            throw new Volo.Abp.UserFriendlyException("Mã điều kiện đã tồn tại");
        }

        var entity = await _repository.GetAsync(id);
        ObjectMapper.Map(input, entity);
        await _repository.UpdateAsync(entity);
        return ObjectMapper.Map<Entities.DanhMucs.DieuKienCapCTSTheoLL, DieuKienCapCTSTheoLLDto>(entity);
    }

    [Authorize(CtsPermissions.DanhMucs.DK_CTS_LucLuongDelete)]

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}


