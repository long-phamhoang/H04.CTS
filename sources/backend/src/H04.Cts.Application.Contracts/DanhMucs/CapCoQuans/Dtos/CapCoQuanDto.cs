using H04.Cts.Utilities;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class CapCoQuanDto : AuditedEntityDto<long>
{

    public string? MaCapCoQuan { get; set; }

    public string? TenCapCoQuan { get; set; }

    public TrangThai TrangThai { get; set; }

    public string? GhiChu { get; set; }
}