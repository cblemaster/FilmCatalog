USE master
GO

DECLARE @SQL nvarchar(1000);
IF EXISTS (SELECT 1 FROM sys.databases WHERE name = N'FilmCatalog')
BEGIN
    SET @SQL = N'USE FilmCatalog;

                 ALTER DATABASE FilmCatalog SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                 USE master;

                 DROP DATABASE FilmCatalog;';
    EXEC (@SQL);
END;

CREATE DATABASE FilmCatalog
GO

USE FilmCatalog
GO

CREATE TABLE Formats (
	FormatId			int IDENTITY(1,1)					NOT NULL,
	FormatName			varchar(255)						NOT NULL,
	CONSTRAINT PK_Formats PRIMARY KEY(FormatId),
	CONSTRAINT UC_FormatName UNIQUE(FormatName),
)
GO

INSERT INTO Formats(FormatName) VALUES('VHS');
INSERT INTO Formats(FormatName) VALUES('Betamax');
INSERT INTO Formats(FormatName) VALUES('Reel to reel');
INSERT INTO Formats(FormatName) VALUES('Laser Disc');
INSERT INTO Formats(FormatName) VALUES('DVD');
INSERT INTO Formats(FormatName) VALUES('Blu-ray');
INSERT INTO Formats(FormatName) VALUES('UHD Blu-ray');

CREATE TABLE Directors (
	DirectorId			int IDENTITY(1,1)					NOT NULL,
	[Name]				varchar(255)						NOT NULL,
	CONSTRAINT PK_Directors PRIMARY KEY(DirectorId),
)
GO

INSERT INTO Directors(Name) VALUES('George Lucas');
INSERT INTO Directors(Name) VALUES('Stanley Kubrick');
INSERT INTO Directors(Name) VALUES('Christopher Nolan');
INSERT INTO Directors(Name) VALUES('Paul Thomas Anderson');
INSERT INTO Directors(Name) VALUES('Peter Jackson');
INSERT INTO Directors(Name) VALUES('Steven Spielberg');
INSERT INTO Directors(Name) VALUES('David Fincher');
INSERT INTO Directors(Name) VALUES('Martin Scorsese');
INSERT INTO Directors(Name) VALUES('Mike Nichols');
INSERT INTO Directors(Name) VALUES('John Milnus');

CREATE TABLE Films (
	FilmId								int IDENTITY(1,1)					NOT NULL,
	Title								varchar(255)						NOT NULL,
	[Description]						varchar(255)						NULL,
	DirectorId							int									NULL,
	FormatId							int									NOT NULL,
	Quantity							int									NOT NULL,
	[Year]								varchar(4)							NULL,
	Studio								varchar(255)						NULL,
	IsFavorite							bit									NOT NULL,
	IsRareCollectibleAndOrValuable		bit									NOT NULL,
	CreateDate							datetime 							NOT NULL,
	UpdateDate							datetime							NULL,
	CONSTRAINT PK_Films PRIMARY KEY(FilmId),
	CONSTRAINT FK_Films_Formats FOREIGN KEY(FormatId) REFERENCES Formats(FormatId),
	CONSTRAINT FK_Films_Directors FOREIGN KEY(DirectorId) REFERENCES Directors(DirectorId),
)
GO

CREATE TABLE Actors (
	ActorId				int IDENTITY(1,1)					NOT NULL,
	[Name]				varchar(255)						NOT NULL,
	CONSTRAINT PK_Actors PRIMARY KEY(ActorId),
)
GO

