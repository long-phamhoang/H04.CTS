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

        var chucVus = danhMucs.AddChild(CtsPermissions.DanhMucs.ChucVu, L("Permission:DanhMucs.ChucVu"));
        chucVus.AddChild(CtsPermissions.DanhMucs.ChucVuCreate, L("Permission:DanhMucs.ChucVu.Create"));
        chucVus.AddChild(CtsPermissions.DanhMucs.ChucVuEdit, L("Permission:DanhMucs.ChucVu.Edit"));
        chucVus.AddChild(CtsPermissions.DanhMucs.ChucVuDelete, L("Permission:DanhMucs.ChucVu.Delete"));        
        
        var thueBaoCaNhans = danhMucs.AddChild(CtsPermissions.DanhMucs.ThueBaoCaNhan, L("Permission:DanhMucs.ThueBaoCaNhan"));
        thueBaoCaNhans.AddChild(CtsPermissions.DanhMucs.ThueBaoCaNhanCreate, L("Permission:DanhMucs.ThueBaoCaNhan.Create"));
        thueBaoCaNhans.AddChild(CtsPermissions.DanhMucs.ThueBaoCaNhanEdit, L("Permission:DanhMucs.ThueBaoCaNhan.Edit"));
        thueBaoCaNhans.AddChild(CtsPermissions.DanhMucs.ThueBaoCaNhanDelete, L("Permission:DanhMucs.ThueBaoCaNhan.Delete"));

        #endregion
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CtsResource>(name);
    }
}