#!/bin/bash

echo "========================================"
echo "MultiApi.Net - Container Test Script"
echo "========================================"

# Build the image
echo "Building Docker image..."
docker build -t multiapi:test .

# Run the container
echo "Starting container..."
docker run --rm -d --name test-container -p 8080:80 \
  -e CONNECTIONSTRINGS__MONGODB="mongodb://localhost:27017" \
  multiapi:test

echo "Container started. Container ID:"
docker ps -a | grep test-container

echo ""
echo "Waiting for services to start..."
sleep 30

echo ""
echo "Container logs (last 50 lines):"
docker logs test-container --tail 50

echo ""
echo "Container status:"
docker ps -a | grep test-container

echo ""
echo "Testing endpoints..."

echo "1. Health endpoint:"
curl -v http://localhost:8080/health || echo "FAILED"

echo ""
echo "2. Root endpoint:"
curl -v http://localhost:8080/ || echo "FAILED"

echo ""
echo "3. StarWars API:"
curl -v http://localhost:8080/starwars/ || echo "FAILED"

echo ""
echo "4. Collection Manager API:"
curl -v http://localhost:8080/collections/ || echo "FAILED"

echo ""
echo "Container logs after tests:"
docker logs test-container --tail 20

echo ""
echo "Cleaning up..."
docker stop test-container
docker rm test-container

echo "Test completed!"
