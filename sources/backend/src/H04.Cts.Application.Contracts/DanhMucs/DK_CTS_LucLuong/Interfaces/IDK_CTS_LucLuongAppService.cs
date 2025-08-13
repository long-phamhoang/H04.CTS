using H04.Cts.DanhMucs.DieuKienCapCTSTheoLL.Dto;
using H04.Cts.Dtos.DanhMucs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace H04.Cts.DanhMucs.DieuKienCapCTSTheoLL.Interfaces
{

    public interface IDkCtsLucLuongAppService : ICrudAppService<DieuKienCapCTSTheoLLDto, long, PagedAndSortedResultRequestDto, DieuKienCapCTSTheoLL_CreateUpdateDto>
    {
    }
}
