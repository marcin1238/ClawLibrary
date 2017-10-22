CREATE TABLE [dbo].[Role]
(
	[Id] BIGINT NOT NULL IDENTITY CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC),
	[Key] UNIQUEIDENTIFIER NOT NULL,
	[Name] NVARCHAR(256) NOT NULL,

	[CreatedDate] DATETIMEOFFSET NOT NULL,
	[CreatedBy] NVARCHAR(256) NULL,
	[ModifiedDate] DATETIMEOFFSET NULL,
	[ModifiedBy] NVARCHAR(256) NULL,

	[Status] NVARCHAR(128) NOT NULL
);
GO
-- FOREIGN KEYS -------------------------------------------------------------------------------------------------------------

-- INDEXES -------------------------------------------------------------------------------------------------------------
CREATE UNIQUE NONCLUSTERED INDEX IX_Role_Name  ON [dbo].[Role] ([Name]);


