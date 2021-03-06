﻿CREATE TABLE [dbo].[Artists](
	[ArtistId] INT IDENTITY(1,1),
 	[Name] NVARCHAR(50) COLLATE Latin1_General_BIN2
		ENCRYPTED WITH (ENCRYPTION_TYPE = DETERMINISTIC,
 		ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256',   
 		COLUMN_ENCRYPTION_KEY = AE_CEK) NOT NULL
PRIMARY KEY CLUSTERED ([ArtistId] ASC) ON [PRIMARY] );
GO
CREATE PROCEDURE Insert_AE (
	@Name NVARCHAR(50)
)  
AS   
INSERT INTO dbo.[Artists]  
   ([Name])  
VALUES (@Name);  
GO

EXEC Insert_AE @Name = N'Gregory'