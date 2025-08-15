using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H04.Cts.DanhMucs.ChucVucs.Dtos
{
    public class CreateUpdateChucVuDto
    {
        [Required]
        [StringLength(ChucVuConsts.TenChucVuMaxLength)]
        public string TenChucVu { get; set; }

        [Required]
        [StringLength(ChucVuConsts.MaChucVuMaxLength)]
        public string MaChucVu { get; set; }

        public TrangThai? TrangThai { get; set; }

        public string? GhiChu { get; set; }
    }

}
