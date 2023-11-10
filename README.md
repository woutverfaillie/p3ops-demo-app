# SportStore

This project is a very simple e-commerce website for sports products.

## Requirements

- [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## How to run in development

> These steps assume that you have a SQL Server instance running on your machine. If not, you can use the [SQL Server Docker image](https://hub.docker.com/_/microsoft-mssql-server) to run one. Make sure to set the correct connection string of the SQL Server instance in `src/Server/appsettings.Development.json`.

1. Clone the repository
2. Restore the packages: `dotnet restore src/Server/Server.csproj`
3. Start the server: `dotnet run watch --project src/Server/Server.csproj`

## How to run in production

1. Clone the repository
2. Restore the packages: `dotnet restore src/Server/Server.csproj`
3. Build the server: `dotnet build src/Server/Server.csproj`
4. Publish the server: `dotnet publish src/Server/Server.csproj -c Release -o publish`
5. Make sure the following environment variables are set:
   - `DOTNET_ENVIRONMENT`: environment name, e.g. `Production`
   - `DOTNET_ConnectionStrings__SqlDatabase`: connection string to the SQL Server database
6. Start the server: `dotnet publish/Server.dll`

## How to test

> No database is required to run the unit tests.

1. Clone the repository
2. Restore the packages: `dotnet restore src/Server/Server.csproj` and `dotnet restore tests/Domain.Tests/Domain.Tests.csproj`
3. Run the unit tests for the domain: `dotnet test tests/Domain.Tests/Domain.Tests.csproj`
