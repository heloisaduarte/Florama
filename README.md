# Florama - E-commerce API

API de e-commerce desenvolvida em **.NET** para uma loja de produtos aromáticos chamada **Florama**, especializada na venda de **velas aromáticas, sabonetes artesanais e aromatizadores de ambiente**.

O objetivo do projeto é demonstrar a construção de uma API utilizando **DDD (Domain Driven Design)** e **Clean Architecture**, com foco em boas práticas de desenvolvimento, organização de código e escalabilidade.

---

## Tecnologias utilizadas

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Swagger / OpenAPI
* JWT (autenticação) + Rate Limiting
* DDD (Domain Driven Design)
* Clean Architecture
* xUnit (testes unitários)
* Docker

---

## Estrutura do projeto

O projeto está organizado seguindo princípios de **DDD e separação de responsabilidades**:

src/

* **Florama.Api** → Camada de apresentação (Controllers, Filters, validações, configuração da API)
* **Florama.Application** → Casos de uso, serviços e DTOs
* **Florama.Domain** → Entidades, regras de negócio, interfaces de segurança e opções (Domínio)
* **Florama.CrossCutting** → Serviços de infraestrutura compartilhados (JWT, password hasher)
* **Florama.Data** → Acesso a dados, Entity Framework, repositórios, migrações, seed de dados

tests/

* **Florama.Tests** → Testes unitários de domínio (entities, value objects)

---

## Como executar

1. **Configurar o banco de dados** no `appsettings.Development.json` (`ConnectionStrings:DefaultConnection`).
2. **Configurar os segredos** em `appsettings.Development.json` (arquivo gitignored) ou via variáveis de ambiente:
   * `Jwt:SecretKey` → chave com pelo menos 32 bytes (obrigatória).
   * `Admin:Email` e `Admin:Senha` → credenciais do usuário administrador inicial.
3. **Executar a API:**

   ```bash
   dotnet run --project src\Florama.Api
   ```

   Ao iniciar, a aplicação aplica as migrações pendentes, cria o usuário administrador (`Admin:Email`/`Admin:Senha`) e, se a tabela de categorias estiver vazia, insere **dados de demonstração** (categorias, produtos, fotos e estoque). Para desativar o seed de demonstração, defina `SeedDemoData` como `false` nas configurações.

4. **Executar os testes:**

   ```bash
   dotnet test tests\Florama.Tests
   ```

---

## Demo online

A API está disponível publicamente para demonstração (hospedada no **Render**, com banco de dados no **Azure SQL**):

* **Swagger:** `https://florama-api.onrender.com/swagger`
* **Health check:** `https://florama-api.onrender.com/health`

> O plano gratuito do Render "dorme" após alguns minutos sem uso — o primeiro acesso pode demorar de 30 a 60 segundos. É normal, aguarde a resposta.

---

## Funcionalidades

* Cadastro de categorias
* Cadastro de produtos com fotos (ordenação) e estoque
* Listagem de produtos
* Autenticação JWT com refresh token e bloqueio por tentativas
* Gerenciamento de usuários
* Controle de concorrência no estoque (RowVersion)
* Health check em `/health`

---

## Domínio da aplicação

A loja Florama trabalha com produtos voltados para **bem-estar e aromatização de ambientes**, incluindo:

* Velas aromáticas
* Sabonetes artesanais
* Aromatizadores de ambiente

---

## Autor

Desenvolvido por **Heloisa Duarte**
