using H04.Cts.Entities.DanhMucs;
using H04.Cts.Utilities;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.Data.DanhMucs;

public class ToChucDataSeederContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<ToChuc, long> _toChucRepository;

    public ToChucDataSeederContributor(IRepository<ToChuc, long> bookRepository)
    {
        _toChucRepository = bookRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        if (await _toChucRepository.GetCountAsync() <= 0)
        {
            await _toChucRepository.InsertAsync(
                new ToChuc
                {
                    TenToChuc = @"Tổ chức A",
                    MaToChuc = "A",
                    TrangThai = TrangThai.HoatDong,
                },
                autoSave: true
            );

            await _toChucRepository.InsertAsync(
                new ToChuc
                {
                    ToChucCapTrenId = 1,
                    TenToChuc = @"Tổ chức A1",
                    MaToChuc = "A1",
                    TrangThai = TrangThai.HoatDong,
                },
                autoSave: true
            );
        }
    }
}