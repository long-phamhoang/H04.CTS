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

        var tichYeus = danhMucs.AddChild(CtsPermissions.DanhMucs.TrichYeu, L("Permission:DanhMucs.TrichYeu"));
        tichYeus.AddChild(CtsPermissions.DanhMucs.TrichYeuCreate, L("Permission:DanhMucs.TrichYeu.Create"));
        tichYeus.AddChild(CtsPermissions.DanhMucs.TrichYeuEdit, L("Permission:DanhMucs.TrichYeu.Edit"));
        tichYeus.AddChild(CtsPermissions.DanhMucs.TrichYeuDelete, L("Permission:DanhMucs.TrichYeu.Delete"));

        var mangCTSs = danhMucs.AddChild(CtsPermissions.DanhMucs.MangHeThongCapCTS, L("Permission:DanhMucs.MangHeThongCapCTS"));
        mangCTSs.AddChild(CtsPermissions.DanhMucs.MangHeThongCapCTSCreate, L("Permission:DanhMucs.MangHeThongCapCTS.Create"));
        mangCTSs.AddChild(CtsPermissions.DanhMucs.MangHeThongCapCTSEdit, L("Permission:DanhMucs.MangHeThongCapCTS.Edit"));
        mangCTSs.AddChild(CtsPermissions.DanhMucs.MangHeThongCapCTSDelete, L("Permission:DanhMucs.MangHeThongCapCTS.Delete"));
        #endregion
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CtsResource>(name);
    }
}