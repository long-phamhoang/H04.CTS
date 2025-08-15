using H04.Cts.Utilities;
using System;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class CtsVaThietBiDto : AuditedEntityDto<long>
{
    public virtual string? TenCts { get; set; }

    public virtual string? SoHieuCts { get; set; }

    public virtual string? DiaChiThuDienTuCongVu { get; set; }

    public DateTime? NgayHieuLuc { get; set; }

    public DateTime? NgayHetHan { get; set; }

    public virtual long? ToChucId { get; set; }

    public virtual LoaiCts? LoaiCts { get; set; }

    public virtual TrangThai? TrangThai { get; set; }

    public virtual string? HoSo { get; set; }

    public virtual string? VanBan { get; set; }

    public virtual string? HoSoThuHoi { get; set; }

    public virtual string? VanBanThuHoi { get; set; }

    public virtual string? Notes { get; set; }

    public virtual string? Ten { get; set; }

    public DateTime NgaySinh { get; set; }

    public virtual string? SoDienThoai { get; set; }

    public virtual string? MaDinhDanh { get; set; }

    public DateTime NgayCap { get; set; }

    public virtual long NoiCapId { get; set; }

    public virtual long? ChucVuId { get; set; }

    public virtual string? MaSoThue { get; set; }

    public virtual long TinhThanhPhoId { get; set; }

    public virtual long? PhuongXaId { get; set; }

    public virtual string? MaQuanHeNganSach { get; set; }
}