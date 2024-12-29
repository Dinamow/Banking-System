# Banking-System

A Banking System with RESTful API and Database Persistence in ASP.NET Core Web API.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
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

## Configuration

Before running the application, no manual configuration is required. The application will automatically configure itself and apply the necessary database migrations upon startup.

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

1. Pull the prebuilt Docker image from Docker Hub:

   ```sh
   docker pull dinamow/bankingsystem
   ```

2. Run the Docker container:

   ```sh
   docker run -p 8080:8080 -p 8081:8081 dinamow/bankingsystem
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

## License

This project is licensed under the MIT License.
