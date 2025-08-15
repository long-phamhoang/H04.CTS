using Volo.Abp.Application.Dtos;

namespace H04.Cts.DanhMucs.ThietBiDichVuPhanMems.Dtos;

public class ThietBiDichVuPhanMemDto : AuditedEntityDto<long>
{
    public virtual long LoaiThietBiDichVuPhanMemId { get; set; }

    public virtual long? TinhThanhPhoId { get; set; }

    public virtual long? ToChucId { get; set; }

    public virtual string? TenCoQuanToChuc { get; set; }

    public virtual string? TenThietBiDichVuPhanMem { get; set; }

    public virtual string? DiaChiThuCongVu { get; set; }

    public virtual string? DiaChiIP { get; set; }

    public virtual string? DNS { get; set; }

    public virtual string? DiaChiIP1 { get; set; }

    public virtual string? DNS1 { get; set; }
}
