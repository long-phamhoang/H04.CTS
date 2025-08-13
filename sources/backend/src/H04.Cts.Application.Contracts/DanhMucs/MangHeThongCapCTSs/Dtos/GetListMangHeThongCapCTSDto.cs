using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class GetListMangHeThongCapCTSDto : PagedAndSortedResultRequestDto 
{
    public string? FilterInput { get; set; }
}