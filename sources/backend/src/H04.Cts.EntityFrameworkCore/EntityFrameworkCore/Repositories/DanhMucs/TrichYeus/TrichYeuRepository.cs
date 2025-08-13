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

public class TrichYeuRepository : EfCoreRepository<CtsDbContext, TrichYeu, long>, ITrichYeuRepository
{
    public TrichYeuRepository(IDbContextProvider<CtsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<bool> EnsureExistsByMaTrichYeuAsync(string MaTrichYeu)
    {
        var dbContext = await GetDbContextAsync();
        var trichYeu = await dbContext.Set<TrichYeu>()
            .Where(p => p.MaTrichYeu == MaTrichYeu && p.IsDeleted != true)
            .FirstOrDefaultAsync();
        return trichYeu != null;
    }
}
