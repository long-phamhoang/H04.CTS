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

        var nguoiTiepNhans = danhMucs.AddChild(CtsPermissions.DanhMucs.NguoiTiepNhan, L("Permission:DanhMucs.NguoiTiepNhan"));
        nguoiTiepNhans.AddChild(CtsPermissions.DanhMucs.NguoiTiepNhanCreate, L("Permission:DanhMucs.NguoiTiepNhan.Create"));
        nguoiTiepNhans.AddChild(CtsPermissions.DanhMucs.NguoiTiepNhanEdit, L("Permission:DanhMucs.NguoiTiepNhan.Edit"));
        nguoiTiepNhans.AddChild(CtsPermissions.DanhMucs.NguoiTiepNhanDelete, L("Permission:DanhMucs.NguoiTiepNhan.Delete"));
        #endregion
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CtsResource>(name);
    }
}