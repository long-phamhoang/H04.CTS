using System;
using System.ComponentModel.DataAnnotations;

namespace H04.Cts.Dtos.DanhMucs;

public class CreateUpdateNoiCapCCCDDto
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string Abbreviation { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string? Province { get; set; }

    public string Note { get; set; } = string.Empty;

    public bool IsActive { get; set; }

}