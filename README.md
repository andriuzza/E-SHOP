# E-SHOP
For completing the project, I used:
- ASP.NET MVC5/WebApi 2.0
- Database: MSSQL
- For mapping data from db to object - Entityframework 6.2.0 Code-First
- For client-side functionality - JQuery
- Unity container to handle Dependancy injetions
- The project has layered architecture.  Repository,Service, Presentation
-------------------------------------------------------------------------
The features I have accomplished:
- Search input for searching product's name (works on a client-side)
- Checkboxes for manufacturers, storages and operating systems filter (client-side also)
- Pagination (made without any plugin)
- After click of a product - shows detailed information about product

Back-end:
- Used IQueryable for not sending all data set, but parts of it, for not loading
all data. So that helps to get a better user experience (for showing that it works - products list's page size is 2)
- RESTful
- LINQ
------------------------------------------------------------------------
How to launch WebApi:
- Open web application
- Make as a startup project - repository project
- Go to package manager console and input - update-database
- Make as a startup project - E-SHOP and launch
- To get json data from products.json file and have it in db.
We need Postman for instance and call POST method {localhost}/api/products/post
- Finished
