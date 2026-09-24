# Git Workflow

## Branches

Use short feature branches from `main`.

Example:

```text
main
  └── feature/hangfire-auto-close
```

## Commit style

Use small, descriptive commits.

Examples:

```text
feat: add job commands and queries
feat: add hangfire dashboard
feat: add stale job recurring task
docs: add architecture decision record
```

## Pull request flow

1. Create a feature branch.
2. Make one focused change.
3. Run the application locally.
4. Verify Swagger and Hangfire.
5. Commit with a descriptive message.
6. Push the branch.
7. Open a Pull Request.
8. Request review.
9. Address review feedback.
10. Merge after approval.

## Sensitive files

Do not commit passwords, tokens, connection strings, or local secrets.

Use environment variables or local configuration for real deployments.
