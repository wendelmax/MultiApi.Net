FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY MultiApi.Net.sln ./
COPY StarWars.Api/*.csproj ./StarWars.Api/

# Restore dependencies
RUN dotnet restore

# Copy source code
COPY StarWars.Api/ ./StarWars.Api/
COPY StarWars.Api/Data/ ./Data/
COPY StarWars.Api/Data/ ./app/Data/

# Build project
RUN dotnet build -c Release --no-restore

# Publish project
RUN dotnet publish StarWars.Api/StarWars.Api.csproj -c Release -o /app/StarWars.Api --no-build

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Set environment variables
ENV DOTNET_ROOT=/usr/share/dotnet
ENV ASPNETCORE_URLS=http://+:5000;http://+:5001

# Copy published app
COPY --from=build /app/StarWars.Api ./StarWars.Api

# Expose ports
EXPOSE 5000 5001

# Set working directory to the app
WORKDIR /app/StarWars.Api

# Start the application
ENTRYPOINT ["dotnet", "StarWars.Api.dll"]
