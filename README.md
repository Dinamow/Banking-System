# Banking-System

A Banking System with RESTful API and Database Persistence in ASP.NET Core Web API.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
- [Configuration](#configuration)
- [License](#license)

## Introduction

The Banking System is a web application built with ASP.NET Core Web API that provides a RESTful API for managing bank accounts, transactions, and user authentication. It includes features such as account creation, balance inquiry, deposits, withdrawals, and transfers.

## Features

- User registration and authentication using JWT.
- Account management (create, get balance).
- Transaction management (deposit, withdraw, transfer).
- Daily interest calculation for savings accounts.
- Swagger documentation for API endpoints.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker](https://www.docker.com/get-started) (optional, for containerized deployment)

## Getting Started

1. **Clone the repository:**

    ```sh
    git clone https://github.com/your-username/Banking-System.git
    cd Banking-System
    ```

2. **Set up the database:**

    Update the connection string in [appsettings.json](http://_vscodecontentref_/0) to point to your SQL Server instance:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Your SQL Server connection string here"
      }
    }
    ```

3. **Apply migrations:**

    ```sh
    dotnet ef database update
    ```

## Running the Application

### Using Visual Studio

1. Open the solution file [Banking-System.sln](http://_vscodecontentref_/1) in Visual Studio.
2. Set the startup project to [Banking-System](http://_vscodecontentref_/2).
3. Press `F5` to run the application.

### Using Command Line

1. Navigate to the project directory:

    ```sh
    cd Banking-System
    ```

2. Run the application:

    ```sh
    dotnet run
    ```

### Using Docker

1. Build the Docker image:

    ```sh
    docker build -t banking-system .
    ```

2. Run the Docker container:

    ```sh
    docker run -p 8080:80 -p 8081:443 banking-system
    ```

## API Endpoints

### Authentication

- **Register:** `POST /register`
- **Login:** `POST /login`

### Account Management

- **Get Account:** `GET /api/accounts/{id}`
- **Create Account:** `POST /api/accounts`
- **Get Account Balance:** `GET /api/accounts/{id}/balance`

### Transaction Management

- **Deposit:** `POST /api/account/deposit`
- **Withdraw:** `POST /api/account/withdraw`
- **Transfer:** `POST /api/account/transfer`

## Configuration

### [appsettings.json](http://_vscodecontentref_/3)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Your SQL Server connection string here"
  },
  "Jwt": {
    "Issuer": "http://localhost:5000",
    "Audience": "http://localhost:5000",
    "Key": "Your JWT secret key here",
    "TokenValidityMins": 30
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## License

This project is licensed under the MIT License. See the [LICENSE](http://_vscodecontentref_/4) file for details.
