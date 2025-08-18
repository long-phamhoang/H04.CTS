using H04.Cts.Dtos.DanhMucs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace H04.Cts.Application.DanhMucs;

public interface ILucLuongAppService : ICrudAppService<LucLuongDto, long, GetLucLuongListDto, CreateUpdateLucLuongDto>
{
    Task SoftDeleteAsync(long id);

    /// <summary>
    /// Lấy toàn bộ dữ liệu (không phân trang) phục vụ import/export theo filter và sorting.
    /// </summary>
    Task<List<LucLuongDto>> GetAllForExcelAsync(GetLucLuongAllDto input);
}
