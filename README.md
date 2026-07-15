# API Boilerplate - ASP.NET Core

A clean architecture boilerplate for building RESTful APIs using ASP.NET Core, following best practices and separation of concerns.

## < Architecture

This project follows a **Clean Architecture** approach with the following layers:

```
src/
%%% Boilerplate-ASPNet-API.Domain/          # Domain entities and business logic
%%% Boilerplate-ASPNet-API.Application/     # Application services, DTOs, and interfaces
%%% Boilerplate-ASPNet-API.Infrastructure/  # Data access and external services
%%% Boilerplate-ASPNet-API.WebApi/          # API controllers and presentation layer
```

### Project Structure

| Layer | Description |
|-------|-------------|
| **Domain** | Contains core business entities (e.g., `User`) |
| **Application** | Business logic, services, DTOs, and interfaces |
| **Infrastructure** | Database context, repositories, and external dependencies |
| **WebApi** | REST API endpoints, controllers, and middleware |

## =؀ Features

- ' **Clean Architecture** - Separation of concerns with clear layer boundaries
- ' **RESTful API** - Standard HTTP methods for CRUD operations
- ' **Entity Framework Core** - ORM with SQLite database
- ' **Dependency Injection** - Built-in IoC container
- ' **Swagger/OpenAPI** - Interactive API documentation
- ' **DTO Pattern** - Data transfer objects for API requests/responses
- ' **Generic API Response** - Consistent response format with `ApiResponse<T>`
- ' **Auto-migration** - Database auto-creation on startup

## = Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- [SQLite](https://www.sqlite.org/download.html) (included with EF Core)

## = Getting Started

### Clone the Repository

```bash
git clone (https://github.com/GalaMaya/API-Broilerplate-ASP.NET)
cd Boilerplate-ASPNet-API
```

### Restore Dependencies

```bash
dotnet restore
```

### Run the Application

```bash
cd src/Boilerplate-ASPNet-API.WebApi
dotnet run
```

The API will be available at:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`

### Swagger UI

Access the interactive API documentation at:
```
https://localhost:5001/swagger
```

## = API Endpoints

### Users Controller (`/api/users`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/api/users` | Create a new user |
| `GET` | `/api/users` | Get all users |
| `GET` | `/api/users/{id}` | Get user by ID |
| `PUT` | `/api/users/{id}` | Update an existing user |

### Example Requests

#### Create User

```bash
curl -X POST https://localhost:5001/api/users \
  -H "Content-Type: application/json" \
  -d '{
    "name": "John Doe",
    "email": "john@example.com",
    "passwordHash": "hashed_password"
  }'
```

#### Get All Users

```bash
curl -X GET https://localhost:5001/api/users
```

#### Get User by ID

```bash
curl -X GET https://localhost:5001/api/users/{id}
```

#### Update User

```bash
curl -X PUT https://localhost:5001/api/users/{id} \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Jane Doe",
    "email": "jane@example.com"
  }'
```

## = Database

The application uses **SQLite** with Entity Framework Core. The database file (`boilerplate.db`) is automatically created on first run.

### Connection String

Located in `Program.cs`:
```csharp
options.UseSqlite("Data Source=boilerplate.db");
```

## = Dependencies

### Main Packages

| Package | Version | Purpose |
|---------|---------|---------|
| `Microsoft.EntityFrameworkCore` | 10.0.9 | ORM framework |
| `Microsoft.EntityFrameworkCore.Sqlite` | 10.0.9 | SQLite provider |
| `Swashbuckle.AspNetCore` | 7.2.0 | Swagger/OpenAPI documentation |

## =' Development

### Build the Solution

```bash
dotnet build
```

### Run Tests (if available)

```bash
dotnet test
```

### Add Migration (if needed)

```bash
dotnet ef migrations add InitialCreate --project src/Boilerplate-ASPNet-API.Infrastructure --startup-project src/Boilerplate-ASPNet-API.WebApi
```

### Update Database

```bash
dotnet ef database update --project src/Boilerplate-ASPNet-API.Infrastructure --startup-project src/Boilerplate-ASPNet-API.WebApi
```

## = Code Examples

### Creating a Service

Services are defined in the Application layer:

```csharp
public interface IUserService
{
    Task<User> CreateUserAsync(CreateUserRequest request);
    Task<User?> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);
    Task<UserResponseDto> UpdateUserAsync(Guid Id, UpdateUserRequest request);
}
```

### Registering Dependencies

Dependencies are registered in `Program.cs`:

```csharp
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
```

## > Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request
