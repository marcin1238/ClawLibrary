-- USERS -------------------------------------------------------------------------------------------------------------------
INSERT INTO [dbo].[User] ([Key], [Email], [PasswordSalt],[PasswordHash],[FirstName],[LastName],[PhoneNumber],[CreatedDate],[Status], [CreatedBy])VALUES(NEWID(),'mohammed@test.com','1b568a7c-61cf-415c-b293-dcf40362192c','AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==','Mohammed','Test','123456789',SYSDATETIMEOFFSET(),'Active', 'System');
INSERT INTO [dbo].[User] ([Key], [Email], [PasswordSalt],[PasswordHash],[FirstName],[LastName],[PhoneNumber],[CreatedDate],[Status], [CreatedBy])VALUES(NEWID(),'olga@test.com','1b568a7c-61cf-415c-b293-dcf40362192c','AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==','Olga ','Test','223456789',SYSDATETIMEOFFSET(),'Active', 'System');
INSERT INTO [dbo].[User] ([Key], [Email], [PasswordSalt],[PasswordHash],[FirstName],[LastName],[PhoneNumber],[CreatedDate],[Status], [CreatedBy])VALUES(NEWID(),'maciej@test.com','1b568a7c-61cf-415c-b293-dcf40362192c','AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==','Maciej','Test','323456789',SYSDATETIMEOFFSET(),'Active', 'System');
INSERT INTO [dbo].[User] ([Key], [Email], [PasswordSalt],[PasswordHash],[FirstName],[LastName],[PhoneNumber],[CreatedDate],[Status], [CreatedBy])VALUES(NEWID(),'bartus@test.pl','1b568a7c-61cf-415c-b293-dcf40362192c','AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==','Bartosz ','Test','423456789',SYSDATETIMEOFFSET(),'Active', 'System');
INSERT INTO [dbo].[User] ([Key], [Email], [PasswordSalt],[PasswordHash],[FirstName],[LastName],[PhoneNumber],[CreatedDate],[Status], [CreatedBy])VALUES(NEWID(),'sara@test.com','1b568a7c-61cf-415c-b293-dcf40362192c','AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==','Sara','Test','523456789',SYSDATETIMEOFFSET(),'Active', 'System');
INSERT INTO [dbo].[User] ([Key], [Email], [PasswordSalt],[PasswordHash],[FirstName],[LastName],[PhoneNumber],[CreatedDate],[Status], [CreatedBy])VALUES(NEWID(),'karolina@test.pl','1b568a7c-61cf-415c-b293-dcf40362192c','AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==','Karolina','Test','623456789',SYSDATETIMEOFFSET(),'Active', 'System');
INSERT INTO [dbo].[User] ([Key], [Email], [PasswordSalt],[PasswordHash],[FirstName],[LastName],[PhoneNumber],[CreatedDate],[Status], [CreatedBy])VALUES(NEWID(),'katarzyna@test.eu','1b568a7c-61cf-415c-b293-dcf40362192c','AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==','Katarzyna','Test','723456789',SYSDATETIMEOFFSET(),'Active', 'System');
INSERT INTO [dbo].[User] ([Key], [Email], [PasswordSalt],[PasswordHash],[FirstName],[LastName],[PhoneNumber],[CreatedDate],[Status], [CreatedBy])VALUES(NEWID(),'martyna@test.com','1b568a7c-61cf-415c-b293-dcf40362192c','AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==','Martyna ','Test','823456789',SYSDATETIMEOFFSET(),'Active', 'System');
INSERT INTO [dbo].[User] ([Key], [Email], [PasswordSalt],[PasswordHash],[FirstName],[LastName],[PhoneNumber],[CreatedDate],[Status], [CreatedBy])VALUES(NEWID(),'bogna@test.pl','1b568a7c-61cf-415c-b293-dcf40362192c','AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==','Bogna ','Test','923456789',SYSDATETIMEOFFSET(),'Active', 'System');
INSERT INTO [dbo].[User] ([Key], [Email], [PasswordSalt],[PasswordHash],[FirstName],[LastName],[PhoneNumber],[CreatedDate],[Status], [CreatedBy])VALUES(NEWID(),'dorota@test.pl','1b568a7c-61cf-415c-b293-dcf40362192c','AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==','Dorota','Test','103456789',SYSDATETIMEOFFSET(),'Active', 'System');
INSERT INTO [dbo].[User] ([Key], [Email], [PasswordSalt],[PasswordHash],[FirstName],[LastName],[PhoneNumber],[CreatedDate],[Status], [CreatedBy])VALUES(NEWID(),'sebastian@test.pl','1b568a7c-61cf-415c-b293-dcf40362192c','AM9FYjFIMAwUZ0n1xxt/nNFQilf4dI/OkwRTieH+Y0U2vgPYSMvqv5XfxGNQJksODQ==','Sebastian','Test','113456789',SYSDATETIMEOFFSET(),'Inactive', 'System');

-- USER ROLE -------------------------------------------------------------------------------------------------------------------
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),2,1,SYSDATETIMEOFFSET(),null,null,'Inactive', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),2,2,SYSDATETIMEOFFSET(),null,null,'Active', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),3,1,SYSDATETIMEOFFSET(),null,null,'Inactive', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),3,2,SYSDATETIMEOFFSET(),null,null,'Active', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),4,1,SYSDATETIMEOFFSET(),null,null,'Inactive', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),4,2,SYSDATETIMEOFFSET(),null,null,'Active', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),5,1,SYSDATETIMEOFFSET(),null,null,'Inactive', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),5,2,SYSDATETIMEOFFSET(),null,null,'Active', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),6,1,SYSDATETIMEOFFSET(),null,null,'Inactive', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),6,2,SYSDATETIMEOFFSET(),null,null,'Active', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),7,1,SYSDATETIMEOFFSET(),null,null,'Inactive', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),7,2,SYSDATETIMEOFFSET(),null,null,'Active', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),8,1,SYSDATETIMEOFFSET(),null,null,'Inactive', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),8,2,SYSDATETIMEOFFSET(),null,null,'Active', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),9,1,SYSDATETIMEOFFSET(),null,null,'Inactive', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),9,2,SYSDATETIMEOFFSET(),null,null,'Active', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),10,1,SYSDATETIMEOFFSET(),null,null,'Inactive', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),10,2,SYSDATETIMEOFFSET(),null,null,'Active', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),11,1,SYSDATETIMEOFFSET(),null,null,'Inactive', 'System');
INSERT INTO [dbo].[UserRole]([Key],[UserId],[RoleId],[CreatedDate],[ModifiedDate],[ModifiedBy],[Status], [CreatedBy])
VALUES(NEWID(),12,2,SYSDATETIMEOFFSET(),null,null,'Active', 'System');











