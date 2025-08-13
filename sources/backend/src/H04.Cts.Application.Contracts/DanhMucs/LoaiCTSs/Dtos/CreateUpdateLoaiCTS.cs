using System.ComponentModel.DataAnnotations;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;

namespace H04.Cts.DanhMucs.LoaiCTSs.Dtos
{
    public class CreateUpdateLoaiCTS
    {
        [StringLength(LoaiCTSConsts.MaLoaiCTSMaxLength)]
        public string MaLoaiCTS { get; set; }
        [StringLength(LoaiCTSConsts.TenLoaiCTSMaxLength)]
        public string TenLoaiCTS { get; set; }
        public TrangThai TrangThai { get; set; }
        [StringLength(LoaiCTSConsts.GhiChuMaxLength)]
        public string? GhiChu { get; set; }
    }
}
