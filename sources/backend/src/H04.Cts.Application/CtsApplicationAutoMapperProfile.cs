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
        CreateMap<NguoiTiepNhan, NguoiTiepNhanDto>()
            .ForMember(d => d.OrganizationIds, opt => opt.MapFrom(s => s.Organizations != null ? s.Organizations.Select(o => o.Id).ToArray() : Array.Empty<long>()))
            .ForMember(d => d.OrganizationNames, opt => opt.MapFrom(s => s.Organizations != null ? s.Organizations.Select(o => o.TenToChuc ?? string.Empty).ToArray() : Array.Empty<string>()));
        CreateMap<CreateUpdateNguoiTiepNhanDto, NguoiTiepNhan>()
            .ForMember(d => d.Organizations, opt => opt.Ignore());
    }
}