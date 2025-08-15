FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY MultiApi.Net.sln ./
COPY StarWars.Api/*.csproj ./StarWars.Api/
COPY CollectionManager.Api/*.csproj ./CollectionManager.Api/

# Restore dependencies
RUN dotnet restore

# Copy source code
COPY StarWars.Api/ ./StarWars.Api/
COPY CollectionManager.Api/ ./CollectionManager.Api/

# Build projects
RUN dotnet build -c Release --no-restore

# Publish projects
RUN dotnet publish StarWars.Api/StarWars.Api.csproj -c Release -o /app/StarWars.Api --no-build
RUN dotnet publish CollectionManager.Api/CollectionManager.Api.csproj -c Release -o /app/CollectionManager.Api --no-build

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Install nginx
RUN apt-get update && apt-get install -y nginx && rm -rf /var/lib/apt/lists/*

# Copy published apps
COPY --from=build /app/StarWars.Api ./StarWars.Api
COPY --from=build /app/CollectionManager.Api ./CollectionManager.Api

# Copy nginx configuration
COPY nginx.conf /etc/nginx/nginx.conf

# Copy startup script
COPY start.sh ./start.sh
RUN chmod +x ./start.sh

# Expose ports
EXPOSE 80 5000 5001

# Start both services and nginx
CMD ["./start.sh"]
