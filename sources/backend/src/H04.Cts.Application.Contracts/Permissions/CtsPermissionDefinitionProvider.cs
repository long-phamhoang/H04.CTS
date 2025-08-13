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

        var documentTypes = danhMucs.AddChild(CtsPermissions.DanhMucs.LoaiHoSo, L("Permission:DanhMucs.LoaiHoSo"));
        documentTypes.AddChild(CtsPermissions.DanhMucs.LoaiHoSoCreate, L("Permission:DanhMucs.LoaiHoSo.Create"));
        documentTypes.AddChild(CtsPermissions.DanhMucs.LoaiHoSoEdit, L("Permission:DanhMucs.LoaiHoSo.Edit"));
        documentTypes.AddChild(CtsPermissions.DanhMucs.LoaiHoSoDelete, L("Permission:DanhMucs.LoaiHoSo.Delete"));

        var digitalAuthen = danhMucs.AddChild(CtsPermissions.DanhMucs.LoaiCTS, L("Permission:DanhMucs.LoaiCTS"));
        digitalAuthen.AddChild(CtsPermissions.DanhMucs.LoaiCTSCreate, L("Permission:DanhMucs.LoaiCTS.Create"));
        digitalAuthen.AddChild(CtsPermissions.DanhMucs.LoaiCTSEdit, L("Permission:DanhMucs.LoaiCTS.Edit"));
        digitalAuthen.AddChild(CtsPermissions.DanhMucs.LoaiCTSDelete, L("Permission:DanhMucs.LoaiCTS.Delete"));
        #endregion
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CtsResource>(name);
    }
}