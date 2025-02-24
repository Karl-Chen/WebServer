USE [master]
GO
/****** Object:  Database [MySchool]    Script Date: 2025/2/20 上午 09:15:42 ******/
CREATE DATABASE [MySchool]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MySchool', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MySchool.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MySchool_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MySchool_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MySchool] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MySchool].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MySchool] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MySchool] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MySchool] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MySchool] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MySchool] SET ARITHABORT OFF 
GO
ALTER DATABASE [MySchool] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MySchool] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MySchool] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MySchool] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MySchool] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MySchool] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MySchool] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MySchool] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MySchool] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MySchool] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MySchool] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MySchool] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MySchool] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MySchool] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MySchool] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MySchool] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MySchool] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MySchool] SET RECOVERY FULL 
GO
ALTER DATABASE [MySchool] SET  MULTI_USER 
GO
ALTER DATABASE [MySchool] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MySchool] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MySchool] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MySchool] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MySchool] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MySchool] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MySchool', N'ON'
GO
ALTER DATABASE [MySchool] SET QUERY_STORE = ON
GO
ALTER DATABASE [MySchool] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MySchool]
GO
/****** Object:  UserDefinedFunction [dbo].[getCourseID]    Script Date: 2025/2/20 上午 09:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[getCourseID] (@iDeptID nchar(1))
returns nchar(5)
as
begin
	declare @RetCourseID nchar(5), @TmpDepID nchar(1)
	declare @TmpCout int = 0
	select @TmpCout = count(*) from [Department]
	inner join [Course] on Department.DeptID=Course.DeptID where @iDeptID = Department.DeptID
	
	set @TmpCout += 1
	set @RetCourseID = 'C' + @iDeptID + RIGHT(REPLICATE('0', 3) + CAST(@TmpCout as varchar), 3)
	return @RetCourseID
end
GO
/****** Object:  Table [dbo].[Course]    Script Date: 2025/2/20 上午 09:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseID] [nchar](5) NOT NULL,
	[CourseName] [nvarchar](30) NOT NULL,
	[Credit] [int] NOT NULL,
	[Hour] [int] NOT NULL,
	[DeptID] [nchar](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2025/2/20 上午 09:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DeptID] [nchar](1) NOT NULL,
	[DeptName] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DeptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SelectionDetail]    Script Date: 2025/2/20 上午 09:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SelectionDetail](
	[StuID] [nchar](10) NOT NULL,
	[CourseID] [nchar](5) NOT NULL,
	[Year] [int] NULL,
	[Term] [tinyint] NULL,
	[score] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StuID] ASC,
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 2025/2/20 上午 09:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StuID] [nchar](10) NOT NULL,
	[StuName] [nvarchar](20) NOT NULL,
	[Tel] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](100) NULL,
	[Birthday] [datetime] NULL,
	[DeptID] [nchar](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Course] ([CourseID], [CourseName], [Credit], [Hour], [DeptID]) VALUES (N'CA001', N'凝', 4, 2, N'A')
INSERT [dbo].[Course] ([CourseID], [CourseName], [Credit], [Hour], [DeptID]) VALUES (N'CA002', N'發', 10, 5, N'A')
INSERT [dbo].[Course] ([CourseID], [CourseName], [Credit], [Hour], [DeptID]) VALUES (N'CA003', N'缠', 10, 5, N'A')
INSERT [dbo].[Course] ([CourseID], [CourseName], [Credit], [Hour], [DeptID]) VALUES (N'CA004', N'絕', 10, 5, N'A')
GO
INSERT [dbo].[Department] ([DeptID], [DeptName]) VALUES (N'C', N'具現化系')
INSERT [dbo].[Department] ([DeptID], [DeptName]) VALUES (N'F', N'放出系')
INSERT [dbo].[Department] ([DeptID], [DeptName]) VALUES (N'D', N'特質系')
INSERT [dbo].[Department] ([DeptID], [DeptName]) VALUES (N'A', N'強化系')
INSERT [dbo].[Department] ([DeptID], [DeptName]) VALUES (N'E', N'操作系')
INSERT [dbo].[Department] ([DeptID], [DeptName]) VALUES (N'B', N'變化系')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Departme__5E508265FAA81548]    Script Date: 2025/2/20 上午 09:15:42 ******/
ALTER TABLE [dbo].[Department] ADD UNIQUE NONCLUSTERED 
(
	[DeptName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Course] ADD  DEFAULT ((0)) FOR [Credit]
GO
ALTER TABLE [dbo].[Course] ADD  DEFAULT ((2)) FOR [Hour]
GO
ALTER TABLE [dbo].[SelectionDetail] ADD  DEFAULT (datepart(year,getdate())) FOR [Year]
GO
ALTER TABLE [dbo].[SelectionDetail] ADD  DEFAULT ((0)) FOR [score]
GO
ALTER TABLE [dbo].[SelectionDetail]  WITH CHECK ADD FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[SelectionDetail]  WITH CHECK ADD FOREIGN KEY([StuID])
REFERENCES [dbo].[Student] ([StuID])
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD FOREIGN KEY([DeptID])
REFERENCES [dbo].[Department] ([DeptID])
GO
/****** Object:  StoredProcedure [dbo].[InsertDeptmentData]    Script Date: 2025/2/20 上午 09:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[InsertDeptmentData]
	@AddDeptID nchar(1), @AddDeptName nvarchar(30)
as
begin
	declare @TmpID nchar(1) , @TmpName nvarchar(30)
	select @TmpID = DeptID from Department where DeptName = @AddDeptName
	select @TmpName = DeptName from Department where DeptID = @AddDeptID
	if (@TmpID is not null) print 'DeptName is Exist!'
	if (@TmpName is not null) print'DeptID is Exist!'
	if (@TmpID is null and @TmpName is null)
		insert into [Department] values (@AddDeptID, @AddDeptName)
end
GO
USE [master]
GO
ALTER DATABASE [MySchool] SET  READ_WRITE 
GO
