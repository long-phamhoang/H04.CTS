using AutoMapper;
using H04.Cts.DanhMucs;
using H04.Cts.DanhMucs.TinhTHanhPhos;
using H04.Cts.DanhMucs.XaPhuongs;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;

namespace H04.Cts;

public class CtsApplicationAutoMapperProfile : Profile
{
    public CtsApplicationAutoMapperProfile()
    {
        CreateMap<ToChuc, ToChucDto>();
        CreateMap<CreateUpdateToChucDto, ToChuc>();
        CreateMap<CreateProvinceDto, TinhThanhPho>().PreserveReferences();
        CreateMap<TinhThanhPho,CreateProvinceDto>().PreserveReferences();
        CreateMap<WardDto, XaPhuong>().PreserveReferences();
        CreateMap<XaPhuong, WardDto>().PreserveReferences();
    }
}