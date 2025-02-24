USE [master]
GO
/****** Object:  Database [GuestBook]    Script Date: 2025/2/20 上午 09:12:52 ******/
CREATE DATABASE [GuestBook]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GuestBook', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\GuestBook.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GuestBook_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\GuestBook_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [GuestBook] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GuestBook].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GuestBook] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GuestBook] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GuestBook] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GuestBook] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GuestBook] SET ARITHABORT OFF 
GO
ALTER DATABASE [GuestBook] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GuestBook] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GuestBook] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GuestBook] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GuestBook] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GuestBook] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GuestBook] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GuestBook] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GuestBook] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GuestBook] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GuestBook] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GuestBook] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GuestBook] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GuestBook] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GuestBook] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GuestBook] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [GuestBook] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GuestBook] SET RECOVERY FULL 
GO
ALTER DATABASE [GuestBook] SET  MULTI_USER 
GO
ALTER DATABASE [GuestBook] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GuestBook] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GuestBook] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GuestBook] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GuestBook] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GuestBook] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'GuestBook', N'ON'
GO
ALTER DATABASE [GuestBook] SET QUERY_STORE = ON
GO
ALTER DATABASE [GuestBook] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [GuestBook]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2025/2/20 上午 09:12:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 2025/2/20 上午 09:12:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[BookID] [nvarchar](36) NOT NULL,
	[SN] [bigint] NOT NULL,
	[Title] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Author] [nvarchar](20) NOT NULL,
	[TimeStmp] [datetime2](7) NOT NULL,
	[PhotoType] [nvarchar](max) NULL,
	[Photo] [nvarchar](44) NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 2025/2/20 上午 09:12:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[Account] [nvarchar](10) NOT NULL,
	[Password] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[Account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rebook]    Script Date: 2025/2/20 上午 09:12:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rebook](
	[ReBookID] [nvarchar](36) NOT NULL,
	[SN] [bigint] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Author] [nvarchar](20) NOT NULL,
	[TimeStmp] [datetime2](7) NOT NULL,
	[BookID] [nvarchar](36) NOT NULL,
 CONSTRAINT [PK_Rebook] PRIMARY KEY CLUSTERED 
