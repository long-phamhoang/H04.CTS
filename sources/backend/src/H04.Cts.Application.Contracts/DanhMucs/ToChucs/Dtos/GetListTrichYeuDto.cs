using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class GetListTrichYeuDto : PagedAndSortedResultRequestDto 
{
    public string? FilterInput { get; set; }
}