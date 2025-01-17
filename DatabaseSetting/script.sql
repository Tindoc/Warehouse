USE [master]
GO
/****** Object:  Database [Warehouse]    Script Date: 2019/7/11 19:46:41 ******/
CREATE DATABASE [Warehouse]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Wareouse', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Wareouse.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Wareouse_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Wareouse_log.ldf' , SIZE = 3840KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Warehouse] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Warehouse].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [Warehouse] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Warehouse] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Warehouse] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Warehouse] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Warehouse] SET ARITHABORT OFF 
GO
ALTER DATABASE [Warehouse] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Warehouse] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Warehouse] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Warehouse] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Warehouse] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Warehouse] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Warehouse] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Warehouse] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Warehouse] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Warehouse] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Warehouse] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Warehouse] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Warehouse] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Warehouse] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Warehouse] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Warehouse] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Warehouse] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Warehouse] SET RECOVERY FULL 
GO
ALTER DATABASE [Warehouse] SET  MULTI_USER 
GO
ALTER DATABASE [Warehouse] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Warehouse] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Warehouse] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Warehouse] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Warehouse] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Warehouse', N'ON'
GO
ALTER DATABASE [Warehouse] SET QUERY_STORE = OFF
GO
USE [Warehouse]
GO
/****** Object:  Table [dbo].[Agent]    Script Date: 2019/7/11 19:46:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agent](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Contact] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Address] [varchar](200) NULL,
	[LevelName] [varchar](50) NULL,
	[Tel] [varchar](50) NULL,
	[Fox] [varchar](50) NULL,
 CONSTRAINT [PK_Agent] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Argument]    Script Date: 2019/7/11 19:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Argument](
	[ArgName] [varchar](50) NOT NULL,
	[ArgValue] [varchar](150) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InW]    Script Date: 2019/7/11 19:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InW](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Batch] [varchar](50) NOT NULL,
	[NormName] [varchar](50) NULL,
	[Barcode] [varchar](100) NULL,
	[CreateTime] [datetime] NULL,
	[Operator] [varchar](50) NULL,
	[Cnt] [int] NULL,
	[InTime] [datetime] NULL,
	[BigCnt] [int] NULL,
	[Machine] [int] NULL,
	[Length] [int] NULL,
	[Model] [varchar](50) NULL,
 CONSTRAINT [PK_InW] PRIMARY KEY CLUSTERED 
(
	[Batch] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InWDetail]    Script Date: 2019/7/11 19:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InWDetail](
	[BatchID] [varchar](50) NULL,
	[Barcode] [varchar](50) NULL,
	[NormName] [varchar](50) NULL,
	[Cnt] [int] NULL,
	[CreateTime] [datetime] NULL,
	[PrintCnt] [int] NULL,
	[Length] [int] NULL,
	[Model] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Level]    Script Date: 2019/7/11 19:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Level](
	[LevelID] [int] IDENTITY(1,1) NOT NULL,
	[LevelName] [varchar](50) NOT NULL,
	[Price] [money] NULL,
 CONSTRAINT [PK_Level] PRIMARY KEY CLUSTERED 
(
	[LevelName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LevelPrice]    Script Date: 2019/7/11 19:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LevelPrice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LevelName] [varchar](50) NOT NULL,
	[ApplyDate] [datetime] NOT NULL,
	[Price] [money] NULL,
	[Operator] [varchar](50) NULL,
	[OperatorTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Norm]    Script Date: 2019/7/11 19:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Norm](
	[NormID] [int] IDENTITY(1,1) NOT NULL,
	[NormName] [varchar](50) NULL,
 CONSTRAINT [PK_Norm] PRIMARY KEY CLUSTERED 
(
	[NormID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OutW]    Script Date: 2019/7/11 19:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OutW](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Batch] [varchar](50) NULL,
	[Agent] [varchar](50) NULL,
	[Barcode] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supply]    Script Date: 2019/7/11 19:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SupplyID] [varchar](50) NOT NULL,
	[AgentName] [varchar](50) NOT NULL,
	[Price] [money] NOT NULL,
	[SumPrice] [money] NOT NULL,
	[Operator] [varchar](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Supply] PRIMARY KEY CLUSTERED 
(
	[SupplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplyDetail]    Script Date: 2019/7/11 19:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplyDetail](
	[SupplyID] [varchar](50) NOT NULL,
	[Barcode] [varchar](50) NULL,
	[NormName] [varchar](50) NULL,
	[Price] [money] NULL,
	[SumMoney] [money] NULL,
	[Cnt] [int] NULL,
	[CreateTime] [datetime] NULL,
	[Length] [int] NULL,
	[Model] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2019/7/11 19:46:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[UserPwd] [varchar](50) NULL,
	[Position] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[InW] ADD  CONSTRAINT [DF_InW_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[InW] ADD  CONSTRAINT [DF_InW_InTime]  DEFAULT (getdate()) FOR [InTime]
GO
ALTER TABLE [dbo].[InWDetail] ADD  CONSTRAINT [DF_InwDetail_Cnt]  DEFAULT ((1)) FOR [Cnt]
GO
ALTER TABLE [dbo].[InWDetail] ADD  CONSTRAINT [DF_InwDetail_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[Supply] ADD  CONSTRAINT [DF_Supply_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[SupplyDetail] ADD  CONSTRAINT [DF_SupplyDetail_Cnt]  DEFAULT ((1)) FOR [Cnt]
GO
ALTER TABLE [dbo].[SupplyDetail] ADD  CONSTRAINT [DF_SupplyDetail_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
USE [master]
GO
ALTER DATABASE [Warehouse] SET  READ_WRITE 
GO
/* Insert Data to [dbo].[Argument] */
use [Warehouse]
GO
INSERT INTO [dbo].[User] (UserName, UserPwd, Position) VALUES ('admin', '123456', '系统管理员')
GO
INSERT INTO [dbo].[Argument] (ArgName, ArgValue) VALUES ('Name', NULL)
GO
INSERT INTO [dbo].[Argument] (ArgName, ArgValue) VALUES ('Phone', NULL)
GO
INSERT INTO [dbo].[Argument] (ArgName, ArgValue) VALUES ('Address', NULL)
GO
INSERT INTO [dbo].[Argument] (ArgName, ArgValue) VALUES ('GoodsName', NULL)
GO