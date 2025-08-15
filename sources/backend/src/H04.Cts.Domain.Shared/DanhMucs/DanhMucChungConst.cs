namespace H04.Cts.DanhMucs;
public class DanhMucChungConst
{
    public const int MaSoChungMaxLength = 16;

    public const int TenDanhMucChungMaxLength = 256;

    public const int DienThoaiMaxLength = 16;

    public const string DienThoaiPattern = @"^(0(3|5|7|8|9)\d{8,9})?$";

    public const int DiaChiThuCongVuMaxLength = 256;

    public const string DiaChiThuCongVuPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    public const int GhiChuMaxLength = 1024;
}
