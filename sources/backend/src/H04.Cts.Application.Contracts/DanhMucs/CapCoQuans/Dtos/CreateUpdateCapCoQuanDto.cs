using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using System.ComponentModel.DataAnnotations;

namespace H04.Cts.Dtos.DanhMucs;

public class CreateUpdateCapCoQuanDto
{

    [Required]
    [StringLength(CapCoQuanConsts.MaCapCoQuanMaxLength)]
    public virtual string? MaCapCoQuan { get; set; }

    [Required]
    [StringLength(CapCoQuanConsts.TenCapCoQuanMaxLength)]
    public virtual string? TenCapCoQuan { get; set; }

    public virtual TrangThai TrangThai { get; set; }

    [StringLength(CapCoQuanConsts.GhiChuMaxLength)]
    public virtual string? GhiChu { get; set; }

}