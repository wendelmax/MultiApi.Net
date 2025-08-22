# Star Wars API 🚀

Uma API .NET completa para dados do universo Star Wars, ideal para projetos educacionais e aplicações que precisam de dados do universo Star Wars.

## 📋 **Visão Geral**

A Star Wars API é uma solução completa que fornece acesso a dados do universo Star Wars através de uma API REST moderna e documentada.

## 🏗️ **Arquitetura**

```
┌─────────────────────────────────────────────────────────────┐
│                    Container Docker                         │
├─────────────────────────────────────────────────────────────┤
│  StarWars API (Porta 5000)                                  │
├─────────────────────────────────────────────────────────────┤
│  Banco de Dados Local (SQLite)                              │
└─────────────────────────────────────────────────────────────┘
```

## 🚀 **Funcionalidades Principais**

- Endpoints para personagens, planetas, espécies, naves e veículos
- **Banco de dados local** com dados completos do universo Star Wars
- **LocalDB** como padrão (performance máxima para desenvolvimento)
- **SQLite** como alternativa cross-platform
- Documentação Swagger integrada
- Dados de exemplo do universo Star Wars (287 registros)
- CRUD completo para todas as entidades
- Relacionamentos entre entidades (filmes, personagens, planetas, etc.)

## 🛠️ **Tecnologias Utilizadas**

- **.NET 9.0** - Framework principal
- **Entity Framework Core** - ORM para banco de dados
- **SQL Server LocalDB** - Banco padrão para desenvolvimento (Windows)
- **SQLite** - Banco alternativo cross-platform
- **Swagger/OpenAPI** - Documentação da API
- **Docker** - Containerização
- **GitHub Actions** - CI/CD automatizado

## 📁 **Estrutura do Projeto**

```
StarWars.Api/
├── Controllers/                 # Endpoints da API
│   ├── FilmsController.cs      # Gerenciamento de filmes
│   ├── PeopleController.cs     # Gerenciamento de personagens
│   ├── PlanetsController.cs    # Gerenciamento de planetas
│   ├── SpeciesController.cs    # Gerenciamento de espécies
│   ├── StarshipsController.cs  # Gerenciamento de naves
│   └── VehiclesController.cs   # Gerenciamento de veículos
├── Services/                    # Lógica de negócio
├── Repositories/               # Acesso a dados
├── Models/                     # Entidades do banco de dados
├── DTOs/                      # Objetos de transferência de dados
├── Data/                      # Contexto do Entity Framework
└── Database/                  # Scripts e configurações de banco
```

## 📦 **Instalação e Execução**

### **Pré-requisitos**
- Docker Desktop instalado e rodando (opcional)
- .NET 9.0 SDK (para desenvolvimento local)

### **Opção 1: Docker (Recomendado)**

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/StarWars.Api.git
cd StarWars.Api

# Execute com Docker
docker build -t starwars-api .
docker run -p 5000:5000 starwars-api
```

### **Opção 2: Desenvolvimento Local**

```bash
# Restaure as dependências
dotnet restore

# Execute a API
dotnet run --project StarWars.Api
```

## 🌐 **Endpoints Disponíveis**

### **Star Wars API**
- **Base URL**: `http://localhost:5000/`
- **Swagger**: `http://localhost:5000/swagger`

### **Endpoints Principais**
- **Filmes**: `GET /api/films` - Lista todos os filmes
- **Personagens**: `GET /api/people` - Lista todos os personagens
- **Planetas**: `GET /api/planets` - Lista todos os planetas
- **Espécies**: `GET /api/species` - Lista todas as espécies
- **Naves**: `GET /api/starships` - Lista todas as naves
- **Veículos**: `GET /api/vehicles` - Lista todos os veículos

### **Operações CRUD**
Cada endpoint suporta:
- `GET /api/{entity}` - Listar todos
- `GET /api/{entity}/{id}` - Buscar por ID
- `POST /api/{entity}` - Criar novo
- `PUT /api/{entity}/{id}` - Atualizar
- `DELETE /api/{entity}/{id}` - Excluir

## 📚 **Como Usar a API**

### **1. Listar Todos os Filmes**

```bash
curl -X GET "http://localhost:5000/api/films"
```

