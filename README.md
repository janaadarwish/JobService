# JobService — Final Backend Project

A small interview-ready ASP.NET Core backend that combines:

- Clean Architecture
- CQRS-style request/handler separation
- REST API + Swagger
- Hangfire recurring jobs
- A practical recurring task: automatically close jobs older than 7 days
- Git workflow and engineering documentation

## Project idea

**Job Service** manages simple jobs.

A job starts as `Open`. The system exposes an API for creating, listing, and closing jobs. A Hangfire recurring job runs every day and automatically closes jobs that have been open for more than 7 days.

The project intentionally stays small so the architecture is easy to explain in an interview.

## Architecture

```text
JobService.Api
    |
    +--> JobService.Application
    |       |
    |       +--> Commands
    |       +--> Queries
    |       +--> Abstractions
    |
    +--> JobService.Infrastructure
            |
            +--> Repository
            +--> Hangfire recurring job

JobService.Domain
    |
    +--> Job entity
    +--> JobStatus
```

### Clean Architecture responsibilities

- **Domain:** business entity and rules.
- **Application:** use cases, CQRS requests/handlers, repository abstraction.
- **Infrastructure:** repository implementation and Hangfire job.
- **API:** HTTP endpoints, Swagger, dependency injection, Hangfire dashboard.

## CQRS

The application separates reads and writes:

- `GetJobsQuery` → read all jobs.
- `CreateJobCommand` → create a job.
- `CloseJobCommand` → close a job.

A full event-sourcing system is unnecessary for this small service, so CQRS is intentionally lightweight.

## Hangfire

Dashboard:

`http://localhost:5000/hangfire`

Swagger:

`http://localhost:5000/swagger`

Recurring job:

`auto-close-old-jobs`

Schedule:

`Cron.Daily`

The job finds open jobs older than seven days and closes them.

## Run

Requirements:

- .NET 8 SDK

From the repository root:

```bash
dotnet restore
dotnet run --project src/JobService.Api
```

Then open:

- Swagger: http://localhost:5000/swagger
- Hangfire: http://localhost:5000/hangfire
- Health: http://localhost:5000/health

## API examples

### Create

```http
POST /api/jobs
Content-Type: application/json

{
  "title": "Review candidate applications"
}
```

### List

```http
GET /api/jobs
```

### Close

```http
POST /api/jobs/{id}/close
```

## Important note

This final-project version uses Hangfire MemoryStorage and an in-memory repository to keep setup simple. In a production deployment, the repository and Hangfire storage should use persistent infrastructure such as PostgreSQL or SQL Server.

## Definition of Done

- API starts successfully.
- Swagger exposes the endpoints.
- Jobs can be created and listed.
- Jobs can be closed.
- Hangfire dashboard is available.
- Recurring job appears in Hangfire's Recurring Jobs section.
- Architecture and workflow are documented.
- No secrets are committed.
