using System.ComponentModel.DataAnnotations;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;

namespace H04.Cts.DanhMucs;
public class BaseCreateUpdateDtoDanhMuc
{
    public TrangThai TrangThai { get; set; }

    [StringLength(ToChucConsts.GhiChuMaxLength)]
    public string? GhiChu { get; set; }
}
