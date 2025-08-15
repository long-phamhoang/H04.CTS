using H04.Cts.DanhMucs;
using H04.Cts.DanhMucs.CtsVaThietBis.Dtos;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
namespace H04.Cts.Application.DanhMucs;

[Authorize(CtsPermissions.DanhMucs.CtsVaThietBi)]
public class CtsVaThietBiAppService : ApplicationService, ICtsVaThietBiAppService
{
    private readonly IRepository<CtsVaThietBi, long> _repository;

    public CtsVaThietBiAppService(IRepository<CtsVaThietBi, long> repository)
    {
        _repository = repository;
    }
    #region Crud_CtsVaThietBi
    #region Get
    public async Task<CtsVaThietBiDto> GetAsync(long id)
    {
        var book = await _repository.GetAsync(id);
        return ObjectMapper.Map<CtsVaThietBi, CtsVaThietBiDto>(book);
    }
    #endregion

    #region List
    public async Task<PagedResultDto<CtsVaThietBiDto>> GetListAsync(SearchCtsSVaThietBiInput input)
    {
        var queryable = await _repository.GetQueryableAsync();

        if (!input.Filter.IsNullOrWhiteSpace())
        {
            queryable = queryable.Where(x => (x.TenCts != null && x.TenCts.ToLower().Contains(input.Filter.ToLower()))
                                          || (x.SoHieuCts != null && x.SoHieuCts.ToLower().Contains(input.Filter.ToLower())));
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

        return new PagedResultDto<CtsVaThietBiDto>(
            totalCount,
            ObjectMapper.Map<List<CtsVaThietBi>, List<CtsVaThietBiDto>>(books)
        );
    }
    #endregion

    #region Create
    [Authorize(CtsPermissions.DanhMucs.CtsVaThietBiCreate)]
    public async Task<CtsVaThietBiDto> CreateAsync(CreateUpdateCtsVaThietBiDto input)
    {
        if (await _repository.AnyAsync(x => x.SoHieuCts == input.SoHieuCts))
        {
            throw new UserFriendlyException(CtsDomainErrorCodes.DuplicateCode);
        }
        var book = ObjectMapper.Map<CreateUpdateCtsVaThietBiDto, CtsVaThietBi>(input);
        await _repository.InsertAsync(book);
        return ObjectMapper.Map<CtsVaThietBi, CtsVaThietBiDto>(book);
    }
    #endregion

    #region Update
    [Authorize(CtsPermissions.DanhMucs.CtsVaThietBiEdit)]
    public async Task<CtsVaThietBiDto> UpdateAsync(long id, CreateUpdateCtsVaThietBiDto input)
    {
        if (await _repository.AnyAsync(x => x.SoHieuCts == input.SoHieuCts && x.Id != id))
        {
            throw new UserFriendlyException(CtsDomainErrorCodes.DuplicateCode);
        }
        var book = await _repository.GetAsync(id);
        ObjectMapper.Map(input, book);
        await _repository.UpdateAsync(book);
        return ObjectMapper.Map<CtsVaThietBi, CtsVaThietBiDto>(book);
    }
    #endregion

    #region SoftDelete
    [Authorize(CtsPermissions.DanhMucs.CtsVaThietBiDelete)]
    public async Task SoftDeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
    #endregion

    #region HardDelete
    [Authorize(CtsPermissions.DanhMucs.CtsVaThietBiDelete)]
    public async Task DeleteAsync(long id)
    {
        var entity = await _repository.GetAsync(id);
        await _repository.HardDeleteAsync(entity);
    }
    #endregion
    #endregion
}