### **2. Buscar Personagem por ID**

```bash
curl -X GET "http://localhost:5000/api/people/1"
```

### **3. Criar Novo Personagem**

```bash
curl -X POST "http://localhost:5000/api/people" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Luke Skywalker",
    "height": "172",
    "mass": "77",
    "hairColor": "blond",
    "eyeColor": "blue",
    "birthYear": "19BBY",
    "gender": "male"
  }'
```

### **4. Atualizar Personagem**

```bash
curl -X PUT "http://localhost:5000/api/people/1" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Luke Skywalker",
    "height": "175",
    "mass": "80"
  }'
```

## 🗄️ **Banco de Dados**

### **Configuração**
- **LocalDB** (Windows): Banco padrão para desenvolvimento
- **SQLite**: Alternativa cross-platform
- **Dados Iniciais**: Scripts SQL incluídos para popular o banco

### **Entidades Principais**
- **Films**: Informações sobre os filmes da saga
- **People**: Personagens do universo Star Wars
- **Planets**: Planetas e suas características
- **Species**: Espécies alienígenas
- **Starships**: Naves espaciais
- **Vehicles**: Veículos terrestres e aéreos

### **Relacionamentos**
- Filmes ↔ Personagens (muitos para muitos)
- Filmes ↔ Planetas (muitos para muitos)
- Filmes ↔ Espécies (muitos para muitos)
- Filmes ↔ Naves (muitos para muitos)
- Filmes ↔ Veículos (muitos para muitos)
- Personagens ↔ Espécies (muitos para muitos)
- Personagens ↔ Naves (muitos para muitos)
- Personagens ↔ Veículos (muitos para muitos)
- Planetas ↔ Residentes (muitos para muitos)

## 🐳 **Docker e Deploy**

### **Estrutura do Container**
- **Porta 5000**: Star Wars API

### **Scripts de Deploy**
- **Windows**: `build-docker.bat`
- **Linux/Mac**: `build-docker.sh`

### **Variáveis de Ambiente**
```bash
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__DefaultConnection=Data Source=starwars.db
```

## 🔄 **CI/CD com GitHub Actions**

O projeto inclui workflow automatizado que:
- **Build e Teste**: Compila e testa o código
- **Docker Build**: Cria e testa a imagem Docker
- **Push Automático**: Envia para GitHub Container Registry
- **Releases**: Gera artefatos automaticamente com tags
- **Segurança**: Escaneamento de vulnerabilidades

### **Triggers Automáticos**
- Push para branch `main`
- Criação de tags `v*.*.*`
- Builds agendados diários

## 📊 **Monitoramento e Saúde**

- **Health Check**: `GET /health`
- **Status Geral**: `GET /` (informações dos serviços)
- **Logs**: Acessíveis via Docker logs
- **Métricas**: Swagger UI integrado

## 🚨 **Troubleshooting**

### **Problemas Comuns**

1. **Container não inicia**
   ```bash
   docker logs starwars-api
   ```

2. **Erro de conexão com banco**
   - Verifique se o arquivo de banco existe
   - Confirme as permissões de arquivo

3. **Portas em uso**
   ```bash
   netstat -an | findstr :5000
   ```

### **Logs e Debug**
```bash
# Logs do container
docker logs -f starwars-api

# Acesso ao container
docker exec -it starwars-api bash

# Status dos serviços
docker ps
```

## 🤝 **Contribuição**

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 **Licença**

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## 📞 **Suporte**

- **Issues**: [GitHub Issues](https://github.com/seu-usuario/StarWars.Api/issues)
- **Documentação**: Swagger UI integrado na API
- **Exemplos**: Veja os endpoints para casos de uso

## 🎯 **Casos de Uso**

### **Educacional**
- Aulas de programação com APIs REST
- Projetos escolares sobre Star Wars
- Desenvolvimento de aplicações web

### **Desenvolvimento**
- Prototipagem rápida de APIs
- Testes de integração
- Demonstração de Entity Framework Core

### **Produção**
- APIs para aplicações Star Wars
- Sistema de gerenciamento de dados
- Microserviços com banco relacional

---

**Star Wars API** - Explorando o universo Star Wars através de APIs modernas 🚀
