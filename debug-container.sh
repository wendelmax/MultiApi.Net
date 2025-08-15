#!/bin/bash

echo "========================================"
echo "MultiApi.Net - Container Debug Script"
echo "========================================"

# Build the image
echo "Building Docker image..."
docker build -t multiapi:debug .

# Run the container with more verbose output
echo "Starting container in foreground to see startup logs..."
docker run --rm --name debug-container -p 8080:80 \
  -e CONNECTIONSTRINGS__MONGODB="mongodb://localhost:27017" \
  multiapi:debug

echo ""
echo "Container exited. Exit code: $?"
echo ""
echo "Checking if container still exists:"
docker ps -a | grep debug-container || echo "Container not found"

echo ""
echo "Debug completed!"
