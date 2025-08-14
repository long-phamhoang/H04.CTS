using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using H04.Cts.Dtos.DanhMucs;

namespace H04.Cts.Application.DanhMucs;

public interface INguoiTiepNhanAppService :
    ICrudAppService< //Defines CRUD methods
        NguoiTiepNhanDto, //Used to show books
        long, //Primary key of the book entity
        GetNguoiTiepNhanListDto, //Used for paging/sorting/filtering
        CreateUpdateNguoiTiepNhanDto> //Used to create/update a book
{

}