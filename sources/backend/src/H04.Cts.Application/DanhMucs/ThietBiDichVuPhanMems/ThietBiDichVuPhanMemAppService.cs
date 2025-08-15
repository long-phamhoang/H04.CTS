using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using H04.Cts.DanhMucs.LoaiThietBiDichVuPhanMems.Interfaces;
using H04.Cts.DanhMucs.ThietBiDichVuPhanMems.Dtos;
using H04.Cts.DanhMucs.ThietBiDichVuPhanMems.Interfaces;
using H04.Cts.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.DanhMucs.ThietBiDichVuPhanMems;

public class ThietBiDichVuPhanMemAppService : ApplicationService, IThietBiDichVuPhanMemAppService
{
    private readonly IRepository<ThietBiDichVuPhanMem, long> _repository;

    public ThietBiDichVuPhanMemAppService(IRepository<ThietBiDichVuPhanMem, long> repository)
    {
        _repository = repository;
    }

    public async Task<ThietBiDichVuPhanMemDto> GetAsync(long id)
    {
        var thietBiDVPM = await _repository.GetAsync(id);
        return ObjectMapper.Map<ThietBiDichVuPhanMem, ThietBiDichVuPhanMemDto>(thietBiDVPM);
    }

    public async Task<PagedResultDto<ThietBiDichVuPhanMemDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "TenCoQuanToChuc" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var books = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<ThietBiDichVuPhanMemDto>(
            totalCount,
            ObjectMapper.Map<List<ThietBiDichVuPhanMem>, List<ThietBiDichVuPhanMemDto>>(books)
        );
    }


    //[Authorize(CtsPermissions.DanhMucs.ThietBiDichVuPhanMemCreate)]
    public async Task<ThietBiDichVuPhanMemDto> CreateAsync(CreateUpdateThietBiDichVuPhanMemDto input)
    {
        var thietBiDVPM = ObjectMapper.Map<CreateUpdateThietBiDichVuPhanMemDto, ThietBiDichVuPhanMem>(input);
        await _repository.InsertAsync(thietBiDVPM);
        return ObjectMapper.Map<ThietBiDichVuPhanMem, ThietBiDichVuPhanMemDto>(thietBiDVPM);
    }

    //[Authorize(CtsPermissions.DanhMucs.ThietBiDichVuPhanMemEdit)]
    public async Task<ThietBiDichVuPhanMemDto> UpdateAsync(long id, CreateUpdateThietBiDichVuPhanMemDto input)
    {
        var thietBiDVPM = await _repository.GetAsync(id);
        ObjectMapper.Map(input, thietBiDVPM);
        await _repository.UpdateAsync(thietBiDVPM);
        return ObjectMapper.Map<ThietBiDichVuPhanMem, ThietBiDichVuPhanMemDto>(thietBiDVPM);
    }

    //[Authorize(CtsPermissions.DanhMucs.ThietBiDichVuPhanMemDelete)]
    public async Task SoftDeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    //[Authorize(CtsPermissions.DanhMucs.ThietBiDichVuPhanMemDelete)]
    public async Task DeleteAsync(long id)
    {
        var entity = await _repository.GetAsync(id);
        await _repository.HardDeleteAsync(entity);
    }

}

