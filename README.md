[README.md](https://github.com/user-attachments/files/33428171/README.md)
# LapShop

A laptop e-commerce web application built with **ASP.NET Core (.NET 10)**. It has a customer storefront, an Admin dashboard, and a RESTful API secured with JWT.

> This project started from a course and I keep extending it to practice clean architecture, design patterns and API design.

## Features

**Storefront (MVC)**
- Browse items by category, view item details
- Cookie-based shopping cart (add / remove items, totals)
- Order success page and "My Orders"
- Register / login with ASP.NET Core Identity

**Admin area** (role: `Admin`)
- Manage categories, items and homepage sliders (CRUD)
- Admin dashboard

**REST API** (`/api/...`)
- Items: list with **pagination, sorting and filtering** (by category / search term), create, update, delete (Admin only)
- Categories API
- Account API: register and login, returns a **JWT** token
- Swagger UI in development, global exception handling with `ProblemDetails`

## Architecture

N-tier solution with separated concerns:

```
LapShop.slnx
├── Domains/          # Entities (Tb*, Vw*), shared models (PagedResult, ApplicationUser)
├── BusinessLayer/    # DbContext, EF Core migrations, Repository + Unit of Work
│   ├── Bl/Interfaces
│   └── Bl/Classes
└── LapShop/          # Web layer: MVC controllers + views, Admin area, API controllers, DTOs, AutoMapper profiles
```

**Patterns and practices**
- Generic Repository (`IGenericRepository<T>`) and Unit of Work (`IUnitOfWork`)
- Dependency Injection for all services
- DTOs + AutoMapper between entities and API models
- MVC with Areas
- Role-based authorization (`Admin`, `Customer`)
- Cookie authentication for MVC, JWT bearer for the API
- Global exception handler (`IExceptionHandler`) returning `ProblemDetails`

## Tech stack

C#, ASP.NET Core MVC, ASP.NET Core Web API, Entity Framework Core, SQL Server, ASP.NET Core Identity, JWT, AutoMapper, Swagger (Swashbuckle), Bootstrap, JavaScript

## Getting started

**Prerequisites:** .NET 10 SDK, SQL Server (or LocalDB)

1. Clone the repository
   ```bash
   git clone https://github.com/shawket12/LapShop.git
   cd LapShop
   ```
2. Configure `LapShop/appsettings.json` (or use User Secrets, recommended):
   ```json
   {
     "ConnectionStrings": { "DefaultConnection": "Server=.;Database=LapShop;Trusted_Connection=True;TrustServerCertificate=True" },
     "Jwt": { "Key": "<a long random secret, at least 32 characters>" }
   }
   ```
3. Create the database:
   ```bash
   dotnet ef database update --project BusinessLayer --startup-project LapShop
   ```
4. Make sure the `Admin` and `Customer` roles exist in the database (registration assigns the `Customer` role).
5. Run:
   ```bash
   dotnet run --project LapShop
   ```
   - Site: https://localhost:7050
   - Swagger (Development): https://localhost:7050/swagger

## Roadmap

- [ ] Move the remaining services (`Items`, `Categories`, `Sliders`...) to the Generic Repository + Unit of Work
- [ ] Store the shopping cart in the database instead of cookies
- [ ] Seed default roles and an admin user
- [ ] Persist orders through the sales invoice entities
- [ ] Unit tests

## Screenshots

_Add screenshots of the storefront, admin dashboard and Swagger here._

## Author

**Shauket Sayed** — [LinkedIn](https://www.linkedin.com/in/shawkat-sayed-b6115a233) · [GitHub](https://github.com/shawket12)
