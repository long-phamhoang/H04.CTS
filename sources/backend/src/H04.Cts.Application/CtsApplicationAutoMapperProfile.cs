using AutoMapper;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;
using System;
using System.Linq;

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
        CreateMap<ToChuc, OrganizationSummaryDto>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.TenToChuc ?? string.Empty));

        CreateMap<NoiCapCCCD, NoiCapCCCDSummaryDto>();

        CreateMap<NguoiTiepNhan, NguoiTiepNhanDto>()
            .ForMember(d => d.Organizations, opt => opt.MapFrom(s => s.Organizations))
            .ForMember(d => d.NoiCapCCCD, opt => opt.MapFrom(s => s.NoiCapCCCDFk));
        CreateMap<CreateUpdateNguoiTiepNhanDto, NguoiTiepNhan>()
            .ForMember(d => d.Organizations, opt => opt.Ignore());
    }
}