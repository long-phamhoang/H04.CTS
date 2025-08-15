using H04.Cts.DanhMucs.CtsVaThietBis.Dtos;
using H04.Cts.Dtos.DanhMucs;
using Volo.Abp.Application.Services;

namespace H04.Cts.Application.DanhMucs;

public interface ICtsVaThietBiAppService : ICrudAppService<CtsVaThietBiDto, long, SearchCtsSVaThietBiInput, CreateUpdateCtsVaThietBiDto>
{

}