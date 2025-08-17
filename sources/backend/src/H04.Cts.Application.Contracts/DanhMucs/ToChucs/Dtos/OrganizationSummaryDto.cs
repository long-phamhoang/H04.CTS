using Volo.Abp.Application.Dtos;

namespace H04.Cts.Dtos.DanhMucs;

public class OrganizationSummaryDto : EntityDto<long>
{
    public string Name { get; set; } = string.Empty;
}


