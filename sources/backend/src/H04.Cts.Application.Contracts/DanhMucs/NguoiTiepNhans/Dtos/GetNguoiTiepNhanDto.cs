using System;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class GetNguoiTiepNhanListDto : PagedAndSortedResultRequestDto
{   
    public string? Keyword { get; set; }

    public long? OrganizationId { get; set; }

    public string? FullName { get; set; }

    public string? CCCD { get; set; }

    public DateTime? DateOfIssue { get; set; }

    public long? NoiCapCCCDId { get; set; }

    public string? Position { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? SubmissionAddress { get; set; }

    public string? Province { get; set; }

    public string? Ward { get; set; }

    public bool? IsDefault { get; set; }

    public bool? IsDeleted { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }
} 