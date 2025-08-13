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
    public class LoaiHoSoDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<LoaiHoSo, long> _loaiHoSoRepository;
        public LoaiHoSoDataSeederContributor(IRepository<LoaiHoSo, long> loaiHoSoRepository)
        {
            _loaiHoSoRepository = loaiHoSoRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if(await _loaiHoSoRepository.GetCountAsync() <= 0)
            {
                await _loaiHoSoRepository.InsertManyAsync(new List<LoaiHoSo>()
                {
                    new LoaiHoSo()
                    {
                        MaLoaiHoSo = "HS01",
                        TenLoaiHoSo = "Hồ sơ loại 01",
                        TrangThai = TrangThai.HoatDong
                    },
                    new LoaiHoSo()
                    {
                        MaLoaiHoSo = "HS02",
                        TenLoaiHoSo = "Hồ sơ loại 02",
                        TrangThai = TrangThai.HoatDong
                    },
                    new LoaiHoSo()
                    {
                        MaLoaiHoSo = "HS03",
                        TenLoaiHoSo = "Hồ sơ loại 03",
                        TrangThai = TrangThai.HoatDong
                    },
                    new LoaiHoSo()
                    {
                        MaLoaiHoSo = "HS04",
                        TenLoaiHoSo = "Hồ sơ loại 04",
                        TrangThai = TrangThai.HoatDong
                    },
                    new LoaiHoSo()
                    {
                        MaLoaiHoSo = "HS05",
                        TenLoaiHoSo = "Hồ sơ loại 05",
                        TrangThai = TrangThai.HoatDong
                    }
                },
                autoSave: true);
            }
        }
    }
}
