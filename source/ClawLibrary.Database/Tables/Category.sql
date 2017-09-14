-- TABLE -------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Category]
(
	[Id] BIGINT NOT NULL IDENTITY CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Id] ASC),
	[Key] UNIQUEIDENTIFIER NOT NULL,
	
	[Name] NVARCHAR(256) NOT NULL,

	[CreatedDate] DATETIMEOFFSET NOT NULL CONSTRAINT [DF_Category_CreatedDate] DEFAULT (SYSDATETIMEOFFSET()),
	[CreatedBy] NVARCHAR(256) NULL,
	[ModifiedDate] DATETIMEOFFSET NULL CONSTRAINT [DF_Category_ModifiedDate] DEFAULT (SYSDATETIMEOFFSET()),
	[ModifiedBy] NVARCHAR(256) NULL,

	[Status] NVARCHAR(128) NOT NULL
);
GO
-- INDEXES -------------------------------------------------------------------------------------------------------------
CREATE UNIQUE NONCLUSTERED INDEX IX_Book_Title  ON [dbo].[Category] ([Name]);

