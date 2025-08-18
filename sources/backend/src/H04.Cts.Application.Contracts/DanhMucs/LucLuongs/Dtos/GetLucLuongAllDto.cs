using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class GetLucLuongAllDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
    public int? MaxCount { get; set; } = 10000;
}
