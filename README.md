# Dapper Demo

This project shows how to use Dapper with the Repository pattern and LINQ in a Blazor Server application.

For Blazor WebAssembly, you'll need to create an API layer.

## Databases

This demo uses three databases, each with a different type of primary key.

### Chinook:

The Primary Key of Customer table is an int and is not auto-generated.

Download the SQL script here:

https://github.com/lerocha/chinook-database/blob/master/ChinookDatabase/DataSources/Chinook_SqlServer.sql

Index.razor demonstrates accessing the Chinook Customer table.

### Northwind:

The Primary Key for the Customers table is a string.

Download the SQL script here:

https://github.com/microsoft/sql-server-samples/blob/master/samples/databases/northwind-pubs/instnwnd.sql

FetchData.razor demonstrates accessing the Northwind Customers table.

### BandBooker:

The Primary Key is an int and IS auto-generated

BandBooker.sql (included)

Counter.razor demonstrates accessing the BandBooker Instrument table.

