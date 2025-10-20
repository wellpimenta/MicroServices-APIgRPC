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

git clone <url-do-repositorio>
cd TaskGrpcApi

###Instalar dependências

dotnet add package Dapper
dotnet add package Microsoft.Data.Sqlite
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

Definição do Serviço Proto
As definições de serviço e mensagem estão localizadas em Protos/tasks.proto.

##RPCs Principais:
AuthService.Authenticate - autenticar usuário e obter token JWT

TaskService.CreateTask - criar uma nova tarefa

TaskService.ListTasks - listar todas as tarefas

TaskService.UpdateTask - atualizar uma tarefa existente

TaskService.DeleteTask - excluir uma tarefa por ID

##Banco de Dados
Utiliza SQLite (arquivo tasks.db) criado automaticamente na primeira execução

Tabela: Tasks com colunas Id, Title, Description e IsCompleted

Acesso a dados via Dapper ORM para desempenho e simplicidade

##Testando a API
Você pode usar clientes gRPC como BloomRPC ou Postman (com suporte a gRPC) para testar os endpoints:

##Fluxo de Teste:
Autentique-se para obter um token JWT

Adicione o token ao cabeçalho de metadados Authorization nas requisições subsequentes do TaskService

Execute operações CRUD de tarefas

##Exemplo de Uso:

# Executar a aplicação
dotnet run

# A aplicação estará disponível em https://localhost:7000
Observações
A chave de assinatura JWT e as credenciais de usuário estão codificadas apenas para demonstração

Para produção, use armazenamento seguro para segredos e um bande de dados de usuários real

HTTPS está habilitado por padrão ao executar localmente com dotnet run

Certifique-se de que as portas necessárias estejam disponíveis para a aplicação

Estrutura do Projeto
text
TaskGrpcApi/
├── Protos/
│   └── tasks.proto
├── Services/
├── Models/
├── Data/
└── README.md
Licença
Este projeto está licenciado sob a MIT License.

text

Here's the downloadable README.md file. You can copy this content and save it as `README.md` in your project directory.
