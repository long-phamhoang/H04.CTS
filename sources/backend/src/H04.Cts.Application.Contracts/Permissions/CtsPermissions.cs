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

        public const string LoaiHoSo = GroupName + ".LoaiHoSo";
        public const string LoaiHoSoCreate = LoaiHoSo + ".Create";
        public const string LoaiHoSoEdit = LoaiHoSo + ".Edit";
        public const string LoaiHoSoDelete = LoaiHoSo + ".Delete";

        public const string LoaiCTS = GroupName + ".LoaiCTS";
        public const string LoaiCTSCreate = LoaiCTS + ".Create";
        public const string LoaiCTSEdit = LoaiCTS + ".Edit";
        public const string LoaiCTSDelete = LoaiCTS + ".Delete";
    }
}