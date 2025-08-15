using System.ComponentModel.DataAnnotations;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.DanhMucs;
public abstract class BaseEntityDanhMuc<Tkey> : FullAuditedAggregateRoot<Tkey> 
{
    public virtual TrangThai TrangThai { get; set; }

    [StringLength(ToChucConsts.GhiChuMaxLength)]
    public virtual string? GhiChu { get; set; }
}
