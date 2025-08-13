using H04.Cts.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.DanhMucs.DieuKienCapCTSTheoLL.Dto
{
    public class DieuKienCapCTSTheoLLDto : AuditedEntityDto<long>
    {

        public string? TenDieuKien { get; set; }
        public string? MaDieuKien { get; set; }
        public TrangThai TrangThai { get; set; }
        public string? GhiChu { get; set; }
        public long? LucLuongId { get; set; }
    }
}
