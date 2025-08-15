using H04.Cts.DanhMucs.CapCoQuans.Dtos;
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

[Authorize(CtsPermissions.DanhMucs.CapCoQuan)]
public class CapCoQuanAppService : ApplicationService, ICapCoQuanAppService
{
    private readonly IRepository<CapCoQuan, long> _repository;

    public CapCoQuanAppService(IRepository<CapCoQuan, long> repository)
    {
        _repository = repository;
    }
    #region Crud_CapCoQuan
    #region Get
    public async Task<CapCoQuanDto> GetAsync(long id)
    {
        var book = await _repository.GetAsync(id);
        return ObjectMapper.Map<CapCoQuan, CapCoQuanDto>(book);
    }
    #endregion

    #region List
    public async Task<PagedResultDto<CapCoQuanDto>> GetListAsync(ListCapCoQuanRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();

        if (!input.FilterString.IsNullOrWhiteSpace())
        {
            queryable = queryable.Where(x => (x.TenCapCoQuan != null && x.TenCapCoQuan.ToLower().Contains(input.FilterString.ToLower()))
                                          || (x.MaCapCoQuan != null && x.MaCapCoQuan.ToLower().Contains(input.FilterString.ToLower())));
        }
        if (!input.Sorting.IsNullOrWhiteSpace())
            queryable = queryable.OrderBy(input.Sorting)
                                 .ThenByDescending(x => x.LastModificationTime != null ? x.LastModificationTime : x.CreationTime);
        else
            queryable = queryable.OrderByDescending(x => x.LastModificationTime != null ? x.LastModificationTime : x.CreationTime);

        var query = queryable
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var books = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<CapCoQuanDto>(
            totalCount,
            ObjectMapper.Map<List<CapCoQuan>, List<CapCoQuanDto>>(books)
        );
    }
    #endregion

    #region Create
    [Authorize(CtsPermissions.DanhMucs.CapCoQuanCreate)]
    public async Task<CapCoQuanDto> CreateAsync(CreateUpdateCapCoQuanDto input)
    {
        var book = ObjectMapper.Map<CreateUpdateCapCoQuanDto, CapCoQuan>(input);
        await _repository.InsertAsync(book);
        return ObjectMapper.Map<CapCoQuan, CapCoQuanDto>(book);
    }
    #endregion

    #region Update
    [Authorize(CtsPermissions.DanhMucs.CapCoQuanEdit)]
    public async Task<CapCoQuanDto> UpdateAsync(long id, CreateUpdateCapCoQuanDto input)
    {
        var book = await _repository.GetAsync(id);
        ObjectMapper.Map(input, book);
        await _repository.UpdateAsync(book);
        return ObjectMapper.Map<CapCoQuan, CapCoQuanDto>(book);
    }
    #endregion

    #region SoftDelete
    [Authorize(CtsPermissions.DanhMucs.CapCoQuanDelete)]
    public async Task SoftDeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
    #endregion

    #region HardDelete
    [Authorize(CtsPermissions.DanhMucs.CapCoQuanDelete)]
    public async Task DeleteAsync(long id)
    {
        var entity = await _repository.GetAsync(id);
        await _repository.HardDeleteAsync(entity);
    }
    #endregion
    #endregion
}