# 02-update-nuget-packages: Update NuGet packages to net10-compatible versions

Identify and upgrade NuGet packages flagged in the assessment (especially `Microsoft.AspNetCore.*`, `Microsoft.EntityFrameworkCore.*`, `Microsoft.Data.SqlClient`, `Newtonsoft.Json` if present). Prefer stable releases that support `net10.0`.

**Done when**: `dotnet restore` succeeds and no package-compatibility issues remain for updated packages.
