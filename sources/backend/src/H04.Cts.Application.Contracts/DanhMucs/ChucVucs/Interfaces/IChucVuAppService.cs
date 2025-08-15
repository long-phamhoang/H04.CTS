using H04.Cts.DanhMucs.ChucVucs.Dtos;
using H04.Cts.Dtos.DanhMucs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace H04.Cts.DanhMucs.ChucVucs.Interfaces
{
    public interface IChucVuAppService : ICrudAppService<ChucVuDto, long, GetListChucVuInput, CreateUpdateChucVuDto>
    {
        Task<List<ChucVuDto>> GetChucVuForDropDown();
        Task<bool> IsMaChucVuUniqueAsync(string maChucVu);

    }
}
