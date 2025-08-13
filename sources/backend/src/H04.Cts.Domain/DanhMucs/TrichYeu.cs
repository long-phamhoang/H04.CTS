using System;
using System.ComponentModel.DataAnnotations;
using H04.Cts.Utilities;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.Entities.DanhMucs;

public class TrichYeu : FullAuditedAggregateRoot<long>
{
    [Required]
    [StringLength(TichYeuConsts.MaTrichYeuMaxLength)]
    public required string MaTrichYeu { get; set; }
    [Required]
    [StringLength(TichYeuConsts.TenTrichYeuMaxLength)]
    public required string TenTrichYeu { get; set; }
    [Required]
    public required TrangThai TrangThai { get; set; }
    [StringLength(TichYeuConsts.MoTaMaxLength)]
    public string? MoTa { get; set; }
    [StringLength(TichYeuConsts.GhiChuMaxLength)]
    public string? GhiChu { get; set; }
}
