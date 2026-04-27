# 03-migrate-sqlclient-if-needed: Replace `System.Data.SqlClient` with `Microsoft.Data.SqlClient` where detected

Replace namespaces and package references for projects using `System.Data.SqlClient`. Verify connection string usage and any behavioral differences.

**Done when**: Code compiles and database integration flows run as expected locally.
