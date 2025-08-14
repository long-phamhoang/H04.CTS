using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class LucLuongDto : AuditedEntityDto<long>
{
    public string? TenLucLuong { get; set; }
    public string? MaLucLuong { get; set; }
    public TrangThai TrangThai { get; set; }
    public string? GhiChu { get; set; }
}
public class GetLucLuongListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
