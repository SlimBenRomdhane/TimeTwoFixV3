# Copilot Instructions for TimeTwoFix

## Project Overview
- **TimeTwoFix** is a modular ASP.NET Core 8 application for managing work orders, interventions, vehicle repairs, and spare parts.
- Follows **Clean Architecture**: Core (domain), Application, Infrastructure, and Web (presentation) layers.
- Uses **Entity Framework Core** for data access and **SQL Server** as the primary database.

## Key Architectural Patterns
- **Repository & Unit of Work**: All data access is abstracted via repositories and coordinated with Unit of Work for transaction safety. See `TimeTwoFix.Core/Common/Entities/` and `TimeTwoFix.Application/Base/`.
- **Service Segmentation**: Each domain (e.g., Client, Vehicle, WorkOrder) has its own service, DTOs, interfaces, and mapping in `TimeTwoFix.Application/<Domain>Services/`.
- **DTO Mapping**: Data transfer between layers uses explicit mapping classes in `Mapping/` folders.
- **Role-based Security**: Authentication/authorization handled via ASP.NET Core Identity (see `TimeTwoFix.Infrastructure/Extension/`).

## Developer Workflows
- **Build**: Use Visual Studio or `dotnet build TimeTwoFix.sln` from the root.
- **Run**: Launch `TimeTwoFix.Web` as the startup project. Use `dotnet run --project TimeTwoFix.Web`.
- **Migrations**: Managed via EF Core CLI. Example: `dotnet ef migrations add <Name> --project TimeTwoFix.Infrastructure --startup-project TimeTwoFix.Web`.
- **Testing**: (If present) tests are in `TimeTwoFix.Test/`.

## Project Conventions
- **Folder Structure**: Each service/domain has Dtos, Interfaces, Mapping, and Services subfolders.
- **Naming**: Service, DTO, and mapping classes are named after their domain (e.g., `ClientService`, `ClientDto`, `ClientMapping`).
- **Extension Points**: New domains/services should follow the existing folder and naming conventions.
- **No business logic in controllers**: Controllers in `TimeTwoFix.Web/Controllers/` should delegate to Application services.

## Integration & Communication
- **Data Access**: Only via repositories and Unit of Work.
- **External Integrations**: Add adapters in `TimeTwoFix.Infrastructure/External/`.
- **Shared Utilities**: Place in `TimeTwoFix.Core/Utilities/`.

## Examples
- To add a new entity (e.g., Supplier):
  1. Create entity in `TimeTwoFix.Core/Common/Entities/`.
  2. Add repository interface in `TimeTwoFix.Core/Interfaces/`.
  3. Implement service, DTOs, mapping in `TimeTwoFix.Application/SupplierServices/`.
  4. Register in DI via `TimeTwoFix.Application/Extension/ApplicationRegistration.cs`.

## References
- See `README.md` for high-level overview and tech stack.
- Review `TimeTwoFix.Application/Base/` for base service patterns.
- Use `TimeTwoFix.Infrastructure/Extension/` for integration and identity setup.

---
**Keep instructions up to date as architecture or workflows evolve.**
