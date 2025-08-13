using System.ComponentModel.DataAnnotations;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.DanhMucs
{
    public class LoaiHoSo : FullAuditedAggregateRoot<long>
    {
        [StringLength(LoaiHoSoConsts.MaLoaiHoSoMaxLength)]
        public virtual string MaLoaiHoSo { get; set; }

        [StringLength(LoaiHoSoConsts.TenLoaiHoSoMaxLength)]
        public virtual string TenLoaiHoSo { get; set; }

        public virtual TrangThai TrangThai { get; set; }

        [StringLength(LoaiHoSoConsts.GhiChuMaxLength)]
        public virtual string? GhiChu { get; set; }
    }
}
