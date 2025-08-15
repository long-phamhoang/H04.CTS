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

        #region CapCoQuans
        var capCoQuans = danhMucs.AddChild(CtsPermissions.DanhMucs.CapCoQuan, L("Permission:DanhMucs.CapCoQuan"));
        capCoQuans.AddChild(CtsPermissions.DanhMucs.CapCoQuanCreate, L("Permission:DanhMucs.CapCoQuan.Create"));
        capCoQuans.AddChild(CtsPermissions.DanhMucs.CapCoQuanEdit, L("Permission:DanhMucs.CapCoQuan.Edit"));
        capCoQuans.AddChild(CtsPermissions.DanhMucs.CapCoQuanDelete, L("Permission:DanhMucs.CapCoQuan.Delete"));
        #endregion

        #region CtsVaThietBis
        var ctsVaThietBis = danhMucs.AddChild(CtsPermissions.DanhMucs.CtsVaThietBi, L("Permission:DanhMucs.CtsVaThietBi"));
        ctsVaThietBis.AddChild(CtsPermissions.DanhMucs.CtsVaThietBiCreate, L("Permission:DanhMucs.CtsVaThietBi.Create"));
        ctsVaThietBis.AddChild(CtsPermissions.DanhMucs.CtsVaThietBiEdit, L("Permission:DanhMucs.CtsVaThietBi.Edit"));
        ctsVaThietBis.AddChild(CtsPermissions.DanhMucs.CtsVaThietBiDelete, L("Permission:DanhMucs.CtsVaThietBi.Delete"));
        #endregion
        #endregion
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CtsResource>(name);
    }
}