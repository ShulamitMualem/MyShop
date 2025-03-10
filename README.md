# MyShop - WEB API .NET 8

## Overview
MyShop is a RESTful Web API built with .NET 8, designed following best practices of REST architecture. The project is structured in layers to ensure scalability, maintainability, and clean code principles.

### API Documentation
The API is documented using Swagger. You can access the API documentation via the following link:
```
https://localhost:44379/Swagger/index.html
```

## Project Structure
The project is divided into multiple layers:

1. **Presentation Layer (Controllers)** - Handles HTTP requests and responses.
2. **Application Layer (Services, DTOs)** - Contains business logic and manages data transfer objects (DTOs).
3. **Domain Layer (Entities, Interfaces)** - Represents core domain logic and entity definitions.
4. **Infrastructure Layer (Database, Repositories, Logging, Configuration)** - Handles persistence, logging, and configurations.

### Why Layers?
- **Separation of concerns** - Each layer has a distinct responsibility.
- **Scalability** - The architecture supports adding new features without affecting existing code.
- **Testability** - Layers make it easier to write unit and integration tests.

## Key Features

- **AutoMapper**: Used for mapping between DTOs and domain entities.
- **Dependency Injection (DI)**: Services and repositories are injected using .NET’s built-in DI to ensure loose coupling.
- **Asynchronous Processing**: Implemented using `async/await` for scalability and performance.
- **SQL Database with Code First and Database First Approach:** The database can be managed using Entity Framework Core with migrations or by designing the database first and generating the code. This project was built using the Database First approach.
- **Configuration Management**: App settings are managed through config files.
- **Global Error Handling (Middleware)**: Errors are logged and fatal errors are sent via email.
- **Request Logging**: Every request is logged for analytics and rating purposes.

## Database Setup
To set up the database, use the following commands:
```sh
# Add migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

## Running the Project
1. Clone the repository.
2. Restore dependencies:
   ```sh
   dotnet restore
   ```
3. Run the application:
   ```sh
   dotnet run
   ```

## Clean Code Principles
The project follows clean code principles to ensure maintainability:
- Meaningful variable and method names.
- Separation of concerns.
- Avoiding magic numbers and hardcoded values.
- Proper exception handling.

---
## Contact Information
- **Name**: Shulamit Mualem
- **Phone**: 0548552214
- **Email**: c0527671088@gmail.com
