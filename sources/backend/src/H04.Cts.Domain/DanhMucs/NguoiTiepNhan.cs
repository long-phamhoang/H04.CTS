using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace H04.Cts.Entities.DanhMucs;

public class NguoiTiepNhan : FullAuditedAggregateRoot<long>
{
    public virtual long? OrganizationId { get; set; }

    [ForeignKey(nameof(OrganizationId))]
    public ToChuc OrganizationFk { get; set; }

    [StringLength(CommonConsts.FullNameMaxLength)]
    public virtual string FullName { get; set; }

    [StringLength(CommonConsts.CCCDMaxLength)]
    public virtual string CCCD { get; set; }

    public virtual DateTime DateOfIssue { get; set; }

    public virtual long? NoiCapCCCDId { get; set; }

    [ForeignKey(nameof(NoiCapCCCDId))]
    public NoiCapCCCD? NoiCapCCCDFk { get; set; }

    public virtual string Position { get; set; }

    [StringLength(CommonConsts.PhoneMaxLength)]
    [RegularExpression(CommonConsts.PhonePattern)]
    public virtual string Phone { get; set; }

    [StringLength(CommonConsts.EmailMaxLength)]
    [RegularExpression(CommonConsts.EmailPattern)]
    public virtual string? Email { get; set; }

    [StringLength(CommonConsts.SubmissionAddressMaxLength)]
    public virtual string SubmissionAddress { get; set; }

    [StringLength(CommonConsts.ProvinceMaxLength)]
    public virtual string Province {get; set;}

    [StringLength(CommonConsts.WardMaxLength)]
    public virtual string? Ward {get; set;}

    public virtual bool IsDefault {get; set;}

    public virtual bool IsDeleted {get; set;}

    public virtual string? DeletedBy {get; set;}

    public virtual DateTime? DeletedAt {get; set;}
}