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
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    CREATE TABLE [Adendums] (
        [Id] int NOT NULL IDENTITY,
        [CodigoAdendum] nvarchar(25) NOT NULL,
        [FechaAdendum] datetime2 NOT NULL,
        [FechaActivacion] datetime2 NOT NULL,
        [FechaExpiracion] datetime2 NOT NULL,
        [Observacion] nvarchar(max) NULL,
        [Documento] nvarchar(max) NULL,
        CONSTRAINT [PK_Adendums] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    CREATE TABLE [Adendumsp] (
        [Id] int NOT NULL IDENTITY,
        [CodigoAdendum] nvarchar(25) NOT NULL,
        [FechaAdendum] datetime2 NOT NULL,
        [FechaActivacion] datetime2 NOT NULL,
        [FechaExpiracion] datetime2 NOT NULL,
        [Observacion] nvarchar(max) NULL,
        [Documento] nvarchar(max) NULL,
        CONSTRAINT [PK_Adendumsp] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    CREATE TABLE [Clientes] (
        [Id] int NOT NULL IDENTITY,
        [RazonSocial] nvarchar(50) NOT NULL,
        [NombreComercial] nvarchar(50) NOT NULL,
        [ContactoCliente] nvarchar(25) NOT NULL,
        [Telefono] nvarchar(10) NOT NULL,
        [Nit] nvarchar(15) NOT NULL,
        [Email] nvarchar(30) NOT NULL,
        CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    CREATE TABLE [Contratos] (
        [Id] int NOT NULL IDENTITY,
        [CodigoContrato] nvarchar(300) NOT NULL,
        [FechaContrato] datetime2 NOT NULL,
        [FechaActivacion] datetime2 NOT NULL,
        [FechaExpiracion] datetime2 NOT NULL,
        [TipoContrato] nvarchar(max) NULL,
        [Documento] nvarchar(max) NULL,
        CONSTRAINT [PK_Contratos] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    CREATE TABLE [Contratosp] (
        [Id] int NOT NULL IDENTITY,
        [CodigoContrato] nvarchar(300) NOT NULL,
        [FechaContrato] datetime2 NOT NULL,
        [FechaActivacion] datetime2 NOT NULL,
        [FechaExpiracion] datetime2 NOT NULL,
        [TipoContrato] nvarchar(max) NULL,
        [Documento] nvarchar(max) NULL,
        CONSTRAINT [PK_Contratosp] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    CREATE TABLE [Proveedores] (
        [Id] int NOT NULL IDENTITY,
        [RazonSocial] nvarchar(max) NULL,
        [NombreComercial] nvarchar(max) NULL,
        [ContactoComercial] nvarchar(max) NULL,
        [PaginaWeb] nvarchar(max) NULL,
        [Telefono] nvarchar(max) NULL,
        [Nit] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        CONSTRAINT [PK_Proveedores] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    CREATE TABLE [Servicios] (
        [Id] int NOT NULL IDENTITY,
        [CodigoServicio] nvarchar(25) NOT NULL,
        [TipoServicio] nvarchar(max) NULL,
        [FechaActivacion] datetime2 NOT NULL,
        [FechaExpiracion] datetime2 NOT NULL,
        CONSTRAINT [PK_Servicios] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    CREATE TABLE [ClientesAdendums] (
        [ClienteId] int NOT NULL,
        [AdendumId] int NOT NULL,
        CONSTRAINT [PK_ClientesAdendums] PRIMARY KEY ([ClienteId], [AdendumId]),
        CONSTRAINT [FK_ClientesAdendums_Adendums_AdendumId] FOREIGN KEY ([AdendumId]) REFERENCES [Adendums] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ClientesAdendums_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    CREATE TABLE [CContratos] (
        [ClienteId] int NOT NULL,
        [ContratoId] int NOT NULL,
        [Personaje] nvarchar(100) NULL,
        [Orden] int NOT NULL,
        CONSTRAINT [PK_CContratos] PRIMARY KEY ([ContratoId], [ClienteId]),
        CONSTRAINT [FK_CContratos_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CContratos_Contratos_ContratoId] FOREIGN KEY ([ContratoId]) REFERENCES [Contratos] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    CREATE TABLE [ClientesServicios] (
        [ClienteId] int NOT NULL,
        [ServicioId] int NOT NULL,
        CONSTRAINT [PK_ClientesServicios] PRIMARY KEY ([ClienteId], [ServicioId]),
        CONSTRAINT [FK_ClientesServicios_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ClientesServicios_Servicios_ServicioId] FOREIGN KEY ([ServicioId]) REFERENCES [Servicios] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    CREATE INDEX [IX_CContratos_ClienteId] ON [CContratos] ([ClienteId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    CREATE INDEX [IX_ClientesAdendums_AdendumId] ON [ClientesAdendums] ([AdendumId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    CREATE INDEX [IX_ClientesServicios_ServicioId] ON [ClientesServicios] ([ServicioId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220523222434_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220523222434_Initial', N'5.0.16');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524061613_SistemaDeUsuarios')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220524061613_SistemaDeUsuarios', N'5.0.16');
END;
GO

COMMIT;
GO

