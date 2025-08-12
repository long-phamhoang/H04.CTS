using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using System.ComponentModel.DataAnnotations;

namespace H04.Cts.Dtos.DanhMucs;

public class CreateUpdateToChucDto
{
    public long? ToChucCapTrenId { get; set; }

    [Required]
    [StringLength(ToChucConsts.TenToChucMaxLength)]
    public required string TenToChuc { get; set; }

    [Required]
    [StringLength(ToChucConsts.MaToChucMaxLength)]
    public required string MaToChuc { get; set; }

    [StringLength(ToChucConsts.MaSoThueMaxLength)]
    public string? MaSoThue { get; set; }

    [StringLength(ToChucConsts.DiaChiThuCongVuMaxLength)]
    [RegularExpression(ToChucConsts.DiaChiThuCongVuPattern)]
    public string? DiaChiThuCongVu { get; set; }

    [StringLength(ToChucConsts.DienThoaiMaxLength)]
    [RegularExpression(ToChucConsts.DienThoaiPattern)]
    public string? DienThoai { get; set; }

    [StringLength(ToChucConsts.MaQuanHeNganSachMaxLength)]
    public string? MaQuanHeNganSach { get; set; }

    public long? CapCoQuanId { get; set; }



    [StringLength(ToChucConsts.SoNhaMaxLength)]
    public string? SoNha { get; set; }

    public long? TinhThanhPhoId { get; set; }

    public long? XaPhuongId { get; set; }

    [StringLength(ToChucConsts.CoQuanPhuTrachMaxLength)]
    public string? CoQuanPhuTrach { get; set; }

    public TrangThai TrangThai { get; set; }

    [StringLength(ToChucConsts.GhiChuMaxLength)]
    public string? GhiChu { get; set; }

}