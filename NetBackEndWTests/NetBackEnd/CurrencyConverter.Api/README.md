
# Currency Converter API (.NET 8, SQL Server) — Split, Clean, Human Style

- SQL Server (EF Core SqlServer)
- AutoMapper + DI
- Plain HttpClient
- Each interface/implementation in its own file
- Each DTO in its own file

## Run
1. Update `appsettings.json` with your SQL Server connection string.
2. Create schema with EF:
   ```powershell
   dotnet tool install --global dotnet-ef
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
3. F5 to run; Swagger at `/swagger`.

## Endpoints
- POST `/api/conversion`
- GET `/api/conversion/history`
- GET `/api/currencies`
- GET `/api/rates/{base}`
