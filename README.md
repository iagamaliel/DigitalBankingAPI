# Digital Banking API

API REST desarrollada en **.NET 9** para operaciones bancarias básicas como:

* Consulta de cuentas
* Depósitos
* Transferencias entre cuentas
* Cálculo de interés diario
* Consulta de historial de intereses

El proyecto fue implementado siguiendo principios de **Clean Architecture** y **CQRS**, utilizando **MediatR** para desacoplar comandos y consultas, **FluentValidation** para validaciones y **SQL Server** con **Stored Procedures** para la persistencia.

---

# Requisitos Previos

Antes de ejecutar el proyecto asegúrese de tener instalado:

* **.NET 9 SDK**
* **SQL Server 2019 o superior**
* **SQL Server Management Studio (SSMS)** o Azure Data Studio
* **Visual Studio 2022**

---

# Configuración de Base de Datos

Nombre de la base de datos:

```
DigitalBankingDb
```

Los scripts SQL se encuentran en la carpeta:

```
/Database
```

Ejecutar los scripts en el siguiente orden:

```
01-create-tables.sql
02-create-stored-procedures.sql
03-seed-data.sql
```

Primero crear la base de datos:

```sql
CREATE DATABASE DigitalBankingDb;
GO
```

Luego seleccionar la base de datos y ejecutar los scripts.

---

# Connection String de Ejemplo

Configurar en **appsettings.json**

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=DigitalBankingDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Ejemplo con autenticación SQL:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=DigitalBankingDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True"
}
```

---

# Ejecutar el Proyecto

Restaurar dependencias:

```
dotnet restore
```

Ejecutar la aplicación:

```
dotnet run
```

La API iniciará en:

```
https://localhost:7050
```

---

# Documentación Swagger

Swagger está disponible en:

```
https://localhost:7050/swagger
```

Desde allí se pueden probar todos los endpoints.

---

# Ejemplos de Requests y Responses

A continuación se muestran ejemplos de cómo consumir los endpoints de la API utilizando **curl**.

---

# Obtener información de una cuenta

Endpoint

```
GET /api/v1/accounts/{accountId}
```

Ejemplo

```
curl -X GET \
'https://localhost:7050/api/v1/accounts/ACC1001' \
-H 'accept: */*'
```

Response

```json
{
  "succeeded": true,
  "message": null,
  "data": {
    "accountId": "ACC1001",
    "balance": 1510,
    "lastTransactions": [
      {
        "type": "DEPOSIT",
        "amount": 10,
        "date": "2026-03-12T11:27:41.723",
        "description": "Account deposit"
      },
      {
        "type": "DEPOSIT",
        "amount": 1500,
        "date": "2026-03-12T11:27:01.063",
        "description": "Initial deposit"
      }
    ],
    "totalInterest": 0
  },
  "statusCode": 200
}
```

---

# Depositar dinero

Endpoint

```
POST /api/v1/accounts/{accountId}/deposit
```

Ejemplo

```
curl -X POST \
'https://localhost:7050/api/v1/accounts/ACC1001/deposit' \
-H 'accept: */*' \
-H 'Content-Type: application/json' \
-d '{
  "amount": 10
}'
```

Response

```json
{
  "succeeded": true,
  "message": null,
  "data": {
    "accountId": "ACC1001",
    "customerName": "John Carter",
    "balance": 1510
  },
  "statusCode": 200
}
```

---

# Transferencia entre cuentas

Endpoint

```
POST /api/v1/accounts
```

Ejemplo

```
curl -X POST \
'https://localhost:7050/api/v1/accounts' \
-H 'accept: */*' \
-H 'Content-Type: application/json' \
-d '{
  "fromAccountId": "ACC1001",
  "toAccountId": "ACC1002",
  "amount": 10
}'
```

Response

```json
{
  "succeeded": true,
  "message": null,
  "data": {
    "fromAccount": "ACC1001",
    "toAccount": "ACC1002",
    "amount": 10,
    "message": "Transfer completed successfully"
  },
  "statusCode": 200
}
```

---

# Calcular interés diario

Endpoint

```
POST /api/v1/interest/calculate
```

Ejemplo

```
curl -X POST \
'https://localhost:7050/api/v1/interest/calculate' \
-H 'accept: */*' \
-d ''
```

Response

```json
{
  "succeeded": true,
  "message": null,
  "data": "Daily interest calculated successfully",
  "statusCode": 200
}
```

---

# Consultar historial de intereses

Endpoint

```
GET /api/v1/accounts/{accountId}/interest-history
```

Ejemplo

```
curl -X GET \
'https://localhost:7050/api/v1/accounts/ACC1001/interest-history' \
-H 'accept: */*'
```

Response

```json
{
  "succeeded": true,
  "message": null,
  "data": [
    {
      "id": 1,
      "interestRate": 0.05,
      "calculatedInterest": 0.76,
      "calculationDate": "2026-03-12T11:30:50.02"
    }
  ],
  "statusCode": 200
}
```

---

# Estructura del Proyecto

```
src
 ├ DigitalBanking.API
 ├ DigitalBanking.Application
 ├ DigitalBanking.Domain
 ├ DigitalBanking.Infrastructure
Database
```

---

# Validaciones

Las validaciones se implementan utilizando **FluentValidation** mediante un **Pipeline Behavior de MediatR**.

Ejemplos:

* AccountId solo permite caracteres alfanuméricos
* Amount debe ser mayor a cero
* Transferencias no pueden realizarse a la misma cuenta

---

# Manejo de Errores

La API implementa un **middleware global de excepciones** para capturar errores no controlados y retornar respuestas JSON estandarizadas.

Ejemplo de error:

```json
{
  "succeeded": false,
  "message": "An unexpected error occurred.",
  "data": null
}
```

---

# Scripts SQL incluidos

```
/Database
01-create-tables.sql
02-create-stored-procedures.sql
03-seed-data.sql
```

---

# Colección de Postman

Se incluye colección para pruebas en:

```
/Postman/DigitalBankingAPI.postman_collection.json
```

Importar en **Postman** para probar todos los endpoints rápidamente.

---

# Autor

Proyecto desarrollado como parte de una **prueba técnica backend**, demostrando:

* Clean Architecture
* CQRS
* MediatR
* FluentValidation
* SQL Server con Stored Procedures
* Buenas prácticas en APIs REST
