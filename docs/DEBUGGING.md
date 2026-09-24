# Debugging Notes

## Problem

A recurring task can appear to be configured correctly but still fail to update jobs.

## Debugging approach

1. Confirm the API starts.
2. Open `/hangfire`.
3. Check the `Recurring Jobs` section.
4. Confirm `auto-close-old-jobs` exists.
5. Check the job execution history.
6. Inspect the job repository state.
7. Reproduce the business rule with an old open job.
8. Run the job again and verify that its status becomes `Closed`.

## Key lesson

Debugging should verify the failure path and the observable system state instead of changing code randomly.
