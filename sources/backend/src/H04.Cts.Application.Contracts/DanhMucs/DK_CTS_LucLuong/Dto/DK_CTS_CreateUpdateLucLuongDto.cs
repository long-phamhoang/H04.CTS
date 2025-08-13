using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H04.Cts.DanhMucs.DieuKienCapCTSTheoLL.Dto
{
    public class DieuKienCapCTSTheoLL_CreateUpdateDto
    {


        [StringLength(DieuKienCapCTSTheoLLConts.TenDieuKienMaxLength)]
        public virtual string? TenDieuKien { get; set; }

        [StringLength(DieuKienCapCTSTheoLLConts.MaDieuKienMaxLength)]
        public virtual string? MaDieuKien { get; set; }


        public virtual TrangThai TrangThai { get; set; }


        [StringLength(DieuKienCapCTSTheoLLConts.GhiChuMaxLength)]
        public virtual string? GhiChu { get; set; }


        public long? LucLuongId { get; set; }
    }
}
