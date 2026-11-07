# Task 21 Progress

## Branch

- `feature/task21-ads-posts-charts`
- Based on local `main` at `197930c` (`Merge Task 20 feature branch`).
- A remote refresh was attempted before implementation, but GitHub was temporarily unreachable from the environment (`Could not resolve host: github.com`). Connectivity later recovered and the completed branch was pushed successfully.
- Pull request: <https://github.com/YahiaAlAfifi/Task-20-DataTables-AutoMapper-Toastr-Ads/pull/2>

## Lecture review

The required lectures were reviewed completely and in the requested order:

1. `08-Part2.wmv` — Advertisement update and Track CRUD.
2. `09-Part1.wmv` — Post CRUD, attachments, status changes, email notification, and content change history.
3. `09-Part2.wmv` — Dashboard counters, Chart.js charts, chart endpoints, and navigation links.

Temporary frames, screenshots, contact sheets, and smoke-test logs were kept outside the Git repository. The generated `lecture-review` directory was verified as untracked analysis output and removed from the repository without deleting any source video or original project file.

## Implemented features

### Advertisement update

- Added `UpdateAdvertisementDto` and AutoMapper mappings.
- Added Advertisement service `Get` and `Update` operations.
- Added GET/POST update controller actions and the modal update form.
- Added the edit action to the Advertisement DataTable.
- Existing image files are retained unless a replacement image is uploaded.

### Track CRUD

- Added Track create/update DTOs and Track/category view models.
- Added category list service used by Track and Post forms.
- Added Track list, create, update, and soft-delete service/controller operations.
- Added Track Index/Create/Update views and server-side DataTable integration.
- Added audio playback from the uploaded `Tracks` directory.
- File upload directories are created automatically when needed.

### Post CRUD and content change log

- Added Post create/update DTOs and Post/attachment/change-log view models.
- Added author list support for users of type `ArticleAuthor`.
- Added Post list, create, update, soft-delete, status update, and history operations.
- Added multi-file attachment upload support.
- Added Approved/Rejected actions and email notification to the post author.
- Added `ContentChangeLog` records for every Post status transition.
- Added the history modal and Post DataTable actions.
- Replaced the placeholder `ContentType.A` value with `Post` and `Track`, preserving the existing stored value `1` for Post.

### Dashboard and charts

- Added Dashboard and chart view models and service registration.
- Added counters for Posts, Users, Tracks, and Advertisements.
- Added user-type and content-type pie charts.
- Added the monthly Post bar chart.
- Added the three JSON chart endpoints in `HomeController`.
- Updated the sidebar links for Posts, Tracks, and Advertisements.

## Commits

- `797e1e3` — Implement advertisement update and track CRUD.
- `23e0118` — Implement post CRUD and content change log.
- `92c3535` — Add dashboard counts and content charts.

## Verification

- `dotnet restore CMS.Web/CMS.Web.csproj -p:NuGetAudit=false --ignore-failed-sources` — passed using the available package cache.
- Debug build — passed with 0 warnings and 0 errors.
- Full Release solution build — passed with 0 warnings and 0 errors.
- HTTP smoke test:
  - `/Track/Index` — 200 with rendered HTML.
  - `/Post/Index` — 200 with rendered HTML.
  - `/Advertisement/Index` — 200 with rendered HTML.
  - Dashboard and chart requests reached the application, but database-backed verification is blocked because the configured local SQL Server database `CMS.Web` cannot be opened for the current Windows account (SQL error 4060).
- No migration was added because Task 21 uses models and `DbSet` entries that were already present; the enum rename preserves the stored integer values and does not change the database schema.
- `git diff --check` — passed.
- No lecture-analysis images, videos, frames, or logs are tracked by Git.

## Remaining local configuration

- Provide or initialize the configured `CMS.Web` database to run data-backed UI CRUD tests locally.
- Configure SMTP credentials to exercise the Post status email notification.
