# Space Lasers

This repository contains an ASP.NET Core (`net10.0`) ToDo List app using an
in-memory provider with categories, due dates, and priorities.

## Structure

- `src/webapp` contains the Razor Pages application.
- `src/webapp.tests` contains the xUnit test suite.
- `infra` contains the Azure App Service Bicep deployment.
- `docs` contains project documentation.

## Development

Run the application:

```bash
dotnet run --project src/webapp
```

Build and test the solution:

```bash
dotnet build
dotnet test
```

The test project collects Cobertura coverage and fails when line coverage is
below 40%. Its report is written to
`src/webapp.tests/coverage.cobertura.xml`.
