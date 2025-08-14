using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.Entities.DanhMucs;

public class NoiCapCCCD : FullAuditedAggregateRoot<long>
{
    [StringLength(CommonConsts.FullNameMaxLength)]
    public virtual string Name { get; set; }

    public virtual string Code { get; set; }
    public virtual string Abbreviation { get; set; }

    public virtual string Address { get; set; }

    [StringLength(CommonConsts.ProvinceMaxLength)]
    public virtual string Province {get; set;}

    public virtual string? Note {get; set;}

    public virtual bool IsActive {get; set;}

    public virtual bool IsDeleted {get; set;}

    public virtual string? DeletedBy {get; set;}

    public virtual DateTime? DeletedAt {get; set;}
}