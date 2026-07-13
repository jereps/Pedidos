# 🏛️ Clean Architecture com .NET 10 e PostgreSQL

Este projeto é uma API REST desenvolvida em **.NET 10** aplicando os princípios da **Clean Architecture (Arquitetura Limpa)**, garantindo desacoplamento, alta testabilidade e independência de tecnologias externas. A persistência de dados utiliza o **Entity Framework Core** integrado ao banco de dados **PostgreSQL**.

## 🛠️ Tecnologias Utilizadas

*   **.NET 10 (C#)** - Plataforma de desenvolvimento principal.
*   **ASP.NET Core Minimal APIs** - Abordagem moderna e de alta performance para endpoints HTTP.
*   **Entity Framework Core** - ORM para mapeamento e persistência de dados.
*   **EFCore.NamingConventions** - Automatização de nomenclatura para padrão *snake_case* do PostgreSQL.
*   **PostgreSQL** - Banco de dados relacional.
*   **Docker & Docker Compose** - Containerização para o ambiente de banco de dados local.

## 📐 Estrutura do Projeto (Clean Architecture)

A solução está dividida em 4 camadas principais:
*   `Domain`: Contém as entidades de negócio e os contratos (interfaces) dos repositórios. Totalmente isolada de dependências externas.
*   `Application`: Contém as regras de aplicação e serviços (Casos de Uso) que orquestram o fluxo de dados.
*   `Infrastructure`: Implementação prática de acesso a dados (DbContext, Migrations e Repositórios).
*   `WebApi`: Porta de entrada da aplicação contendo as configurações de inicialização e as Minimal APIs.

---

## 🚀 Como Rodar a Aplicação Localmente

### 📋 Pré-requisitos
Antes de iniciar, você precisará ter instalado em sua máquina:
*   [.NET 10 SDK](https://microsoft.com)
*   [Docker](https://docker.com) e Docker Compose
*   Ferramenta global do EF Core (opcional, instalada via `dotnet tool install --global dotnet-ef`)

### 📦 1. Subir o Banco de Dados (PostgreSQL no Docker)

Na raiz do projeto, crie um arquivo chamado `docker-compose.yml` com as configurações do banco de dados (se já não tiver criado):

```yaml
services:
  postgres-db:
    image: postgres:16-alpine
    container_name: clean_arch_postgres
    environment:
      POSTGRES_DB: CleanArchDb
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
    ports:
      - "5432:5432"
    volumes:
      - pgdata:/var/lib/postgresql/data

volumes:
  pgdata:
```

Para iniciar o container do PostgreSQL em segundo plano, execute o seguinte comando no terminal:
```bash
docker compose up -d
```

### ⚙️ 2. Configurar a String de Conexão

Verifique se o arquivo `src/MinhaSolucao.WebApi/appsettings.json` está apontando corretamente para as credenciais do seu container Docker:

```json
"ConnectionStrings": {
  "PostgresConnection": "Host=localhost;Port=5432;Database=CleanArchDb;Username=postgres;Password=postgres"
}
```

### 🏗️ 3. Criar e Aplicar as Migrations do Banco

O projeto já está configurado para aplicar as migrações automaticamente ao iniciar, mas caso prefira criá-las ou atualizá-las manualmente via CLI, utilize os comandos na raiz da solução:

```bash
# Gerar uma nova migration (caso altere entidades)
dotnet ef migrations add NomeDaSuaAlteracao --project src/MinhaSolucao.Infrastructure/MinhaSolucao.Infrastructure.csproj --startup-project src/MinhaSolucao.WebApi/MinhaSolucao.WebApi.csproj

# Aplicar as tabelas no banco de dados manual
dotnet ef database update --project src/MinhaSolucao.Infrastructure/MinhaSolucao.Infrastructure.csproj --startup-project src/MinhaSolucao.WebApi/MinhaSolucao.WebApi.csproj
```

### ▶️ 4. Executar a API C#

Para compilar e rodar a aplicação web, execute o seguinte comando a partir da raiz da solução:

```bash
dotnet run --project src/MinhaSolucao.WebApi/MinhaSolucao.WebApi.csproj
```

A API estará disponível para receber requisições em:
*   `http://localhost:5051` (ou na porta configurada pelo seu .NET na inicialização do terminal)

---

## 🛣️ Endpoints Disponíveis (CRUD de Pedidos)

| Método | Endpoint | Descrição | Corpo da Requisição (JSON) |
| :--- | :--- | :--- | :--- |
| **POST** | `/api/pedidos` | Cria um novo pedido | `{ "nome": "Teclado Mecânico",item": "teclado", "total": 350.00 }` |
| **GET** | `/api/pedidos` | Lista todos os Pedidos | *Nenhum* |
| **GET** | `/api/pedidos/{id}` | Busca um pedido pelo ID (`long`) | *Nenhum* |
| **PUT** | `/api/pedidos/{id}` | Atualiza um pedido por ID | `{ "nome": "Teclado RGB",item": "teclado", "total": 399.90 }` |
| **DELETE**| `/api/pedidos/{id}` | Deleta um pedido por ID | *Nenhum* |

---

## 🛑 Como Parar o Ambiente Local

Para desligar o container do PostgreSQL e liberar as portas da sua máquina, execute:
```bash
docker compose down
```