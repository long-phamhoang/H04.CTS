using AutoMapper;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Provinces;
using H04.Cts.Wards;

namespace H04.Cts;

public class CtsApplicationAutoMapperProfile : Profile
{
    public CtsApplicationAutoMapperProfile()
    {
        CreateMap<ToChuc, ToChucDto>();
        CreateMap<CreateUpdateToChucDto, ToChuc>();
        CreateMap<CreateProvinceDto, Province>().PreserveReferences();
        CreateMap<WardDto, Ward>().PreserveReferences();
    }
}