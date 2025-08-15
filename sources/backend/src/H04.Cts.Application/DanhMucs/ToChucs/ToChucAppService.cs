using H04.Cts.DanhMucs.ChucVucs.Dtos;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.Application.DanhMucs;

[Authorize(CtsPermissions.DanhMucs.ToChuc)]
public class ToChucAppService : ApplicationService, IToChucAppService
{
    private readonly IRepository<ToChuc, long> _repository;

    public ToChucAppService(IRepository<ToChuc, long> repository)
    {
        _repository = repository;
    }

    public async Task<ToChucDto> GetAsync(long id)
    {
        var book = await _repository.GetAsync(id);
        return ObjectMapper.Map<ToChuc, ToChucDto>(book);
    }

    public async Task<PagedResultDto<ToChucDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "TenToChuc" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var books = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<ToChucDto>(
            totalCount,
            ObjectMapper.Map<List<ToChuc>, List<ToChucDto>>(books)
        );
    }

    [Authorize(CtsPermissions.DanhMucs.ToChucCreate)]
    public async Task<ToChucDto> CreateAsync(CreateUpdateToChucDto input)
    {
        var book = ObjectMapper.Map<CreateUpdateToChucDto, ToChuc>(input);
        await _repository.InsertAsync(book);
        return ObjectMapper.Map<ToChuc, ToChucDto>(book);
    }

    [Authorize(CtsPermissions.DanhMucs.ToChucEdit)]
    public async Task<ToChucDto> UpdateAsync(long id, CreateUpdateToChucDto input)
    {
        var book = await _repository.GetAsync(id);
        ObjectMapper.Map(input, book);
        await _repository.UpdateAsync(book);
        return ObjectMapper.Map<ToChuc, ToChucDto>(book);
    }

    [Authorize(CtsPermissions.DanhMucs.ToChucDelete)]
    public async Task SoftDeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    [Authorize(CtsPermissions.DanhMucs.ToChucDelete)]
    public async Task DeleteAsync(long id)
    {
        var entity = await _repository.GetAsync(id);
        await _repository.HardDeleteAsync(entity);
    }

    [AllowAnonymous]
    public async Task<List<ToChucDto>> GetToChucForDropDown()
    {
        var queryable = await _repository.GetQueryableAsync();


        return queryable
            .Where(x => x.TrangThai == Utilities.TrangThai.HoatDong && !x.IsDeleted)
            .Select(x => new ToChucDto
            {
                Id = x.Id,
                TenToChuc = x.TenToChuc
            }).ToList();
    }
}