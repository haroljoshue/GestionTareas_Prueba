CREATE TABLE [Usuarios] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]        NVARCHAR (MAX) NOT NULL,
    [Apellido]      NVARCHAR (MAX) NOT NULL,
    [Email]         NVARCHAR (MAX) NOT NULL,
    [Contrasena]    NVARCHAR (MAX) NOT NULL,
    [FechaRegistro] DATETIME2 (7)  NOT NULL,
    [UserId]        NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([Id] ASC)
);



CREATE TABLE [Proyectos] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]            NVARCHAR (MAX) NOT NULL,
    [Descripcion]       NVARCHAR (MAX) NOT NULL,
    [FechaCreacion]     DATETIME2 (7)  NOT NULL,
    [FechaFinalizacion] DATETIME2 (7)  NOT NULL,
    [UsuarioId]         INT            NOT NULL,
    CONSTRAINT [PK_Proyectos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Proyectos_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([Id])
);

GO
CREATE NONCLUSTERED INDEX [IX_Proyectos_UsuarioId]
    ON [Proyectos]([UsuarioId] ASC);



CREATE TABLE [MiembrosProyectos] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [UsuarioId]  INT NOT NULL,
    [ProyectoId] INT NOT NULL,
    CONSTRAINT [PK_MiembrosProyectos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MiembrosProyectos_Proyectos_ProyectoId] FOREIGN KEY ([ProyectoId]) REFERENCES [dbo].[Proyectos] ([Id]),
    CONSTRAINT [FK_MiembrosProyectos_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([Id])
);

GO
CREATE NONCLUSTERED INDEX [IX_MiembrosProyectos_ProyectoId]
    ON [MiembrosProyectos]([ProyectoId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_MiembrosProyectos_UsuarioId]
    ON [MiembrosProyectos]([UsuarioId] ASC);



CREATE TABLE[Tareas] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]      NVARCHAR (MAX) NOT NULL,
    [Descripcion] NVARCHAR (MAX) NOT NULL,
    [FechaInicio] DATETIME2 (7)  NOT NULL,
    [FechaFin]    DATETIME2 (7)  NOT NULL,
    [Prioridad]   INT            NOT NULL,
    [Estado]      NVARCHAR (MAX) NOT NULL,
    [UsuarioId]   INT            NOT NULL,
    [ProyectoId]  INT            NOT NULL,
    CONSTRAINT [PK_Tareas] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tareas_Proyectos_ProyectoId] FOREIGN KEY ([ProyectoId]) REFERENCES [dbo].[Proyectos] ([Id]),
    CONSTRAINT [FK_Tareas_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([Id])
);

GO
CREATE NONCLUSTERED INDEX [IX_Tareas_ProyectoId]
    ON [Tareas]([ProyectoId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Tareas_UsuarioId]
    ON [Tareas]([UsuarioId] ASC);


CREATE TABLE [LogEventos] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Fecha]       DATETIME2 (7)  NOT NULL,
    [Descripcion] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_LogEventos] PRIMARY KEY CLUSTERED ([Id] ASC)
);



CREATE TABLE [AspNetUsers] (
    [Id]                   NVARCHAR (450)     NOT NULL,
    [UserName]             NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [AspNetUsers]([NormalizedEmail] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [AspNetUsers]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);



CREATE TABLE [AspNetRoles] (
    [Id]               NVARCHAR (450) NOT NULL,
    [Name]             NVARCHAR (256) NULL,
    [NormalizedName]   NVARCHAR (256) NULL,
    [ConcurrencyStamp] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [AspNetRoles]([NormalizedName] ASC) WHERE ([NormalizedName] IS NOT NULL);




CREATE TABLE [AspNetUserRoles] (
    [UserId] NVARCHAR (450) NOT NULL,
    [RoleId] NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId]
    ON [AspNetUserRoles]([RoleId] ASC);



CREATE TABLE [AspNetRoleClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [RoleId]     NVARCHAR (450) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
);



CREATE TABLE [AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (450) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId]
    ON [AspNetUserClaims]([UserId] ASC);



CREATE TABLE [AspNetUserLogins] (
    [LoginProvider]       NVARCHAR (128) NOT NULL,
    [ProviderKey]         NVARCHAR (128) NOT NULL,
    [ProviderDisplayName] NVARCHAR (MAX) NULL,
    [UserId]              NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId]
    ON [AspNetUserLogins]([UserId] ASC);



CREATE TABLE [AspNetUserTokens] (
    [UserId]        NVARCHAR (450) NOT NULL,
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [Name]          NVARCHAR (128) NOT NULL,
    [Value]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);







