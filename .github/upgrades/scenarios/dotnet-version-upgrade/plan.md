# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade solution `Horizons.sln` from `net8.0` to `net10.0`.
**Scope**: 6 projects including a Razor Pages web app. Focus on package compatibility, project TFM updates, EF Core and SqlClient migrations where needed.

## Tasks

### 01-update-project-targets: Update project `TargetFramework` to `net10.0`

Update each project's `<TargetFramework>` (or `TargetFrameworks`) to `net10.0`. This task ensures the compiler and SDK target match the chosen runtime.

**Done when**: All projects' `.csproj` files reference `net10.0` and solution loads without project-file parse errors.

---

### 02-update-nuget-packages: Update NuGet packages to net10-compatible versions

Identify and upgrade NuGet packages flagged in the assessment (especially `Microsoft.AspNetCore.*`, `Microsoft.EntityFrameworkCore.*`, `Microsoft.Data.SqlClient`, `Newtonsoft.Json` if present). Prefer stable releases that support `net10.0`.

**Done when**: `dotnet restore` succeeds and no package-compatibility issues remain for updated packages.

---

### 03-migrate-sqlclient-if-needed: Replace `System.Data.SqlClient` with `Microsoft.Data.SqlClient` where detected

Replace namespaces and package references for projects using `System.Data.SqlClient`. Verify connection string usage and any behavioral differences.

**Done when**: Code compiles and database integration flows run as expected locally.

---

### 04-verify-ef-core-and-migrations: Ensure EF Core compatibility and migration scripts

For projects using EF Core, upgrade `Microsoft.EntityFrameworkCore` packages as needed, verify `DbContext` registrations, and run/validate migrations.

**Done when**: Migrations apply successfully on a test database and runtime queries execute without errors.

---

### 05-update-razor-and-aspnetcore-config: Verify Razor Pages & middleware compatibility

Adjust `Razor` SDK settings, `Program.cs` or `Startup` as needed for endpoint routing, authentication, and TagHelpers. Run the Razor Pages app and validate UI routes and pages.

**Done when**: The web app starts, pages render, and common user flows (login, CRUD) work in local testing.

---

### 06-build-and-test: Build solution and run tests

Run `dotnet build` and unit/integration tests. Fix any compiler/runtime errors introduced by package or TFM changes.

**Done when**: Solution builds successfully and all tests pass (or known failing tests are documented with remediation tasks).

---

### 07-integration-validation: Manual/automated smoke tests and deployment validation

Perform smoke tests against a deployed test environment. Validate EF migrations, database connectivity, and Razor Pages behavior.

**Done when**: Smoke tests pass and deployment artifacts are validated.
