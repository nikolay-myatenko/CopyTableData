# CopyTableData

This is an application to demonstrate copying data from one database table to another.

The *Person* class implements a model for passing a row of data from the source table to the copy table (target table).

The *DatabaseService* class contains methods for interacting with the database.

The *DatabaseService.BulkInsert()* method implements an approach to bulk insertion of data items into a destination table.

The CopySettings class contains all the necessary custom settings for interacting with the database:
- Database connection strings
- SQL query to read data from the source table
- SQL query to check if the specified table exists
- SQL query to create the target table

as well as a custom method to match data from a record in the source table and the DTO.
