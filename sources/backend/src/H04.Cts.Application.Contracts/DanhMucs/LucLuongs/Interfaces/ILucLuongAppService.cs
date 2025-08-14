using H04.Cts.Dtos.DanhMucs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using System.Threading.Tasks;

namespace H04.Cts.Application.DanhMucs;

public interface ILucLuongAppService : ICrudAppService<LucLuongDto, long, GetLucLuongListDto, CreateUpdateLucLuongDto>
{
    Task SoftDeleteAsync(long id);
}
