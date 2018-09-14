USE [master]
GO
/****** Object:  Database [TicTacToe]    Script Date: 9/14/2018 1:52:10 PM ******/
CREATE DATABASE [TicTacToe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TicTacToe', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\TicTacToe.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TicTacToe_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\TicTacToe_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TicTacToe] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TicTacToe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TicTacToe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TicTacToe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TicTacToe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TicTacToe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TicTacToe] SET ARITHABORT OFF 
GO
ALTER DATABASE [TicTacToe] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TicTacToe] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [TicTacToe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TicTacToe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TicTacToe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TicTacToe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TicTacToe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TicTacToe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TicTacToe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TicTacToe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TicTacToe] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TicTacToe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TicTacToe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TicTacToe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TicTacToe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TicTacToe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TicTacToe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TicTacToe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TicTacToe] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TicTacToe] SET  MULTI_USER 
GO
ALTER DATABASE [TicTacToe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TicTacToe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TicTacToe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TicTacToe] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [TicTacToe]
GO
/****** Object:  Table [dbo].[Logging]    Script Date: 9/14/2018 1:52:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Logging](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequestType] [varchar](50) NOT NULL,
	[RequestStatus] [varchar](50) NOT NULL,
	[Exception] [varchar](50) NOT NULL,
	[Comment] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Logging] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TTTPlayers]    Script Date: 9/14/2018 1:52:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TTTPlayers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[AccessToken] [varchar](50) NOT NULL,
	[NumberString] [varchar](50) NOT NULL,
	[IsPlaying] [int] NOT NULL,
 CONSTRAINT [PK_TTTPlayers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [TicTacToe] SET  READ_WRITE 
GO
