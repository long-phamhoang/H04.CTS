using AutoMapper;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Tinhs;
using H04.Cts.Xas;

namespace H04.Cts;

public class CtsApplicationAutoMapperProfile : Profile
{
    public CtsApplicationAutoMapperProfile()
    {
        CreateMap<ToChuc, ToChucDto>();
        CreateMap<CreateUpdateToChucDto, ToChuc>();
        CreateMap<CreateProvinceDto, Tinh>().PreserveReferences();
        CreateMap<WardDto, Xa>().PreserveReferences();
    }
}