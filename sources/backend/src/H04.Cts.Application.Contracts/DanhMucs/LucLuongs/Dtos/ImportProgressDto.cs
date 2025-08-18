using System;

namespace H04.Cts.Dtos.DanhMucs;

public class ImportProgressDto
{
    public string BatchId { get; set; } = default!;
    public string Status { get; set; } = "Pending"; // Pending, Running, Completed, Failed
    public int TotalRows { get; set; }
    public int ProcessedRows { get; set; }
    public int Percent => TotalRows > 0 ? (int)Math.Floor(ProcessedRows * 100.0 / TotalRows) : 0;
    public string? Message { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
}
