using H04.Cts.DanhMucs.LoaiThietBiDichVuPhanMems.Dtos;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.DanhMucs.LoaiThietBiDichVuPhanMems.Interfaces;
public interface ILoaiThietBiDichVuPhanMemAppService : IBaseDanhMuc<LoaiThietBiDichVuPhanMemDto, long, PagedAndSortedResultRequestDto, CreateUpdateLoaiThietBiDichVuPhanMemDto>
{
}
