namespace H04.Cts.Entities.DanhMucs;

public class CommonConsts
{
    public const int FullNameMaxLength = 256;

    public const int CCCDMaxLength = 12;

    public const int PositionMaxLength = 256;

    public const int PhoneMaxLength = 16;
    public const string PhonePattern = @"^(0(3|5|7|8|9)\d{8,9})?$";

    public const int EmailMaxLength = 256;
    public const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    public const int SubmissionAddressMaxLength = 256;

    public const int ProvinceMaxLength = 256;

    public const int WardMaxLength = 256;

    public const int NoteMaxLength = 1024;

}