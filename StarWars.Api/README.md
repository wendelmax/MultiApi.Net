# StarWars.Api (Estudos)

API de estudos em .NET 8 que faz proxy para a SWAPI (Star Wars API) e expõe documentação em português via Swagger.

- Documentação da SWAPI: https://swapi.py4e.com/documentation
- Base da SWAPI: https://swapi.py4e.com/api/

### Como executar

1. Requisitos: .NET 8 SDK
2. Restaurar e executar:
   - dotnet build
   - dotnet run --project StarWars.Api/StarWars.Api.csproj
3. Acessar documentação: https://localhost:5001/swagger (ou conforme `launchSettings.json`).

### Recursos

- GET / raiz com links úteis
- GET /v1/pessoas lista pessoas (proxy de `people/`)
- GET /v1/pessoas/{id} pessoa por id
- GET /v1/pessoas/buscar?termo=... busca por termo
- GET /v1/pessoas/filtrar?termo=&pagina=&tamanhoPagina=&ordenarPor=&ordenarDirecao=
- GET /v1/planetas lista planetas (proxy de `planets/`)
- GET /v1/planetas/{id} planeta por id
- GET /v1/planetas/buscar?termo=... busca por termo
- GET /v1/planetas/filtrar?termo=&pagina=&tamanhoPagina=&ordenarPor=&ordenarDirecao=
- GET /v1/filmes lista filmes
- GET /v1/filmes/{id} filme por id
- GET /v1/filmes/buscar?termo=... busca por termo
- GET /v1/filmes/filtrar?termo=&pagina=&tamanhoPagina=&ordenarPor=&ordenarDirecao=
- GET /v1/naves | /{id} | /buscar?termo=...
- GET /v1/veiculos | /{id} | /buscar?termo=...
- GET /v1/especies | /{id} | /buscar?termo=...
- GET /health verificação de saúde

### Observações

- Sem autenticação. Esta API repassa respostas da SWAPI.
- Paginação segue a SWAPI (`page` por query string).
- CORS liberado para facilitar consumo em apps React Native.
- Comentários XML habilitados para textos em PT-BR no Swagger.

### Exemplos rápidos

- Pessoas (buscar): GET `/v1/pessoas/buscar?termo=luke`
- Pessoas (filtrar): GET `/v1/pessoas/filtrar?termo=r2&pagina=1&tamanhoPagina=5&ordenarPor=name&ordenarDirecao=asc`
- Planetas: GET `/v1/planetas/filtrar?termo=tatooine&ordenarPor=name&ordenarDirecao=asc`
- Filmes: GET `/v1/filmes/filtrar?ordenarPor=title&ordenarDirecao=desc&tamanhoPagina=3`
- Naves: GET `/v1/naves/filtrar?termo=wing&pagina=2`
- Veículos: GET `/v1/veiculos/filtrar?termo=speeder`
- Espécies: GET `/v1/especies/filtrar?ordenarPor=name&tamanhoPagina=10`

### Licença

Uso educacional. Dados originais de https://swapi.py4e.com/documentation.
