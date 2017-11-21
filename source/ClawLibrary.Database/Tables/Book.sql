-- TABLE -------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Book]
(
	[Id] BIGINT NOT NULL IDENTITY CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED ([Id] ASC),
	[Key] UNIQUEIDENTIFIER NOT NULL,
	[Title] NVARCHAR(256) NOT NULL,

	[Publisher] NVARCHAR(256) NULL,
	[Language] NVARCHAR(2) NOT NULL,

	[ISBN] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[Paperback] BIGINT NOT NULL,
	[PublishDate] DATE NOT NULL,
	[AuthorId] BIGINT NOT NULL,
	[ImageFileId] BIGINT NULL,
	[CategoryId] BIGINT NOT NULL,

	[CreatedDate] DATETIMEOFFSET NOT NULL,
	[CreatedBy] NVARCHAR(256) NULL,
	[ModifiedDate] DATETIMEOFFSET NULL,
	[ModifiedBy] NVARCHAR(256) NULL,

	[Status] NVARCHAR(128) NOT NULL
);
GO
-- FOREIGN KEYS -------------------------------------------------------------------------------------------------------------
ALTER TABLE [dbo].[Book] ADD CONSTRAINT [FK_Book_File] FOREIGN KEY([ImageFileId]) REFERENCES [dbo].[File] ([Id]);
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_File];
GO
ALTER TABLE [dbo].[Book] ADD CONSTRAINT [FK_Book_Author] FOREIGN KEY([AuthorId]) REFERENCES [dbo].[Author] ([Id]);
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Author];
GO
ALTER TABLE [dbo].[Book] ADD CONSTRAINT [FK_Book_Category] FOREIGN KEY([CategoryId]) REFERENCES [dbo].[Category] ([Id]);
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Category];
GO
-- INDEXES -------------------------------------------------------------------------------------------------------------
CREATE UNIQUE NONCLUSTERED INDEX IX_Book_Title  ON [dbo].[Book] ([Title]);
GO
CREATE UNIQUE NONCLUSTERED INDEX IX_Book_ISBN  ON [dbo].[Book] ([ISBN]);

