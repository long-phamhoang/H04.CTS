using H04.Cts.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace H04.Cts.Permissions;

public class CtsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var app = context.AddGroup(CtsPermissions.AppName);

        #region 1. DanhMucs
        var danhMucs = app.AddPermission(CtsPermissions.DanhMucs.GroupName, L("Permission:DanhMucs"));

        var toChucs = danhMucs.AddChild(CtsPermissions.DanhMucs.ToChuc, L("Permission:DanhMucs.ToChuc"));
        toChucs.AddChild(CtsPermissions.DanhMucs.ToChucCreate, L("Permission:DanhMucs.ToChuc.Create"));
        toChucs.AddChild(CtsPermissions.DanhMucs.ToChucEdit, L("Permission:DanhMucs.ToChuc.Edit"));
        toChucs.AddChild(CtsPermissions.DanhMucs.ToChucDelete, L("Permission:DanhMucs.ToChuc.Delete"));

        var thietBiDichVuPhanMems = danhMucs.AddChild(CtsPermissions.DanhMucs.ThietBiDichVuPhanMem, L("Permission:DanhMucs.ThietBiDichVuPhanMem"));
        thietBiDichVuPhanMems.AddChild(CtsPermissions.DanhMucs.ThietBiDichVuPhanMemCreate, L("Permission:DanhMucs.ThietBiDichVuPhanMem.Create"));
        thietBiDichVuPhanMems.AddChild(CtsPermissions.DanhMucs.ThietBiDichVuPhanMemEdit, L("Permission:DanhMucs.ThietBiDichVuPhanMem.Edit"));
        thietBiDichVuPhanMems.AddChild(CtsPermissions.DanhMucs.ThietBiDichVuPhanMemDelete, L("Permission:DanhMucs.ThietBiDichVuPhanMem.Delete"));

        var loaiThietBiDichVuPhanMems = danhMucs.AddChild(CtsPermissions.DanhMucs.LoaiThietBiDichVuPhanMem, L("Permission:DanhMucs.LoaiThietBiDichVuPhanMem"));
        loaiThietBiDichVuPhanMems.AddChild(CtsPermissions.DanhMucs.LoaiThietBiDichVuPhanMemCreate, L("Permission:DanhMucs.LoaiThietBiDichVuPhanMem.Create"));
        loaiThietBiDichVuPhanMems.AddChild(CtsPermissions.DanhMucs.LoaiThietBiDichVuPhanMemEdit, L("Permission:DanhMucs.LoaiThietBiDichVuPhanMem.Edit"));
        loaiThietBiDichVuPhanMems.AddChild(CtsPermissions.DanhMucs.LoaiThietBiDichVuPhanMemDelete, L("Permission:DanhMucs.LoaiThietBiDichVuPhanMem.Delete"));
        #endregion
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CtsResource>(name);
    }
}