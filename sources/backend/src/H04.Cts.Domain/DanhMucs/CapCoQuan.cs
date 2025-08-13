using H04.Cts.Utilities;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.Entities.DanhMucs;

public class CapCoQuan : FullAuditedAggregateRoot<long>
{
    // [Required]
    [StringLength(CapCoQuanConsts.MaCapCoQuanMaxLength)]
    public virtual string? MaCapCoQuan { get; set; }

    // [Required]
    [StringLength(CapCoQuanConsts.TenCapCoQuanMaxLength)]
    public virtual string? TenCapCoQuan { get; set; }

    public virtual TrangThai TrangThai { get; set; }

    [StringLength(CapCoQuanConsts.GhiChuMaxLength)]
    public virtual string? GhiChu { get; set; }
}