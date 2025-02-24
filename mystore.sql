USE [master]
GO
/****** Object:  Database [MyStore]    Script Date: 2025/2/20 上午 09:13:38 ******/
CREATE DATABASE [MyStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MyStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MyStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MyStore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MyStore] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyStore] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MyStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyStore] SET RECOVERY FULL 
GO
ALTER DATABASE [MyStore] SET  MULTI_USER 
GO
ALTER DATABASE [MyStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MyStore', N'ON'
GO
ALTER DATABASE [MyStore] SET QUERY_STORE = ON
GO
ALTER DATABASE [MyStore] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MyStore]
GO
/****** Object:  UserDefinedFunction [dbo].[fnGetOrderID]    Script Date: 2025/2/20 上午 09:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[fnGetOrderID]()
returns nchar(12)
as
begin
--取得當天規則

--四碼流水號規則
declare @icount int=0, @date datetime = getdate(), @strdate nchar(8)
set @strdate = convert(char, @date, 112)
--如果今天沒有任何訂單，則為0001
--set @strdate+='0001'
--否則為最後一張訂單號碼+1
--set @strdate+=(lastID+1)

select @icount = count(OrderID) from [Order] where datediff(day,OrderDate, @date) = 0
set @icount+=1
declare @newN nchar(12)
set @newN = @strdate + RIGHT(REPLICATE('0', 4) + CAST(@icount as varchar), 4)
return @newN

end
GO
/****** Object:  UserDefinedFunction [dbo].[getOrderID2]    Script Date: 2025/2/20 上午 09:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[getOrderID2]()
	returns nchar(12)
as
begin
--取得當天日期西元年月日（8碼）
	declare @today char(8) = convert(varchar, getdate(), 112)
	--print @today
	--四風流水號規則
	--如果今天沒有訂單則為0001
	declare @lastID char(12), @newID char(12)
	--抓取今天最後一張訂單號碼
	select top 1 @lastID = OrderID
	from [Order]
	where convert(varchar, OrderDate, 112) = convert(varchar, getdate(), 112)
	order by orderDate desc

	if @lastID is null 
		set @newID = @today + '0001'		--今天的第一張訂單編號
	else
		set @newID = cast(cast(@lastID as bigint) + 1 as varchar)			--否則為今天最後一張編號+1

	return @newID
end
GO
/****** Object:  UserDefinedFunction [dbo].[getProductID]    Script Date: 2025/2/20 上午 09:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[getProductID](@CateID nchar(2))
	returns nchar(5)
as
begin
	--從參數取得CateID
	--比對現有產品資料表所以同類的產品資料，抓最後一個同類的產品編號
	declare @lastID nchar(5)
	select top 1 @lastID = ProductID
	from [product]
	where CateID = @CateID
	order by ProductID desc
	declare @newID nchar(5)
	if @lastID is null
		set @newID = @CateID + '001'
	else
	begin
		declare @num int 
		set @num = cast(Right(@lastID, 3) as int) + 1
		set @newID = @CateID + Right(REPLICATE('0', 3) + cast(@num as varchar), 3)
	end
	return @newID

end
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2025/2/20 上午 09:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CateID] [nchar](2) NOT NULL,
	[CateName] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 2025/2/20 上午 09:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberID] [nchar](6) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Gender] [bit] NOT NULL,
	[Point] [int] NOT NULL,
	[Account] [nchar](12) NOT NULL,
	[PassWorld] [nchar](20) NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[MemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 2025/2/20 上午 09:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [nchar](17) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[MemberID] [nchar](6) NOT NULL,
	[ContactName] [nvarchar](27) NOT NULL,
	[ContactAddress] [nvarchar](60) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 2025/2/20 上午 09:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderID] [nchar](17) NOT NULL,
	[ProductID] [nchar](5) NOT NULL,
	[Qty] [int] NOT NULL,
	[Price] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2025/2/20 上午 09:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [nchar](5) NOT NULL,
	[ProductName] [nvarchar](40) NOT NULL,
	[Price] [money] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Picture] [nvarchar](12) NOT NULL,
	[CateID] [nchar](2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'A1', N'一般飲料')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'A2', N'咖啡')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'A3', N'啤酒')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'A4', N'提神飲料')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'B1', N'甜酸醬')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'B2', N'配料')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'B3', N'塗抹醬')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'B4', N'香料')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'C1', N'甜點心')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'C2', N'糖果')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'C3', N'麵包')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'D1', N'餅乾')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'E1', N'麵粉')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'E2', N'麥片')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'F1', N'豬肉')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'G1', N'水果乾')
INSERT [dbo].[Category] ([CateID], [CateName]) VALUES (N'G2', N'豆腐')
GO
INSERT [dbo].[Member] ([MemberID], [Name], [Gender], [Point], [Account], [PassWorld]) VALUES (N'M00001', N'余俊德', 1, 20, N'ABcde       ', N'123456              ')
INSERT [dbo].[Member] ([MemberID], [Name], [Gender], [Point], [Account], [PassWorld]) VALUES (N'M00002', N'陳小美', 0, 0, N'ABcde123    ', N'1234567890          ')
INSERT [dbo].[Member] ([MemberID], [Name], [Gender], [Point], [Account], [PassWorld]) VALUES (N'M00003', N'葉小文', 1, 0, N'ABcde13     ', N'12345567            ')
GO
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'20241217         ', CAST(N'2024-12-17T14:02:14.893' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170002     ', CAST(N'2024-12-17T14:12:17.343' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170003     ', CAST(N'2024-12-17T14:12:18.290' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170004     ', CAST(N'2024-12-17T14:12:19.013' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170005     ', CAST(N'2024-12-17T14:12:19.743' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170006     ', CAST(N'2024-12-17T14:12:20.593' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170007     ', CAST(N'2024-12-17T14:12:21.150' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170008     ', CAST(N'2024-12-17T14:12:21.657' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170009     ', CAST(N'2024-12-17T14:12:22.173' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170010     ', CAST(N'2024-12-17T14:12:22.827' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170011     ', CAST(N'2024-12-17T14:12:23.330' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170012     ', CAST(N'2024-12-17T14:12:23.777' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170013     ', CAST(N'2024-12-17T14:12:24.147' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170014     ', CAST(N'2024-12-17T14:12:24.617' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170015     ', CAST(N'2024-12-17T14:12:25.060' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170016     ', CAST(N'2024-12-17T14:37:02.047' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
INSERT [dbo].[Order] ([OrderID], [OrderDate], [MemberID], [ContactName], [ContactAddress]) VALUES (N'202412170017     ', CAST(N'2024-12-17T14:37:30.937' AS DateTime), N'M00001', N'余俊德', N'高雄市後德路100號')
GO
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'A2001', N'濃縮咖啡', 99.0000, N'高品質濃縮咖啡，適合用來製作各種咖啡飲品。', N'A2001.jpg', N'A2')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'A2002', N'即食咖啡', 89.0000, N'快速沖泡即飲咖啡，隨時享受濃郁的咖啡香氣。', N'A2002.jpg', N'A2')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'A3001', N'精釀啤酒', 129.0000, N'經典手工釀造啤酒，口感醇厚，適合與朋友聚會時享用。', N'A3001.jpg', N'A3')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'A3002', N'精選啤酒禮盒', 399.0000, N'包含多種啤酒口味的禮盒，適合作為送禮佳品。', N'A3002.jpg', N'A3')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'A4001', N'馬力夯', 35.0000, N'富含咖啡因的提神飲料，幫助你保持清醒和活力。', N'A4001.jpg', N'A4')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'B1001', N'蕃茄', 45.0000, N'經典甜酸醬，適合搭配炸物、春捲等食物。', N'B1001.jpg', N'B1')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'B2001', N'五香配料', 22.0000, N'多功能烹飪配料，讓您的料理更加美味。', N'B3001.jpg', N'B2')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'B2002', N'烤雞香料', 16.0000, N'專為烤雞設計的香料調味包，讓雞肉更加美味。', N'B2002.jpg', N'B2')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'B3001', N'花生塗抹醬', 59.0000, N'香濃花生醬，完美塗抹在吐司或搭配餅乾。', N'B3001.jpg', N'B3')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'B3002', N'肉桂香料', 14.0000, N'新鮮肉桂香料，適合用於甜點或飲品中。', N'B3002.jpg', N'B3')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'B3003', N'巧克力慕斯', 69.0000, N'濃郁巧克力慕斯，口感絲滑，完美享受。', N'B3003.jpg', N'B3')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'B3004', N'藍莓果醬', 49.0000, N'新鮮藍莓製作的果醬，搭配麵包或餅乾非常美味。', N'B3004.jpg', N'B3')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'B3005', N'手工草莓果醬', 39.0000, N'新鮮草莓製作的手工果醬，口感濃郁，甜美可口。', N'B3005.jpg', N'B3')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'B4001', N'迷迭香', 12.0000, N'新鮮迷迭香香料，提升料理的香氣和風味。', N'B4001.jpg', N'B4')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'C1001', N'巧克力甜點', 49.0000, N'濃郁巧克力味的甜點，香甜可口，令人回味無窮。', N'C1001.jpg', N'C1')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'C1002', N'紅豆餅', 59.0000, N'甜美的紅豆餡餅，外皮酥脆，內餡香甜。', N'C1002.jpg', N'C1')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'C1003', N'香濃巧克力棒', 59.0000, N'香濃的巧克力棒，帶來滿滿的幸福感。', N'C1003.jpg', N'C1')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'C2001', N'水果糖', 19.0000, N'多種口味的水果糖，口感清新，適合隨時享用。', N'C2001.jpg', N'C2')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'C2002', N'蜂蜜糖果', 24.0000, N'天然蜂蜜糖果，口感香甜，保健又美味。', N'C2002.jpg', N'C2')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'C2003', N'無糖堅果', 69.0000, N'高蛋白無糖堅果，健康零食的最佳選擇。', N'C2003.jpg', N'C2')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'C3001', N'全麥麵包', 39.0000, N'新鮮出爐的全麥麵包，健康又美味。', N'C3001.jpg', N'C3')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'C3002', N'多穀麵包', 49.0000, N'富含多種穀物的麵包，健康又營養，適合早餐享用。', N'C3002.jpg', N'C3')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'D1001', N'巧克力餅乾', 29.0000, N'濃郁巧克力餅乾，外脆內軟，口感豐富。', N'C3001.jpg', N'D1')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'D1002', N'椰子餅乾', 29.0000, N'帶有椰香的酥脆餅乾，口感輕盈又香甜。', N'D1002.jpg', N'D1')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'E1001', N'高筋麵粉', 59.0000, N'高筋麵粉，製作麵包和餅乾的理想選擇。', N'E1001.jpg', N'E1')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'E2001', N'即食麥片', 99.0000, N'即食麥片，含有豐富的纖維和營養，適合快速早餐。', N'E2001.jpg', N'E2')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'F1001', N'豬肉脯', 79.0000, N'美味豬肉脯，嚼勁十足，帶有濃郁的肉香。', N'F1001.jpg', N'F1')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'G1001', N'無糖水果乾', 55.0000, N'自然乾燥的水果乾，無添加糖，健康又美味。', N'G1001.jpg', N'G1')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'G1002', N'芒果乾', 39.0000, N'天然乾燥芒果，甜美無比，適合作為小吃。', N'G1002.jpg', N'G1')
INSERT [dbo].[Product] ([ProductID], [ProductName], [Price], [Description], [Picture], [CateID]) VALUES (N'G2001', N'老豆腐', 19.0000, N'傳統風味的老豆腐，適合各種料理。', N'G2001.jpg', N'G2')
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [dbo].[OrderDetail] ADD  DEFAULT ((1)) FOR [Qty]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([CateID])
REFERENCES [dbo].[Category] ([CateID])
GO
/****** Object:  StoredProcedure [dbo].[InsertMemberData]    Script Date: 2025/2/20 上午 09:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[InsertMemberData]
	@memID nchar(6), @name nvarchar(27),
	@gender bit, @account nvarchar(12), @password nvarchar(20)
as
begin
	--將資料寫入前先檢查會員帳號(Account)是否存在
	declare @result  nchar(6)
	select @result = MemberID from [Member] where @account = Account
	if @result is null
		insert into [Member] values(@memID, @name, @gender, 0, @account, @password)
	else
		print '會員帳號重複'
	
end
GO
/****** Object:  StoredProcedure [dbo].[insertToMemberCheck]    Script Date: 2025/2/20 上午 09:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[insertToMemberCheck]
	@memID nchar(6), @Name nvarchar(50), @gender bit, @Acc nvarchar(12), @Ps nvarchar(20)
as
begin

	declare @tabAcc nvarchar(50)
	select @tabAcc = Account from [Member] where @Name = Account
	if @tabAcc is null
		insert into [Member] values(@memID, @Name, @gender, 0, @Acc, @Ps)
	else
		print '會員帳號已存在'

end
GO
USE [master]
GO
ALTER DATABASE [MyStore] SET  READ_WRITE 
GO
