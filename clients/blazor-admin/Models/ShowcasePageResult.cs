namespace Blazor.Admin.Models;

public sealed class ShowcasePageResult
{
    public IReadOnlyList<ShowcaseRowModel> Items { get; set; } = [];
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public double ServerProcessingMs { get; set; }
    public int ActualGeneratedRows { get; set; }
    public double ActualGenerationMs { get; set; }
    public double ActualNsPerRow { get; set; }
    public long SimulatedTotalRecords { get; set; }
    public double EstimatedServerMsForSimulatedTotal { get; set; }
    public double EstimatedRowsPerSecond { get; set; }
}

public sealed class ShowcaseRowModel
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool Status { get; set; }
}
