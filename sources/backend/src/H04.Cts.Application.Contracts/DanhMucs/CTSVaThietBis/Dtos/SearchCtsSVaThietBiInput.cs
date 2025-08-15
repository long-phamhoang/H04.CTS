using Volo.Abp.Application.Dtos;

namespace H04.Cts.DanhMucs.CtsVaThietBis.Dtos
{
    public class SearchCtsSVaThietBiInput : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
