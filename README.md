# PaymentApi (Starter)

A minimal **Payment Processing API** (C# / ASP.NET Core) starter project to demonstrate:
- REST endpoints for creating payments and receiving webhooks
- SQLite storage via Entity Framework Core
- Swagger (OpenAPI) documentation
- Simple unit test example (xUnit)
- Dockerfile and docker-compose for local demo

## Quick local run (requires .NET SDK 6/7/8)
1. `dotnet restore`
2. `dotnet run --project src/PaymentApi` 
3. Open Swagger UI: `https://localhost:5001/swagger/index.html`

## Docker (recommended for easiest demo)
```bash
docker-compose up --build
# then open http://localhost:5000/swagger/index.html
