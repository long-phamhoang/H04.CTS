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

        var lucLuongs = danhMucs.AddChild(CtsPermissions.DanhMucs.LucLuong, L("Permission:DanhMucs.LucLuong"));
        lucLuongs.AddChild(CtsPermissions.DanhMucs.LucLuongCreate, L("Permission:DanhMucs.LucLuong.Create"));
        lucLuongs.AddChild(CtsPermissions.DanhMucs.LucLuongEdit, L("Permission:DanhMucs.LucLuong.Edit"));
        lucLuongs.AddChild(CtsPermissions.DanhMucs.LucLuongDelete, L("Permission:DanhMucs.LucLuong.Delete"));

        var dkCtsLucLuongs = danhMucs.AddChild(CtsPermissions.DanhMucs.DK_CTS_LucLuong, L("Permission:DanhMucs.DK_CTS_LucLuong"));
        dkCtsLucLuongs.AddChild(CtsPermissions.DanhMucs.DK_CTS_LucLuongCreate, L("Permission:DanhMucs.DK_CTS_LucLuong.Create"));
        dkCtsLucLuongs.AddChild(CtsPermissions.DanhMucs.DK_CTS_LucLuongEdit, L("Permission:DanhMucs.DK_CTS_LucLuong.Edit"));
        dkCtsLucLuongs.AddChild(CtsPermissions.DanhMucs.DK_CTS_LucLuongDelete, L("Permission:DanhMucs.DK_CTS_LucLuong.Delete"));
        #endregion
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CtsResource>(name);
    }
}