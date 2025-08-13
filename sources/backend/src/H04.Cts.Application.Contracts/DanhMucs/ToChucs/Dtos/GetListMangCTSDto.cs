using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class GetListMangCTSDto : PagedAndSortedResultRequestDto 
{
    public string? FilterInput { get; set; }
}