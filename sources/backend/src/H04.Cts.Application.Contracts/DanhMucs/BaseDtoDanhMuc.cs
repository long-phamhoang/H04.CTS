using H04.Cts.Utilities;
using Volo.Abp.Application.Dtos;

namespace H04.Cts.DanhMucs;
public abstract class BaseDtoDanhMuc<Tkey> : AuditedEntityDto<Tkey>
{
    public virtual TrangThai TrangThai { get; set; }

    public virtual string? GhiChu { get; set; }
}
