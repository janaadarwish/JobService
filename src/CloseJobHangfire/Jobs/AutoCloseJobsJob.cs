using CloseJobHangfire.Services;

namespace CloseJobHangfire.Jobs;

public sealed class AutoCloseJobsJob
{
    private readonly JobService _jobService;
    private readonly ILogger<AutoCloseJobsJob> _logger;

    public AutoCloseJobsJob(JobService jobService, ILogger<AutoCloseJobsJob> logger)
    {
        _jobService = jobService;
        _logger = logger;
    }

    public Task ExecuteAsync()
    {
        var closedCount = _jobService.AutoCloseStaleJobs();
        _logger.LogInformation("Auto-close recurring job completed. Closed {ClosedCount} stale jobs.", closedCount);
        return Task.CompletedTask;
    }
}
