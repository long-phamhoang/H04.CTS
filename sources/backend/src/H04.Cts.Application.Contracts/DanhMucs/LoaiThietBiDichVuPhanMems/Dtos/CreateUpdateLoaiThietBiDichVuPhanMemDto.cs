using System.ComponentModel.DataAnnotations;

namespace H04.Cts.DanhMucs.LoaiThietBiDichVuPhanMems.Dtos;
public class CreateUpdateLoaiThietBiDichVuPhanMemDto : BaseCreateUpdateDtoDanhMuc
{
    [StringLength(DanhMucChungConst.MaSoChungMaxLength)]
    public virtual string? MaLoaiThietBiDichVuPhanMem { get; set; }

    [StringLength(DanhMucChungConst.TenDanhMucChungMaxLength)]
    public virtual string? TenLoaiThietBiDichVuPhanMem { get; set; }
}
