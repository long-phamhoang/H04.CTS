using H04.Cts.DanhMucs.Dtos;
using H04.Cts.DanhMucs.ThueBaoCaNhans.Dtos;
using H04.Cts.DanhMucs.ThueBaoCaNhans.Interfaces;
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
using Microsoft.EntityFrameworkCore;

namespace H04.Cts.DanhMucs.ThueBaoCaNhans;

[Authorize(CtsPermissions.DanhMucs.ThueBaoCaNhan)]
public class ThueBaoCaNhanAppService : ApplicationService, IThueBaoCaNhanAppService
{
    private readonly IRepository<ThueBaoCaNhan, long> _repository;

    public ThueBaoCaNhanAppService(IRepository<ThueBaoCaNhan, long> repository)
    {
        _repository = repository;
    }

    public async Task<ThueBaoCaNhanDto> GetAsync(long id)
    {
        var book = await _repository.GetAsync(id);
        return ObjectMapper.Map<ThueBaoCaNhan, ThueBaoCaNhanDto>(book);
    }

    public async Task<PagedResultDto<ThueBaoCaNhanDto>> GetListAsync(GetListThueBaoCaNhanInput input)
    {
        var queryable = await _repository.GetQueryableAsync();
        queryable = queryable
        .Include(x => x.ChucVuFk).Include(x => x.ToChucFk);
        // filter
        if (!string.IsNullOrWhiteSpace(input.FilterInput))
        {
            queryable = queryable.Where(x =>
                x.HoTen.Contains(input.FilterInput) || x.ChucVuFk.TenChucVu.Contains(input.FilterInput));
        }

        // Sắp xếp
        queryable = queryable.OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "HoTen" : input.Sorting);

        // Lấy tổng số bản ghi sau khi filter
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        // Lấy dữ liệu theo phân trang
        var items = await AsyncExecuter.ToListAsync(queryable
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount));

        return new PagedResultDto<ThueBaoCaNhanDto>(
            totalCount,
            ObjectMapper.Map<List<ThueBaoCaNhan>, List<ThueBaoCaNhanDto>>(items)
        );
    }

    [Authorize(CtsPermissions.DanhMucs.ThueBaoCaNhanCreate)]
    public async Task<ThueBaoCaNhanDto> CreateAsync(CreateUpdateThueBaoCaNhanDto input)
    {
        var book = ObjectMapper.Map<CreateUpdateThueBaoCaNhanDto, ThueBaoCaNhan>(input);
        await _repository.InsertAsync(book);
        return ObjectMapper.Map<ThueBaoCaNhan, ThueBaoCaNhanDto>(book);
    }

    [Authorize(CtsPermissions.DanhMucs.ThueBaoCaNhanEdit)]
    public async Task<ThueBaoCaNhanDto> UpdateAsync(long id, CreateUpdateThueBaoCaNhanDto input)
    {
        var book = await _repository.GetAsync(id);
        ObjectMapper.Map(input, book);
        await _repository.UpdateAsync(book);
        return ObjectMapper.Map<ThueBaoCaNhan, ThueBaoCaNhanDto>(book);
    }

    [Authorize(CtsPermissions.DanhMucs.ThueBaoCaNhanDelete)]
    public async Task SoftDeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    [Authorize(CtsPermissions.DanhMucs.ThueBaoCaNhanDelete)]
    public async Task DeleteAsync(long id)
    {
        var entity = await _repository.GetAsync(id);
        await _repository.HardDeleteAsync(entity);
    }
}

