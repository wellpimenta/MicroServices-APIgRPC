# MicroServices-APIgRPC

# TaskGrpcApi

Uma API gRPC desenvolvida com .NET 7 C# para gerenciamento de tarefas com operações de criar, listar, atualizar e excluir. A API utiliza Protocol Buffers para definições de mensagens e serviços, Dapper ORM com SQLite para persistência de dados, e autenticação e autorização baseada em JWT para proteger os endpoints. A aplicação é containerizada usando Docker.

---

## Funcionalidades

- API gRPC com operações CRUD em tarefas
- Protocol Buffers para definições de contratos de serviço e mensagem
- Banco de dados SQLite com Dapper ORM para acesso leve aos dados
- Autenticação e autorização JWT
- Suporte a Docker para implantação containerizada

---

## Primeiros Passos

### Pré-requisitos

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Docker](https://www.docker.com/get-started) (opcional, para containerização)
- Cliente gRPC (ex: [BloomRPC](https://github.com/uw-labs/bloomrpc), [Postman](https://www.postman.com/) ou cliente personalizado)

---

## Configuração e Execução

### Clonar o repositório

```bash
git clone <url-do-repositorio>
cd TaskGrpcApi
