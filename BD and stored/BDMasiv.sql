


CREATE DATABASE BDMasiv
GO
USE BDMasiv
GO

CREATE TABLE Roulette
(
 RouletteId		INT IDENTITY(1,1)	NOT NULL	
,Names			VARCHAR(50)			NOT NULL
,AmountMax		DECIMAL(15,2)		NOT NULL
,State			VARCHAR(10)			NOT NULL
,Active			BIT					NOT NULL
,CONSTRAINT Pk_Roulette PRIMARY KEY(RouletteId)
)
GO
SELECT * FROM Roulette
GO

CREATE TABLE UserSession
(
 UserId			INT IDENTITY(1,1)	NOT NULL	
,Name			VARCHAR(50)			NOT NULL	
,Password		VARCHAR(50)			NOT NULL	
,State			VARCHAR(10)			NOT NULL
,Active			BIT					NOT NULL
,CONSTRAINT Pk_UserSession PRIMARY KEY(UserId)
)
GO
/*
INSERT INTO UserSession(Name,Password,State,Active)VALUES
('JLRU','123456','ACTIVO',1),
('CBRB','111111','ACTIVO',1),
('CAPJ','123456','ACTIVO',1),
('RESA','222222','ACTIVO',1)
*/
GO
SELECT * FROM UserSession
GO
CREATE TABLE Client
(
 ClientId			INT IDENTITY(1,1)	NOT NULL	
,Names				VARCHAR(50)			NOT NULL	
,PaternalSurname	VARCHAR(30)			NOT NULL
,MaternalSurname	VARCHAR(30)			NOT NULL
,Email				VARCHAR(100)		NOT NULL	
,Credit				DECIMAL(15,2)		NOT NULL
,State				VARCHAR(10)			NOT NULL
,Active				BIT					NOT NULL
,UserId				INT					NOT NULL
,CONSTRAINT Pk_Client PRIMARY KEY(ClientId)
)
GO
/*
INSERT INTO Client(Names,PaternalSurname,MaternalSurname,Email,Credit,State,Active,UserId)VALUES
('JORGE LUIS','REYES','URBANO','jlreyesu@gamil.com',5000,'ACTIVO',1,1),
('CAMILITA BELEN','REYES','BRAVO','cbrb@gamil.com',3000,'ACTIVO',1,2),
('CARLOS ALBERTO','PEREZ','JIMENEZ','capj@gamil.com',4000,'ACTIVO',1,3),
('RICARDO ENRRIQUE','SUAREZ','ALMOND','resa@gamil.com',6500,'ACTIVO',1,4)
*/
GO
--TRUNCATE TABLE Client
SELECT * FROM Client
GO
CREATE TABLE Bet
(
 BetId			INT	IDENTITY(1,1)		NOT NULL	
,Number			INT						NOT NULL	
,Color			CHAR(1)					NOT NULL
,BetAmount		NUMERIC					NOT NULL
,DateAmount		DATETIME				NOT NULL
,Winner			BIT						NOT NULL
,PaymentxN		DECIMAL(15,2)			NOT NULL
,PaymentxColor	DECIMAL(15,2)			NOT NULL
--,TotalxN		DECIMAL(15,2)			NOT NULL
--,TotalxColor	DECIMAL(15,2)			NOT NULL
--,Total		DECIMAL(15,2)		NOT NULL
,State			VARCHAR(10)				NOT NULL
,Active			BIT						NOT NULL
--,ClientId		INT						NOT NULL
,UserId			INT						NOT NULL
,RouletteId		INT						NOT NULL
,CONSTRAINT Pk_Bet PRIMARY KEY(BetId)
)
GO
SELECT * FROM Bet
