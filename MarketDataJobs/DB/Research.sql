USE [master]
GO
/****** Object:  Database [Research]    Script Date: 2018/4/8 0:48:11 ******/
CREATE DATABASE [Research]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Research', FILENAME = N'D:\Microsoft SQL Server\MSSQL13.SKYWOLFDB\MSSQL\DATA\Research.mdf' , SIZE = 1253376KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Research_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL13.SKYWOLFDB\MSSQL\DATA\Research_log.ldf' , SIZE = 335872KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Research] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Research].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Research] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Research] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Research] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Research] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Research] SET ARITHABORT OFF 
GO
ALTER DATABASE [Research] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Research] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Research] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Research] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Research] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Research] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Research] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Research] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Research] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Research] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Research] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Research] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Research] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Research] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Research] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Research] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Research] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Research] SET RECOVERY FULL 
GO
ALTER DATABASE [Research] SET  MULTI_USER 
GO
ALTER DATABASE [Research] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Research] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Research] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Research] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Research] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Research', N'ON'
GO
ALTER DATABASE [Research] SET QUERY_STORE = OFF
GO
USE [Research]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Research]
GO
/****** Object:  User [liangshu]    Script Date: 2018/4/8 0:48:11 ******/
CREATE USER [liangshu] FOR LOGIN [liangshu] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Schema [sec]    Script Date: 2018/4/8 0:48:11 ******/
CREATE SCHEMA [sec]
GO
/****** Object:  Table [dbo].[bookvalueperoutstandingshares]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bookvalueperoutstandingshares](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cashandequivalentsgeneric]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cashandequivalentsgeneric](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[earningspershare1yearannualgrowth]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[earningspershare1yearannualgrowth](
	[Ticker] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[earningspershare5yearannualgrowth]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[earningspershare5yearannualgrowth](
	[Ticker] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[enterprisevalue]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[enterprisevalue](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[longtermdebt]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[longtermdebt](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[marketcapitalizationfiscalperiodend]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[marketcapitalizationfiscalperiodend](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[netincome1yearannualgrowth]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[netincome1yearannualgrowth](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[netincome5yearannualgrowth]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[netincome5yearannualgrowth](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[netsalesrevenues1yearannualgrowth]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[netsalesrevenues1yearannualgrowth](
	[Ticker] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[netsalesrevenues5yearannualgrowth]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[netsalesrevenues5yearannualgrowth](
	[Ticker] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[returnonassets]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[returnonassets](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[returnonequitytotalpercent]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[returnonequitytotalpercent](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[returnoninvestedcapital]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[returnoninvestedcapital](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[salespershare]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[salespershare](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shorttermdebtandcurrentportionoflongtermdebt]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shorttermdebtandcurrentportionoflongtermdebt](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[totalassets]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[totalassets](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[totalassets1yearannualgrowth]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[totalassets1yearannualgrowth](
	[Ticker] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[totalassets5yearannualgrowth]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[totalassets5yearannualgrowth](
	[Ticker] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ttmcashflowpershare]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ttmcashflowpershare](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ttmdividendspershare]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ttmdividendspershare](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ttmearningspershare]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ttmearningspershare](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ttmebitda]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ttmebitda](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ttmnetsalesrevenue]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ttmnetsalesrevenue](
	[Ticker] [varchar](50) NULL,
	[entity] [varchar](50) NULL,
	[Freq] [varchar](50) NULL,
	[Seq] [varchar](50) NULL,
	[Period_End_Date] [varchar](50) NULL,
	[Report_Date] [varchar](50) NULL,
	[Value] [varchar](50) NULL,
	[Currency_] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [sec].[Stock_Q500US_Members]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[Stock_Q500US_Members](
	[AsOfDate] [date] NOT NULL,
	[Ticker] [varchar](200) NOT NULL,
	[TS] [datetime] NULL,
 CONSTRAINT [PK_Stock_Q500US_Members] PRIMARY KEY CLUSTERED 
(
	[AsOfDate] DESC,
	[Ticker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sec].[Stock_USETFList_Nasdaq]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[Stock_USETFList_Nasdaq](
	[AsOfDate] [date] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[FullName] [varchar](200) NOT NULL,
	[LastPrice] [float] NULL,
	[NetChange] [float] NULL,
	[PercentChange] [float] NULL,
	[PercentChange1Y] [float] NULL,
	[TS] [datetime] NULL,
 CONSTRAINT [PK_Stock_USETFList] PRIMARY KEY CLUSTERED 
(
	[AsOfDate] DESC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sec].[Stock_USStockList_Nasdaq]    Script Date: 2018/4/8 0:48:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[Stock_USStockList_Nasdaq](
	[AsOfDate] [date] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[FullName] [varchar](200) NOT NULL,
	[LastPrice] [float] NULL,
	[MarketCap] [decimal](25, 7) NULL,
	[IPOyear] [int] NULL,
	[Sector] [varchar](200) NULL,
	[Industry] [varchar](200) NULL,
	[SummaryQuote] [varchar](200) NULL,
	[TS] [datetime] NULL,
 CONSTRAINT [PK_Stock_USStockList] PRIMARY KEY CLUSTERED 
(
	[AsOfDate] DESC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [sec].[Stock_Q500US_Members] ADD  CONSTRAINT [DF_Stock_Q500US_Members_TS]  DEFAULT (getutcdate()) FOR [TS]
GO
ALTER TABLE [sec].[Stock_USETFList_Nasdaq] ADD  CONSTRAINT [DF_Stock_USETFList_TS]  DEFAULT (getutcdate()) FOR [TS]
GO
ALTER TABLE [sec].[Stock_USStockList_Nasdaq] ADD  CONSTRAINT [DF_Stock_USStockListk_TS]  DEFAULT (getutcdate()) FOR [TS]
GO
USE [master]
GO
ALTER DATABASE [Research] SET  READ_WRITE 
GO
