using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.Application.DanhMucs;

[Authorize(CtsPermissions.DanhMucs.LucLuong)]
public class LucLuongAppService : ApplicationService, ILucLuongAppService
{
    private readonly IRepository<LucLuong, long> _repository;

    public LucLuongAppService(IRepository<LucLuong, long> repository)
    {
        _repository = repository;
    }

    public async Task<LucLuongDto> GetAsync(long id)
    {
        var entity = await _repository.GetAsync(id);
        return ObjectMapper.Map<LucLuong, LucLuongDto>(entity);
    }

    public async Task<PagedResultDto<LucLuongDto>> GetListAsync(GetLucLuongListDto input)
    {
        var queryable = await _repository.GetQueryableAsync();

        if (!string.IsNullOrWhiteSpace(input.Filter))
        {
            var raw = input.Filter.Trim();
            try
            {
                using var doc = JsonDocument.Parse(raw);
                var root = doc.RootElement;

                if (root.ValueKind == JsonValueKind.Object)
                {
                    string ten = root.TryGetProperty("tenLucLuong", out var tenEl) ? (tenEl.GetString() ?? string.Empty).Trim() : string.Empty;
                    string ma = root.TryGetProperty("maLucLuong", out var maEl) ? (maEl.GetString() ?? string.Empty).Trim() : string.Empty;
                    string ttStr = root.TryGetProperty("trangThai", out var ttEl) ? (ttEl.ToString() ?? string.Empty).Trim() : string.Empty;

                    if (!string.IsNullOrWhiteSpace(ten))
                    {
                        var tenLower = ten.ToLower();
                        queryable = queryable.Where(x => ((x.TenLucLuong ?? string.Empty).ToLower()).Contains(tenLower));
                    }
                    if (!string.IsNullOrWhiteSpace(ma))
                    {
                        var maLower = ma.ToLower();
                        queryable = queryable.Where(x => ((x.MaLucLuong ?? string.Empty).ToLower()).Contains(maLower));
                    }
                    if (!string.IsNullOrWhiteSpace(ttStr) && int.TryParse(ttStr, out var tt))
                    {
                        queryable = queryable.Where(x => ((int)x.TrangThai) == tt);
                    }
                }
                else
                {
                    var f2 = (root.ValueKind == JsonValueKind.String || root.ValueKind == JsonValueKind.Number)
                        ? (root.ToString() ?? string.Empty).Trim().ToLower()
                        : raw.ToLower();

                    if (!string.IsNullOrWhiteSpace(f2))
                    {
                        queryable = queryable.Where(x =>
                            ((x.TenLucLuong ?? string.Empty).ToLower().Contains(f2)) ||
                            ((x.MaLucLuong ?? string.Empty).ToLower().Contains(f2))
                        );
                    }
                }
            }
            catch (JsonException)
            {
                var f = raw.ToLower();
                queryable = queryable.Where(x =>
                    ((x.TenLucLuong ?? string.Empty).ToLower().Contains(f)) ||
                    ((x.MaLucLuong ?? string.Empty).ToLower().Contains(f))
                );
            }
        }

        var totalCount = await AsyncExecuter.CountAsync(queryable);

        var query = queryable
            .OrderBy(string.IsNullOrWhiteSpace(input.Sorting) ? "TenLucLuong" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var items = await AsyncExecuter.ToListAsync(query);

        return new PagedResultDto<LucLuongDto>(
            totalCount,
            ObjectMapper.Map<List<LucLuong>, List<LucLuongDto>>(items)
        );
    }

    [Authorize(CtsPermissions.DanhMucs.LucLuongCreate)]
    public async Task<LucLuongDto> CreateAsync(CreateUpdateLucLuongDto input)
    {
        //check mã tồn tại
        var exists = await _repository.AnyAsync(x => x.MaLucLuong == input.MaLucLuong);
        if (exists)
        {
            throw new Volo.Abp.UserFriendlyException("Mã lực lượng đã tồn tại");
        }

        var entity = ObjectMapper.Map<CreateUpdateLucLuongDto, LucLuong>(input);
        await _repository.InsertAsync(entity);
        return ObjectMapper.Map<LucLuong, LucLuongDto>(entity);
    }

    [Authorize(CtsPermissions.DanhMucs.LucLuongEdit)]
    public async Task<LucLuongDto> UpdateAsync(long id, CreateUpdateLucLuongDto input)
    {

        //check mã tồn tại
        var exists = await _repository.AnyAsync(x => x.Id != id && x.MaLucLuong == input.MaLucLuong);
        if (exists)
        {
            throw new Volo.Abp.UserFriendlyException("Mã lực lượng đã tồn tại");
        }

        var entity = await _repository.GetAsync(id);
        ObjectMapper.Map(input, entity);
        await _repository.UpdateAsync(entity);
        return ObjectMapper.Map<LucLuong, LucLuongDto>(entity);
    }

    [Authorize(CtsPermissions.DanhMucs.LucLuongDelete)]
    public async Task SoftDeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    [Authorize(CtsPermissions.DanhMucs.LucLuongDelete)]
    public async Task DeleteAsync(long id)
    {
        var entity = await _repository.GetAsync(id);
        await _repository.HardDeleteAsync(entity);
    }



}
