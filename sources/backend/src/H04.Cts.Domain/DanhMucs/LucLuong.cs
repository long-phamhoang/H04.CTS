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
    public class LucLuong : FullAuditedAggregateRoot<long>
    {

        [StringLength(LucLuongConsts.TenLucLuongMaxLength)]
        public virtual string? TenLucLuong { get; set; }

        // [Required]
        [StringLength(LucLuongConsts.MaLucLuongcMaxLength)]
        public virtual string? MaLucLuong { get; set; }

        public virtual TrangThai TrangThai { get; set; }


        [StringLength(LucLuongConsts.GhiChuMaxLength)]
        public virtual string? GhiChu { get; set; }

    }
}
