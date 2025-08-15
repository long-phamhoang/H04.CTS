using System;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.DanhMucs.Dtos
{
    public class ThueBaoCaNhanDto : AuditedEntityDto<long>
    {
        public string HoTen { get; set; } = null!;
        public DateTime? NgaySinh { get; set; }
        public string SoDinhDanhCaNhan { get; set; } = null!;
        public string NoiCap { get; set; } = null!;
        public DateTime NgayCap { get; set; }

        public long? ToChucId { get; set; }
        public string? TenToChuc { get; set; } 

        public long? ChucVuId { get; set; }
        public string? TenChucVu { get; set; }

        public string? DiaChiThuDienTuCongVu { get; set; }

        public long? TinhThanhPho { get; set; }
        public long? PhuongXa { get; set; }
    }
}
