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


### Resources
1. To hash a password
https://bcrypt.online/
2. GUID Generator
https://guidgenerator.com/






