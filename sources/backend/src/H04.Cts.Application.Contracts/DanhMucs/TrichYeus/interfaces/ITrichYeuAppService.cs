using System.Collections.Generic;
using System.Threading.Tasks;
using H04.Cts.Dtos.DanhMucs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace H04.Cts.Application.DanhMucs;

public interface ITrichYeuAppService : ICrudAppService<TrichYeuDto, long, GetListTrichYeuDto, CreateUpdateTrichYeuDto>
{
    Task<bool> EnsureExistsByMaTrichYeuAsync(string MaTrichYeu);
    Task SoftDeleteAsync(long id);
    Task MultipleDeleteAsync(List<long> ids);
    Task MultipleCreateAsync(List<CreateUpdateTrichYeuDto> inputs);
}