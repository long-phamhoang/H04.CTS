using AutoMapper;
using H04.Cts.DanhMucs;
using H04.Cts.DanhMucs.ChucVucs.Dtos;
using H04.Cts.DanhMucs.Dtos;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;

namespace H04.Cts;

public class CtsApplicationAutoMapperProfile : Profile
{
    public CtsApplicationAutoMapperProfile()
    {
        CreateMap<ToChuc, ToChucDto>();
        CreateMap<CreateUpdateToChucDto, ToChuc>();

        CreateMap<ChucVu, ChucVuDto>();
        CreateMap<CreateUpdateChucVuDto, ChucVu>();

        CreateMap<ThueBaoCaNhan, ThueBaoCaNhanDto>()
            .ForMember(dest => dest.TenChucVu, opt => opt.MapFrom(src => src.ChucVuFk.TenChucVu))
            .ForMember(dest => dest.TenToChuc, opt => opt.MapFrom(src => src.ToChucFk.TenToChuc)); ;

        CreateMap<CreateUpdateThueBaoCaNhanDto, ThueBaoCaNhan>();
    }
}