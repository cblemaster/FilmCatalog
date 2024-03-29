# FilmCatalog

## An application for keeping track of a collection of films

### Built with:
- .NET 8 / C# 12
- SQL Server database
- API: ASP.NET Core, minimal API, Entity Framework Core 8
- UI: .NET MAUI (targeting Windows desktop only), CommunityToolkit.Mvvm and CommunityToolkit.Maui nuget packages
- Programming techniques:
	- Null object pattern
	- Immutable objects and collections
	- Expression bodied members
	- Pattern matching
	- Nullable reference types
	- Asynchronous programming
	- Dependency injection
	- MVVM pattern

## Features:
- Actors: Create, Rename, Delete*, View, View all
- Categories: Create, Delete*, View, View all
- Directors: Create, Rename, Delete*, View, View all
- Films: Create, Update, Delete, View, View all, View favorites, View rare or collectible, View five-star rated, assign actors, assign categories
- Films: View films grouped by actor, director, category, or format
- Formats: Create, Delete*, View, View all
/* Can delete only if not associated with any film(s)

## Business rules:
## UI conventions:
- TBD

## Instructions for running the application:
- Note that SQL server and Visual Studio are required for running the application
- Clone or download the repo
- Browse to \FilmCatalog\FilmCatalog.API\Database
- Run the database script 'FilmCatalog-Create-DB-And-Initialize-Data-Script.sql'
	- This script will drop (if exists) and re-create the database and tables
	- Note that there is optional sample data that can be inserted into the database as well; it can be found throughout the database script and it is commented out
- Browse to \FilmCatalog\FilmCatalog.API\appsettings.json
	- There is a database connection string in this config file that needs to point to your database
- Run the solution in Visual Studio

## Improvement opportunities:
- TBD