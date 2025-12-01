### Product Inventory Management API ###

# Overview

This project is a robust, RESTful Web API designed to manage product inventories. It was built as a technical assessment to demonstrate Enterprise-level application architecture, SOLID principles, and Secure API design using .NET 8.

The application focuses on scalability, maintainability, and performance optimization through a layered architecture and advanced middleware configurations.

# Security & Resilience

JWT Authentication: Secure access control using Bearer tokens, protecting sensitive endpoints (POST, PUT, DELETE).
Global Exception Handling: A centralized custom Middleware catches all unhandled exceptions, ensuring the API always returns structured, secure, and user-friendly error responses (preventing stack trace leaks).

# Performance & Optimization
Optimized JSON Serialization: Configured System.Text.Json to automatically omit null values (e.g., InStock when out of stock). This reduces payload size and bandwidth usage significantly.
Bulk Operations: Implemented /bulk endpoints to handle high-volume data insertion efficiently.
In-Memory Database: Utilizes EF Core In-Memory provider for rapid prototyping and zero-dependency testing (easily swappable for SQL Server).

# Architecture
Layered Design: Strict separation of concerns between Controllers (Presentation), Services (Business Logic), and Data (Persistence).
DTO Pattern: Decoupled internal Entities from external API contracts using Data Transfer Objects (Request/Response models).

# Technology Stack
Core Framework: .NET 8 (ASP.NET Core Web API)
ORM: Entity Framework Core
Authentication: JWT Bearer (Microsoft.AspNetCore.Authentication.JwtBearer)
Documentation: Swagger / OpenAPI (Swashbuckle)
Serialization: System.Text.Json


# API Endpoints

GET /api/Products = Retrieves all products. Supports filtering by category, minPrice, maxPrice.

GET /api/Products/{id} = Retrieves a single product detail.

POST /api/Products = Creates a new single product.

POST /api/Products/bulk = Bulk Insert: Creates multiple products in one request.

PUT /api/Products/{id} = Updates an existing product.

DELETE /api/Products/{id} = Deletes a product.

POST /api/Auth/login = Generates a JWT Token for testing (User: admin, Pass: password).

# How to Run

Clone the repository
Restore packages:
dotnet restore

Run the application:
dotnet run


# Access Swagger UI:
Open your browser to https://localhost:<PORT>/swagger/index.html 

 

# To seed this data:
Authenticate using the steps above.
Open the DummyData.json file and copy its content.
Go to the POST /api/Products/bulk endpoint in Swagger.
Paste the JSON content into the request body and execute.
Use GET /api/Products with filters (e.g., minPrice, category) to verify the data and the InStock logic.

