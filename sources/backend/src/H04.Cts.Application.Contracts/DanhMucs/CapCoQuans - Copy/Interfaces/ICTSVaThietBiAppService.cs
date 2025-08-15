using H04.Cts.DanhMucs.CTSVaThietBis.Dtos;
using H04.Cts.Dtos.DanhMucs;
using Volo.Abp.Application.Services;

namespace H04.Cts.Application.DanhMucs;

public interface ICTSVaThietBiAppService : ICrudAppService<CTSVaThietBiDto, long, ListCTSVaThietBiRequestDto, CreateUpdateCTSVaThietBiDto>
{

}