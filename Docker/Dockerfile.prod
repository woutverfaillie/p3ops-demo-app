# Build the dotnet project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app/src
COPY ["src/Client/Client.csproj", "./Client/"]
COPY ["src/Domain/Domain.csproj", "./Domain/"]
COPY ["src/Persistence/Persistence.csproj", "./Persistence/"]
COPY ["src/Server/Server.csproj", "./Server/"]
COPY ["src/Services/Services.csproj", "./Services/"]
COPY ["src/Shared/Shared.csproj", "./Shared/"]

RUN dotnet restore "./Server/Server.csproj"

COPY ./src .

RUN dotnet build "./Server/Server.csproj"
RUN dotnet publish "./Server/Server.csproj" -c Release -o /app/publish

# Create test image
FROM build AS test

WORKDIR /app/tests
COPY ["tests/Domain.Tests/Domain.Tests.csproj", "./Domain.Tests/"]

RUN dotnet restore "./Domain.Tests/Domain.Tests.csproj"

COPY ./tests .

CMD ["dotnet", "test", "./Domain.Tests/Domain.Tests.csproj"]

# Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app
EXPOSE 80

# Install curl for health check
RUN apt update && apt install -y curl

COPY --from=build /app/publish .

HEALTHCHECK --interval=30s --timeout=3s --start-period=20s \
  CMD curl -f http://localhost/ || exit 1

CMD ["dotnet", "/app/Server.dll"]
