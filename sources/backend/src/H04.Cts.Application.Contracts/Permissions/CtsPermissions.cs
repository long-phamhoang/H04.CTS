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

        public const string ThietBiDichVuPhanMem = GroupName + ".ThietBiDichVuPhanMem";
        public const string ThietBiDichVuPhanMemCreate = ThietBiDichVuPhanMem + ".Create";
        public const string ThietBiDichVuPhanMemEdit = ThietBiDichVuPhanMem + ".Edit";
        public const string ThietBiDichVuPhanMemDelete = ThietBiDichVuPhanMem + ".Delete";

        public const string LoaiThietBiDichVuPhanMem = GroupName + ".LoaiThietBiDichVuPhanMem";
        public const string LoaiThietBiDichVuPhanMemCreate = LoaiThietBiDichVuPhanMem + ".Create";
        public const string LoaiThietBiDichVuPhanMemEdit = LoaiThietBiDichVuPhanMem + ".Edit";
        public const string LoaiThietBiDichVuPhanMemDelete = LoaiThietBiDichVuPhanMem + ".Delete";
    }
}