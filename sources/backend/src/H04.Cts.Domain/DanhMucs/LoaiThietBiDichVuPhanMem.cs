using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace H04.Cts.DanhMucs;
public class LoaiThietBiDichVuPhanMem : BaseEntityDanhMuc<long>
{
    [StringLength(DanhMucChungConst.MaSoChungMaxLength)]
    public virtual string? MaLoaiThietBiDichVuPhanMem {get; set;}

    [StringLength(DanhMucChungConst.TenDanhMucChungMaxLength)]
    public virtual string? TenLoaiThietBiDichVuPhanMem { get; set; }

    public ICollection<ThietBiDichVuPhanMem>? ThietBiDichVuPhanMems { get; set; } = new HashSet<ThietBiDichVuPhanMem>();

}
