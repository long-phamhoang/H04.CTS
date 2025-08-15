using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using H04.Cts.Utilities;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.DanhMucs
{
    public class XaPhuong : FullAuditedAggregateRoot<Guid>
    {
        //[Required]
        [MaxLength(16)]
        public string MaXaPhuong { get; set; }

        [Required]
        [MaxLength(128)]
        public string TenXaPhuong { get; set; }

        public virtual TrangThai TrangThai { get; set; }

        public virtual string? GhiChu { get; set; }

        [Required]
        public Guid TinhThanhPhoId { get; set; }

        [ForeignKey(nameof(TinhThanhPhoId))]
        public TinhThanhPho TinhThanhPho { get; set; }
    }
}
