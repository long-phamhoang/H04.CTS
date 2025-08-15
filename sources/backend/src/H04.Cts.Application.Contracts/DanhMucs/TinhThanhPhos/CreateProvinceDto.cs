using System;
using Volo.Abp.Application.Dtos;
using H04.Cts.Utilities;

namespace H04.Cts.DanhMucs.TinhTHanhPhos
{
    public class CreateProvinceDto : AuditedEntityDto<Guid>
    {
        public string MaTinhThanhPho { get; set; }

        public string TenTinhThanhPho { get; set; }

        public TrangThai TrangThai { get; set; }

        public string? GhiChu { get; set; }
    }
}
