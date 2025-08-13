using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.DanhMucs.LoaiCTSs.Dtos
{
    public class LoaiCTSDto : AuditedEntityDto<long>
    {
        public string MaLoaiCTS { get; set; }
        public string TenLoaiCTS { get; set; }
        public TrangThai TrangThai { get; set; }
        public string? GhiChu { get; set; }
    }
}
