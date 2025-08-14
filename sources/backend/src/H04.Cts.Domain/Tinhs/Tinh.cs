using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using H04.Cts.Xas;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.Tinhs
{
    public class Tinh : AuditedAggregateRoot<Guid>
    {
        //[Required]
        [MaxLength(16)]
        public string MaTinh { get; set; }

        [Required]
        [MaxLength(128)]
        public string TenTinh { get; set; }

        public int? SoThuTu { get; set; }
        public string? MoTa { get; set; }

        public ICollection<Xa> Xas { get; set; }
    }
}
