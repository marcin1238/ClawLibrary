-- TABLE -------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[UserRole]
(
	[Id] BIGINT NOT NULL IDENTITY CONSTRAINT [PKUserRole] PRIMARY KEY CLUSTERED ([Id] ASC),
	[Key] UNIQUEIDENTIFIER NOT NULL,

	[UserId] BIGINT NOT NULL,
	[RoleId] BIGINT NOT NULL,
	
	[CreatedDate] DATETIMEOFFSET NOT NULL CONSTRAINT [DFUserRole_CreatedDate] DEFAULT (SYSDATETIMEOFFSET()),
	[CreatedBy] NVARCHAR(256) NULL,
	[ModifiedDate] DATETIMEOFFSET NULL CONSTRAINT [DFUserRole_ModifiedDate] DEFAULT (SYSDATETIMEOFFSET()),
	[ModifiedBy] NVARCHAR(256) NULL,

	[Status] NVARCHAR(128) NOT NULL
);
GO
-- FOREIGN KEYS -------------------------------------------------------------------------------------------------------------
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [FKUserRole_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[User] ([Id]);
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FKUserRole_UserId];
GO
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [FKUserRole_RoleId] FOREIGN KEY([RoleId]) REFERENCES [dbo].[Role] ([Id]);
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FKUserRole_RoleId];
GO
-- INDEXES -------------------------------------------------------------------------------------------------------------
CREATE UNIQUE NONCLUSTERED INDEX IXUserRole_Name  ON [dbo].[UserRole] ([UserId], [RoleId]);

