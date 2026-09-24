using Hangfire;
using Hangfire.MemoryStorage;
using JobService.Application.Abstractions;
using JobService.Application.Jobs.Commands;
using JobService.Application.Jobs.Queries;
using JobService.Infrastructure;
using JobService.Infrastructure.RecurringJobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IJobRepository, InMemoryJobRepository>();
builder.Services.AddScoped<CreateJobHandler>();
builder.Services.AddScoped<CloseJobHandler>();
builder.Services.AddScoped<GetJobsHandler>();
builder.Services.AddScoped<AutoCloseOldJobsJob>();

builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<AutoCloseOldJobsJob>(
    "auto-close-old-jobs",
    job => job.Run(),
    Cron.Daily);

app.MapGet("/api/jobs", (GetJobsHandler handler) =>
    Results.Ok(handler.Handle(new GetJobsQuery())));

app.MapPost("/api/jobs", (CreateJobRequest request, CreateJobHandler handler) =>
{
    try
    {
        var job = handler.Handle(new CreateJobCommand(request.Title));
        return Results.Created($"/api/jobs/{job.Id}", job);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.MapPost("/api/jobs/{id:guid}/close", (Guid id, CloseJobHandler handler) =>
    handler.Handle(new CloseJobCommand(id))
        ? Results.NoContent()
        : Results.NotFound(new { error = "Job not found." }));

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();

public sealed record CreateJobRequest(string Title);
