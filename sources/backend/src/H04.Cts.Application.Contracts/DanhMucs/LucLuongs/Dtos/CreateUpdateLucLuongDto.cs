using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using System.ComponentModel.DataAnnotations;

namespace H04.Cts.Dtos.DanhMucs;

public class CreateUpdateLucLuongDto
{
    [StringLength(LucLuongConsts.TenLucLuongMaxLength)]
    public string? TenLucLuong { get; set; }

    [StringLength(LucLuongConsts.MaLucLuongcMaxLength)]
    public string? MaLucLuong { get; set; }

    public TrangThai TrangThai { get; set; }

    [StringLength(LucLuongConsts.GhiChuMaxLength)]
    public string? GhiChu { get; set; }
}
