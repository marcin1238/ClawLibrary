-- TABLE -------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[File]
(
	[Id] BIGINT NOT NULL IDENTITY CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED ([Id] ASC),
	[Key] UNIQUEIDENTIFIER NOT NULL,
	[FileName] NVARCHAR(512) NOT NULL,

	[CreatedDate] DATETIMEOFFSET NOT NULL,
	[CreatedBy] NVARCHAR(256) NULL,
	[ModifiedDate] DATETIMEOFFSET NULL,
	[ModifiedBy] NVARCHAR(256) NULL,

	[Status] NVARCHAR(128) NOT NULL
);
GO
-- FOREIGN KEYS -------------------------------------------------------------------------------------------------------------

-- INDEXES -------------------------------------------------------------------------------------------------------------
CREATE UNIQUE NONCLUSTERED INDEX IX_File_Name  ON [dbo].[File] ([FileName]);

