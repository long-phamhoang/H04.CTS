using H04.Cts.DanhMucs;
using H04.Cts.Entities.DanhMucs;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Volo.Abp.Domain.Entities.Auditing;

public class ThueBaoCaNhan : FullAuditedAggregateRoot<long>
{
    [Required]
    [StringLength(ThueBaoCaNhanConsts.HoTenMaxLength)]
    public virtual string HoTen { get; set; } = null!;

    [Required]
    public virtual DateTime NgaySinh { get; set; }

    [Required]
    [StringLength(ThueBaoCaNhanConsts.SoDinhDanhCaNhanMaxLength)]
    public virtual string SoDinhDanhCaNhan { get; set; } = null!;

    [Required]
    public virtual string NoiCap { get; set; } = null!;

    [Required]
    public virtual DateTime NgayCap { get; set; }

    [Required]
    public virtual long ToChucId { get; set; }

    [ForeignKey(nameof(ToChucId))]
    public virtual ToChuc ToChucFk { get; set; } = null!;

    [Required]
    public virtual long ChucVuId { get; set; }

    [ForeignKey(nameof(ChucVuId))]
    public virtual ChucVu ChucVuFk { get; set; } = null!;

    [Required]
    [StringLength(ThueBaoCaNhanConsts.DiaChiThuDienTuCongVuMaxLength)]
    public virtual string DiaChiThuDienTuCongVu { get; set; } = null!;

    // Không bắt buộc
    public virtual long? TinhThanhPho { get; set; }
    public virtual long? PhuongXa { get; set; }
}
