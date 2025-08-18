using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Entities;
using Volo.Abp;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Dtos.DanhMucs;

namespace H04.Cts.Application.DanhMucs;

public class NoiCapCCCDAppService : ApplicationService, INoiCapCCCDAppService
{
    private readonly IRepository<NoiCapCCCD, long> _repository;

    public NoiCapCCCDAppService(IRepository<NoiCapCCCD, long> repository)
    {
        _repository = repository;
    }

    public async Task<NoiCapCCCDDto> GetAsync(long id)
    {
        var queryable = await _repository.GetQueryableAsync();
        var noiCapCCCD = await AsyncExecuter.FirstOrDefaultAsync(
            queryable.Where(x => x.Id == id && !x.IsDeleted)
        );
        if (noiCapCCCD == null)
        {
            throw new EntityNotFoundException(typeof(NoiCapCCCD), id);
        }
        return ObjectMapper.Map<NoiCapCCCD, NoiCapCCCDDto>(noiCapCCCD);
    }

    public async Task<PagedResultDto<NoiCapCCCDDto>> GetListAsync(GetNoiCapCCCDListDto input)
    {
        var queryable = await _repository.GetQueryableAsync();

        // Exclude soft-deleted records by default
        queryable = queryable.Where(x => !x.IsDeleted);

        // Only use keyword filter for all searchable fields
        if (!string.IsNullOrWhiteSpace(input.Keyword))
        {
            var keyword = input.Keyword.Trim().ToLower();
            queryable = queryable.Where(x =>
                (x.Name != null && x.Name.ToLower().Contains(keyword)) ||
                (x.Code != null && x.Code.ToLower().Contains(keyword)) ||
                (x.Abbreviation != null && x.Abbreviation.ToLower().Contains(keyword)) ||
                (x.Address != null && x.Address.ToLower().Contains(keyword)) ||
                (x.Province != null && x.Province.ToLower().Contains(keyword)) ||
                (x.Note != null && x.Note.ToLower().Contains(keyword))
            );
        }

        // Per-field filters
        if (!string.IsNullOrWhiteSpace(input.Name))
        {
            var value = input.Name.Trim().ToLower();
            queryable = queryable.Where(x => x.Name != null && x.Name.ToLower().Contains(value));
        }
        if (!string.IsNullOrWhiteSpace(input.Code))
        {
            var value = input.Code.Trim().ToLower();
            queryable = queryable.Where(x => x.Code != null && x.Code.ToLower().Contains(value));
        }
        if (!string.IsNullOrWhiteSpace(input.Abbreviation))
        {
            var value = input.Abbreviation.Trim().ToLower();
            queryable = queryable.Where(x => x.Abbreviation != null && x.Abbreviation.ToLower().Contains(value));
        }
        if (!string.IsNullOrWhiteSpace(input.Address))
        {
            var value = input.Address.Trim().ToLower();
            queryable = queryable.Where(x => x.Address != null && x.Address.ToLower().Contains(value));
        }
        if (!string.IsNullOrWhiteSpace(input.Province))
        {
            var value = input.Province.Trim().ToLower();
            queryable = queryable.Where(x => x.Province != null && x.Province.ToLower().Contains(value));
        }
        if (!string.IsNullOrWhiteSpace(input.Note))
        {
            var value = input.Note.Trim().ToLower();
            queryable = queryable.Where(x => x.Note != null && x.Note.ToLower().Contains(value));
        }
        if (input.IsActive.HasValue)
        {
            queryable = queryable.Where(x => x.IsActive == input.IsActive.Value);
        }

        var query = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Name" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var noiCapCCCDs = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<NoiCapCCCDDto>(
            totalCount,
            ObjectMapper.Map<List<NoiCapCCCD>, List<NoiCapCCCDDto>>(noiCapCCCDs)
        );
    }

    public async Task<NoiCapCCCDDto> CreateAsync(CreateUpdateNoiCapCCCDDto input)
    {
        var normalizedName = input.Name?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(normalizedName))
        {
            throw new UserFriendlyException("Tên không được để trống.");
        }

        var exists = await _repository.AnyAsync(x => x.Name == normalizedName);
        if (exists)
        {
            throw new UserFriendlyException($"Tên '{normalizedName}' đã tồn tại.");
        }

        var noiCapCCCD = ObjectMapper.Map<CreateUpdateNoiCapCCCDDto, NoiCapCCCD>(input);
        noiCapCCCD.Name = normalizedName;
        await _repository.InsertAsync(noiCapCCCD);
        return ObjectMapper.Map<NoiCapCCCD, NoiCapCCCDDto>(noiCapCCCD);
    }

    public async Task<NoiCapCCCDDto> UpdateAsync(long id, CreateUpdateNoiCapCCCDDto input)
    {
        var noiCapCCCD = await _repository.GetAsync(id);
        var normalizedName = input.Name?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(normalizedName))
        {
            throw new UserFriendlyException("Tên không được để trống.");
        }

        var duplicate = await _repository.AnyAsync(x => x.Id != id && x.Name == normalizedName);
        if (duplicate)
        {
            throw new UserFriendlyException($"Tên '{normalizedName}' đã tồn tại.");
        }

        ObjectMapper.Map(input, noiCapCCCD);
        noiCapCCCD.Name = normalizedName;
        await _repository.UpdateAsync(noiCapCCCD);
        return ObjectMapper.Map<NoiCapCCCD, NoiCapCCCDDto>(noiCapCCCD);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task DeleteManyAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0)
        {
            return;
        }
        foreach (var id in ids.Distinct())
        {
            await _repository.DeleteAsync(id);
        }
    }
}
