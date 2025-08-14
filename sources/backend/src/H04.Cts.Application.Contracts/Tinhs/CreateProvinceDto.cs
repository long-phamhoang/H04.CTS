using System;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Tinhs
{
    public class CreateProvinceDto : AuditedEntityDto<Guid>
    {
        public string MaTinh { get; set; }

        public string TenTinh { get; set; }

        public int? SoThuTu { get; set; }

        public string? MoTa { get; set; }
    }
}
