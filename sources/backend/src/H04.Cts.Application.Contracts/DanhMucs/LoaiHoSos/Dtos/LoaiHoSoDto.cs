using System.ComponentModel.DataAnnotations;
using H04.Cts.Utilities;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.DanhMucs.LoaiHoSos.Dtos
{
    public class LoaiHoSoDto : AuditedEntityDto<long>
    {
        public string MaLoaiHoSo { get; set; }

        public string TenLoaiHoSo { get; set; }

        public TrangThai TrangThai { get; set; }

        public string? GhiChu { get; set; }
    }
}
