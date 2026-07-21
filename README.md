# space-lasers
A new project to target our ToDo List from afar

## ToDo web app

This project is an ASP.NET Core (`net10.0`) ToDo List app using an in-memory provider with:

- categories
- due dates
- priorities

Run locally:

```bash
dotnet run
```

## Azure IaC (Bicep)

Bicep files for deploying to Azure App Service are in `/infra`:

- `/home/runner/work/space-lasers/space-lasers/infra/main.bicep`
- `/home/runner/work/space-lasers/space-lasers/infra/main.parameters.json`
