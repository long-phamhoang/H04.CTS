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
public class TrichYeuAppService : ApplicationService, ITrichYeuAppService
{
    private readonly ITrichYeuRepository _repository;

    public TrichYeuAppService(ITrichYeuRepository repository)
    {
        _repository = repository;
    }

    public async Task<TrichYeuDto> CreateAsync(CreateUpdateTrichYeuDto input)
    {
        var exists = await _repository.EnsureExistsByMaTrichYeuAsync(input.MaTrichYeu);
        if (exists)
        {
            throw new UserFriendlyException($"Entity with MaTrichYeu = {input.MaTrichYeu} already exists", "App:BadRequest");
        }
        var trichYeu = ObjectMapper.Map<CreateUpdateTrichYeuDto, TrichYeu>(input);
        var new_trichYeu = await _repository.InsertAsync(trichYeu);
        return ObjectMapper.Map<TrichYeu, TrichYeuDto>(new_trichYeu);
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

    public async Task<bool> EnsureExistsByMaTrichYeuAsync(string MaTrichYeu)
    {
        using (_repository.DisableTracking())
        {
            return await _repository.EnsureExistsByMaTrichYeuAsync(MaTrichYeu);
        }
    }

    public async Task<TrichYeuDto> GetAsync(long id)
    {
        using (_repository.DisableTracking())
        {
            var trichYeu = await _repository.GetAsync(id);
            return ObjectMapper.Map<TrichYeu, TrichYeuDto>(trichYeu);
        }
    }

    public async Task<PagedResultDto<TrichYeuDto>> GetListAsync(GetListTrichYeuDto input)
    {
        using (_repository.DisableTracking())
        {
            var queryable = await _repository.GetQueryableAsync();

            if (!string.IsNullOrWhiteSpace(input.FilterInput)) {
                queryable = queryable.Where(x =>
                    x.MaTrichYeu.ToLower().Contains(input.FilterInput.ToLower()) ||
                    x.TenTrichYeu.ToLower().Contains(input.FilterInput.ToLower())
                );
            }

            var query = queryable
                .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Id" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var list = await AsyncExecuter.ToListAsync(query);
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            return new PagedResultDto<TrichYeuDto>(
                totalCount,
                ObjectMapper.Map<List<TrichYeu>, List<TrichYeuDto>>(list)
            );
        }
    }

    public async Task<TrichYeuDto> UpdateAsync(long id, CreateUpdateTrichYeuDto input)
    {
        var trichYeu = await _repository.GetAsync(id);
        var exists = await _repository.EnsureExistsByMaTrichYeuAsync(input.MaTrichYeu);
        if (exists && !trichYeu.MaTrichYeu.Equals(input.MaTrichYeu))
        {
            throw new UserFriendlyException($"Entity with MaTrichYeu = {input.MaTrichYeu} already exists", "App:BadRequest");
        }
        ObjectMapper.Map<CreateUpdateTrichYeuDto, TrichYeu>(input, trichYeu);
        await _repository.UpdateAsync(trichYeu);
        return ObjectMapper.Map<TrichYeu, TrichYeuDto>(trichYeu);
    }

    public async Task MultipleDeleteAsync(List<long> ids)
    {
        throw new NotImplementedException();
    }

    public async Task MultipleCreateAsync(List<CreateUpdateTrichYeuDto> inputs)
    {
        throw new NotImplementedException();
    }
}