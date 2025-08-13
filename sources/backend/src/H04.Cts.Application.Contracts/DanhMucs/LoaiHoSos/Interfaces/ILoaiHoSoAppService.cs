using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H04.Cts.DanhMucs.LoaiHoSos.Dtos;
using H04.Cts.Dtos.DanhMucs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace H04.Cts.DanhMucs.LoaiHoSos.Interfaces
{
    public interface ILoaiHoSoAppService : ICrudAppService<
        LoaiHoSoDto,
        long,
        PagedAndSortedResultRequestDto,
        CreateUpdateLoaiHoSoDto>
    {
        Task SoftDeleteAsync(long id);
    }
}
