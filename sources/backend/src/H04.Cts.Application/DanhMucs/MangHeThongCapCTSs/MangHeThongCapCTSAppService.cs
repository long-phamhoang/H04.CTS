using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Repositories.DanhMucs;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.Application.DanhMucs;

[RemoteService(IsEnabled = false)]
public class MangHeThongCapCTSAppService : ApplicationService, IMangHeThongCapCTSAppService
{
    private readonly IMangHeThongCapCTSRepository _repository;

    public MangHeThongCapCTSAppService(IMangHeThongCapCTSRepository repository)
    {
        _repository = repository;
    }

    public async Task<MangHeThongCapCTSDto> CreateAsync(CreateUpdateMangHeThongCapCTSDto input)
    {
        var exists = await _repository.EnsureExistsByMaMangHeThongCapCTSAsync(input.MaMangHeThongCapCTS);
        if (exists)
        {
            throw new UserFriendlyException($"Entity with MaMangHeThongCapCTS = {input.MaMangHeThongCapCTS} already exists", "App:BadRequest");
        }
        var mangCTS = ObjectMapper.Map<CreateUpdateMangHeThongCapCTSDto, MangHeThongCapCTS>(input);
        await _repository.InsertAsync(mangCTS);
        return ObjectMapper.Map<MangHeThongCapCTS, MangHeThongCapCTSDto>(mangCTS);
    }

    public async Task SoftDeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _repository.GetAsync(id);
        await _repository.HardDeleteAsync(entity);
    }

    public async Task<bool> EnsureExistsByMaMangHeThongCapCTSAsync(string MaMangHeThongCapCTS)
    {
        using (_repository.DisableTracking())
        {
            return await _repository.EnsureExistsByMaMangHeThongCapCTSAsync(MaMangHeThongCapCTS);
        }
    }

    public async Task<MangHeThongCapCTSDto> GetAsync(long id)
    {
        using (_repository.DisableTracking())
        {
            var mangCTS = await _repository.GetAsync(id);
            return ObjectMapper.Map<MangHeThongCapCTS, MangHeThongCapCTSDto>(mangCTS);
        }
    }

    public async Task<PagedResultDto<MangHeThongCapCTSDto>> GetListAsync(GetListMangHeThongCapCTSDto input)
    {
        using (_repository.DisableTracking())
        {
            var queryable = await _repository.GetQueryableAsync();
            
            if (!string.IsNullOrWhiteSpace(input.FilterInput)) {
                queryable = queryable.Where(x =>
                    x.MaMangHeThongCapCTS.ToLower().Contains(input.FilterInput.ToLower()) ||
                    x.TenMangHeThongCapCTS.ToLower().Contains(input.FilterInput.ToLower())
                );
            }

            var query = queryable
                .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Id" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var list = await AsyncExecuter.ToListAsync(query);
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            return new PagedResultDto<MangHeThongCapCTSDto>(
                totalCount,
                ObjectMapper.Map<List<MangHeThongCapCTS>, List<MangHeThongCapCTSDto>>(list)
            );
        }
    }

    public async Task<MangHeThongCapCTSDto> UpdateAsync(long id, CreateUpdateMangHeThongCapCTSDto input)
    {
        var mangCTS = await _repository.GetAsync(id);
        var exists = await _repository.EnsureExistsByMaMangHeThongCapCTSAsync(input.MaMangHeThongCapCTS);
        if (exists && !mangCTS.MaMangHeThongCapCTS.Equals(input.MaMangHeThongCapCTS))
        {
            throw new UserFriendlyException($"Entity with MaMangHeThongCapCTS = {input.MaMangHeThongCapCTS} already exists", "App:BadRequest");
        }
        ObjectMapper.Map<CreateUpdateMangHeThongCapCTSDto, MangHeThongCapCTS>(input, mangCTS);
        await _repository.UpdateAsync(mangCTS);
        return ObjectMapper.Map<MangHeThongCapCTS, MangHeThongCapCTSDto>(mangCTS);
    }

    public Task MultipleDeleteAsync(List<long> ids)
    {
        throw new NotImplementedException();
    }

    public Task MultipleCreateAsync(List<CreateUpdateTrichYeuDto> inputs)
    {
        throw new NotImplementedException();
    }
}
