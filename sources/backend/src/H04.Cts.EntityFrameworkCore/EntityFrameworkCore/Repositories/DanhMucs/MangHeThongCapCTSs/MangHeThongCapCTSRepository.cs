using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Repositories.DanhMucs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace H04.Cts.EntityFrameworkCore.Repositories.DanhMucs;

public class MangHeThongCapCTSRepository : EfCoreRepository<CtsDbContext, MangHeThongCapCTS, long>, IMangHeThongCapCTSRepository
{
    public MangHeThongCapCTSRepository(IDbContextProvider<CtsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<bool> EnsureExistsByMaMangHeThongCapCTSAsync(string MaMangHeThongCapCTS)
    {
        var dbContext = await GetDbContextAsync();
        var mangCTS = await dbContext.Set<MangHeThongCapCTS>()
            .Where(p => p.MaMangHeThongCapCTS == MaMangHeThongCapCTS && p.IsDeleted != true)
            .FirstOrDefaultAsync();
        return mangCTS != null;
    }
}
