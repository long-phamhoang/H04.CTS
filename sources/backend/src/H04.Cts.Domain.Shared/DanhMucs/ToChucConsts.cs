namespace H04.Cts.Entities.DanhMucs;

public class ToChucConsts
{
    public const int TenToChucMaxLength = 256;

    public const int MaToChucMaxLength = 16;

    public const int MaSoThueMaxLength = 16;

    public const int DiaChiThuCongVuMaxLength = 256;

    public const string DiaChiThuCongVuPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    public const int DienThoaiMaxLength = 16;

    public const string DienThoaiPattern = @"^(0(3|5|7|8|9)\d{8,9})?$";

    public const int MaQuanHeNganSachMaxLength = 16;

    public const int SoNhaMaxLength = 256;

    public const int CoQuanPhuTrachMaxLength = 256;

    public const int GhiChuMaxLength = 1024;

}