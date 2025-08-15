using Volo.Abp.Application.Dtos;

namespace H04.Cts.DanhMucs.CTSVaThietBis.Dtos
{
    public class ListCTSVaThietBiRequestDto : PagedAndSortedResultRequestDto
    {
        public string? FilterString { get; set; }
    }
}
