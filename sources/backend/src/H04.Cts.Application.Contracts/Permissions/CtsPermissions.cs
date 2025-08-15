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

        public const string ChucVu = GroupName + ".ChucVu";
        public const string ChucVuCreate = ChucVu + ".Create";
        public const string ChucVuEdit = ChucVu + ".Edit";
        public const string ChucVuDelete = ChucVu + ".Delete";

        public const string ThueBaoCaNhan = GroupName + ".ThueBaoCaNhan";
        public const string ThueBaoCaNhanCreate = ThueBaoCaNhan + ".Create";
        public const string ThueBaoCaNhanEdit = ThueBaoCaNhan + ".Edit";
        public const string ThueBaoCaNhanDelete = ThueBaoCaNhan + ".Delete";
    }
}