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

        // Keyword filter across short text fields (<=256)
        if (!string.IsNullOrWhiteSpace(input.Keyword))
        {
            var keyword = input.Keyword.Trim();
            queryable = queryable.Where(x =>
                (x.Name != null && x.Name.Contains(keyword)) ||
                (x.Code != null && x.Code.Contains(keyword)) ||
                (x.Abbreviation != null && x.Abbreviation.Contains(keyword)) ||
                (x.Address != null && x.Address.Contains(keyword)) ||
                (x.Province != null && x.Province.Contains(keyword)) ||
                (x.Note != null && x.Note.Contains(keyword))
            );
        }

        // Filters
        if (!string.IsNullOrWhiteSpace(input.Name))
        {
            queryable = queryable.Where(x => x.Name.Contains(input.Name));
        }

        if (!string.IsNullOrWhiteSpace(input.Code))
        {
            queryable = queryable.Where(x => x.Code.Contains(input.Code));
        }

        if (!string.IsNullOrWhiteSpace(input.Abbreviation))
        {
            queryable = queryable.Where(x => x.Abbreviation.Contains(input.Abbreviation));
        }

        if (!string.IsNullOrWhiteSpace(input.Address))
        {
            queryable = queryable.Where(x => x.Address.Contains(input.Address));
        }

        if (!string.IsNullOrWhiteSpace(input.Province))
        {
            queryable = queryable.Where(x => x.Province.Contains(input.Province));
        }

        if (!string.IsNullOrWhiteSpace(input.Note))
        {
            queryable = queryable.Where(x => x.Note.Contains(input.Note));
        }

        if (input.IsActive.HasValue)
        {
            queryable = queryable.Where(x => x.IsActive == input.IsActive.Value);
        }

        // Intentionally not exposing deleted records in list

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
        var noiCapCCCD = await _repository.GetAsync(id);
        noiCapCCCD.IsDeleted = true;
        await _repository.UpdateAsync(noiCapCCCD);
    }
}
