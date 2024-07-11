# SQL Queries


USE SupplierDiligenceDb;

3. When receive the token of the Login. To execute the Supplier Endpoints add the token in the Lock in Swagger and type "Bearer [token]" in one line. Example:
```
Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdHVzZXIyIiwiZXhwIjoxNzIwNjIwNDkzfQ.AM6RgBQekPo7TewvpkBPZovaxItrbOldnw6B_B1WdD0
```

## Endpoints
### Auth Token and Password Hashing
|Enpoint| Work|Endpoint|
|---|---|---|
|Login|Yes|https://localhost:7062/api/Auth/login|


1. <b>EXECUTE</b> this query <b>TO HAVE A USER</b>
```SQL
INSERT INTO [User] (Id, Username, PasswordHash) 
VALUES ('5daa4203-9433-46a2-a5bb-d3155ebd542c','testuser2', '$2y$10$nqUq7pfBf8vIKRwsJrwDu.arfRCQBkV0ROn5uw36HNJ4hNl3/2Ghe');
```

2. Use the login with this values
```json
{
  "username": "testuser2",
  "password": "test@123"
}
```


### Supplier
|Enpoint| Work|endpoint|
|---|---|---|
|GetAll|Yes|https://localhost:7062/api/Supplier|
|GetById|Yes|https://localhost:7062/api/Supplier/{id}|
|Post|Yes|https://localhost:7062/api/Supplier|
|Put|Yes|https://localhost:7062/api/Supplier/{id}|
|Delete|Yes|https://localhost:7062/api/Supplier/{id}|
|Screening|Yes|https://localhost:7062/api/Supplier/screening/offshoreleaks/{entity_name}|

1. GetAll works! Just execute in the SwaggerAPI
2. GetById works! use a GUID in the <b>id</b> field showed in GetAll.
* Example of the field, <b>use the showed in GetAll</b>
```json
 "id": "6f6ba615-5598-462a-8ed9-fae91867d81d",
```

3. Post works! It needs email and phone Validation.

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "businessName": "string",
  "tradeName": "string",
  "taxId": "string",
  "phoneNumber": "string",
  "email": "string",
  "website": "string",
  "physicalAddress": "string",
  "country": "string",
  "annualBilling": 0,
  "lastEdited": "2024-07-09T16:09:27.112Z"
}
```
4. Put works! All the fields are optional.

```json
{
  "businessName": "string",
  "tradeName": "string",
  "taxId": "string",
  "phoneNumber": "string",
  "email": "string",
  "website": "string",
  "physicalAddress": "string",
  "country": "string",
  "annualBilling": 0
}
```
5. Delete works! use a GUID in the <b>id</b> field showed in GetAll.
* Example of the field, <b>use the showed in GetAll</b>
```json
 "id": "6f6ba615-5598-462a-8ed9-fae91867d81d",
```

6. Screening works! These are the Suppliers on High Risk
* Example <br>
`
https://localhost:7062/api/Supplier/screening/offshoreleaks/New Entity Limited
`
* Returns Good
```json
{
    "hits": 100,
    "rows": [
        {
            "DataFrom": "Offshore Leaks",
            "Entity": "New Entity Limited",
            "Jurisdiction": "British Virgin Islands",
            "LinkedTo": "British Virgin Islands"
        },
        {
            "DataFrom": "Offshore Leaks",
            "Entity": "TOPWELL ENTITY LIMITED",
            "Jurisdiction": "British Virgin Islands",
            "LinkedTo": "British Virgin Islands"
        },
        {
            "DataFrom": "Bahamas Leaks",
            "Entity": "SWIFT ENTITY LIMITED",
            "Jurisdiction": "Bahamas",
            "LinkedTo": ""
        },
        {
          ...
          ...
          ...
```
* This may also return due to Offshore Leaks blocking us from the website.
```json
{
    "error": "No se encontraron resultados para la entidad especificada."
}
```


### Resources
1. To hash a password
https://bcrypt.online/
2. GUID Generator
https://guidgenerator.com/






