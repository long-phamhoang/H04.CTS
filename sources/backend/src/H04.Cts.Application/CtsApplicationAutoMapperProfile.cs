using AutoMapper;
using H04.Cts.DanhMucs.DieuKienCapCTSTheoLL.Dto;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;

namespace H04.Cts;

public class CtsApplicationAutoMapperProfile : Profile
{
    public CtsApplicationAutoMapperProfile()
    {
        CreateMap<ToChuc, ToChucDto>();
        CreateMap<CreateUpdateToChucDto, ToChuc>();

        CreateMap<LucLuong, LucLuongDto>();
        CreateMap<CreateUpdateLucLuongDto, LucLuong>();

        CreateMap<DieuKienCapCTSTheoLL, DieuKienCapCTSTheoLLDto>();
        CreateMap<DieuKienCapCTSTheoLL_CreateUpdateDto, DieuKienCapCTSTheoLL>();
    }
}