(
	[ReBookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241227033701_InitialCreate', N'8.0.11')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250107005349_AddLoginTable', N'8.0.11')
GO
INSERT [dbo].[Book] ([BookID], [SN], [Title], [Description], [Author], [TimeStmp], [PhotoType], [Photo]) VALUES (N'18999f00-0504-4aab-8a81-0c4a360c6a89', 0, N'脆皮鴨', N'香香脆脆又有水果味！！！！', N'Jack', CAST(N'2024-12-27T11:52:17.7448705' AS DateTime2), N'image/jpeg', N'18999f00-0504-4aab-8a81-0c4a360c6a89.jpg')
INSERT [dbo].[Book] ([BookID], [SN], [Title], [Description], [Author], [TimeStmp], [PhotoType], [Photo]) VALUES (N'35be5ceb-c0f3-4bf6-81b1-1d4bc948fe78', 0, N'asdg', N'ashdzfmkxkg
4564564', N'dhjds', CAST(N'2024-12-30T14:44:39.6038377' AS DateTime2), N'image/png', N'35be5ceb-c0f3-4bf6-81b1-1d4bc948fe78.jpg')
INSERT [dbo].[Book] ([BookID], [SN], [Title], [Description], [Author], [TimeStmp], [PhotoType], [Photo]) VALUES (N'38ee0ae9-4120-4011-9d5d-c1cb24d2275e', 0, N'櫻桃鴨', N'感覺好像很好吃耶！！！！', N'Jack', CAST(N'2024-12-27T11:52:17.7345978' AS DateTime2), N'image/jpeg', N'38ee0ae9-4120-4011-9d5d-c1cb24d2275e.jpg')
INSERT [dbo].[Book] ([BookID], [SN], [Title], [Description], [Author], [TimeStmp], [PhotoType], [Photo]) VALUES (N'3df107c7-35a4-4bd3-be42-011b4f8ad1a3', 0, N'asdg', N'asdhdz
asdyae7
ud
suds
ri



', N'asdftgsyh', CAST(N'2024-12-30T14:48:27.6557372' AS DateTime2), N'image/png', N'3df107c7-35a4-4bd3-be42-011b4f8ad1a3.jpg')
INSERT [dbo].[Book] ([BookID], [SN], [Title], [Description], [Author], [TimeStmp], [PhotoType], [Photo]) VALUES (N'5f12ce1d-3b36-4fd0-849d-61e7fe104019', 0, N'鴨油高麗菜', N'好像有點油！！！！', N'Mary', CAST(N'2024-12-27T11:52:17.7448608' AS DateTime2), N'image/jpeg', N'5f12ce1d-3b36-4fd0-849d-61e7fe104019.jpg')
INSERT [dbo].[Book] ([BookID], [SN], [Title], [Description], [Author], [TimeStmp], [PhotoType], [Photo]) VALUES (N'8f23002d-a456-4ccd-9e29-7a254b4f3de7', 0, N'sdfsd', N'asdhxdnjxdz
yuoyi''
yuogyhlgfkjy
', N'dshjdsj', CAST(N'2024-12-30T14:43:20.9474309' AS DateTime2), N'image/png', N'8f23002d-a456-4ccd-9e29-7a254b4f3de7.jpg')
INSERT [dbo].[Book] ([BookID], [SN], [Title], [Description], [Author], [TimeStmp], [PhotoType], [Photo]) VALUES (N'97912a90-7791-4fdb-9b89-cc867a7596c8', 0, N'dfhjxdjx', N'fdjcfgod', N'sdj', CAST(N'2025-01-06T15:08:47.2659236' AS DateTime2), N'image/png', N'97912a90-7791-4fdb-9b89-cc867a7596c8.jpg')
INSERT [dbo].[Book] ([BookID], [SN], [Title], [Description], [Author], [TimeStmp], [PhotoType], [Photo]) VALUES (N'caa9550f-1079-481d-ab61-1f4f500ad015', 0, N'薑母鴨', N'超暖耶！！！！', N'Jckason', CAST(N'2024-12-27T11:52:17.7448702' AS DateTime2), N'image/jpeg', N'caa9550f-1079-481d-ab61-1f4f500ad015.jpg')
GO
INSERT [dbo].[Login] ([Account], [Password]) VALUES (N'abc123', N'abc123456')
INSERT [dbo].[Login] ([Account], [Password]) VALUES (N'admin', N'12345678')
GO
INSERT [dbo].[Rebook] ([ReBookID], [SN], [Description], [Author], [TimeStmp], [BookID]) VALUES (N'0ae7558e-de58-4ec8-9e0a-a6a86de8cf50', 0, N'吃完開車上路就有理由了', N'Mike', CAST(N'2024-12-27T11:52:17.9865536' AS DateTime2), N'caa9550f-1079-481d-ab61-1f4f500ad015')
INSERT [dbo].[Rebook] ([ReBookID], [SN], [Description], [Author], [TimeStmp], [BookID]) VALUES (N'140666e0-5ea3-475f-bc3a-a5f64a995a34', 0, N'sadhn', N'shyzs', CAST(N'2025-01-06T15:40:20.3629704' AS DateTime2), N'97912a90-7791-4fdb-9b89-cc867a7596c8')
INSERT [dbo].[Rebook] ([ReBookID], [SN], [Description], [Author], [TimeStmp], [BookID]) VALUES (N'2ac29741-902d-4a30-ad74-5aebe751c715', 0, N'樓上愛吃噴~', N'Mike', CAST(N'2024-12-27T11:52:17.9865523' AS DateTime2), N'38ee0ae9-4120-4011-9d5d-c1cb24d2275e')
INSERT [dbo].[Rebook] ([ReBookID], [SN], [Description], [Author], [TimeStmp], [BookID]) VALUES (N'306161a6-8a16-446e-af89-c54547d41fa5', 0, N'什麼脆脆，根本超硬好嗎！！', N'Frank', CAST(N'2024-12-27T11:52:17.9865529' AS DateTime2), N'18999f00-0504-4aab-8a81-0c4a360c6a89')
INSERT [dbo].[Rebook] ([ReBookID], [SN], [Description], [Author], [TimeStmp], [BookID]) VALUES (N'46a022fc-6a26-4116-92cf-0f44f396ada1', 0, N'你確定？這道超雷耶', N'Mike', CAST(N'2024-12-27T11:52:17.9864895' AS DateTime2), N'38ee0ae9-4120-4011-9d5d-c1cb24d2275e')
INSERT [dbo].[Rebook] ([ReBookID], [SN], [Description], [Author], [TimeStmp], [BookID]) VALUES (N'6381a663-b79c-4b38-b3f9-1a5a3c14c283', 0, N'你的舌頭壞掉了吧', N'Mark', CAST(N'2024-12-27T11:52:17.9865525' AS DateTime2), N'5f12ce1d-3b36-4fd0-849d-61e7fe104019')
INSERT [dbo].[Rebook] ([ReBookID], [SN], [Description], [Author], [TimeStmp], [BookID]) VALUES (N'7377c638-b3a0-4df7-bc66-d4d139c40820', 0, N'冬天吃這個超爽的啦~', N'Ammy', CAST(N'2024-12-27T11:52:17.9865528' AS DateTime2), N'caa9550f-1079-481d-ab61-1f4f500ad015')
INSERT [dbo].[Rebook] ([ReBookID], [SN], [Description], [Author], [TimeStmp], [BookID]) VALUES (N'a5c6aaf9-fadb-4814-9b82-a4fa6bf6b15e', 0, N'不夠油我還不想吃咧', N'Arkya', CAST(N'2024-12-27T11:52:17.9865535' AS DateTime2), N'5f12ce1d-3b36-4fd0-849d-61e7fe104019')
INSERT [dbo].[Rebook] ([ReBookID], [SN], [Description], [Author], [TimeStmp], [BookID]) VALUES (N'c8cce07d-e1ea-4f86-9ee2-f2d0f9b49002', 0, N'樓下別亂說，這道菜超讚的，我都配三碗白飯了', N'Su', CAST(N'2024-12-27T11:52:17.9865518' AS DateTime2), N'38ee0ae9-4120-4011-9d5d-c1cb24d2275e')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Rebook_BookID]    Script Date: 2025/2/20 上午 09:12:52 ******/
CREATE NONCLUSTERED INDEX [IX_Rebook_BookID] ON [dbo].[Rebook]
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Rebook]  WITH CHECK ADD  CONSTRAINT [FK_Rebook_Book_BookID] FOREIGN KEY([BookID])
REFERENCES [dbo].[Book] ([BookID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rebook] CHECK CONSTRAINT [FK_Rebook_Book_BookID]
GO
USE [master]
GO
ALTER DATABASE [GuestBook] SET  READ_WRITE 
GO
