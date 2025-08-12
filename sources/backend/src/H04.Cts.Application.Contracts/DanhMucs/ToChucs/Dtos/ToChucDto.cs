using H04.Cts.Utilities;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class ToChucDto : AuditedEntityDto<long>
{
    public long? ToChucCapTrenId { get; set; }

    public string? TenToChuc { get; set; }

    public string? MaToChuc { get; set; }

    public string? MaSoThue { get; set; }

    public string? DiaChiThuCongVu { get; set; }

    public string? DienThoai { get; set; }

    public string? MaQuanHeNganSach { get; set; }

    public long? CapCoQuanId { get; set; }



    public string? SoNha { get; set; }

    public long? TinhThanhPhoId { get; set; }

    public long? XaPhuongId { get; set; }

    public string? CoQuanPhuTrach { get; set; }

    public TrangThai TrangThai { get; set; }

    public string? GhiChu { get; set; }

}