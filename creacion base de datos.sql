USE [master]
GO

/****** Object:  Database [TributoDB]    Script Date: 30/07/2024 9:31:14 ******/
CREATE DATABASE [TributoDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TributoDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TributoDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TributoDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TributoDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
	EXEC [TributoDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [TributoDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [TributoDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [TributoDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [TributoDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [TributoDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [TributoDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [TributoDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [TributoDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [TributoDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [TributoDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [TributoDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [TributoDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [TributoDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [TributoDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [TributoDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [TributoDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [TributoDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [TributoDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [TributoDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [TributoDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [TributoDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [TributoDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [TributoDB] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [TributoDB] SET  MULTI_USER 
GO

ALTER DATABASE [TributoDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [TributoDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [TributoDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [TributoDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [TributoDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [TributoDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [TributoDB] SET QUERY_STORE = OFF
GO

ALTER DATABASE [TributoDB] SET  READ_WRITE 
GO

