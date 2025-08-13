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

        #region CapCoQuan
        public const string CapCoQuan = GroupName + ".CapCoQuan";
        public const string CapCoQuanCreate = CapCoQuan + ".Create";
        public const string CapCoQuanEdit = CapCoQuan + ".Edit";
        public const string CapCoQuanDelete = CapCoQuan + ".Delete";
        #endregion
    }
}