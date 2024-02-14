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

--INSERT INTO Formats(FormatName) VALUES('VHS');
--INSERT INTO Formats(FormatName) VALUES('Betamax');
--INSERT INTO Formats(FormatName) VALUES('Reel to reel tape');
--INSERT INTO Formats(FormatName) VALUES('Laser disc');
--INSERT INTO Formats(FormatName) VALUES('DVD');
--INSERT INTO Formats(FormatName) VALUES('Blu-ray');
--INSERT INTO Formats(FormatName) VALUES('UHD Blu-ray');

CREATE TABLE Directors (
	DirectorId			int IDENTITY(1,1)					NOT NULL,
	[Name]				varchar(255)						NOT NULL,
	CONSTRAINT PK_Directors PRIMARY KEY(DirectorId),
)
GO

--INSERT INTO Directors(Name) VALUES('George Lucas');
--INSERT INTO Directors(Name) VALUES('Stanley Kubrick');
--INSERT INTO Directors(Name) VALUES('Christopher Nolan');
--INSERT INTO Directors(Name) VALUES('Paul Thomas Anderson');
--INSERT INTO Directors(Name) VALUES('Peter Jackson');
--INSERT INTO Directors(Name) VALUES('Steven Spielberg');
--INSERT INTO Directors(Name) VALUES('David Fincher');
--INSERT INTO Directors(Name) VALUES('Martin Scorsese');
--INSERT INTO Directors(Name) VALUES('Mike Nichols');
--INSERT INTO Directors(Name) VALUES('John Milnus');

CREATE TABLE Films (
	FilmId								int IDENTITY(1,1)					NOT NULL,
	Title								varchar(255)						NOT NULL,
	[Description]						varchar(255)						NULL,
	DirectorId							int									NULL,
	FormatId							int									NOT NULL,
	Quantity							int									NOT NULL,
	[Year]								varchar(4)							NULL,
	Studio								varchar(255)						NULL,
	StarRating							int									NULL,
	IsFavorite							bit									NOT NULL,
	IsRareCollectibleAndOrValuable		bit									NOT NULL,
	CreateDate							datetime 							NOT NULL,
	UpdateDate							datetime							NULL,
	CONSTRAINT PK_Films PRIMARY KEY(FilmId),
	CONSTRAINT FK_Films_Formats FOREIGN KEY(FormatId) REFERENCES Formats(FormatId),
	CONSTRAINT FK_Films_Directors FOREIGN KEY(DirectorId) REFERENCES Directors(DirectorId),
	CONSTRAINT CHK_StarRating_BetweenZeroAndFiveInclsive CHECK (StarRating >= 0 AND StarRating <= 5),
	CONSTRAINT CHK_Year_IsFourCharacters CHECK (LEN([Year]) = 4),
)
GO

