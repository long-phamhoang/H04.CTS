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

        var tinhThanhPhos = danhMucs.AddChild(CtsPermissions.DanhMucs.TinhThanhPho, L("Permission:DanhMucs.TinhThanhPho"));
        tinhThanhPhos.AddChild(CtsPermissions.DanhMucs.TinhThanhPhoCreate, L("Permission:DanhMucs.TinhThanhPho.Create"));
        tinhThanhPhos.AddChild(CtsPermissions.DanhMucs.TinhThanhPhoEdit, L("Permission:DanhMucs.TinhThanhPho.Edit"));
        tinhThanhPhos.AddChild(CtsPermissions.DanhMucs.TinhThanhPhoDelete, L("Permission:DanhMucs.TinhThanhPho.Delete"));

        var xaPhuongs = danhMucs.AddChild(CtsPermissions.DanhMucs.XaPhuong, L("Permission:DanhMucs.XaPhuong"));
        xaPhuongs.AddChild(CtsPermissions.DanhMucs.XaPhuongCreate, L("Permission:DanhMucs.XaPhuong.Create"));
        xaPhuongs.AddChild(CtsPermissions.DanhMucs.XaPhuongEdit, L("Permission:DanhMucs.XaPhuong.Edit"));
        xaPhuongs.AddChild(CtsPermissions.DanhMucs.XaPhuongDelete, L("Permission:DanhMucs.XaPhuong.Delete"));
        #endregion
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CtsResource>(name);
    }
}