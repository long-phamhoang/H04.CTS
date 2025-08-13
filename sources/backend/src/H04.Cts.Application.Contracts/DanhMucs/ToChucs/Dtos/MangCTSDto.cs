using System;
using H04.Cts.Utilities;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class MangCTSDto : AuditedEntityDto<long>
{
    public required string MaMangCTS { get; set; }
    public required string TenMangCTS { get; set; }
    public required TrangThai TrangThai { get; set; }
    public string? GhiChu { get; set; }
}
