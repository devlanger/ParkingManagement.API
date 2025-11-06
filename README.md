# 🚗 Simple Parking API

A lightweight and extensible API for managing parking spots, cars, and assignments — built with **ASP.NET Core** and **Entity Framework Core**.

---

### TODO Checklist:
- [ ] Remove generic repository and implement dedicated ones (Breaks DDD and considered anti-pattern)
- [ ] Add Unit of Work pattern
- [ ] Make sure query calls to repository are projected and not all columns are selected if not required
- 

---

## 🧭 1. How to Run the Program

### ✅ 1.1 Prerequisites
Before you start, ensure you have the following installed:
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Entity Framework Core Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)
- (Optional) A database engine such as **SQL Server**, **PostgreSQL**, or **SQLite**, depending on your configuration.

### ▶️ 1.2 Steps to Run locally

1. Clone the repository and navigate to the root directory.
2. Run the API using:
   ```bash
   dotnet run -c Release -p .\CarAssignment\
   ```
3. Once running, open your browser and visit:
   ```
   http://localhost:5008/swagger/index.html
   ```
4. You’ll be greeted by the **Swagger UI**, where you can explore and test all available API endpoints interactively.

### ▶️ 1.3 Docker Support
You can also run it in docker using docker-compose file:
1. Go to tools/docker folder in terminal
2. run command:
   ```
   docker compose up -d
   ```
3. Wait for docker compose to spin up container instances
4. Go to url:
   ```
   http://localhost:8080/swagger/index.html
   ```
5. Try endpoints.

---

## 🗃️ 2. Database Migrations

This project uses **Entity Framework Core Code-First Migrations** for database schema management. 
These will pick up on application start.

### ➕ Add a New Migration
```bash
dotnet ef migrations add [YOUR_MIGRATION_NAME_HERE] -p .\CarAssignment.Infrastructure\ -s .\CarAssignment\
```

### 🔄 Update the Database
```bash
dotnet ef database update -p .\CarAssignment.Infrastructure\ -s .\CarAssignment\
```

---

## 🌱 3. Data Seeding

- The database is **automatically seeded** after migrations are applied.
- Seeding ensures the database is populated with default data (e.g., cars, parking spots, assignments).
- The seeding process runs **automatically on application startup**, detecting and applying any new migrations.

---

## ⚙️ 4. Configuration

You can configure settings such as the database connection string and API URLs in:
```
CarAssignment/appsettings.json
```

Environment-specific configurations can be added in:
```
appsettings.Development.json
appsettings.Production.json
```

---

## 🧪 5. Testing the API

You can test endpoints via **Swagger UI** or tools like **Postman** or **curl**.

Example request using curl:
```bash
curl -X GET http://localhost:5008/api/parking
```

---

## 🚧 6. Future Improvements

This project was developed in a short time frame and still needs refinement.  
Planned improvements include:

- ⚙️ Enhanced **exception handling**
- 🧪 **Unit and integration tests**
- 🔒 Improved **input validation**
- 🪵 Centralized **logging & monitoring**
- 🐳 **Docker support** for deployment