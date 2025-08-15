using AutoMapper;
using H04.Cts.DanhMucs;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;

namespace H04.Cts;

public class CtsApplicationAutoMapperProfile : Profile
{
    public CtsApplicationAutoMapperProfile()
    {
        CreateMap<ToChuc, ToChucDto>();
        CreateMap<CreateUpdateToChucDto, ToChuc>();

        CreateMap<CapCoQuan, CapCoQuanDto>().ReverseMap();
        CreateMap<CreateUpdateCapCoQuanDto, CapCoQuan>().ReverseMap();

        CreateMap<CtsVaThietBi, CtsVaThietBiDto>().ReverseMap();
        CreateMap<CreateUpdateCtsVaThietBiDto, CtsVaThietBi>().ReverseMap();
    }
}