--INSERT INTO Films(Title,[Description],DirectorId,FormatId,Quantity,[Year],Studio,StarRating,IsFavorite,IsRareCollectibleAndOrValuable,CreateDate,UpdateDate) VALUES('Star Wars','Where it all began...',(SELECT d.DirectorId FROM Directors d WHERE d.Name = 'George Lucas'),(SELECT f.FormatId FROM Formats f WHERE f.FormatName = 'Blu-ray'),1,'1977',null,5,1,1,GETDATE(),null);
--INSERT INTO Films(Title,[Description],DirectorId,FormatId,Quantity,[Year],Studio,StarRating,IsFavorite,IsRareCollectibleAndOrValuable,CreateDate,UpdateDate) VALUES('Boogie Nights',null,(SELECT d.DirectorId FROM Directors d WHERE d.Name = 'Paul Thomas Anderson'),(SELECT f.FormatId FROM Formats f WHERE f.FormatName = 'Blu-ray'),1,'1997',null,5,1,0,GETDATE(),null);
--INSERT INTO Films(Title,[Description],DirectorId,FormatId,Quantity,[Year],Studio,StarRating,IsFavorite,IsRareCollectibleAndOrValuable,CreateDate,UpdateDate) VALUES('Flash Gordon',null,null,(SELECT f.FormatId FROM Formats f WHERE f.FormatName = 'Blu-ray'),1,'1980',null,4,0,0,GETDATE(),null);
--INSERT INTO Films(Title,[Description],DirectorId,FormatId,Quantity,[Year],Studio,StarRating,IsFavorite,IsRareCollectibleAndOrValuable,CreateDate,UpdateDate) VALUES('The Karate Kid',null,null,(SELECT f.FormatId FROM Formats f WHERE f.FormatName = 'DVD'),1,null,null,4,0,0,GETDATE(),null);
--INSERT INTO Films(Title,[Description],DirectorId,FormatId,Quantity,[Year],Studio,StarRating,IsFavorite,IsRareCollectibleAndOrValuable,CreateDate,UpdateDate) VALUES('Argo',null,null,(SELECT f.FormatId FROM Formats f WHERE f.FormatName = 'Blu-ray'),1,'2013',null,4,1,0,GETDATE(),null);
--INSERT INTO Films(Title,[Description],DirectorId,FormatId,Quantity,[Year],Studio,StarRating,IsFavorite,IsRareCollectibleAndOrValuable,CreateDate,UpdateDate) VALUES('Back to the Future',null,null,(SELECT f.FormatId FROM Formats f WHERE f.FormatName = 'Blu-ray'),1,null,null,4,0,0,GETDATE(),null);
--INSERT INTO Films(Title,[Description],DirectorId,FormatId,Quantity,[Year],Studio,StarRating,IsFavorite,IsRareCollectibleAndOrValuable,CreateDate,UpdateDate) VALUES('The Martian',null,null,(SELECT f.FormatId FROM Formats f WHERE f.FormatName = 'Blu-ray'),1,'2015',null,3,0,0,GETDATE(),null);
--INSERT INTO Films(Title,[Description],DirectorId,FormatId,Quantity,[Year],Studio,StarRating,IsFavorite,IsRareCollectibleAndOrValuable,CreateDate,UpdateDate) VALUES('Dune',null,null,(SELECT f.FormatId FROM Formats f WHERE f.FormatName = 'DVD'),1,null,null,5,1,0,GETDATE(),null);
--INSERT INTO Films(Title,[Description],DirectorId,FormatId,Quantity,[Year],Studio,StarRating,IsFavorite,IsRareCollectibleAndOrValuable,CreateDate,UpdateDate) VALUES('The Graduate',null,(SELECT d.DirectorId FROM Directors d WHERE d.Name = 'Mike Nichols'),(SELECT f.FormatId FROM Formats f WHERE f.FormatName = 'DVD'),1,'1967',null,4,0,0,GETDATE(),null);
--INSERT INTO Films(Title,[Description],DirectorId,FormatId,Quantity,[Year],Studio,StarRating,IsFavorite,IsRareCollectibleAndOrValuable,CreateDate,UpdateDate) VALUES('Braveheart',null,null,(SELECT f.FormatId FROM Formats f WHERE f.FormatName = 'DVD'),1,null,null,3,0,0,GETDATE(),null);
--INSERT INTO Films(Title,[Description],DirectorId,FormatId,Quantity,[Year],Studio,StarRating,IsFavorite,IsRareCollectibleAndOrValuable,CreateDate,UpdateDate) VALUES('The Matrix',null,null,(SELECT f.FormatId FROM Formats f WHERE f.FormatName = 'DVD'),1,'1999',null,3,0,0,GETDATE(),null);
--INSERT INTO Films(Title,[Description],DirectorId,FormatId,Quantity,[Year],Studio,StarRating,IsFavorite,IsRareCollectibleAndOrValuable,CreateDate,UpdateDate) VALUES('Kung-Fu Panda',null,null,(SELECT f.FormatId FROM Formats f WHERE f.FormatName = 'Blu-ray'),1,'2008',null,3,0,0,GETDATE(),null);
--INSERT INTO Films(Title,[Description],DirectorId,FormatId,Quantity,[Year],Studio,StarRating,IsFavorite,IsRareCollectibleAndOrValuable,CreateDate,UpdateDate) VALUES('2001: A Space Odyssey',null,(SELECT d.DirectorId FROM Directors d WHERE d.Name = 'Stanley Kubrick'),(SELECT f.FormatId FROM Formats f WHERE f.FormatName = 'Blu-ray'),1,'1968',null,5,1,1,GETDATE(),null);

CREATE TABLE Actors (
	ActorId				int IDENTITY(1,1)					NOT NULL,
	[Name]				varchar(255)						NOT NULL,
	CONSTRAINT PK_Actors PRIMARY KEY(ActorId),
)
GO

