using H04.Cts.DanhMucs.CapCoQuans.Dtos;
using H04.Cts.Dtos.DanhMucs;
using Volo.Abp.Application.Services;

namespace H04.Cts.Application.DanhMucs;

public interface ICapCoQuanAppService : ICrudAppService<CapCoQuanDto, long, ListCapCoQuanRequestDto, CreateUpdateCapCoQuanDto>
{

}