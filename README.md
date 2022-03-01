# Departure-Management-System

This is a **web** implementation of a departure management system that can be used by companies or any organizations who want to have full control over the leaves of their employees.

This project provides complete solution following the principles of **Clean Architecture** which powered by an **internal REST API** all using latest .NET Core.

## Technology stack

#### Architecture Pattern

* [Clean architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

#### Design Pattern
* [CQRS design pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
* [Mediator design pattern](https://refactoring.guru/design-patterns/mediator)
* [Repository design pattern](https://www.c-sharpcorner.com/article/repository-design-pattern-in-asp-net-mvc/#:~:text=What%20is%20a%20Repository%20Design,entities%20in%20the%20business%20logic.)
* [Unit of work](https://www.c-sharpcorner.com/UploadFile/b1df45/unit-of-work-in-repository-pattern/)

#### Backend
* [Language: C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [Framework: ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)

#### UI
* [Razor view engine](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-6.0)

#### Database
* [MS SQL](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* DB Connectivity : [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/) - Code First Approach

#### Service
* [Web API (Restful service)](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio)

#### Authentication
* [JWT Authentication](https://jwt.io/)

#### Feature
* Custom Exception Handler
* [Auto Mapper](https://automapper.org/)
* [Fluent validation](https://fluentvalidation.net/)
* [Serilog](https://serilog.net/)
* [Swagger UI](https://swagger.io/tools/swagger-ui/)


## Getting Started

Use these instructions to get the project up and running.

#### Prerequisites
* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
* [SQL Server 2019](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) 
* [SQL Server Management Studio 18.x](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15) (Optional)

#### Setup
1. Download or clone the repository
2. Run **`Departure_Management.sln`** with visual studio
3. From visual studio, navigate to src > API and right click on **`Departure_Management.Api`** then hit **`Set as Startup Project`**
4. From Tools tab select **`NuGet Package Manager`** then hit **`Package Manager Console`**
5. Select **`Departure_Management.Identity`** as Default project
6. In the console write below command and hit enter:
```
Update-Database -Context DepartureManagementIdentityDbContext
```
7. Select **`Departure_Management.Persistence`** as Default project
8. In the console write below command and hit enter:
```
Update-Database -Context DepartureManagementDbContext
```
9. Right click on **`Solution Departure_Management`** go to **`Set Startup Projects...`** select **`Multiple Startup projects`** and set Api and MVC projects to be started.
10. Now you can run it by press **`F5`**
11. Default administrator user:
* Email: **`admin@localhost.com`**
* Password: **`Abc123*`**
12. Default employee user:
* Email: **`employee@localhost.com`**
* Password: **`Abc123*`**