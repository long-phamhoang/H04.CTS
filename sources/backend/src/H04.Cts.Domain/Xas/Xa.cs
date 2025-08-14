using System;
using System.ComponentModel.DataAnnotations;
using H04.Cts.Tinhs;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.Xas
{
    public class Xa : AuditedAggregateRoot<Guid>
    {
        //[Required]
        [MaxLength(10)]
        public string MaXa { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenXa { get; set; }

        public int? SoThuTu { get; set; }
        public string? MoTa { get; set; }

        [Required]
        public Guid TinhId { get; set; }
        [Required]        
        public Tinh Tinh { get; set; }
    }
}
