using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace H04.Cts.DanhMucs;
public interface IBaseDanhMuc<TGetOutputDto, TKey, in TGetListInput, in TCreateUpdateInputDto>
    : ICrudAppService<TGetOutputDto, TKey, TGetListInput, TCreateUpdateInputDto>
{
    /// <summary>
    /// Kiểm tra tồn tại Mã số
    /// </summary>
    /// <param name="maSo"></param>
    /// <returns></returns>
    Task<bool> CheckExistsMaSoAsync(string maSo);

    /// <summary>
    /// Xóa mềm
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task SoftDeleteAsync(TKey id);

    /// <summary>
    /// Xóa hàng loạt
    /// </summary>
    /// <param name="ids"></param>
    /// <returns>Danh sách ID xảy ra lỗi không xóa được</returns>
    Task<IList<long>> BulkDeleteAsync(IEnumerable<TKey> ids);
}
