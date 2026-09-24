# API Documentation

## GET /api/jobs

Returns all jobs.

## POST /api/jobs

Creates a job.

Request:

```json
{
  "title": "Review applications"
}
```

Returns `201 Created`.

## POST /api/jobs/{id}/close

Closes the requested job.

Returns `204 No Content` when successful and `404 Not Found` when the ID does not exist.

## GET /health

Returns:

```json
{
  "status": "ok"
}
```

## Background job

Hangfire dashboard:

`/hangfire`

Recurring job:

`auto-close-old-jobs`

Schedule:

daily.
