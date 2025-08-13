using H04.Cts.Dtos.DanhMucs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace H04.Cts.Application.DanhMucs;

public interface IMangHeThongCapCTSAppService : ICrudAppService<MangHeThongCapCTSDto, long, GetListMangHeThongCapCTSDto, CreateUpdateMangHeThongCapCTSDto>
{
    Task<bool> EnsureExistsByMaMangHeThongCapCTSAsync(string MaMangHeThongCapCTS);
    Task SoftDeleteAsync(long id);
    Task MultipleDeleteAsync(List<long> ids);
    Task MultipleCreateAsync(List<CreateUpdateTrichYeuDto> inputs);
}
