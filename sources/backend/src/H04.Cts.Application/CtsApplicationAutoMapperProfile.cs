using AutoMapper;
using H04.Cts.DanhMucs;
using H04.Cts.DanhMucs.LoaiThietBiDichVuPhanMems.Dtos;
using H04.Cts.DanhMucs.ThietBiDichVuPhanMems.Dtos;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;

namespace H04.Cts;

public class CtsApplicationAutoMapperProfile : Profile
{
    public CtsApplicationAutoMapperProfile()
    {
        CreateMap<ToChuc, ToChucDto>();
        CreateMap<CreateUpdateToChucDto, ToChuc>();

        CreateMap<ThietBiDichVuPhanMem, ThietBiDichVuPhanMemDto>();
        CreateMap<CreateUpdateThietBiDichVuPhanMemDto, ThietBiDichVuPhanMem>();

        CreateMap<LoaiThietBiDichVuPhanMem, LoaiThietBiDichVuPhanMemDto>();
        CreateMap<CreateUpdateLoaiThietBiDichVuPhanMemDto, LoaiThietBiDichVuPhanMem>();
    }
}