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
public class MangCTSAppService : ApplicationService, IMangCTSAppService
{
    private readonly IMangCTSRepository _repository;

    public MangCTSAppService(IMangCTSRepository repository)
    {
        _repository = repository;
    }

    public async Task<MangCTSDto> CreateAsync(CreateUpdateMangCTSDto input)
    {
        var exists = await _repository.EnsureExistsByMaMangCTSAsync(input.MaMangCTS);
        if (exists)
        {
            throw new UserFriendlyException($"Entity with MaMangCTS = {input.MaMangCTS} already exists", "App:BadRequest");
        }
        var mangCTS = ObjectMapper.Map<CreateUpdateMangCTSDto, MangCTS>(input);
        await _repository.InsertAsync(mangCTS);
        return ObjectMapper.Map<MangCTS, MangCTSDto>(mangCTS);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> EnsureExistsByMaMangCTSAsync(string MaMangCTS)
    {
        using (_repository.DisableTracking())
        {
            return await _repository.EnsureExistsByMaMangCTSAsync(MaMangCTS);
        }
    }

    public async Task<MangCTSDto> GetAsync(long id)
    {
        using (_repository.DisableTracking())
        {
            var mangCTS = await _repository.GetAsync(id);
            return ObjectMapper.Map<MangCTS, MangCTSDto>(mangCTS);
        }
    }

    public async Task<PagedResultDto<MangCTSDto>> GetListAsync(GetListMangCTSDto input)
    {
        using (_repository.DisableTracking())
        {
            var queryable = await _repository.GetQueryableAsync();
            
            if (!string.IsNullOrWhiteSpace(input.FilterInput)) {
                queryable = queryable.Where(x =>
                    x.MaMangCTS.ToLower().Contains(input.FilterInput.ToLower()) ||
                    x.TenMangCTS.ToLower().Contains(input.FilterInput.ToLower())
                );
            }

            var query = queryable
                .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Id" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var list = await AsyncExecuter.ToListAsync(query);
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            return new PagedResultDto<MangCTSDto>(
                totalCount,
                ObjectMapper.Map<List<MangCTS>, List<MangCTSDto>>(list)
            );
        }
    }

    public async Task<MangCTSDto> UpdateAsync(long id, CreateUpdateMangCTSDto input)
    {
        var mangCTS = await _repository.GetAsync(id);
        var exists = await _repository.EnsureExistsByMaMangCTSAsync(input.MaMangCTS);
        if (exists && !mangCTS.MaMangCTS.Equals(input.MaMangCTS))
        {
            throw new UserFriendlyException($"Entity with MaMangCTS = {input.MaMangCTS} already exists", "App:BadRequest");
        }
        ObjectMapper.Map<CreateUpdateMangCTSDto, MangCTS>(input, mangCTS);
        await _repository.UpdateAsync(mangCTS);
        return ObjectMapper.Map<MangCTS, MangCTSDto>(mangCTS);
    }
}
