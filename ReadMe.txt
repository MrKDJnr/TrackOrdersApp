Order Viewer Web Application

A lightweight ASP.NET Core Razor Pages web application that allows users to browse, filter, and manage e-commerce orders.

1. Features

- Order List
  - Displays Order ID, Customer Name, Status, Total (in currency), and Created Date
  - Responsive and mobile-friendly

- Filtering Panel
  - Filter orders by:
    - Date Range (From / To)
    - Order Status (Pending, Processing, Shipped, Cancelled, Paid)
    - Minimum and Maximum Order Total

- Order Details
  - Expandable rows that show line items including product name, quantity, and price

- Live Statistics
  - Shows total number of currently filtered orders and their combined total amount
  - Updates live without refreshing the full page

- Mark as Paid
  - Mark any order as "Paid" using a button
  - Updates UI instantly using AJAX, without a page reload
2. Prerequisites

- .NET 6 SDK or later
- MySQL Server or MariaDB
- (Optional) MySQL Workbench or another client for DB management

3. Getting Started

3.1 Clone the Repository

```bash
git clone https://github.com/MrKDJnr/TrackOrdersApp.git
cd order-viewer

3.2 Configure the Database Connection

In appsettings.json, set your MySQL connection string:

"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=OrderViewerDb;user=root;password=yourpassword"
}

3.3 Run Migrations and Seed the Database

dotnet ef database update

TrackOrdersApp/
│
├── Data/
│   └── AppDbContext.cs
│
├── Models/
│   ├── Order.cs
│   └── OrderItem.cs
│
├── Pages/
│   ├── Orders.cshtml
│   └── Orders.cshtml.cs
│
├── wwwroot/
│   └── css/site.css
│
└── appsettings.json
