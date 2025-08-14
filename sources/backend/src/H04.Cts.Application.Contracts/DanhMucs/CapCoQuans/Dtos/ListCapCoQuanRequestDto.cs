using Volo.Abp.Application.Dtos;

namespace H04.Cts.DanhMucs.CapCoQuans.Dtos
{
    public class ListCapCoQuanRequestDto : PagedAndSortedResultRequestDto
    {
        public string? FilterString { get; set; }
    }
}
