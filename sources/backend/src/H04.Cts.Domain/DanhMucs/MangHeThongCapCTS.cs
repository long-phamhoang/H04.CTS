using System;
using System.ComponentModel.DataAnnotations;
using H04.Cts.Utilities;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.Entities.DanhMucs;

public class MangHeThongCapCTS : FullAuditedAggregateRoot<long>
{
    [Required]
    [StringLength(MangMangHeThongCapCTSConsts.MaMangHeThongCapCTSMaxLength)]
    public required string MaMangHeThongCapCTS { get; set; }
    [Required]
    [StringLength(MangMangHeThongCapCTSConsts.TenMangHeThongCapCTSMaxLength)]
    public required string TenMangHeThongCapCTS { get; set; }
    [Required]
    public required TrangThai TrangThai { get; set; }
    [StringLength(MangMangHeThongCapCTSConsts.GhiChuMaxLength)]
    public string? GhiChu { get; set; }
}
