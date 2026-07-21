# Space Lasers

Space Lasers is a Razor Pages ToDo application built on ASP.NET Core (`net10.0`).
It is designed as a small, testable sample that demonstrates:

- Form handling and validation with Razor Pages
- In-memory data storage through a repository service
- Task categorization, priority, due dates, and completion toggling
- Unit testing with xUnit and coverage thresholds
- Basic Azure App Service infrastructure with Bicep

See the supporting docs in [docs/README.md](docs/README.md).

## Features

- Add tasks with title, category, priority, and optional due date
- Mark tasks complete or active
- Visual grouping with badges for category, priority, and due date
- Server-side validation for required and length-constrained input
- In-memory repository suitable for local demos and exercises

## Tech Stack

- .NET 10
- ASP.NET Core Razor Pages
- xUnit
- Coverlet (Cobertura output + minimum line coverage threshold)
- Azure Bicep (App Service plan + Linux web app)

## Prerequisites

- .NET 10 SDK
- (Optional) Azure CLI for infrastructure deployment

Verify your SDK:

```powershell
dotnet --version
```

## Quick Start

Restore dependencies and run the web app:

```powershell
dotnet restore
dotnet run --project src/webapp
```

By default, the app starts on local development URLs shown in console output.

## Build And Test

Build all projects:

```powershell
dotnet build
```

Run tests:

```powershell
dotnet test
```

Coverage details:

- Coverage format: `cobertura`
- Coverage threshold: `40%` line coverage
- Report output: `src/webapp.tests/coverage.cobertura.xml`

If line coverage drops below the configured threshold, the test run fails.

## Project Structure

```text
.
|- src/
|  |- webapp/                # Razor Pages application
|  |  |- Models/             # Domain model (TodoItem, priority)
|  |  |- Pages/              # Razor Pages UI and handlers
|  |  |- Services/           # In-memory repository implementation
|  |
|  |- webapp.tests/          # xUnit test project
|
|- infra/                    # Azure Bicep template and parameters
|- docs/                     # Supplemental project documentation
```

## Runtime Notes

- The repository is registered as a singleton, so task data persists for the
  lifetime of the running process.
- Data is not persisted to disk. Restarting the app clears all tasks.
- HTTPS redirection is enabled.

## Deploy Infrastructure (Azure)

The Bicep template in `infra/main.bicep` provisions:

- Linux App Service plan
- Linux Web App configured for `.NET 10`

Create a resource group and deploy infrastructure:

```powershell
az group create --name <resource-group> --location <azure-region>
az deployment group create \
  --resource-group <resource-group> \
  --template-file infra/main.bicep \
  --parameters @infra/main.parameters.json
```

Template parameters are defined in `infra/main.parameters.json`:

- `appServicePlanName`
- `webAppName`
- `skuName` (`B1`, `S1`, or `P1v3`)

## Next Improvements

If you want to extend this sample, useful next steps are:

- Replace in-memory storage with a persistent database
- Add edit/delete operations for tasks
- Add filtering and sorting in the UI
- Add integration tests for page workflows
