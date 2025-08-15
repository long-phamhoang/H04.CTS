using System;
using H04.Cts.Utilities;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.DanhMucs.XaPhuongs
{
    public class WardDto : AuditedEntityDto<Guid>
    {
        public string MaXaPhuong { get; set; }
        public string TenXaPhuong { get; set; }
        public Guid TinhThanhPhoId { get; set; }

        public TrangThai TrangThai { get; set; }

        public string? GhiChu { get; set; }
    }
}
