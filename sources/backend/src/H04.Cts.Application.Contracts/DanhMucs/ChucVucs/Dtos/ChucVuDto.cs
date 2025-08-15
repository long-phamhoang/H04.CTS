using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.DanhMucs.ChucVucs.Dtos
{
    public class ChucVuDto : AuditedEntityDto<long>
    {
        public string? TenChucVu { get; set; }
        public  string? MaChucVu { get; set; }
        public TrangThai TrangThai { get; set; }
        public  string? GhiChu { get; set; }
    }
}
