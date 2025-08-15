# Collection Manager API

Uma API .NET que permite aos alunos criarem coleções MongoDB e realizarem operações CRUD usando chaves de API únicas.

## Funcionalidades

- Criar novas coleções com chaves de API únicas
- Realizar operações CRUD em documentos dentro das coleções
- Executar consultas MongoDB personalizadas
- Controle de acesso seguro usando chaves de API
- Suporte para estruturas de documentos dinâmicas

## Pré-requisitos

- .NET 9.0 ou superior
- Instância MongoDB rodando (local ou remota)
- A string de conexão já está configurada para MongoDB Atlas

## Como Funciona o Sistema

### 1. Criação de Coleções
- Cada aluno pode criar sua própria coleção
- O sistema gera automaticamente uma chave de API única (32 caracteres)
- A chave é necessária para todas as operações na coleção

### 2. Operações nos Documentos
- Os alunos podem inserir, consultar, atualizar e deletar documentos
- Cada documento pode ter qualquer estrutura de dados
- O sistema valida se a chave de API corresponde à coleção

### 3. Consultas Personalizadas
- Suporte para operações MongoDB avançadas (find, aggregate, count, distinct)
- Filtros, ordenação e paginação
- Pipeline de agregação completo

## Como Usar

### Passo 1: Criar uma Coleção
```bash
POST https://localhost:7001/api/collections
Content-Type: application/json

{
  "name": "meu-projeto",
  "description": "Dados do meu projeto escolar",
  "owner": "aluno@escola.edu"
}
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

**IMPORTANTE:** Guarde a `apiKey`! Ela será necessária para todas as operações.

### Passo 2: Inserir Dados na Coleção
```bash
POST https://localhost:7001/api/documents/meu-projeto
X-API-Key: aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5
Content-Type: application/json

{
  "nome": "João Silva",
  "idade": 18,
  "nota": 9.5,
  "materias": ["Matemática", "Física"],
  "dataCadastro": "2024-01-15"
}
```

### Passo 3: Consultar Dados
```bash
# Buscar todos os documentos
GET https://localhost:7001/api/documents/meu-projeto
X-API-Key: aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5

# Buscar documento específico
GET https://localhost:7001/api/documents/meu-projeto/DOCUMENT_ID
X-API-Key: aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5

# Filtrar por idade
GET https://localhost:7001/api/documents/meu-projeto?idade=18
X-API-Key: aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5
```

### Passo 4: Atualizar Dados
```bash
PUT https://localhost:7001/api/documents/meu-projeto/DOCUMENT_ID
X-API-Key: aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5
Content-Type: application/json

{
  "nota": 10.0,
  "idade": 19
}
```

### Passo 5: Deletar Dados
```bash
DELETE https://localhost:7001/api/documents/meu-projeto/DOCUMENT_ID
X-API-Key: aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5
```

## Consultas Avançadas

### Buscar com Filtros Complexos
```bash
POST https://localhost:7001/api/query/meu-projeto
X-API-Key: aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5
Content-Type: application/json

{
  "operation": "find",
  "filter": "{\"idade\": {\"$gte\": 18}, \"nota\": {\"$gte\": 8.0}}",
  "sort": "{\"nota\": -1}",
  "limit": 10
}
```

### Contar Documentos
```bash
POST https://localhost:7001/api/query/meu-projeto
X-API-Key: aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5
Content-Type: application/json

{
  "operation": "count",
  "filter": "{\"nota\": {\"$gte\": 9.0}}"
}
```

### Valores Distintos
```bash
POST https://localhost:7001/api/query/meu-projeto
X-API-Key: aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5
Content-Type: application/json

{
  "operation": "distinct",
  "field": "materias"
}
```

### Pipeline de Agregação
```bash
POST https://localhost:7001/api/query/meu-projeto
X-API-Key: aB3cD4eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5
Content-Type: application/json

