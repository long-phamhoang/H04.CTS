using H04.Cts.Dtos.DanhMucs;
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

namespace H04.Cts.Application.DanhMucs;

[Authorize(CtsPermissions.DanhMucs.LucLuong)]
public class LucLuongAppService : ApplicationService, ILucLuongAppService
{
    private readonly IRepository<LucLuong, long> _repository;

    public LucLuongAppService(IRepository<LucLuong, long> repository)
    {
        _repository = repository;
    }

    public async Task<LucLuongDto> GetAsync(long id)
    {
        var entity = await _repository.GetAsync(id);
        return ObjectMapper.Map<LucLuong, LucLuongDto>(entity);
    }

    public async Task<PagedResultDto<LucLuongDto>> GetListAsync(GetLucLuongListDto input)
    {
        var queryable = await _repository.GetQueryableAsync();

        if (!string.IsNullOrWhiteSpace(input.Filter))
        {
            var f = input.Filter.ToLower();
            queryable = queryable.Where(x =>
                ((x.TenLucLuong ?? "").ToLower().Contains(f)) ||
                ((x.MaLucLuong ?? "").ToLower().Contains(f))
            );
        }

        var totalCount = await AsyncExecuter.CountAsync(queryable);

        var query = queryable
            .OrderBy(string.IsNullOrWhiteSpace(input.Sorting) ? "TenLucLuong" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var items = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<LucLuongDto>(
            totalCount,
            ObjectMapper.Map<List<LucLuong>, List<LucLuongDto>>(items)
        );
    }

    [Authorize(CtsPermissions.DanhMucs.LucLuongCreate)]
    public async Task<LucLuongDto> CreateAsync(CreateUpdateLucLuongDto input)
    {
        //check mã tồn tại
        var exists = await _repository.AnyAsync(x => x.MaLucLuong == input.MaLucLuong);
        if (exists)
        {
            throw new Volo.Abp.UserFriendlyException("Mã lực lượng đã tồn tại");
        }

        var entity = ObjectMapper.Map<CreateUpdateLucLuongDto, LucLuong>(input);
        await _repository.InsertAsync(entity);
        return ObjectMapper.Map<LucLuong, LucLuongDto>(entity);
    }

    [Authorize(CtsPermissions.DanhMucs.LucLuongEdit)]
    public async Task<LucLuongDto> UpdateAsync(long id, CreateUpdateLucLuongDto input)
    {

        //check mã tồn tại
        var exists = await _repository.AnyAsync(x => x.Id != id && x.MaLucLuong == input.MaLucLuong);
        if (exists)
        {
            throw new Volo.Abp.UserFriendlyException("Mã lực lượng đã tồn tại");
        }

        var entity = await _repository.GetAsync(id);
        ObjectMapper.Map(input, entity);
        await _repository.UpdateAsync(entity);
        return ObjectMapper.Map<LucLuong, LucLuongDto>(entity);
    }

    [Authorize(CtsPermissions.DanhMucs.LucLuongDelete)]
    public async Task SoftDeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    [Authorize(CtsPermissions.DanhMucs.LucLuongDelete)]
    public async Task DeleteAsync(long id)
    {
        var entity = await _repository.GetAsync(id);
        await _repository.HardDeleteAsync(entity);
    }



}
