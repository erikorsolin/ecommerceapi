# Ecommerce API (em desenvolvimento)
Uma API REST para gerenciar produtos e categorias em um e-commerce.

## Pré-requisitos
- .NET 8.0 SDK
- Docker e Docker Compose
- Git

## Como Rodar
1. Clone o repositório: `git clone <url-do-repositorio>`.
2. Navegue até a pasta: `cd ecommerceapi`.
3. (Opcional) Atualize `POSTGRES_PASSWORD` no `docker-compose.yml` ou crie um arquivo `.env`.
4. Execute: `docker-compose up --build`.
5. Acesse a API em `http://localhost:8080/swagger`.

## Estrutura do Projeto
- **Controllers/**: Endpoints CRUD para produtos e categorias.
- **Models/**: Entidades Product e Category.
- **Data/**: Contexto do Entity Framework Core.
- **Migrations/**: Migrações do banco de dados.
- **Dockerfile**: Construção da imagem da API.
- **docker-compose.yml**: Orquestração da API e PostgreSQL.
- **.dockerignore**: Otimização do build Docker.
- **.gitignore**: Ignora arquivos locais (ex.: bin/, obj/).
- **appsettings.json**: Configurações padrão.
- **Program.cs**: Ponto de entrada da aplicação.
- **EcommerceApi.csproj**: Dependências do projeto.

## Endpoints
- **GET /api/products**: Lista todos os produtos.
- **POST /api/products**: Cria um novo produto.
- **GET /api/categories**: Lista todas as categorias.
- **POST /api/categories**: Cria uma nova categoria.

## Notas
- Substitua `POSTGRES_PASSWORD` no `docker-compose.yml` por uma senha segura ou use um arquivo `.env`.
- As migrações do Entity Framework Core são aplicadas automaticamente ao iniciar a API.
