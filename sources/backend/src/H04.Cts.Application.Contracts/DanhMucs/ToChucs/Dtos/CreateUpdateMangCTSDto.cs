using System;
using System.ComponentModel.DataAnnotations;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;

namespace H04.Cts.Dtos.DanhMucs;

public class CreateUpdateMangCTSDto
{
    [Required]
    [StringLength(MangCTSConsts.MaMangCTSMaxLength)]
    public required string MaMangCTS { get; set; }

    [Required]
    [StringLength(MangCTSConsts.TenMangCTSMaxLength)]
    public required string TenMangCTS  { get; set; }

    [Required]
    public required TrangThai trangThai { get; set; }

    [StringLength(MangCTSConsts.GhiChuMaxLength)]
    public string? GhiChu { get; set; }
}
