USE [master]
GO
/****** Object:  Database [AddGameBD]    Script Date: 14.12.2023 21:34:53 ******/
CREATE DATABASE [AddGameBD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AddGameBD', FILENAME = N'D:\project\DataBase\AddGameBD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AddGameBD_log', FILENAME = N'D:\project\DataBase\AddGameBD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AddGameBD] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AddGameBD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AddGameBD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AddGameBD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AddGameBD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AddGameBD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AddGameBD] SET ARITHABORT OFF 
GO
ALTER DATABASE [AddGameBD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AddGameBD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AddGameBD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AddGameBD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AddGameBD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AddGameBD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AddGameBD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AddGameBD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AddGameBD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AddGameBD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AddGameBD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AddGameBD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AddGameBD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AddGameBD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AddGameBD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AddGameBD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AddGameBD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AddGameBD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AddGameBD] SET  MULTI_USER 
GO
ALTER DATABASE [AddGameBD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AddGameBD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AddGameBD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AddGameBD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AddGameBD] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AddGameBD] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AddGameBD] SET QUERY_STORE = OFF
GO
USE [AddGameBD]
GO
/****** Object:  Table [dbo].[CPU]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CPU](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](25) NOT NULL,
	[idManufacturersCPU] [int] NOT NULL,
 CONSTRAINT [PK_CPU] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Developers]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Developers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[idUser] [int] NOT NULL,
	[address] [nvarchar](200) NOT NULL,
	[dateCreate] [date] NOT NULL,
 CONSTRAINT [PK_Developers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favorites]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorites](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUser] [int] NOT NULL,
	[idGame] [int] NOT NULL,
 CONSTRAINT [PK_Favorites] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameGenre]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameGenre](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[genre] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_GenreGame] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameMode]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameMode](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[mode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_GameMode] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Games]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Games](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[idGPU] [int] NOT NULL,
	[idCPU] [int] NOT NULL,
	[idOC] [int] NOT NULL,
	[RAM] [int] NOT NULL,
	[diskSpace] [int] NOT NULL,
	[idGameMode] [int] NOT NULL,
	[idGameGenre] [int] NOT NULL,
	[description] [nvarchar](max) NULL,
	[idDeveloper] [int] NOT NULL,
	[pathImage] [nvarchar](max) NOT NULL,
	[price] [money] NOT NULL,
	[releaseDate] [date] NULL,
	[discount] [float] NULL,
 CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GPU]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GPU](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](25) NOT NULL,
	[idManufacturersGPU] [int] NOT NULL,
 CONSTRAINT [PK_VideoCards] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManufacturersCPU]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManufacturersCPU](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ManufacturersCPU] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManufacturersGPU]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManufacturersGPU](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ManufacturersGPU] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OC]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OC](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_OC] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderList]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderList](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idOrder] [int] NOT NULL,
	[idGame] [int] NOT NULL,
	[price] [money] NOT NULL,
	[discount] [float] NULL,
	[keyGame] [nvarchar](10) NULL,
 CONSTRAINT [PK_OrderList] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUsers] [int] NOT NULL,
	[priceAll] [money] NOT NULL,
	[idPaymentMethod] [int] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethod]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethod](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[method] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_PaymentMethod] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usesrs]    Script Date: 14.12.2023 21:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usesrs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](50) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[patronimyc] [nvarchar](50) NULL,
	[login] [nvarchar](20) NOT NULL,
	[password] [nvarchar](20) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[pathAvatar] [nvarchar](max) NULL,
 CONSTRAINT [PK_Usesrs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CPU] ON 

INSERT [dbo].[CPU] ([id], [name], [idManufacturersCPU]) VALUES (1, N'Redeon 2600', 1)
INSERT [dbo].[CPU] ([id], [name], [idManufacturersCPU]) VALUES (3, N'Radeon 3600', 1)
INSERT [dbo].[CPU] ([id], [name], [idManufacturersCPU]) VALUES (4, N'Core i3', 2)
INSERT [dbo].[CPU] ([id], [name], [idManufacturersCPU]) VALUES (5, N'Core i5', 2)
SET IDENTITY_INSERT [dbo].[CPU] OFF
GO
SET IDENTITY_INSERT [dbo].[Developers] ON 

INSERT [dbo].[Developers] ([id], [name], [idUser], [address], [dateCreate]) VALUES (1, N'Ubisoft', 1, N'Рязань', CAST(N'2023-10-22' AS Date))
INSERT [dbo].[Developers] ([id], [name], [idUser], [address], [dateCreate]) VALUES (2, N'Diss324', 2, N'Россия, Рязань, Островского, 24', CAST(N'2023-11-08' AS Date))
INSERT [dbo].[Developers] ([id], [name], [idUser], [address], [dateCreate]) VALUES (3, N'ARM', 3, N'Россия, Рязань, Гоголя, 21', CAST(N'2023-11-19' AS Date))
SET IDENTITY_INSERT [dbo].[Developers] OFF
GO
SET IDENTITY_INSERT [dbo].[Favorites] ON 

INSERT [dbo].[Favorites] ([id], [idUser], [idGame]) VALUES (37, 1, 3)
SET IDENTITY_INSERT [dbo].[Favorites] OFF
GO
SET IDENTITY_INSERT [dbo].[GameGenre] ON 

INSERT [dbo].[GameGenre] ([id], [genre]) VALUES (1, N'шутер')
INSERT [dbo].[GameGenre] ([id], [genre]) VALUES (2, N'хоррор')
INSERT [dbo].[GameGenre] ([id], [genre]) VALUES (3, N'выживание')
INSERT [dbo].[GameGenre] ([id], [genre]) VALUES (4, N'открытый мир')
SET IDENTITY_INSERT [dbo].[GameGenre] OFF
GO
SET IDENTITY_INSERT [dbo].[GameMode] ON 

INSERT [dbo].[GameMode] ([id], [mode]) VALUES (1, N'Мультиплеер')
INSERT [dbo].[GameMode] ([id], [mode]) VALUES (2, N'Одиночный')
INSERT [dbo].[GameMode] ([id], [mode]) VALUES (3, N'Мультиплеер и одиночный')
SET IDENTITY_INSERT [dbo].[GameMode] OFF
GO
SET IDENTITY_INSERT [dbo].[Games] ON 

INSERT [dbo].[Games] ([id], [name], [idGPU], [idCPU], [idOC], [RAM], [diskSpace], [idGameMode], [idGameGenre], [description], [idDeveloper], [pathImage], [price], [releaseDate], [discount]) VALUES (1, N'FarCry5', 3, 4, 2, 12, 78, 3, 1, N'Игра с открытом миром.', 1, N'C:\Users\Дмитрий\Desktop\Запасной магазин\AddGameApp\AddGameApp\Documets\iconFarCry5.jpg', 1500.0000, CAST(N'2018-06-12' AS Date), 0.5)
INSERT [dbo].[Games] ([id], [name], [idGPU], [idCPU], [idOC], [RAM], [diskSpace], [idGameMode], [idGameGenre], [description], [idDeveloper], [pathImage], [price], [releaseDate], [discount]) VALUES (3, N'FarCry4', 2, 1, 2, 10, 34, 3, 4, N'', 1, N'C:\Users\Дмитрий\Desktop\Запасной магазин\AddGameApp\AddGameApp\Documets\iconFarCry4.jpg', 1200.0000, CAST(N'2015-03-24' AS Date), 0.25)
INSERT [dbo].[Games] ([id], [name], [idGPU], [idCPU], [idOC], [RAM], [diskSpace], [idGameMode], [idGameGenre], [description], [idDeveloper], [pathImage], [price], [releaseDate], [discount]) VALUES (4, N'FarCry3', 1, 4, 1, 8, 20, 2, 4, N'Третья часть популярной серии шутеров. Far Cry 3 познакомит игроков с темными секретами таинственного острова, на котором главному герою, Джейсону Броди, предстоит стать настоящим мужчиной. Огромный открытый мир, большое разнообразие оружия, харизматичные персонажи и запоминающийся сюжет.
', 1, N'C:\Users\Дмитрий\Desktop\Запасной магазин\AddGameApp\AddGameApp\Documets\iconFarCry3.jpg', 300.0000, CAST(N'2011-08-11' AS Date), 0.15)
INSERT [dbo].[Games] ([id], [name], [idGPU], [idCPU], [idOC], [RAM], [diskSpace], [idGameMode], [idGameGenre], [description], [idDeveloper], [pathImage], [price], [releaseDate], [discount]) VALUES (6, N'The forest', 2, 1, 1, 6, 12, 3, 3, N'', 2, N'C:\Users\Дмитрий\Desktop\Запасной магазин\AddGameApp\AddGameApp\Documets\iconTheForest.jpg', 1200.0000, CAST(N'2023-11-16' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[Games] OFF
GO
SET IDENTITY_INSERT [dbo].[GPU] ON 

INSERT [dbo].[GPU] ([id], [name], [idManufacturersGPU]) VALUES (1, N'RX 580', 1)
INSERT [dbo].[GPU] ([id], [name], [idManufacturersGPU]) VALUES (2, N'RX 780', 1)
INSERT [dbo].[GPU] ([id], [name], [idManufacturersGPU]) VALUES (3, N'GeForce rtx 2080', 2)
INSERT [dbo].[GPU] ([id], [name], [idManufacturersGPU]) VALUES (4, N'GeForce gtx 1660', 2)
SET IDENTITY_INSERT [dbo].[GPU] OFF
GO
SET IDENTITY_INSERT [dbo].[ManufacturersCPU] ON 

INSERT [dbo].[ManufacturersCPU] ([id], [name]) VALUES (1, N'AMD')
INSERT [dbo].[ManufacturersCPU] ([id], [name]) VALUES (2, N'Intel')
SET IDENTITY_INSERT [dbo].[ManufacturersCPU] OFF
GO
SET IDENTITY_INSERT [dbo].[ManufacturersGPU] ON 

INSERT [dbo].[ManufacturersGPU] ([id], [name]) VALUES (1, N'AMD')
INSERT [dbo].[ManufacturersGPU] ([id], [name]) VALUES (2, N'Nvidia')
SET IDENTITY_INSERT [dbo].[ManufacturersGPU] OFF
GO
SET IDENTITY_INSERT [dbo].[OC] ON 

INSERT [dbo].[OC] ([id], [name]) VALUES (1, N'Windows 10')
INSERT [dbo].[OC] ([id], [name]) VALUES (2, N'Windows 11')
SET IDENTITY_INSERT [dbo].[OC] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderList] ON 

INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (27, 14, 1, 1200.0000, NULL, NULL)
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (28, 15, 4, 300.0000, 0.15, NULL)
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (29, 16, 3, 500.0000, NULL, NULL)
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (30, 17, 1, 1200.0000, NULL, NULL)
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (31, 18, 4, 300.0000, 0.15, NULL)
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (32, 19, 3, 500.0000, NULL, NULL)
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (33, 20, 1, 1200.0000, NULL, NULL)
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (34, 20, 3, 500.0000, NULL, NULL)
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (35, 21, 4, 300.0000, 0.15, NULL)
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (36, 22, 6, 700.0000, NULL, NULL)
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (37, 23, 6, 1200.0000, NULL, N'hpHnpRinq')
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (38, 24, 1, 1500.0000, 0.5, N'09oXl9UPX')
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (39, 25, 4, 300.0000, 0.15, N'ZaVJ3PGptS')
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (40, 25, 6, 1200.0000, NULL, N'ooD9yPCOV')
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (41, 26, 3, 1200.0000, 0.25, N'0umYiyeRG')
INSERT [dbo].[OrderList] ([id], [idOrder], [idGame], [price], [discount], [keyGame]) VALUES (42, 27, 3, 1200.0000, 0.25, N'H6kljMXgR')
SET IDENTITY_INSERT [dbo].[OrderList] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (14, 2, 1200.0000, NULL)
INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (15, 2, -945.0000, NULL)
INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (16, 2, 245.0000, NULL)
INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (17, 3, 1200.0000, NULL)
INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (18, 3, 255.0000, NULL)
INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (19, 3, 500.0000, NULL)
INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (20, 1, 1700.0000, NULL)
INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (21, 1, 255.0000, NULL)
INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (22, 1, 700.0000, NULL)
INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (23, 3, 1200.0000, NULL)
INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (24, 10, 750.0000, NULL)
INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (25, 10, 1455.0000, 2)
INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (26, 10, 900.0000, 3)
INSERT [dbo].[Orders] ([id], [idUsers], [priceAll], [idPaymentMethod]) VALUES (27, 11, 900.0000, 4)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[PaymentMethod] ON 

INSERT [dbo].[PaymentMethod] ([id], [method]) VALUES (1, N'Visa')
INSERT [dbo].[PaymentMethod] ([id], [method]) VALUES (2, N'MasterCard')
INSERT [dbo].[PaymentMethod] ([id], [method]) VALUES (3, N'МИР')
INSERT [dbo].[PaymentMethod] ([id], [method]) VALUES (4, N'QIWI кошелек')
SET IDENTITY_INSERT [dbo].[PaymentMethod] OFF
GO
SET IDENTITY_INSERT [dbo].[Usesrs] ON 

INSERT [dbo].[Usesrs] ([id], [firstName], [lastName], [patronimyc], [login], [password], [email], [pathAvatar]) VALUES (1, N'Дмитрий', N'Каширкин', N'Андреевич', N'123', N'123', N'dm', N'')
INSERT [dbo].[Usesrs] ([id], [firstName], [lastName], [patronimyc], [login], [password], [email], [pathAvatar]) VALUES (2, N'Вадим', N'Галкин', N'Иванович', N'12', N'12', N'dghwerwerwer', N'C:\Users\Дмитрий\Desktop\Магазин_игр\AddGameApp\AddGameApp\Documets\2db9004b5969d123191db46eb3325f4e.jpg')
INSERT [dbo].[Usesrs] ([id], [firstName], [lastName], [patronimyc], [login], [password], [email], [pathAvatar]) VALUES (3, N'Алексей', N'Тупин', N'Владимирович', N'13', N'13', N'ааа', N'')
INSERT [dbo].[Usesrs] ([id], [firstName], [lastName], [patronimyc], [login], [password], [email], [pathAvatar]) VALUES (5, N'Андрей', N'Лучший', NULL, N'Log1', N'pas123', N'dm@gmail.com', NULL)
INSERT [dbo].[Usesrs] ([id], [firstName], [lastName], [patronimyc], [login], [password], [email], [pathAvatar]) VALUES (7, N'dfgdf', N'dfgdf', N'dfgdfg', N'dfgdfgf', N'123456', N'dm@gmail.com', NULL)
INSERT [dbo].[Usesrs] ([id], [firstName], [lastName], [patronimyc], [login], [password], [email], [pathAvatar]) VALUES (8, N'dfg', N'gfdg', N'dfg', N'1234', N'123456', N'as@mail.ru', NULL)
INSERT [dbo].[Usesrs] ([id], [firstName], [lastName], [patronimyc], [login], [password], [email], [pathAvatar]) VALUES (9, N'dfs', N'dsfs', N'dsf', N'fsdf', N'123456', N'g@mail.ru', NULL)
INSERT [dbo].[Usesrs] ([id], [firstName], [lastName], [patronimyc], [login], [password], [email], [pathAvatar]) VALUES (10, N'Кирилл', N'Лапкин', N'Егорович', N'dima', N'123456', N'@gmail.com', NULL)
INSERT [dbo].[Usesrs] ([id], [firstName], [lastName], [patronimyc], [login], [password], [email], [pathAvatar]) VALUES (11, N'Дмитрий', N'Каширкин', N'Андреевич', N'diman', N'123456', N'ip@gmail.com', NULL)
INSERT [dbo].[Usesrs] ([id], [firstName], [lastName], [patronimyc], [login], [password], [email], [pathAvatar]) VALUES (12, N'Максим', N'Леонидов', N'Егорович', N'maks12', N'123456', N'maks@mail.ru', NULL)
SET IDENTITY_INSERT [dbo].[Usesrs] OFF
GO
ALTER TABLE [dbo].[CPU]  WITH CHECK ADD  CONSTRAINT [FK_CPU_ManufacturersCPU] FOREIGN KEY([idManufacturersCPU])
REFERENCES [dbo].[ManufacturersCPU] ([id])
GO
ALTER TABLE [dbo].[CPU] CHECK CONSTRAINT [FK_CPU_ManufacturersCPU]
GO
ALTER TABLE [dbo].[Developers]  WITH CHECK ADD  CONSTRAINT [FK_Developers_Usesrs] FOREIGN KEY([idUser])
REFERENCES [dbo].[Usesrs] ([id])
GO
ALTER TABLE [dbo].[Developers] CHECK CONSTRAINT [FK_Developers_Usesrs]
GO
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [FK_Favorites_Games] FOREIGN KEY([idGame])
REFERENCES [dbo].[Games] ([id])
GO
ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [FK_Favorites_Games]
GO
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [FK_Favorites_Usesrs] FOREIGN KEY([idUser])
REFERENCES [dbo].[Usesrs] ([id])
GO
ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [FK_Favorites_Usesrs]
GO
ALTER TABLE [dbo].[Games]  WITH CHECK ADD  CONSTRAINT [FK_Games_CPU] FOREIGN KEY([idCPU])
REFERENCES [dbo].[CPU] ([id])
GO
ALTER TABLE [dbo].[Games] CHECK CONSTRAINT [FK_Games_CPU]
GO
ALTER TABLE [dbo].[Games]  WITH CHECK ADD  CONSTRAINT [FK_Games_Developers] FOREIGN KEY([idDeveloper])
REFERENCES [dbo].[Developers] ([id])
GO
ALTER TABLE [dbo].[Games] CHECK CONSTRAINT [FK_Games_Developers]
GO
ALTER TABLE [dbo].[Games]  WITH CHECK ADD  CONSTRAINT [FK_Games_GameGenre] FOREIGN KEY([idGameGenre])
REFERENCES [dbo].[GameGenre] ([id])
GO
ALTER TABLE [dbo].[Games] CHECK CONSTRAINT [FK_Games_GameGenre]
GO
ALTER TABLE [dbo].[Games]  WITH CHECK ADD  CONSTRAINT [FK_Games_GameMode] FOREIGN KEY([idGameMode])
REFERENCES [dbo].[GameMode] ([id])
GO
ALTER TABLE [dbo].[Games] CHECK CONSTRAINT [FK_Games_GameMode]
GO
ALTER TABLE [dbo].[Games]  WITH CHECK ADD  CONSTRAINT [FK_Games_GPU] FOREIGN KEY([idGPU])
REFERENCES [dbo].[GPU] ([id])
GO
ALTER TABLE [dbo].[Games] CHECK CONSTRAINT [FK_Games_GPU]
GO
ALTER TABLE [dbo].[Games]  WITH CHECK ADD  CONSTRAINT [FK_Games_OC] FOREIGN KEY([idOC])
REFERENCES [dbo].[OC] ([id])
GO
ALTER TABLE [dbo].[Games] CHECK CONSTRAINT [FK_Games_OC]
GO
ALTER TABLE [dbo].[GPU]  WITH CHECK ADD  CONSTRAINT [FK_GPU_ManufacturersGPU] FOREIGN KEY([idManufacturersGPU])
REFERENCES [dbo].[ManufacturersGPU] ([id])
GO
ALTER TABLE [dbo].[GPU] CHECK CONSTRAINT [FK_GPU_ManufacturersGPU]
GO
ALTER TABLE [dbo].[OrderList]  WITH CHECK ADD  CONSTRAINT [FK_OrderList_Games] FOREIGN KEY([idGame])
REFERENCES [dbo].[Games] ([id])
GO
ALTER TABLE [dbo].[OrderList] CHECK CONSTRAINT [FK_OrderList_Games]
GO
ALTER TABLE [dbo].[OrderList]  WITH CHECK ADD  CONSTRAINT [FK_OrderList_Orders] FOREIGN KEY([idOrder])
REFERENCES [dbo].[Orders] ([id])
GO
ALTER TABLE [dbo].[OrderList] CHECK CONSTRAINT [FK_OrderList_Orders]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_PaymentMethod] FOREIGN KEY([idPaymentMethod])
REFERENCES [dbo].[PaymentMethod] ([id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_PaymentMethod]
GO
USE [master]
GO
ALTER DATABASE [AddGameBD] SET  READ_WRITE 
GO
