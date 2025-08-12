using H04.Cts.Dtos.DanhMucs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace H04.Cts.Application.DanhMucs;

public interface IToChucAppService : ICrudAppService<ToChucDto, long, PagedAndSortedResultRequestDto, CreateUpdateToChucDto>
{

}