# .NET Version Upgrade Progress

## Overview

Upgrading `Horizons.sln` from net8.0 to net10.0. Plan focuses on TFM updates, NuGet upgrades, EF Core and SqlClient migration, Razor Pages verification, and validation.

**Progress**: 0/7 tasks complete (0%) ![0%](https://progress-bar.xyz/0)

## Tasks

- 🔄 01-update-project-targets: Update project `TargetFramework` to `net10.0`
- 🔲 02-update-nuget-packages: Update NuGet packages to net10-compatible versions
- 🔲 03-migrate-sqlclient-if-needed: Replace `System.Data.SqlClient` with `Microsoft.Data.SqlClient` where detected
- 🔲 04-verify-ef-core-and-migrations: Ensure EF Core compatibility and migration scripts
- 🔲 05-update-razor-and-aspnetcore-config: Verify Razor Pages & middleware compatibility
- 🔲 06-build-and-test: Build solution and run tests
- 🔲 07-integration-validation: Manual/automated smoke tests and deployment validation
