using System;
using System.Threading.Tasks;
using H04.Cts.Entities.DanhMucs;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.Repositories.DanhMucs;

public interface ITrichYeuRepository : IRepository<TrichYeu, long>
{
    Task<bool> EnsureExistsByMaTrichYeuAsync(string MaTrichYeu);
}   
