#!/bin/bash

echo "========================================"
echo "MultiApi.Net - Docker Build and Run"
echo "========================================"
echo

echo "[1/4] Verificando Docker..."
if ! command -v docker &> /dev/null; then
    echo "ERRO: Docker não está instalado!"
    echo "Por favor, instale o Docker e tente novamente."
    exit 1
fi

if ! docker info &> /dev/null; then
    echo "ERRO: Docker não está rodando!"
    echo "Por favor, inicie o Docker e tente novamente."
    exit 1
fi

echo "[2/4] Parando containers existentes..."
docker-compose down > /dev/null 2>&1

echo "[3/4] Construindo e executando container..."
docker-compose up --build -d

if [ $? -ne 0 ]; then
    echo
    echo "ERRO: Falha ao construir ou executar o container!"
    echo "Verifique os logs com: docker-compose logs"
    exit 1
fi

echo
echo "[4/4] Aguardando serviços iniciarem..."
sleep 10

echo
echo "========================================"
echo "SUCESSO! Serviços estão rodando:"
echo "========================================"
echo
echo "StarWars API:"
echo "- Endpoints: http://localhost/starwars/*"
echo "- Swagger:   http://localhost/starwars/swagger"
echo
echo "Collection Manager API:"
echo "- Endpoints: http://localhost/collections/*"
echo "- Swagger:   http://localhost/collections/swagger"
echo
echo "Health Check: http://localhost/health"
echo
echo "Para ver logs: docker-compose logs -f"
echo "Para parar:    docker-compose down"
echo "========================================"
echo
