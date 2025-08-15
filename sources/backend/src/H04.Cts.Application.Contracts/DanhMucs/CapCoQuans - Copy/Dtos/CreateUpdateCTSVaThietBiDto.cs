using H04.Cts.DanhMucs;
using H04.Cts.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace H04.Cts.Dtos.DanhMucs;

public class CreateUpdateCTSVaThietBiDto
{
    [StringLength(CTSVaThietBiConst.TenCTSMaxLength)]
    public virtual string? TenCTS { get; set; }

    [StringLength(CTSVaThietBiConst.SoHieuCTSMaxLength)]
    public virtual string? SoHieuCTS { get; set; }

    [StringLength(CTSVaThietBiConst.DiaChiThuDienTuCongVuMaxLength)]
    public virtual string? DiaChiThuDienTuCongVu { get; set; }

    public DateTime? NgayHieuLuc { get; set; }
    public DateTime? NgayHetHan { get; set; }

    [StringLength(CTSVaThietBiConst.CoQuanToChucMaxLength)]
    public virtual string? CoQuanToChuc { get; set; }

    [StringLength(CTSVaThietBiConst.LoaiCTSMaxLength)]
    public virtual string? LoaiCTS { get; set; }

    public virtual TrangThai TrangThai { get; set; }

    [StringLength(CTSVaThietBiConst.HoSoMaxLength)]
    public virtual string? HoSo { get; set; }

    [StringLength(CTSVaThietBiConst.VanBanMaxLength)]
    public virtual string? VanBan { get; set; }

    [StringLength(CTSVaThietBiConst.HoSoThuHoiMaxLength)]
    public virtual string? HoSoThuHoi { get; set; }

    [StringLength(CTSVaThietBiConst.VanBanThuHoiMaxLength)]
    public virtual string? VanBanThuHoi { get; set; }

    [StringLength(CTSVaThietBiConst.NotesMaxLength)]
    public virtual string? Notes { get; set; }

    #region Cá nhân
    [StringLength(CTSVaThietBiConst.CnTenMaxLength)]
    public virtual string? CnTen { get; set; }

    public DateTime? CnNgaySinh { get; set; }

    [StringLength(CTSVaThietBiConst.CnDienThoaiMaxLength)]
    public virtual string? CnSDT { get; set; }

    [StringLength(CTSVaThietBiConst.CnCCCDMaxLength)]
    public virtual string? CnCCCD { get; set; }

    public DateTime? CnNgayCap { get; set; }

    [StringLength(CTSVaThietBiConst.CnNoiCapMaxLength)]
    public virtual string? CnNoiCap { get; set; }

    [StringLength(CTSVaThietBiConst.CnChucVuMaxLength)]
    public virtual string? CnChucVu { get; set; }

    [StringLength(CTSVaThietBiConst.CnDiaChiThuDienTuCongVuMaxLength)]
    public virtual string? CnDiaChiThuDienTuCongVu { get; set; }

    [StringLength(CTSVaThietBiConst.CnTinhTPMaxLength)]
    public virtual string? CnTinhTP { get; set; }
    #endregion

    #region Tổ chức
    [StringLength(CTSVaThietBiConst.TcTenMaxLength)]
    public virtual string? TcTen { get; set; }

    [StringLength(CTSVaThietBiConst.TcMaDinhDanhMaxLength)]
    public virtual string? TcMaDinhDanh { get; set; }

    [StringLength(CTSVaThietBiConst.TcMaSoThueMaxLength)]
    public virtual string? TcMaSoThue { get; set; }

    [StringLength(CTSVaThietBiConst.TcDiaChiThuDienTuCongVuMaxLength)]
    public virtual string? TcDiaChiThuDienTuCongVu { get; set; }

    [StringLength(CTSVaThietBiConst.TcSDTMaxLength)]
    public virtual string? TcSDT { get; set; }

    [StringLength(CTSVaThietBiConst.TcTinhTPMaxLength)]
    public virtual string? TcTinhTP { get; set; }

    [StringLength(CTSVaThietBiConst.TcPhuongXaMaxLength)]
    public virtual string? TcPhuongXa { get; set; }

    [StringLength(CTSVaThietBiConst.TcMaQuanHeNganSachMaxLength)]
    public virtual string? TcMaQuanHeNganSach { get; set; }
    #endregion
}