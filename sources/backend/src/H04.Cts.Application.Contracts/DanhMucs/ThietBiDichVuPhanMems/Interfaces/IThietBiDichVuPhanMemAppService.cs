using H04.Cts.DanhMucs.ThietBiDichVuPhanMems.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace H04.Cts.DanhMucs.ThietBiDichVuPhanMems.Interfaces;

public interface IThietBiDichVuPhanMemAppService : ICrudAppService<ThietBiDichVuPhanMemDto, long, PagedAndSortedResultRequestDto, CreateUpdateThietBiDichVuPhanMemDto>
{

}

