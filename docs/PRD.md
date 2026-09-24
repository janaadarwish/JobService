# Product Requirements Document

## Problem

Teams need a simple way to track jobs while automatically cleaning up jobs that remain open too long.

## Goal

Build a small backend service that supports job management and automatically closes stale jobs.

## Users

- Backend engineers
- Internal operations users
- Interview reviewers evaluating the service

## Functional requirements

1. Create a job with a title.
2. List jobs.
3. Close a job manually.
4. Automatically close open jobs older than 7 days.
5. Expose Swagger API documentation.
6. Provide a Hangfire dashboard for background-job visibility.

## Non-functional requirements

- Clear separation of responsibilities.
- Simple request/handler CQRS structure.
- Testable application logic.
- No secrets in source control.
- Easy local setup.

## Success criteria

The service starts with one command, the API works through Swagger, and the recurring job is visible in Hangfire.
