using H04.Cts.Utilities;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.DanhMucs
{
    public class CtsVaThietBi : FullAuditedAggregateRoot<long>
    {
        [StringLength(CtsVaThietBiConst.TenCtsMaxLength)]
        public virtual string? TenCts { get; set; }

        [StringLength(CtsVaThietBiConst.SoHieuCtsMaxLength)]
        public virtual string? SoHieuCts { get; set; }

        [StringLength(CtsVaThietBiConst.DiaChiThuDienTuCongVuMaxLength)]
        public virtual string? DiaChiThuDienTuCongVu { get; set; }

        public DateTime? NgayHieuLuc { get; set; }
        public DateTime? NgayHetHan { get; set; }

        public virtual long? ToChucId { get; set; }

        public virtual LoaiCts? LoaiCts { get; set; }

        public virtual TrangThai? TrangThai { get; set; }

        [StringLength(CtsVaThietBiConst.HoSoMaxLength)]
        public virtual string? HoSo { get; set; }

        [StringLength(CtsVaThietBiConst.VanBanMaxLength)]
        public virtual string? VanBan { get; set; }

        [StringLength(CtsVaThietBiConst.HoSoThuHoiMaxLength)]
        public virtual string? HoSoThuHoi { get; set; }

        [StringLength(CtsVaThietBiConst.VanBanThuHoiMaxLength)]
        public virtual string? VanBanThuHoi { get; set; }

        [StringLength(CtsVaThietBiConst.NotesMaxLength)]
        public virtual string? Notes { get; set; }

        [StringLength(CtsVaThietBiConst.TenMaxLength)]
        public virtual string? Ten { get; set; }

        public DateTime NgaySinh { get; set; }

        [StringLength(CtsVaThietBiConst.DienThoaiMaxLength)]
        public virtual string? SoDienThoai { get; set; }

        [StringLength(CtsVaThietBiConst.CCCDMaxLength)]
        public virtual string? MaDinhDanh { get; set; }

        public DateTime NgayCap { get; set; }

        public virtual long NoiCapId { get; set; }

        public virtual long? ChucVuId { get; set; }

        [StringLength(CtsVaThietBiConst.MaSoThueMaxLength)]
        public virtual string? MaSoThue { get; set; }

        public virtual long TinhThanhPhoId { get; set; }

        public virtual long? PhuongXaId { get; set; }

        [StringLength(CtsVaThietBiConst.MaQuanHeNganSachMaxLength)]
        public virtual string? MaQuanHeNganSach { get; set; }
    }
}
