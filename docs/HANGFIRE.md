# Hangfire Research Notes

## What is a recurring job?

A recurring job is a background operation scheduled to execute repeatedly according to a schedule.

## How AddOrUpdate works

`RecurringJob.AddOrUpdate` creates or updates a recurring job using a stable job ID.

Example:

```csharp
RecurringJob.AddOrUpdate<AutoCloseOldJobsJob>(
    "auto-close-old-jobs",
    job => job.Run(),
    Cron.Daily);
```

If the application starts again, the same job ID is updated rather than creating a second logical recurring job.

## Cron expression

A Cron expression describes a schedule.

Examples:

- `Cron.Daily` — once per day.
- `Cron.Hourly` — once per hour.
- `0 9 * * *` — commonly interpreted as 09:00 every day.

The project uses `Cron.Daily` because the stale-job rule does not need minute-by-minute processing.
