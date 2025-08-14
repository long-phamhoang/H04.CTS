using System;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Xas
{
    public class WardDto : AuditedEntityDto<Guid>
    {
        public string MaXa { get; set; }
        public string TenXa { get; set; }
        public Guid TinhId { get; set; }
        public int? SoThuTu { get; set; }
        public string? MoTa { get; set; }
    }
}
