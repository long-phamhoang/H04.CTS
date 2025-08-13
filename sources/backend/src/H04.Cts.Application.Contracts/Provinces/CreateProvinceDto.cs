using System;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Provinces
{
    public class CreateProvinceDto : AuditedEntityDto<Guid>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public ProvinceType Type { get; set; }

        public int? SortOrder { get; set; }

        public string Description { get; set; }
    }
}
