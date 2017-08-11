﻿-- TABLE -------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[User]
(
	[Id] BIGINT NOT NULL IDENTITY CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC),
	[Key] UNIQUEIDENTIFIER NOT NULL,
	[Email] NVARCHAR(256) NOT NULL,

	[PasswordSalt] NVARCHAR(MAX) NULL,
	[PasswordHash] NVARCHAR(MAX) NULL,

	[FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50) NOT NULL,
	[PhoneNumber] NVARCHAR(20) NULL,

	[PasswordResetKey] NVARCHAR(72) NULL,
	[PasswordResetKeyCreatedDate] DATETIMEOFFSET NULL,

	[ImageFileId] BIGINT NULL,

	[CreatedDate] DATETIMEOFFSET NOT NULL CONSTRAINT [DF_User_CreatedDate] DEFAULT (SYSDATETIMEOFFSET()),
	[CreatedBy] NVARCHAR(256) NULL,
	[ModifiedDate] DATETIMEOFFSET NULL CONSTRAINT [DF_User_ModifiedDate] DEFAULT (SYSDATETIMEOFFSET()),
	[ModifiedBy] NVARCHAR(256) NULL,

	[Status] NVARCHAR(128) NOT NULL
);
GO
-- FOREIGN KEYS -------------------------------------------------------------------------------------------------------------
ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_File] FOREIGN KEY([ImageFileId]) REFERENCES [dbo].[File] ([Id]);
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_File];
GO
-- INDEXES -------------------------------------------------------------------------------------------------------------
CREATE UNIQUE NONCLUSTERED INDEX IX_User_Email  ON [dbo].[User] ([Email]);

