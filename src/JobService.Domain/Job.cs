namespace JobService.Domain;

public enum JobStatus
{
    Open,
    Closed
}

public sealed class Job
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public JobStatus Status { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? ClosedAtUtc { get; private set; }

    public Job(Guid id, string title, DateTime createdAtUtc)
    {
        Id = id;
        Title = title;
        Status = JobStatus.Open;
        CreatedAtUtc = createdAtUtc;
    }

    public void Close(DateTime closedAtUtc)
    {
        if (Status == JobStatus.Closed) return;
        Status = JobStatus.Closed;
        ClosedAtUtc = closedAtUtc;
    }
}
