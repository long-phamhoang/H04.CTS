using AutoMapper;
using H04.Cts.DanhMucs;
using H04.Cts.DanhMucs.LoaiCTSs.Dtos;
using H04.Cts.DanhMucs.LoaiHoSos.Dtos;
using H04.Cts.DanhMucs.LoaiHoSos.Interfaces;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;

namespace H04.Cts;

public class CtsApplicationAutoMapperProfile : Profile
{
    public CtsApplicationAutoMapperProfile()
    {
        CreateMap<ToChuc, ToChucDto>();
        CreateMap<CreateUpdateToChucDto, ToChuc>();

        CreateMap<LoaiHoSo, LoaiHoSoDto>();
        CreateMap<CreateUpdateLoaiHoSoDto, LoaiHoSo>();

        CreateMap<LoaiCTS, LoaiCTSDto>();
        CreateMap<CreateUpdateLoaiCTS, LoaiCTS>();
    }
}