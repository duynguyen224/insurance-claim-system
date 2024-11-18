# Insurance Management System

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

# About

Full-stack Developer Technical Assessment

# Features

- This system allows you to submit insurance claims online.
- You can track the status and manage your claims if you have an account.
- Alternatively, you can submit claims as an anonymous user.
- Your claims will be processes with a 50/50 chance of - approval or rejection.

# System Analytics


# Technologies

- Back-end: C# with .NET Core (.NET 8)
- Database: In-memory database
- Front-end: HTML, CSS, jQuery

# How to Run This Project

## Prerequisites

- Install .NET 8 SDK
- Install Visual Studio 2022 with the "ASP.NET and web development" workload.

## Steps

1. Clone the repository

```
  git clone <repository_url>
  cd <project_directory>
```

2. Run the Back-End API

- Open the project in Visual Studio.
- Restore dependencies

```
dotnet restore
```

- Start the project

```
dotnet run
```

- The API will be available at: https://localhost:<7139> or http://localhost:<7139>

3. Run the Front-End Application

- Open the index.html file in a browser.
- Ensure the back-end API is running for full functionality.

# Code Structure and Design

The project follows the principles of clean architecture with a clear separation of concerns, even though all components are organized within a single project. This approach keeps the structure simple and manageable.

## Key Design Principles

### RESTful API Design:

- The API is designed following RESTful conventions to provide consistency and ease of use.
- Example routes:
  - `GET /api/claims?status={status}` – Retrieve claims filtered by status.
  - `POST /api/claims` – Submit a new claim.
  - `PUT /api/claims/{id}/process` – Process a claim (approve or reject).
  - `DELETE /api/claims/{id}` – Delete a claim

### Service Layer

- Encapsulates the business logic in service classes, ensuring the controllers remain lightweight and focused on handling HTTP requests.
- Interfaces define contracts, and concrete implementations handle logic.
- Example:
  - **Interface**: IClaimService
  - **Concrete Implementation**: ClaimService

### Repository Pattern

- Data access logic is abstracted into repository classes, adhering to the single-responsibility principle.
- Interfaces define database access contracts, while concrete implementations handle the operations.
- Example:
  - **Interface**: IClaimRepository
  - **Concrete Implementation**: ClaimRepository

## Folder Structure

Within the single project, the classes are logically grouped into folders:

- **Controllers**: Handle API routing and HTTP request/response processing.
- **Services**: Contain the business logic and interact with repositories to fulfill API requests.
- **Repositories**: Handle data access operations, abstracting database interactions.
- **Models**: Define the entity data model.
- **DTOs**: Define the data transfer objects for request and response models.
- **Extensions**: Contains extension methods to simplify or enhance existing functionality.
- **Data**: Handles the database setup and configurations, including the DbContext class.
- **Mappings**: Contains mapper profiles to map between domain models and DTOs.
- **Middlewares**: Handles global error, logging, ...

# API Documentation

Refer: https://github.com/ml-archive/readme/blob/master/Documentation/how-to-write-apis.md

| Method | URL              | Description              |
| :----: | :--------------- | :----------------------- |
|  GET   | /api/claims      | Retrieve all claims      |
|  GET   | /api/claims/{id} | Retrieve claim #id       |
|  POST  | /api/claims      | Create a new claim       |
|  PUT   | /api/claims/{id} | Update data in claim #id |
| DELETE | /api/claims/{id} | Delete claim #id         |

# Testing

As I am not yet familiar with writing unit tests, I chose to manually test the system to ensure its functionality and reliability. Since unit testing was outside my expertise, I dedicated my time to developing the optional front-end application to enhance the overall project and demonstrate additional skills.

As a next step, I plan to:

- Learn how to write unit tests using testing frameworks like xUnit and MSTest.
- Integrate unit tests and integration tests into my workflow for future projects to ensure robust and scalable systems.

# Challenges and Decisions
