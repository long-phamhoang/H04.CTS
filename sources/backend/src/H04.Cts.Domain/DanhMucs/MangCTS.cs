using System;
using System.ComponentModel.DataAnnotations;
using H04.Cts.Utilities;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.Entities.DanhMucs;

public class MangCTS : FullAuditedAggregateRoot<long>
{
    [Required]
    [StringLength(MangCTSConsts.MaMangCTSMaxLength)]
    public required string MaMangCTS { get; set; }
    [Required]
    [StringLength(MangCTSConsts.TenMangCTSMaxLength)]
    public required string TenMangCTS { get; set; }
    [Required]
    public required TrangThai TrangThai { get; set; }
    [StringLength(MangCTSConsts.GhiChuMaxLength)]
    public string? GhiChu { get; set; }
}
