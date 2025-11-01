# NetBackEnd – Tests & API Usage

This repository contains the **CurrencyConverter.Api** (.NET 8.0) and a minimal **NetBackEnd.UnitTests** project using **MSTest v2**, **Moq**, and **AutoFixture**.

## Prerequisites
- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- Any IDE (VS Code / Visual Studio / Rider)

## Restore & Build

```bash
dotnet restore
dotnet build
```

## Run the API

```bash
dotnet run --project NetBackEnd/NetBackEnd/CurrencyConverter.Api
```

By default Swagger is available at:
```
https://localhost:5001/swagger
```
(or `http://localhost:5000/swagger` if HTTP).

## Run Unit Tests

```bash
dotnet test NetBackEnd/NetBackEnd.UnitTests
```

> Note: The test project references the API project directly and targets the **same framework** (`net8.0`).

## HTTP File (VS Code / Rider)
Use the included `NetBackEnd.http` file for quick requests (requires the REST Client extension in VS Code).

## Postman
You can also import the same HTTP definitions into Postman by copying the requests from `NetBackEnd.http`.

---

## Test Coverage (minimal)
- **ConversionController.Convert** → verifies `200 OK` with mapped payload
- **CurrenciesController.GetCurrencies** → verifies `200 OK` and sorted list
- **RatesController.GetRates** → verifies `200 OK` and payload object

These tests are **unit** tests and do not hit the database or network.
