using System;
using System.ComponentModel.DataAnnotations;
using H04.Cts.Provinces;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.Wards
{
    public class Ward : AuditedAggregateRoot<Guid>
    {
        //[Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public WardType Type { get; set; }

        public int? sortOrder { get; set; }
        public string? Description { get; set; }

        [Required]
        public Guid ProvinceId { get; set; }
        [Required]        
        public Province Province { get; set; }
    }
}
