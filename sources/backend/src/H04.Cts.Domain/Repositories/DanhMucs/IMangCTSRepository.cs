using System;
using System.Threading.Tasks;
using H04.Cts.Entities.DanhMucs;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.Repositories.DanhMucs;

public interface IMangCTSRepository : IRepository<MangCTS, long>
{
    Task<bool> EnsureExistsByMaMangCTSAsync(string MaMangCTS);
}
