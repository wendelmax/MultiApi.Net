#!/bin/bash

# Start StarWars API on port 5000
echo "Starting StarWars API on port 5000..."
cd /app/StarWars.Api
dotnet StarWars.Api.dll --urls "http://localhost:5000" &
STARWARS_PID=$!

# Start Collection Manager API on port 5001
echo "Starting Collection Manager API on port 5001..."
cd /app/CollectionManager.Api
dotnet CollectionManager.Api.dll --urls "http://localhost:5001" &
COLLECTION_PID=$!

# Wait a moment for services to start
sleep 5

# Start Nginx
echo "Starting Nginx..."
nginx -g "daemon off;" &
NGINX_PID=$!

echo "All services started:"
echo "- StarWars API: PID $STARWARS_PID (port 5000)"
echo "- Collection Manager API: PID $COLLECTION_PID (port 5001)"
echo "- Nginx: PID $NGINX_PID (port 80)"

# Function to handle shutdown
shutdown_services() {
    echo "Shutting down services..."
    kill $STARWARS_PID
    kill $COLLECTION_PID
    kill $NGINX_PID
    exit 0
}

# Trap SIGTERM and SIGINT
trap shutdown_services SIGTERM SIGINT

# Wait for all background processes
wait
