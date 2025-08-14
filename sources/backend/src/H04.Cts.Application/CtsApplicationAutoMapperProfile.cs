using AutoMapper;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;

namespace H04.Cts;

public class CtsApplicationAutoMapperProfile : Profile
{
    public CtsApplicationAutoMapperProfile()
    {
        CreateMap<ToChuc, ToChucDto>();
        CreateMap<CreateUpdateToChucDto, ToChuc>();

        // IssuingAuthority
        CreateMap<NoiCapCCCD, NoiCapCCCDDto>();
        CreateMap<CreateUpdateNoiCapCCCDDto, NoiCapCCCD>();

        // Receiver
        CreateMap<NguoiTiepNhan, NguoiTiepNhanDto>();
        CreateMap<CreateUpdateNguoiTiepNhanDto, NguoiTiepNhan>();
    }
}