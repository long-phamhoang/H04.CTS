using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.DanhMucs;
public class ThietBiDichVuPhanMem : FullAuditedAggregateRoot<long>
{
    [StringLength(DanhMucChungConst.TenDanhMucChungMaxLength)]
    public virtual string? TenCoQuanToChuc { get; set; }

    [StringLength(DanhMucChungConst.TenDanhMucChungMaxLength)]
    public virtual string? TenThietBiDichVuPhanMem { get; set; }

    #region FK LoaiThietBiDichVuPhanMem
    public virtual long LoaiThietBiDichVuPhanMemId { get; set; }

    [ForeignKey(nameof(LoaiThietBiDichVuPhanMemId))]
    public virtual LoaiThietBiDichVuPhanMem? LoaiThietBiDichVuPhanMemFk { get; set; }
    #endregion

    public virtual long? TinhThanhPhoId { get; set; }
    // [ForeignKey(nameof(TinhThanhPhoId))]
    // public TinhThanhPho TinhThanhPhoFk { get; set; }

    [StringLength(DanhMucChungConst.DiaChiThuCongVuMaxLength)]
    [RegularExpression(DanhMucChungConst.DiaChiThuCongVuPattern)]
    public virtual string? DiaChiThuCongVu { get; set; }

    [RegularExpression(ThietBiDichVuPhanMemConst.IPPattern)]
    public virtual string? DiaChiIP { get; set; }

    [RegularExpression(ThietBiDichVuPhanMemConst.DNSPattern)]
    public virtual string? DNS { get; set; }

    [RegularExpression(ThietBiDichVuPhanMemConst.IPPattern)]
    public virtual string? DiaChiIP1 { get; set; }

    [RegularExpression(ThietBiDichVuPhanMemConst.DNSPattern)]
    public virtual string? DNS1 { get; set; }
}
