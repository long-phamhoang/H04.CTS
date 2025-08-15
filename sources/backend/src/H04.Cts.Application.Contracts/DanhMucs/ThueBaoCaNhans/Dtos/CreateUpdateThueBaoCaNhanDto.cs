using H04.Cts.DanhMucs;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

public class CreateUpdateThueBaoCaNhanDto
{
    [Required]
    [StringLength(ThueBaoCaNhanConsts.HoTenMaxLength)]
    public string HoTen { get; set; } = null!;

    [Required]
    public DateTime NgaySinh { get; set; }

    [Required]
    [StringLength(ThueBaoCaNhanConsts.SoDinhDanhCaNhanMaxLength)]
    public string SoDinhDanhCaNhan { get; set; } = null!;

    [Required]
    public string NoiCap { get; set; } = null!;

    [Required]
    public DateTime NgayCap { get; set; }

    [Required]
    public long ToChucId { get; set; }

    [Required]
    public long ChucVuId { get; set; }


    [Required]
    [StringLength(ThueBaoCaNhanConsts.DiaChiThuDienTuCongVuMaxLength)]
    public string DiaChiThuDienTuCongVu { get; set; } = null!;

    // Không bắt buộc
    public long? TinhThanhPho { get; set; }
    public long? PhuongXa { get; set; }
}
