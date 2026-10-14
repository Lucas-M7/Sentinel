# Sentinel - Uptime Monitor

![.NET Version](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet)
![Docker](https://img.shields.io/badge/Docker-Containerized-2496ED?style=flat&logo=docker)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Database-4169E1?style=flat&logo=postgresql)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-success)
![License](https://img.shields.io/badge/License-MIT-blue.svg)

O **Sentinel** é um sistema de monitoramento de sites e APIs (*Uptime Monitor*) de alta performance, desenvolvido em **.NET 10**. O projeto foi construído do zero aplicando princípios de **Clean Architecture**, encapsulamento estrito (*POO*) e padrões de projeto avançados (*Design Patterns*), focando na robustez, resiliência e observabilidade profissional.

---

## 🚀 Principais Destaques Técnicos

- **Clean Architecture & Separação de Responsabilidades:** Camadas bem definidas isolando Regras de Domínio, Camada de Serviço, Repositórios e API Controllers.
- **Resolução de *Captive Dependency* (Factory Pattern):** O Worker principal roda como um `BackgroundService` (*Singleton*), mas consome repositórios de banco de dados (*Scoped*) de forma segura através de `IServiceScopeFactory`.
- **Agendamento Dinâmico Inteligente:** O sistema dispensa loops cegos baseados em atrasos fixos globais. Ele utiliza uma estratégia temporal no banco de dados (`NextCheckAt`), garantindo que cada site respeite o seu próprio intervalo de verificação configurado de forma independente.
- **Gerenciamento Seguro de Rede:** Uso de `IHttpClientFactory` para evitar o esgotamento de *Sockets* (*Socket Exhaustion*), combinado com a classe `Stopwatch` para rastrear latências em milissegundos.
- **Persistência de Histórico (*Uptime/Downtime*):** Cada checagem gera entidades de log estruturadas (`CheckLog`), permitindo auditoria e análises futuras.
- **Observabilidade com Serilog:** Logs coloridos e estruturados no console com carimbo de data/hora otimizado e ícones visuais para facilitar o monitoramento operacional.

---

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C# 14 / .NET 10
- **Banco de Dados:** PostgreSQL
- **ORM:** Entity Framework Core (EF Core) com Npgsql
- **Concorrência e Workers:** `BackgroundService` & `Task`
- **Logging:** Serilog (`Serilog.AspNetCore` e `Serilog.Sinks.Console`)
- **Containerização:** Docker & Docker Compose

---

## 📁 Estrutura do Projeto

```text
Sentinel.Api/
├── Controllers/       # Endpoints HTTP da API (Monitors, Logs)
├── Data/              # DbContext do EF Core e configurações de banco
├── Entities/          # Modelos de domínio ricos (Monitor, CheckLog)
├── Repositories/      # Padrão Repository e Interfaces de acesso a dados
├── Services/          # Regras de negócio da aplicação
├── Workers/           # Background Service de monitoramento contínuo (UptimeCheckerWorker)
├── DTOs/              # Objetos de Transferência de Dados
└── Program.cs         # Configuração de IoC, Injeção de Dependências e Pipeline
```

---

## ⚙️ Como Executar o Projeto

### Pré-requisitos
Certifique-se de ter instalado em sua máquina:
- [.NET SDK 10.0.0 ou superior](https://dotnet.microsoft.com/)
- [Docker e Docker Compose](https://www.docker.com/)

### 1. Clonar o Repositório
```bash
git clone https://github.com/Lucas-M7/sentinel.git
cd sentinel/Sentinel.Api
```

### 2. Subir o Banco de Dados (Docker)
Certifique-se de ter o arquivo `docker-compose.yml` configurado com o PostgreSQL e execute:
```bash
docker-compose up -d
```

### 3. Aplicar as Migrations do EF Core
Gere e aplique as migrações para criar as tabelas no PostgreSQL:
```bash
dotnet ef database update
```

### 4. Executar a Aplicação
```bash
dotnet run
```
A API estará rodando localmente. Você poderá acessar a documentação interativa via **Swagger** na URL exibida no seu terminal (geralmente `http://localhost:5000/swagger` ou `https://localhost:7100/swagger`).

---

## 📌 Principais Endpoints da API

| Método | Rota | Descrição |
| :--- | :--- | :--- |
| `POST` | `/api/monitors` | Cadastra um novo site para monitoramento (requer nome, URL e intervalo). |
| `GET` | `/api/monitors` | Lista todos os monitores cadastrados. |
| `GET` | `/api/monitors/{id}/logs` | Retorna o histórico de logs de verificações (sucessos/erros e latência) de um monitor específico. |

---

## 👨‍💻 Autor

Desenvolvido por **Lucas Mateus**. 
Se este projeto te inspirou ou foi útil, deixe uma ⭐ no repositório!