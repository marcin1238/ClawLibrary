-- USERS -------------------------------------------------------------------------------------------------------------------
INSERT INTO [dbo].[User] ([Key], [Email], [PasswordSalt],[PasswordHash],[FirstName],[LastName],[PhoneNumber],[CreatedDate],[Status], [CreatedBy])VALUES(NEWID(),'admin@test.com','1b568a7c-61cf-415c-b293-dcf40362192c','AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==','Admin','Admin','123456789',SYSDATETIMEOFFSET(),'Active','System');

-- ROLES -------------------------------------------------------------------------------------------------------------------
INSERT INTO [dbo].[Role]([Key],[Name],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[Status])VALUES(NEWID(),'Admin',SYSDATETIMEOFFSET(),'System',null,null,'Active');
INSERT INTO [dbo].[Role]([Key],[Name],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[Status])VALUES(NEWID(),'Regular',SYSDATETIMEOFFSET(),'System',null,null,'Active');

-- USER ROLE -------------------------------------------------------------------------------------------------------------------
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[Status])
     VALUES(NEWID(),1,1,SYSDATETIMEOFFSET(),'System',null,null,'Active');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[CreatedBy],[ModifiedDate],[ModifiedBy],[Status])
     VALUES(NEWID(),1,2,SYSDATETIMEOFFSET(),'System',null,null,'Active');