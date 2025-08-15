using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using H04.Cts.Utilities;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.DanhMucs
{
    public class TinhThanhPho : FullAuditedAggregateRoot<Guid>
    {
        //[Required]
        [MaxLength(16)]
        public virtual string MaTinhThanhPho { get; set; }

        [Required]
        [MaxLength(128)]
        public virtual string TenTinhThanhPho { get; set; }

        public virtual TrangThai TrangThai { get; set; }

        public virtual string? GhiChu { get; set; }

        public virtual ICollection<XaPhuong> XaPhuongs { get; set; }
    }
}
