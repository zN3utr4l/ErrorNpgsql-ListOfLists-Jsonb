- Setup PostgreSQL at 127.0.0.1:5432
- Run Update-Database
- Open your DB (Pg-Admin) and insert manually the json in the table my_entity **
- Run App
- Navigate to https://localhost:7012/error-jsonb


**JSON :**
```
[
   [
      {
         "Key":"Test",
         "Value":"0"
      },
      {
         "Key":"Test2",
         "Value":"0"
      }
   ],
   [
      {
         "Key":"Test",
         "Value":"650"
      },
      {
         "Key":"Test2",
         "Value":"150"
      }
   ]
]
```
