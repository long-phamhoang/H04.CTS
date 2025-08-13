using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.Entities.DanhMucs
{
    public class DieuKienCapCTSTheoLL : FullAuditedAggregateRoot<long>
    {
        [StringLength(DieuKienCapCTSTheoLLConts.TenDieuKienMaxLength)]
        public virtual string? TenDieuKien { get; set; }

        // [Required]
        [StringLength(DieuKienCapCTSTheoLLConts.MaDieuKienMaxLength)]
        public virtual string? MaDieuKien { get; set; }


        public virtual TrangThai TrangThai { get; set; }


        [StringLength(DieuKienCapCTSTheoLLConts.GhiChuMaxLength)]
        public virtual string? GhiChu { get; set; }

        public long? LucLuongId { get; set; }

        [ForeignKey(nameof(LucLuongId))]
        public LucLuong? LucLuong { get; set; }
    }
}
