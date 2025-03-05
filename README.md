# Lost and Found Hub

## Project Overview
Lost and Found Hub is a web-based platform built using **ASP.NET Core MVC** and **SQL Server** that simplifies the process of reporting and recovering lost items. The system provides a centralized and user-friendly environment where:

- Users can **report lost items** with detailed descriptions.
- Finders can **report discovered items** to help reunite them with their owners.
- **Advanced search filters** allow users to find items by category, date, and location.
- An **automated matching system** connects lost and found items based on descriptions and images.
- **Secure communication channels** facilitate direct interaction between users.
- **Email and real-time notifications** alert users about potential matches and status updates.

## Features

### 1. User Management
- Secure **user registration and authentication**.
- Verified **user profiles** to establish trust.
- **Role-based access** for item reporters and finders.
- Profile management capabilities.

### 2. Item Reporting System
- **Submit detailed item descriptions** with optional image uploads.
- **Classify items by category** for better organization.
- **Track item location** for easier recovery.
- **Record date and time** of loss or discovery.

### 3. Search and Filters
- **Advanced filtering** (category, date, location).
- **Keyword-based search** for quick results.
- **Automated matching algorithm** for better accuracy.
- **Real-time search results** to improve discovery.

### 4. Matching Algorithm
- **Analyzes descriptions, categories, and images** to identify potential matches.
- **Notifies both the finder and the owner** when a match is detected.

### 5. Notification System
- **Automated email alerts** for potential matches.
- **Real-time notifications** within the platform.
- **Status updates** on reported items.
- **Communication alerts** between users.

## Technologies Used
- **Frontend:** C#, CSS, Bootstrap  
- **Backend:** ASP.NET Core MVC  
- **Database:** SQL Server  
- **Authentication:** ASP.NET Identity  
- **Notifications:** Email (SMTP), SignalR (for real-time updates)  

## Dependencies

### Install VS Code
Download and install [Visual Studio 2022](https://learn.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2022).

### Install Required Packages
Ensure you have the following packages installed:
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`
- `BCrypt.Net-Next`
- `Microsoft.AspNetCore.Authentication.Cookies`

### Start Apache and MySQL
Download and open [SQL Server Management Studio 20.2](https://learn.microsoft.com/en-us/ssms/download-sql-server-management-studio-ssms).

### Set Up MySQL Database
1. Add your `ConnectionString` in `appsettings.json`.
2. Copy the server name of your device from Microsoft SQL Server.

### Using Package Manager Console (PMC) in Visual Studio
1. Open **Package Manager Console**:  
   **Tools** → **NuGet Package Manager** → **Package Manager Console**
2. Add a new migration:
   ```powershell
   Add-Migration InitialCreate
   ```
3. Apply the migration to the database:
   ```powershell
   Update-Database
   ```
4. Rollback last migration (if needed):
   ```powershell
   Remove-Migration
   ```
   (Only works if the migration has not been applied yet.)

## Verification
- Open **SQL Server Management Studio (SSMS)**.
- Check the updated database schema.

This ensures that the database is correctly updated with the latest changes.

