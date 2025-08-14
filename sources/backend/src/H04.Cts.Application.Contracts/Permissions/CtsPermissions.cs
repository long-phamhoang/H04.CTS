namespace H04.Cts.Permissions;

public static class CtsPermissions
{
    public const string AppName = "Cts";

    public static class DanhMucs
    {
        public const string GroupName = AppName + ".DanhMucs";

        public const string ToChuc = GroupName + ".ToChuc";
        public const string ToChucCreate = ToChuc + ".Create";
        public const string ToChucEdit = ToChuc + ".Edit";
        public const string ToChucDelete = ToChuc + ".Delete";

        public const string NguoiTiepNhan = GroupName + ".NguoiTiepNhan";
        public const string NguoiTiepNhanCreate = NguoiTiepNhan + ".Create";
        public const string NguoiTiepNhanEdit = NguoiTiepNhan + ".Edit";
        public const string NguoiTiepNhanDelete = NguoiTiepNhan + ".Delete";
    }
}