--INSERT INTO Actors(Name) VALUES('Brad Pitt');
--INSERT INTO Actors(Name) VALUES('Bradley Cooper');
--INSERT INTO Actors(Name) VALUES('George Clooney');
--INSERT INTO Actors(Name) VALUES('Ryan Reynolds');
--INSERT INTO Actors(Name) VALUES('Mark Hammil');
--INSERT INTO Actors(Name) VALUES('Harrison Ford');
--INSERT INTO Actors(Name) VALUES('Carrie Fisher');
--INSERT INTO Actors(Name) VALUES('Alec Guiness');
--INSERT INTO Actors(Name) VALUES('Sean Connery');
--INSERT INTO Actors(Name) VALUES('Denzel Washington');
--INSERT INTO Actors(Name) VALUES('Sylvester Stallone');
--INSERT INTO Actors(Name) VALUES('Sandra Bullock');
--INSERT INTO Actors(Name) VALUES('Mark Wahlburg');
--INSERT INTO Actors(Name) VALUES('Ben Affleck');
--INSERT INTO Actors(Name) VALUES('Margo Kidder');
--INSERT INTO Actors(Name) VALUES('Robert Downey, Jr.');
--INSERT INTO Actors(Name) VALUES('Arnold Schwarzenegger');
--INSERT INTO Actors(Name) VALUES('Will Ferrell');
--INSERT INTO Actors(Name) VALUES('Jennifer Lawrence');
--INSERT INTO Actors(Name) VALUES('Bryan Cranston');
--INSERT INTO Actors(Name) VALUES('John Goodman');
--INSERT INTO Actors(Name) VALUES('Holly Hunter');
--INSERT INTO Actors(Name) VALUES('Nicholas Cage');
--INSERT INTO Actors(Name) VALUES('Matt Damon');
--INSERT INTO Actors(Name) VALUES('Benicio Del Toro');
--INSERT INTO Actors(Name) VALUES('Matthew McConaughey');
--INSERT INTO Actors(Name) VALUES('Michael Caine');
--INSERT INTO Actors(Name) VALUES('Jessica Chastain');
--INSERT INTO Actors(Name) VALUES('Anne Hathaway');
--INSERT INTO Actors(Name) VALUES('Michael J. Fox');
--INSERT INTO Actors(Name) VALUES('Mel Gibson');
--INSERT INTO Actors(Name) VALUES('Keanu Reeves');
--INSERT INTO Actors(Name) VALUES('Jack Nicholson');
--INSERT INTO Actors(Name) VALUES('Clint Eastwood');
--INSERT INTO Actors(Name) VALUES('Glenn Close');
--INSERT INTO Actors(Name) VALUES('Annette Benning');
--INSERT INTO Actors(Name) VALUES('Pierce Brosnan');
--INSERT INTO Actors(Name) VALUES('Danny DeVito');
--INSERT INTO Actors(Name) VALUES('Sarah Jessica Parker');
--INSERT INTO Actors(Name) VALUES('Natalie Portman');
--INSERT INTO Actors(Name) VALUES('Carl Weathers');
--INSERT INTO Actors(Name) VALUES('Dustin Hoffman');
--INSERT INTO Actors(Name) VALUES('Edward Norton');
--INSERT INTO Actors(Name) VALUES('Russell Crowe');
--INSERT INTO Actors(Name) VALUES('Hilary Swank');
--INSERT INTO Actors(Name) VALUES('Morgan Freeman');
--INSERT INTO Actors(Name) VALUES('Bill Murray');
--INSERT INTO Actors(Name) VALUES('Bruce Willis');
--INSERT INTO Actors(Name) VALUES('Robert Deniro');
--INSERT INTO Actors(Name) VALUES('Ethan Hawke');
--INSERT INTO Actors(Name) VALUES('Elizabeth Shue');
--INSERT INTO Actors(Name) VALUES('Pat Morita');
--INSERT INTO Actors(Name) VALUES('Ralph Maccio');
--INSERT INTO Actors(Name) VALUES('Sam Jones');
--INSERT INTO Actors(Name) VALUES('Max Von Sydow');
--INSERT INTO Actors(Name) VALUES('Heather Graham');
--INSERT INTO Actors(Name) VALUES('Julianne Moore');

CREATE TABLE Categories (
	CategoryId			int IDENTITY(1,1)					NOT NULL,
	CategoryName		varchar(255)						NOT NULL,
	CONSTRAINT PK_Categories PRIMARY KEY(CategoryId),
	CONSTRAINT UC_CategoryName UNIQUE(CategoryName),
)
GO

