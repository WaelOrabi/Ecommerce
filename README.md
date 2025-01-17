
# E-Commerce System API

A robust **E-Commerce System API** designed to manage essential e-commerce functionalities, including orders, categories, products, and the cart. The system leverages **N-Tier Architecture**, the **Repository Pattern**, and **Unit of Work** for modularity, scalability, and maintainability.

---

## Solution Structure

The project is organized into distinct projects and layers, promoting separation of concerns and clean code practices.

### 1. **ECommerce.API**

This project is the entry point for the application, managing HTTP communication and API endpoints.

- **Controllers**:  
  Includes controllers for managing entities like:
  - `ApplicationUserController`: Handles user profile and account operations.
  - `AuthenticationController`: Manages user authentication.
  - `AuthorizationController`: Implements role-based access control (RBAC).
  - `CartController`: Handles cart-related operations (add, remove, update cart items).
  - `CategoryController`: Manages product categories.
  - `OrderController`: Manages orders (creation, updates, cancellations).
  - `ProductController`: Handles product operations (CRUD, filtering, and sorting).

- **Filters**:  
  Provides mechanisms like global exception handling or custom action filters.

- **Extensions**:  
  Contains extension methods for configuring services and middleware.

**Key Files:**
- `Program.cs`: Configures the application, registers services, and sets up the middleware pipeline.
- `appsettings.json`: Stores application-level configurations (e.g., database connection strings, JWT settings).

---

### 2. **ECommerce.Domain**

This project encapsulates the core domain models and shared components.

- **Entities**:  
  Defines the database entities such as:
  - Products
  - Categories
  - Orders
  - Cart Items
  - Users

- **Helpers**:  
  Utility classes or methods used throughout the domain layer.

- **Permission.cs**:  
  Centralized management of permissions and roles for RBAC.

---

### 3. **ECommerce.Infrastructure**

This project handles data access, database operations, and external integrations.

- **Database**:  
  Manages database context and configurations for entity mappings.

- **RepositoriesImplementation**:  
  Implements the **Repository Pattern** for abstracting data access logic.

- **Interfaces**:  
  Contains repository interfaces for interacting with the data layer.

- **Seeder**:  
  Initializes the database with seed data for testing or initial deployment.

- **Extensions**:  
  Provides helper extensions for infrastructure-related setups.

---

### 4. **ECommerce.Application**

This project contains the application logic and shared utilities.

- **DTOs (Data Transfer Objects)**:  
  Defines request and response structures for API endpoints.

- **Services**:  
  Business logic implementation for core operations, such as:
  - Order processing
  - Cart management
  - Product filtering and sorting

- **Bases**:  
  Contains base classes for shared functionalities.

- **Behaviors**:  
  Implements cross-cutting concerns like validation and logging.

- **Middleware**:  
  Manages middleware for authentication, authorization, and exception handling.

- **Resources**:  
  Stores static resources, such as error messages or constants.

- **Mapping**:  
  Configures object mapping (e.g., using AutoMapper).

---

## Features

### **Order Management**
- Create, update, and delete orders.
- Manage order status and track progress.
- Fetch paginated and filtered lists of orders.

### **Product Management**
- CRUD operations for products.
- Implement filtering, sorting, and searching.

### **Category Management**
- Add, update, delete, and fetch product categories.
- Supports hierarchical structures for subcategories.

### **Cart Management**
- Add, update, and remove cart items.
- Calculate totals dynamically (including taxes and discounts).

### **Authentication and Authorization**
- **JWT Authentication**: Secure API endpoints with token-based authentication.
- Role-based access control (RBAC) for different user roles (Admin, Customer).

---

## Technical Highlights

### N-Tier Architecture
- Divides the application into distinct layers for clean separation of concerns.
- Each layer focuses on a specific responsibility.

### Repository Pattern with Unit of Work
- Abstracts data access logic to promote reusability and testability.
- Ensures atomic operations across multiple repositories.

### JWT Authentication
- Secures API endpoints with token-based authentication.
- Provides functionality for token refresh and validation.
