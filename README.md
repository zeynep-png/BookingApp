
# Hotel Management System

A comprehensive hotel management system built with ASP.NET Core, designed to handle multiple aspects of hotel administration, including guest management, room reservations, and service management. This project follows a layered architecture and incorporates essential features like authentication, authorization, logging, and error handling.

---

## Table of Contents
- [Project Overview](#project-overview)
- [Technologies Used](#technologies-used)
- [System Architecture](#system-architecture)
- [Key Features](#key-features)
  - [Authentication and Authorization](#authentication-and-authorization)
  - [API Endpoints](#api-endpoints)
  - [Middleware](#middleware)
  - [Business Logic](#business-logic)
  - [Data Layer](#data-layer)
  - [Validation and Filters](#validation-and-filters)
  - [Exception Handling](#exception-handling)
  - [Data Protection](#data-protection)
- [Installation and Setup](#installation-and-setup)
- [Usage](#usage)
- [License](#license)

---

## Project Overview

The Hotel Management System (HMS) is built to simplify and automate hotel operations. The system provides a secure platform for managing guests, rooms, bookings, and services, implementing a robust authentication and authorization mechanism with JWT.


![image](https://github.com/user-attachments/assets/fd2da52c-3ede-4dbb-a15f-a0a4a10893a0)




## Technologies Used

- **Framework**: ASP.NET Core
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity / JWT
- **Dependency Injection**
- **Middleware for Logging and Maintenance Mode**
- **Data Protection**: ASP.NET Core Data Protection
- **Exception Handling**: Global Exception Middleware

## System Architecture

This application is designed with a three-layered architecture to separate concerns and improve modularity:

1. **Presentation Layer (API Layer)**: Contains Controllers for handling API requests.
2. **Business Layer (Service Layer)**: Contains the core business logic.
3. **Data Access Layer (Repository Layer)**: Manages database operations, using Entity Framework for data transactions.

## Key Features

### Authentication and Authorization

- **JWT (JSON Web Token)**: Secures API endpoints and enables user roles and permissions.
- **Roles**: Includes roles like "Customer" and "Admin" for different access levels.
- **ASP.NET Core Identity**: Used for user management, or alternatively, a custom identity service.

![image](https://github.com/user-attachments/assets/2efe551b-eddf-45d9-b303-5ea1a635815e)



### API Endpoints

The system provides comprehensive API endpoints for interacting with hotel data. These include:

- **GET**: Retrieve data (e.g., list of guests, room details, etc.).
- **POST**: Create new records (e.g., booking, guest registration).
- **PUT**: Update existing records.
- **PATCH**: Partially update records.
- **DELETE**: Remove records.

  ![image](https://github.com/user-attachments/assets/b10fbb12-5cf7-490b-aad1-8348474ba611)


### Middleware

1. **Logging Middleware**:
   - Logs every incoming request, capturing details such as URL, request time, and customer identity.

2. **Maintenance Mode Middleware**:
   - This middleware can activate a maintenance mode for the system. This functionality is supported by an additional database table to control maintenance status.

### Business Logic

The Business Layer implements various services, with logic encapsulated to ensure data integrity and proper application flow. This layer handles crucial operations such as room availability checks, booking status updates, and guest registration.

### Data Layer

- **Entity Framework Core (Code First)**: Defines models and relationships, including one many-to-many relationship (e.g., between Guests and Rooms).
- **Repository Pattern and Unit of Work**: Ensures efficient data operations and separation of data access code.

### Validation and Filters

1. **Model Validation**:
   - Ensures data accuracy with validation rules such as:
     - Valid email format for guests.
     - Required fields like guest name and room type.
     - Positive stock count for room availability.

2. **Action Filters**:
   - A custom Action Filter restricts access to specific APIs based on time periods, ensuring that certain features are accessible only during specified hours.

### Exception Handling

A Global Exception Handling mechanism is implemented to intercept and manage errors across the application. This feature returns a unified error response to users, improving the system's resilience and user experience.

### Data Protection

Data Protection safeguards sensitive information such as passwords. It is implemented using ASP.NET Core Data Protection, ensuring that passwords and sensitive data are stored securely.

## Installation and Setup

1. **Clone the Repository**:
   ```bash
   git clone <repository-url>
   ```

2. **Navigate to the Project Directory**:
   ```bash
   cd HotelManagementSystem
   ```

3. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```

4. **Setup Database**:
   - Ensure SQL Server is installed and running.
   - Configure the connection string in `appsettings.json`.
   - Run migrations to create the database schema:
     ```bash
     dotnet ef database update
     ```

5. **Run the Application**:
   ```bash
   dotnet run
   ```

## Usage

The API offers endpoints for managing hotels, rooms, bookings, and guests. To access the endpoints, ensure that your requests include the necessary JWT token for authentication. Admin users have full access to all endpoints, while customer access is limited according to role-based restrictions.

