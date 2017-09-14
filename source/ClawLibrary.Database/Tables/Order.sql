-- TABLE -------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[Order]
(
	[Id] BIGINT NOT NULL IDENTITY CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC),
	[Key] UNIQUEIDENTIFIER NOT NULL,
	
	[BookId] BIGINT NOT NULL,
	[DueDate] DATETIMEOFFSET NULL,

	[CreatedDate] DATETIMEOFFSET NOT NULL CONSTRAINT [DF_Order_CreatedDate] DEFAULT (SYSDATETIMEOFFSET()),
	[CreatedBy] NVARCHAR(256) NULL,
	[ModifiedDate] DATETIMEOFFSET NULL CONSTRAINT [DF_Order_ModifiedDate] DEFAULT (SYSDATETIMEOFFSET()),
	[ModifiedBy] NVARCHAR(256) NULL,

	[Status] NVARCHAR(128) NOT NULL
);
GO
-- FOREIGN KEYS -------------------------------------------------------------------------------------------------------------
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [FK_Order_Book] FOREIGN KEY([BookId]) REFERENCES [dbo].[Book] ([Id]);
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Book];

