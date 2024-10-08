USE [master]
GO
/****** Object:  Database [GestionEscuela]    Script Date: 8/13/2024 10:28:53 PM ******/
CREATE DATABASE [GestionEscuela]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GestionEscuela', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\GestionEscuela.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GestionEscuela_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\GestionEscuela_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [GestionEscuela] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GestionEscuela].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GestionEscuela] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GestionEscuela] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GestionEscuela] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GestionEscuela] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GestionEscuela] SET ARITHABORT OFF 
GO
ALTER DATABASE [GestionEscuela] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GestionEscuela] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GestionEscuela] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GestionEscuela] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GestionEscuela] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GestionEscuela] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GestionEscuela] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GestionEscuela] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GestionEscuela] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GestionEscuela] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GestionEscuela] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GestionEscuela] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GestionEscuela] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GestionEscuela] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GestionEscuela] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GestionEscuela] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GestionEscuela] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GestionEscuela] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GestionEscuela] SET  MULTI_USER 
GO
ALTER DATABASE [GestionEscuela] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GestionEscuela] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GestionEscuela] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GestionEscuela] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GestionEscuela] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GestionEscuela] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [GestionEscuela] SET QUERY_STORE = ON
GO
ALTER DATABASE [GestionEscuela] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [GestionEscuela]
GO
/****** Object:  User [Admin]    Script Date: 8/13/2024 10:28:53 PM ******/
CREATE USER [Admin] FOR LOGIN [Admin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [Admin]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [Admin]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [Admin]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [Admin]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [Admin]
GO
ALTER ROLE [db_datareader] ADD MEMBER [Admin]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [Admin]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [Admin]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [Admin]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 8/13/2024 10:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[asistencia]    Script Date: 8/13/2024 10:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[asistencia](
	[idAsistencia] [nvarchar](128) NOT NULL,
	[dia] [date] NULL,
	[asistencia] [bit] NULL,
	[idMateria] [nvarchar](128) NULL,
	[idEstudiante] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[idAsistencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 8/13/2024 10:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 8/13/2024 10:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 8/13/2024 10:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 8/13/2024 10:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 8/13/2024 10:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[calificacion]    Script Date: 8/13/2024 10:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[calificacion](
	[idCalificacion] [nvarchar](128) NOT NULL,
	[idEstudiante] [nvarchar](128) NULL,
	[idMateria] [nvarchar](128) NULL,
	[calificacion1] [decimal](5, 2) NULL,
	[calificacion2] [decimal](5, 2) NULL,
	[calificacion3] [decimal](5, 2) NULL,
	[promedioMateria] [decimal](5, 2) NULL,
 CONSTRAINT [PK__califica__E056358FC58D874A] PRIMARY KEY CLUSTERED 
(
	[idCalificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[docente]    Script Date: 8/13/2024 10:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[docente](
	[idDocente] [nvarchar](128) NOT NULL,
	[idUsuario] [nvarchar](128) NULL,
	[nombre] [nvarchar](50) NULL,
	[paterno] [nvarchar](50) NULL,
	[materno] [nvarchar](50) NULL,
	[telefono] [nvarchar](20) NULL,
	[direccion] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[idDocente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[estudiante]    Script Date: 8/13/2024 10:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estudiante](
	[matricula] [nvarchar](128) NOT NULL,
	[nombre] [nvarchar](50) NULL,
	[paterno] [nvarchar](50) NULL,
	[materno] [nvarchar](50) NULL,
	[curp] [nvarchar](18) NULL,
	[telefono] [nvarchar](20) NULL,
	[direccion] [nvarchar](250) NOT NULL,
	[tipoSangre] [nvarchar](5) NULL,
	[promGeneral] [decimal](5, 2) NULL,
	[idUsuario] [nvarchar](128) NULL,
	[idTutor] [nvarchar](128) NULL,
 CONSTRAINT [PK__estudian__30962D1408896603] PRIMARY KEY CLUSTERED 
(
	[matricula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[grupo]    Script Date: 8/13/2024 10:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[grupo](
	[idGrupo] [nvarchar](128) NOT NULL,
	[nombre] [nvarchar](20) NOT NULL,
	[inicio] [date] NOT NULL,
	[fin] [date] NOT NULL,
	[descripcion] [nvarchar](100) NULL,
 CONSTRAINT [PK__grupo__EC597A87CC9BC733] PRIMARY KEY CLUSTERED 
(
	[idGrupo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[grupoEstudiante]    Script Date: 8/13/2024 10:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[grupoEstudiante](
	[idEstudiante] [nvarchar](128) NULL,
	[idGrupo] [nvarchar](128) NULL,
	[promedio] [decimal](3, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[grupoMateria]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[grupoMateria](
	[idGrupo] [nvarchar](128) NULL,
	[idMateria] [nvarchar](128) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[materia]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[materia](
	[idMateria] [nvarchar](128) NOT NULL,
	[nombre] [nvarchar](50) NULL,
	[descripcion] [nvarchar](255) NULL,
	[idDocente] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[idMateria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reporte]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reporte](
	[idReporte] [nvarchar](128) NOT NULL,
	[tipo] [nvarchar](50) NULL,
	[detalle] [nvarchar](250) NULL,
	[fecha] [date] NULL,
	[idEstudiante] [nvarchar](128) NULL,
	[creadoPor] [nvarchar](128) NULL,
 CONSTRAINT [PK__reporte__40D65D3CF69F2ED9] PRIMARY KEY CLUSTERED 
(
	[idReporte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tutor]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tutor](
	[idTutor] [nvarchar](128) NOT NULL,
	[idUsuario] [nvarchar](128) NULL,
	[nombre] [nvarchar](50) NULL,
	[paterno] [nvarchar](50) NULL,
	[materno] [nvarchar](50) NULL,
	[movil] [nvarchar](20) NULL,
	[telefono] [nvarchar](20) NULL,
	[direccion] [nvarchar](250) NULL,
	[relacion] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[idTutor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 8/13/2024 10:28:54 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 8/13/2024 10:28:54 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 8/13/2024 10:28:54 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 8/13/2024 10:28:54 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 8/13/2024 10:28:54 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 8/13/2024 10:28:54 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[asistencia]  WITH CHECK ADD  CONSTRAINT [FK__asistenci__idEst__73501C2F] FOREIGN KEY([idEstudiante])
REFERENCES [dbo].[estudiante] ([matricula])
GO
ALTER TABLE [dbo].[asistencia] CHECK CONSTRAINT [FK__asistenci__idEst__73501C2F]
GO
ALTER TABLE [dbo].[asistencia]  WITH CHECK ADD FOREIGN KEY([idMateria])
REFERENCES [dbo].[materia] ([idMateria])
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[calificacion]  WITH CHECK ADD  CONSTRAINT [FK__calificac__idEst__6E8B6712] FOREIGN KEY([idEstudiante])
REFERENCES [dbo].[estudiante] ([matricula])
GO
ALTER TABLE [dbo].[calificacion] CHECK CONSTRAINT [FK__calificac__idEst__6E8B6712]
GO
ALTER TABLE [dbo].[calificacion]  WITH CHECK ADD  CONSTRAINT [FK__calificac__idMat__6F7F8B4B] FOREIGN KEY([idMateria])
REFERENCES [dbo].[materia] ([idMateria])
GO
ALTER TABLE [dbo].[calificacion] CHECK CONSTRAINT [FK__calificac__idMat__6F7F8B4B]
GO
ALTER TABLE [dbo].[docente]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[estudiante]  WITH CHECK ADD  CONSTRAINT [FK__estudiant__idUsu__65F62111] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[estudiante] CHECK CONSTRAINT [FK__estudiant__idUsu__65F62111]
GO
ALTER TABLE [dbo].[estudiante]  WITH CHECK ADD  CONSTRAINT [FK_estudiante_tutor] FOREIGN KEY([idTutor])
REFERENCES [dbo].[tutor] ([idTutor])
GO
ALTER TABLE [dbo].[estudiante] CHECK CONSTRAINT [FK_estudiante_tutor]
GO
ALTER TABLE [dbo].[grupoEstudiante]  WITH CHECK ADD  CONSTRAINT [FK__grupoEstu__idEst__6ABAD62E] FOREIGN KEY([idEstudiante])
REFERENCES [dbo].[estudiante] ([matricula])
GO
ALTER TABLE [dbo].[grupoEstudiante] CHECK CONSTRAINT [FK__grupoEstu__idEst__6ABAD62E]
GO
ALTER TABLE [dbo].[grupoEstudiante]  WITH CHECK ADD  CONSTRAINT [FK__grupoEstu__idGru__6BAEFA67] FOREIGN KEY([idGrupo])
REFERENCES [dbo].[grupo] ([idGrupo])
GO
ALTER TABLE [dbo].[grupoEstudiante] CHECK CONSTRAINT [FK__grupoEstu__idGru__6BAEFA67]
GO
ALTER TABLE [dbo].[grupoMateria]  WITH CHECK ADD  CONSTRAINT [FK__grupoMate__idGru__67DE6983] FOREIGN KEY([idGrupo])
REFERENCES [dbo].[grupo] ([idGrupo])
GO
ALTER TABLE [dbo].[grupoMateria] CHECK CONSTRAINT [FK__grupoMate__idGru__67DE6983]
GO
ALTER TABLE [dbo].[grupoMateria]  WITH CHECK ADD FOREIGN KEY([idMateria])
REFERENCES [dbo].[materia] ([idMateria])
GO
ALTER TABLE [dbo].[materia]  WITH CHECK ADD FOREIGN KEY([idDocente])
REFERENCES [dbo].[docente] ([idDocente])
GO
ALTER TABLE [dbo].[reporte]  WITH CHECK ADD  CONSTRAINT [FK__reporte__idEstud__7908F585] FOREIGN KEY([idEstudiante])
REFERENCES [dbo].[estudiante] ([matricula])
GO
ALTER TABLE [dbo].[reporte] CHECK CONSTRAINT [FK__reporte__idEstud__7908F585]
GO
ALTER TABLE [dbo].[tutor]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[ActualizarPromedioGeneral]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimiento almacenado para actualizar el promedio general de un estudiante
CREATE PROCEDURE [dbo].[ActualizarPromedioGeneral]
AS
BEGIN
    UPDATE estudiante
    SET promedioGeneral = (
        SELECT AVG(calificacionCicloEscolar)
        FROM grupo
        WHERE grupo.idGrupo = estudiante.idGrupo
    )
    FROM estudiante;
END
GO
/****** Object:  StoredProcedure [dbo].[AsignarMateriaAGrupo]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AsignarMateriaAGrupo]
    @idGrupo NVARCHAR(128),
    @idMateria NVARCHAR(128)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM grupoMateria WHERE idGrupo = @idGrupo AND idMateria = @idMateria)
    BEGIN
        INSERT INTO grupoMateria (idGrupo, idMateria)
        VALUES (@idGrupo, @idMateria);
    END
END
GO
/****** Object:  StoredProcedure [dbo].[CalcularPromedioGeneralEstudiante]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CalcularPromedioGeneralEstudiante]
    @idEstudiante NVARCHAR(128)
AS
BEGIN
    DECLARE @TotalMaterias INT
    DECLARE @SumaCalificaciones DECIMAL(5, 2)

    -- Contar el número de materias en las que el estudiante tiene calificaciones
    SELECT @TotalMaterias = COUNT(DISTINCT idMateria)
    FROM calificacion
    WHERE idEstudiante = @idEstudiante;

    -- Sumar todas las calificaciones del estudiante
    SELECT @SumaCalificaciones = SUM((calificacion1 + calificacion2 + calificacion3) / 3.0)
    FROM calificacion
    WHERE idEstudiante = @idEstudiante;

    -- Calcular el promedio general y actualizar la tabla de estudiantes
    IF @TotalMaterias > 0
    BEGIN
        UPDATE estudiante
        SET promGeneral = @SumaCalificaciones / @TotalMaterias
        WHERE matricula = @idEstudiante;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[CalcularPromedioGeneralPorEstudiante]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CalcularPromedioGeneralPorEstudiante]
AS
BEGIN
    -- Declaramos un cursor para iterar sobre cada estudiante
    DECLARE curEstudiantes CURSOR FOR
    SELECT matricula FROM estudiante;

    DECLARE @matricula NVARCHAR(128);
    DECLARE @promedioGeneral DECIMAL(5, 2);

    OPEN curEstudiantes;

    FETCH NEXT FROM curEstudiantes INTO @matricula;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Calcular el promedio general de las materias del estudiante
        SELECT @promedioGeneral = AVG(promedioMateria)
        FROM calificacion
        WHERE idEstudiante = @matricula;

        -- Actualizar el promedio general en la tabla estudiante
        UPDATE estudiante
        SET promGeneral = @promedioGeneral
        WHERE matricula = @matricula;

        FETCH NEXT FROM curEstudiantes INTO @matricula;
    END

    CLOSE curEstudiantes;
    DEALLOCATE curEstudiantes;
END
GO
/****** Object:  StoredProcedure [dbo].[CrearCalificacionesPorMateria]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CrearCalificacionesPorMateria]
    @idMateria NVARCHAR(128)
AS
BEGIN
    -- Insertar registros en la tabla calificación solo para los estudiantes inscritos en la materia
    INSERT INTO calificacion (idCalificacion, idEstudiante, idMateria, calificacion1, calificacion2, calificacion3)
    SELECT NEWID(), e.matricula, @idMateria, NULL, NULL, NULL
    FROM estudiante e
    INNER JOIN grupoEstudiante ge ON e.matricula = ge.idEstudiante
    INNER JOIN grupoMateria gm ON ge.idGrupo = gm.idGrupo
    WHERE gm.idMateria = @idMateria
    AND e.matricula NOT IN (
        SELECT c.idEstudiante
        FROM calificacion c
        WHERE c.idMateria = @idMateria
    )
END
GO
/****** Object:  StoredProcedure [dbo].[DeterminarGrupoActual]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeterminarGrupoActual]
    @idEstudiante NVARCHAR(128)
AS
BEGIN
    DECLARE @fechaActual DATE = GETDATE();

    SELECT 
        g.idGrupo,
        g.nombre AS NombreGrupo,
        g.inicio,
        g.fin
    FROM 
        grupoEstudiante ge
    INNER JOIN 
        grupo g ON ge.idGrupo = g.idGrupo
    WHERE 
        ge.idEstudiante = @idEstudiante
        AND g.inicio <= @fechaActual
        AND g.fin >= @fechaActual;
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarEstudiante]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarEstudiante]
    @matricula NVARCHAR(128),
    @nombre NVARCHAR(50),
    @paterno NVARCHAR(50),
    @materno NVARCHAR(50),
    @curp NVARCHAR(18),
    @telefono NVARCHAR(20),
    @direccion NVARCHAR(250),
    @tipoSangre NVARCHAR(5),
    @idTutor NVARCHAR(128)
AS
BEGIN
    INSERT INTO estudiante (matricula, nombre, paterno, materno, curp, telefono, direccion, tipoSangre, idTutor)
    VALUES (@matricula, @nombre, @paterno, @materno, @curp, @telefono, @direccion, @tipoSangre, @idTutor);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerReporteAsistencia]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ObtenerReporteAsistencia]
    @idMateria NVARCHAR(128),
    @fechaInicio DATE,
    @fechaFin DATE
AS
BEGIN
    DECLARE @cols AS NVARCHAR(MAX);
    DECLARE @query AS NVARCHAR(MAX);

    -- Generar la lista de columnas dinámicas basada en las fechas de asistencia
    SET @cols = STUFF((
        SELECT DISTINCT ',' + QUOTENAME(CONVERT(VARCHAR, dia, 103))
        FROM asistencia
        WHERE idMateria = @idMateria AND dia BETWEEN @fechaInicio AND @fechaFin
        FOR XML PATH(''), TYPE
    ).value('.', 'NVARCHAR(MAX)') 
    , 1, 1, '');

    -- Crear la consulta dinámica para obtener el reporte de asistencia
    SET @query = '
        SELECT 
            NombreCompleto, ' + @cols + ',
            CAST(ROUND((CAST(SUM(CASE WHEN asistencia = ''ASISTIÓ'' THEN 1 ELSE 0 END) AS FLOAT) * 100.0) / COUNT(*), 2) AS NVARCHAR(5)) + '' %'' AS [% ASISTENCIA]
        FROM 
            (
                SELECT 
                    e.matricula, 
                    e.nombre + '' '' + e.paterno + '' '' + e.materno AS NombreCompleto, 
                    CONVERT(VARCHAR, a.dia, 103) AS dia, 
                    CASE WHEN a.asistencia = 1 THEN ''ASISTIÓ'' ELSE ''FALTÓ'' END AS asistencia
                FROM asistencia a
                INNER JOIN estudiante e ON a.idEstudiante = e.matricula
                WHERE a.idMateria = @idMateria AND a.dia BETWEEN @fechaInicio AND @fechaFin
            ) AS src
        PIVOT
        (
            MAX(asistencia)
            FOR dia IN (' + @cols + ')
        ) AS pvt
        ORDER BY NombreCompleto';

    -- Ejecutar la consulta dinámica
    EXEC sp_executesql @query, N'@idMateria NVARCHAR(128), @fechaInicio DATE, @fechaFin DATE', @idMateria, @fechaInicio, @fechaFin;
END
GO
/****** Object:  StoredProcedure [dbo].[VerCalificacionesEstudiante]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[VerCalificacionesEstudiante]
    @idEstudiante NVARCHAR(128)
AS
BEGIN
    SELECT 
        m.nombre AS NombreMateria,
        c.calificacion1,
        c.calificacion2,
        c.calificacion3,
        c.promedioMateria
    FROM 
        calificacion c
    INNER JOIN 
        materia m ON c.idMateria = m.idMateria
    WHERE 
        c.idEstudiante = @idEstudiante
    ORDER BY 
        m.nombre;
END
GO
/****** Object:  StoredProcedure [dbo].[VerCalificacionesYReportesParaTutor]    Script Date: 8/13/2024 10:28:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VerCalificacionesYReportesParaTutor]
    @idTutor NVARCHAR(128),
    @idEstudiante NVARCHAR(128)
AS
BEGIN
    -- Verificar que el estudiante está asignado al tutor
    IF EXISTS (SELECT 1 FROM estudiante WHERE idTutor = @idTutor AND matricula = @idEstudiante)
    BEGIN
        -- Calificaciones
        SELECT 
            m.nombre AS NombreMateria,
            c.calificacion1,
            c.calificacion2,
            c.calificacion3,
            c.promedioMateria
        FROM 
            calificacion c
        INNER JOIN 
            materia m ON c.idMateria = m.idMateria
        WHERE 
            c.idEstudiante = @idEstudiante
        ORDER BY 
            m.nombre;

        -- Reportes
        SELECT 
            r.tipo,
            r.detalle,
            r.fecha
        FROM 
            reporte r
        WHERE 
            r.idEstudiante = @idEstudiante
        ORDER BY 
            r.fecha DESC;
    END
    ELSE
    BEGIN
        PRINT 'El estudiante no está asignado al tutor';
    END
END
GO
USE [master]
GO
ALTER DATABASE [GestionEscuela] SET  READ_WRITE 
GO
