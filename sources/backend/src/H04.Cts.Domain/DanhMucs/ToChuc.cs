using H04.Cts.Utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.Entities.DanhMucs;

public class ToChuc : FullAuditedAggregateRoot<long>
{
    public virtual long? ToChucCapTrenId { get; set; }

    [ForeignKey(nameof(ToChucCapTrenId))]
    public ToChuc ToChucCapTrenFk { get; set; }

    // [Required]
    [StringLength(ToChucConsts.TenToChucMaxLength)]
    public virtual string? TenToChuc { get; set; }

    // [Required]
    [StringLength(ToChucConsts.MaToChucMaxLength)]
    public virtual string? MaToChuc { get; set; }

    [StringLength(ToChucConsts.MaSoThueMaxLength)]
    public virtual string? MaSoThue { get; set; }

    [StringLength(ToChucConsts.DiaChiThuCongVuMaxLength)]
    [RegularExpression(ToChucConsts.DiaChiThuCongVuPattern)]
    public virtual string? DiaChiThuCongVu { get; set; }

    [StringLength(ToChucConsts.DienThoaiMaxLength)]
    [RegularExpression(ToChucConsts.DienThoaiPattern)]
    public virtual string? DienThoai { get; set; }

    [StringLength(ToChucConsts.MaQuanHeNganSachMaxLength)]
    public virtual string? MaQuanHeNganSach { get; set; }

    public virtual long? CapCoQuanId { get; set; }

    // [ForeignKey(nameof(CapCoQuanId))]
    // public CapCoQuan CapCoQuanFk { get; set; }



    [StringLength(ToChucConsts.SoNhaMaxLength)]
    public virtual string? SoNha { get; set; }

    public virtual long? TinhThanhPhoId { get; set; }

    // [ForeignKey(nameof(TinhThanhPhoId))]
    // public TinhThanhPho TinhThanhPhoFk { get; set; }

    public virtual long? XaPhuongId { get; set; }

    // [ForeignKey(nameof(XaPhuongId))]
    // public XaPhuong XaPhuongFk { get; set; }

    [StringLength(ToChucConsts.CoQuanPhuTrachMaxLength)]
    public virtual string? CoQuanPhuTrach { get; set; }

    public virtual TrangThai TrangThai { get; set; }

    [StringLength(ToChucConsts.GhiChuMaxLength)]
    public virtual string? GhiChu { get; set; }



    public ICollection<ToChuc> ToChucCapDuois { get; set; } = new HashSet<ToChuc>();
    // public ICollection<ThietBiDichVuPhanMem> ThietBiDichVuPhanMems { get; set; } = new HashSet<ThietBiDichVuPhanMem>();
    // public ICollection<ThueBaoCaNhan> ThueBaoCaNhans { get; set; } = new HashSet<ThueBaoCaNhan>();
    // public ICollection<NguoiTiepNhan> NguoiTiepNhans { get; set; } = new HashSet<NguoiTiepNhan>();

}