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

public class MangCTSRepository : EfCoreRepository<CtsDbContext, MangCTS, long>, IMangCTSRepository
{
    public MangCTSRepository(IDbContextProvider<CtsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<bool> EnsureExistsByMaMangCTSAsync(string MaMangCTS)
    {
        var dbContext = await GetDbContextAsync();
        var mangCTS = await dbContext.Set<MangCTS>()
            .Where(p => p.MaMangCTS == MaMangCTS && p.IsDeleted != true)
            .FirstOrDefaultAsync();
        return mangCTS != null;
    }
}