INSERT INTO Actors(Name) VALUES('Brad Pitt');
INSERT INTO Actors(Name) VALUES('Bradley Cooper');
INSERT INTO Actors(Name) VALUES('George Clooney');
INSERT INTO Actors(Name) VALUES('Ryan Reynolds');
INSERT INTO Actors(Name) VALUES('Mark Hammil');
INSERT INTO Actors(Name) VALUES('Harrison Ford');
INSERT INTO Actors(Name) VALUES('Carrie Fisher');
INSERT INTO Actors(Name) VALUES('Alec Guiness');
INSERT INTO Actors(Name) VALUES('Sean Connery');
INSERT INTO Actors(Name) VALUES('Denzel Washington');
INSERT INTO Actors(Name) VALUES('Sylvester Stallone');
INSERT INTO Actors(Name) VALUES('Sandra Bullock');
INSERT INTO Actors(Name) VALUES('Mark Wahlburg');
INSERT INTO Actors(Name) VALUES('Ben Affleck');
INSERT INTO Actors(Name) VALUES('Margo Kidder');
INSERT INTO Actors(Name) VALUES('Robert Downey, Jr.');
INSERT INTO Actors(Name) VALUES('Arnold Schwarzenegger');
INSERT INTO Actors(Name) VALUES('Will Ferrell');
INSERT INTO Actors(Name) VALUES('Jennifer Lawrence');
INSERT INTO Actors(Name) VALUES('Bryan Cranston');
INSERT INTO Actors(Name) VALUES('John Goodman');
INSERT INTO Actors(Name) VALUES('Holly Hunter');
INSERT INTO Actors(Name) VALUES('Nicholas Cage');
INSERT INTO Actors(Name) VALUES('Matt Damon');
INSERT INTO Actors(Name) VALUES('Benicio Del Toro');
INSERT INTO Actors(Name) VALUES('Matthew McConaughey');
INSERT INTO Actors(Name) VALUES('Michael Caine');
INSERT INTO Actors(Name) VALUES('Jessica Chastain');
INSERT INTO Actors(Name) VALUES('Anne Hathaway');
INSERT INTO Actors(Name) VALUES('Michael J. Fox');
INSERT INTO Actors(Name) VALUES('Mel Gibson');
INSERT INTO Actors(Name) VALUES('Keanu Reeves');
INSERT INTO Actors(Name) VALUES('Jack Nicholson');
INSERT INTO Actors(Name) VALUES('Clint Eastwood');
INSERT INTO Actors(Name) VALUES('Glenn Close');
INSERT INTO Actors(Name) VALUES('Annette Benning');
INSERT INTO Actors(Name) VALUES('Pierce Brosnan');
INSERT INTO Actors(Name) VALUES('Danny DeVito');
INSERT INTO Actors(Name) VALUES('Sarah Jessica Parker');
INSERT INTO Actors(Name) VALUES('Natalie Portman');
INSERT INTO Actors(Name) VALUES('Carl Weathers');
INSERT INTO Actors(Name) VALUES('Dustin Hoffman');
INSERT INTO Actors(Name) VALUES('Edward Norton');
INSERT INTO Actors(Name) VALUES('Russell Crowe');
INSERT INTO Actors(Name) VALUES('Hilary Swank');
INSERT INTO Actors(Name) VALUES('Morgan Freeman');
INSERT INTO Actors(Name) VALUES('Bill Murray');
INSERT INTO Actors(Name) VALUES('Bruce Willis');
INSERT INTO Actors(Name) VALUES('Robert Deniro');
INSERT INTO Actors(Name) VALUES('Ethan Hawke');
INSERT INTO Actors(Name) VALUES('Elizabeth Shue');
INSERT INTO Actors(Name) VALUES('Pat Morita');
INSERT INTO Actors(Name) VALUES('Ralph Maccio');
INSERT INTO Actors(Name) VALUES('Sam Jones');
INSERT INTO Actors(Name) VALUES('Max Von Sydow');
INSERT INTO Actors(Name) VALUES('Heather Graham');
INSERT INTO Actors(Name) VALUES('Julianne Moore');
INSERT INTO Actors(Name) VALUES('Matt Damon');

CREATE TABLE Categories (
	CategoryId			int IDENTITY(1,1)					NOT NULL,
	CategoryName		varchar(255)						NOT NULL,
	CONSTRAINT PK_Categories PRIMARY KEY(CategoryId),
	CONSTRAINT UC_CategoryName UNIQUE(CategoryName),
)
GO

INSERT INTO Categories(CategoryName) VALUES('Science Fiction');
INSERT INTO Categories(CategoryName) VALUES('Fantasy');
INSERT INTO Categories(CategoryName) VALUES('Space opera');
INSERT INTO Categories(CategoryName) VALUES('Western');
INSERT INTO Categories(CategoryName) VALUES('Documentary');
INSERT INTO Categories(CategoryName) VALUES('Action/Thriller');
INSERT INTO Categories(CategoryName) VALUES('Mystery/Suspense');
INSERT INTO Categories(CategoryName) VALUES('Horror');
INSERT INTO Categories(CategoryName) VALUES('Crime/Heist');
INSERT INTO Categories(CategoryName) VALUES('Adventure');
INSERT INTO Categories(CategoryName) VALUES('Romance');
INSERT INTO Categories(CategoryName) VALUES('Historical fiction');
INSERT INTO Categories(CategoryName) VALUES('Biography');
INSERT INTO Categories(CategoryName) VALUES('Autobiography');
INSERT INTO Categories(CategoryName) VALUES('Sports');
INSERT INTO Categories(CategoryName) VALUES('Drama');
INSERT INTO Categories(CategoryName) VALUES('Foreign cinema');

CREATE TABLE FilmsCategories (
	FilmId				int									NOT NULL,
	CategoryId			int									NOT NULL,
	CONSTRAINT PK_FilmsCategories PRIMARY KEY CLUSTERED(FilmId, CategoryId),
	CONSTRAINT FK_FilmsCategories_Films FOREIGN KEY(FilmId) REFERENCES Films(FilmId),
	CONSTRAINT FK_FilmsCategories_Categories FOREIGN KEY(CategoryId) REFERENCES Categories(CategoryId),
)
GO

CREATE TABLE FilmsActors (
	FilmId				int									NOT NULL,
	ActorId				int									NOT NULL,
	CONSTRAINT PK_FilmsActors PRIMARY KEY CLUSTERED(FilmId, ActorId),
	CONSTRAINT FK_FilmsActors_Films FOREIGN KEY(FilmId) REFERENCES Films(FilmId),
	CONSTRAINT FK_FilmsActors_Actors FOREIGN KEY(ActorId) REFERENCES Actors(ActorId),
)
GO
