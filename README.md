### Simple Parking API ###

**1. How to run a Program:**

- Go into root folder of the project.
- Run API with command:
  - ```dotnet run -c Release -p .\CarAssignment\```
- Go onto API url: 
  - http://localhost:5008
- Enjoy the API and test endpoints.

**2. Adding Migrations:**

In order to add new migration use command:

```dotnet ef migrations add [YOUR_MIGRATION_NAME_HERE] -p .\CarAssignment.Infrastructure\ -s .\CarAssignment\```

You can also manually update database using following command:

```dotnet ef database update -p .\CarAssignment.Infrastructure\ -s .\CarAssignment\```

**3. Data Seeding:**

Seeding is done automatically right after migrations. 

These are added with code-first approach one the start of an API. 

It will detect whether new migrations are added and apply them accordingly.

**4. More information**

This code has been written in short period of time. There are things including exception handling, unit-tests etc. that needs more refining and adjustments. 