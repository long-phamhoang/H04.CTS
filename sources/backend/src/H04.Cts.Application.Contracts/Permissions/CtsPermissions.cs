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

        public const string LucLuong = GroupName + ".LucLuong";
        public const string LucLuongCreate = LucLuong + ".Create";
        public const string LucLuongEdit = LucLuong + ".Edit";
        public const string LucLuongDelete = LucLuong + ".Delete";

        public const string DK_CTS_LucLuong = GroupName + ".DK_CTS_LucLuong";
        public const string DK_CTS_LucLuongCreate = DK_CTS_LucLuong + ".Create";
        public const string DK_CTS_LucLuongEdit = DK_CTS_LucLuong + ".Edit";
        public const string DK_CTS_LucLuongDelete = DK_CTS_LucLuong + ".Delete";
    }
}