using System;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Wards
{
    public class WardDto : AuditedEntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid ProvinceId { get; set; }
        public int? SortOrder { get; set; }
        public string Description { get; set; }

        public WardType Type { get; set; } 
    }
}
