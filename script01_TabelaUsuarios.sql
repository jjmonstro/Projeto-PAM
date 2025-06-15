IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [TB_USUARIOS] (
    [Id] int NOT NULL IDENTITY,
    [Username] Varchar(200) NOT NULL,
    [PasswordHash] varbinary(max) NULL,
    [PasswordSalt] varbinary(max) NULL,
    [Foto] Varchar(200) NULL,
    [Email] Varchar(200) NULL,
    CONSTRAINT [PK_TB_USUARIOS] PRIMARY KEY ([Id])
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Foto', N'PasswordHash', N'PasswordSalt', N'Username') AND [object_id] = OBJECT_ID(N'[TB_USUARIOS]'))
    SET IDENTITY_INSERT [TB_USUARIOS] ON;
INSERT INTO [TB_USUARIOS] ([Id], [Email], [Foto], [PasswordHash], [PasswordSalt], [Username])
VALUES (1, 'seuEmail@example.com', 'https://thvnext.bing.com/th/id/OIP.fU7XmhYQvxJs89FnvKwgigHaEk?cb=thvnext&rs=1&pid=ImgDetMain', 0x6E83244C34404CAD3B5963B52B10DFE034680017C3C507464E36EB929A48792AB3CAA613FC42B5F9DC8BED08BE84FA0FF582CBD3C07C0C198AC43BD34CA7694B, 0xB47DAD52C201B52EC915460AD95A8749B9F2474A9524615660DE6952EBE7417B7CDF67F54D4AA314FF41931E870635FA6D5283FE7CC88DF272E7D226F7FCDB7E34BC351AEA126BE9F8756C99A51922265188E1465B1C0B7455D417227356F88A6024013F264D3DD4A9E38F74599A20506921427C012A785386E313CBE595839E, 'UsuarioAdmin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'Foto', N'PasswordHash', N'PasswordSalt', N'Username') AND [object_id] = OBJECT_ID(N'[TB_USUARIOS]'))
    SET IDENTITY_INSERT [TB_USUARIOS] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250614141417_InitialCreate', N'9.0.6');

COMMIT;
GO