{
  "operation": "aggregate",
  "pipeline": [
    "{\"$match\": {\"idade\": {\"$gte\": 18}}}",
    "{\"$group\": {\"_id\": \"$materias\", \"mediaNota\": {\"$avg\": \"$nota\"}}}"
  ]
}
```

## Endpoints da API

### Gerenciamento de Coleções
- `POST /api/collections` - Criar nova coleção
- `GET /api/collections` - Listar todas as coleções
- `GET /api/collections/{nome}` - Obter coleção específica
- `DELETE /api/collections/{id}` - Deletar coleção

### Operações em Documentos
- `POST /api/documents/{nomeColecao}` - Criar documento
- `GET /api/documents/{nomeColecao}` - Listar documentos
- `GET /api/documents/{nomeColecao}/{id}` - Obter documento específico
- `PUT /api/documents/{nomeColecao}/{id}` - Atualizar documento
- `DELETE /api/documents/{nomeColecao}/{id}` - Deletar documento

### Consultas Personalizadas
- `POST /api/query/{nomeColecao}` - Executar consultas MongoDB

## Segurança

- Cada coleção tem uma chave de API única de 32 caracteres
- As chaves são necessárias para todas as operações em documentos
- As chaves são validadas contra o nome da coleção
- As coleções são isoladas por chave de API

## Códigos de Status HTTP

- **200** - Sucesso
- **201** - Criado com sucesso
- **204** - Sem conteúdo (operações de atualização/deleção)
- **400** - Requisição inválida
- **401** - Chave de API ausente
- **403** - Chave de API inválida ou acesso negado
- **404** - Não encontrado
- **409** - Conflito (nome de coleção duplicado)

## Exemplos Práticos para Alunos

### Exemplo 1: Sistema de Notas
```bash
# 1. Criar coleção para notas
curl -X POST "https://localhost:7001/api/collections" \
     -H "Content-Type: application/json" \
     -d '{"name": "notas-2024", "description": "Sistema de notas da turma", "owner": "professor@escola.edu"}'

# 2. Inserir notas dos alunos
curl -X POST "https://localhost:7001/api/documents/notas-2024" \
     -H "Content-Type: application/json" \
     -H "X-API-Key: SUA_API_KEY_AQUI" \
     -d '{"aluno": "Maria Santos", "materia": "Matemática", "nota": 9.5, "data": "2024-01-15"}'

# 3. Consultar média das notas
curl -X POST "https://localhost:7001/api/query/notas-2024" \
     -H "Content-Type: application/json" \
     -H "X-API-Key: SUA_API_KEY_AQUI" \
     -d '{"operation": "aggregate", "pipeline": ["{\"$group\": {\"_id\": null, \"media\": {\"$avg\": \"$nota\"}}}"]}'
```

### Exemplo 2: Controle de Presença
```bash
# 1. Criar coleção para presença
curl -X POST "https://localhost:7001/api/collections" \
     -H "Content-Type: application/json" \
     -d '{"name": "presenca-aula", "description": "Controle de presença nas aulas", "owner": "coordenador@escola.edu"}'

# 2. Registrar presença
curl -X POST "https://localhost:7001/api/documents/presenca-aula" \
     -H "Content-Type: application/json" \
     -H "X-API-Key: SUA_API_KEY_AQUI" \
     -d '{"aluno": "Pedro Costa", "aula": "História", "presente": true, "data": "2024-01-15", "horario": "08:00"}'

# 3. Contar presenças por aula
curl -X POST "https://localhost:7001/api/query/presenca-aula" \
     -H "Content-Type: application/json" \
     -H "X-API-Key: SUA_API_KEY_AQUI" \
     -d '{"operation": "aggregate", "pipeline": ["{\"$match\": {\"presente\": true}}", "{\"$group\": {\"_id\": \"$aula\", \"totalPresentes\": {\"$sum\": 1}}}]}'
```

## Dicas Importantes

1. **Sempre guarde sua chave de API** - Ela é única e não pode ser recuperada
2. **Use nomes descritivos** para suas coleções
3. **Teste suas consultas** no Swagger antes de implementar no código
4. **Mantenha backup** dos dados importantes
5. **Use filtros** para consultas eficientes
6. **Valide os dados** antes de inserir

## Suporte

- Acesse o Swagger em: `https://localhost:7001/swagger`
- Documentação completa da API disponível no Swagger UI
- Todos os endpoints estão documentados com exemplos

## Tecnologias Utilizadas

- **.NET 9** - Framework da aplicação
- **MongoDB** - Banco de dados NoSQL
- **MongoDB.Driver** - Driver oficial para .NET
- **Swagger** - Documentação da API
- **CORS** - Suporte para requisições cross-origin
