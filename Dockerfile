FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Install native compilation tools for AOT
RUN apt-get update && apt-get install -y \
    clang \
    gcc \
    g++ \
    zlib1g-dev \
    libssl-dev \
    && rm -rf /var/lib/apt/lists/*

# Copy solution and project files
COPY MultiApi.Net.sln ./
COPY StarWars.Api/*.csproj ./StarWars.Api/

# Restore dependencies
RUN dotnet restore

# Copy source code
COPY StarWars.Api/ ./StarWars.Api/

# Build project
RUN dotnet build -c Release --no-restore

# Publish project with AOT
RUN dotnet publish StarWars.Api/StarWars.Api.csproj -c Release -o /app/StarWars.Api --no-build

# Runtime image - using distroless for smaller size
FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-jammy-chiseled AS runtime
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
ENTRYPOINT ["./StarWars.Api"]
