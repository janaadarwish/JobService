using CloseJobHangfire.Jobs;
using CloseJobHangfire.Repositories;
using CloseJobHangfire.Services;
using Hangfire;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IJobRepository, InMemoryJobRepository>();
builder.Services.AddSingleton<JobService>();
builder.Services.AddTransient<AutoCloseJobsJob>();
builder.Services.AddHangfire(configuration => configuration.UseMemoryStorage());
builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<AutoCloseJobsJob>(
    "auto-close-stale-jobs",
    job => job.ExecuteAsync(),
    "*/5 * * * *",
    new RecurringJobOptions
    {
        TimeZone = TimeZoneInfo.Local
    });

app.MapGet("/", () => Results.Ok(new
{
    message = "Close Job API",
    dashboard = "/hangfire"
}));

app.MapGet("/api/jobs", (JobService service) => Results.Ok(service.GetAll()));

app.MapGet("/api/jobs/{id:int}", (int id, JobService service) =>
{
    var job = service.GetById(id);
    return job is null ? Results.NotFound() : Results.Ok(job);
});

app.MapPost("/api/jobs", (CreateJobRequest request, JobService service) =>
{
    try
    {
        var job = service.Create(request.Title);
        return Results.Created($"/api/jobs/{job.Id}", job);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.Run();

public sealed record CreateJobRequest(string Title);
