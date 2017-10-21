-- TABLE -------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Author]
(
	[Id] BIGINT NOT NULL IDENTITY CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED ([Id] ASC),
	[Key] UNIQUEIDENTIFIER NOT NULL,

	[FirstName] NVARCHAR(256) NOT NULL,
	[LastName] NVARCHAR(256) NOT NULL,

	[Description] NVARCHAR(2000) NULL,
	
	[ImageFileId] BIGINT NULL,

	[CreatedDate] DATETIMEOFFSET NOT NULL CONSTRAINT [DF_Author_CreatedDate] DEFAULT (SYSDATETIMEOFFSET()),
	[CreatedBy] NVARCHAR(256) NULL,
	[ModifiedDate] DATETIMEOFFSET NULL CONSTRAINT [DF_Author_ModifiedDate] DEFAULT (SYSDATETIMEOFFSET()),
	[ModifiedBy] NVARCHAR(256) NULL,

	[Status] NVARCHAR(128) NOT NULL
);
GO
-- FOREIGN KEYS -------------------------------------------------------------------------------------------------------------
ALTER TABLE [dbo].[Author] ADD CONSTRAINT [FK_Author_File] FOREIGN KEY([ImageFileId]) REFERENCES [dbo].[File] ([Id]);
GO
ALTER TABLE [dbo].[Author] CHECK CONSTRAINT [FK_Author_File];
GO
-- INDEXES -------------------------------------------------------------------------------------------------------------
CREATE UNIQUE NONCLUSTERED INDEX IX_Author_FirstName_LastName  ON [dbo].[Author] ([FirstName], [LastName]);
GO

