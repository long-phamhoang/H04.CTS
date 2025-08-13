using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H04.Cts.DanhMucs;
using H04.Cts.Utilities;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.Data.DanhMucs
{
    public class LoaiCTSDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<LoaiCTS, long> _loaiCTSRepository;
        public LoaiCTSDataSeederContributor(IRepository<LoaiCTS, long> loaiCTSRepository)
        {
            _loaiCTSRepository = loaiCTSRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if(await _loaiCTSRepository.GetCountAsync() <= 0)
            {
                await _loaiCTSRepository.InsertManyAsync(new List<LoaiCTS>()
                {
                    new LoaiCTS()
                    {
                        MaLoaiCTS = "CTS01",
                        TenLoaiCTS = "Loại chứng thực số 01",
                        TrangThai = TrangThai.HoatDong
                    },
                    new LoaiCTS()
                    {
                        MaLoaiCTS = "CTS02",
                        TenLoaiCTS = "Loại chứng thực số 02",
                        TrangThai = TrangThai.HoatDong
                    },
                    new LoaiCTS()
                    {
                        MaLoaiCTS = "CTS03",
                        TenLoaiCTS = "Loại chứng thực số 03",
                        TrangThai = TrangThai.HoatDong
                    },
                    new LoaiCTS()
                    {
                        MaLoaiCTS = "CTS04",
                        TenLoaiCTS = "Loại chứng thực số 04",
                        TrangThai = TrangThai.HoatDong
                    },
                },
                autoSave: true);
            }
        }
    }
}
