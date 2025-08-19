# MicroServices-APIgRPC

# TaskGrpcApi

A gRPC API built with .NET 7 C# for managing tasks with create, list, update, and delete operations. The API uses Protocol Buffers for message and service definitions, 
Dapper ORM with SQLite for data persistence, and JWT-based authentication and authorization to secure endpoints. The application is containerized using Docker.

---

## Features

- gRPC API with CRUD operations on tasks
- Protocol Buffers for message and service contract definitions
- SQLite database with Dapper ORM for lightweight data access
- JWT authentication and authorization
- Docker support for containerized deployment

---

## Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Docker](https://www.docker.com/get-started) (optional, for containerization)
- gRPC client (e.g., [BloomRPC](https://github.com/uw-labs/bloomrpc), [Postman](https://www.postman.com/), or custom client)

---

## Setup and Run

### Clone the repository

```bash
git clone <repository-url>
cd TaskGrpcApi

dotnet add package Dapper
dotnet add package Microsoft.Data.Sqlite
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

## Proto Service Definition

The service and message definitions are located in Protos/tasks.proto.

### Key RPCs:
- AuthService.Authenticate - authenticate user and get JWT token
- TaskService.CreateTask - create a new task
- TaskService.ListTasks - list all tasks
- TaskService.UpdateTask - update an existing task
- TaskService.DeleteTask - delete a task by ID
### Database
- Uses SQLite (tasks.db) file created automatically on first run.
- Table: Tasks with columns Id, Title, Description, and IsCompleted.
- Data access via Dapper ORM for performance and simplicity.

## Testing the API
You can use gRPC clients like BloomRPC or Postman (with gRPC support) to test the endpoints:

### Authenticate to get a JWT token.
- Add the token to the Authorization metadata header on subsequent TaskService requests.
- Perform task CRUD operations.
### Notes
- The JWT signing key and user credentials are hard-coded for demonstration only.
- For production, use secure storage for secrets and a real user database.
- HTTPS is enabled by default when running locally with dotnet run.

