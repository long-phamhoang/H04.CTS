using H04.Cts.DanhMucs.LoaiThietBiDichVuPhanMems.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace H04.Cts.DanhMucs.LoaiThietBiDichVuPhanMems.Interfaces;
public interface ILoaiThietBiDichVuPhanMemAppService : ICrudAppService<LoaiThietBiDichVuPhanMemDto, long, PagedAndSortedResultRequestDto, CreateUpdateLoaiThietBiDichVuPhanMemDto>
{
}
