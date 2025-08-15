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

        public const string TinhThanhPho = GroupName + ".TinhThanhPho";
        public const string TinhThanhPhoCreate = TinhThanhPho + ".Create";
        public const string TinhThanhPhoEdit = TinhThanhPho + ".Edit";
        public const string TinhThanhPhoDelete = TinhThanhPho + ".Delete";

        public const string XaPhuong = GroupName + ".XaPhuong";
        public const string XaPhuongCreate = XaPhuong + ".Create";
        public const string XaPhuongEdit = XaPhuong + ".Edit";
        public const string XaPhuongDelete = XaPhuong + ".Delete";
    }
}