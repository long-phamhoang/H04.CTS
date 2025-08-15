using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.DanhMucs.ChucVucs.Dtos
{
    public class GetListChucVuInput : PagedAndSortedResultRequestDto
    {
        public string? FilterInput { get; set; }
    }
}
