using H04.Cts.Utilities;
using System;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class CTSVaThietBiDto : AuditedEntityDto<long>
{
    public virtual string? TenCTS { get; set; }

    public virtual string? SoHieuCTS { get; set; }

    public virtual string? DiaChiThuDienTuCongVu { get; set; }

    public DateTime? NgayHieuLuc { get; set; }
    public DateTime? NgayHetHan { get; set; }

    public virtual string? CoQuanToChuc { get; set; }

    public virtual string? LoaiCTS { get; set; }

    public virtual TrangThai TrangThai { get; set; }

    public virtual string? HoSo { get; set; }

    public virtual string? VanBan { get; set; }

    public virtual string? HoSoThuHoi { get; set; }

    public virtual string? VanBanThuHoi { get; set; }

    public virtual string? Notes { get; set; }

    #region Cá nhân
    public virtual string? CnTen { get; set; }

    public DateTime? CnNgaySinh { get; set; }

    public virtual string? CnSDT { get; set; }

    public virtual string? CnCCCD { get; set; }

    public DateTime? CnNgayCap { get; set; }

    public virtual string? CnNoiCap { get; set; }

    public virtual string? CnChucVu { get; set; }

    public virtual string? CnDiaChiThuDienTuCongVu { get; set; }

    public virtual string? CnTinhTP { get; set; }
    #endregion

    #region Tổ chức
    public virtual string? TcTen { get; set; }

    public virtual string? TcMaDinhDanh { get; set; }

    public virtual string? TcMaSoThue { get; set; }

    public virtual string? TcDiaChiThuDienTuCongVu { get; set; }

    public virtual string? TcSDT { get; set; }

    public virtual string? TcTinhTP { get; set; }

    public virtual string? TcPhuongXa { get; set; }

    public virtual string? TcMaQuanHeNganSach { get; set; }
    #endregion
}