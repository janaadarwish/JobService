# Close Job API - Hangfire Recurring Jobs

A small ASP.NET Core Web API demonstrating a real Hangfire recurring task.

## Recurring task

The application automatically closes jobs that have remained open for more than 7 days.

The recurring job is registered with:

`RecurringJob.AddOrUpdate<AutoCloseJobsJob>("auto-close-stale-jobs", job => job.ExecuteAsync(), "*/5 * * * *", ...)`

The Cron expression `*/5 * * * *` means the job is scheduled every 5 minutes.

Hangfire checks recurring jobs on a minute-based interval, then enqueues the matching background job for processing.

## Project structure

- `Models/Job.cs` contains the job model and status.
- `Repositories/IJobRepository.cs` defines the repository contract.
- `Repositories/InMemoryJobRepository.cs` stores sample jobs in memory.
- `Services/JobService.cs` contains the auto-close business rule.
- `Jobs/AutoCloseJobsJob.cs` is the Hangfire background job.
- `Program.cs` configures Hangfire, the dashboard, the recurring schedule, and API endpoints.

## Run

Install .NET 8 SDK, then run:

```bash
dotnet restore
dotnet run --project src/CloseJobHangfire
```

Open:

- API: `http://localhost:5000`
- Hangfire Dashboard: `http://localhost:5000/hangfire`

The exact port may be shown by the ASP.NET Core console output.

## Verify the recurring job

1. Start the application.
2. Open `/hangfire`.
3. Select **Recurring Jobs**.
4. Find `auto-close-stale-jobs`.
5. The schedule should show every 5 minutes.
6. The seeded job `Prepare interview questions` is older than 7 days, so the recurring task will close it on its next execution.

You can also use the Dashboard's manual trigger for `auto-close-stale-jobs` to demonstrate the task immediately.

## API examples

Get all jobs:

`GET /api/jobs`

Get one job:

`GET /api/jobs/1`

Create a job:

`POST /api/jobs`

```json
{
  "title": "Create a new backend feature"
}
```

## Packages

- Hangfire.AspNetCore 1.8.25
- Hangfire.MemoryStorage 1.8.1.2
