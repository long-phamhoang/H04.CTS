using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using H04.Cts.DanhMucs.LoaiThietBiDichVuPhanMems.Dtos;
using H04.Cts.DanhMucs.LoaiThietBiDichVuPhanMems.Interfaces;
using H04.Cts.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.DanhMucs.LoaiThietBiDichVuPhanMems;
public class LoaiThietBiDichVuPhanMemAppService : ApplicationService, ILoaiThietBiDichVuPhanMemAppService
{
    private readonly IRepository<LoaiThietBiDichVuPhanMem, long> _repository;

    public LoaiThietBiDichVuPhanMemAppService(IRepository<LoaiThietBiDichVuPhanMem, long> repository)
    {
        _repository = repository;
    }

    public async Task<LoaiThietBiDichVuPhanMemDto> GetAsync(long id)
    {
        var thietbi = await _repository.GetAsync(id);
        return ObjectMapper.Map<LoaiThietBiDichVuPhanMem, LoaiThietBiDichVuPhanMemDto>(thietbi);
    }

    public async Task<PagedResultDto<LoaiThietBiDichVuPhanMemDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "TenLoaiThietBiDichVuPhanMem" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var listThietbi = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<LoaiThietBiDichVuPhanMemDto>(
            totalCount,
            ObjectMapper.Map<List<LoaiThietBiDichVuPhanMem>, List<LoaiThietBiDichVuPhanMemDto>>(listThietbi)
        );
    }

    //[Authorize(CtsPermissions.DanhMucs.LoaiThietBiDichVuPhanMemCreate)]
    public async Task<LoaiThietBiDichVuPhanMemDto> CreateAsync(CreateUpdateLoaiThietBiDichVuPhanMemDto input)
    {
        var thietbi = ObjectMapper.Map<CreateUpdateLoaiThietBiDichVuPhanMemDto, LoaiThietBiDichVuPhanMem>(input);
        await _repository.InsertAsync(thietbi);
        return ObjectMapper.Map<LoaiThietBiDichVuPhanMem, LoaiThietBiDichVuPhanMemDto>(thietbi);
    }

    //[Authorize(CtsPermissions.DanhMucs.LoaiThietBiDichVuPhanMemEdit)]
    public async Task<LoaiThietBiDichVuPhanMemDto> UpdateAsync(long id, CreateUpdateLoaiThietBiDichVuPhanMemDto input)
    {
        var loaiThietBi = await _repository.GetAsync(id);
        ObjectMapper.Map(input, loaiThietBi);
        await _repository.UpdateAsync(loaiThietBi);
        return ObjectMapper.Map<LoaiThietBiDichVuPhanMem, LoaiThietBiDichVuPhanMemDto>(loaiThietBi);
    }

    //[Authorize(CtsPermissions.DanhMucs.LoaiThietBiDichVuPhanMemDelete)]
    public async Task SoftDeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    //[Authorize(CtsPermissions.DanhMucs.LoaiThietBiDichVuPhanMemDelete)]
    public async Task DeleteAsync(long id)
    {
        var entity = await _repository.GetAsync(id);
        await _repository.HardDeleteAsync(entity);
    }

    public async Task<bool> CheckExistsMaSoAsync(string maSo)
    {
        if(string.IsNullOrEmpty(maSo))
        {
            return false;
        }
        return await _repository.AnyAsync(x => x.MaLoaiThietBiDichVuPhanMem!.ToLower().Equals(maSo.ToLower()));
    }

    public async Task<IList<long>> BulkDeleteAsync(IEnumerable<long> ids)
    {
        var failedIds = new List<long>();
        var entities = await _repository.GetListAsync(x => ids.Contains(x.Id));

        // Thêm Id không tồn tại vào danh sách lỗi
        var foundIds = entities.Select(x => x.Id).ToList();
        failedIds.AddRange(ids.Where(x => !foundIds.Contains(x)));
        try
        {
            await _repository.DeleteManyAsync(entities, autoSave: true); 
        }
        catch (Exception)
        {
            // Xảy ra exception khi DeleteMany sẽ xử lý xóa lần lượt
            foreach (var entity in entities) {
                try
                {
                    await _repository.DeleteAsync(entity, autoSave: true); // commit DB ngay khi xóa 
                }
                catch (Exception)
                {
                    failedIds.Add(entity.Id);
                }
            }
        }
        return failedIds;
    }
}
