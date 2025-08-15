using System;
using System.ComponentModel.DataAnnotations;

namespace H04.Cts.Dtos.DanhMucs;

public class CreateUpdateNguoiTiepNhanDto
{
    public long? OrganizationId { get; set; }

    public string FullName { get; set; } = string.Empty;

    [Required]
    [StringLength(128)]
    public string CCCD { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfIssue { get; set; }

    public long? NoiCapCCCDId { get; set; }

    public string Position { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string? Email { get; set; } = string.Empty;

    public string SubmissionAddress { get; set; } = string.Empty;

    public string Province { get; set; } = string.Empty;

    public string? Ward { get; set; } = string.Empty;

    public bool IsDefault { get; set; }
}