@echo off
echo ========================================
echo MultiApi.Net - Docker Build and Run
echo ========================================
echo.

echo [1/4] Verificando Docker...
docker --version >nul 2>&1
if errorlevel 1 (
    echo ERRO: Docker nao esta instalado ou nao esta rodando!
    echo Por favor, instale o Docker Desktop e tente novamente.
    pause
    exit /b 1
)

echo [2/4] Parando containers existentes...
docker-compose down >nul 2>&1

echo [3/4] Construindo e executando container...
docker-compose up --build -d

if errorlevel 1 (
    echo.
    echo ERRO: Falha ao construir ou executar o container!
    echo Verifique os logs com: docker-compose logs
    pause
    exit /b 1
)

echo.
echo [4/4] Aguardando servicos iniciarem...
timeout /t 10 /nobreak >nul

echo.
echo ========================================
echo SUCESSO! Servicos estao rodando:
echo ========================================
echo.
echo StarWars API:
echo - Endpoints: http://localhost/starwars/*
echo - Swagger:   http://localhost/starwars/swagger
echo.
echo Collection Manager API:
echo - Endpoints: http://localhost/collections/*
echo - Swagger:   http://localhost/collections/swagger
echo.
echo Health Check: http://localhost/health
echo.
echo Para ver logs: docker-compose logs -f
echo Para parar:    docker-compose down
echo ========================================
echo.
pause
