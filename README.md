# CarRental

## Project Description

**CarRental** is a desktop application for car rental management, built with .NET 8 using WPF (Windows Presentation Foundation) and the MVVM architectural pattern. The application supports database operations via Entity Framework Core and provides essential features for both users and administrators.

---

## Features

- **User registration and authentication** (with roles: user and admin)
- **Car catalog browsing and filtering** by price, body type, year, and other parameters
- **Car booking** with booking history display
- **Admin panel**: add, edit, delete cars, view statistics, and manage bookings
- **Modern and user-friendly interface** with custom styles and themes
- **Car image support**
- **Repository and Unit of Work patterns** for data access

---

## Technology Stack

- **.NET 8.0 (WPF)**
- **C#**
- **MVVM (Model-View-ViewModel)**
- **Entity Framework Core 8 (Code First, SQL Server)**
- **XAML** (custom styles, templates, resources)
- **Repository, Unit of Work patterns**
- **Adaptive UI for different user roles**

---

## Architecture Overview

- **Model**: Defines main entities (User, Car, Booking) and their relationships, as well as the database context (`ApplicationDataContext`).
- **ViewModel**: Handles logic between UI and data, command processing, filtering, and navigation.
- **View**: XAML-based UI for various screens (main, bookings, statistics, car management, etc.).
- **Repositories**: Classes for working with database entities.
- **Unit of Work**: Manages transactions and coordinated work with multiple repositories.

---

## How to Run

1. Open the solution in Visual Studio 2022 or newer.
2. Check the database connection string in `ApplicationDataContext` (by default, SQL Server is used).
3. Apply database migrations if needed.
4. Build and run the project.

---


<img width="1125" height="707" alt="image" src="https://github.com/user-attachments/assets/b8be971b-34bc-499b-b16d-69eff51b0038" />

<img width="1129" height="674" alt="image" src="https://github.com/user-attachments/assets/8f70de6b-41a9-4d6b-9794-99ae97ae9471" />

<img width="1119" height="407" alt="image" src="https://github.com/user-attachments/assets/724630e2-8451-4c3e-a886-f65633395e4f" />

<img width="1127" height="707" alt="image" src="https://github.com/user-attachments/assets/45cab93b-67e2-4476-b9e2-540d905f401e" />

<img width="1109" height="654" alt="image" src="https://github.com/user-attachments/assets/3fc62235-2c65-4c33-83c9-09b27383f3ac" />




