using System;

namespace H04.Cts.Application.DanhMucs.LucLuongs.Jobs
{
    [Serializable]
    public class ImportLucLuongJobArgs
    {
        public string ObjectName { get; set; } = default!; // MinIO object key
        public string? OriginalFileName { get; set; }
        public string? BatchId { get; set; }
    }
}
