# 01-update-project-targets: Update project `TargetFramework` to `net10.0`

Update each project's `<TargetFramework>` (or `TargetFrameworks`) to `net10.0`. This task ensures the compiler and SDK target match the chosen runtime.

**Done when**: All projects' `.csproj` files reference `net10.0` and solution loads without project-file parse errors.
