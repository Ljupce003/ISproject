# Personal News Aggregator & Bookmark Manager

A full-stack ASP.NET Core web application that aggregates news from external APIs and allows users to organize articles into a personal collection.

Users can browse articles from multiple sources, bookmark interesting content, organize bookmarks into folders, and export curated collections to Excel for offline use.

The project demonstrates a clean **Onion Architecture**, separation of concerns, API integration, and cloud deployment using Microsoft Azure.

### Original hosting

The original Azure deployment was available at:
- https://isprojectweb20250823163857-b3btc6aeawbehfb2.switzerlandnorth-01.azurewebsites.net

The current application uses **PostgreSQL** for development and production. See [PostgreSQL setup and migrations](docs/database-postgresql.md). The original Azure SQL migration history is retained as an inactive archive.

---

## Features

* **News aggregation**

  * Fetches articles and sources from external news APIs
  * Displays the latest news in a structured interface

* **User bookmarking**

  * Save individual articles to a personal bookmark collection
  * Manage and review saved articles later

* **Bookmark folders**

  * Organize bookmarks into folders
  * Add articles to folders individually or in groups

* **Bulk actions**

  * Select multiple bookmarked articles
  * Add multiple articles to folders simultaneously

* **Export to Excel**

  * Export bookmarked articles or folders into `.xlsx` files
  * Useful for research, archiving, or offline reading

* **Cloud deployment**

  * Originally hosted on **Azure App Service** with **Azure SQL**
  * Current database configuration targets **PostgreSQL**

---

## Architecture

The application follows **Onion Architecture**, which separates responsibilities into clear layers.

### Domain Layer

Contains the core business objects and application contracts.

Includes:

* Domain models
* DTOs
* Core entities used throughout the application

This layer has **no external dependencies** and represents the core of the system.

---

### Repository Layer

Responsible for **data access abstraction**.

Includes:

* Generic repository interface for CRUD operations
* Specialized repository for user-related functionality
* Communication with the database through Entity Framework Core

Purpose:

* Decouple business logic from persistence logic
* Allow easier testing and maintainability

---

### Service Layer

Contains the **application business logic**.

Responsibilities include:

* Calling repositories for CRUD operations
* Fetching news articles from external APIs
* Managing bookmarks
* Managing bookmark folders
* Handling bulk operations
* Generating Excel exports

This layer acts as the **bridge between controllers and repositories**.

---

### Presentation Layer

Implemented using **ASP.NET Core MVC with Razor (CSHTML) views**.

Responsibilities:

* Handling HTTP requests through controllers
* Rendering UI pages
* Calling application services
* Displaying news articles and user collections

---

## Technologies Used

* **ASP.NET Core**
* **C#**
* **Razor Views (CSHTML)**
* **Entity Framework Core**
* **PostgreSQL**
* **External News APIs**
* **Excel Export Libraries (ClosedXML / EPPlus)**
* **Azure App Service**
* **Azure SQL Database**

---

## Database

The application uses **PostgreSQL** to store:

* Users
* Bookmarked articles
* Bookmark folders
* Relationships between folders and articles

Active EF Core migrations and their model snapshot are in `ISproject.Repository/Data/Migrations/PostgreSql`. Historical SQL Server migrations are in `Data/Migrations/SqlServer` and excluded from compilation.

See [PostgreSQL setup and migrations](docs/database-postgresql.md) for connection configuration and database update commands.

---

## Deployment

Production must provide PostgreSQL credentials through `ConnectionStrings__DefaultConnection` or the structured `Database__*` settings used by Docker Compose. The application does not apply migrations automatically at startup; apply the committed PostgreSQL migrations as a deployment step.

The new initial migration creates a fresh schema. Moving existing Azure SQL data into PostgreSQL requires a separate data transfer. See [Docker build, image publishing, and VM deployment](docs/docker-deployment.md) for the production Compose setup. The deployment uses separate published app and migration images, with native or Docker Nginx support.

---

## Project Goals

This project was built to demonstrate:

* Clean architecture principles
* Separation of concerns
* Integration with external APIs
* Database-driven web application design
* Cloud deployment using Azure

<!--
---

## Possible Future Improvements

* Article search and filtering
* Tagging system for bookmarks
* Authentication via OAuth providers
* Background jobs for automatic news updates
* REST API for mobile clients -->

---

## Author

Made by: [Ljupcho Angelovski](https://ljupchoangelovski.com)

