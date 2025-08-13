using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H04.Cts.Utilities;

namespace H04.Cts.DanhMucs.LoaiHoSos.Dtos
{
    public class CreateUpdateLoaiHoSoDto
    {
        [StringLength(LoaiHoSoConsts.MaLoaiHoSoMaxLength)]
        public required string MaLoaiHoSo { get; set; }

        [StringLength(LoaiHoSoConsts.TenLoaiHoSoMaxLength)]
        public required string TenLoaiHoSo { get; set; }

        public TrangThai TrangThai { get; set; }

        [StringLength(LoaiHoSoConsts.GhiChuMaxLength)]
        public string? GhiChu { get; set; }
    }
}
