CREATE DATABASE MovieCatalog

GO

USE MovieCatalog

GO

CREATE TABLE [dbo].[Users] (
	[Id]		INT IDENTITY(1, 1) NOT NULL,
	[Username]	VARCHAR(40) NOT NULL,
	[Password]	VARCHAR(40) NOT NULL
	PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT C_UNIQUE_USERID UNIQUE(Id)
);

GO

CREATE TABLE [dbo].[Directors] (
	[Id]		INT IDENTITY(1, 1) NOT NULL,
	[Director]	VARCHAR(40) NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT C_UNIQUE_DIRID UNIQUE(Id)
);

GO

CREATE TABLE [dbo].[Genre] (
	[Id]		INT IDENTITY(1, 1) NOT NULL,
	[Genre]		VARCHAR(40) NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT C_UNIQUE_GENREID UNIQUE(Id)
);

GO

CREATE TABLE [dbo].[Movie] (
    [Id]			INT IDENTITY(1, 1) NOT NULL,
    [Title]       	VARCHAR (40) NOT NULL,
    [ReleaseYear] 	INT NULL,
    [GenreId]     	INT NULL,
	[DirectorId]	INT NULL,
    [Duration]		INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT C_UNIQUE_MOVIEID UNIQUE(Id),
	CONSTRAINT FK_Movie_Director FOREIGN KEY (DirectorId)
	REFERENCES [dbo].[Directors] (Id) ON UPDATE CASCADE,
	CONSTRAINT FK_Movie_Genre FOREIGN KEY (GenreId)
	REFERENCES [dbo].[Genre] (Id) ON UPDATE CASCADE
);

GO

INSERT INTO [dbo].[Users] ([Username], [Password])
VALUES	(N'Teszt', N'Teszt'),
		(N'User1', N'Password'),
		(N'User2', N'Jelszó');

INSERT INTO [dbo].[Directors] ([Director])
VALUES 	(N'Ridley Scott'),
		(N'Francis Ford Coppola'),
		(N'Steven Spielberg'),
		(N'Quentin Tarantino'),
		(N'J.J. Abrams'),
		(N'John Woo'),
		(N'James Cameron'),
		(N'Woody Allen'),
		(N'Martin Scorsese'),
		(N'Stanley Kubrick');

INSERT INTO [dbo].[Genre] ([Genre])
VALUES 	(N'Thriller'),
		(N'Romance'),
		(N'Action'),
		(N'Horror'),
		(N'Drama'),
		(N'Comedy'),
		(N'Animation'),
		(N'Fantasy'),
		(N'Sci-Fi'),
		(N'Crime'),
		(N'Historical');

INSERT INTO [dbo].[Movie] ([Title], [ReleaseYear], [GenreId], [DirectorId], [Duration])
VALUES	(N'Get Out', 2017, 1, 1, 140),
		(N'Dilwale Dulhanya Lejaengy', 1998, 2, 2, 200),
		(N'Fast and Furious', 2014, 1, 3, 140),
		(N'Perks of Being a Wall Flower', 2011, 5, 4, 135),
		(N'SpiderMan: Homecoming', 2017, 3, 5, 122),
		(N'Romeo and Juliet', 1993, 2, 6, 175),
		(N'Hotel Transalvinya', 2017, 6, 7, 103),
		(N'Scent of a Woman', 1996, 5, 8, 140),
		(N'Gangster', 2017, 4, 9, 130),
		(N'Happy Feet', 2006, 7, 10, 110),
		(N'Harry Potter VI - Halbblutprinz', 2009, 8, 1, 122),
		(N'Hercules',2014, 7, 2, 124),
		(N'I am Legend',2007, 3, 3, 128),
		(N'Identity', 2003, 4, 4, 127),
		(N'Kate & Leopold', 2001, 5, 5, 150),
		(N'Jurassic World',2005, 6, 6, 130),
		(N'King Arthur',2004, 7, 7, 120),
		(N'Laws of Attraction',2004, 8, 8, 110),
		(N'Lion King',1994, 9, 9, 134),
		(N'Matrix Revolutions',2003, 10, 10, 185),
		(N'Next', 2007, 11, 10, 132),
		(N'Oblivion', 2013, 1, 1, 111),
		(N'Password Swordfish', 2001, 2, 2, 150),
		(N'S.W.A.T',2003, 3, 3, 130),
		(N'Half Light', 2006, 4, 4, 120),
		(N'Gone Girl', 2014, 5, 5, 110),
		(N'Gladiator', 2000, 6, 6, 160),
		(N'From Hell', 2001, 7, 7, 180),
		(N'Frequency',2000, 8, 8, 120),
		(N'Gattaca',1997, 9, 9, 110),
		(N'Cowboys and Aliens',2011, 10, 10, 100),
		(N'Constantine',	2005, 11, 10, 90),
		(N'Cool Runnings', 1993, 1, 1, 120),
		(N'Avatar', 2009, 2, 2, 120),
		(N'Babel', 2011, 3, 3, 120);

GO