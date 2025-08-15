using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.DanhMucs
{
    public class ChucVu : FullAuditedAggregateRoot<long>
    {
        // [Required]
        [StringLength(ChucVuConsts.TenChucVuMaxLength)]
        public virtual string? TenChucVu { get; set; }

        public virtual TrangThai TrangThai { get; set; }

        // [Required]
        [StringLength(ChucVuConsts.MaChucVuMaxLength)]
        public virtual string? MaChucVu { get; set; }

        [StringLength(ToChucConsts.GhiChuMaxLength)]
        public virtual string? GhiChu { get; set; }

        public ICollection<ThueBaoCaNhan> ThueBaoCaNhans { get; set; }
    }
}
