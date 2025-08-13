using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.DanhMucs
{
    public class LoaiCTS : FullAuditedAggregateRoot<long>
    {
        [StringLength(LoaiCTSConsts.MaLoaiCTSMaxLength)]
        public virtual string MaLoaiCTS { get; set; }
        [StringLength(LoaiCTSConsts.TenLoaiCTSMaxLength)]
        public virtual string TenLoaiCTS { get; set; }
        public virtual TrangThai TrangThai { get; set; }
        [StringLength(LoaiCTSConsts.GhiChuMaxLength)]
        public virtual string? GhiChu { get; set; }
    }
}
