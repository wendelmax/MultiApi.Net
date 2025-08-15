# MultiApi.Net 🚀

Uma solução completa de APIs .NET com MongoDB para gerenciamento de coleções e dados dinâmicos, ideal para projetos educacionais e aplicações multi-tenant.

## 📋 **Visão Geral**

O MultiApi.Net é um projeto que combina duas APIs distintas em uma única solução containerizada:

- **StarWars API**: API de exemplo com dados do universo Star Wars
- **Collection Manager API**: Sistema de gerenciamento de coleções MongoDB com chaves de API únicas

## 🏗️ **Arquitetura**

```
┌─────────────────────────────────────────────────────────────┐
│                    Container Docker                         │
├─────────────────────────────────────────────────────────────┤
│  Nginx (Porta 80) - Proxy Reverso                         │
├─────────────────────────────────────────────────────────────┤
│  StarWars API (Porta 5000)                                │
│  Collection Manager API (Porta 5001)                      │
├─────────────────────────────────────────────────────────────┤
│  MongoDB (Externo - Atlas)                                 │
└─────────────────────────────────────────────────────────────┘
```

## 🚀 **Funcionalidades Principais**

### **StarWars API**
- Endpoints para personagens, planetas e naves
- Documentação Swagger integrada
- Dados de exemplo do universo Star Wars

### **Collection Manager API**
- **Criação de Coleções**: Cada aluno cria sua própria coleção
- **API Keys Únicas**: Credenciais individuais para acesso
- **CRUD Completo**: Operações de leitura, escrita, atualização e exclusão
- **Consultas Personalizadas**: Suporte a queries MongoDB avançadas
- **Documentos Dinâmicos**: Estrutura flexível para qualquer tipo de dado
- **Controle de Acesso**: Isolamento entre coleções de diferentes usuários

## 🛠️ **Tecnologias Utilizadas**

- **.NET 9.0** - Framework principal
- **MongoDB.Driver** - Driver oficial do MongoDB
- **Swagger/OpenAPI** - Documentação da API
- **Docker** - Containerização
- **Nginx** - Proxy reverso
- **GitHub Actions** - CI/CD automatizado

## 📦 **Instalação e Execução**

### **Pré-requisitos**
- Docker Desktop instalado e rodando
- .NET 9.0 SDK (para desenvolvimento local)
- MongoDB Atlas ou instância local

### **Opção 1: Docker Compose (Recomendado)**

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/MultiApi.Net.git
cd MultiApi.Net

# Configure as variáveis de ambiente
cp .env.example.txt .env
# Edite o arquivo .env com sua connection string do MongoDB

# Execute com Docker Compose
docker-compose up --build
```

### **Opção 2: Desenvolvimento Local**

```bash
# Restaure as dependências
dotnet restore

# Execute as APIs
dotnet run --project StarWars.Api
dotnet run --project CollectionManager.Api
```

## 🌐 **Endpoints Disponíveis**

### **StarWars API**
- **Base URL**: `http://localhost/starwars/`
- **Swagger**: `http://localhost/starwars/swagger`

### **Collection Manager API**
- **Base URL**: `http://localhost/collections/`
- **Swagger**: `http://localhost/collections/swagger`
- **Endpoints**:
  - `POST /collections` - Criar nova coleção
  - `GET /collections` - Listar coleções
  - `POST /documents/{collectionName}` - Inserir documento
  - `GET /documents/{collectionName}` - Buscar documentos
  - `PUT /documents/{collectionName}/{id}` - Atualizar documento
  - `DELETE /documents/{collectionName}/{id}` - Excluir documento
  - `POST /query/{collectionName}` - Consultas personalizadas

## 📚 **Como Usar o Sistema**

### **1. Criar uma Coleção**

```bash
curl -X POST http://localhost/collections \
  -H "Content-Type: application/json" \
  -d '{
    "name": "meu-projeto",
    "description": "Dados do meu projeto escolar",
    "owner": "aluno@escola.edu"
  }'
```

**Resposta:**
```json
{
  "collectionId": "507f1f77bcf86cd799439011",
  "name": "meu-projeto",
  "apiKey": "aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5",
  "createdAt": "2024-01-15T10:30:00Z",
  "message": "Collection created successfully"
}
```

### **2. Inserir Dados na Coleção**

```bash
curl -X POST http://localhost/documents/meu-projeto \
  -H "Content-Type: application/json" \
  -H "X-API-Key: aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5" \
  -d '{
    "nome": "João Silva",
    "idade": 18,
    "notas": [8.5, 9.0, 7.8],
    "projeto": "Sistema de Gestão Escolar"
  }'
```

### **3. Consultar Dados**

```bash
curl -X GET "http://localhost/documents/meu-projeto?filter[idade]=18" \
  -H "X-API-Key: aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5"
```

### **4. Consultas Personalizadas**

```bash
curl -X POST http://localhost/query/meu-projeto \
  -H "Content-Type: application/json" \
  -H "X-API-Key: aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5" \
  -d '{
    "operation": "aggregate",
    "pipeline": [
      {"$match": {"idade": {"$gte": 18}}},
      {"$group": {"_id": "$projeto", "total": {"$sum": 1}}}
    ]
  }'
```

## 🔐 **Segurança e Autenticação**

- **API Keys Únicas**: Cada coleção tem sua própria chave de acesso
- **Isolamento de Dados**: Usuários só acessam suas próprias coleções
- **Validação de Headers**: Todas as operações requerem `X-API-Key`
- **CORS Configurado**: Suporte a aplicações web

## 🐳 **Docker e Deploy**

### **Estrutura do Container**
- **Porta 80**: Nginx (proxy reverso)
- **Porta 5000**: StarWars API (interno)
- **Porta 5001**: Collection Manager API (interno)

### **Scripts de Deploy**
- **Windows**: `build-docker.bat`
- **Linux/Mac**: `build-docker.sh`

### **Variáveis de Ambiente**
```bash
CONNECTIONSTRINGS__MONGODB=mongodb+srv://user:pass@cluster.mongodb.net/
ASPNETCORE_ENVIRONMENT=Development
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
- **Métricas**: Swagger UI para cada API

## 🚨 **Troubleshooting**

### **Problemas Comuns**

1. **Container não inicia**
   ```bash
   docker-compose logs multiapi
   ```

2. **Erro de conexão MongoDB**
   - Verifique a connection string no arquivo `.env`
   - Confirme se o MongoDB Atlas está acessível

3. **Portas em uso**
   ```bash
   netstat -an | findstr :80
   ```

### **Logs e Debug**
```bash
# Logs do container
docker-compose logs -f multiapi

# Acesso ao container
docker-compose exec multiapi bash

# Status dos serviços
docker-compose ps
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

- **Issues**: [GitHub Issues](https://github.com/seu-usuario/MultiApi.Net/issues)
- **Documentação**: Swagger UI integrado em cada API
- **Exemplos**: Veja a pasta `examples/` para casos de uso

## 🎯 **Casos de Uso**

### **Educacional**
- Projetos escolares com dados isolados
- Aulas de programação com MongoDB
- Desenvolvimento de aplicações multi-tenant

### **Desenvolvimento**
- Prototipagem rápida de APIs
- Testes de integração com MongoDB
- Demonstração de arquiteturas containerizadas

### **Produção**
- APIs multi-tenant
- Sistema de gerenciamento de dados dinâmicos
- Microserviços com proxy reverso

---

**MultiApi.Net** - Simplificando o desenvolvimento de APIs com MongoDB 🚀
