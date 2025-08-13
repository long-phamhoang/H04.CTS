using System;
using System.Threading.Tasks;
using H04.Cts.Dtos.DanhMucs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace H04.Cts.Application.DanhMucs;

public interface IMangCTSAppService : ICrudAppService<MangCTSDto, long, GetListMangCTSDto, CreateUpdateMangCTSDto>
{
    Task<bool> EnsureExistsByMaMangCTSAsync(string MaMangCTS);
}