--INSERT INTO Categories(CategoryName) VALUES('Science Fiction');
--INSERT INTO Categories(CategoryName) VALUES('Fantasy');
--INSERT INTO Categories(CategoryName) VALUES('Space opera');
--INSERT INTO Categories(CategoryName) VALUES('Western');
--INSERT INTO Categories(CategoryName) VALUES('Documentary');
--INSERT INTO Categories(CategoryName) VALUES('Action/Thriller');
--INSERT INTO Categories(CategoryName) VALUES('Mystery/Suspense');
--INSERT INTO Categories(CategoryName) VALUES('Horror');
--INSERT INTO Categories(CategoryName) VALUES('Crime/Heist');
--INSERT INTO Categories(CategoryName) VALUES('Adventure');
--INSERT INTO Categories(CategoryName) VALUES('Romance');
--INSERT INTO Categories(CategoryName) VALUES('Historical fiction');
--INSERT INTO Categories(CategoryName) VALUES('Biography');
--INSERT INTO Categories(CategoryName) VALUES('Autobiography');
--INSERT INTO Categories(CategoryName) VALUES('Sports');
--INSERT INTO Categories(CategoryName) VALUES('Drama');
--INSERT INTO Categories(CategoryName) VALUES('Foreign cinema');
--INSERT INTO Categories(CategoryName) VALUES('Comedy');
--INSERT INTO Categories(CategoryName) VALUES('Animated');

CREATE TABLE FilmsCategories (
	FilmId				int									NOT NULL,
	CategoryId			int									NOT NULL,
	CONSTRAINT PK_FilmsCategories PRIMARY KEY CLUSTERED(FilmId, CategoryId),
	CONSTRAINT FK_FilmsCategories_Films FOREIGN KEY(FilmId) REFERENCES Films(FilmId) ON DELETE CASCADE,
	CONSTRAINT FK_FilmsCategories_Categories FOREIGN KEY(CategoryId) REFERENCES Categories(CategoryId) ON DELETE CASCADE,
)
GO

--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Star Wars'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Space opera'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Boogie Nights'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Drama'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Flash Gordon'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Space opera'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'The Karate Kid'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Drama'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'The Karate Kid'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Comedy'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Argo'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Drama'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Argo'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Action/Thriller'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Back to the Future'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Comedy'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Back to the Future'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Adventure'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'The Martian'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Science Fiction'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'The Graduate'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Drama'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'The Graduate'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Comedy'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Braveheart'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Historical fiction'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'The Matrix'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Science Fiction'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Kung-Fu Panda'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Comedy'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Kung-Fu Panda'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Animated'));
--INSERT INTO FilmsCategories(FilmId,CategoryId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = '2001: A Space Odyssey'),(SELECT c.CategoryId FROM Categories c WHERE c.CategoryName = 'Science Fiction'));

CREATE TABLE FilmsActors (
	FilmId				int									NOT NULL,
	ActorId				int									NOT NULL,
	CONSTRAINT PK_FilmsActors PRIMARY KEY CLUSTERED(FilmId, ActorId),
	CONSTRAINT FK_FilmsActors_Films FOREIGN KEY(FilmId) REFERENCES Films(FilmId) ON DELETE CASCADE,
	CONSTRAINT FK_FilmsActors_Actors FOREIGN KEY(ActorId) REFERENCES Actors(ActorId) ON DELETE CASCADE,
)
GO

--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Star Wars'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Mark Hammil'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Star Wars'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Harrison Ford'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Star Wars'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Carrie Fisher'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Star Wars'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Alec Guiness'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Boogie Nights'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Mark Wahlburg'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Boogie Nights'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Julianne Moore'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Flash Gordon'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Sam Jones'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Flash Gordon'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Max Von Sydow'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'The Karate Kid'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Pat Morita'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'The Karate Kid'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Elizabeth Shue'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'The Karate Kid'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Ralph Maccio'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Argo'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Ben Affleck'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Argo'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Bryan Cranston'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Argo'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'John Goodman'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Back to the Future'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Michael J. Fox'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'The Martian'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Matt Damon'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'The Graduate'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Dustin Hoffman'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'Braveheart'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Mel Gibson'));
--INSERT INTO FilmsActors(FilmId,ActorId) VALUES((SELECT f.FilmId FROM Films f WHERE f.Title = 'The Matrix'),(SELECT a.ActorId FROM Actors a WHERE a.Name = 'Keanu Reeves'));
