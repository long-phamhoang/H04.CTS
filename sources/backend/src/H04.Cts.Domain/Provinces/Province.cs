using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H04.Cts.Wards;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.Provinces
{
    public class Province : AuditedAggregateRoot<Guid>
    {
        //[Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public ProvinceType Type { get; set; }

        public int? sortOrder { get; set; }
        public string? Description { get; set; }

        public ICollection<Ward> Wards { get; set; }
    }
}
