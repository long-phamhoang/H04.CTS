using H04.Cts.DanhMucs.ChucVucs.Dtos;
using H04.Cts.Dtos.DanhMucs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace H04.Cts.Application.DanhMucs;

public interface IToChucAppService : ICrudAppService<ToChucDto, long, PagedAndSortedResultRequestDto, CreateUpdateToChucDto>
{
    Task<List<ToChucDto>> GetToChucForDropDown();
}