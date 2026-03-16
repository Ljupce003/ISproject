# Personal News Aggregator & Bookmark Manager

A full-stack ASP.NET Core web application that aggregates news from external APIs and allows users to organize articles into a personal collection.

Users can browse articles from multiple sources, bookmark interesting content, organize bookmarks into folders, and export curated collections to Excel for offline use.

The project demonstrates a clean **Onion Architecture**, separation of concerns, API integration, and cloud deployment using Microsoft Azure.

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

  * Application hosted on **Azure App Service**
  * Database hosted on **Azure SQL**

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
* **SQL Server**
* **External News APIs**
* **Excel Export Libraries (ClosedXML / EPPlus)**
* **Azure App Service**
* **Azure SQL Database**

---

## Database

The application uses **SQL Server** to store:

* Users
* Bookmarked articles
* Bookmark folders
* Relationships between folders and articles

The database is hosted on **Azure SQL Database** in production.

---

## Deployment

The project is deployed on **Microsoft Azure**.

Infrastructure includes:

* **Azure App Service** – hosts the ASP.NET Core web application
* **Azure SQL Database** – stores application data

This setup allows the application to be accessed publicly while maintaining scalable cloud infrastructure.

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

