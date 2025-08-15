using H04.Cts.DanhMucs.Dtos;
using H04.Cts.DanhMucs.ThueBaoCaNhans.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace H04.Cts.DanhMucs.ThueBaoCaNhans.Interfaces
{
    public interface IThueBaoCaNhanAppService : ICrudAppService<ThueBaoCaNhanDto, long, GetListThueBaoCaNhanInput, CreateUpdateThueBaoCaNhanDto>
    {

    }
}
