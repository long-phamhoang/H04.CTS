using H04.Cts.Utilities;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class TrichYeuDto : AuditedEntityDto<long>
{
    public required string MaTrichYeu { get; set; }
    public required string TenTrichYeu { get; set; }
    public required TrangThai TrangThai { get; set; }
    public string? MoTa { get; set; }
    public string? GhiChu { get; set; }
}