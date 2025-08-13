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
        public const string TrichYeu = GroupName + ".TrichYeu";
        public const string TrichYeuCreate = TrichYeu + ".Create";
        public const string TrichYeuEdit = TrichYeu + ".Edit";
        public const string TrichYeuDelete = TrichYeu + ".Delete";
        public const string MangCTS = GroupName + ".MangCTS";
        public const string MangCTSCreate = MangCTS + ".Create";
        public const string MangCTSEdit = MangCTS + ".Edit";
        public const string MangCTSDelete = MangCTS + ".Delete";
    }
}