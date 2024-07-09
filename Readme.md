# SQL Queries


USE SupplierDiligenceDb;

1. Execute this query to have a user
```
INSERT INTO [User] (Id, Username, PasswordHash) 
VALUES ('5daa4203-9433-46a2-a5bb-d3155ebd542c','testuser2', '$2y$10$nqUq7pfBf8vIKRwsJrwDu.arfRCQBkV0ROn5uw36HNJ4hNl3/2Ghe');
```

2. Use the login with this values
```
{
  "username": "testuser2",
  "password": "test@123"
}
```

3. When receive the token of the Login. To execute the Supplier Endpoints add the token in the Lock in Swagger and type "Bearer [token]" in one line. Example:
```
Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdHVzZXIyIiwiZXhwIjoxNzIwNjIwNDkzfQ.AM6RgBQekPo7TewvpkBPZovaxItrbOldnw6B_B1WdD0
```

## Endpoints
### Auth
|Enpoint| Work|
|---|---|
|Login|Yes|

### Supplier
|Enpoint| Work|
|---|---|
|GetAll|Yes|
|GetById|Yes|
|Post|Yes|
|Put|Yes|
|Delete|Yes|
|Screening|Yes|

1. Post works! It needs email and phone Validation.

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
2. Put works! All the fields are optional.

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
3. Screening works! These are the Suppliers on High Risk
```
Id = new Guid("11A1C681-ACD4-446A-9272-61165DD04FC2"),
BusinessName = "Tech Innovators",
TaxId = "12345678901" ,
```
```
Id = new Guid("05C099D4-97A5-4DB3-85F9-F82CDE962C26"),
BusinessName = "Green Solutions", 
TaxId = "12345678901"
```

### Resources
1. To hash a password
https://bcrypt.online/
2. GUID Generator
https://guidgenerator.com/






