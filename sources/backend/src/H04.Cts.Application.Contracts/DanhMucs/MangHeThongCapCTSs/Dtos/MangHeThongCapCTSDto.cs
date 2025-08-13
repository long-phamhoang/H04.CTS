using System;
using H04.Cts.Utilities;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class MangHeThongCapCTSDto : AuditedEntityDto<long>
{
    public required string MaMangHeThongCapCTS { get; set; }
    public required string TenMangHeThongCapCTS { get; set; }
    public required TrangThai TrangThai { get; set; }
    public string? GhiChu { get; set; }
}
