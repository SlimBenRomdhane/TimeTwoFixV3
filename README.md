# TimeTwoFix 

TimeTwoFix is a system designed for managing work orders, interventions, vehicle repairs, and spare parts with an emphasis on scalability, security, and maintainability. Built using ASP.NET Core and following Clean Architecture principles, it ensures a well-structured, testable, and flexible codebase.

Key Features
- Identity & Authentication: Uses ASP.NET Core Identity with role-based authorization (UserManager, RoleManager, SignInManager).
- Unit of Work Pattern: Ensures efficient transaction management across multiple repositories.
- Repository Pattern: Encapsulates data access logic with dedicated repositories for entities like Clients, Vehicles, WorkOrders, and Interventions.
- Scalability: Designed to be modular and easy to extend, supporting future integrations.
- Security: Implements best practices for authentication, password hashing, and role-based access control.


Technology Stack
- ASP.NET Core 8 – Backend framework
- Entity Framework Core – ORM for database interactions
- SQL Server – Database storage
- Clean Architecture – Modular design principle


Architecture Overview
Layers
- Domain Layer – Business logic and core models.
- Application Layer – Use cases and service logic.
- Infrastructure Layer – Database configuration, Identity integration.
- Presentation Layer (Razor) .
