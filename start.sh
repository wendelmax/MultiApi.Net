#!/bin/bash

# Start StarWars API on port 5000
echo "Starting StarWars API on port 5000..."
cd /app/StarWars.Api
dotnet StarWars.Api.dll --urls "http://localhost:5000" &
STARWARS_PID=$!
echo "StarWars API started with PID: $STARWARS_PID"

# Start Collection Manager API on port 5001
echo "Starting Collection Manager API on port 5001..."
cd /app/CollectionManager.Api
dotnet CollectionManager.Api.dll --urls "http://localhost:5001" &
COLLECTION_PID=$!
echo "Collection Manager API started with PID: $COLLECTION_PID"

# Wait for services to start and verify they're running
echo "Waiting for services to start..."
sleep 20

# Check if processes are still running
echo "Checking if processes are still running..."
if ! kill -0 $STARWARS_PID 2>/dev/null; then
    echo "ERROR: StarWars API process died!"
    exit 1
fi

if ! kill -0 $COLLECTION_PID 2>/dev/null; then
    echo "ERROR: Collection Manager API process died!"
    exit 1
fi

echo "Both processes are running. Verifying APIs are responding..."

# Verify services are running
for i in {1..60}; do
    if curl -s http://localhost:5000/health > /dev/null 2>&1 && \
       curl -s http://localhost:5001/health > /dev/null 2>&1; then
        echo "Both APIs are responding!"
        break
    fi
    echo "Waiting for APIs to be ready... (attempt $i/60)"
    
    # Check if processes are still running
    if ! kill -0 $STARWARS_PID 2>/dev/null; then
        echo "ERROR: StarWars API process died during startup!"
        exit 1
    fi
    
    if ! kill -0 $COLLECTION_PID 2>/dev/null; then
        echo "ERROR: Collection Manager API process died during startup!"
        exit 1
    fi
    
    sleep 2
done

# Final verification
if ! curl -s http://localhost:5000/health > /dev/null 2>&1 || \
   ! curl -s http://localhost:5001/health > /dev/null 2>&1; then
    echo "ERROR: One or both APIs failed to start properly!"
    echo "StarWars API status:"
    curl -v http://localhost:5000/health || echo "StarWars API not responding"
    echo "Collection Manager API status:"
    curl -v http://localhost:5001/health || echo "Collection Manager API not responding"
    echo "Process status:"
    ps aux | grep dotnet || echo "No dotnet processes found"
    exit 1
fi

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
