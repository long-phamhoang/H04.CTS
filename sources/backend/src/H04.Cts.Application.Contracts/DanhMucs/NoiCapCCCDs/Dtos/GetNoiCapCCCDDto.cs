using System;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class GetNoiCapCCCDListDto : PagedAndSortedResultRequestDto
{   
    public string? Keyword { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? Abbreviation { get; set; }

    public string? Address { get; set; }

    public string? Province { get; set; }

    public string? Note { get; set; }

    public bool? IsActive { get; set; }
} 