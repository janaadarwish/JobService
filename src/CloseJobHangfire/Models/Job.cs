namespace CloseJobHangfire.Models;

public enum JobStatus
{
    Open,
    Closed
}

public sealed class Job
{
    public int Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public JobStatus Status { get; set; }
    public DateTime CreatedAtUtc { get; init; }
    public DateTime? ClosedAtUtc { get; set; }
}
