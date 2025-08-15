namespace H04.Cts.DanhMucs.LoaiThietBiDichVuPhanMems.Dtos;
public class LoaiThietBiDichVuPhanMemDto : BaseDtoDanhMuc<long>
{
    public virtual string? MaLoaiThietBiDichVuPhanMem { get; set; }

    public virtual string? TenLoaiThietBiDichVuPhanMem { get; set; }
}
