using System.ComponentModel.DataAnnotations;

namespace H04.Cts.DanhMucs.ThietBiDichVuPhanMems.Dtos;
public class CreateUpdateThietBiDichVuPhanMemDto
{
    public virtual long LoaiThietBiDichVuPhanMemId { get; set; }

    public virtual long? TinhThanhPhoId { get; set; }

    [StringLength(DanhMucChungConst.TenDanhMucChungMaxLength)]
    public virtual string? TenCoQuanToChuc { get; set; }

    [StringLength(DanhMucChungConst.TenDanhMucChungMaxLength)]
    public virtual string? TenThietBiDichVuPhanMem { get; set; }

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
