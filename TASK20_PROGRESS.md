# Task 20 Progress

## Scope

- DataTables integration for remote, paged data.
- AutoMapper mappings demonstrated in the lectures.
- Toastr success and error messages.
- Advertisement listing and Create operation.

## Required Lectures

1. `07-Part2.wmv` - DataTables, AutoMapper, and Toastr.
2. `08-Part1.wmv` - Advertisement Create operation.

## Git Workflow

- Base branch: `main`.
- Feature branch: `feature/task20-ads-datatable`.
- Baseline commit: `3a8dd57 chore: initialize task 20 from task 19`.

## Current Status

- Created a separate Task 20 project folder from the completed Task 19 state.
- Reviewed both required lectures in the specified order.
- Implemented remote DataTables for users and advertisements.
- Added the AutoMapper mappings used by the user and advertisement services.
- Added the shared JSON response helpers, Toastr/Ajax workflow, and exception handler.
- Implemented the advertisement Create flow for a new or existing advertisement owner.
- Implemented advertisement listing and soft delete.
- Registered the advertisement service and exception handler in `Program.cs`.

## Verification

- `dotnet restore CMS.sln`: passed.
- `dotnet build CMS.sln --configuration Debug --no-restore`: passed with 0 warnings and 0 errors.
- Local application startup: passed on `http://127.0.0.1:5188`.
- Home page HTTP response: `200 OK`.

## Compatibility Notes

- The project targets .NET 10, so controller references use an alias for the instructor's `Results` helper to avoid a name collision with `Microsoft.AspNetCore.Http.Results`.
- The exception middleware uses `System.Text.Json`, which is already included in the target framework, while preserving the JSON response shape demonstrated in the lecture.

