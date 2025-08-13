using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H04.Cts.DanhMucs.LoaiCTSs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace H04.Cts.DanhMucs.LoaiCTSs.Interfaces
{
    public interface ILoaiCTSAppService : ICrudAppService<
        LoaiCTSDto,
        long,
        PagedAndSortedResultRequestDto,
        CreateUpdateLoaiCTS>
    {
        Task SoftDeleteAsync(long id);
    }
}
