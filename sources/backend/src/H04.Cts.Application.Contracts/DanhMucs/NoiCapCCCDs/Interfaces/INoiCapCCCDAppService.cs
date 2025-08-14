using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using H04.Cts.Dtos.DanhMucs;

namespace H04.Cts.Application.DanhMucs;

public interface INoiCapCCCDAppService :
    ICrudAppService< //Defines CRUD methods
        NoiCapCCCDDto, //Used to show books
        long, //Primary key of the book entity
        GetNoiCapCCCDListDto, //Used for paging/sorting/filtering
        CreateUpdateNoiCapCCCDDto> //Used to create/update a book
{

}