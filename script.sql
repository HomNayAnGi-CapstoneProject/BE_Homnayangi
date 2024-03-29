USE [master]
GO
/****** Object:  Database [Homnayangi]    Script Date: 4/20/2023 10:30:43 AM ******/
CREATE DATABASE [Homnayangi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Homnayangi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MONKINAM\MSSQL\DATA\Homnayangi.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Homnayangi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MONKINAM\MSSQL\DATA\Homnayangi_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Homnayangi] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Homnayangi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Homnayangi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Homnayangi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Homnayangi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Homnayangi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Homnayangi] SET ARITHABORT OFF 
GO
ALTER DATABASE [Homnayangi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Homnayangi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Homnayangi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Homnayangi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Homnayangi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Homnayangi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Homnayangi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Homnayangi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Homnayangi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Homnayangi] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Homnayangi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Homnayangi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Homnayangi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Homnayangi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Homnayangi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Homnayangi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Homnayangi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Homnayangi] SET RECOVERY FULL 
GO
ALTER DATABASE [Homnayangi] SET  MULTI_USER 
GO
ALTER DATABASE [Homnayangi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Homnayangi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Homnayangi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Homnayangi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Homnayangi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Homnayangi] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Homnayangi', N'ON'
GO
ALTER DATABASE [Homnayangi] SET QUERY_STORE = OFF
GO
USE [Homnayangi]
GO
/****** Object:  Table [dbo].[Accomplishment]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accomplishment](
	[accomplishmentId] [uniqueidentifier] NOT NULL,
	[content] [nvarchar](max) NULL,
	[authorId] [uniqueidentifier] NULL,
	[createdDate] [datetime] NULL,
	[status] [int] NULL,
	[blogId] [uniqueidentifier] NULL,
	[confirmBy] [uniqueidentifier] NULL,
	[listVideoUrl] [nvarchar](max) NULL,
	[listImageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Accomplishment] PRIMARY KEY CLUSTERED 
(
	[accomplishmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccomplishmentReaction]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccomplishmentReaction](
	[accomplishmentId] [uniqueidentifier] NOT NULL,
	[customerId] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_AccomplishmentReaction] PRIMARY KEY CLUSTERED 
(
	[accomplishmentId] ASC,
	[customerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Badge]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Badge](
	[badgeId] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](max) NULL,
	[createDate] [datetime] NULL,
	[imageURL] [nvarchar](max) NULL,
	[status] [int] NULL,
	[voucherId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Reward] PRIMARY KEY CLUSTERED 
(
	[badgeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BadgeCondition]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BadgeCondition](
	[badgeConditionId] [uniqueidentifier] NOT NULL,
	[accomplishments] [int] NULL,
	[orders] [int] NULL,
	[createdDate] [datetime] NULL,
	[status] [bit] NULL,
	[badgeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BadgeCondition] PRIMARY KEY CLUSTERED 
(
	[badgeConditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[blogId] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](max) NULL,
	[imageURL] [nvarchar](max) NULL,
	[createdDate] [datetime] NULL,
	[updatedDate] [datetime] NULL,
	[reaction] [int] NULL,
	[view] [int] NULL,
	[authorId] [uniqueidentifier] NULL,
	[blogStatus] [int] NULL,
	[videoURL] [nvarchar](max) NULL,
	[recipeId] [uniqueidentifier] NULL,
	[minutesToCook] [int] NULL,
	[isEvent] [bit] NULL,
	[eventExpiredDate] [datetime] NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[blogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogReaction]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogReaction](
	[blogId] [uniqueidentifier] NOT NULL,
	[customerId] [uniqueidentifier] NOT NULL,
	[status] [bit] NULL,
	[createdDate] [datetime] NULL,
 CONSTRAINT [PK_BlogReaction] PRIMARY KEY CLUSTERED 
(
	[blogId] ASC,
	[customerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogReference]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogReference](
	[blogReferenceId] [uniqueidentifier] NOT NULL,
	[text] [nvarchar](max) NULL,
	[html] [nvarchar](max) NULL,
	[type] [int] NOT NULL,
	[blogId] [uniqueidentifier] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_BlogReference] PRIMARY KEY CLUSTERED 
(
	[blogReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogSubCate]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogSubCate](
	[blogId] [uniqueidentifier] NOT NULL,
	[subCateId] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_BlogSubCate] PRIMARY KEY CLUSTERED 
(
	[blogId] ASC,
	[subCateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CaloReference]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CaloReference](
	[caloReferenceId] [uniqueidentifier] NOT NULL,
	[fromAge] [int] NULL,
	[toAge] [int] NULL,
	[calo] [int] NULL,
	[isMale] [bit] NULL,
 CONSTRAINT [PK_CaloReference] PRIMARY KEY CLUSTERED 
(
	[caloReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[categoryId] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](max) NULL,
	[description] [nvarchar](max) NULL,
	[status] [bit] NULL,
	[createdDate] [datetime] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[commentId] [uniqueidentifier] NOT NULL,
	[authorId] [uniqueidentifier] NULL,
	[createdDate] [datetime] NULL,
	[content] [nvarchar](max) NULL,
	[status] [bit] NULL,
	[parentId] [uniqueidentifier] NULL,
	[blogId] [uniqueidentifier] NULL,
	[byStaff] [bit] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[commentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CronJobTimeConfig]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CronJobTimeConfig](
	[cronJobTimeConfigId] [uniqueidentifier] NOT NULL,
	[minute] [int] NULL,
	[hour] [int] NULL,
	[day] [int] NULL,
	[month] [int] NULL,
	[createdDate] [datetime] NULL,
	[updatedDate] [datetime] NULL,
	[targetObject] [int] NULL,
 CONSTRAINT [PK_CronJobTimeConfig] PRIMARY KEY CLUSTERED 
(
	[cronJobTimeConfigId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[customerId] [uniqueidentifier] NOT NULL,
	[displayname] [nvarchar](50) NULL,
	[username] [nvarchar](50) NULL,
	[firstname] [nvarchar](50) NULL,
	[lastname] [nvarchar](max) NULL,
	[email] [nvarchar](max) NULL,
	[password] [nvarchar](max) NULL,
	[phonenumber] [nvarchar](max) NULL,
	[gender] [int] NULL,
	[avatar] [nvarchar](max) NULL,
	[createdDate] [datetime] NULL,
	[updatedDate] [datetime] NULL,
	[isBlocked] [bit] NULL,
	[isGoogle] [bit] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[customerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerBadge]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerBadge](
	[customerId] [uniqueidentifier] NOT NULL,
	[badgeId] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NULL,
 CONSTRAINT [PK_CustomerReward] PRIMARY KEY CLUSTERED 
(
	[customerId] ASC,
	[badgeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerVoucher]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerVoucher](
	[customerVoucherId] [uniqueidentifier] NOT NULL,
	[voucherId] [uniqueidentifier] NOT NULL,
	[customerId] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NULL,
 CONSTRAINT [PK_CustomerVoucher] PRIMARY KEY CLUSTERED 
(
	[customerVoucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredient]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredient](
	[ingredientId] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](max) NULL,
	[description] [nvarchar](max) NULL,
	[unitId] [uniqueidentifier] NULL,
	[quantity] [int] NULL,
	[picture] [nvarchar](max) NULL,
	[kcal] [int] NULL,
	[createdDate] [datetime] NULL,
	[updatedDate] [datetime] NULL,
	[status] [bit] NULL,
	[price] [money] NULL,
	[listImage] [nvarchar](max) NULL,
	[typeId] [uniqueidentifier] NULL,
	[listImagePosition] [nvarchar](max) NULL,
 CONSTRAINT [PK_Ingredient] PRIMARY KEY CLUSTERED 
(
	[ingredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[notificationId] [uniqueidentifier] NOT NULL,
	[description] [nvarchar](max) NULL,
	[createdDate] [datetime] NULL,
	[status] [bit] NULL,
	[receiverId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[notificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[orderId] [uniqueidentifier] NOT NULL,
	[orderDate] [datetime] NULL,
	[shippedDate] [datetime] NULL,
	[shippedAddress] [nvarchar](max) NULL,
	[totalPrice] [money] NULL,
	[orderStatus] [int] NULL,
	[customerId] [uniqueidentifier] NULL,
	[isCooked] [bit] NULL,
	[voucherId] [uniqueidentifier] NULL,
	[paymentMethod] [int] NULL,
	[paypalUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[orderDetailId] [uniqueidentifier] NOT NULL,
	[orderId] [uniqueidentifier] NOT NULL,
	[ingredientId] [uniqueidentifier] NOT NULL,
	[quantity] [int] NULL,
	[recipeId] [uniqueidentifier] NULL,
	[price] [money] NULL,
 CONSTRAINT [PK_OrderIngredientDetail] PRIMARY KEY CLUSTERED 
(
	[orderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceNote]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceNote](
	[priceNoteId] [uniqueidentifier] NOT NULL,
	[ingredientId] [uniqueidentifier] NULL,
	[price] [money] NULL,
	[dateApply] [datetime] NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_PriceNote] PRIMARY KEY CLUSTERED 
(
	[priceNoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipe]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipe](
	[recipeId] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](max) NULL,
	[imageURL] [nvarchar](max) NULL,
	[packagePrice] [money] NULL,
	[cookedPrice] [money] NULL,
	[minSize] [int] NULL,
	[maxSize] [int] NULL,
	[totalKcal] [int] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED 
(
	[recipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeDetail]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeDetail](
	[recipeId] [uniqueidentifier] NOT NULL,
	[ingredientId] [uniqueidentifier] NOT NULL,
	[description] [nvarchar](max) NULL,
	[quantity] [int] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_RecipeDetail] PRIMARY KEY CLUSTERED 
(
	[recipeId] ASC,
	[ingredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeasonReference]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeasonReference](
	[seasonReferenceId] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](150) NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_SeasonReference] PRIMARY KEY CLUSTERED 
(
	[seasonReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategory](
	[subCategoryId] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](max) NULL,
	[description] [nvarchar](max) NULL,
	[createdDate] [datetime] NULL,
	[status] [bit] NULL,
	[categoryId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[subCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[transactionId] [uniqueidentifier] NOT NULL,
	[totalAmount] [money] NULL,
	[createdDate] [datetime] NULL,
	[transactionStatus] [int] NULL,
	[orderId] [uniqueidentifier] NULL,
	[customerId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[transactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[typeId] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](max) NULL,
	[description] [nvarchar](max) NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[typeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[unitId] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](max) NULL,
	[createdDate] [datetime] NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED 
(
	[unitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userId] [uniqueidentifier] NOT NULL,
	[displayname] [nvarchar](50) NULL,
	[username] [nvarchar](max) NULL,
	[firstname] [nvarchar](50) NULL,
	[lastname] [nvarchar](max) NULL,
	[email] [nvarchar](max) NULL,
	[password] [nvarchar](max) NULL,
	[phonenumber] [nvarchar](max) NULL,
	[gender] [int] NULL,
	[avatar] [nvarchar](max) NULL,
	[role] [int] NULL,
	[createdDate] [datetime] NULL,
	[updatedDate] [datetime] NULL,
	[isBlocked] [bit] NULL,
	[isGoogle] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 4/20/2023 10:30:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher](
	[voucherId] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NULL,
	[status] [int] NULL,
	[createdDate] [datetime] NOT NULL,
	[validFrom] [datetime] NULL,
	[validTo] [datetime] NULL,
	[discount] [money] NULL,
	[minimumOrderPrice] [money] NULL,
	[maximumOrderPrice] [money] NULL,
	[authorId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
(
	[voucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Accomplishment] ([accomplishmentId], [content], [authorId], [createdDate], [status], [blogId], [confirmBy], [listVideoUrl], [listImageUrl]) VALUES (N'b43cbb08-cff0-42b5-b8b6-0fb9a59de0d8', N'tuyệt vời', N'89fcfb56-be89-442d-9b99-8f498023a5cc', CAST(N'2023-04-08T11:12:43.363' AS DateTime), 3, N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', NULL, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/accomplishments%2Fcach-lam-muc-xao-sa-te-sieu-ngon-ma-don-gian%2F89fcfb56-be89-442d-9b99-8f498023a5cc%2FtestVideo2.mp4ed8c00b0-062c-4246-bdc0-21cc0fb9ac8e?alt=media&token=fb86c439-fefc-4c50-9bca-dd280c2c14b5', N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/accomplishments%2Fcach-lam-muc-xao-sa-te-sieu-ngon-ma-don-gian%2F89fcfb56-be89-442d-9b99-8f498023a5cc%2Fcanh-chua-tom.jpg78e4c51f-fc1b-4050-8731-50cd19d612a6?alt=media&token=5f7fa93f-ac43-43a0-8e7c-f18d5beeefad;https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/accomplishments%2Fcach-lam-muc-xao-sa-te-sieu-ngon-ma-don-gian%2F89fcfb56-be89-442d-9b99-8f498023a5cc%2Fabout_2.jpg11c6bd73-291d-4bbb-9cdf-f05c90fd1dea?alt=media&token=c3c2675e-2eeb-4033-ad44-9127151b9e4a')
GO
INSERT [dbo].[Badge] ([badgeId], [name], [description], [createDate], [imageURL], [status], [voucherId]) VALUES (N'1893ab75-ac06-436f-8b77-27ac5aa4d8a7', N'Làm quen', N'danh hiệu làm quen', CAST(N'2023-04-08T10:54:41.600' AS DateTime), N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/badges%2F9269b795-016f-4666-9823-d18758163b69%2Fbadge2.png65bdad93-1ae1-45df-b9f5-078a83840c87?alt=media&token=e3094590-5697-46a7-a255-def57431ff44', 1, NULL)
INSERT [dbo].[Badge] ([badgeId], [name], [description], [createDate], [imageURL], [status], [voucherId]) VALUES (N'a2f16014-5afb-4e1d-8289-494e179d1e88', N'Khách quý', N'danh hiệu khách quý', CAST(N'2023-04-08T10:55:15.167' AS DateTime), N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/badges%2F4553eb20-07f4-4170-9564-e27ded7daf3f%2Fbadge4.pngf6c38938-076d-4e8e-a431-27763c2c68c2?alt=media&token=629d336d-e80f-40d7-b2ec-e513697c9e7e', 1, NULL)
INSERT [dbo].[Badge] ([badgeId], [name], [description], [createDate], [imageURL], [status], [voucherId]) VALUES (N'ba432563-e3b7-4472-b91b-a833c5825042', N'Hội viên', N'danh hiệu hội viên', CAST(N'2023-04-08T10:54:57.513' AS DateTime), N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/badges%2Fa8a08f7f-bd30-4d4f-93bf-b85b245de422%2Fbadge3.pnged7d6bf9-0972-468a-b121-33774a426aca?alt=media&token=54376b22-b4fb-4396-af94-7e521c246239', 1, NULL)
INSERT [dbo].[Badge] ([badgeId], [name], [description], [createDate], [imageURL], [status], [voucherId]) VALUES (N'46cabf39-a93e-4703-bdd8-bf06aa66adf2', N'Khởi đầu', N'danh hiệu khởi đầu', CAST(N'2023-04-08T10:53:22.333' AS DateTime), N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/badges%2Ff1324ee7-cb66-4a77-a8c1-5cd1d04ec530%2Fbadge1.pngf23df742-0e56-4e30-b5db-e5f25c7e42a6?alt=media&token=d32f6b72-d191-4b0d-beb6-69e41e63c0fc', 1, NULL)
INSERT [dbo].[Badge] ([badgeId], [name], [description], [createDate], [imageURL], [status], [voucherId]) VALUES (N'6e9aded4-932a-41ce-8eed-e45d93f612e2', N'Lão làng', N'danh hiệu lão làng', CAST(N'2023-04-08T10:55:27.927' AS DateTime), N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/badges%2F95e30a37-c845-40e5-b960-1700d3fc17f5%2Fbadge5.pngfc10e7a1-546e-456e-a1d8-641fbd0df681?alt=media&token=f9b23abd-fc53-4fa9-af95-bf1531b37aac', 1, NULL)
INSERT [dbo].[Badge] ([badgeId], [name], [description], [createDate], [imageURL], [status], [voucherId]) VALUES (N'8bfb7423-4f71-413b-a3c4-f2c8d670f847', N'Chuyên gia mua sắm', N'danh hiệu chuyên gia mua sắm', CAST(N'2023-04-08T10:55:42.950' AS DateTime), N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/badges%2Fa557c27b-f5ca-4ff5-b45a-c4b94f79fb9f%2Fbadge6.png39bf9924-6e1c-451b-874a-8c2e35f8f222?alt=media&token=58aaa243-bd47-4c7f-a3aa-3428d5e97b98', 1, NULL)
GO
INSERT [dbo].[BadgeCondition] ([badgeConditionId], [accomplishments], [orders], [createdDate], [status], [badgeId]) VALUES (N'e29d0091-32ce-4088-aa8a-1e5135ade6b0', 5, 20, CAST(N'2023-04-08T11:10:47.503' AS DateTime), 1, N'ba432563-e3b7-4472-b91b-a833c5825042')
INSERT [dbo].[BadgeCondition] ([badgeConditionId], [accomplishments], [orders], [createdDate], [status], [badgeId]) VALUES (N'0d1866a4-9e0c-4dda-9870-45f019de1dc1', 0, 2, CAST(N'2023-04-08T11:09:55.677' AS DateTime), 1, N'46cabf39-a93e-4703-bdd8-bf06aa66adf2')
INSERT [dbo].[BadgeCondition] ([badgeConditionId], [accomplishments], [orders], [createdDate], [status], [badgeId]) VALUES (N'c0b78014-6eaf-416b-a053-7a83199630f8', 7, 30, CAST(N'2023-04-08T11:11:00.423' AS DateTime), 1, N'a2f16014-5afb-4e1d-8289-494e179d1e88')
INSERT [dbo].[BadgeCondition] ([badgeConditionId], [accomplishments], [orders], [createdDate], [status], [badgeId]) VALUES (N'f5fd2df3-1608-4a78-a663-d04d060efdd0', 10, 40, CAST(N'2023-04-08T11:11:24.777' AS DateTime), 1, N'6e9aded4-932a-41ce-8eed-e45d93f612e2')
INSERT [dbo].[BadgeCondition] ([badgeConditionId], [accomplishments], [orders], [createdDate], [status], [badgeId]) VALUES (N'a29a96e0-1d2e-4484-abb1-d610c7567688', 0, 10, CAST(N'2023-04-08T11:10:39.540' AS DateTime), 1, N'1893ab75-ac06-436f-8b77-27ac5aa4d8a7')
INSERT [dbo].[BadgeCondition] ([badgeConditionId], [accomplishments], [orders], [createdDate], [status], [badgeId]) VALUES (N'0128a851-4761-4b23-a33a-e0935fddc4e0', 20, 50, CAST(N'2023-04-08T11:11:32.187' AS DateTime), 1, N'8bfb7423-4f71-413b-a3c4-f2c8d670f847')
GO
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', N'Cách làm mì ý sốt bò bằm ngon tại nhà 🥘🥘', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_215304133.pngb7cff8cd-7d1d-47af-b5ff-84a8e59ae23e?alt=media&token=7fdf9aa6-faf6-4f65-99d1-07fd572fec21', CAST(N'2023-02-27T21:52:41.743' AS DateTime), CAST(N'2023-02-28T15:38:27.760' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', 30, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'5b579874-46a8-4895-bad2-1f33d4cb006a', N'Cách nấu canh bí đỏ với tôm thanh ngọt, tăng cường sức khỏe 😍😍😍', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163207623.pngc4084603-19db-4d3e-badb-4b9a416130f4?alt=media&token=4c544174-9d5e-4d50-9c08-23b48fc180b5', CAST(N'2023-02-27T16:32:00.197' AS DateTime), CAST(N'2023-02-28T15:47:13.667' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'5b579874-46a8-4895-bad2-1f33d4cb006a', 60, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'ac41da30-8998-4a1f-b2e4-291d0b24dcf4', N'Cách làm bông bí xào tỏi xanh mướt như ngoài hàng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fac41da30-8998-4a1f-b2e4-291d0b24dcf4%2Fimage_2023-03-03_233436189.pngcb39a877-2687-471b-912f-2b49c455fcf0?alt=media&token=00c67bdf-860e-40fe-92a8-50707699f75c', CAST(N'2023-03-03T23:33:46.617' AS DateTime), CAST(N'2023-03-03T23:36:52.213' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'ac41da30-8998-4a1f-b2e4-291d0b24dcf4', 60, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'b5603c80-4003-41f9-a7a5-3076de517599', N'Cách làm mì xào rau muống siêu hấp dẫn, ăn giòn sần sật', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_220834895.pngc579bfe3-a2d6-47fb-b4a2-c00f44d1685e?alt=media&token=f9070137-12ee-40af-ad2a-6916697ff9cd', CAST(N'2023-02-27T22:08:28.647' AS DateTime), CAST(N'2023-02-28T15:38:49.387' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'b5603c80-4003-41f9-a7a5-3076de517599', 60, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', N'Cách làm món sườn kho thơm lạ miệng, ngon quên sầu', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F3ef7c4e0-b1ac-4506-9556-3541afd421fe%2Fimage_2023-02-28_155658710.png7554bd97-20ff-4807-accb-39871d10816a?alt=media&token=a15bffbe-5ed3-43b3-a712-674762c19106', CAST(N'2023-02-28T15:51:11.687' AS DateTime), CAST(N'2023-03-03T16:22:53.717' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, N'https://www.youtube.com/watch?v=5l-4sVrXnoA', N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', 40, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', N'Cách làm sườn xào chua ngọt trong 30 phút (cách mới)', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fcc8da8e0-0c9d-4893-8ea1-387c0519a0f3%2Fimage_2023-02-27_170936652.png394c2734-1f4b-4798-83a7-30d46a2928f4?alt=media&token=c632a627-2afc-4941-b33e-215dd558aedb', CAST(N'2023-02-27T17:09:30.823' AS DateTime), CAST(N'2023-02-28T15:38:05.537' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', 45, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', N'Cách làm thịt kho mắm ruốc sả ớt thơm ngon đậm đà', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage_2023-02-28_160422598.png2e06cf2d-c0bd-47a4-b2d7-1e47f6efa89a?alt=media&token=3ffed372-d387-411a-8c26-c70651ed166d', CAST(N'2023-02-28T16:04:04.187' AS DateTime), CAST(N'2023-02-28T21:21:53.987' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', 50, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', N'Cách làm salad rau mầm, ăn ngon không đắng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fae2bf83d-2b03-4a44-ac29-4c41246cc355%2Fimage_2023-03-03_232608815.png6ded854b-93cb-4498-a4bb-e01ab43ec198?alt=media&token=1429f1eb-d515-47df-abbd-bba9f0cbefc2', CAST(N'2023-03-03T23:25:20.807' AS DateTime), CAST(N'2023-03-03T23:32:01.610' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', 30, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', N'Cách làm nộm tai heo với công thức pha nước trộn nộm chuẩn', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage_2023-02-28_124336275.png321d2377-5234-4ef5-917b-0d094eaa1d81?alt=media&token=67e45ddc-88ea-433d-8f45-56908d13ad40', CAST(N'2023-02-28T12:42:53.070' AS DateTime), CAST(N'2023-02-28T15:39:15.240' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', 60, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'5de76825-4f4e-469d-b94e-5e457294b045', N'Cách nấu canh khoai mỡ trắng với tôm tươi ngon bổ 😊', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5de76825-4f4e-469d-b94e-5e457294b045%2Fimage_2023-02-27_162358233.png8d401f60-8477-4961-a67d-5b8174d5f923?alt=media&token=e48424c8-884a-402e-90d3-b24fa32c5d4d', CAST(N'2023-02-27T16:23:33.550' AS DateTime), CAST(N'2023-02-28T15:39:24.313' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'5de76825-4f4e-469d-b94e-5e457294b045', 60, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'Cách làm nem rán miền Bắc giòn rụm, vàng đều, không bị ỉu', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage_2023-02-28_111644581.pngf41b9372-3237-49b6-8878-549a094ee6f1?alt=media&token=e094a57a-0fd7-4b75-a5a6-deb81dc81341', CAST(N'2023-02-28T11:16:06.077' AS DateTime), CAST(N'2023-02-28T15:40:11.757' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', 30, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'f578682a-d641-4a0b-aa54-6a81eaf7be08', N'Cách làm thịt kho tiêu đơn giản ngon đậm đà', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_225457525.png0bcfca79-ffba-4393-a0b0-8dc28cc30593?alt=media&token=ce3d9940-8080-431b-98a8-f3c03417bc4b', CAST(N'2023-02-27T22:39:26.437' AS DateTime), CAST(N'2023-02-28T15:40:18.120' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'f578682a-d641-4a0b-aa54-6a81eaf7be08', 40, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'956f6fc0-72e1-4204-852b-73cff12eae37', N'Cách làm bánh ngào mật mía Nghệ An', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F956f6fc0-72e1-4204-852b-73cff12eae37%2Fimage_2023-02-28_172432113.png250851e7-28b1-477f-9663-034e436b290c?alt=media&token=7a42f319-2612-4503-abe9-842bf629c5a3', CAST(N'2023-02-28T17:24:07.240' AS DateTime), CAST(N'2023-02-28T17:27:22.987' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'956f6fc0-72e1-4204-852b-73cff12eae37', 45, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'b9f39ecc-c689-4910-ac82-816e20994156', N'Cách làm cá lóc kho tiêu, nghệ và lá gừng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_212437842.png8464529a-8f47-4c32-b489-cf74fc5b99f9?alt=media&token=289ed741-6bef-4cf6-b997-444c0ed1620f', CAST(N'2023-02-27T21:23:13.850' AS DateTime), CAST(N'2023-02-28T15:40:24.730' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'b9f39ecc-c689-4910-ac82-816e20994156', 60, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'047935c2-9a60-4b0e-865a-89c5e1a680e5', N'Cách làm món cà pháo mắm tôm ngon, ăn là ghiền', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F047935c2-9a60-4b0e-865a-89c5e1a680e5%2Fimage_2023-02-28_171556392.png51311fd1-cf08-48bf-b7b0-b0857f09b971?alt=media&token=d0179eba-23e9-40e4-8c6a-47078bb79d79', CAST(N'2023-02-28T17:15:31.637' AS DateTime), CAST(N'2023-02-28T17:20:01.343' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'047935c2-9a60-4b0e-865a-89c5e1a680e5', 30, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', N'Cách làm mực xào sa tế siêu ngon mà đơn giản', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage_2023-02-28_170532772.png8ff2b7e0-f648-42b1-a587-d25ade0485b1?alt=media&token=16a1a0d3-6ebe-4f49-a06b-04d4aa8c80ea', CAST(N'2023-02-28T17:05:03.617' AS DateTime), CAST(N'2023-02-28T17:10:01.950' AS DateTime), 0, 3, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', 35, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', N'Cách làm salad rau trộn sốt mè rang cho người giảm cân', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234%2Fimage_2023-02-28_173749843.png52ed5779-daf6-494a-913c-4fe817dc8d0b?alt=media&token=bbaf1262-a0ab-4713-b62a-870a05d13360', CAST(N'2023-02-28T17:37:05.963' AS DateTime), CAST(N'2023-03-03T18:19:26.823' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', 35, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', N'Cách làm kho quẹt tôm khô chấm rau củ luộc ngon bá cháy', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f%2Fimage_2023-02-28_104036431.pngf3c9441f-52a5-4540-9dfe-ea8a46fa560e?alt=media&token=b1867ae8-75b7-45c1-8846-a07a060e9398', CAST(N'2023-02-28T10:40:25.150' AS DateTime), CAST(N'2023-02-28T15:40:51.210' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', 45, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'a8ab27eb-4401-4b4b-93b9-bda34397621f', N'Cách nấu thịt kho tàu ngon bá cháy 😤', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_180748893.png22240233-c47a-49ec-a87e-a9a739903619?alt=media&token=58908dea-18e9-4c4b-9ddd-99acc1a19e81', CAST(N'2023-02-27T18:06:56.163' AS DateTime), CAST(N'2023-02-28T15:41:35.723' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'a8ab27eb-4401-4b4b-93b9-bda34397621f', 30, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'e3f6c554-29c7-460d-a692-d23d15dae638', N'Cách làm gà xào sả ớt ngon chuẩn vị', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110346984.pngee39ba8b-f8a0-4985-91f9-ab191d3503ee?alt=media&token=275087bf-4963-4178-ac3a-95d8a8216bf5', CAST(N'2023-02-28T11:03:14.827' AS DateTime), CAST(N'2023-02-28T15:41:41.557' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'e3f6c554-29c7-460d-a692-d23d15dae638', 60, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', N'Cách nấu canh chua cá lóc miền Tây Nam Bộ 🍴', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F0458adf1-68ce-44b1-89b1-f069b44a3ee2%2Fimage_2023-02-27_165412388.png3115e5dd-4342-448a-94d5-200214246041?alt=media&token=0298552b-30c0-4f12-b5b2-052e9faf6009', CAST(N'2023-02-27T16:37:28.650' AS DateTime), CAST(N'2023-02-28T15:42:03.360' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', 60, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'346dfff7-d030-408f-b521-f4b15b139bd3', N'Cách làm khổ qua xào trứng không bị đắng 😉', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F346dfff7-d030-408f-b521-f4b15b139bd3%2Fimage_2023-02-27_174804334.png80da20c5-ab7c-4d7f-8f32-8df02445e04f?alt=media&token=98a34829-03d4-4347-88be-8b66c0c2cf4f', CAST(N'2023-02-27T17:47:58.367' AS DateTime), CAST(N'2023-02-28T15:42:08.737' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'346dfff7-d030-408f-b521-f4b15b139bd3', 60, NULL, NULL)
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId], [minutesToCook], [isEvent], [eventExpiredDate]) VALUES (N'dc7234fa-21fd-45bc-9612-f4fb5e2f4f8b', N'Cách làm rau muống xào tỏi xanh giòn thơm phức', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fdc7234fa-21fd-45bc-9612-f4fb5e2f4f8b%2Fimage_2023-02-27_173736886.pngf71ed7f6-3fa0-40ec-8a60-39a5cd816501?alt=media&token=8f44c238-3659-499f-8923-db2e971585f2', CAST(N'2023-02-27T17:37:27.223' AS DateTime), CAST(N'2023-02-28T15:42:14.567' AS DateTime), 0, 0, N'749f5b3b-dea1-49a4-98b8-96da197d123f', 1, NULL, N'dc7234fa-21fd-45bc-9612-f4fb5e2f4f8b', 30, NULL, NULL)
GO
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'71295ec8-abdf-4202-98fa-0034a0bb88bf', N'
## Nấu canh khoai mỡ

![image_2023-02-27_162703258.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5de76825-4f4e-469d-b94e-5e457294b045%2Fimage_2023-02-27_162703258.png57a456c3-27d6-4c6b-957e-dffbd78ba7ec?alt=media&token=5df988e0-164b-4ccd-a735-739bbb45c02c)

![image_2023-02-27_162721248.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5de76825-4f4e-469d-b94e-5e457294b045%2Fimage_2023-02-27_162721248.pnga4029a97-977a-45f3-b771-9b618e45784a?alt=media&token=5532e00b-13fc-4f86-8fe8-483e9e11e938)

![image_2023-02-27_162730707.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5de76825-4f4e-469d-b94e-5e457294b045%2Fimage_2023-02-27_162730707.png3dce0847-0698-4d99-8810-3a299ce2356c?alt=media&token=61b897d7-4a8c-4481-808a-12e0a9a4a958)


Xào khoai mỡ và tôm thêm khoảng 2 phút thì đổ nước lọc đầu tôm vào, chế thêm nước lã để vừa với lượng người ăn. Tăng nhiệt độ để đun sôi canh.

Khi nước canh bùng sôi thì nêm gia vị gồm bột canh, mì chính/bột nêm và có thể thêm chút nước mắm. Tùy vào khẩu vị ăn nhạt/mặn mà các bạn điều chỉnh gia vị cho phù hợp.

Ninh thêm khoảng 10 phút để khoai mỡ chín mềm, nếu muốn ăn khoai nhuyễn có thể tăng thời gian ninh lâu hơn 1 chút. Còn nếu muốn ăn khoai mỡ nguyên miếng mà vẫn mềm thì chỉ cần ninh thêm khoảng 10 phút là được.

**Lưu ý trong quá trình nấu canh, để nước canh được trong thì nên hớt bọt thường xuyên.**', N'<h2>Nấu canh khoai mỡ</h2>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5de76825-4f4e-469d-b94e-5e457294b045%2Fimage_2023-02-27_162703258.png57a456c3-27d6-4c6b-957e-dffbd78ba7ec?alt=media&amp;token=5df988e0-164b-4ccd-a735-739bbb45c02c" alt="image_2023-02-27_162703258.png"></p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5de76825-4f4e-469d-b94e-5e457294b045%2Fimage_2023-02-27_162721248.pnga4029a97-977a-45f3-b771-9b618e45784a?alt=media&amp;token=5532e00b-13fc-4f86-8fe8-483e9e11e938" alt="image_2023-02-27_162721248.png"></p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5de76825-4f4e-469d-b94e-5e457294b045%2Fimage_2023-02-27_162730707.png3dce0847-0698-4d99-8810-3a299ce2356c?alt=media&amp;token=61b897d7-4a8c-4481-808a-12e0a9a4a958" alt="image_2023-02-27_162730707.png"></p>
<p>Xào khoai mỡ và tôm thêm khoảng 2 phút thì đổ nước lọc đầu tôm vào, chế thêm nước lã để vừa với lượng người ăn. Tăng nhiệt độ để đun sôi canh.</p>
<p>Khi nước canh bùng sôi thì nêm gia vị gồm bột canh, mì chính/bột nêm và có thể thêm chút nước mắm. Tùy vào khẩu vị ăn nhạt/mặn mà các bạn điều chỉnh gia vị cho phù hợp.</p>
<p>Ninh thêm khoảng 10 phút để khoai mỡ chín mềm, nếu muốn ăn khoai nhuyễn có thể tăng thời gian ninh lâu hơn 1 chút. Còn nếu muốn ăn khoai mỡ nguyên miếng mà vẫn mềm thì chỉ cần ninh thêm khoảng 10 phút là được.</p>
<p><strong>Lưu ý trong quá trình nấu canh, để nước canh được trong thì nên hớt bọt thường xuyên.</strong></p>
', 2, N'5de76825-4f4e-469d-b94e-5e457294b045', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'ab5e6772-0d63-4f4f-b69f-0042e2902a12', N'Hoặc nếu không biết mổ cá, bạn có thể nhờ hàng cá sơ chế rồi rửa sạch lại với muối hạt và nước. Lưu ý nhớ bỏ hết phần màng đen bám ở bụng cá để thịt cá thơm ngon hơn.

Đối với món canh chua cá lóc cho 3-4 người ăn, có thể chỉ dùng 3 khúc cá, phần còn lại có thể bảo quản trong tủ lạnh để chế biến cho lần nấu tiếp theo. Cá sau khi rửa sạch, cho vào tô ướp cùng với 1 chút bột canh và 1 ít ớt tươi băm nhỏ.

![image_2023-02-27_165615553.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F0458adf1-68ce-44b1-89b1-f069b44a3ee2%2Fimage_2023-02-27_165615553.png7008abd1-5913-49fa-829c-0dfe74dd45b8?alt=media&token=97c91939-fb17-4c96-bcc6-dee93884d6c9)

Cà chua rửa sạch, bổ múi cau.

Dứa gọt vỏ, bỏ mắt, cắt phần đầu và đuôi sau đó rửa sạch, bổ dọc thân dứa làm 4, gọt bỏ phần lõi cứng bên trong và thái dứa thành từng miếng hình tam giác. Tiếp đến ướp dứa với 1 chút đường và muối để làm dịu đi vị chua của dứa, giúp miếng dứa đậm vị hơn.

Giá đỗ ngâm qua với nước muối, rửa sạch rồi vớt ra để ráo nước.

Dọc mùng (bạc hà) tước vỏ, cắt chéo khúc khoảng 3-4cm, sau đó ngâm vào tô nước đá có pha chút muối để tiêu vị ngứa nếu có, giúp miếng dọc mùng dòn hơn, và không bị thâm.

Đậu bắp cắt bỏ phần ngọn và cuống, rửa sạch, xắt khúc giống như bạc hà và ngâm vào tô nước pha muối loãng để khử bớt chất nhớt. Dọc mùng và đậu bắp ngâm khoảng 10 phút thì vớt ra, để ráo.

![image_2023-02-27_165632198.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F0458adf1-68ce-44b1-89b1-f069b44a3ee2%2Fimage_2023-02-27_165632198.png7860399b-45b6-480d-af79-83e30b7e2610?alt=media&token=19d0c5ed-c605-415f-93a4-5bc82beff307)

Sả bóc bỏ phần bẹ già bên ngoài, rửa sạch rồi băm nhỏ.

Rau om và ngò gai nhặt bỏ lá vàng và phần thân già, rửa sạch, xắt nhỏ.

Tỏi bóc vỏ, băm nhỏ. Giữ lại 1 phần, 1 phần đem phi thơm.

![image_2023-02-27_165652081.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F0458adf1-68ce-44b1-89b1-f069b44a3ee2%2Fimage_2023-02-27_165652081.png7b4ccb45-7a23-4024-9e24-632587880887?alt=media&token=55c9b3fa-9cb0-4d85-8b9b-632da4af1ac3)', N'<p>Hoặc nếu không biết mổ cá, bạn có thể nhờ hàng cá sơ chế rồi rửa sạch lại với muối hạt và nước. Lưu ý nhớ bỏ hết phần màng đen bám ở bụng cá để thịt cá thơm ngon hơn.</p>
<p>Đối với món canh chua cá lóc cho 3-4 người ăn, có thể chỉ dùng 3 khúc cá, phần còn lại có thể bảo quản trong tủ lạnh để chế biến cho lần nấu tiếp theo. Cá sau khi rửa sạch, cho vào tô ướp cùng với 1 chút bột canh và 1 ít ớt tươi băm nhỏ.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F0458adf1-68ce-44b1-89b1-f069b44a3ee2%2Fimage_2023-02-27_165615553.png7008abd1-5913-49fa-829c-0dfe74dd45b8?alt=media&amp;token=97c91939-fb17-4c96-bcc6-dee93884d6c9" alt="image_2023-02-27_165615553.png"></p>
<p>Cà chua rửa sạch, bổ múi cau.</p>
<p>Dứa gọt vỏ, bỏ mắt, cắt phần đầu và đuôi sau đó rửa sạch, bổ dọc thân dứa làm 4, gọt bỏ phần lõi cứng bên trong và thái dứa thành từng miếng hình tam giác. Tiếp đến ướp dứa với 1 chút đường và muối để làm dịu đi vị chua của dứa, giúp miếng dứa đậm vị hơn.</p>
<p>Giá đỗ ngâm qua với nước muối, rửa sạch rồi vớt ra để ráo nước.</p>
<p>Dọc mùng (bạc hà) tước vỏ, cắt chéo khúc khoảng 3-4cm, sau đó ngâm vào tô nước đá có pha chút muối để tiêu vị ngứa nếu có, giúp miếng dọc mùng dòn hơn, và không bị thâm.</p>
<p>Đậu bắp cắt bỏ phần ngọn và cuống, rửa sạch, xắt khúc giống như bạc hà và ngâm vào tô nước pha muối loãng để khử bớt chất nhớt. Dọc mùng và đậu bắp ngâm khoảng 10 phút thì vớt ra, để ráo.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F0458adf1-68ce-44b1-89b1-f069b44a3ee2%2Fimage_2023-02-27_165632198.png7860399b-45b6-480d-af79-83e30b7e2610?alt=media&amp;token=19d0c5ed-c605-415f-93a4-5bc82beff307" alt="image_2023-02-27_165632198.png"></p>
<p>Sả bóc bỏ phần bẹ già bên ngoài, rửa sạch rồi băm nhỏ.</p>
<p>Rau om và ngò gai nhặt bỏ lá vàng và phần thân già, rửa sạch, xắt nhỏ.</p>
<p>Tỏi bóc vỏ, băm nhỏ. Giữ lại 1 phần, 1 phần đem phi thơm.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F0458adf1-68ce-44b1-89b1-f069b44a3ee2%2Fimage_2023-02-27_165652081.png7b4ccb45-7a23-4024-9e24-632587880887?alt=media&amp;token=55c9b3fa-9cb0-4d85-8b9b-632da4af1ac3" alt="image_2023-02-27_165652081.png"></p>
', 1, N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'f5b73aca-6032-4f17-a30a-0559b4984803', N'Làm nem rán theo cách mà Homnayangi chia sẻ dưới đây chắc chắn sẽ khiến bạn tự tin trổ tài trong những dịp nhà có cỗ hoặc làm mâm cơm Tết.', N'<p>Làm nem rán theo cách mà Homnayangi chia sẻ dưới đây chắc chắn sẽ khiến bạn tự tin trổ tài trong những dịp nhà có cỗ hoặc làm mâm cơm Tết.</p>
', 0, N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'7a1d4a9e-edb4-4ae3-8263-06608498c9c7', N'Không chỉ giòn sần sật, làm theo cách mà Homnayangi chia sẻ dưới đây, đĩa mì xào rau muống của bạn còn có màu xanh mướt mắt không hề thua kém ngoài hàng.', N'<p>Không chỉ giòn sần sật, làm theo cách mà Homnayangi chia sẻ dưới đây, đĩa mì xào rau muống của bạn còn có màu xanh mướt mắt không hề thua kém ngoài hàng.</p>
', 0, N'b5603c80-4003-41f9-a7a5-3076de517599', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'b8ffa76f-c9ee-4c72-89e4-0a23b93ca5d0', N'Thử làm **salad rau củ trộn** với nước sốt mè rang hấp dẫn, thanh mát và bổ dưỡng theo cách mà Homnayangi chia sẻ trong bài viết dưới đây để giảm câ', N'<p>Thử làm <strong>salad rau củ trộn</strong> với nước sốt mè rang hấp dẫn, thanh mát và bổ dưỡng theo cách mà Homnayangi chia sẻ trong bài viết dưới đây để giảm câ</p>
', 0, N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'd2d3faa6-5da5-4f45-b494-0ae06ac1ed8d', N'Cho 2 muỗng canh dầu ăn vào chảo, cho hành tỏi băm vào phi thơm

![image_2023-02-27_175252694.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F346dfff7-d030-408f-b521-f4b15b139bd3%2Fimage_2023-02-27_175252694.pngebb53945-71ab-457f-b3da-b8b67da87d87?alt=media&token=ca04282c-e2a6-4241-87ab-5574e9cc5061)

Tiếp đến, cho khổ qua vào để xào. Nếu muốn ăn giòn thì bạn xào ở lửa lớn, ngược lại muốn ăn khổ qua mềm thì xào ở lửa nhỏ.

![image_2023-02-27_175314342.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F346dfff7-d030-408f-b521-f4b15b139bd3%2Fimage_2023-02-27_175314342.png7cc33c4f-3c86-486d-baac-5e151141ba7b?alt=media&token=413fc4d0-d65c-428b-82d1-b48d93620e33)

Nêm gia vị gồm có dầu hào, bột ngọt rồi đảo đều tay để khổ qua thấm đậm. Khi khổ qua chín, bạn cho trứng gà vào. Đảo nhanh tay để trứng không bị vón cục. Trứng chín, nêm lại gia vị vừa ăn lần nữa rồi cho hành lá, nấu thêm 10 giây rồi tắt bếp.', N'<p>Cho 2 muỗng canh dầu ăn vào chảo, cho hành tỏi băm vào phi thơm</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F346dfff7-d030-408f-b521-f4b15b139bd3%2Fimage_2023-02-27_175252694.pngebb53945-71ab-457f-b3da-b8b67da87d87?alt=media&amp;token=ca04282c-e2a6-4241-87ab-5574e9cc5061" alt="image_2023-02-27_175252694.png"></p>
<p>Tiếp đến, cho khổ qua vào để xào. Nếu muốn ăn giòn thì bạn xào ở lửa lớn, ngược lại muốn ăn khổ qua mềm thì xào ở lửa nhỏ.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F346dfff7-d030-408f-b521-f4b15b139bd3%2Fimage_2023-02-27_175314342.png7cc33c4f-3c86-486d-baac-5e151141ba7b?alt=media&amp;token=413fc4d0-d65c-428b-82d1-b48d93620e33" alt="image_2023-02-27_175314342.png"></p>
<p>Nêm gia vị gồm có dầu hào, bột ngọt rồi đảo đều tay để khổ qua thấm đậm. Khi khổ qua chín, bạn cho trứng gà vào. Đảo nhanh tay để trứng không bị vón cục. Trứng chín, nêm lại gia vị vừa ăn lần nữa rồi cho hành lá, nấu thêm 10 giây rồi tắt bếp.</p>
', 2, N'346dfff7-d030-408f-b521-f4b15b139bd3', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'0e254df6-a2ea-446b-85fd-0c8dec1800bf', N'![image_2023-02-27_162552494.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5de76825-4f4e-469d-b94e-5e457294b045%2Fimage_2023-02-27_162552494.png8926b663-9092-451a-8971-9ee3a8cda9fa?alt=media&token=2ba9d84d-94ef-4958-b05e-5ecfc62ebe24)

Khoai mỡ rửa qua cho trôi lớp bùn đất bám bên ngoài, gọt vỏ sau đó rửa sạch lại, thái khúc vừa ăn. Ướp thêm 1 chút bột canh để khoai mỡ đậm vị.

Tôm bóc vỏ, lột chỉ đen dọc sống lưng. Tách riêng phần đầu tôm, bóc bỏ phần phân đen, rửa sạch sau đó giã nhuyễn, lọc lấy nước cốt cho ra riêng 1 tô để dùng nấu canh cho ngọt nước.

Phần thân tôm băm nhỏ, không nên băm quá nhuyễn, sau đó ướp cùng với 1 ít bột canh. Đảo đều và ướp tôm khoảng 5-10 phút để tôm thấm gia vị.

Hành khô bóc vỏ, băm nhỏ.

Hành lá, mùi tàu nhặt rồi rửa sạch, xắt nhỏ.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5de76825-4f4e-469d-b94e-5e457294b045%2Fimage_2023-02-27_162552494.png8926b663-9092-451a-8971-9ee3a8cda9fa?alt=media&amp;token=2ba9d84d-94ef-4958-b05e-5ecfc62ebe24" alt="image_2023-02-27_162552494.png"></p>
<p>Khoai mỡ rửa qua cho trôi lớp bùn đất bám bên ngoài, gọt vỏ sau đó rửa sạch lại, thái khúc vừa ăn. Ướp thêm 1 chút bột canh để khoai mỡ đậm vị.</p>
<p>Tôm bóc vỏ, lột chỉ đen dọc sống lưng. Tách riêng phần đầu tôm, bóc bỏ phần phân đen, rửa sạch sau đó giã nhuyễn, lọc lấy nước cốt cho ra riêng 1 tô để dùng nấu canh cho ngọt nước.</p>
<p>Phần thân tôm băm nhỏ, không nên băm quá nhuyễn, sau đó ướp cùng với 1 ít bột canh. Đảo đều và ướp tôm khoảng 5-10 phút để tôm thấm gia vị.</p>
<p>Hành khô bóc vỏ, băm nhỏ.</p>
<p>Hành lá, mùi tàu nhặt rồi rửa sạch, xắt nhỏ.</p>
', 1, N'5de76825-4f4e-469d-b94e-5e457294b045', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'817a27ab-7b6f-4c48-b079-0ec4a9a2c49c', N'Để rau mầm không bị đắng, bạn cắt phần gốc rễ đi sau đó ngâm 1 lúc trong nước để các hạt mầm nơi đầu ngọn bở và tách ra, rồi nhẹ nhàng rửa lại. Ngâm với nước muối loãng trong 5 phút, vớt ra rửa sạch rồi để ráo.

![image_2023-03-03_233050743.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fae2bf83d-2b03-4a44-ac29-4c41246cc355%2Fimage_2023-03-03_233050743.pngdebbef6e-ce27-4176-b4fc-f60c6d96473f?alt=media&token=6df584b9-99db-48d3-b26b-102ef8a5e803)

Cà chua bi bỏ cuống, rửa sạch, ngâm nước muối loãng rồi rửa lại, để ráo thì bổ làm đôi.

Dưa chuột rửa sạch, nạo vỏ. Thường thì mình sẽ nạo vỏ, bạn nào thích ăn vỏ thì nhớ ngâm nước muối kỹ càng để bảo đảm vệ sinh an toàn vì giống dưa leo rất hay phun thuốc.

Dưa chuột sau khi rửa sạch sẽ thì thái hạt lựu.', N'<p>Để rau mầm không bị đắng, bạn cắt phần gốc rễ đi sau đó ngâm 1 lúc trong nước để các hạt mầm nơi đầu ngọn bở và tách ra, rồi nhẹ nhàng rửa lại. Ngâm với nước muối loãng trong 5 phút, vớt ra rửa sạch rồi để ráo.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fae2bf83d-2b03-4a44-ac29-4c41246cc355%2Fimage_2023-03-03_233050743.pngdebbef6e-ce27-4176-b4fc-f60c6d96473f?alt=media&amp;token=6df584b9-99db-48d3-b26b-102ef8a5e803" alt="image_2023-03-03_233050743.png"></p>
<p>Cà chua bi bỏ cuống, rửa sạch, ngâm nước muối loãng rồi rửa lại, để ráo thì bổ làm đôi.</p>
<p>Dưa chuột rửa sạch, nạo vỏ. Thường thì mình sẽ nạo vỏ, bạn nào thích ăn vỏ thì nhớ ngâm nước muối kỹ càng để bảo đảm vệ sinh an toàn vì giống dưa leo rất hay phun thuốc.</p>
<p>Dưa chuột sau khi rửa sạch sẽ thì thái hạt lựu.</p>
', 1, N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'543f7622-7e9e-40ad-9d70-10a4ab410c7d', N'**Cách làm gà xào sả ớt** ngon đậm đà. Thịt gà giòn, thơm mùi gừng sả rất thích hợp làm món chính trong bữa cơm gia đình.', N'<p><strong>Cách làm gà xào sả ớt</strong> ngon đậm đà. Thịt gà giòn, thơm mùi gừng sả rất thích hợp làm món chính trong bữa cơm gia đình.</p>
', 0, N'e3f6c554-29c7-460d-a692-d23d15dae638', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'1ea1a7d0-928e-43ab-9ab5-139499e1a35f', N'Sườn non mềm, đậm vị nhưng vẫn giòn sần sật trong khi thơm ngọt, quyện vị vào sườn ăn rất đưa miệng. Đây là món ngon từ sườn mà các bà nội trợ không nên bỏ qua vì độ thơm ngon và lạ miệng của nó.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F3ef7c4e0-b1ac-4506-9556-3541afd421fe%2Fimage.png7d13590c-34d5-4201-b4cb-98ef23975941?alt=media&token=cb73db37-3e62-48a0-8114-4e03bf04e95c)', N'<p>Sườn non mềm, đậm vị nhưng vẫn giòn sần sật trong khi thơm ngọt, quyện vị vào sườn ăn rất đưa miệng. Đây là món ngon từ sườn mà các bà nội trợ không nên bỏ qua vì độ thơm ngon và lạ miệng của nó.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F3ef7c4e0-b1ac-4506-9556-3541afd421fe%2Fimage.png7d13590c-34d5-4201-b4cb-98ef23975941?alt=media&amp;token=cb73db37-3e62-48a0-8114-4e03bf04e95c" alt="image.png"></p>
', 3, N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'4c9645d3-0cae-40ca-b4ac-14dc92e90480', N'![image_2023-03-03_233109334.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fae2bf83d-2b03-4a44-ac29-4c41246cc355%2Fimage_2023-03-03_233109334.pngea970c14-4ba4-4a35-83d3-a158809bfad9?alt=media&token=8aae148f-c394-4dc6-824c-8480fad06795)

Cho rau mầm vào âu lớn, tiếp đến là dưa chuột, cà chua bi. Sau đó rưới nước sốt mè rang lên đều bề mặt. Nhẹ nhàng trộn để tránh rau không bị nát. Để món salad vừa vị, bạn nên rưới nước sốt từ từ để điều chỉnh, không cho quá nhiều 1 lúc.

![image_2023-03-03_233121310.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fae2bf83d-2b03-4a44-ac29-4c41246cc355%2Fimage_2023-03-03_233121310.png9be49a6d-e14c-4c51-852f-0a7ed9bfe895?alt=media&token=c61b9cf0-28df-442b-a091-075921d7b6ac)

Và thêm 1 mẹo hay nữa là khi làm salad, bạn nên rưới nước sốt đậm hơn ở góc phần nguyên liệu cứng, ví dụ như dưa chuột, cà chua bi, hay táo, củ đậu... Bởi vì rau mầm hay rau diếp, xà lách thường mảnh, rưới nước sốt quá đậm thường bị mặn, và trộn nhiều thì dễ bị nát.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fae2bf83d-2b03-4a44-ac29-4c41246cc355%2Fimage_2023-03-03_233109334.pngea970c14-4ba4-4a35-83d3-a158809bfad9?alt=media&amp;token=8aae148f-c394-4dc6-824c-8480fad06795" alt="image_2023-03-03_233109334.png"></p>
<p>Cho rau mầm vào âu lớn, tiếp đến là dưa chuột, cà chua bi. Sau đó rưới nước sốt mè rang lên đều bề mặt. Nhẹ nhàng trộn để tránh rau không bị nát. Để món salad vừa vị, bạn nên rưới nước sốt từ từ để điều chỉnh, không cho quá nhiều 1 lúc.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fae2bf83d-2b03-4a44-ac29-4c41246cc355%2Fimage_2023-03-03_233121310.png9be49a6d-e14c-4c51-852f-0a7ed9bfe895?alt=media&amp;token=c61b9cf0-28df-442b-a091-075921d7b6ac" alt="image_2023-03-03_233121310.png"></p>
<p>Và thêm 1 mẹo hay nữa là khi làm salad, bạn nên rưới nước sốt đậm hơn ở góc phần nguyên liệu cứng, ví dụ như dưa chuột, cà chua bi, hay táo, củ đậu... Bởi vì rau mầm hay rau diếp, xà lách thường mảnh, rưới nước sốt quá đậm thường bị mặn, và trộn nhiều thì dễ bị nát.</p>
', 2, N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'703af719-3504-49f2-aaf0-204e83bb6ca4', N'
### Nấu sườn xào chua ngọt


Cho nồi sườn đã ướp gia vị lên bếp, đậy kín nắp vung và nấu ở nhiệt độ nhỏ.

Cách làm sườn xào chua ngọt thông thường sẽ là chiên sườn, nhưng nếu làm không khéo sẽ khiến cho miếng sườn bị chiên non quá hoặc cháy quá, khi ăn sườn dễ bị cứng và khô.

Trong khi đó, cách nấu sườn xào chua ngọt này sau khi ướp, chỉ cần để sườn om liu riu ở nhiệt độ nhỏ, không cần chiên hay để nhiệt độ lớn.

Bởi sườn cũng giống như thịt gà, chín hơi nên cứ đậy kín vung và nấu trong khoảng 25 phút là sườn sẽ mềm và róc xương.


![image_2023-02-27_173444039.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fcc8da8e0-0c9d-4893-8ea1-387c0519a0f3%2Fimage_2023-02-27_173444039.png1f7dc6bf-135c-4313-bdf1-2e22c746a155?alt=media&token=6c16d61d-a8b9-45af-9c3e-21a9f7d6b282)


Sau khoảng 25 phút sườn đã chín mềm, bạn cho tỏi ớt băm vào, tăng nhiệt độ lớn hơn và đảo đều tay đến khi thấy miếng sườn ngả sang màu cánh gián đẹp mắt, nước sốt chua ngọt sền sệt lại thì tắt bếp.

Trút sườn xào ra đĩa, ăn kèm cơm nóng.

Lưu ý, điểm đặc trưng của món sườn xào chua ngọt đó là ăn ngon nhất lúc nóng và không nên xào đi xào lại hay hâm nóng lại ăn thành nhiều bữa.

Sườn khi nấu lại sẽ bị khô, không được ngọt mềm như ăn bữa đầu tiên. Chính vì thế khi chế biến bạn chỉ nên dùng 1 lượng sườn đủ, tránh lãng phí.', N'<h3>Nấu sườn xào chua ngọt</h3>
<p>Cho nồi sườn đã ướp gia vị lên bếp, đậy kín nắp vung và nấu ở nhiệt độ nhỏ.</p>
<p>Cách làm sườn xào chua ngọt thông thường sẽ là chiên sườn, nhưng nếu làm không khéo sẽ khiến cho miếng sườn bị chiên non quá hoặc cháy quá, khi ăn sườn dễ bị cứng và khô.</p>
<p>Trong khi đó, cách nấu sườn xào chua ngọt này sau khi ướp, chỉ cần để sườn om liu riu ở nhiệt độ nhỏ, không cần chiên hay để nhiệt độ lớn.</p>
<p>Bởi sườn cũng giống như thịt gà, chín hơi nên cứ đậy kín vung và nấu trong khoảng 25 phút là sườn sẽ mềm và róc xương.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fcc8da8e0-0c9d-4893-8ea1-387c0519a0f3%2Fimage_2023-02-27_173444039.png1f7dc6bf-135c-4313-bdf1-2e22c746a155?alt=media&amp;token=6c16d61d-a8b9-45af-9c3e-21a9f7d6b282" alt="image_2023-02-27_173444039.png"></p>
<p>Sau khoảng 25 phút sườn đã chín mềm, bạn cho tỏi ớt băm vào, tăng nhiệt độ lớn hơn và đảo đều tay đến khi thấy miếng sườn ngả sang màu cánh gián đẹp mắt, nước sốt chua ngọt sền sệt lại thì tắt bếp.</p>
<p>Trút sườn xào ra đĩa, ăn kèm cơm nóng.</p>
<p>Lưu ý, điểm đặc trưng của món sườn xào chua ngọt đó là ăn ngon nhất lúc nóng và không nên xào đi xào lại hay hâm nóng lại ăn thành nhiều bữa.</p>
<p>Sườn khi nấu lại sẽ bị khô, không được ngọt mềm như ăn bữa đầu tiên. Chính vì thế khi chế biến bạn chỉ nên dùng 1 lượng sườn đủ, tránh lãng phí.</p>
', 2, N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'329a94d4-61c8-49f4-b17c-2053d1d5a40d', N'Cho 1 thìa canh dầu ăn vào chảo, phi thơm gừng và 1 ít tỏi băm, sau đó cho thịt bò vào xào ở lửa lớn. Để thịt bò xào được mềm, không dai, bạn chỉ cần xào đến khi thịt bò chín tái, màu thịt bò chuyển sang màu hồng nhạt là được, trút thịt bò ra bát riêng.

![image_2023-02-27_223635402.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223635402.pnge8d2f118-4bfe-4dcb-9f5c-11a86cab8c27?alt=media&token=8895d925-d90c-4057-af0a-3b855d1da767)

Tiếp tục cho 2 thìa canh dầu ăn vào chảo vừa xào thịt bò, làm nóng dầu thì phi thơm nốt số tỏi băm, sau đó cho rau muống vào xào. Để rau muống xào được xanh, giòn thì bạn nên xào ở lửa lớn. Đặc biệt, bạn có thể tham khảo 2 mẹo cực hay để xào rau muống xanh, giòn như sau:

![image_2023-02-27_223659038.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223659038.png339bd8c1-267d-4314-940b-1fbc9f5b9508?alt=media&token=c5bfdfcc-5d4c-469f-a2ca-cfd9d9720f0a)

Khi rau muống đã thấm đều dầu và chuyển sang màu xanh mướt, bạn nêm gia vị gồm có 1/2 thìa cà phê bột canh hoặc bột nêm, 1/2 thìa cà phê bột ngọt, đảo đều tay.

![image_2023-02-27_223713559.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223713559.png25e45cf3-0b13-4e6d-b1a7-70b2f907f568?alt=media&token=5b430cfe-0b12-475f-b013-dfaa56d52959)

![image_2023-02-27_223723695.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223723695.pngf78dea2e-cd56-4aea-a6a8-3e1bf02acaf1?alt=media&token=b83c172b-967c-4095-9fea-3c3851803d52)

Đảo đều rồi nêm lại gia vị vừa ăn và cho thêm ít dầu hào nếu như thấy nhạt. Xào mì tôm với rau muống thêm tầm 10 giây nữa thì tắt bếp, cho ra đĩa.', N'<p>Cho 1 thìa canh dầu ăn vào chảo, phi thơm gừng và 1 ít tỏi băm, sau đó cho thịt bò vào xào ở lửa lớn. Để thịt bò xào được mềm, không dai, bạn chỉ cần xào đến khi thịt bò chín tái, màu thịt bò chuyển sang màu hồng nhạt là được, trút thịt bò ra bát riêng.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223635402.pnge8d2f118-4bfe-4dcb-9f5c-11a86cab8c27?alt=media&amp;token=8895d925-d90c-4057-af0a-3b855d1da767" alt="image_2023-02-27_223635402.png"></p>
<p>Tiếp tục cho 2 thìa canh dầu ăn vào chảo vừa xào thịt bò, làm nóng dầu thì phi thơm nốt số tỏi băm, sau đó cho rau muống vào xào. Để rau muống xào được xanh, giòn thì bạn nên xào ở lửa lớn. Đặc biệt, bạn có thể tham khảo 2 mẹo cực hay để xào rau muống xanh, giòn như sau:</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223659038.png339bd8c1-267d-4314-940b-1fbc9f5b9508?alt=media&amp;token=c5bfdfcc-5d4c-469f-a2ca-cfd9d9720f0a" alt="image_2023-02-27_223659038.png"></p>
<p>Khi rau muống đã thấm đều dầu và chuyển sang màu xanh mướt, bạn nêm gia vị gồm có 1/2 thìa cà phê bột canh hoặc bột nêm, 1/2 thìa cà phê bột ngọt, đảo đều tay.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223713559.png25e45cf3-0b13-4e6d-b1a7-70b2f907f568?alt=media&amp;token=5b430cfe-0b12-475f-b013-dfaa56d52959" alt="image_2023-02-27_223713559.png"></p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223723695.pngf78dea2e-cd56-4aea-a6a8-3e1bf02acaf1?alt=media&amp;token=b83c172b-967c-4095-9fea-3c3851803d52" alt="image_2023-02-27_223723695.png"></p>
<p>Đảo đều rồi nêm lại gia vị vừa ăn và cho thêm ít dầu hào nếu như thấy nhạt. Xào mì tôm với rau muống thêm tầm 10 giây nữa thì tắt bếp, cho ra đĩa.</p>
', 2, N'b5603c80-4003-41f9-a7a5-3076de517599', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'350c9964-bdaa-44c1-b82d-21b7dd583d3b', N'![image_2023-02-27_162803352.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5de76825-4f4e-469d-b94e-5e457294b045%2Fimage_2023-02-27_162803352.png94b48ee0-5fd7-4a58-acdd-229dea4e6a91?alt=media&token=dac9abdf-8201-4f76-bdef-8cd092563550)

Canh khoai mỡ nấu tôm có mùi thơm hấp dẫn, ngọt dịu. Khoai mỡ mềm mịn và ngậy, tôm ngọt và thơm, thêm phần hương vị hấp dẫn của mùi tàu hành lá càng tăng độ hấp dẫn cho món canh bổ dưỡng này. 😍😍', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5de76825-4f4e-469d-b94e-5e457294b045%2Fimage_2023-02-27_162803352.png94b48ee0-5fd7-4a58-acdd-229dea4e6a91?alt=media&amp;token=dac9abdf-8201-4f76-bdef-8cd092563550" alt="image_2023-02-27_162803352.png"></p>
<p>Canh khoai mỡ nấu tôm có mùi thơm hấp dẫn, ngọt dịu. Khoai mỡ mềm mịn và ngậy, tôm ngọt và thơm, thêm phần hương vị hấp dẫn của mùi tàu hành lá càng tăng độ hấp dẫn cho món canh bổ dưỡng này. 😍😍</p>
', 3, N'5de76825-4f4e-469d-b94e-5e457294b045', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'194e4e7c-7495-458c-8df8-2438b937598c', N'![image_2023-02-27_175044937.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F346dfff7-d030-408f-b521-f4b15b139bd3%2Fimage_2023-02-27_175044937.pngc774a00a-47b8-45ec-8686-2fc86077458c?alt=media&token=c4414748-f1cd-4143-844d-3c981df6b2d4)


### Sơ chế khổ qua không bị đắng

Khổ qua (mướp đắng) rửa qua, cắt bỏ mủm đầu mủm cuống, bổ làm đôi, cạo hết phần ruột và hạt bỏ đi. Sau đó cắt thành những lát mỏng.
Để khổ qua bớt đắng, sau khi cắt thái, bạn ngâm khổ qua trong tô nước đá có pha chút muối hạt. Nước đá muối sẽ giúp khổ qua đỡ đắng và đặc biệt sẽ rất giòn. Cẩn thận hơn thì chần qua rồi mới ngâm nước đá. Cách khác là tránh chọn những trái khổ qua già. Trái càng già sẽ càng đắng.
Sau khi ngâm 10 phút, bạn vớt khổ qua ra rổ, để ráo nước.


### Sơ chế các nguyên liệu khác

Hành bóc vỏ, băm nhỏ hoặc thái lát mỏng.

Hành lá nhặt rửa sạch, cắt khúc tầm nửa đốt ngón tay.

Đập trứng gà ra bát, nêm 1 ít nước mắm, 1 xíu dầu ăn, 1 chút nước lã, đánh tơi lên. Những nguyên liệu và gia vị trên giúp cho trứng đậm thơm và tơi xốp, mềm hơn.
', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F346dfff7-d030-408f-b521-f4b15b139bd3%2Fimage_2023-02-27_175044937.pngc774a00a-47b8-45ec-8686-2fc86077458c?alt=media&amp;token=c4414748-f1cd-4143-844d-3c981df6b2d4" alt="image_2023-02-27_175044937.png"></p>
<h3>Sơ chế khổ qua không bị đắng</h3>
<p>Khổ qua (mướp đắng) rửa qua, cắt bỏ mủm đầu mủm cuống, bổ làm đôi, cạo hết phần ruột và hạt bỏ đi. Sau đó cắt thành những lát mỏng.
Để khổ qua bớt đắng, sau khi cắt thái, bạn ngâm khổ qua trong tô nước đá có pha chút muối hạt. Nước đá muối sẽ giúp khổ qua đỡ đắng và đặc biệt sẽ rất giòn. Cẩn thận hơn thì chần qua rồi mới ngâm nước đá. Cách khác là tránh chọn những trái khổ qua già. Trái càng già sẽ càng đắng.
Sau khi ngâm 10 phút, bạn vớt khổ qua ra rổ, để ráo nước.</p>
<h3>Sơ chế các nguyên liệu khác</h3>
<p>Hành bóc vỏ, băm nhỏ hoặc thái lát mỏng.</p>
<p>Hành lá nhặt rửa sạch, cắt khúc tầm nửa đốt ngón tay.</p>
<p>Đập trứng gà ra bát, nêm 1 ít nước mắm, 1 xíu dầu ăn, 1 chút nước lã, đánh tơi lên. Những nguyên liệu và gia vị trên giúp cho trứng đậm thơm và tơi xốp, mềm hơn.</p>
', 1, N'346dfff7-d030-408f-b521-f4b15b139bd3', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'aab94b1d-fcb6-45da-8f46-250a6c12ac0c', N'![image_2023-02-28_104746545.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f%2Fimage_2023-02-28_104746545.png5983fecb-ead3-4f4c-ad44-4ce8516be290?alt=media&token=6e9a80d9-8ca1-427c-9143-3bcf80ce15dc)

Tôm khô rửa qua, ngâm với nước ấm trong khoảng 15 phút cho tôm mềm sau đó vớt ra, rửa sạch rồi để ráo nước.

Thịt ba chỉ rửa qua với nước pha muối loãng, để ráo, thái con chì hoặc xắt hạt lựu.

Hành, tỏi bóc vỏ, băm nhỏ.

Hành lá rửa sạch, xắt nhỏ.

Tiêu đen và tiêu xanh đập dập nếu muốn có vị the, còn muốn đẹp mắt thì để nguyên quả.

Để làm kho quẹt ngon, các bạn nên chọn phần thịt ba chỉ. Đây là phần thịt có cả nạc cả mỡ nên khi kho sẽ không bị khô. Bên cạnh đó, khi thái thịt không nên thái quá to, nên thái miếng nhỏ bằng với tôm khô là được.

### Pha nước mắm

![image_2023-02-28_104915251.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f%2Fimage_2023-02-28_104915251.png2834c591-ba20-434b-b79d-29c111d2e8c3?alt=media&token=282b13aa-cc15-4836-b0cf-0af188cbe73b)

Thực tế, có người thích ăn kho quẹt mặn nhưng cũng có người thích ăn hơi ngọt 1 chút. Ngoài ra còn phụ thuộc vào độ đậm của nước mắm nữa. Chính vì vậy, liều lượng nước mắm và đường pha vào để nấu kho quẹt cần điều chỉnh phù hợp với khẩu vị của từng gia đình.

Với khối lượng tôm khô và thịt ba chỉ như trên thì các bạn có thể pha nước mắm đường theo tỷ lệ như sau:
1. 2 muỗng nước mắm
2. 1/2 hoặc 1 muỗng đường tùy vào khẩu vị ăn ngọt
3. 2,5 muỗng nước lọc

Khuấy đều cho đường hòa tan.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f%2Fimage_2023-02-28_104746545.png5983fecb-ead3-4f4c-ad44-4ce8516be290?alt=media&amp;token=6e9a80d9-8ca1-427c-9143-3bcf80ce15dc" alt="image_2023-02-28_104746545.png"></p>
<p>Tôm khô rửa qua, ngâm với nước ấm trong khoảng 15 phút cho tôm mềm sau đó vớt ra, rửa sạch rồi để ráo nước.</p>
<p>Thịt ba chỉ rửa qua với nước pha muối loãng, để ráo, thái con chì hoặc xắt hạt lựu.</p>
<p>Hành, tỏi bóc vỏ, băm nhỏ.</p>
<p>Hành lá rửa sạch, xắt nhỏ.</p>
<p>Tiêu đen và tiêu xanh đập dập nếu muốn có vị the, còn muốn đẹp mắt thì để nguyên quả.</p>
<p>Để làm kho quẹt ngon, các bạn nên chọn phần thịt ba chỉ. Đây là phần thịt có cả nạc cả mỡ nên khi kho sẽ không bị khô. Bên cạnh đó, khi thái thịt không nên thái quá to, nên thái miếng nhỏ bằng với tôm khô là được.</p>
<h3>Pha nước mắm</h3>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f%2Fimage_2023-02-28_104915251.png2834c591-ba20-434b-b79d-29c111d2e8c3?alt=media&amp;token=282b13aa-cc15-4836-b0cf-0af188cbe73b" alt="image_2023-02-28_104915251.png"></p>
<p>Thực tế, có người thích ăn kho quẹt mặn nhưng cũng có người thích ăn hơi ngọt 1 chút. Ngoài ra còn phụ thuộc vào độ đậm của nước mắm nữa. Chính vì vậy, liều lượng nước mắm và đường pha vào để nấu kho quẹt cần điều chỉnh phù hợp với khẩu vị của từng gia đình.</p>
<p>Với khối lượng tôm khô và thịt ba chỉ như trên thì các bạn có thể pha nước mắm đường theo tỷ lệ như sau:</p>
<ol>
<li>2 muỗng nước mắm</li>
<li>1/2 hoặc 1 muỗng đường tùy vào khẩu vị ăn ngọt</li>
<li>2,5 muỗng nước lọc</li>
</ol>
<p>Khuấy đều cho đường hòa tan.</p>
', 1, N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'9456224e-b5b5-452b-af92-276d8db54c77', N'Lót lá gừng xuống dưới đáy nồi, rồi xếp 1 lượt thịt, cá chiên xong thì cho vào nồi, cuối cùng là 1 lớp thịt ba chỉ xen kẽ với cá.

![image_2023-02-27_213431732.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_213431732.pnga5981ce6-5794-4fbf-987c-5ae169954199?alt=media&token=88a5bcb0-a633-494e-a084-31c7fc270549)

Đổ phần dầu ăn vừa chiên cá vào nồi kho, rưới hết phần nước mắm ướp cá và chế thêm 1 ít nước lọc sao cho xâm xấp mặt cá. Cho 1 thìa cà phê hạt tiêu nguyên hạt vào để kho cùng kèm với 1-2 trái ớt.

![image_2023-02-27_213445917.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_213445917.pnga2900829-bbb5-46fa-8029-7b52b3ccaf59?alt=media&token=da781599-88c2-459e-8281-dcb75b0aa75f)

Ban đầu bật lửa to 1 chút để cá định hình đồng thời nước kho sôi bùng lên, phủ đều để mặt trên của cá có màu sắc đẹp mắt.

Để cá sôi ở lửa lớn tầm 3-4 phút thì hạ lửa xuống, đậy nắp nồi, cho cá kho liu riu tầm 1 tiếng, đến khi nước kho cá sền sệt lại là được. Lưu ý trong quá trình kho, bạn nên nêm nếm lại sao cho vừa với khẩu vị ăn của gia đình mình.', N'<p>Lót lá gừng xuống dưới đáy nồi, rồi xếp 1 lượt thịt, cá chiên xong thì cho vào nồi, cuối cùng là 1 lớp thịt ba chỉ xen kẽ với cá.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_213431732.pnga5981ce6-5794-4fbf-987c-5ae169954199?alt=media&amp;token=88a5bcb0-a633-494e-a084-31c7fc270549" alt="image_2023-02-27_213431732.png"></p>
<p>Đổ phần dầu ăn vừa chiên cá vào nồi kho, rưới hết phần nước mắm ướp cá và chế thêm 1 ít nước lọc sao cho xâm xấp mặt cá. Cho 1 thìa cà phê hạt tiêu nguyên hạt vào để kho cùng kèm với 1-2 trái ớt.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_213445917.pnga2900829-bbb5-46fa-8029-7b52b3ccaf59?alt=media&amp;token=da781599-88c2-459e-8281-dcb75b0aa75f" alt="image_2023-02-27_213445917.png"></p>
<p>Ban đầu bật lửa to 1 chút để cá định hình đồng thời nước kho sôi bùng lên, phủ đều để mặt trên của cá có màu sắc đẹp mắt.</p>
<p>Để cá sôi ở lửa lớn tầm 3-4 phút thì hạ lửa xuống, đậy nắp nồi, cho cá kho liu riu tầm 1 tiếng, đến khi nước kho cá sền sệt lại là được. Lưu ý trong quá trình kho, bạn nên nêm nếm lại sao cho vừa với khẩu vị ăn của gia đình mình.</p>
', 2, N'b9f39ecc-c689-4910-ac82-816e20994156', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'6420a209-c659-4798-9822-2ec463a89f62', N'![image_2023-02-27_213506847.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_213506847.png0553748b-e11d-4724-9d24-467210ce54bd?alt=media&token=4d267216-403b-443d-8f00-c8967a758048)

Cá lóc kho nên ăn lúc nóng, từng khúc cá có màu vàng nâu bóng óng ả, nước kho sánh mịn. Cá kho có mùi thơm vô cùng hấp dẫn, khi ăn cảm nhận rõ thịt cá lóc mềm, ngọt và đậm đà. Mùi tiêu, lá gừng và nghệ quyện lẫn vào nhau, thấm vào thịt cá lóc mang đến độ thơm ngon tuyệt vời.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_213506847.png0553748b-e11d-4724-9d24-467210ce54bd?alt=media&amp;token=4d267216-403b-443d-8f00-c8967a758048" alt="image_2023-02-27_213506847.png"></p>
<p>Cá lóc kho nên ăn lúc nóng, từng khúc cá có màu vàng nâu bóng óng ả, nước kho sánh mịn. Cá kho có mùi thơm vô cùng hấp dẫn, khi ăn cảm nhận rõ thịt cá lóc mềm, ngọt và đậm đà. Mùi tiêu, lá gừng và nghệ quyện lẫn vào nhau, thấm vào thịt cá lóc mang đến độ thơm ngon tuyệt vời.</p>
', 3, N'b9f39ecc-c689-4910-ac82-816e20994156', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'2390d47a-c9fa-44bf-90e8-2fbddfe1b533', N'![image_2023-02-27_173941756.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fdc7234fa-21fd-45bc-9612-f4fb5e2f4f8b%2Fimage_2023-02-27_173941756.pngc656a399-f18e-49ae-ac89-a8500d305323?alt=media&token=e0f9c3bb-d87e-4199-8cc5-00a53f80d6ad)

Rau muống nhặt bỏ phần gốc già, lá vàng, nhặt lấy phần ngọn non. Để xào rau muống giòn thì nên lấy phần cuống nhiều hơn phần lá.

Rau muống sau khi nhặt sạch thì rửa qua sau đó ngâm nước muối loãng khoảng 5-10 phút rồi vớt ra rửa lại, để ở rổ/rá cho ráo nước.

**Mẹo:**
Ở bước này, có 1 bí quyết rất hay mà bạn có thể áp dụng, đó là sau khi rửa sạch, bạn có thể ngâm rau muống trong thau nước đá lạnh, vừa giúp rau giòn lại xanh. Ngâm khoảng 3 phút thì vớt rau ra, để ráo.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fdc7234fa-21fd-45bc-9612-f4fb5e2f4f8b%2Fimage_2023-02-27_173941756.pngc656a399-f18e-49ae-ac89-a8500d305323?alt=media&amp;token=e0f9c3bb-d87e-4199-8cc5-00a53f80d6ad" alt="image_2023-02-27_173941756.png"></p>
<p>Rau muống nhặt bỏ phần gốc già, lá vàng, nhặt lấy phần ngọn non. Để xào rau muống giòn thì nên lấy phần cuống nhiều hơn phần lá.</p>
<p>Rau muống sau khi nhặt sạch thì rửa qua sau đó ngâm nước muối loãng khoảng 5-10 phút rồi vớt ra rửa lại, để ở rổ/rá cho ráo nước.</p>
<p><strong>Mẹo:</strong>
Ở bước này, có 1 bí quyết rất hay mà bạn có thể áp dụng, đó là sau khi rửa sạch, bạn có thể ngâm rau muống trong thau nước đá lạnh, vừa giúp rau giòn lại xanh. Ngâm khoảng 3 phút thì vớt rau ra, để ráo.</p>
', 1, N'dc7234fa-21fd-45bc-9612-f4fb5e2f4f8b', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'd422dd9e-4bb9-44d5-b169-300ba4ff7d17', N'Mì xào rau muống có hương vị hấp dẫn, ăn giòn và không gây ngán. Do xào ở nhiệt độ và thời gian hợp lý nên rau muống xanh mướt và giòn, mì tôm không bị nhũn nát và đặc biệt là thịt bò mềm, không hề dai cứng.

![image_2023-02-27_223751311.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223751311.pngaabfcc0c-6885-418d-8d94-f9321c46d7b1?alt=media&token=d5d206fb-2f7e-45f0-8149-1f166e000b29)', N'<p>Mì xào rau muống có hương vị hấp dẫn, ăn giòn và không gây ngán. Do xào ở nhiệt độ và thời gian hợp lý nên rau muống xanh mướt và giòn, mì tôm không bị nhũn nát và đặc biệt là thịt bò mềm, không hề dai cứng.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223751311.pngaabfcc0c-6885-418d-8d94-f9321c46d7b1?alt=media&amp;token=d5d206fb-2f7e-45f0-8149-1f166e000b29" alt="image_2023-02-27_223751311.png"></p>
', 3, N'b5603c80-4003-41f9-a7a5-3076de517599', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'd8002277-b6e8-4dda-9b4f-30802d01909c', N'Áp dụng cách làm **rau muống xào tỏi** này bạn sẽ có 1 đĩa rau muống xào xanh mướt, giòn ngon và dậy mùi tỏi như ngoài hàng.', N'<p>Áp dụng cách làm <strong>rau muống xào tỏi</strong> này bạn sẽ có 1 đĩa rau muống xào xanh mướt, giòn ngon và dậy mùi tỏi như ngoài hàng.</p>
', 0, N'dc7234fa-21fd-45bc-9612-f4fb5e2f4f8b', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'4a2bcec6-8fac-4139-964e-30fd3a7fa909', N'Cho 2 thìa canh dầu ăn vào chảo, nóng dầu thì phi thơm tỏi, gừng băm. Sau đó cho 1 thìa canh sa tế vào xào thơm lên. Nếu thích sa tế và ăn được cay, bạn có thể cho thêm.

![image_2023-02-28_170926768.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage_2023-02-28_170926768.png1cf875a7-eedf-4265-8132-db24ea5c89dd?alt=media&token=4a84ecf5-96d6-4cc3-b293-967ef02a80d9)

Tiếp đến, cho mực vào xào lăn qua. Để mực xào được giòn, không bị dai, không ra nước, bạn nên xào mực ở lửa lớn. Và chỉ cần xào nhanh mực đến khi mực co lại và chuyển sang màu trắng đục thì cho bông cải, rồi đến ớt chuông vào. 

Đảo đều cho các nguyên liệu thấm đều vào nhau rồi cho hành tây, tiếp tục xào nhanh tay.

Lúc này nêm lại gia vị vừa ăn, gồm có 1/2 thìa cà phê bột ngọt, 1/2 thìa cà phê bột nêm, 1 thìa cà phê dầu hào, 1 thìa cà phê nước mắm.  Đặc biệt, bạn nhớ cho thêm 1/2 thìa canh đường để làm dịu độ gắt của sa tế. Cuối cùng cho cần tây và ớt sừng vào, đảo đều thêm 5-6 giây rồi tắt bếp, trút mực xào ra đĩa.

![image_2023-02-28_170942132.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage_2023-02-28_170942132.png620048ee-09e6-41e9-a8ab-d3131159c571?alt=media&token=d5107a98-ccb3-4dff-866c-90dddef99847)', N'<p>Cho 2 thìa canh dầu ăn vào chảo, nóng dầu thì phi thơm tỏi, gừng băm. Sau đó cho 1 thìa canh sa tế vào xào thơm lên. Nếu thích sa tế và ăn được cay, bạn có thể cho thêm.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage_2023-02-28_170926768.png1cf875a7-eedf-4265-8132-db24ea5c89dd?alt=media&amp;token=4a84ecf5-96d6-4cc3-b293-967ef02a80d9" alt="image_2023-02-28_170926768.png"></p>
<p>Tiếp đến, cho mực vào xào lăn qua. Để mực xào được giòn, không bị dai, không ra nước, bạn nên xào mực ở lửa lớn. Và chỉ cần xào nhanh mực đến khi mực co lại và chuyển sang màu trắng đục thì cho bông cải, rồi đến ớt chuông vào.</p>
<p>Đảo đều cho các nguyên liệu thấm đều vào nhau rồi cho hành tây, tiếp tục xào nhanh tay.</p>
<p>Lúc này nêm lại gia vị vừa ăn, gồm có 1/2 thìa cà phê bột ngọt, 1/2 thìa cà phê bột nêm, 1 thìa cà phê dầu hào, 1 thìa cà phê nước mắm.  Đặc biệt, bạn nhớ cho thêm 1/2 thìa canh đường để làm dịu độ gắt của sa tế. Cuối cùng cho cần tây và ớt sừng vào, đảo đều thêm 5-6 giây rồi tắt bếp, trút mực xào ra đĩa.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage_2023-02-28_170942132.png620048ee-09e6-41e9-a8ab-d3131159c571?alt=media&amp;token=d5107a98-ccb3-4dff-866c-90dddef99847" alt="image_2023-02-28_170942132.png"></p>
', 2, N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'ee3b83ca-b1b9-47a8-90d4-34b74fe55899', N'Điều quan trọng trong **cách làm nộm tai heo** là bước pha nước trộn nộm. Bên cạnh đó, miếng tai heo trắng giòn không bị dính nhớt vào nhau, dưa chuột không ra quá nhiều nước.', N'<p>Điều quan trọng trong <strong>cách làm nộm tai heo</strong> là bước pha nước trộn nộm. Bên cạnh đó, miếng tai heo trắng giòn không bị dính nhớt vào nhau, dưa chuột không ra quá nhiều nước.</p>
', 0, N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'32f18d8c-4d26-437c-8f26-37da88be5024', N'Thịt mua về ngâm trong nước muối pha loãng tầm 10 phút để làm sạch và khử mùi, sau đó vớt ra rửa sạch để ráo. Hoặc bạn cũng có thể chần qua thịt, rửa lại rồi để ráo nước. 

Theo kinh nghiệm của Cookbeo, việc chần qua giúp miếng thịt dễ thái hơn. Tuy nhiên, để thịt sống thì khi ướp sẽ thấm vị hơn.

Sau khi ráo nước, thái thịt thành từng khúc với kích cỡ tùy thích nhưng không nên nhỏ quá sẽ dễ bị nát trong quá trình kho. Thường thì khi nấu thịt kho tàu, người ta hay thái to bản, bề ngang tầm 2-3 đốt ngón tay còn độ dày khoảng 2 đốt ngón tay.

![image_2023-02-27_181759106.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_181759106.png2e67f883-fb04-4aff-ad42-618f04892d5b?alt=media&token=cce67dcb-6706-4e11-a462-ae5067e09029)


### Ướp thịt

Hành và tỏi bóc vỏ, ớt cắt làm đôi, cho tất cả vào cối. Cho vào cối 1 thìa cà phê muối, 1 thìa cà phê đường rồi giã nhuyễn.

![image_2023-02-27_181853443.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_181853443.pngcdb1b56a-f503-4ed2-9c50-5131a674236e?alt=media&token=5b79d7c5-6301-4d1e-aec0-1d4704bcb2ed)

Sau khi đã giã nhuyễn, cho hỗn hợp trên ướp cùng với thịt và thêm vào đó 1 thìa cà phê hạt tiêu, 2 thìa canh nước mắm, 1 thìa cà phê bột ngọt, 1 thìa canh xì dầu, 2-3 thìa canh màu đường. Một mẹo nhỏ giúp cho miếng thịt kho thơm và trong, nhanh mềm đó là bạn vắt thêm 2 thìa nước cốt chanh vào tô ướp cùng thịt. Trộn đều và để thịt thấm gia vị ít nhất trong 30 phút.

![image_2023-02-27_181912316.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_181912316.pngc77c28ce-47f3-489c-99d9-245ea0616e2f?alt=media&token=dff654be-0566-4bef-accd-3d41bde698d3)


### Luộc trứng

Trong lúc đợi thịt ngấm gia vị, cho trứng vào nồi luộc. Để trứng đậm và dễ bóc, cho vào nồi luộc 1 ít muối hạt. Để nhiệt độ bếp ở mức vừa phải, không nên quá lớn trứng dễ bị vỡ vỏ. Sau khi nước sôi, hạ bớt nhiệt độ xuống, để sôi liu riu trong 7-8 phút là trứng chín tới.

Trứng luộc chín, để nguội rồi bóc vỏ, cho trứng vào bát riêng.

Trứng luộc chín, để nguội rồi bóc vỏ, cho trứng vào bát riêng. Để trứng dễ bóc vỏ mà không bị nứt bên trong, trước khi bóc bạn dập nhẹ xung quanh viền quả, sau đó lột nhẹ nhàng lớp vỏ ra. 

Trứng các bạn để nguyên quả kho, khi ăn thì tách ra, tránh lòng đỏ bị tan ra hòa vào nước kho thịt. Món ăn này với người miền Nam xưa hay ăn vào dịp Tết, với miếng thịt được thái to vuông vắn, trong khi hột vịt to tròn vành vạnh mang ý nghĩa "vuông tròn đều đặn, mọi sự bình an" cho 1 năm mới.

Để làm thịt kho tàu, bạn dùng trứng vịt, trứng gà hay trứng cút đều được. Nhưng phổ biến nhất vẫn là trứng vịt bởi vì trứng vịt lòng trắng trứng có độ trong, dai, rất thích hợp để kho.

![image_2023-02-27_181946236.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_181946236.png800fb67c-59e1-43a9-92de-31534cfbcd52?alt=media&token=e0f89c12-7949-47fa-b7b9-e73d244c5234)

Cho 2 thìa canh dầu ăn vào nồi. Làm nóng dầu thì mình đổ thịt vào, đảo đều ở lửa vừa, mục đích để làm xém cạnh và săn chắc miếng thịt, giúp cho thịt lát nữa có kho lâu cũng không bị bở, nát miếng.

**Kho thịt lần 1:**

Khi thịt săn lại thì các bạn đổ hết phần nước ướp thịt cùng với 900ml nước dừa vào. Về lượng nước dừa thì các bạn cứ áng chừng sao cho ngập mặt thịt là được.

![image_2023-02-27_182015956.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_182015956.png94e7d041-9eae-4f1f-ab72-98ded84b7dda?alt=media&token=0beffca7-0fe3-4db9-8921-b994624af54d)

Để ở lửa vừa thôi, đợi đến khi nước kho thịt sôi thì mình hớt bỏ bọt, sau đó hạ nhỏ lửa xuống, để sôi liu riu, kho thịt trong khoảng 30-35 phút. 

Để thịt được trong, không bị đục thì mình không đậy nắp nồi, thay vào đó sẽ sử dụng 1 miếng lá chuối hoặc là lá mít cũng được, rửa sạch sẽ sau đó phủ lên trên bề mặt thịt. Lá chuối vừa thơm, vừa có tác dụng hút các chất bọt bẩn, giúp cho nước kho thịt trong hơn.

Ngoài ra, vì sử dụng nước dừa nên các bạn cần phải chú ý đến nhiệt độ 1 chút, mình không nên để lửa quá lớn, dễ khiến nước kho thịt bị đục mà miếng thịt cũng sẽ bị đậm màu hơn vì trong dừa có nước đường.

**Kho thịt lần 2:**

Sau 30 phút, khi nước dừa sền sệt lại, lúc này chế thêm nước lọc vào để nấu lần 2. Lượng nước thêm vào xâm xấp mặt thịt, ước tính khoảng 900ml. Tiếp tục kho thịt ở lửa nhỏ tầm 40 phút nữa. Lần này bạn điều chỉnh lửa nhỏ hơn so với lần kho đầu để thịt mềm mà nước kho không bị cạn quá.

![image_2023-02-27_182058282.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_182058282.png36433ae8-fb54-4bd8-b245-cb341afa94e0?alt=media&token=918ec1cf-8a50-445e-ad3c-892bd3817a4c)

Sở dĩ chia ra làm 2 lần chêm nước vì lần 1, nước dừa sẽ thấm vào thịt, giúp miếng thịt có độ ngọt thơm vừa đủ và có màu nâu vàng. Tuy nhiên nếu cho nước dừa quá nhiều miếng thịt thường hay bị ngọt khé, mất đi độ ngọt vốn có và nếu kho lâu, thịt hay bị quá màu, sẽ chuyển sang màu nâu đậm và đôi khi còn có vị chua nữa.

Trong khi đó, khi chêm nước lọc vào sau sẽ giúp bạn dễ điều chỉnh vị đậm nhạt hợp với khẩu vị, đồng thời thịt vừa mềm, trong và có màu sắc đẹp mắt. Đây chính là kinh nghiệm mà Cookbeo rút ra được sau nhiều lần làm thịt kho tàu nên muốn chia sẻ cùng với các bạn.

Sau 45 phút kho lần 2, thịt lúc này đã chín mềm, bạn cho trứng vào và kho thêm tầm 10 phút nữa. Trước khi cho trứng, bạn dùng que tăm chọc 1 vài lỗ trên bề mặt để trứng thấm gia vị. Cho trứng vào sau để trứng không bị chai cứng do kho quá lâu và đặc biệt, màu trứng không bị sẫm màu quá. Ở bước này các bạn có thể nêm nếm lại gia vị vừa ăn.', N'<p>Thịt mua về ngâm trong nước muối pha loãng tầm 10 phút để làm sạch và khử mùi, sau đó vớt ra rửa sạch để ráo. Hoặc bạn cũng có thể chần qua thịt, rửa lại rồi để ráo nước.</p>
<p>Theo kinh nghiệm của Cookbeo, việc chần qua giúp miếng thịt dễ thái hơn. Tuy nhiên, để thịt sống thì khi ướp sẽ thấm vị hơn.</p>
<p>Sau khi ráo nước, thái thịt thành từng khúc với kích cỡ tùy thích nhưng không nên nhỏ quá sẽ dễ bị nát trong quá trình kho. Thường thì khi nấu thịt kho tàu, người ta hay thái to bản, bề ngang tầm 2-3 đốt ngón tay còn độ dày khoảng 2 đốt ngón tay.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_181759106.png2e67f883-fb04-4aff-ad42-618f04892d5b?alt=media&amp;token=cce67dcb-6706-4e11-a462-ae5067e09029" alt="image_2023-02-27_181759106.png"></p>
<h3>Ướp thịt</h3>
<p>Hành và tỏi bóc vỏ, ớt cắt làm đôi, cho tất cả vào cối. Cho vào cối 1 thìa cà phê muối, 1 thìa cà phê đường rồi giã nhuyễn.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_181853443.pngcdb1b56a-f503-4ed2-9c50-5131a674236e?alt=media&amp;token=5b79d7c5-6301-4d1e-aec0-1d4704bcb2ed" alt="image_2023-02-27_181853443.png"></p>
<p>Sau khi đã giã nhuyễn, cho hỗn hợp trên ướp cùng với thịt và thêm vào đó 1 thìa cà phê hạt tiêu, 2 thìa canh nước mắm, 1 thìa cà phê bột ngọt, 1 thìa canh xì dầu, 2-3 thìa canh màu đường. Một mẹo nhỏ giúp cho miếng thịt kho thơm và trong, nhanh mềm đó là bạn vắt thêm 2 thìa nước cốt chanh vào tô ướp cùng thịt. Trộn đều và để thịt thấm gia vị ít nhất trong 30 phút.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_181912316.pngc77c28ce-47f3-489c-99d9-245ea0616e2f?alt=media&amp;token=dff654be-0566-4bef-accd-3d41bde698d3" alt="image_2023-02-27_181912316.png"></p>
<h3>Luộc trứng</h3>
<p>Trong lúc đợi thịt ngấm gia vị, cho trứng vào nồi luộc. Để trứng đậm và dễ bóc, cho vào nồi luộc 1 ít muối hạt. Để nhiệt độ bếp ở mức vừa phải, không nên quá lớn trứng dễ bị vỡ vỏ. Sau khi nước sôi, hạ bớt nhiệt độ xuống, để sôi liu riu trong 7-8 phút là trứng chín tới.</p>
<p>Trứng luộc chín, để nguội rồi bóc vỏ, cho trứng vào bát riêng.</p>
<p>Trứng luộc chín, để nguội rồi bóc vỏ, cho trứng vào bát riêng. Để trứng dễ bóc vỏ mà không bị nứt bên trong, trước khi bóc bạn dập nhẹ xung quanh viền quả, sau đó lột nhẹ nhàng lớp vỏ ra.</p>
<p>Trứng các bạn để nguyên quả kho, khi ăn thì tách ra, tránh lòng đỏ bị tan ra hòa vào nước kho thịt. Món ăn này với người miền Nam xưa hay ăn vào dịp Tết, với miếng thịt được thái to vuông vắn, trong khi hột vịt to tròn vành vạnh mang ý nghĩa &quot;vuông tròn đều đặn, mọi sự bình an&quot; cho 1 năm mới.</p>
<p>Để làm thịt kho tàu, bạn dùng trứng vịt, trứng gà hay trứng cút đều được. Nhưng phổ biến nhất vẫn là trứng vịt bởi vì trứng vịt lòng trắng trứng có độ trong, dai, rất thích hợp để kho.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_181946236.png800fb67c-59e1-43a9-92de-31534cfbcd52?alt=media&amp;token=e0f89c12-7949-47fa-b7b9-e73d244c5234" alt="image_2023-02-27_181946236.png"></p>
<p>Cho 2 thìa canh dầu ăn vào nồi. Làm nóng dầu thì mình đổ thịt vào, đảo đều ở lửa vừa, mục đích để làm xém cạnh và săn chắc miếng thịt, giúp cho thịt lát nữa có kho lâu cũng không bị bở, nát miếng.</p>
<p><strong>Kho thịt lần 1:</strong></p>
<p>Khi thịt săn lại thì các bạn đổ hết phần nước ướp thịt cùng với 900ml nước dừa vào. Về lượng nước dừa thì các bạn cứ áng chừng sao cho ngập mặt thịt là được.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_182015956.png94e7d041-9eae-4f1f-ab72-98ded84b7dda?alt=media&amp;token=0beffca7-0fe3-4db9-8921-b994624af54d" alt="image_2023-02-27_182015956.png"></p>
<p>Để ở lửa vừa thôi, đợi đến khi nước kho thịt sôi thì mình hớt bỏ bọt, sau đó hạ nhỏ lửa xuống, để sôi liu riu, kho thịt trong khoảng 30-35 phút.</p>
<p>Để thịt được trong, không bị đục thì mình không đậy nắp nồi, thay vào đó sẽ sử dụng 1 miếng lá chuối hoặc là lá mít cũng được, rửa sạch sẽ sau đó phủ lên trên bề mặt thịt. Lá chuối vừa thơm, vừa có tác dụng hút các chất bọt bẩn, giúp cho nước kho thịt trong hơn.</p>
<p>Ngoài ra, vì sử dụng nước dừa nên các bạn cần phải chú ý đến nhiệt độ 1 chút, mình không nên để lửa quá lớn, dễ khiến nước kho thịt bị đục mà miếng thịt cũng sẽ bị đậm màu hơn vì trong dừa có nước đường.</p>
<p><strong>Kho thịt lần 2:</strong></p>
<p>Sau 30 phút, khi nước dừa sền sệt lại, lúc này chế thêm nước lọc vào để nấu lần 2. Lượng nước thêm vào xâm xấp mặt thịt, ước tính khoảng 900ml. Tiếp tục kho thịt ở lửa nhỏ tầm 40 phút nữa. Lần này bạn điều chỉnh lửa nhỏ hơn so với lần kho đầu để thịt mềm mà nước kho không bị cạn quá.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_182058282.png36433ae8-fb54-4bd8-b245-cb341afa94e0?alt=media&amp;token=918ec1cf-8a50-445e-ad3c-892bd3817a4c" alt="image_2023-02-27_182058282.png"></p>
<p>Sở dĩ chia ra làm 2 lần chêm nước vì lần 1, nước dừa sẽ thấm vào thịt, giúp miếng thịt có độ ngọt thơm vừa đủ và có màu nâu vàng. Tuy nhiên nếu cho nước dừa quá nhiều miếng thịt thường hay bị ngọt khé, mất đi độ ngọt vốn có và nếu kho lâu, thịt hay bị quá màu, sẽ chuyển sang màu nâu đậm và đôi khi còn có vị chua nữa.</p>
<p>Trong khi đó, khi chêm nước lọc vào sau sẽ giúp bạn dễ điều chỉnh vị đậm nhạt hợp với khẩu vị, đồng thời thịt vừa mềm, trong và có màu sắc đẹp mắt. Đây chính là kinh nghiệm mà Cookbeo rút ra được sau nhiều lần làm thịt kho tàu nên muốn chia sẻ cùng với các bạn.</p>
<p>Sau 45 phút kho lần 2, thịt lúc này đã chín mềm, bạn cho trứng vào và kho thêm tầm 10 phút nữa. Trước khi cho trứng, bạn dùng que tăm chọc 1 vài lỗ trên bề mặt để trứng thấm gia vị. Cho trứng vào sau để trứng không bị chai cứng do kho quá lâu và đặc biệt, màu trứng không bị sẫm màu quá. Ở bước này các bạn có thể nêm nếm lại gia vị vừa ăn.</p>
', 2, N'a8ab27eb-4401-4b4b-93b9-bda34397621f', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'c2b6f931-f545-471e-bc55-38616f4b57e4', N'Cho sườn ướp vào nồi, cho thêm 1 thìa canh dầu ăn. Bật lửa nhỏ, đậy kín nắp nồi và ninh sườn mềm trong 40 phút.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F3ef7c4e0-b1ac-4506-9556-3541afd421fe%2Fimage.png75391735-591d-4a91-9182-746b358036d4?alt=media&token=18d896f7-2746-4aeb-8c1c-56c228ba26dc)

Khi sườn non đã mềm, bạn cho thơm và hành, tỏi băm vào kho cùng thêm 10-15 phút.

Nêm nếm lại gia vị vừa ăn, ở đây Cookbeo nêm thêm 1 thìa canh nước mắm rồi cho hành lá, ớt vào, đảo đều.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F3ef7c4e0-b1ac-4506-9556-3541afd421fe%2Fimage.pngfb869230-f90a-4ef3-a8fb-8b86a7e63eb8?alt=media&token=baf3ec36-7d2e-46d6-a18a-886b8f6fbcdd)

Nếu như muốn sườn bật lên màu rám nâu hơn nữa, thì ở bước này bạn tăng nhiệt độ lớn lên, đảo nhanh tay trong khoảng 15-20 giây rồi tắt bếp.

', N'<p>Cho sườn ướp vào nồi, cho thêm 1 thìa canh dầu ăn. Bật lửa nhỏ, đậy kín nắp nồi và ninh sườn mềm trong 40 phút.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F3ef7c4e0-b1ac-4506-9556-3541afd421fe%2Fimage.png75391735-591d-4a91-9182-746b358036d4?alt=media&amp;token=18d896f7-2746-4aeb-8c1c-56c228ba26dc" alt="image.png"></p>
<p>Khi sườn non đã mềm, bạn cho thơm và hành, tỏi băm vào kho cùng thêm 10-15 phút.</p>
<p>Nêm nếm lại gia vị vừa ăn, ở đây Cookbeo nêm thêm 1 thìa canh nước mắm rồi cho hành lá, ớt vào, đảo đều.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F3ef7c4e0-b1ac-4506-9556-3541afd421fe%2Fimage.pngfb869230-f90a-4ef3-a8fb-8b86a7e63eb8?alt=media&amp;token=baf3ec36-7d2e-46d6-a18a-886b8f6fbcdd" alt="image.png"></p>
<p>Nếu như muốn sườn bật lên màu rám nâu hơn nữa, thì ở bước này bạn tăng nhiệt độ lớn lên, đảo nhanh tay trong khoảng 15-20 giây rồi tắt bếp.</p>
', 2, N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'0bc5553b-fc69-4804-b441-3de436f75a1b', N'**Thịt kho tiêu** đạt chuẩn sẽ có màu cam đỏ đẹp mắt, nước sốt sánh quyện, thơm lừng mùi thịt quyện với tiêu đen xay. Khi ăn, cảm nhận rõ thịt mềm, đậm đà và rất đưa cơm.', N'<p><strong>Thịt kho tiêu</strong> đạt chuẩn sẽ có màu cam đỏ đẹp mắt, nước sốt sánh quyện, thơm lừng mùi thịt quyện với tiêu đen xay. Khi ăn, cảm nhận rõ thịt mềm, đậm đà và rất đưa cơm.</p>
', 0, N'f578682a-d641-4a0b-aa54-6a81eaf7be08', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'90a8873b-d8dc-4e35-a427-46d60dac04f1', N'Hành, tỏi khô bóc vỏ, băm nhỏ.

Hành lá cắt rễ, rửa sạch, băm nhỏ phần đầu hành, phần thân lá xắt nhỏ.

Thịt heo mua về xóc muối hạt, rửa sạch rồi để ráo. Sau đó thái miếng hình chữ nhật hoặc thái con chì. Lưu ý không nên thái thịt quá dày cũng không nên quá mỏng, ít nhất độ dày của miếng thịt tầm hơn nửa phân là được.

Để thịt đậm đà, nên ướp thịt với:
* 2 thìa cà phê nước mắm
* 1/2 thìa cà phê bột ngọt
* 1/3 thìa cà phê hạt tiêu
* 1 ít hành tỏi băm
* 1 thìa cà phê dầu ăn

Trộn đều lên và để khoảng 15-20 phút cho thịt thấm gia vị.

![image_2023-02-27_230101208.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_230101208.png1fc3f00b-bce6-470e-8b5f-cddb110126c5?alt=media&token=7d67c375-ffc5-4259-b00e-616e46ef71c7)

### Làm nước màu

Để món thịt kho tiêu vừa ngon vừa có màu sắc đẹp mắt, hấp dẫn người ăn, bạn nên thắng nước màu hay còn gọi là thắng nước hàng, thắng nước đường. Không chỉ tạo màu sắc, nước hàng còn tăng hương vị thơm ngon cho món ăn.

Cho 1 thìa canh dầu ăn, 1 thìa canh đường vào nồi, bật bếp ở nhiệt độ thấp đủ để đường chảy. Dùng đũa khuấy đều đến khi đường thắng chuyển sang màu nâu cánh gián thì tắt bếp, cho 1 thìa nước sôi nóng vào, khuấy đều là hoàn thành bước thắng nước màu.

![image_2023-02-27_230135591.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_230135591.png2ea14861-eb7a-4b0b-9cfe-05ee6b7ef554?alt=media&token=5ef92c1f-2e67-46b8-bdc5-be77a6a79a80)', N'<p>Hành, tỏi khô bóc vỏ, băm nhỏ.</p>
<p>Hành lá cắt rễ, rửa sạch, băm nhỏ phần đầu hành, phần thân lá xắt nhỏ.</p>
<p>Thịt heo mua về xóc muối hạt, rửa sạch rồi để ráo. Sau đó thái miếng hình chữ nhật hoặc thái con chì. Lưu ý không nên thái thịt quá dày cũng không nên quá mỏng, ít nhất độ dày của miếng thịt tầm hơn nửa phân là được.</p>
<p>Để thịt đậm đà, nên ướp thịt với:</p>
<ul>
<li>2 thìa cà phê nước mắm</li>
<li>1/2 thìa cà phê bột ngọt</li>
<li>1/3 thìa cà phê hạt tiêu</li>
<li>1 ít hành tỏi băm</li>
<li>1 thìa cà phê dầu ăn</li>
</ul>
<p>Trộn đều lên và để khoảng 15-20 phút cho thịt thấm gia vị.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_230101208.png1fc3f00b-bce6-470e-8b5f-cddb110126c5?alt=media&amp;token=7d67c375-ffc5-4259-b00e-616e46ef71c7" alt="image_2023-02-27_230101208.png"></p>
<h3>Làm nước màu</h3>
<p>Để món thịt kho tiêu vừa ngon vừa có màu sắc đẹp mắt, hấp dẫn người ăn, bạn nên thắng nước màu hay còn gọi là thắng nước hàng, thắng nước đường. Không chỉ tạo màu sắc, nước hàng còn tăng hương vị thơm ngon cho món ăn.</p>
<p>Cho 1 thìa canh dầu ăn, 1 thìa canh đường vào nồi, bật bếp ở nhiệt độ thấp đủ để đường chảy. Dùng đũa khuấy đều đến khi đường thắng chuyển sang màu nâu cánh gián thì tắt bếp, cho 1 thìa nước sôi nóng vào, khuấy đều là hoàn thành bước thắng nước màu.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_230135591.png2ea14861-eb7a-4b0b-9cfe-05ee6b7ef554?alt=media&amp;token=5ef92c1f-2e67-46b8-bdc5-be77a6a79a80" alt="image_2023-02-27_230135591.png"></p>
', 1, N'f578682a-d641-4a0b-aa54-6a81eaf7be08', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'2302381a-3299-4730-8fdf-4a0fd9e50882', N'Pha 3 thìa muối hạt vào chậu nước tầm 1,5 lít nước. Cà pháo mua về rửa sạch, sau đó cắt bỏ phần cuống, bổ làm đôi rồi thả ngay vào chậu nước muối.

Cà ngâm trong nước muối khoảng 15 phút, sau đó vớt ra, rửa qua. Tiếp tục pha thêm 1 chậu nước muối, rồi lại thả cà vào ngâm thêm 15 phút. Cứ thế ngâm cà và thay nước muối khoảng 3-4 lần.

Ngoài ra, việc ngâm cà trong nước muối còn giúp cà trắng và không bị thâm, làm tăng tính thẩm mỹ cho món ăn.

Dưa chuột ngâm nước muối, gọt vỏ rồi thái lát mỏng. Ngoài dưa chuột, bạn có thể sử dụng loại dưa gang, dưa hồng ăn kèm với cà pháo mắm tôm cũng rất hợp vị.

Các loại rau ăn kèm nhặt bỏ những lá hư, dập nát, ngâm rửa sạch với nước muối rồi để ráo.

### Pha mắm tôm

![image_2023-02-28_171830831.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F047935c2-9a60-4b0e-865a-89c5e1a680e5%2Fimage_2023-02-28_171830831.pngc5c771ba-fd9f-4f7a-9cbf-a8294906dae8?alt=media&token=8e3ee06e-9b60-4127-afcb-da8304d9f5e2)

Vì mắm tôm đóng chai khá đậm và gắt, chính vì thế cần pha chế thêm. Với 40ml mắm tôm thì pha thêm với 2-3 thìa canh đường (tùy vào sở thích ăn ngọt của từng người), 1 thìa canh bột ngọt, 2-3 trái ớt tươi. Khuấy đều để đường và bột ngọt tan.

Để yên tâm hơn khi ăn mắm tôm, bạn nên chưng mắm tôm lên. Cho 1-2 thìa dầu ăn vào chảo, nóng dầu thì cho mắm tôm vào chưng cùng với đường và bột ngọt, ớt. Mắm tôm chưng vừa dậy mùi thơm, khi ăn cũng ngon và dịu hơn.', N'<p>Pha 3 thìa muối hạt vào chậu nước tầm 1,5 lít nước. Cà pháo mua về rửa sạch, sau đó cắt bỏ phần cuống, bổ làm đôi rồi thả ngay vào chậu nước muối.</p>
<p>Cà ngâm trong nước muối khoảng 15 phút, sau đó vớt ra, rửa qua. Tiếp tục pha thêm 1 chậu nước muối, rồi lại thả cà vào ngâm thêm 15 phút. Cứ thế ngâm cà và thay nước muối khoảng 3-4 lần.</p>
<p>Ngoài ra, việc ngâm cà trong nước muối còn giúp cà trắng và không bị thâm, làm tăng tính thẩm mỹ cho món ăn.</p>
<p>Dưa chuột ngâm nước muối, gọt vỏ rồi thái lát mỏng. Ngoài dưa chuột, bạn có thể sử dụng loại dưa gang, dưa hồng ăn kèm với cà pháo mắm tôm cũng rất hợp vị.</p>
<p>Các loại rau ăn kèm nhặt bỏ những lá hư, dập nát, ngâm rửa sạch với nước muối rồi để ráo.</p>
<h3>Pha mắm tôm</h3>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F047935c2-9a60-4b0e-865a-89c5e1a680e5%2Fimage_2023-02-28_171830831.pngc5c771ba-fd9f-4f7a-9cbf-a8294906dae8?alt=media&amp;token=8e3ee06e-9b60-4127-afcb-da8304d9f5e2" alt="image_2023-02-28_171830831.png"></p>
<p>Vì mắm tôm đóng chai khá đậm và gắt, chính vì thế cần pha chế thêm. Với 40ml mắm tôm thì pha thêm với 2-3 thìa canh đường (tùy vào sở thích ăn ngọt của từng người), 1 thìa canh bột ngọt, 2-3 trái ớt tươi. Khuấy đều để đường và bột ngọt tan.</p>
<p>Để yên tâm hơn khi ăn mắm tôm, bạn nên chưng mắm tôm lên. Cho 1-2 thìa dầu ăn vào chảo, nóng dầu thì cho mắm tôm vào chưng cùng với đường và bột ngọt, ớt. Mắm tôm chưng vừa dậy mùi thơm, khi ăn cũng ngon và dịu hơn.</p>
', 1, N'047935c2-9a60-4b0e-865a-89c5e1a680e5', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'67c8ab97-46fc-483a-97a8-4a19be26e709', N'Canh bí đỏ nấu tôm có màu sắc đẹp mắt, bí đỏ màu cam tươi tắn, thịt tôm trắng hồng, hành lá mùi tàu xanh mướt. Nước canh trong, ngọt thanh và thơm.

![image_2023-02-27_163626591.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163626591.pngb4d09b74-c537-41af-b6b1-1eb1437dd7aa?alt=media&token=10b5594c-ebd4-43c3-bf52-5f2a8692ca5e)', N'<p>Canh bí đỏ nấu tôm có màu sắc đẹp mắt, bí đỏ màu cam tươi tắn, thịt tôm trắng hồng, hành lá mùi tàu xanh mướt. Nước canh trong, ngọt thanh và thơm.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163626591.pngb4d09b74-c537-41af-b6b1-1eb1437dd7aa?alt=media&amp;token=10b5594c-ebd4-43c3-bf52-5f2a8692ca5e" alt="image_2023-02-27_163626591.png"></p>
', 3, N'5b579874-46a8-4895-bad2-1f33d4cb006a', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'07e1d1bf-09e4-47d1-a9f5-51f957a527e0', N'**Cá lóc kho tiêu** cùng với nghệ và đặc biệt có thêm lá gừng chắc chắn sẽ khiến bạn không thể chối từ bởi mùi vị vô cùng hấp dẫn của nó.', N'<p><strong>Cá lóc kho tiêu</strong> cùng với nghệ và đặc biệt có thêm lá gừng chắc chắn sẽ khiến bạn không thể chối từ bởi mùi vị vô cùng hấp dẫn của nó.</p>
', 0, N'b9f39ecc-c689-4910-ac82-816e20994156', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'b261c5da-fbad-4fc3-a18f-5518a3f90769', N'![image_2023-03-03_233146482.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fae2bf83d-2b03-4a44-ac29-4c41246cc355%2Fimage_2023-03-03_233146482.png22e94cb0-66fc-4515-bbb9-f3c8ea56971c?alt=media&token=dd2ae6e1-ad75-415d-83d5-2876557f9337)

Nếu chưa ăn tới, bạn đừng nên trộn vội, mà chỉ rưới sẵn nước sốt thôi. Bởi vì salad sau khi trộn, nếu để quá lâu rau sẽ bị ấu, khi ăn hơi nát dẫn đến hương vị kém ngon.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fae2bf83d-2b03-4a44-ac29-4c41246cc355%2Fimage_2023-03-03_233146482.png22e94cb0-66fc-4515-bbb9-f3c8ea56971c?alt=media&amp;token=dd2ae6e1-ad75-415d-83d5-2876557f9337" alt="image_2023-03-03_233146482.png"></p>
<p>Nếu chưa ăn tới, bạn đừng nên trộn vội, mà chỉ rưới sẵn nước sốt thôi. Bởi vì salad sau khi trộn, nếu để quá lâu rau sẽ bị ấu, khi ăn hơi nát dẫn đến hương vị kém ngon.</p>
', 3, N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'63099ebc-ff7b-41af-89d4-5582ba170538', N'### Sơ chế nguyên liệu

Thịt bò bằm ướp cùng với 1/2 thìa cà phê muối tinh, 1/3 thìa cà phê đường, 1/2 thìa cà phê tiêu xay và 2 thìa canh dầu oliu. Trộn đều và để thịt thấm gia vị khoảng 15 phút.

![image_2023-02-27_215703818.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_215703818.png82fb55fa-dc0c-476f-b3ac-51290681f3ff?alt=media&token=4a2cf529-d9b2-4ed2-99db-3134cf82122b)

Cà chua rửa sạch, xắt hạt lựu nhỏ. Hành tây lột vỏ, rửa sạch rồi xắt hạt lựu tương tự như cà chua. Có thể xắt hành tây bé hơn 1 chút vì hành tây có độ cứng.

Tỏi bóc vỏ, băm nhỏ.

### Xào thịt bò

Cho 1 thìa canh dầu oliu vào chảo, phi thơm nửa số tỏi băm, sau đó cho thịt bò bằm vào xào chín tái rồi tắt bếp, trút thịt ra bát riêng.

![image_2023-02-27_215758052.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_215758052.png64da317f-62f0-4bb8-ab84-91e18ca8d191?alt=media&token=ac37b873-b40e-44bb-9623-d7c563bc50a1)

Phần thịt bò này sẽ cho vào lại khi sốt cà chua gần hoàn thành, giúp cho thịt bò khi ăn không bị khô và dai.', N'<h3>Sơ chế nguyên liệu</h3>
<p>Thịt bò bằm ướp cùng với 1/2 thìa cà phê muối tinh, 1/3 thìa cà phê đường, 1/2 thìa cà phê tiêu xay và 2 thìa canh dầu oliu. Trộn đều và để thịt thấm gia vị khoảng 15 phút.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_215703818.png82fb55fa-dc0c-476f-b3ac-51290681f3ff?alt=media&amp;token=4a2cf529-d9b2-4ed2-99db-3134cf82122b" alt="image_2023-02-27_215703818.png"></p>
<p>Cà chua rửa sạch, xắt hạt lựu nhỏ. Hành tây lột vỏ, rửa sạch rồi xắt hạt lựu tương tự như cà chua. Có thể xắt hành tây bé hơn 1 chút vì hành tây có độ cứng.</p>
<p>Tỏi bóc vỏ, băm nhỏ.</p>
<h3>Xào thịt bò</h3>
<p>Cho 1 thìa canh dầu oliu vào chảo, phi thơm nửa số tỏi băm, sau đó cho thịt bò bằm vào xào chín tái rồi tắt bếp, trút thịt ra bát riêng.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_215758052.png64da317f-62f0-4bb8-ab84-91e18ca8d191?alt=media&amp;token=ac37b873-b40e-44bb-9623-d7c563bc50a1" alt="image_2023-02-27_215758052.png"></p>
<p>Phần thịt bò này sẽ cho vào lại khi sốt cà chua gần hoàn thành, giúp cho thịt bò khi ăn không bị khô và dai.</p>
', 1, N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'd691d089-67a9-471a-80f2-58b41ed1b1d4', N'**Sườn non kho thơm**, hay còn có tên gọi khác là sườn non kho khóm hoặc sườn non kho dứa tùy thuộc vào từng vùng miền. Đây là món ngon từ sườn và hấp dẫn không hề thua kém sườn xào chua ngọt quen thuộc.', N'<p><strong>Sườn non kho thơm</strong>, hay còn có tên gọi khác là sườn non kho khóm hoặc sườn non kho dứa tùy thuộc vào từng vùng miền. Đây là món ngon từ sườn và hấp dẫn không hề thua kém sườn xào chua ngọt quen thuộc.</p>
', 0, N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'1e2e8840-532b-4ae0-9658-597bff33ccc4', N'![image_2023-02-27_181559949.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_181559949.pngdd3992b3-6afd-4052-b784-7f853be7becf?alt=media&token=d4b38e74-9ef6-48d4-8453-bde0a0824317)


### Lưu ý về nguyên liệu

Ngoài ra khi nấu món ăn này, có 3 điểm quan trọng mà Cookbeo cần chia sẻ với các bạn, thứ 1 đó là món thịt này cần có độ nhừ, ngậy, thứ 2 là hương vị mặn ngọt hòa quyện, thơm và thứ 3 là màu sắc của miếng thịt kho.

* Thứ 1, để làm thịt kho tàu, các bạn chọn thịt ba rọi hoặc phần nạc vai mềm có thêm chút mỡ nhá thì khi kho, thịt mới có độ ngậy. Còn về độ nhừ thì thời gian kho lâu thịt sẽ có độ nhừ, mềm như ý.
* Thứ 2, ngoài các gia vị để kho thì cần nhắc đến nước dừa. Thật ra thì tùy vào vùng miền, khẩu vị mà cũng sẽ có cách nấu thịt kho tàu khác nhau, nhất là việc có sử dụng nước dừa hay không. Thì thịt kho tàu như ở miền Nam thì sẽ dùng nước dừa, miền Bắc trước thì sẽ không hay dùng nước dừa.
Tuy nhiên món ăn này bây giờ phổ biến và trở thành 1 món ăn quen thuộc hàng ngày, nên nó đã được biến tấu sao cho phù hợp với khẩu vị của người ăn. Chính vì vậy mà giờ ở miền Bắc khi nấu thịt kho tàu, nhiều người cũng sử dụng nước dừa để kho thịt, nhằm tạo độ ngậy cho thịt cũng như nước kho.
Ngoài ra, dùng nước dừa thì miếng thịt khi kho lâu nó sẽ có màu sắc đậm, rám nâu rất hấp dẫn đấy ạ.Tất nhiên nếu không chuẩn bị được nước dừa, các bạn thay bằng nước lọc nha
* Thứ 3, về màu sắc của thịt kho, đó là để miếng thịt kho tàu nó có màu rám nâu đỏ bóng thì bạn cần thắng đường để tạo màu hoặc dùng nước màu đường nấu sẵn. Bởi vì theo như kinh nghiệm của Cookbeo, nếu chỉ dùng nước dừa để kho thịt thôi, mà không dùng nước màu (nước hàng ấy ạ) thì màu sắc của miếng thịt kho nó sẽ hơi nhạt, thậm chí là nó hơi bợt màu.
Nên nếu bạn nào mà thích ăn miếng thịt kho tàu có cái màu đỏ nâu của thịt nạc, lớp bì heo hầm nhừ có màu nâu cánh gián bóng bẩy thì nên dùng nước màu hoặc là thắng đường. Cách thắng nước màu thì các bạn có thể tham khảo ở video này.
Hoặc các bạn cũng có thể dùng nước hàng đóng sẵn thành chai, tuy nhiên màu nước hàng này nó khá đậm, nên khi cho các bạn nhớ điều chỉnh phù hợp, không là miếng thịt kho sẽ bị sẫm màu, hơi đen đen.
', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_181559949.pngdd3992b3-6afd-4052-b784-7f853be7becf?alt=media&amp;token=d4b38e74-9ef6-48d4-8453-bde0a0824317" alt="image_2023-02-27_181559949.png"></p>
<h3>Lưu ý về nguyên liệu</h3>
<p>Ngoài ra khi nấu món ăn này, có 3 điểm quan trọng mà Cookbeo cần chia sẻ với các bạn, thứ 1 đó là món thịt này cần có độ nhừ, ngậy, thứ 2 là hương vị mặn ngọt hòa quyện, thơm và thứ 3 là màu sắc của miếng thịt kho.</p>
<ul>
<li>Thứ 1, để làm thịt kho tàu, các bạn chọn thịt ba rọi hoặc phần nạc vai mềm có thêm chút mỡ nhá thì khi kho, thịt mới có độ ngậy. Còn về độ nhừ thì thời gian kho lâu thịt sẽ có độ nhừ, mềm như ý.</li>
<li>Thứ 2, ngoài các gia vị để kho thì cần nhắc đến nước dừa. Thật ra thì tùy vào vùng miền, khẩu vị mà cũng sẽ có cách nấu thịt kho tàu khác nhau, nhất là việc có sử dụng nước dừa hay không. Thì thịt kho tàu như ở miền Nam thì sẽ dùng nước dừa, miền Bắc trước thì sẽ không hay dùng nước dừa.
Tuy nhiên món ăn này bây giờ phổ biến và trở thành 1 món ăn quen thuộc hàng ngày, nên nó đã được biến tấu sao cho phù hợp với khẩu vị của người ăn. Chính vì vậy mà giờ ở miền Bắc khi nấu thịt kho tàu, nhiều người cũng sử dụng nước dừa để kho thịt, nhằm tạo độ ngậy cho thịt cũng như nước kho.
Ngoài ra, dùng nước dừa thì miếng thịt khi kho lâu nó sẽ có màu sắc đậm, rám nâu rất hấp dẫn đấy ạ.Tất nhiên nếu không chuẩn bị được nước dừa, các bạn thay bằng nước lọc nha</li>
<li>Thứ 3, về màu sắc của thịt kho, đó là để miếng thịt kho tàu nó có màu rám nâu đỏ bóng thì bạn cần thắng đường để tạo màu hoặc dùng nước màu đường nấu sẵn. Bởi vì theo như kinh nghiệm của Cookbeo, nếu chỉ dùng nước dừa để kho thịt thôi, mà không dùng nước màu (nước hàng ấy ạ) thì màu sắc của miếng thịt kho nó sẽ hơi nhạt, thậm chí là nó hơi bợt màu.
Nên nếu bạn nào mà thích ăn miếng thịt kho tàu có cái màu đỏ nâu của thịt nạc, lớp bì heo hầm nhừ có màu nâu cánh gián bóng bẩy thì nên dùng nước màu hoặc là thắng đường. Cách thắng nước màu thì các bạn có thể tham khảo ở video này.
Hoặc các bạn cũng có thể dùng nước hàng đóng sẵn thành chai, tuy nhiên màu nước hàng này nó khá đậm, nên khi cho các bạn nhớ điều chỉnh phù hợp, không là miếng thịt kho sẽ bị sẫm màu, hơi đen đen.</li>
</ul>
', 1, N'a8ab27eb-4401-4b4b-93b9-bda34397621f', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'70e2f574-9609-4f1c-8f5a-63b017296168', N'Cách nấu **thịt kho tàu** mà Homnayangi chia sẻ dưới đây chắc chắn sẽ khiến các bạn cảm thấy hài lòng với thành phẩm thu được và đặc biệt tự tin hơn khi nấu món ngon này để chiêu đãi gia đình và bạn bè!', N'<p>Cách nấu <strong>thịt kho tàu</strong> mà Homnayangi chia sẻ dưới đây chắc chắn sẽ khiến các bạn cảm thấy hài lòng với thành phẩm thu được và đặc biệt tự tin hơn khi nấu món ngon này để chiêu đãi gia đình và bạn bè!</p>
', 0, N'a8ab27eb-4401-4b4b-93b9-bda34397621f', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'738a3394-1f2c-4ca2-a9e3-643e1a5fa953', N'![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage.png47c875dd-c99e-40e0-b790-825d9b45445f?alt=media&token=9c6b87e5-189c-4a1e-affc-2a7e3f69132b)

Thịt kho mắm ruốc sả ớt ngon, hương vị hấp dẫn và đậm đà, ăn rất hao cơm.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage.png47c875dd-c99e-40e0-b790-825d9b45445f?alt=media&amp;token=9c6b87e5-189c-4a1e-affc-2a7e3f69132b" alt="image.png"></p>
<p>Thịt kho mắm ruốc sả ớt ngon, hương vị hấp dẫn và đậm đà, ăn rất hao cơm.</p>
', 3, N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'66d83fef-d6ec-450f-b08a-64f1de677f44', N'![image_2023-02-28_105031059.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f%2Fimage_2023-02-28_105031059.pngcfbc5352-b66b-4bb4-b3dd-79c78807d48d?alt=media&token=25dfc2a7-b8f3-40f3-b10b-7b0f4fba0100)

Trước khi tắt bếp thì rắc hành lá vào, múc lượng kho quẹt vừa ăn vào bát.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f%2Fimage_2023-02-28_105031059.pngcfbc5352-b66b-4bb4-b3dd-79c78807d48d?alt=media&amp;token=25dfc2a7-b8f3-40f3-b10b-7b0f4fba0100" alt="image_2023-02-28_105031059.png"></p>
<p>Trước khi tắt bếp thì rắc hành lá vào, múc lượng kho quẹt vừa ăn vào bát.</p>
', 3, N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'3d08a50b-534a-4259-9773-68db45081970', N'Cách làm món **bông bí xào tỏi** thơm ngon, xanh mướt mà không bị đắng, hấp dẫn như ngoài hàng. Ngoài cách xào, khi sơ chế bông bí bạn nhớ ngắt bỏ phần nhụy hoa bên trong.', N'<p>Cách làm món <strong>bông bí xào tỏi</strong> thơm ngon, xanh mướt mà không bị đắng, hấp dẫn như ngoài hàng. Ngoài cách xào, khi sơ chế bông bí bạn nhớ ngắt bỏ phần nhụy hoa bên trong.</p>
', 0, N'ac41da30-8998-4a1f-b2e4-291d0b24dcf4', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'97d137d0-640a-4311-99d5-6a583c9f09df', N'Không giống những cách làm khác, cách làm sườn xào chua ngọt này không chiên sườn nên chỉ mất 30 phút để hoàn thành, sườn vẫn mềm và róc xương, thích hợp cho người bận rộn.', N'<p>Không giống những cách làm khác, cách làm sườn xào chua ngọt này không chiên sườn nên chỉ mất 30 phút để hoàn thành, sườn vẫn mềm và róc xương, thích hợp cho người bận rộn.</p>
', 0, N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'f76463cb-fb2e-4c65-9b71-6a59275cb309', N'
### Chế biến cá lóc

![image_2023-02-27_213154807.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_213154807.pngb164ad78-a9fd-44f1-b2c7-da8c0892c0b7?alt=media&token=cb3582e8-401d-471b-9f02-7a1a23b36c7e)

Dùng cán dao đập vào đầu cá lóc, cá hết quẫy thì đánh vẩy, bỏ vây bỏ mang. Rạch 1 đường dọc dưới bụng cá, moi ruột, cạo sạch màng đen. Xát muối hạt thật kỹ toàn thân cá để làm sạch, khử nhớt và mùi tanh cá. Sau đó rửa sạch lại, để ráo nước rồi đem thái từng khúc nhỏ.

### Sơ chế các nguyên liệu khác

Lá gừng rửa sạch, cắt khúc dài tầm 5-7 cm. Lá gừng bạn có thể mua ở các sạp hàng rau, giá thành rất rẻ, thậm chí rẻ hơn so với việc mua củ gừng. Giá lá gừng (có kèm luôn cả củ nhỏ) bán ở chợ Hà Nội tầm 6 ngìn đồng/bó gồm 5 nhánh. 

Hoặc bạn cũng có thể tự trồng lấy bằng cách vùi củ gừng xuống dưới đất, 2-3 ngày tưới đất ẩm 1 lần, sau 2 tuần cây sẽ nhú mầm cao tầm 3cm.

Hành khô và tỏi bóc vỏ, băm nhỏ. Gừng rửa sạch, đập dập băm nhỏ.

Thịt ba chỉ xóc muối hạt rửa sạch, thái miếng dày 1 phân, để ráo rồi ướp cùng với 1 thìa cà phê nước mắm.

### Ướp cá lóc

![image_2023-02-27_213324078.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_213324078.png544d316c-48fd-4631-b0fd-1a06ae3a57d2?alt=media&token=8eaa74b1-3d89-4fb9-b786-08bfe2450c3f)

Cá lóc ướp cùng với 2 muỗng nước mắm, 1/2 thìa canh đường, 1 thìa canh nước màu, 1 thìa cà phê hạt tiêu xay, 1 thìa cà phê bột ngọt, 1 thìa canh bột nghệ cùng với 1 ít gừng, hành tỏi băm. Trộn đều lên và để cá thấm gia vị, thời gian ướp cá ít nhất trong khoảng 30 phút.

### Chiên sơ cá lóc

Sau khi ướp cá, bạn mang đi chiên sơ qua. Mục đích của việc chiên giúp cá săn chắc, không bị vỡ nát trong quá trình kho. Đồng thời, cá sẽ thơm hơn và có màu vàng ánh nâu đẹp mắt.

![image_2023-02-27_213401855.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_213401855.pnga30d3535-18b7-4d17-979e-89813f02b9c0?alt=media&token=e0ad9beb-c14a-4915-8207-af06a5a673b8)

Cho 2 thìa canh dầu ăn vào chảo, làm nóng dầu thì phi thơm nốt số hành, tỏi và gừng băm còn lại, tiếp đến cho cá vào chiên sơ. Lật đều để các mặt cá săn lại.', N'<h3>Chế biến cá lóc</h3>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_213154807.pngb164ad78-a9fd-44f1-b2c7-da8c0892c0b7?alt=media&amp;token=cb3582e8-401d-471b-9f02-7a1a23b36c7e" alt="image_2023-02-27_213154807.png"></p>
<p>Dùng cán dao đập vào đầu cá lóc, cá hết quẫy thì đánh vẩy, bỏ vây bỏ mang. Rạch 1 đường dọc dưới bụng cá, moi ruột, cạo sạch màng đen. Xát muối hạt thật kỹ toàn thân cá để làm sạch, khử nhớt và mùi tanh cá. Sau đó rửa sạch lại, để ráo nước rồi đem thái từng khúc nhỏ.</p>
<h3>Sơ chế các nguyên liệu khác</h3>
<p>Lá gừng rửa sạch, cắt khúc dài tầm 5-7 cm. Lá gừng bạn có thể mua ở các sạp hàng rau, giá thành rất rẻ, thậm chí rẻ hơn so với việc mua củ gừng. Giá lá gừng (có kèm luôn cả củ nhỏ) bán ở chợ Hà Nội tầm 6 ngìn đồng/bó gồm 5 nhánh.</p>
<p>Hoặc bạn cũng có thể tự trồng lấy bằng cách vùi củ gừng xuống dưới đất, 2-3 ngày tưới đất ẩm 1 lần, sau 2 tuần cây sẽ nhú mầm cao tầm 3cm.</p>
<p>Hành khô và tỏi bóc vỏ, băm nhỏ. Gừng rửa sạch, đập dập băm nhỏ.</p>
<p>Thịt ba chỉ xóc muối hạt rửa sạch, thái miếng dày 1 phân, để ráo rồi ướp cùng với 1 thìa cà phê nước mắm.</p>
<h3>Ướp cá lóc</h3>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_213324078.png544d316c-48fd-4631-b0fd-1a06ae3a57d2?alt=media&amp;token=8eaa74b1-3d89-4fb9-b786-08bfe2450c3f" alt="image_2023-02-27_213324078.png"></p>
<p>Cá lóc ướp cùng với 2 muỗng nước mắm, 1/2 thìa canh đường, 1 thìa canh nước màu, 1 thìa cà phê hạt tiêu xay, 1 thìa cà phê bột ngọt, 1 thìa canh bột nghệ cùng với 1 ít gừng, hành tỏi băm. Trộn đều lên và để cá thấm gia vị, thời gian ướp cá ít nhất trong khoảng 30 phút.</p>
<h3>Chiên sơ cá lóc</h3>
<p>Sau khi ướp cá, bạn mang đi chiên sơ qua. Mục đích của việc chiên giúp cá săn chắc, không bị vỡ nát trong quá trình kho. Đồng thời, cá sẽ thơm hơn và có màu vàng ánh nâu đẹp mắt.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_213401855.pnga30d3535-18b7-4d17-979e-89813f02b9c0?alt=media&amp;token=e0ad9beb-c14a-4915-8207-af06a5a673b8" alt="image_2023-02-27_213401855.png"></p>
<p>Cho 2 thìa canh dầu ăn vào chảo, làm nóng dầu thì phi thơm nốt số hành, tỏi và gừng băm còn lại, tiếp đến cho cá vào chiên sơ. Lật đều để các mặt cá săn lại.</p>
', 1, N'b9f39ecc-c689-4910-ac82-816e20994156', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'88f95b67-9dce-4d04-9319-6ecb666c6d94', N'![image_2023-02-28_174003437.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234%2Fimage_2023-02-28_174003437.png8b19cfef-4b8d-4ec4-8714-f84846568e0e?alt=media&token=40f9b1f8-7027-4188-8688-8fafc066c0b1)

Cho tất cả các nguyên liệu trên vào tô to, sau đó rưới nước sốt mè rang lên. Với lượng nguyên liệu như trên thì tầm 3 thìa canh nước sốt mè rang là vừa. Nếu muốn ăn đậm hơn, bạn có thể dùng thêm nước sốt.

Đeo bao tay vào và nhẹ nhàng trộn để rau và các loại củ quả thấm đều nước sốt. Sau đó cho salad rau ra đĩa và thưởng thức.

Salad rau, củ quả trộn nước sốt mè rang rất thơm, thanh mát.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234%2Fimage_2023-02-28_174003437.png8b19cfef-4b8d-4ec4-8714-f84846568e0e?alt=media&amp;token=40f9b1f8-7027-4188-8688-8fafc066c0b1" alt="image_2023-02-28_174003437.png"></p>
<p>Cho tất cả các nguyên liệu trên vào tô to, sau đó rưới nước sốt mè rang lên. Với lượng nguyên liệu như trên thì tầm 3 thìa canh nước sốt mè rang là vừa. Nếu muốn ăn đậm hơn, bạn có thể dùng thêm nước sốt.</p>
<p>Đeo bao tay vào và nhẹ nhàng trộn để rau và các loại củ quả thấm đều nước sốt. Sau đó cho salad rau ra đĩa và thưởng thức.</p>
<p>Salad rau, củ quả trộn nước sốt mè rang rất thơm, thanh mát.</p>
', 2, N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'a118d699-67fc-4bdb-8614-6ee5176aedd8', N'Cách làm **bánh ngào Nghệ An** thơm phức mùi nếp, ấm nồng vị gừng và ngọt ngào của mật mía sẽ được chia sẻ chi tiết trong bài viết này. Đây là món bánh không quá cầu kỳ trong khâu thực hiện nhưng thành phẩm thì chắc chắn sẽ chinh phục được bạn ngay từ lần thưởng thức đầu tiên.', N'<p>Cách làm <strong>bánh ngào Nghệ An</strong> thơm phức mùi nếp, ấm nồng vị gừng và ngọt ngào của mật mía sẽ được chia sẻ chi tiết trong bài viết này. Đây là món bánh không quá cầu kỳ trong khâu thực hiện nhưng thành phẩm thì chắc chắn sẽ chinh phục được bạn ngay từ lần thưởng thức đầu tiên.</p>
', 0, N'956f6fc0-72e1-4204-852b-73cff12eae37', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'53993c1f-05e4-4e55-9efb-7846a3b1f3c8', N'![image_2023-02-28_173944943.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234%2Fimage_2023-02-28_173944943.png700d8020-82bb-49ad-84b5-9c051d1475dd?alt=media&token=a0e49367-8f1f-4bcd-a628-9230836b2a55)

Salad là hình thức trộn lẫn các loại nguyên liệu lại với nhau, trong đó chủ yếu là rau, củ quả tươi sống. Chính vì vậy, khâu sơ chế rau củ quả cần được thực hiện cẩn thận để đảm bảo vệ sinh. Nếu có điều kiện, nên chọn mua các loại rau củ hữu cơ để rau củ có chất lượng tốt nhất.

Rau xà lách tách bỏ bẹ lá, rửa qua rồi ngâm với nước muối pha loãng khoảng 10 phút, sau đó vớt ra, rửa sạch lại, để ráo nước rồi cắt thành từng khúc khoảng 2cm.

Bắp cải tím ngâm nước muối loãng, rửa sạch rồi nạo thành từng sợi nhỏ, để ráo.

Cà chua bi ngâm nước muối, rửa sạch, ráo nước thì bổ làm đôi.

Táo rửa rồi gọt vỏ, xắt hạt lựu. Nếu để vỏ thì nên ngâm qua nước muối.

Củ đậu rửa sạch, lột vỏ, cũng xắt hạt lựu giống như táo.

Dưa chuột ngâm nước muối, nạo vỏ hoặc để vỏ tùy thích, xắt nhỏ tương tự như táo và củ đậu.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234%2Fimage_2023-02-28_173944943.png700d8020-82bb-49ad-84b5-9c051d1475dd?alt=media&amp;token=a0e49367-8f1f-4bcd-a628-9230836b2a55" alt="image_2023-02-28_173944943.png"></p>
<p>Salad là hình thức trộn lẫn các loại nguyên liệu lại với nhau, trong đó chủ yếu là rau, củ quả tươi sống. Chính vì vậy, khâu sơ chế rau củ quả cần được thực hiện cẩn thận để đảm bảo vệ sinh. Nếu có điều kiện, nên chọn mua các loại rau củ hữu cơ để rau củ có chất lượng tốt nhất.</p>
<p>Rau xà lách tách bỏ bẹ lá, rửa qua rồi ngâm với nước muối pha loãng khoảng 10 phút, sau đó vớt ra, rửa sạch lại, để ráo nước rồi cắt thành từng khúc khoảng 2cm.</p>
<p>Bắp cải tím ngâm nước muối loãng, rửa sạch rồi nạo thành từng sợi nhỏ, để ráo.</p>
<p>Cà chua bi ngâm nước muối, rửa sạch, ráo nước thì bổ làm đôi.</p>
<p>Táo rửa rồi gọt vỏ, xắt hạt lựu. Nếu để vỏ thì nên ngâm qua nước muối.</p>
<p>Củ đậu rửa sạch, lột vỏ, cũng xắt hạt lựu giống như táo.</p>
<p>Dưa chuột ngâm nước muối, nạo vỏ hoặc để vỏ tùy thích, xắt nhỏ tương tự như táo và củ đậu.</p>
', 1, N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'5988c37d-3520-4015-9807-79fcb80b4280', N'Rau muống nhặt lấy cọng non, ngắt thành từng đoạn dài tầm 4-5 cm. Ngâm rau trong nước muối pha loãng tầm 3 phút rồi rửa sạch lại, để ráo.

Tỏi bóc vỏ, đập dập, nếu băm chỉ nên băm thô, không nên băm quá nhỏ, khi xào ở lửa lớn tỏi dễ bị cháy và gây đắng.

Gừng cắt những phần bị thâm đen, héo úa rồi rửa sạch, đập dập, băm nhỏ.

Mì tôm cho vào bát, để riêng các gói gia vị. Nấu 1 nồi nước sôi rồi dội lên để chần mì. Nếu muốn ăn thật giòn thì bạn chỉ cần chần mì tầm 20 giây, sau đó vớt ra để ráo. Còn nếu muốn ăn mềm hơn thì chần lâu hơn 1 chút nhưng lưu ý không chần mì quá chín.

![image_2023-02-27_223525965.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223525965.pnga79350c9-d517-46c5-94b3-af79f8ae1c06?alt=media&token=5643fa4f-5a5e-4cf0-b5d4-3abf9219f12c)

![image_2023-02-27_223600481.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223600481.png89883648-2222-4d4e-89f1-694c9376d68e?alt=media&token=2fa3af8e-02a3-4d8a-88af-10d76263c630)

Thịt bò thái miếng mỏng, ướp cùng với 1/2 thìa canh dầu hào, 1/2 thìa canh đường, 1 thìa canh dầu ăn, 1/2 thìa cà phê bột nêm, 1/2 thìa cà phê bột canh, 1/2 thìa cà phê bột ngọt, 1/2 thìa cà phê tiêu xay, 1 ít tỏi, gừng băm. Trộn đều và để thịt bò thấm gia vị trong 20-30 phút.

', N'<p>Rau muống nhặt lấy cọng non, ngắt thành từng đoạn dài tầm 4-5 cm. Ngâm rau trong nước muối pha loãng tầm 3 phút rồi rửa sạch lại, để ráo.</p>
<p>Tỏi bóc vỏ, đập dập, nếu băm chỉ nên băm thô, không nên băm quá nhỏ, khi xào ở lửa lớn tỏi dễ bị cháy và gây đắng.</p>
<p>Gừng cắt những phần bị thâm đen, héo úa rồi rửa sạch, đập dập, băm nhỏ.</p>
<p>Mì tôm cho vào bát, để riêng các gói gia vị. Nấu 1 nồi nước sôi rồi dội lên để chần mì. Nếu muốn ăn thật giòn thì bạn chỉ cần chần mì tầm 20 giây, sau đó vớt ra để ráo. Còn nếu muốn ăn mềm hơn thì chần lâu hơn 1 chút nhưng lưu ý không chần mì quá chín.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223525965.pnga79350c9-d517-46c5-94b3-af79f8ae1c06?alt=media&amp;token=5643fa4f-5a5e-4cf0-b5d4-3abf9219f12c" alt="image_2023-02-27_223525965.png"></p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_223600481.png89883648-2222-4d4e-89f1-694c9376d68e?alt=media&amp;token=2fa3af8e-02a3-4d8a-88af-10d76263c630" alt="image_2023-02-27_223600481.png"></p>
<p>Thịt bò thái miếng mỏng, ướp cùng với 1/2 thìa canh dầu hào, 1/2 thìa canh đường, 1 thìa canh dầu ăn, 1/2 thìa cà phê bột nêm, 1/2 thìa cà phê bột canh, 1/2 thìa cà phê bột ngọt, 1/2 thìa cà phê tiêu xay, 1 ít tỏi, gừng băm. Trộn đều và để thịt bò thấm gia vị trong 20-30 phút.</p>
', 1, N'b5603c80-4003-41f9-a7a5-3076de517599', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'4862fd71-103a-447e-a4f5-7dca13ce642c', N'**Canh chua cá lóc** là món ăn dân dã nhưng nếu đã một lần được thưởng thức sẽ khiến người ta cảm thấy nhớ mãi hương vị ngọt dịu của cá lóc, chua thanh của cà chua, dứa, trái bắp, chút giòn dai của bạc hà quyện với mùi thơm của rau ngổ, ngò và thêm chút cay nồng của ớt, bùi bùi của tỏi phi.🍴', N'<p><strong>Canh chua cá lóc</strong> là món ăn dân dã nhưng nếu đã một lần được thưởng thức sẽ khiến người ta cảm thấy nhớ mãi hương vị ngọt dịu của cá lóc, chua thanh của cà chua, dứa, trái bắp, chút giòn dai của bạc hà quyện với mùi thơm của rau ngổ, ngò và thêm chút cay nồng của ớt, bùi bùi của tỏi phi.🍴</p>
', 0, N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'e9640682-2070-4a7c-a5b2-7f71237506f9', N'![image_2023-02-28_105000156.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f%2Fimage_2023-02-28_105000156.png212210f7-39df-4d6d-950e-b9088860a363?alt=media&token=5ff8684d-abf6-4b0d-a93c-1cb42e3e542a)

Bắc nồi lên bếp, làm nóng nồi thì cho thịt ba chỉ vào xào, đảo đều tay để thịt săn lại và cháy vàng đều các mặt.

Khi thịt đã xém vàng các mặt, các bạn vớt ra để vào 1 bát riêng. Chắt bớt phần mỡ, chỉ để lại 1 ít để nấu kho quẹt thôi.

Cho hành tỏi băm vào phi thơm, sau đó cho tôm khô vào xào cùng trong khoảng 2 phút.

Tiếp đến cho thịt ba chỉ, đảo đều tay rồi đổ hỗn hợp nước mắm đường vừa pha vào. Hạ bớt nhiệt độ, kho liu riu trong khoảng 2-3 phút.

Để mắm kho quẹt có màu đẹp mắt thì các bạn cho thêm 1 ít tương ớt, khuấy đều rồi cho tiêu đen, tiêu xanh và ớt vào. Tiếp tục nấu thêm 3-4 phút nữa, đến khi thấy nước kho quẹt sánh lại, sền sệt đúng ý là được.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f%2Fimage_2023-02-28_105000156.png212210f7-39df-4d6d-950e-b9088860a363?alt=media&amp;token=5ff8684d-abf6-4b0d-a93c-1cb42e3e542a" alt="image_2023-02-28_105000156.png"></p>
<p>Bắc nồi lên bếp, làm nóng nồi thì cho thịt ba chỉ vào xào, đảo đều tay để thịt săn lại và cháy vàng đều các mặt.</p>
<p>Khi thịt đã xém vàng các mặt, các bạn vớt ra để vào 1 bát riêng. Chắt bớt phần mỡ, chỉ để lại 1 ít để nấu kho quẹt thôi.</p>
<p>Cho hành tỏi băm vào phi thơm, sau đó cho tôm khô vào xào cùng trong khoảng 2 phút.</p>
<p>Tiếp đến cho thịt ba chỉ, đảo đều tay rồi đổ hỗn hợp nước mắm đường vừa pha vào. Hạ bớt nhiệt độ, kho liu riu trong khoảng 2-3 phút.</p>
<p>Để mắm kho quẹt có màu đẹp mắt thì các bạn cho thêm 1 ít tương ớt, khuấy đều rồi cho tiêu đen, tiêu xanh và ớt vào. Tiếp tục nấu thêm 3-4 phút nữa, đến khi thấy nước kho quẹt sánh lại, sền sệt đúng ý là được.</p>
', 2, N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'42745188-36ca-4963-8cb4-800f8cc4fb3f', N'### Nặn bánh ngào

Trước khi nặn bánh, bạn nhồi lại 1 lần nữa để bột dẻo và có độ kết dính. Sau đó véo 1 nhúm bột nhỏ, vo thành viên hình tròn, hoặc hình tròn rồi ấn dẹt xuống, hay là hình viên kén.

![image_2023-02-28_172636131.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F956f6fc0-72e1-4204-852b-73cff12eae37%2Fimage_2023-02-28_172636131.png4136d7cc-833c-438d-a3f2-5222818bfba9?alt=media&token=bc7163c3-19de-43c4-94c0-71bff5378271)

Nấu 1 nồi nước, khi nước sôi bạn thả viên bánh vào để luộc ở lửa vừa. Khi chín, bánh sẽ nổi lên trên. Tiếp tục nấu thêm khoảng 1-2 phút rồi vớt bánh ra ngâm vào 1 bát nước để bánh ngào không bị dính vào nhau.

![image_2023-02-28_172649315.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F956f6fc0-72e1-4204-852b-73cff12eae37%2Fimage_2023-02-28_172649315.pnge6a891fd-9360-4aa1-b148-cc3f460857e0?alt=media&token=32cb9829-6535-4801-81db-935ea1735cad)

Tiếp đến, bạn cho 550ml nước lọc, 200ml mật mía và gừng tươi thái sợi vào nồi, khuấy đều và nấu ở lửa vừa. Khi mật sôi, bạn nấu thêm khoảng hơn 1 phút để mật có độ sánh rồi thả bánh vào để ngào trong 2-3 phút.

![image_2023-02-28_172703665.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F956f6fc0-72e1-4204-852b-73cff12eae37%2Fimage_2023-02-28_172703665.png279cf01d-4eeb-425e-b60b-bd83890c4880?alt=media&token=c1605429-baa8-468f-8f3b-d60a552343f9)', N'<h3>Nặn bánh ngào</h3>
<p>Trước khi nặn bánh, bạn nhồi lại 1 lần nữa để bột dẻo và có độ kết dính. Sau đó véo 1 nhúm bột nhỏ, vo thành viên hình tròn, hoặc hình tròn rồi ấn dẹt xuống, hay là hình viên kén.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F956f6fc0-72e1-4204-852b-73cff12eae37%2Fimage_2023-02-28_172636131.png4136d7cc-833c-438d-a3f2-5222818bfba9?alt=media&amp;token=bc7163c3-19de-43c4-94c0-71bff5378271" alt="image_2023-02-28_172636131.png"></p>
<p>Nấu 1 nồi nước, khi nước sôi bạn thả viên bánh vào để luộc ở lửa vừa. Khi chín, bánh sẽ nổi lên trên. Tiếp tục nấu thêm khoảng 1-2 phút rồi vớt bánh ra ngâm vào 1 bát nước để bánh ngào không bị dính vào nhau.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F956f6fc0-72e1-4204-852b-73cff12eae37%2Fimage_2023-02-28_172649315.pnge6a891fd-9360-4aa1-b148-cc3f460857e0?alt=media&amp;token=32cb9829-6535-4801-81db-935ea1735cad" alt="image_2023-02-28_172649315.png"></p>
<p>Tiếp đến, bạn cho 550ml nước lọc, 200ml mật mía và gừng tươi thái sợi vào nồi, khuấy đều và nấu ở lửa vừa. Khi mật sôi, bạn nấu thêm khoảng hơn 1 phút để mật có độ sánh rồi thả bánh vào để ngào trong 2-3 phút.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F956f6fc0-72e1-4204-852b-73cff12eae37%2Fimage_2023-02-28_172703665.png279cf01d-4eeb-425e-b60b-bd83890c4880?alt=media&amp;token=c1605429-baa8-468f-8f3b-d60a552343f9" alt="image_2023-02-28_172703665.png"></p>
', 2, N'956f6fc0-72e1-4204-852b-73cff12eae37', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'9b1c0328-6c0a-424c-bf2f-85274d9bd04d', N'Bông bí cắt bỏ bớt phần cọng già, chỉ lấy phần đọt non. Dùng dao tước bỏ lớp vỏ lụa bên ngoài cọng bí để ăn không bị dai. 

Tước xong ngâm bông bí trong nước vo gạo hoặc nước muối pha loãng khoảng 10 phút rồi rửa sạch lại, để ráo.

![image_2023-03-03_233609866.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fac41da30-8998-4a1f-b2e4-291d0b24dcf4%2Fimage_2023-03-03_233609866.pngdabc5aa4-2af9-41be-a5a2-cab6eb2095ab?alt=media&token=2b24e2a1-b1bf-45a9-b7f5-8611e4a73153)

Nên chọn những bông bí cuống còn xanh, dùng tay bấm thử thấy dễ là cọng non, nếu dai và khó bấm là cọng già. Ngoài ra, những bông bí nở to sẽ có độ ngọt hơn là còn đang nụ. 

Tỏi bóc vỏ, đập dập.

Khi thực hiện các món rau xào như rau muống xào tỏi, rau lang xào tỏi... không nên băm tỏi quá nhuyễn. Bởi vì để xào rau xanh thì thường sẽ nấu ở lửa lớn và tỏi băm nhỏ sẽ rất dễ bị cháy, gây đắng khi ăn. Chính vì vậy, chỉ nên đập dập để tỏi vừa tiết ra tinh dầu, vừa không bị cháy xém khi xào.', N'<p>Bông bí cắt bỏ bớt phần cọng già, chỉ lấy phần đọt non. Dùng dao tước bỏ lớp vỏ lụa bên ngoài cọng bí để ăn không bị dai.</p>
<p>Tước xong ngâm bông bí trong nước vo gạo hoặc nước muối pha loãng khoảng 10 phút rồi rửa sạch lại, để ráo.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fac41da30-8998-4a1f-b2e4-291d0b24dcf4%2Fimage_2023-03-03_233609866.pngdabc5aa4-2af9-41be-a5a2-cab6eb2095ab?alt=media&amp;token=2b24e2a1-b1bf-45a9-b7f5-8611e4a73153" alt="image_2023-03-03_233609866.png"></p>
<p>Nên chọn những bông bí cuống còn xanh, dùng tay bấm thử thấy dễ là cọng non, nếu dai và khó bấm là cọng già. Ngoài ra, những bông bí nở to sẽ có độ ngọt hơn là còn đang nụ.</p>
<p>Tỏi bóc vỏ, đập dập.</p>
<p>Khi thực hiện các món rau xào như rau muống xào tỏi, rau lang xào tỏi... không nên băm tỏi quá nhuyễn. Bởi vì để xào rau xanh thì thường sẽ nấu ở lửa lớn và tỏi băm nhỏ sẽ rất dễ bị cháy, gây đắng khi ăn. Chính vì vậy, chỉ nên đập dập để tỏi vừa tiết ra tinh dầu, vừa không bị cháy xém khi xào.</p>
', 1, N'ac41da30-8998-4a1f-b2e4-291d0b24dcf4', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'bf02f545-3618-4396-bfa3-868478ae38bc', N'**Cà pháo chấm mắm tôm** là món ăn dân dã, chứa đựng cả bầu trời ký ức của rất nhiều người, đặc biệt đối với thế hệ 8x trở về trước.', N'<p><strong>Cà pháo chấm mắm tôm</strong> là món ăn dân dã, chứa đựng cả bầu trời ký ức của rất nhiều người, đặc biệt đối với thế hệ 8x trở về trước.</p>
', 0, N'047935c2-9a60-4b0e-865a-89c5e1a680e5', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'cd729488-acf1-4c39-a1ef-8700ef27086c', N'![image_2023-03-03_233646431.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fac41da30-8998-4a1f-b2e4-291d0b24dcf4%2Fimage_2023-03-03_233646431.pngaca95f1b-f242-4dcb-bb77-aa900db262d9?alt=media&token=ccc691d7-13af-4fc3-bfff-12f1e72fc94e)', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fac41da30-8998-4a1f-b2e4-291d0b24dcf4%2Fimage_2023-03-03_233646431.pngaca95f1b-f242-4dcb-bb77-aa900db262d9?alt=media&amp;token=ccc691d7-13af-4fc3-bfff-12f1e72fc94e" alt="image_2023-03-03_233646431.png"></p>
', 3, N'ac41da30-8998-4a1f-b2e4-291d0b24dcf4', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'12ad021b-1c35-40e7-a4a8-8f3f682edd73', N'![image_2023-02-27_174037132.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fdc7234fa-21fd-45bc-9612-f4fb5e2f4f8b%2Fimage_2023-02-27_174037132.pngd14afb2d-9ff6-400f-a26a-a33c025bf12c?alt=media&token=dc7b1517-018c-4408-b683-3e079694f2e1)

Cho 2-3 thìa dầu ăn vào chảo, làm nóng dầu thì cho tỏi vào phi thơm ở nhiệt độ vừa phải để tránh cháy tỏi. Sau khi tỏi đã thơm thì cho 1 ít nước lã vào (khoảng 2ml nước), đảo đều mấy giây thì cho rau muống vào xào. 

Lúc này tăng nhiệt độ lớn hơn 1 chút, đảo đều tay đến khi rau muống ngót lại thì nêm gia vị gồm bột canh, bột ngọt cho vừa ăn. Xào rau ở lửa lớn khoảng 2 phút thì tắt bếp, rắc ít hạt tiêu lên, trộn đều rồi cho ra đĩa.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fdc7234fa-21fd-45bc-9612-f4fb5e2f4f8b%2Fimage_2023-02-27_174037132.pngd14afb2d-9ff6-400f-a26a-a33c025bf12c?alt=media&amp;token=dc7b1517-018c-4408-b683-3e079694f2e1" alt="image_2023-02-27_174037132.png"></p>
<p>Cho 2-3 thìa dầu ăn vào chảo, làm nóng dầu thì cho tỏi vào phi thơm ở nhiệt độ vừa phải để tránh cháy tỏi. Sau khi tỏi đã thơm thì cho 1 ít nước lã vào (khoảng 2ml nước), đảo đều mấy giây thì cho rau muống vào xào.</p>
<p>Lúc này tăng nhiệt độ lớn hơn 1 chút, đảo đều tay đến khi rau muống ngót lại thì nêm gia vị gồm bột canh, bột ngọt cho vừa ăn. Xào rau ở lửa lớn khoảng 2 phút thì tắt bếp, rắc ít hạt tiêu lên, trộn đều rồi cho ra đĩa.</p>
', 2, N'dc7234fa-21fd-45bc-9612-f4fb5e2f4f8b', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'ba6dbb07-1fb6-472e-865d-8f688022442b', N'![image_2023-02-28_111947255.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage_2023-02-28_111947255.png6bc6cf26-21d7-4b05-a730-261cb4b83275?alt=media&token=931b96f0-6394-4d25-a065-d5cfbd34d22c)

Miến rửa qua, ngâm trong nước lạnh ít nhất 30 phút. Nhiều người vội nên thường ngâm nước ấm, tuy nhiên sẽ khiến miến nở mềm ra, khi cho vào nhân nem sẽ bị chảy nát, đây là nguyên nhân làm nem rán bị ỉu. Tốt nhất là ngâm nước lạnh, sau đó vớt ra, để thật ráo.

Mộc nhĩ, nấm hương ngâm nước hơi ấm ấm, khi nở mềm thì phải rửa thật sạch, để ráo. 

Giá đỗ ngâm nước muối loãng, rửa sạch rồi cho ra rổ ráo nước.

Hành lá nhặt rửa sạch, xắt nhỏ.

Cà rốt gọt vỏ, rửa sạch, 2/3 dùng để thái sợi, xắt hạt lựu nhỏ để làm nhân nem. 1/3 củ còn lại tỉa hoa để dùng làm nước chấm nem.

Đu đủ xanh nạo vỏ, chỉ cắt lấy 1/4 quả, gọt phần ruột đi, rửa rồi ngâm qua nước muối loãng tầm 10 phút. Sau đó vớt ra, để ráo thì cắt tỉa hoa giống như cà rốt. Phần đu đủ này cũng dùng để làm nước chấm nem rán.

Tỏi bóc vỏ, băm nhỏ. Ớt tươi băm nhỏ.

Rau sống, rau thơm nhặt rửa sạch, ngâm nước muối, để thật ráo.

Bún chần qua nước sôi để khử vị chua, để ráo rồi cho ra tô hoặc đĩa.

### Trộn nhân nem rán

Cho thịt heo xay vào tô lớn, cà rốt xắt lựu nhỏ vào. Băm nhỏ miến, mộc nhĩ nấm hương và giá đỗ cho vào. Tiếp đến là hành lá. Trứng gà đập tách lấy lòng đỏ cho vào tô nhân nem, giữ lại phần lòng trắng riêng 1 bát nhỏ. Phần lòng trắng này sẽ dùng để phết vào lá nem, giúp nem dính và cuốn chặt hơn.

![image_2023-02-28_112036484.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage_2023-02-28_112036484.pngb852b893-7515-4844-807b-48fdedb73923?alt=media&token=c675d6bb-1c8a-48de-8f9b-5d87d5eda1f0)

![image_2023-02-28_112047034.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage_2023-02-28_112047034.png682abfeb-89a8-465e-8622-e09957f8b615?alt=media&token=507e1d6d-9aa8-4040-826b-fc0378f81bfb)

Ướp nhân nem cùng với 1/3 thìa cà phê muối, 1 thìa cà phê bột ngọt, 1/2 đến 1 thìa cà phê hạt tiêu xay. Trộn đều và để nhân nem thấm gia vị trong khoảng 15-20 phút.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage_2023-02-28_111947255.png6bc6cf26-21d7-4b05-a730-261cb4b83275?alt=media&amp;token=931b96f0-6394-4d25-a065-d5cfbd34d22c" alt="image_2023-02-28_111947255.png"></p>
<p>Miến rửa qua, ngâm trong nước lạnh ít nhất 30 phút. Nhiều người vội nên thường ngâm nước ấm, tuy nhiên sẽ khiến miến nở mềm ra, khi cho vào nhân nem sẽ bị chảy nát, đây là nguyên nhân làm nem rán bị ỉu. Tốt nhất là ngâm nước lạnh, sau đó vớt ra, để thật ráo.</p>
<p>Mộc nhĩ, nấm hương ngâm nước hơi ấm ấm, khi nở mềm thì phải rửa thật sạch, để ráo.</p>
<p>Giá đỗ ngâm nước muối loãng, rửa sạch rồi cho ra rổ ráo nước.</p>
<p>Hành lá nhặt rửa sạch, xắt nhỏ.</p>
<p>Cà rốt gọt vỏ, rửa sạch, 2/3 dùng để thái sợi, xắt hạt lựu nhỏ để làm nhân nem. 1/3 củ còn lại tỉa hoa để dùng làm nước chấm nem.</p>
<p>Đu đủ xanh nạo vỏ, chỉ cắt lấy 1/4 quả, gọt phần ruột đi, rửa rồi ngâm qua nước muối loãng tầm 10 phút. Sau đó vớt ra, để ráo thì cắt tỉa hoa giống như cà rốt. Phần đu đủ này cũng dùng để làm nước chấm nem rán.</p>
<p>Tỏi bóc vỏ, băm nhỏ. Ớt tươi băm nhỏ.</p>
<p>Rau sống, rau thơm nhặt rửa sạch, ngâm nước muối, để thật ráo.</p>
<p>Bún chần qua nước sôi để khử vị chua, để ráo rồi cho ra tô hoặc đĩa.</p>
<h3>Trộn nhân nem rán</h3>
<p>Cho thịt heo xay vào tô lớn, cà rốt xắt lựu nhỏ vào. Băm nhỏ miến, mộc nhĩ nấm hương và giá đỗ cho vào. Tiếp đến là hành lá. Trứng gà đập tách lấy lòng đỏ cho vào tô nhân nem, giữ lại phần lòng trắng riêng 1 bát nhỏ. Phần lòng trắng này sẽ dùng để phết vào lá nem, giúp nem dính và cuốn chặt hơn.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage_2023-02-28_112036484.pngb852b893-7515-4844-807b-48fdedb73923?alt=media&amp;token=c675d6bb-1c8a-48de-8f9b-5d87d5eda1f0" alt="image_2023-02-28_112036484.png"></p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage_2023-02-28_112047034.png682abfeb-89a8-465e-8622-e09957f8b615?alt=media&amp;token=507e1d6d-9aa8-4040-826b-fc0378f81bfb" alt="image_2023-02-28_112047034.png"></p>
<p>Ướp nhân nem cùng với 1/3 thìa cà phê muối, 1 thìa cà phê bột ngọt, 1/2 đến 1 thìa cà phê hạt tiêu xay. Trộn đều và để nhân nem thấm gia vị trong khoảng 15-20 phút.</p>
', 1, N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'45042745-117d-41ea-a265-90d32d30ba74', N'### Sơ chế thịt

Thịt ba rọi bóp với muối hạt, rửa sạch rồi để ráo, thái con chì, có độ dày khoảng 1,5 phân. Để làm món thịt kho mắm ruốc, bạn không nên thái miếng quá to như thịt kho tàu. Chỉ cần thái miếng con chì vừa ăn, dễ thấm gia vị hơn.

Thịt sau khi thái ướp cùng với 1 thìa cà phê nước mắm, 1 thìa cà phê tiêu xay, 1/2 thìa cà phê bột ngọt và 1/2 thìa canh nước màu. Trộn đều và để thịt thấm trong khoảng 20-30 phút.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage.pngd00e5f23-9d60-40b4-9f9e-bb2999d3f07c?alt=media&token=e53fb7e7-bd1a-4ed1-a060-a33b2f224af6)

### Sơ chế các nguyên liệu khác

Cho 4 thìa canh mắm ruốc cho vào bát ăn cơm, đổ nước vào gần đầy bát, khuấy đều rồi lọc lấy nước cốt. Lọc như này thì sẽ lọc bỏ được các cặn, khi ăn sẽ không có cảm giác bị lạo xạo.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage.pngac5775e5-2422-4b22-ae5a-52a9ea7fc4c4?alt=media&token=9748918f-f117-4d34-9e54-b3500144f462)

Sả bóc bỏ lớp bẹ già, rửa sạch, đập dập rồi băm nhỏ, nhuyễn.

Hành, tỏi bóc vỏ, băm nhỏ. Gừng rửa sạch, đập dập, băm nhỏ.

Ớt sừng, ớt hiểm rửa sạch, băm nhỏ.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage.pnge1716832-6087-42fa-b703-08e84b782d32?alt=media&token=a37276fa-73b2-4744-a6cf-4f2c37d807bc)', N'<h3>Sơ chế thịt</h3>
<p>Thịt ba rọi bóp với muối hạt, rửa sạch rồi để ráo, thái con chì, có độ dày khoảng 1,5 phân. Để làm món thịt kho mắm ruốc, bạn không nên thái miếng quá to như thịt kho tàu. Chỉ cần thái miếng con chì vừa ăn, dễ thấm gia vị hơn.</p>
<p>Thịt sau khi thái ướp cùng với 1 thìa cà phê nước mắm, 1 thìa cà phê tiêu xay, 1/2 thìa cà phê bột ngọt và 1/2 thìa canh nước màu. Trộn đều và để thịt thấm trong khoảng 20-30 phút.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage.pngd00e5f23-9d60-40b4-9f9e-bb2999d3f07c?alt=media&amp;token=e53fb7e7-bd1a-4ed1-a060-a33b2f224af6" alt="image.png"></p>
<h3>Sơ chế các nguyên liệu khác</h3>
<p>Cho 4 thìa canh mắm ruốc cho vào bát ăn cơm, đổ nước vào gần đầy bát, khuấy đều rồi lọc lấy nước cốt. Lọc như này thì sẽ lọc bỏ được các cặn, khi ăn sẽ không có cảm giác bị lạo xạo.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage.pngac5775e5-2422-4b22-ae5a-52a9ea7fc4c4?alt=media&amp;token=9748918f-f117-4d34-9e54-b3500144f462" alt="image.png"></p>
<p>Sả bóc bỏ lớp bẹ già, rửa sạch, đập dập rồi băm nhỏ, nhuyễn.</p>
<p>Hành, tỏi bóc vỏ, băm nhỏ. Gừng rửa sạch, đập dập, băm nhỏ.</p>
<p>Ớt sừng, ớt hiểm rửa sạch, băm nhỏ.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage.pnge1716832-6087-42fa-b703-08e84b782d32?alt=media&amp;token=a37276fa-73b2-4744-a6cf-4f2c37d807bc" alt="image.png"></p>
', 1, N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'628a3185-0209-44c8-bbe9-921fb91ac9b0', N'![image_2023-02-28_170948729.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage_2023-02-28_170948729.png2fcda79f-831b-4dc0-a5d4-6a485f6784d9?alt=media&token=8e93cf81-1f3b-4436-985f-7937aefa0931)

Mực xào sa tế có màu sắc khá đẹp mắt, mùi thơm sa tế nổi bật, vị quyện thấm vào mực ăn rất ngon.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage_2023-02-28_170948729.png2fcda79f-831b-4dc0-a5d4-6a485f6784d9?alt=media&amp;token=8e93cf81-1f3b-4436-985f-7937aefa0931" alt="image_2023-02-28_170948729.png"></p>
<p>Mực xào sa tế có màu sắc khá đẹp mắt, mùi thơm sa tế nổi bật, vị quyện thấm vào mực ăn rất ngon.</p>
', 3, N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'350e67a4-d911-43ba-b56a-945ea794d127', N'Nộm tai heo dưa chuột có màu sắc bắt mắt, tai heo trắng hồng, dưa chuột xanh nhạt, cà rốt có màu cam rực rỡ, điểm xuyết màu xanh lá của rau thơm và 1 vài lát ớt đỏ tươi. Khi ăn cảm nhận rõ tai heo giòn, đậm đà, dưa chuột cà rốt và các nguyên liệu khác có vị chua ngọt cay vừa phải, rất hấp dẫn, đưa miệng và không bị ngán.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.png8fb88b7f-1485-4bd8-973b-10adc0dce4ae?alt=media&token=2a2240d2-c347-4547-9ec5-0e320d410c70)', N'<p>Nộm tai heo dưa chuột có màu sắc bắt mắt, tai heo trắng hồng, dưa chuột xanh nhạt, cà rốt có màu cam rực rỡ, điểm xuyết màu xanh lá của rau thơm và 1 vài lát ớt đỏ tươi. Khi ăn cảm nhận rõ tai heo giòn, đậm đà, dưa chuột cà rốt và các nguyên liệu khác có vị chua ngọt cay vừa phải, rất hấp dẫn, đưa miệng và không bị ngán.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.png8fb88b7f-1485-4bd8-973b-10adc0dce4ae?alt=media&amp;token=2a2240d2-c347-4547-9ec5-0e320d410c70" alt="image.png"></p>
', 3, N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'0c83f426-bde5-4157-a4b0-97956c3346f4', N'Cách làm mì ý sốt bò bằm (mì spaghetti bò bằm) tại nhà siêu ngon và đẹp mắt không hề thua kém ngoài hàng, đặc biệt mì không bị cứng và vón cục vào nhau trong khi sốt bò bằm thơm phức, sánh mịn và vừa vị sẽ được Homnayangi chia sẻ chi tiết trong bài viết này.', N'<p>Cách làm mì ý sốt bò bằm (mì spaghetti bò bằm) tại nhà siêu ngon và đẹp mắt không hề thua kém ngoài hàng, đặc biệt mì không bị cứng và vón cục vào nhau trong khi sốt bò bằm thơm phức, sánh mịn và vừa vị sẽ được Homnayangi chia sẻ chi tiết trong bài viết này.</p>
', 0, N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'a60e077e-e523-4136-a065-9d0770c96df5', N'![image_2023-02-28_174027497.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234%2Fimage_2023-02-28_174027497.png3e4a451a-43ab-4ac0-b347-2df361796431?alt=media&token=3cf991da-3950-45ea-9fed-e969f6645ce9)

Rau củ quả tươi rói, ăn ngọt, quyện với nước sốt mè rất dễ ăn và ngon miệng.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234%2Fimage_2023-02-28_174027497.png3e4a451a-43ab-4ac0-b347-2df361796431?alt=media&amp;token=3cf991da-3950-45ea-9fed-e969f6645ce9" alt="image_2023-02-28_174027497.png"></p>
<p>Rau củ quả tươi rói, ăn ngọt, quyện với nước sốt mè rất dễ ăn và ngon miệng.</p>
', 3, N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'6e37ae04-fe6c-41c5-ae7d-9e2f99f1ca8c', N'Canh chua cá lóc ăn lúc nóng mới cảm nhận được hết vị ngon của nó. Canh chua cá có màu sắc tươi tắn khi kết hợp màu trắng hồng của thịt cá, cà chua màu cam, dứa vàng, rau thơm, bạc hà đậu bắp xanh xanh điểm xuyết thêm màu đỏ của ớt tươi.

Canh chua cá lóc có vị ngọt dịu của cá, chua thanh của dứa, đậu bắp, me, ấm nồng của ớt hòa quyện với các gia vị còn lại. Món ăn này khi ăn cảm thấy rất dễ chịu, thanh mát, không hề bị ngán.', N'<p>Canh chua cá lóc ăn lúc nóng mới cảm nhận được hết vị ngon của nó. Canh chua cá có màu sắc tươi tắn khi kết hợp màu trắng hồng của thịt cá, cà chua màu cam, dứa vàng, rau thơm, bạc hà đậu bắp xanh xanh điểm xuyết thêm màu đỏ của ớt tươi.</p>
<p>Canh chua cá lóc có vị ngọt dịu của cá, chua thanh của dứa, đậu bắp, me, ấm nồng của ớt hòa quyện với các gia vị còn lại. Món ăn này khi ăn cảm thấy rất dễ chịu, thanh mát, không hề bị ngán.</p>
', 3, N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'6f280d2e-2c4e-48d2-b2fa-9ff948020bd1', N'**Mực xào sa tế** với hành tây, kết hợp cùng với bông cải, ớt chuông các màu và cần tây rất ngon, vị cay nồng của sa tế quyện vào giúp mực thêm thấm vị và không còn mùi tanh. Đặc biệt làm món này nhanh, không quá cầu kỳ, xứng đáng là 1 trong những món ngon từ mực nên chế biến.', N'<p><strong>Mực xào sa tế</strong> với hành tây, kết hợp cùng với bông cải, ớt chuông các màu và cần tây rất ngon, vị cay nồng của sa tế quyện vào giúp mực thêm thấm vị và không còn mùi tanh. Đặc biệt làm món này nhanh, không quá cầu kỳ, xứng đáng là 1 trong những món ngon từ mực nên chế biến.</p>
', 0, N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'c0fd0d00-cd06-4d6d-9916-a112af3c17f4', N'Để làm **thịt kho mắm ruốc **ngon, bạn cần mua được đúng mắm ruốc và phải có đường, sả và ớt. Món ăn này ăn với cơm trắng, kèm với khế chua, chuối xanh, rau thơm thì rất hao cơm.', N'<p>Để làm **thịt kho mắm ruốc **ngon, bạn cần mua được đúng mắm ruốc và phải có đường, sả và ớt. Món ăn này ăn với cơm trắng, kèm với khế chua, chuối xanh, rau thơm thì rất hao cơm.</p>
', 0, N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'd6115648-f251-457f-9194-a888d17f0801', N'![image_2023-02-27_230240037.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_230240037.pngf552138b-34bd-4e82-a261-d5160d748a21?alt=media&token=f900997d-d333-4f54-9e53-727564a57de2)

**Thịt kho tiêu** nên ăn lúc nóng, ăn cùng cơm nóng và dưa chua, canh rau quả thực rất hợp vị.

Cách làm thịt kho tiêu cũng không quá khó, nguyên liệu dễ tìm, thời gian thực hiện nhanh gọn, thành phẩm lại vô cùng thơm ngon hấp dẫn. Chính vì vậy, bạn đừng bỏ qua món này mà hãy lưu ngay vào thực đơn bữa cơm hàng ngày của gia đình nhé.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_230240037.pngf552138b-34bd-4e82-a261-d5160d748a21?alt=media&amp;token=f900997d-d333-4f54-9e53-727564a57de2" alt="image_2023-02-27_230240037.png"></p>
<p><strong>Thịt kho tiêu</strong> nên ăn lúc nóng, ăn cùng cơm nóng và dưa chua, canh rau quả thực rất hợp vị.</p>
<p>Cách làm thịt kho tiêu cũng không quá khó, nguyên liệu dễ tìm, thời gian thực hiện nhanh gọn, thành phẩm lại vô cùng thơm ngon hấp dẫn. Chính vì vậy, bạn đừng bỏ qua món này mà hãy lưu ngay vào thực đơn bữa cơm hàng ngày của gia đình nhé.</p>
', 3, N'f578682a-d641-4a0b-aa54-6a81eaf7be08', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'68d8f91c-c4f5-41dd-82b7-ae1655047f2a', N'Cho 4 thìa canh dầu ăn vào chảo, nóng dầu thì phi thơm 2/3 số hành, tỏi, ớt, sả, gừng băm, sau đó cho thịt ba rọi vào xào ở lửa lớn.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage.png47e23d99-96dd-4b69-8cc2-9d751e20c1f6?alt=media&token=c6ad1ae3-fbe6-4773-a681-d0f9e4235b77)

Xào đến khi thịt ba rọi săn lại, xém vàng thì đổ nước cốt mắm ruốc đã lọc vào, nấu hỗn hợp này sôi. Sau đó nêm gia vị vừa ăn gồm có bột ngọt, đường.

Như đã nói ở trên, tùy từng loại mắm ruốc mà nó sẽ có độ đậm khác nhau. Chính vì vậy các bạn chủ động nêm nếm gia vị cho phù hợp với khẩu vị. Vì mắm ruốc có vị mặn nên cần điều chỉnh lượng đường thích hợp, ví dụ như trên, nếu sử dụng 4 thìa canh mắm ruốc, bạn nêm vào khoảng 5-6 thìa đường (tùy vào khẩu vị) và 1 thìa cà phê bột ngọt. 

Tiếp tục kho thịt ở lửa nhỏ vừa, để nước kho thịt sôi liu riu trong khoảng 25-30 phút cho thịt có độ thấm gia vị và chín mềm.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage.png23551a89-c944-4047-be23-e24a25b5e6ac?alt=media&token=73a579df-61e9-4d10-b7e2-c0bf56eef2dd)

Đến khi thịt kho mắm ruốc sánh lại, sền sệt thì cho nốt số hành, tỏi, gừng, sả ớt băm vào, đảo đều và nấu thêm khoảng 10 phút. Cho hành tỏi sả thêm vào sau như thế này thì hương vị nó sẽ nổi bật hơn, rất hấp dẫn.

Sau khoảng 10 phút, có thể nêm nếm lại gia vị vừa ăn, rắc chút tiêu xay, đảo nhanh rồi tắt bếp. Ở bước này, nếu muốn thịt sánh và có màu đỏ nâu đẹp mắt hơn nữa thì bạn có thể tăng nhiệt độ lớn và đảo nhanh tay trong khoảng 15 giây. 

Múc thịt ra đĩa, rắc hành lá lên trên, ăn lúc nóng.', N'<p>Cho 4 thìa canh dầu ăn vào chảo, nóng dầu thì phi thơm 2/3 số hành, tỏi, ớt, sả, gừng băm, sau đó cho thịt ba rọi vào xào ở lửa lớn.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage.png47e23d99-96dd-4b69-8cc2-9d751e20c1f6?alt=media&amp;token=c6ad1ae3-fbe6-4773-a681-d0f9e4235b77" alt="image.png"></p>
<p>Xào đến khi thịt ba rọi săn lại, xém vàng thì đổ nước cốt mắm ruốc đã lọc vào, nấu hỗn hợp này sôi. Sau đó nêm gia vị vừa ăn gồm có bột ngọt, đường.</p>
<p>Như đã nói ở trên, tùy từng loại mắm ruốc mà nó sẽ có độ đậm khác nhau. Chính vì vậy các bạn chủ động nêm nếm gia vị cho phù hợp với khẩu vị. Vì mắm ruốc có vị mặn nên cần điều chỉnh lượng đường thích hợp, ví dụ như trên, nếu sử dụng 4 thìa canh mắm ruốc, bạn nêm vào khoảng 5-6 thìa đường (tùy vào khẩu vị) và 1 thìa cà phê bột ngọt.</p>
<p>Tiếp tục kho thịt ở lửa nhỏ vừa, để nước kho thịt sôi liu riu trong khoảng 25-30 phút cho thịt có độ thấm gia vị và chín mềm.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage.png23551a89-c944-4047-be23-e24a25b5e6ac?alt=media&amp;token=73a579df-61e9-4d10-b7e2-c0bf56eef2dd" alt="image.png"></p>
<p>Đến khi thịt kho mắm ruốc sánh lại, sền sệt thì cho nốt số hành, tỏi, gừng, sả ớt băm vào, đảo đều và nấu thêm khoảng 10 phút. Cho hành tỏi sả thêm vào sau như thế này thì hương vị nó sẽ nổi bật hơn, rất hấp dẫn.</p>
<p>Sau khoảng 10 phút, có thể nêm nếm lại gia vị vừa ăn, rắc chút tiêu xay, đảo nhanh rồi tắt bếp. Ở bước này, nếu muốn thịt sánh và có màu đỏ nâu đẹp mắt hơn nữa thì bạn có thể tăng nhiệt độ lớn và đảo nhanh tay trong khoảng 15 giây.</p>
<p>Múc thịt ra đĩa, rắc hành lá lên trên, ăn lúc nóng.</p>
', 2, N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'72e39db0-cfcf-4020-903e-b02a4112d457', N'![image_2023-02-27_174056827.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fdc7234fa-21fd-45bc-9612-f4fb5e2f4f8b%2Fimage_2023-02-27_174056827.png98b07752-546f-4519-9b71-2217f5b9936f?alt=media&token=22b30e11-5be1-4057-8750-905782a17368)

Món rau muống xào tỏi sau khi hoàn thành sẽ có màu xanh mướt và có vị thơm đặc trưng của tỏi. Khi ăn cảm nhận được độ giòn và ngọt của rau, gia vị đậm đà vừa phải. Tỏi bùi thơm mà không bị cháy. Đặc biệt, rau muống xào không bị chảy ra quá nhiều nước vì mình xào rau chín tới ở thời gian và nhiệt độ thích hợp.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fdc7234fa-21fd-45bc-9612-f4fb5e2f4f8b%2Fimage_2023-02-27_174056827.png98b07752-546f-4519-9b71-2217f5b9936f?alt=media&amp;token=22b30e11-5be1-4057-8750-905782a17368" alt="image_2023-02-27_174056827.png"></p>
<p>Món rau muống xào tỏi sau khi hoàn thành sẽ có màu xanh mướt và có vị thơm đặc trưng của tỏi. Khi ăn cảm nhận được độ giòn và ngọt của rau, gia vị đậm đà vừa phải. Tỏi bùi thơm mà không bị cháy. Đặc biệt, rau muống xào không bị chảy ra quá nhiều nước vì mình xào rau chín tới ở thời gian và nhiệt độ thích hợp.</p>
', 3, N'dc7234fa-21fd-45bc-9612-f4fb5e2f4f8b', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'd9b3fa60-d809-46d7-9fbe-b09abfd9612b', N'Cà pháo ngâm sạch sẽ với nước muối rồi vớt ra, để ráo. Xếp cà vào đĩa cùng với dưa chuột và các loại rau thơm. Khi ăn, gắp 1 miếng cà kèm với dưa chuột, rau thơm chấm đẫm mắm tôm.

![image_2023-02-28_171953615.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F047935c2-9a60-4b0e-865a-89c5e1a680e5%2Fimage_2023-02-28_171953615.pngda75bef7-1a62-46e4-98e2-8b6fd690e5f0?alt=media&token=1ee5ee69-b46c-47aa-a530-4f18632d3140)', N'<p>Cà pháo ngâm sạch sẽ với nước muối rồi vớt ra, để ráo. Xếp cà vào đĩa cùng với dưa chuột và các loại rau thơm. Khi ăn, gắp 1 miếng cà kèm với dưa chuột, rau thơm chấm đẫm mắm tôm.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F047935c2-9a60-4b0e-865a-89c5e1a680e5%2Fimage_2023-02-28_171953615.pngda75bef7-1a62-46e4-98e2-8b6fd690e5f0?alt=media&amp;token=1ee5ee69-b46c-47aa-a530-4f18632d3140" alt="image_2023-02-28_171953615.png"></p>
', 2, N'047935c2-9a60-4b0e-865a-89c5e1a680e5', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'7624d630-ebb8-4678-b5ea-b09d1dd2464b', N'### Luộc mì

Cho 3 lít nước vào nồi, thêm 1 thìa canh dầu oliu và 1/2 thìa muối vào, khuấy đều rồi nấu sôi. Dầu và muối giúp cho mì ý đậm vị và sẽ không bị dính vào nhau khi luộc chín.

![image_2023-02-27_215848108.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_215848108.png4df6e9dc-ec7f-492c-b92c-98fbe453cd5e?alt=media&token=09cbde3f-100f-4dd6-bc6f-599059036f84)

Khi nước già sôi, thả mì Ý vào, chỉnh nhiệt độ bếp ở mức sôi vừa. Thời gian luộc chín mì ý trung bình khoảng 15- 17 phút, phụ thuộc vào chế độ lửa bếp. Lúc luộc mì bạn không nên khuấy đảo quá nhiều, chỉ cần thỉnh thoảng đảo nhẹ để sợi mì không bị dính vào đáy nồi là được.

Mỳ ý chín vớt ra cho vào tô lớn. Ở bước này, mì ý sẽ dễ bị dính vào nhau khi nguội dần, chính vì thế, để khắc phục bạn cho 1 ít dầu oliu vào trộn cùng mì. Sợi mì sẽ rời nhau và còn có độ bóng bẩy rất đẹp mắt.

![image_2023-02-27_215903378.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_215903378.pngb0834b2e-df09-4d09-a45b-15dbcd3cf418?alt=media&token=a88be0be-2f8e-4c48-a162-0146373c0e35)

Phần nước luộc mì ý, bạn đừng vội đổ đi vì khi nấu nước sốt sẽ cần dùng đến để tạo độ sánh cho sốt vì trong nước luộc mì có tinh bột.

### Làm sốt bò bằm

Cho 1 thìa canh dầu oliu vào chảo và 4 miếng bơ lạt (~40g), bật lửa nhỏ để bơ tan chảy, sau đó cho nốt số tỏi băm vào để phi thơm rồi cho cà chua vào xào.

![image_2023-02-27_215939613.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_215939613.pngb6030491-5f59-462d-afb5-76691899804b?alt=media&token=42b78939-07cd-46ae-9817-77a157791d16)

Khi cà chua nhuyễn mềm, cho hành tây vào. Cùng với đó là 8 thìa canh sốt cà chua, 1 thìa canh tương cà và 1 muỗng canh nước luộc mì ý. Khuấy đều để các nguyên liệu hòa quyện vào nhau.

Nấu khoảng 5 phút, khi thấy hỗn hợp cà chua đã bắt đầu có độ sánh thì cho thịt bò đã xào vào. Nêm gia vị vừa ăn gồm có 2 thìa canh đường trắng, gần 3,5 thìa cà phê muối và có thể cho thêm 1 ít tiêu xay nếu thích. Đảo đều rồi tắt bếp.', N'<h3>Luộc mì</h3>
<p>Cho 3 lít nước vào nồi, thêm 1 thìa canh dầu oliu và 1/2 thìa muối vào, khuấy đều rồi nấu sôi. Dầu và muối giúp cho mì ý đậm vị và sẽ không bị dính vào nhau khi luộc chín.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_215848108.png4df6e9dc-ec7f-492c-b92c-98fbe453cd5e?alt=media&amp;token=09cbde3f-100f-4dd6-bc6f-599059036f84" alt="image_2023-02-27_215848108.png"></p>
<p>Khi nước già sôi, thả mì Ý vào, chỉnh nhiệt độ bếp ở mức sôi vừa. Thời gian luộc chín mì ý trung bình khoảng 15- 17 phút, phụ thuộc vào chế độ lửa bếp. Lúc luộc mì bạn không nên khuấy đảo quá nhiều, chỉ cần thỉnh thoảng đảo nhẹ để sợi mì không bị dính vào đáy nồi là được.</p>
<p>Mỳ ý chín vớt ra cho vào tô lớn. Ở bước này, mì ý sẽ dễ bị dính vào nhau khi nguội dần, chính vì thế, để khắc phục bạn cho 1 ít dầu oliu vào trộn cùng mì. Sợi mì sẽ rời nhau và còn có độ bóng bẩy rất đẹp mắt.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_215903378.pngb0834b2e-df09-4d09-a45b-15dbcd3cf418?alt=media&amp;token=a88be0be-2f8e-4c48-a162-0146373c0e35" alt="image_2023-02-27_215903378.png"></p>
<p>Phần nước luộc mì ý, bạn đừng vội đổ đi vì khi nấu nước sốt sẽ cần dùng đến để tạo độ sánh cho sốt vì trong nước luộc mì có tinh bột.</p>
<h3>Làm sốt bò bằm</h3>
<p>Cho 1 thìa canh dầu oliu vào chảo và 4 miếng bơ lạt (~40g), bật lửa nhỏ để bơ tan chảy, sau đó cho nốt số tỏi băm vào để phi thơm rồi cho cà chua vào xào.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_215939613.pngb6030491-5f59-462d-afb5-76691899804b?alt=media&amp;token=42b78939-07cd-46ae-9817-77a157791d16" alt="image_2023-02-27_215939613.png"></p>
<p>Khi cà chua nhuyễn mềm, cho hành tây vào. Cùng với đó là 8 thìa canh sốt cà chua, 1 thìa canh tương cà và 1 muỗng canh nước luộc mì ý. Khuấy đều để các nguyên liệu hòa quyện vào nhau.</p>
<p>Nấu khoảng 5 phút, khi thấy hỗn hợp cà chua đã bắt đầu có độ sánh thì cho thịt bò đã xào vào. Nêm gia vị vừa ăn gồm có 2 thìa canh đường trắng, gần 3,5 thìa cà phê muối và có thể cho thêm 1 ít tiêu xay nếu thích. Đảo đều rồi tắt bếp.</p>
', 2, N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'4d19530d-b824-4011-a2cc-b133f7a9f6ac', N'Cho tất cả các nguyên liệu vào tô to, trừ đậu phộng nên cho vào sau để món nộm được bùi ngậy. Rưới nước trộn lên và trộn đều để các nguyên liệu thấm gia vị.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.pnga862ff0b-19df-4513-ae64-a2f4648a95c0?alt=media&token=273611ef-fa24-40dc-8a97-4ca449c4b86f)

Sau khi trộn đều để nộm thấm gia vị thêm khoảng 3-5 phút rồi mới bày ra đĩa, cho đậu phộng rang lên đồng thời trang trí bằng rau mùi và ớt tươi tỉa hoa bên cạnh.', N'<p>Cho tất cả các nguyên liệu vào tô to, trừ đậu phộng nên cho vào sau để món nộm được bùi ngậy. Rưới nước trộn lên và trộn đều để các nguyên liệu thấm gia vị.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.pnga862ff0b-19df-4513-ae64-a2f4648a95c0?alt=media&amp;token=273611ef-fa24-40dc-8a97-4ca449c4b86f" alt="image.png"></p>
<p>Sau khi trộn đều để nộm thấm gia vị thêm khoảng 3-5 phút rồi mới bày ra đĩa, cho đậu phộng rang lên đồng thời trang trí bằng rau mùi và ớt tươi tỉa hoa bên cạnh.</p>
', 2, N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'cfc21284-f2d1-4ddf-b390-b5512d4dca8c', N'Làm nóng 2 thìa dầu ăn rồi cho tỏi vào phi thơm. Chế thêm 1 ít nước lã vào chảo, đảo đều rồi mới cho bông bí. Đây là bí quyết giúp bông bí xào được xanh mà nhiều người chưa biết.

![image_2023-03-03_233630773.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fac41da30-8998-4a1f-b2e4-291d0b24dcf4%2Fimage_2023-03-03_233630773.png74ad0cda-62e7-46b6-a63a-d60ccd9749c4?alt=media&token=1ba83fc4-dc08-490c-a309-67bb044f2b79)

Để lửa lớn, đợi sau khoảng 15 giây thì mới đảo bông bí để lớp phía dưới ngấm đủ dầu. Cứ như thế, đảo đều tay, sau khoảng 1 phút thì nêm gia vị gồm có 1 thìa cà phê dầu hào, gần 1 thìa cà phê bột canh và 1/2 thìa cà phê bột ngọt.

Xào đến khi thấy bông bí chín tới, có màu xanh mướt, nếm thử thấy vừa ăn thì tắt bếp.

Trung bình chỉ xào khoảng 2-3 phút ở lửa lớn là bông bí chín tới, ăn giòn ngọt. Không nên xào quá kỹ, bông bí bị nhũn, chảy nước quá nhiều sẽ ăn không ngon.

Trút bông bí xào tỏi ra đĩa, có thể rắc thêm 1 chút hạt tiêu để tăng thêm hương vị hấp dẫn cho món ăn.', N'<p>Làm nóng 2 thìa dầu ăn rồi cho tỏi vào phi thơm. Chế thêm 1 ít nước lã vào chảo, đảo đều rồi mới cho bông bí. Đây là bí quyết giúp bông bí xào được xanh mà nhiều người chưa biết.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fac41da30-8998-4a1f-b2e4-291d0b24dcf4%2Fimage_2023-03-03_233630773.png74ad0cda-62e7-46b6-a63a-d60ccd9749c4?alt=media&amp;token=1ba83fc4-dc08-490c-a309-67bb044f2b79" alt="image_2023-03-03_233630773.png"></p>
<p>Để lửa lớn, đợi sau khoảng 15 giây thì mới đảo bông bí để lớp phía dưới ngấm đủ dầu. Cứ như thế, đảo đều tay, sau khoảng 1 phút thì nêm gia vị gồm có 1 thìa cà phê dầu hào, gần 1 thìa cà phê bột canh và 1/2 thìa cà phê bột ngọt.</p>
<p>Xào đến khi thấy bông bí chín tới, có màu xanh mướt, nếm thử thấy vừa ăn thì tắt bếp.</p>
<p>Trung bình chỉ xào khoảng 2-3 phút ở lửa lớn là bông bí chín tới, ăn giòn ngọt. Không nên xào quá kỹ, bông bí bị nhũn, chảy nước quá nhiều sẽ ăn không ngon.</p>
<p>Trút bông bí xào tỏi ra đĩa, có thể rắc thêm 1 chút hạt tiêu để tăng thêm hương vị hấp dẫn cho món ăn.</p>
', 2, N'ac41da30-8998-4a1f-b2e4-291d0b24dcf4', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'4b996f76-0e99-4a46-9deb-b5b8ff7e6b5e', N'Cho khoảng 2 thìa dầu ăn vào nồi, làm nóng dầu thì phi thơm hành khô và tỏi băm, sau đó cho thịt tôm vào xào. Lưu ý không nên để lửa lớn, tôm dễ bị cháy dính vào đáy nồi.

![image_2023-02-27_163535425.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163535425.png9350b4ff-f1f8-40ca-b2c1-f3e660042c9c?alt=media&token=5d00eedc-2f3c-488a-b21b-633e79ebbd10)

![image_2023-02-27_163544133.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163544133.png8fe1a4ff-a741-477c-bc53-c428b5931bc4?alt=media&token=dc201a99-9d5e-4f9e-b949-a490c07a976b)

Xào các nguyên liệu thêm khoảng 2 phút thì cho nước lọc đầu tôm lúc nãy vào, chế thêm nước. Thường thì nước canh khoảng 1,2 - 1,5 lít nước là đủ cho 4 người ăn. Tăng nhiệt độ bếp lên 1 chút, nấu khi nước sôi thì nêm thêm gia vị vừa ăn.

Để nước canh bí đỏ nấu tôm được trong, trong quá trình nấu cần hớt bọt thường xuyên. Nấu khoảng thêm 3-4 phút đến khi bí đỏ chín mềm là được, không nên ninh quá lâu, bí chín vỡ ra thì tô canh không được đẹp mắt.

![image_2023-02-27_163609750.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163609750.pngd489870d-04d6-4aa2-8733-82fd3eca30a0?alt=media&token=0ca9efdb-dea6-4e60-8e5f-1cc3b98387e8)', N'<p>Cho khoảng 2 thìa dầu ăn vào nồi, làm nóng dầu thì phi thơm hành khô và tỏi băm, sau đó cho thịt tôm vào xào. Lưu ý không nên để lửa lớn, tôm dễ bị cháy dính vào đáy nồi.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163535425.png9350b4ff-f1f8-40ca-b2c1-f3e660042c9c?alt=media&amp;token=5d00eedc-2f3c-488a-b21b-633e79ebbd10" alt="image_2023-02-27_163535425.png"></p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163544133.png8fe1a4ff-a741-477c-bc53-c428b5931bc4?alt=media&amp;token=dc201a99-9d5e-4f9e-b949-a490c07a976b" alt="image_2023-02-27_163544133.png"></p>
<p>Xào các nguyên liệu thêm khoảng 2 phút thì cho nước lọc đầu tôm lúc nãy vào, chế thêm nước. Thường thì nước canh khoảng 1,2 - 1,5 lít nước là đủ cho 4 người ăn. Tăng nhiệt độ bếp lên 1 chút, nấu khi nước sôi thì nêm thêm gia vị vừa ăn.</p>
<p>Để nước canh bí đỏ nấu tôm được trong, trong quá trình nấu cần hớt bọt thường xuyên. Nấu khoảng thêm 3-4 phút đến khi bí đỏ chín mềm là được, không nên ninh quá lâu, bí chín vỡ ra thì tô canh không được đẹp mắt.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163609750.pngd489870d-04d6-4aa2-8733-82fd3eca30a0?alt=media&amp;token=0ca9efdb-dea6-4e60-8e5f-1cc3b98387e8" alt="image_2023-02-27_163609750.png"></p>
', 2, N'5b579874-46a8-4895-bad2-1f33d4cb006a', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'33988e75-6ee6-4f29-9f68-b682cf759909', N'### Sơ chế sườn:

Sườn non mua về bạn xóc kỹ cùng muối hạt, sau đó rửa sạch lại rồi đem đi chần qua tầm 1 phút. Lưu ý, đợi nước già sôi rồi mới cho sườn vào chần, cho thêm 1 ít muối hạt vào và không đậy nắp để các tạp chất trong sườn bay hơi đi.

Sườn sau khi chần vớt ra, rửa sạch với nước lạnh, để thật ráo. Ướp sườn cùng với 1 muỗng canh nước mắm, 1/2 thìa canh đường, 1/2 thìa canh bột ngọt, 1 thìa canh dầu màu điều (để tạo màu sắc đẹp mắt cho món kho) và 1 thìa canh nước màu, 1 thìa cà phê tiêu hạt, 1/2 thìa cà phê hạt tiêu xay.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F3ef7c4e0-b1ac-4506-9556-3541afd421fe%2Fimage.pngc8affa52-8542-45cf-a368-c827c5bc55ed?alt=media&token=e6bac293-361e-4b70-8def-a68b197707d3)

### Sơ chế các nguyên liệu khác:

Thơm/khóm gọt vỏ, bỏ mắt, bổ làm đôi và chỉ dùng 1 nửa. Gọt lõi sau đó thái thơm thành từng miếng hình tam giác. Nếu thích ăn nhiều dứa, bạn có thể dùng hơn nửa quả. Đừng sợ nhiều dứa sẽ chua quá, bạn hoàn toàn có thể điều chỉnh bằng cách gia giảm nêm nếm gia vị vừa ăn.

Hành, tỏi bóc vỏ, băm nhỏ.

Hành lá nhặt rửa sạch, xắt nhỏ. Ớt cũng xắt nhỏ tương tự.', N'<h3>Sơ chế sườn:</h3>
<p>Sườn non mua về bạn xóc kỹ cùng muối hạt, sau đó rửa sạch lại rồi đem đi chần qua tầm 1 phút. Lưu ý, đợi nước già sôi rồi mới cho sườn vào chần, cho thêm 1 ít muối hạt vào và không đậy nắp để các tạp chất trong sườn bay hơi đi.</p>
<p>Sườn sau khi chần vớt ra, rửa sạch với nước lạnh, để thật ráo. Ướp sườn cùng với 1 muỗng canh nước mắm, 1/2 thìa canh đường, 1/2 thìa canh bột ngọt, 1 thìa canh dầu màu điều (để tạo màu sắc đẹp mắt cho món kho) và 1 thìa canh nước màu, 1 thìa cà phê tiêu hạt, 1/2 thìa cà phê hạt tiêu xay.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F3ef7c4e0-b1ac-4506-9556-3541afd421fe%2Fimage.pngc8affa52-8542-45cf-a368-c827c5bc55ed?alt=media&amp;token=e6bac293-361e-4b70-8def-a68b197707d3" alt="image.png"></p>
<h3>Sơ chế các nguyên liệu khác:</h3>
<p>Thơm/khóm gọt vỏ, bỏ mắt, bổ làm đôi và chỉ dùng 1 nửa. Gọt lõi sau đó thái thơm thành từng miếng hình tam giác. Nếu thích ăn nhiều dứa, bạn có thể dùng hơn nửa quả. Đừng sợ nhiều dứa sẽ chua quá, bạn hoàn toàn có thể điều chỉnh bằng cách gia giảm nêm nếm gia vị vừa ăn.</p>
<p>Hành, tỏi bóc vỏ, băm nhỏ.</p>
<p>Hành lá nhặt rửa sạch, xắt nhỏ. Ớt cũng xắt nhỏ tương tự.</p>
', 1, N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'06b952c5-50c7-4f8b-9ae7-bc248ecf414f', N'Hành khô bóc vỏ, băm nhỏ.

Gừng cạo vỏ, băm nhỏ.

![image_2023-02-28_110622493.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110622493.png8e53fb2c-0f56-44b4-9bbe-1314f5bb4523?alt=media&token=326b51f5-b2c7-4d3f-bda1-d8fcb9ff7950)

Sả bóc bỏ lớp bẹ già, cắt làm đôi. Phần gốc thái lát chéo, phần ngọn băm nhỏ.

![image_2023-02-28_110634964.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110634964.png309643b2-25de-497a-9788-73dbbdfe0777?alt=media&token=8b81d8b5-fca3-46f7-a57f-48eae7a7b701)

Ớt sừng rửa sạch, thái lát chéo.

Lá chanh rửa sạch, thái nhỏ.

### Ướp thịt gà

Gà bóp qua với muối hạt, rượu gừng (nếu có) rồi rửa sạch. Dùng dao nhỏ lọc xương món ăn sẽ hấp dẫn hơn. Nếu để tiết kiệm thời gian thì bỏ qua bước lọc xương và chặt thành miếng vừa ăn.

Ướp thịt gà với:
* 1 thìa canh nước mắm
* 1 thìa hạt tiêu
* 1 ít bột nghệ để tạo màu
* 1 ít gừng băm, sả băm (số gừng sả còn lại sẽ dùng ở bước xào thịt gà)

![image_2023-02-28_110734837.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110734837.pngba539a2b-b833-4838-ac44-92559bdd1a12?alt=media&token=ae6a1e8f-2cbe-42fe-bac6-4c3c08dceefb)

Ướp thịt gà khoảng 30 phút. Có thể dùng màng bọc thực phẩm, bọc lại và để trong ngăn mát tủ lạnh, giúp thịt gà giữ được độ tươi và không bị khô.', N'<p>Hành khô bóc vỏ, băm nhỏ.</p>
<p>Gừng cạo vỏ, băm nhỏ.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110622493.png8e53fb2c-0f56-44b4-9bbe-1314f5bb4523?alt=media&amp;token=326b51f5-b2c7-4d3f-bda1-d8fcb9ff7950" alt="image_2023-02-28_110622493.png"></p>
<p>Sả bóc bỏ lớp bẹ già, cắt làm đôi. Phần gốc thái lát chéo, phần ngọn băm nhỏ.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110634964.png309643b2-25de-497a-9788-73dbbdfe0777?alt=media&amp;token=8b81d8b5-fca3-46f7-a57f-48eae7a7b701" alt="image_2023-02-28_110634964.png"></p>
<p>Ớt sừng rửa sạch, thái lát chéo.</p>
<p>Lá chanh rửa sạch, thái nhỏ.</p>
<h3>Ướp thịt gà</h3>
<p>Gà bóp qua với muối hạt, rượu gừng (nếu có) rồi rửa sạch. Dùng dao nhỏ lọc xương món ăn sẽ hấp dẫn hơn. Nếu để tiết kiệm thời gian thì bỏ qua bước lọc xương và chặt thành miếng vừa ăn.</p>
<p>Ướp thịt gà với:</p>
<ul>
<li>1 thìa canh nước mắm</li>
<li>1 thìa hạt tiêu</li>
<li>1 ít bột nghệ để tạo màu</li>
<li>1 ít gừng băm, sả băm (số gừng sả còn lại sẽ dùng ở bước xào thịt gà)</li>
</ul>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110734837.pngba539a2b-b833-4838-ac44-92559bdd1a12?alt=media&amp;token=ae6a1e8f-2cbe-42fe-bac6-4c3c08dceefb" alt="image_2023-02-28_110734837.png"></p>
<p>Ướp thịt gà khoảng 30 phút. Có thể dùng màng bọc thực phẩm, bọc lại và để trong ngăn mát tủ lạnh, giúp thịt gà giữ được độ tươi và không bị khô.</p>
', 1, N'e3f6c554-29c7-460d-a692-d23d15dae638', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'09b70595-f08b-493c-851f-bf55cdc1802b', N'

### Sơ chế sườn và ướp


Sườn chặt miếng vừa ăn, rửa sạch rồi mang đi chần qua. Đun sôi 1 nồi nước với 1 nhúm muối hạt. Khi nước sôi thì cho sườn vào chần khoảng 2-3 phút rồi vớt ra và rửa thật kỹ dưới vòi nước. Bước làm này giúp sườn sạch, khi nấu không bị lẫn xương lợn cợn và tiết ra nhiều bọt bẩn. Ngoài ra, việc rửa sườn dưới vòi nước cũng giúp miếng sườn mềm hơn.

![image_2023-02-27_173236805.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fcc8da8e0-0c9d-4893-8ea1-387c0519a0f3%2Fimage_2023-02-27_173236805.png7212366c-02a1-4eb4-ac6c-0df17fc04f12?alt=media&token=2d058c56-900b-4b52-9981-d7678373beb7)

**Sườn sau khi rửa sạch cho vào nồi rồi ướp với:**
* 1 muỗng dấm gạo
* 2 muỗng nước mắm
* 1 muỗng đường
* 1/2 muỗng dầu điều
* 1/2 thìa nhỏ bột canh (nếu muốn ăn đậm vị)
* 1/2 thìa nhỏ mì chính
* 1 thìa dầu ăn


Lượng gia vị này rất vừa vặn với khoảng 700g - 800g thịt sườn, nên nếu nấu ít hơn hoặc nhiều hơn, bạn có thể điều chỉnh để hợp với khẩu vị.

Tỏi bóc vỏ, băm nhỏ.

Nếu muốn ăn cay thì băm thêm 1-2 trái ớt tươi.', N'<h3>Sơ chế sườn và ướp</h3>
<p>Sườn chặt miếng vừa ăn, rửa sạch rồi mang đi chần qua. Đun sôi 1 nồi nước với 1 nhúm muối hạt. Khi nước sôi thì cho sườn vào chần khoảng 2-3 phút rồi vớt ra và rửa thật kỹ dưới vòi nước. Bước làm này giúp sườn sạch, khi nấu không bị lẫn xương lợn cợn và tiết ra nhiều bọt bẩn. Ngoài ra, việc rửa sườn dưới vòi nước cũng giúp miếng sườn mềm hơn.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fcc8da8e0-0c9d-4893-8ea1-387c0519a0f3%2Fimage_2023-02-27_173236805.png7212366c-02a1-4eb4-ac6c-0df17fc04f12?alt=media&amp;token=2d058c56-900b-4b52-9981-d7678373beb7" alt="image_2023-02-27_173236805.png"></p>
<p><strong>Sườn sau khi rửa sạch cho vào nồi rồi ướp với:</strong></p>
<ul>
<li>1 muỗng dấm gạo</li>
<li>2 muỗng nước mắm</li>
<li>1 muỗng đường</li>
<li>1/2 muỗng dầu điều</li>
<li>1/2 thìa nhỏ bột canh (nếu muốn ăn đậm vị)</li>
<li>1/2 thìa nhỏ mì chính</li>
<li>1 thìa dầu ăn</li>
</ul>
<p>Lượng gia vị này rất vừa vặn với khoảng 700g - 800g thịt sườn, nên nếu nấu ít hơn hoặc nhiều hơn, bạn có thể điều chỉnh để hợp với khẩu vị.</p>
<p>Tỏi bóc vỏ, băm nhỏ.</p>
<p>Nếu muốn ăn cay thì băm thêm 1-2 trái ớt tươi.</p>
', 1, N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'f6c58e56-e4b1-4eb4-aad2-c29c3e62d716', N'**Canh khoai mỡ** có vị ngọt mát, thơm ngậy, kết hợp nấu với tôm tươi là món canh ngon và rất bổ dưỡng cho sức khỏe, đặc biệt tốt cho người có các bệnh liên quan đến tim mạch, lưu thông đường huyết, nhuận tràng hay béo phì...😊', N'<p><strong>Canh khoai mỡ</strong> có vị ngọt mát, thơm ngậy, kết hợp nấu với tôm tươi là món canh ngon và rất bổ dưỡng cho sức khỏe, đặc biệt tốt cho người có các bệnh liên quan đến tim mạch, lưu thông đường huyết, nhuận tràng hay béo phì...😊</p>
', 0, N'5de76825-4f4e-469d-b94e-5e457294b045', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'ce1f96d7-abae-441b-b1ff-c7fcbe10e77a', N'**Canh bí đỏ nấu tôm** có vị ngọt dịu của bí đỏ và mùi thơm đặc trưng của tôm, thêm hương vị hấp dẫn của hành lá mùi tàu. Cùng học cách nấu canh bí đỏ với tôm tươi theo các bước dưới đây.', N'<p><strong>Canh bí đỏ nấu tôm</strong> có vị ngọt dịu của bí đỏ và mùi thơm đặc trưng của tôm, thêm hương vị hấp dẫn của hành lá mùi tàu. Cùng học cách nấu canh bí đỏ với tôm tươi theo các bước dưới đây.</p>
', 0, N'5b579874-46a8-4895-bad2-1f33d4cb006a', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'0d1c6468-ce6b-4763-9c40-ccc75676fdb0', N'Sau khi thắng nước hàng, bạn cho nốt số hành tỏi băm vào phi thơm rồi cho thịt vào đảo. Lưu ý lúc này nên tăng nhiệt độ bếp lên 1 chút để miếng thịt được săn lại. 

![image_2023-02-27_230154885.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_230154885.png6e91165a-625c-492a-aee1-ed576275669e?alt=media&token=ff8bd735-c719-4075-a898-d14349eb21fe)

Sau khoảng 2 phút thì nêm lại gia vị rồi tiếp tục kho thêm 10-15 phút. Nếu thích ăn nhiều nước kho thịt, bạn có thể đậy nắp nồi khi nấu.

![image_2023-02-27_230211368.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_230211368.png16751d89-0198-4c3f-9818-115fc35521c5?alt=media&token=871de541-b743-43bc-8399-70148c07b5d6)

Khi thịt chín mềm, đậm đà, rắc 1 thìa cà phê hạt tiêu xay lên

![image_2023-02-27_230230410.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_230230410.pngfefe4513-421b-4d0c-854d-1edf78d50e18?alt=media&token=c54f435f-cd27-45d2-a281-e85ef98a5991)', N'<p>Sau khi thắng nước hàng, bạn cho nốt số hành tỏi băm vào phi thơm rồi cho thịt vào đảo. Lưu ý lúc này nên tăng nhiệt độ bếp lên 1 chút để miếng thịt được săn lại.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_230154885.png6e91165a-625c-492a-aee1-ed576275669e?alt=media&amp;token=ff8bd735-c719-4075-a898-d14349eb21fe" alt="image_2023-02-27_230154885.png"></p>
<p>Sau khoảng 2 phút thì nêm lại gia vị rồi tiếp tục kho thêm 10-15 phút. Nếu thích ăn nhiều nước kho thịt, bạn có thể đậy nắp nồi khi nấu.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_230211368.png16751d89-0198-4c3f-9818-115fc35521c5?alt=media&amp;token=871de541-b743-43bc-8399-70148c07b5d6" alt="image_2023-02-27_230211368.png"></p>
<p>Khi thịt chín mềm, đậm đà, rắc 1 thìa cà phê hạt tiêu xay lên</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_230230410.pngfefe4513-421b-4d0c-854d-1edf78d50e18?alt=media&amp;token=c54f435f-cd27-45d2-a281-e85ef98a5991" alt="image_2023-02-27_230230410.png"></p>
', 2, N'f578682a-d641-4a0b-aa54-6a81eaf7be08', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'fb0ddfb3-8a14-4ba5-847c-cde54f848b2d', N'### Sơ chế bột nếp

Cho bột nếp ra bát lớn, sau đó cho 1 thìa canh dầu ăn và 1/4 thìa cà phê muối vào, trộn đều. Dầu và muối giúp bột dẻo mịn và đậm vị hơn, không bị chua.

Tiếp đến bạn rưới nước nóng già vào để nhồi bột. Với 200g bột nếp, lượng nước nóng sử dụng để nhồi bột khoảng 120-125ml. Lưu ý, tránh cho quá nhiều nước khiến cho bột bị nhão, đặc biệt bột nếp rất dễ bị nhão nếu quá ướt. Tốt nhất, bạn cho ít một để dễ điều chỉnh được độ ướt của bột.

Sau đó tiếp tục nhào đến khi thấy khối bột dẻo mịn nhưng không bị dính vào tay là được.

Sau khi nhào bột xong, để bột nghỉ trong khoảng 5-10 phút. Dùng màng bọc thực phẩm bọc kín khối bột, tránh bột để lâu bên ngoài bị khô.

### Sơ chế các nguyên liệu khác

Gừng cạo vỏ, rửa sạch rồi thái thành sợi.

Nếu dùng vừng, bạn nhặt bỏ những tạp chất lẫn vào vừng, sau đó rang vàng dậy mùi thơm rồi cho ra bát riêng.', N'<h3>Sơ chế bột nếp</h3>
<p>Cho bột nếp ra bát lớn, sau đó cho 1 thìa canh dầu ăn và 1/4 thìa cà phê muối vào, trộn đều. Dầu và muối giúp bột dẻo mịn và đậm vị hơn, không bị chua.</p>
<p>Tiếp đến bạn rưới nước nóng già vào để nhồi bột. Với 200g bột nếp, lượng nước nóng sử dụng để nhồi bột khoảng 120-125ml. Lưu ý, tránh cho quá nhiều nước khiến cho bột bị nhão, đặc biệt bột nếp rất dễ bị nhão nếu quá ướt. Tốt nhất, bạn cho ít một để dễ điều chỉnh được độ ướt của bột.</p>
<p>Sau đó tiếp tục nhào đến khi thấy khối bột dẻo mịn nhưng không bị dính vào tay là được.</p>
<p>Sau khi nhào bột xong, để bột nghỉ trong khoảng 5-10 phút. Dùng màng bọc thực phẩm bọc kín khối bột, tránh bột để lâu bên ngoài bị khô.</p>
<h3>Sơ chế các nguyên liệu khác</h3>
<p>Gừng cạo vỏ, rửa sạch rồi thái thành sợi.</p>
<p>Nếu dùng vừng, bạn nhặt bỏ những tạp chất lẫn vào vừng, sau đó rang vàng dậy mùi thơm rồi cho ra bát riêng.</p>
', 1, N'956f6fc0-72e1-4204-852b-73cff12eae37', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'58155a36-1615-472f-a659-ce588a9a1907', N'![image_2023-02-28_172711788.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F956f6fc0-72e1-4204-852b-73cff12eae37%2Fimage_2023-02-28_172711788.png112907fa-716d-484d-b73e-a341354fbc5f?alt=media&token=b478bc57-1784-42be-a3bb-1e145dec1113)

Cuối cùng tắt bếp và múc bánh ngào ra bát. Nếu dùng vừng rang thì rắc lên trên.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F956f6fc0-72e1-4204-852b-73cff12eae37%2Fimage_2023-02-28_172711788.png112907fa-716d-484d-b73e-a341354fbc5f?alt=media&amp;token=b478bc57-1784-42be-a3bb-1e145dec1113" alt="image_2023-02-28_172711788.png"></p>
<p>Cuối cùng tắt bếp và múc bánh ngào ra bát. Nếu dùng vừng rang thì rắc lên trên.</p>
', 3, N'956f6fc0-72e1-4204-852b-73cff12eae37', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'31a71346-1cff-45f9-b126-d5e715b2efb7', N'Làm theo cách này, món **salad rau mầm** trộn sốt mè rang sẽ không hề bị đắng, trái lại rất ngon và thơm.', N'<p>Làm theo cách này, món <strong>salad rau mầm</strong> trộn sốt mè rang sẽ không hề bị đắng, trái lại rất ngon và thơm.</p>
', 0, N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'f3411e60-4780-45d1-a322-d93e85b9546a', N'### Sơ chế tai heo

Vì tai heo khá bẩn nên cần phải sơ chế 2 lần.

Đầu tiên, tai heo khi mua về rửa lại và xát qua muối hạt.

Sau đó đun 1 nồi nước sôi và thả tai heo vào để chần qua trong khoảng 2-3 phút. Lúc chần tai heo bạn cho thêm 1 ít muối hạt và đừng đậy vung để các chất bẩn và mùi hôi được bay hơi đi.

Vớt tai heo ra và tiếp tục rửa sạch, xát muối, cạo sạch lông nơi kẽ tai bằng dao nhỏ mũi nhọn. Tai heo bẩn nhất ở các kẽ nhỏ nên cần chú ý sơ chế sạch sẽ ở những phần này.

Rửa sạch lại rồi cho vào nồi, đổ nước ngập mặt tai và thả thêm khoảng 1-2 củ hành và vài lát gừng để luộc cùng cho thơm.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.png59849f58-a534-420b-8199-6b7030d069d0?alt=media&token=b8eb6bb0-b131-4581-92ef-49a8a8657d53)

### Sơ chế các nguyên liệu khác

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.pngc3f6da50-b06c-425e-8e22-33c03492989d?alt=media&token=05a39dfc-dd39-4b11-bd7a-06fc40b48229)

Dưa chuột ngâm muối rửa sạch, nạo vỏ, bỏ phần đầu đuôi, sau đó cắt làm đôi. Để dưa chuột giòn và không bị chảy nước trong quá trình bóp nộm thì nên bỏ đi phần ruột.

Cà rốt rửa sạch, nạo vỏ rồi bào sợi. Nếu củ cà rốt to thì chỉ cần dùng nửa củ, còn nếu củ nhỏ thì có thể dùng gần hết. Nhiều củ cà rốt già lõi bên trong bị xốp thì nên bỏ đi. Nhìn chung, khi bào cà rốt các bạn có thể ước lượng sao cho cân bằng với lượng dưa chuột để món nộm vừa vị và hài hòa về màu sắc.

Tỏi, ớt băm nhỏ.

Đậu phộng rang chín, giã dập, không cần giã quá nhuyễn để khi ăn được bùi hơn.

Giá đỗ ngâm muối, rửa sạch để ráo nước.

Rau thơm nhặt rửa sạch rồi xắt nhỏ.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.pnge4395722-f982-495a-b56a-996e32a84d56?alt=media&token=1c9c10b7-7510-4d20-a9d7-15a01a3703a6)

### Thái và ướp tai heo

Tai heo chín vớt ra ngâm vào trong 1 tô nước đá khoảng 3-5 phút rồi vớt ra, thái miếng mỏng. Dùng thêm nước cốt chanh hoặc dấm cho vào ngâm cùng để tai heo trắng, giòn và thơm. Tuy nhiên cá nhân mình thấy chanh làm tai heo thơm hơn.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.pngb9a66fc8-c848-4d6d-9b52-acbd79d4ddcd?alt=media&token=f3c0cbe8-2918-4194-90c5-be88ec4dc226)

Để tai đậm đà hơn thì sẽ ướp cùng với: 1/2 thìa bột canh, 1/2 thìa hạt tiêu, trộn đều lên và để tai thấm gia vị thêm 5 phút nữa.

### Pha nước trộn nộm tai heo

Trong lúc đợi tai thấm vị thì tiến hành pha chế nước trộn nộm. Với liều lượng như trên, tỷ lệ pha nước trộn nộm sẽ gồm:

* 2 thìa đầy nước mắm
* 2 thìa đường
* 2 thìa nước lọc
* 2 thìa nước cốt chanh

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.pngbd3acb62-9754-4b1e-9ac8-9c117b8edd98?alt=media&token=bef89507-bf0e-4586-a101-f61f8e9c07bd)

Vì tai heo đã ướp qua với 1 chút bột canh nên tỷ lệ pha nước trộn như trên sẽ vừa ăn, còn nếu muốn ăn đậm hơn thì các bạn có thể chế thêm 1 ít nước mắm nữa là được.
', N'<h3>Sơ chế tai heo</h3>
<p>Vì tai heo khá bẩn nên cần phải sơ chế 2 lần.</p>
<p>Đầu tiên, tai heo khi mua về rửa lại và xát qua muối hạt.</p>
<p>Sau đó đun 1 nồi nước sôi và thả tai heo vào để chần qua trong khoảng 2-3 phút. Lúc chần tai heo bạn cho thêm 1 ít muối hạt và đừng đậy vung để các chất bẩn và mùi hôi được bay hơi đi.</p>
<p>Vớt tai heo ra và tiếp tục rửa sạch, xát muối, cạo sạch lông nơi kẽ tai bằng dao nhỏ mũi nhọn. Tai heo bẩn nhất ở các kẽ nhỏ nên cần chú ý sơ chế sạch sẽ ở những phần này.</p>
<p>Rửa sạch lại rồi cho vào nồi, đổ nước ngập mặt tai và thả thêm khoảng 1-2 củ hành và vài lát gừng để luộc cùng cho thơm.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.png59849f58-a534-420b-8199-6b7030d069d0?alt=media&amp;token=b8eb6bb0-b131-4581-92ef-49a8a8657d53" alt="image.png"></p>
<h3>Sơ chế các nguyên liệu khác</h3>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.pngc3f6da50-b06c-425e-8e22-33c03492989d?alt=media&amp;token=05a39dfc-dd39-4b11-bd7a-06fc40b48229" alt="image.png"></p>
<p>Dưa chuột ngâm muối rửa sạch, nạo vỏ, bỏ phần đầu đuôi, sau đó cắt làm đôi. Để dưa chuột giòn và không bị chảy nước trong quá trình bóp nộm thì nên bỏ đi phần ruột.</p>
<p>Cà rốt rửa sạch, nạo vỏ rồi bào sợi. Nếu củ cà rốt to thì chỉ cần dùng nửa củ, còn nếu củ nhỏ thì có thể dùng gần hết. Nhiều củ cà rốt già lõi bên trong bị xốp thì nên bỏ đi. Nhìn chung, khi bào cà rốt các bạn có thể ước lượng sao cho cân bằng với lượng dưa chuột để món nộm vừa vị và hài hòa về màu sắc.</p>
<p>Tỏi, ớt băm nhỏ.</p>
<p>Đậu phộng rang chín, giã dập, không cần giã quá nhuyễn để khi ăn được bùi hơn.</p>
<p>Giá đỗ ngâm muối, rửa sạch để ráo nước.</p>
<p>Rau thơm nhặt rửa sạch rồi xắt nhỏ.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.pnge4395722-f982-495a-b56a-996e32a84d56?alt=media&amp;token=1c9c10b7-7510-4d20-a9d7-15a01a3703a6" alt="image.png"></p>
<h3>Thái và ướp tai heo</h3>
<p>Tai heo chín vớt ra ngâm vào trong 1 tô nước đá khoảng 3-5 phút rồi vớt ra, thái miếng mỏng. Dùng thêm nước cốt chanh hoặc dấm cho vào ngâm cùng để tai heo trắng, giòn và thơm. Tuy nhiên cá nhân mình thấy chanh làm tai heo thơm hơn.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.pngb9a66fc8-c848-4d6d-9b52-acbd79d4ddcd?alt=media&amp;token=f3c0cbe8-2918-4194-90c5-be88ec4dc226" alt="image.png"></p>
<p>Để tai đậm đà hơn thì sẽ ướp cùng với: 1/2 thìa bột canh, 1/2 thìa hạt tiêu, trộn đều lên và để tai thấm gia vị thêm 5 phút nữa.</p>
<h3>Pha nước trộn nộm tai heo</h3>
<p>Trong lúc đợi tai thấm vị thì tiến hành pha chế nước trộn nộm. Với liều lượng như trên, tỷ lệ pha nước trộn nộm sẽ gồm:</p>
<ul>
<li>2 thìa đầy nước mắm</li>
<li>2 thìa đường</li>
<li>2 thìa nước lọc</li>
<li>2 thìa nước cốt chanh</li>
</ul>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage.pngbd3acb62-9754-4b1e-9ac8-9c117b8edd98?alt=media&amp;token=bef89507-bf0e-4586-a101-f61f8e9c07bd" alt="image.png"></p>
<p>Vì tai heo đã ướp qua với 1 chút bột canh nên tỷ lệ pha nước trộn như trên sẽ vừa ăn, còn nếu muốn ăn đậm hơn thì các bạn có thể chế thêm 1 ít nước mắm nữa là được.</p>
', 1, N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'14adfd1f-b701-4304-9908-da2618ddb91e', N'Để nem rán giòn, để lâu mà không bị ỉu, đồng thời lại có màu sắc vàng ruộm đẹp mắt, bạn nên phết lên vỏ nem 1 hỗn hợp được pha chế từ dấm, đường và nước.

Theo đó, cho 1/2 thìa canh đường vào nồi, nấu ở nhiệt độ vừa phải đủ để đường tan chảy. Khi đường chảy và chuyển sang màu nâu vàng thì tắt bếp, cho 2 thìa canh nước lọc. Tiếp đến cho 1,5 thìa canh dấm vào rồi khuấy đều.

Cho hỗn hợp này ra bát, để nguội. Lưu ý, vì có đường nên khi rán, các bạn phải cẩn thận kiểm soát được nhiệt độ của dầu rán, tránh vỏ nem bị cháy trong khi phần nhân chưa kịp chín.

![image_2023-02-28_112118012.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage_2023-02-28_112118012.pngeafdadce-47d0-47b1-8f51-e77497e79f64?alt=media&token=fde01b9f-7b77-419e-91f2-49eef4391756)

Đặt vỏ nem lên thớt, phết nước dấm đường vừa pha lên bề mặt vỏ, sau đó lật ngược xuống, cho nhân nem vào để cuốn. Mặt được phết lớp hỗn hợp trên sẽ ở ngoài, nên khi rán sẽ lên màu vàng rất đẹp mắt.  

Lưu ý, cho lượng nhân nem vừa đủ vào vỏ, cuốn 2 vòng rồi nhấn 2 đầu để định hình, sau đó gấp 2 mép ngoài vào rồi tiếp tục cuốn. Để làm chả nem không bị bung, ở lần cuốn cuối, bạn phết 1 ít lòng trắng trứng lên vỏ để tạo độ dính.

### Rán nem

Cho dầu vào chảo, lượng dầu nhiều đủ để nem ngập trong chảo. Đun nóng dầu (khoảng tầm nhiệt độ 140 độ C), dùng đũa chọc vào dưới đáy chảo thấy phủi bọt lăn tăn thì cho nem vào để rán.

![image_2023-02-28_112147011.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage_2023-02-28_112147011.png5e8c28a5-d190-4cb1-a108-d9c239af0fda?alt=media&token=12ebd2a6-1b6c-476f-bbef-450c276ac3e7)

Để nem giòn, ngon thì nên chiên 2 lần. Khi nem chín sẽ nổi lên bề mặt chảo, bạn vớt ra và lúc gần ăn thì rán lại. Ở lần chiên thứ 2, bạn chỉ cần đun nóng dầu (không cần tới 140 độ), thả nem vào để rán. Nhớ lật mặt nem để nem chín vàng đều. Dùng đũa ấn vào thấy vỏ cứng là được, vớt nem cho ra giấy thấm bớt dầu rồi xếp ra đĩa.', N'<p>Để nem rán giòn, để lâu mà không bị ỉu, đồng thời lại có màu sắc vàng ruộm đẹp mắt, bạn nên phết lên vỏ nem 1 hỗn hợp được pha chế từ dấm, đường và nước.</p>
<p>Theo đó, cho 1/2 thìa canh đường vào nồi, nấu ở nhiệt độ vừa phải đủ để đường tan chảy. Khi đường chảy và chuyển sang màu nâu vàng thì tắt bếp, cho 2 thìa canh nước lọc. Tiếp đến cho 1,5 thìa canh dấm vào rồi khuấy đều.</p>
<p>Cho hỗn hợp này ra bát, để nguội. Lưu ý, vì có đường nên khi rán, các bạn phải cẩn thận kiểm soát được nhiệt độ của dầu rán, tránh vỏ nem bị cháy trong khi phần nhân chưa kịp chín.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage_2023-02-28_112118012.pngeafdadce-47d0-47b1-8f51-e77497e79f64?alt=media&amp;token=fde01b9f-7b77-419e-91f2-49eef4391756" alt="image_2023-02-28_112118012.png"></p>
<p>Đặt vỏ nem lên thớt, phết nước dấm đường vừa pha lên bề mặt vỏ, sau đó lật ngược xuống, cho nhân nem vào để cuốn. Mặt được phết lớp hỗn hợp trên sẽ ở ngoài, nên khi rán sẽ lên màu vàng rất đẹp mắt.</p>
<p>Lưu ý, cho lượng nhân nem vừa đủ vào vỏ, cuốn 2 vòng rồi nhấn 2 đầu để định hình, sau đó gấp 2 mép ngoài vào rồi tiếp tục cuốn. Để làm chả nem không bị bung, ở lần cuốn cuối, bạn phết 1 ít lòng trắng trứng lên vỏ để tạo độ dính.</p>
<h3>Rán nem</h3>
<p>Cho dầu vào chảo, lượng dầu nhiều đủ để nem ngập trong chảo. Đun nóng dầu (khoảng tầm nhiệt độ 140 độ C), dùng đũa chọc vào dưới đáy chảo thấy phủi bọt lăn tăn thì cho nem vào để rán.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage_2023-02-28_112147011.png5e8c28a5-d190-4cb1-a108-d9c239af0fda?alt=media&amp;token=12ebd2a6-1b6c-476f-bbef-450c276ac3e7" alt="image_2023-02-28_112147011.png"></p>
<p>Để nem giòn, ngon thì nên chiên 2 lần. Khi nem chín sẽ nổi lên bề mặt chảo, bạn vớt ra và lúc gần ăn thì rán lại. Ở lần chiên thứ 2, bạn chỉ cần đun nóng dầu (không cần tới 140 độ), thả nem vào để rán. Nhớ lật mặt nem để nem chín vàng đều. Dùng đũa ấn vào thấy vỏ cứng là được, vớt nem cho ra giấy thấm bớt dầu rồi xếp ra đĩa.</p>
', 2, N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'f0f43f45-cd3a-43b3-bd2c-da3ff0b547ee', N'**Cách làm kho quẹt** tôm khô để chấm rau củ luộc, thịt luộc hay cơm cháy. Kho quẹt đậm đà từ nước mắm kho đường sền sệt quyện với tôm khô, thịt ba chỉ bùi bùi và thơm ngậy.', N'<p><strong>Cách làm kho quẹt</strong> tôm khô để chấm rau củ luộc, thịt luộc hay cơm cháy. Kho quẹt đậm đà từ nước mắm kho đường sền sệt quyện với tôm khô, thịt ba chỉ bùi bùi và thơm ngậy.</p>
', 0, N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'bf2eea64-e934-49ae-91d9-e4aca59b4eee', N'**Khổ qua xào trứng** hay còn gọi là mướp đắng xào trứng là món ăn ngon, bổ dưỡng. Tuy nhiên, làm thế nào để khổ qua bớt đắng lại là vấn đề khiến nhiều người lăn tăn khi thực hiện món ăn này.', N'<p><strong>Khổ qua xào trứng</strong> hay còn gọi là mướp đắng xào trứng là món ăn ngon, bổ dưỡng. Tuy nhiên, làm thế nào để khổ qua bớt đắng lại là vấn đề khiến nhiều người lăn tăn khi thực hiện món ăn này.</p>
', 0, N'346dfff7-d030-408f-b521-f4b15b139bd3', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'f5dd0709-52ac-4814-97f4-e5572e4acb0a', N'Đầu tiên là bước chiên qua cá để miếng cá được săn, thơm, khi nấu sẽ không bị nát và ngon hơn. Cho dầu vào chảo chống dính, phi thơm với 1 chút tỏi và phần sả băm, sau đó cho miếng cá vào chiên. Ở bước này chỉ cần chiên qua để miếng cá săn lại là được.

![image_2023-02-27_165711338.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F0458adf1-68ce-44b1-89b1-f069b44a3ee2%2Fimage_2023-02-27_165711338.png8f8589bc-0fcd-4b12-adc8-66176d55dceb?alt=media&token=56f78573-0d16-448d-9791-103332fa0ab0)

Cho khoảng 1,2L nước vào nồi, đổ phần bát nước cốt me đã lọc vào để nấu cùng. Sau khi nước sôi thì thả miếng cá vào. Sở dĩ cho phần nước cốt me vào trước, thả cá vào sau để phần nước chua của me sẽ dung hòa và khử đi mùi tanh của cá, giúp miếng cá ngọt thịt hơn. Nêm đường, nước mắm và mì chính vào nồi canh, sau đó thả cà chua, dứa, đậu bắp, bạc hà vào, khuấy đều. Lưu ý nhớ hớt bỏ lớp bọt để nước canh cá trong và thanh.

![image_2023-02-27_165725913.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F0458adf1-68ce-44b1-89b1-f069b44a3ee2%2Fimage_2023-02-27_165725913.pnge5d7066e-7fe4-48e8-8a65-d6b735cd7a67?alt=media&token=353f161d-98e2-40ab-ac1e-04e7ed73d59d)

Gắp miếng cá lóc bỏ ra tô trước, sau đó múc các nguyên liệu khác lên trên bề mặt rồi mới rưới nước canh lên trên bề mặt, cuối cùng cho thêm vài lát ớt và tỏi bằm đã phi vào để món canh thêm phần đẹp mắt và dậy mùi thơm.', N'<p>Đầu tiên là bước chiên qua cá để miếng cá được săn, thơm, khi nấu sẽ không bị nát và ngon hơn. Cho dầu vào chảo chống dính, phi thơm với 1 chút tỏi và phần sả băm, sau đó cho miếng cá vào chiên. Ở bước này chỉ cần chiên qua để miếng cá săn lại là được.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F0458adf1-68ce-44b1-89b1-f069b44a3ee2%2Fimage_2023-02-27_165711338.png8f8589bc-0fcd-4b12-adc8-66176d55dceb?alt=media&amp;token=56f78573-0d16-448d-9791-103332fa0ab0" alt="image_2023-02-27_165711338.png"></p>
<p>Cho khoảng 1,2L nước vào nồi, đổ phần bát nước cốt me đã lọc vào để nấu cùng. Sau khi nước sôi thì thả miếng cá vào. Sở dĩ cho phần nước cốt me vào trước, thả cá vào sau để phần nước chua của me sẽ dung hòa và khử đi mùi tanh của cá, giúp miếng cá ngọt thịt hơn. Nêm đường, nước mắm và mì chính vào nồi canh, sau đó thả cà chua, dứa, đậu bắp, bạc hà vào, khuấy đều. Lưu ý nhớ hớt bỏ lớp bọt để nước canh cá trong và thanh.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F0458adf1-68ce-44b1-89b1-f069b44a3ee2%2Fimage_2023-02-27_165725913.pnge5d7066e-7fe4-48e8-8a65-d6b735cd7a67?alt=media&amp;token=353f161d-98e2-40ab-ac1e-04e7ed73d59d" alt="image_2023-02-27_165725913.png"></p>
<p>Gắp miếng cá lóc bỏ ra tô trước, sau đó múc các nguyên liệu khác lên trên bề mặt rồi mới rưới nước canh lên trên bề mặt, cuối cùng cho thêm vài lát ớt và tỏi bằm đã phi vào để món canh thêm phần đẹp mắt và dậy mùi thơm.</p>
', 2, N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'd7f1a31a-155a-4d15-8d66-e63007883db9', N'### Sơ chế mực

Rút đầu mực riêng ra, sau đó ngắt bỏ phần túi mực đen và màng nhầy nơi cuối đầu mực. Tiếp đến rút xương sống dọc lưng mực và lột lớp da lụa bên ngoài thân mực.

![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage.pngef9d475f-24f5-43cd-8b3e-a05cb58655e8?alt=media&token=d216ab9e-b5e8-4d53-a0b8-29addc842a24)

Mực có mùi tanh đặc trưng, để khử mùi tanh này bạn bóp mực cùng với muối hạt, rượu trắng và 1 ít gừng giã nhuyễn. Sau đó đem rửa sạch lại rồi để mực ráo nước rồi cắt thành miếng vừa ăn.

### Sơ chế các nguyên liệu khác

Hành tây lột vỏ, rửa sạch rồi cắt miếng vuông nhỏ. Hoặc bạn cũng có thể bổ múi cau nhỏ tùy ý.

Ớt chuông bỏ ruột, bỏ hạt, cắt miếng vuông nhỏ giống như hành tây. Ớt chuông có độ giòn, thanh mát, sẽ giúp cho món mực xào không bị ngán. Đặc biệt màu sắc rực rỡ của ớt chuông sẽ làm cho món ăn đẹp mắt hơn rất nhiều.

Bông cải ngâm rửa nước muối sạch sẽ, tách thành những nhánh nhỏ.

Tép tỏi đập dập, băm nhỏ. Gừng cắt bỏ những phần thâm, héo rồi rửa sạch, đập dập băm nhỏ.

Ớt sừng rửa sạch, cắt lát chéo.

Cần tây cắt rễ, nhặt rồi ngâm rửa nước muối sạch sẽ, sau đó thái thành những khúc dài khoảng 2cm.

![image_2023-02-28_170842169.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage_2023-02-28_170842169.png58a4de82-68bb-4b03-99a2-32b771054c94?alt=media&token=1c083761-5ae7-4056-aa79-d8209d585063)

### Ướp mực

Ướp mực cùng với 1 thìa cà phê nước mắm, 1 thìa cà phê dầu hào, 1 thìa cà phê sa tế, 1 thìa cà phê bột nêm, 1/2 thìa cà phê bột ngọt, 1/2 thìa cà phê tiêu xay, cho thêm 1 ít gừng băm vào để ướp cùng.

Nếu có ớt bột, bạn có thể cho thêm 1 thìa cà phê, vừa giúp khử tanh vừa tạo màu sắc đẹp mắt cho mực. Vì món này dùng sa tế sẽ rất cay rồi, nên bạn có thể dùng ớt bột loại không cay.

Trộn đều và để mực thấm gia vị trong khoảng 20-25 phút.

![image_2023-02-28_170908699.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage_2023-02-28_170908699.png88003a15-582c-42ff-bedc-219b3298b00e?alt=media&token=039a59ad-3e85-4f2c-ae23-d44c33705023)

', N'<h3>Sơ chế mực</h3>
<p>Rút đầu mực riêng ra, sau đó ngắt bỏ phần túi mực đen và màng nhầy nơi cuối đầu mực. Tiếp đến rút xương sống dọc lưng mực và lột lớp da lụa bên ngoài thân mực.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage.pngef9d475f-24f5-43cd-8b3e-a05cb58655e8?alt=media&amp;token=d216ab9e-b5e8-4d53-a0b8-29addc842a24" alt="image.png"></p>
<p>Mực có mùi tanh đặc trưng, để khử mùi tanh này bạn bóp mực cùng với muối hạt, rượu trắng và 1 ít gừng giã nhuyễn. Sau đó đem rửa sạch lại rồi để mực ráo nước rồi cắt thành miếng vừa ăn.</p>
<h3>Sơ chế các nguyên liệu khác</h3>
<p>Hành tây lột vỏ, rửa sạch rồi cắt miếng vuông nhỏ. Hoặc bạn cũng có thể bổ múi cau nhỏ tùy ý.</p>
<p>Ớt chuông bỏ ruột, bỏ hạt, cắt miếng vuông nhỏ giống như hành tây. Ớt chuông có độ giòn, thanh mát, sẽ giúp cho món mực xào không bị ngán. Đặc biệt màu sắc rực rỡ của ớt chuông sẽ làm cho món ăn đẹp mắt hơn rất nhiều.</p>
<p>Bông cải ngâm rửa nước muối sạch sẽ, tách thành những nhánh nhỏ.</p>
<p>Tép tỏi đập dập, băm nhỏ. Gừng cắt bỏ những phần thâm, héo rồi rửa sạch, đập dập băm nhỏ.</p>
<p>Ớt sừng rửa sạch, cắt lát chéo.</p>
<p>Cần tây cắt rễ, nhặt rồi ngâm rửa nước muối sạch sẽ, sau đó thái thành những khúc dài khoảng 2cm.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage_2023-02-28_170842169.png58a4de82-68bb-4b03-99a2-32b771054c94?alt=media&amp;token=1c083761-5ae7-4056-aa79-d8209d585063" alt="image_2023-02-28_170842169.png"></p>
<h3>Ướp mực</h3>
<p>Ướp mực cùng với 1 thìa cà phê nước mắm, 1 thìa cà phê dầu hào, 1 thìa cà phê sa tế, 1 thìa cà phê bột nêm, 1/2 thìa cà phê bột ngọt, 1/2 thìa cà phê tiêu xay, cho thêm 1 ít gừng băm vào để ướp cùng.</p>
<p>Nếu có ớt bột, bạn có thể cho thêm 1 thìa cà phê, vừa giúp khử tanh vừa tạo màu sắc đẹp mắt cho mực. Vì món này dùng sa tế sẽ rất cay rồi, nên bạn có thể dùng ớt bột loại không cay.</p>
<p>Trộn đều và để mực thấm gia vị trong khoảng 20-25 phút.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage_2023-02-28_170908699.png88003a15-582c-42ff-bedc-219b3298b00e?alt=media&amp;token=039a59ad-3e85-4f2c-ae23-d44c33705023" alt="image_2023-02-28_170908699.png"></p>
', 1, N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'e1e09b66-8c0b-4cb2-bc9e-e68f9dadbe78', N'Cà pháo giòn rôn rốt, dưa chuột ngọt, rau thơm bùi bùi quyện với vị đậm đà đặc trưng của mắm tôm và cay nồng của ớt hòa tan trong miệng khiến cho người ăn phải xuýt xoa. Món này ăn kèm với cơm nóng rất hao cơm. Thậm chí đây còn là món nhậu lai rai vô cùng hấp dẫn của rất nhiều người bởi hương vị đậm thơm, đưa miệng của nó.

Trước đây, cà pháo mắm tôm là món ăn quen thuộc đối với rất nhiều gia đình ở miền Trung. Tuy nhiên hiện nay món ăn này dường như chỉ còn là ký ức. Cùng với sự phát triển của đời sống thì thực tế, cà pháo ăn sống khá độc hại đối với sức khỏe con người. Chính vì vậy, không nên ăn cà pháo sống quá nhiều và cần phải chế biến kỹ càng như Homnayangi đã chia sẻ ở trên để giảm thiểu tác hại của nó đối với người dùng.', N'<p>Cà pháo giòn rôn rốt, dưa chuột ngọt, rau thơm bùi bùi quyện với vị đậm đà đặc trưng của mắm tôm và cay nồng của ớt hòa tan trong miệng khiến cho người ăn phải xuýt xoa. Món này ăn kèm với cơm nóng rất hao cơm. Thậm chí đây còn là món nhậu lai rai vô cùng hấp dẫn của rất nhiều người bởi hương vị đậm thơm, đưa miệng của nó.</p>
<p>Trước đây, cà pháo mắm tôm là món ăn quen thuộc đối với rất nhiều gia đình ở miền Trung. Tuy nhiên hiện nay món ăn này dường như chỉ còn là ký ức. Cùng với sự phát triển của đời sống thì thực tế, cà pháo ăn sống khá độc hại đối với sức khỏe con người. Chính vì vậy, không nên ăn cà pháo sống quá nhiều và cần phải chế biến kỹ càng như Homnayangi đã chia sẻ ở trên để giảm thiểu tác hại của nó đối với người dùng.</p>
', 3, N'047935c2-9a60-4b0e-865a-89c5e1a680e5', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'9fc89ade-70c0-4754-8500-e83fd0a721c9', N'Có 2 cách để thưởng thức mì ý, cách thứ nhất bạn cho mì ý ra đĩa trước rồi mới rưới nước sốt bò bằm lên trên. Cách thứ 2, sau khi nấu xong sốt, bạn đổ mì vào và trộn đều, sau đó cho ra đĩa. 

![image_2023-02-27_220022141.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_220022141.png0357301b-2849-418a-89e2-f3f3c873d743?alt=media&token=f2da454b-6451-4b55-b7b3-7c149b0a1a22)', N'<p>Có 2 cách để thưởng thức mì ý, cách thứ nhất bạn cho mì ý ra đĩa trước rồi mới rưới nước sốt bò bằm lên trên. Cách thứ 2, sau khi nấu xong sốt, bạn đổ mì vào và trộn đều, sau đó cho ra đĩa.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_220022141.png0357301b-2849-418a-89e2-f3f3c873d743?alt=media&amp;token=f2da454b-6451-4b55-b7b3-7c149b0a1a22" alt="image_2023-02-27_220022141.png"></p>
', 3, N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'f0abc89b-1d52-476d-a64c-eee1562bde4d', N'![image.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage.png133f3150-0efb-4974-8ccd-119f90d6a984?alt=media&token=c760f1d5-92a4-42cd-879d-6f96a59bd941)

Như vậy là đã hoàn thành xong món nem rán. Chả nem rán vỏ ngoài vàng, giòn tan trong khi phần nhân mềm thơm và đậm vị. Nem rán ăn kèm với bún, chấm nước mắm chua chua cay cay cùng rau sống rất hấp dẫn. 

Hy vọng với những chia sẻ chi tiết trên đây, các bạn có thể tự tin trổ tài món nem rán để chiêu đãi cả nhà, đặc biệt vào các dịp giỗ chạp, lễ Tết nhé!', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage.png133f3150-0efb-4974-8ccd-119f90d6a984?alt=media&amp;token=c760f1d5-92a4-42cd-879d-6f96a59bd941" alt="image.png"></p>
<p>Như vậy là đã hoàn thành xong món nem rán. Chả nem rán vỏ ngoài vàng, giòn tan trong khi phần nhân mềm thơm và đậm vị. Nem rán ăn kèm với bún, chấm nước mắm chua chua cay cay cùng rau sống rất hấp dẫn.</p>
<p>Hy vọng với những chia sẻ chi tiết trên đây, các bạn có thể tự tin trổ tài món nem rán để chiêu đãi cả nhà, đặc biệt vào các dịp giỗ chạp, lễ Tết nhé!</p>
', 3, N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'6ca0d5ce-7485-40c7-918b-f91ae23f0794', N'Cho 2 thìa dầu ăn vào chảo, làm nóng dầu thì phi thơm hành, tỏi, gừng và 1 nửa số sả.

![image_2023-02-28_110757075.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110757075.png366d608f-a517-40fe-bbe3-6e9270d0937f?alt=media&token=c7fcc95d-b1f8-4d9d-b4bb-021cdcbc67c2)

Phi thơm vàng tất cả các nguyên liệu trên rồi thêm 1 thìa canh nước mắm vào cho thơm.

Tiếp đó trút thịt gà vào xào với lửa lớn để lớp da bên ngoài được giòn trong khi lớp thịt bên trong chín mềm mà không bị khô.

![image_2023-02-28_110812621.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110812621.pngfedec262-87d4-4abc-b717-744b49433a07?alt=media&token=3288e92f-1113-4f77-a35d-5b6593279665)

Xào đến khi thịt gà săn lại thì cho nốt phần sả còn lại và ớt cay vào. Xào thêm 1 phút rồi nêm gia vị gồm nước mắm, bột ngọt cho vừa vị. Tiếp tục xào để thịt gà chín. Trước khi tắt bếp 15 giây thì thêm lá chanh, chút hạt tiêu vào đảo qua rồi tắt bếp.', N'<p>Cho 2 thìa dầu ăn vào chảo, làm nóng dầu thì phi thơm hành, tỏi, gừng và 1 nửa số sả.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110757075.png366d608f-a517-40fe-bbe3-6e9270d0937f?alt=media&amp;token=c7fcc95d-b1f8-4d9d-b4bb-021cdcbc67c2" alt="image_2023-02-28_110757075.png"></p>
<p>Phi thơm vàng tất cả các nguyên liệu trên rồi thêm 1 thìa canh nước mắm vào cho thơm.</p>
<p>Tiếp đó trút thịt gà vào xào với lửa lớn để lớp da bên ngoài được giòn trong khi lớp thịt bên trong chín mềm mà không bị khô.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110812621.pngfedec262-87d4-4abc-b717-744b49433a07?alt=media&amp;token=3288e92f-1113-4f77-a35d-5b6593279665" alt="image_2023-02-28_110812621.png"></p>
<p>Xào đến khi thịt gà săn lại thì cho nốt phần sả còn lại và ớt cay vào. Xào thêm 1 phút rồi nêm gia vị gồm nước mắm, bột ngọt cho vừa vị. Tiếp tục xào để thịt gà chín. Trước khi tắt bếp 15 giây thì thêm lá chanh, chút hạt tiêu vào đảo qua rồi tắt bếp.</p>
', 2, N'e3f6c554-29c7-460d-a692-d23d15dae638', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'e5ad0413-3222-4991-8d50-fb1a0ab9701a', N'![image_2023-02-28_110831215.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110831215.pngfc7e3e7d-9818-44c8-9e04-690c38dc310c?alt=media&token=1f4a48d5-4d5e-428b-9519-b4653f22d8a0)

Món ăn có màu vàng sẫm, điểm xuyết vài lát ớt đỏ trông rất đẹp mắt.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110831215.pngfc7e3e7d-9818-44c8-9e04-690c38dc310c?alt=media&amp;token=1f4a48d5-4d5e-428b-9519-b4653f22d8a0" alt="image_2023-02-28_110831215.png"></p>
<p>Món ăn có màu vàng sẫm, điểm xuyết vài lát ớt đỏ trông rất đẹp mắt.</p>
', 3, N'e3f6c554-29c7-460d-a692-d23d15dae638', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'b1689284-fd01-4949-bd92-fb7afdec6ba9', N'![image_2023-02-27_175333642.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F346dfff7-d030-408f-b521-f4b15b139bd3%2Fimage_2023-02-27_175333642.png4fd21072-b3b8-4412-8880-4258bac1859b?alt=media&token=22783803-3673-4594-8661-8b45887a875f)

Khổ qua xào trứng nên ăn lúc nóng, ăn nóng thì khổ qua giòn, không đắng, trái lại còn có vị ngọt. Để nguội khổ qua ăn sẽ không thơm và còn thoang thoảng vị đắng.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F346dfff7-d030-408f-b521-f4b15b139bd3%2Fimage_2023-02-27_175333642.png4fd21072-b3b8-4412-8880-4258bac1859b?alt=media&amp;token=22783803-3673-4594-8661-8b45887a875f" alt="image_2023-02-27_175333642.png"></p>
<p>Khổ qua xào trứng nên ăn lúc nóng, ăn nóng thì khổ qua giòn, không đắng, trái lại còn có vị ngọt. Để nguội khổ qua ăn sẽ không thơm và còn thoang thoảng vị đắng.</p>
', 3, N'346dfff7-d030-408f-b521-f4b15b139bd3', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'0703608f-2198-4753-b147-fc3cf5f4e451', N'Miếng thịt kho mềm rục có màu trắng trong của lớp mỡ và đỏ au của thịt nạc, màu nâu cánh gián bóng bẩy của lớp bì heo hầm nhừ, màu nước đường vàng đậm, sóng sánh, hương vị thơm ngon, bùi bùi ngầy ngậy.

![image_2023-02-27_182125679.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_182125679.png6f61ec4b-2a38-4280-9c8d-6355f5ae6414?alt=media&token=e9b0e841-aa91-4349-8f4f-81dbc15c9617)

Trứng chín mềm, lòng đỏ béo mịn. Kèm theo đó vị ngọt thanh của nước dừa xiêm, vị mặn đậm đà của nước mắm ngon, cay the the của những lát ớt đỏ, tất cả hoà quyện tạo nên một hương vị rất hấp dẫn.

Món ăn có thể kết hợp cùng nhiều món ăn khác như củ kiệu, dưa muối, bánh tét hay xôi trắng. Thậm chí chỉ cần ăn thịt kho tàu, rưới thêm chút nước thịt kho ăn cùng với cơm trắng cũng đã rất ngon và đưa miệng.', N'<p>Miếng thịt kho mềm rục có màu trắng trong của lớp mỡ và đỏ au của thịt nạc, màu nâu cánh gián bóng bẩy của lớp bì heo hầm nhừ, màu nước đường vàng đậm, sóng sánh, hương vị thơm ngon, bùi bùi ngầy ngậy.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_182125679.png6f61ec4b-2a38-4280-9c8d-6355f5ae6414?alt=media&amp;token=e9b0e841-aa91-4349-8f4f-81dbc15c9617" alt="image_2023-02-27_182125679.png"></p>
<p>Trứng chín mềm, lòng đỏ béo mịn. Kèm theo đó vị ngọt thanh của nước dừa xiêm, vị mặn đậm đà của nước mắm ngon, cay the the của những lát ớt đỏ, tất cả hoà quyện tạo nên một hương vị rất hấp dẫn.</p>
<p>Món ăn có thể kết hợp cùng nhiều món ăn khác như củ kiệu, dưa muối, bánh tét hay xôi trắng. Thậm chí chỉ cần ăn thịt kho tàu, rưới thêm chút nước thịt kho ăn cùng với cơm trắng cũng đã rất ngon và đưa miệng.</p>
', 3, N'a8ab27eb-4401-4b4b-93b9-bda34397621f', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'd79b2284-83b7-4073-b24b-fc8cf4e2a526', N'![image_2023-02-27_173501383.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fcc8da8e0-0c9d-4893-8ea1-387c0519a0f3%2Fimage_2023-02-27_173501383.png19e8356b-fbc8-46f8-bbf5-e0ae76d6e7ad?alt=media&token=d7e11802-0a36-418c-94cb-fca3304747a8)

Nếu không có nhiều thời gian và vẫn muốn được thưởng thức món sườn xào chua ngọt mềm ngon hấp dẫn, bạn hãy thử cách chế biến này.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fcc8da8e0-0c9d-4893-8ea1-387c0519a0f3%2Fimage_2023-02-27_173501383.png19e8356b-fbc8-46f8-bbf5-e0ae76d6e7ad?alt=media&amp;token=d7e11802-0a36-418c-94cb-fca3304747a8" alt="image_2023-02-27_173501383.png"></p>
<p>Nếu không có nhiều thời gian và vẫn muốn được thưởng thức món sườn xào chua ngọt mềm ngon hấp dẫn, bạn hãy thử cách chế biến này.</p>
', 3, N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', 2)
INSERT [dbo].[BlogReference] ([blogReferenceId], [text], [html], [type], [blogId], [status]) VALUES (N'4636520d-4f0e-4569-bd82-fffbcdd0be38', N'![image_2023-02-27_163434502.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163434502.png537f670e-ba87-4752-a23d-05562da8f6a5?alt=media&token=9cfbb216-bec5-41d1-9c7c-57a9b9b55a94)

Bí đỏ rửa sạch, gọt vỏ, bổ đôi, cạo bỏ phần ruột và hạt. Thái bí đỏ thành từng khúc vừa ăn, sau đó ướp với 1 chút bột canh để bí được đậm đà.

Tôm lột vỏ, bỏ chỉ đen nơi dọc sống lưng.


![image_2023-02-27_163505491.png](https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163505491.pngb7710e27-95a1-42a5-a17d-874eb6de0513?alt=media&token=22c66816-e693-423d-af35-b878e92c14eb)

Phần thịt tôm băm nhỏ, ướp cùng với 1 ít bột canh hoặc nước mắm. Ướp tôm khoảng 10 phút cho tôm thấm gia vị.

Hành khô và tỏi bóc vỏ, băm nhỏ.

Hành lá, mùi tàu rửa sạch, xắt nhỏ.', N'<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163434502.png537f670e-ba87-4752-a23d-05562da8f6a5?alt=media&amp;token=9cfbb216-bec5-41d1-9c7c-57a9b9b55a94" alt="image_2023-02-27_163434502.png"></p>
<p>Bí đỏ rửa sạch, gọt vỏ, bổ đôi, cạo bỏ phần ruột và hạt. Thái bí đỏ thành từng khúc vừa ăn, sau đó ướp với 1 chút bột canh để bí được đậm đà.</p>
<p>Tôm lột vỏ, bỏ chỉ đen nơi dọc sống lưng.</p>
<p><img src="https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163505491.pngb7710e27-95a1-42a5-a17d-874eb6de0513?alt=media&amp;token=22c66816-e693-423d-af35-b878e92c14eb" alt="image_2023-02-27_163505491.png"></p>
<p>Phần thịt tôm băm nhỏ, ướp cùng với 1 ít bột canh hoặc nước mắm. Ướp tôm khoảng 10 phút cho tôm thấm gia vị.</p>
<p>Hành khô và tỏi bóc vỏ, băm nhỏ.</p>
<p>Hành lá, mùi tàu rửa sạch, xắt nhỏ.</p>
', 1, N'5b579874-46a8-4895-bad2-1f33d4cb006a', 2)
GO
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', N'8c6d0b63-74d9-4931-9c2a-4b7c8b54ed0e', CAST(N'2023-02-27T21:52:50.643' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', N'8b720ab2-25de-4aae-bef2-a3f780071386', CAST(N'2023-02-27T21:52:51.743' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'5b579874-46a8-4895-bad2-1f33d4cb006a', N'9747494f-5a51-4056-bef5-c6c03305ee5c', CAST(N'2023-02-27T16:32:58.657' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'ac41da30-8998-4a1f-b2e4-291d0b24dcf4', N'c72749fc-917a-469d-b17e-c5c934c350f4', CAST(N'2023-03-03T23:34:09.847' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'ac41da30-8998-4a1f-b2e4-291d0b24dcf4', N'e23f1a4a-f82c-41e3-9305-d50f77566808', CAST(N'2023-03-03T23:34:11.957' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'b5603c80-4003-41f9-a7a5-3076de517599', N'8c6d0b63-74d9-4931-9c2a-4b7c8b54ed0e', CAST(N'2023-02-27T22:09:13.313' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'b5603c80-4003-41f9-a7a5-3076de517599', N'8b720ab2-25de-4aae-bef2-a3f780071386', CAST(N'2023-02-27T22:09:13.317' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', N'13e8010c-f583-4a32-9a76-a7f188983c69', CAST(N'2023-02-28T15:57:49.767' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', N'8519d30b-dfa2-45e0-9541-963e11886015', CAST(N'2023-02-27T17:29:17.410' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', N'13e8010c-f583-4a32-9a76-a7f188983c69', CAST(N'2023-02-28T16:04:35.323' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', N'93a460cc-f092-447f-aaf5-0564cd0ffadc', CAST(N'2023-03-03T23:26:22.310' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', N'c72749fc-917a-469d-b17e-c5c934c350f4', CAST(N'2023-03-03T23:26:23.747' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', N'e23f1a4a-f82c-41e3-9305-d50f77566808', CAST(N'2023-03-03T23:26:33.417' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', N'a7766c2a-8faf-48f9-bdec-1dfaba22ecf3', CAST(N'2023-02-28T12:43:03.780' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'5de76825-4f4e-469d-b94e-5e457294b045', N'9747494f-5a51-4056-bef5-c6c03305ee5c', CAST(N'2023-02-27T16:24:12.483' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'a7766c2a-8faf-48f9-bdec-1dfaba22ecf3', CAST(N'2023-02-28T11:16:20.830' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'f578682a-d641-4a0b-aa54-6a81eaf7be08', N'8b720ab2-25de-4aae-bef2-a3f780071386', CAST(N'2023-02-27T22:55:09.337' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'f578682a-d641-4a0b-aa54-6a81eaf7be08', N'44d5227c-d10d-465e-a509-f70e469143ea', CAST(N'2023-02-27T22:55:10.120' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'956f6fc0-72e1-4204-852b-73cff12eae37', N'c368e82d-dda4-4b39-bc19-43ef148981e0', CAST(N'2023-02-28T17:24:12.247' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'b9f39ecc-c689-4910-ac82-816e20994156', N'76bc7165-d0be-4d32-b819-434dfaeba2ad', CAST(N'2023-02-27T21:24:55.377' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'047935c2-9a60-4b0e-865a-89c5e1a680e5', N'c368e82d-dda4-4b39-bc19-43ef148981e0', CAST(N'2023-02-28T17:15:40.403' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', N'13e8010c-f583-4a32-9a76-a7f188983c69', CAST(N'2023-02-28T17:05:15.607' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', N'93a460cc-f092-447f-aaf5-0564cd0ffadc', CAST(N'2023-02-28T17:37:15.043' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', N'e23f1a4a-f82c-41e3-9305-d50f77566808', CAST(N'2023-02-28T17:37:16.040' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', N'8b720ab2-25de-4aae-bef2-a3f780071386', CAST(N'2023-02-28T10:41:04.097' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', N'44d5227c-d10d-465e-a509-f70e469143ea', CAST(N'2023-02-28T10:41:04.870' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'a8ab27eb-4401-4b4b-93b9-bda34397621f', N'76bc7165-d0be-4d32-b819-434dfaeba2ad', CAST(N'2023-02-27T18:08:44.830' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'e3f6c554-29c7-460d-a692-d23d15dae638', N'a7766c2a-8faf-48f9-bdec-1dfaba22ecf3', CAST(N'2023-02-28T11:03:27.920' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', N'9747494f-5a51-4056-bef5-c6c03305ee5c', CAST(N'2023-02-27T16:50:37.880' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'346dfff7-d030-408f-b521-f4b15b139bd3', N'8519d30b-dfa2-45e0-9541-963e11886015', CAST(N'2023-02-27T17:48:45.240' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'dc7234fa-21fd-45bc-9612-f4fb5e2f4f8b', N'8519d30b-dfa2-45e0-9541-963e11886015', CAST(N'2023-02-27T17:38:08.060' AS DateTime), NULL)
GO
INSERT [dbo].[CaloReference] ([caloReferenceId], [fromAge], [toAge], [calo], [isMale]) VALUES (N'f8c8138f-4e79-4a83-b891-28b2562ad382', 20, 25, 2600, 1)
GO
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'433b682e-3651-4f4b-b688-0eaf344c51bd', N'Phong cách ăn uống', N'Các món ăn được phân loại theo phong cách ăn uống phù hợp với lựa chọn của người dùng', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'13e8bc02-41c4-44ab-bf5f-28fa6c729c41', N'Mùa và Dịp lễ', N'các mùa và dịp lễ trong năm', 1, CAST(N'2023-03-01T16:12:29.097' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'2d80def2-0135-4373-a4e6-2b15fc0166b6', N'Thực đơn hôm nay', N'Các món ăn được chia theo thực đơn nhằm gợi ý cho người dùng thực đơn phù hợp', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'3fa85f64-5717-4562-b3fc-2c963f66afa1', N'Loại nguyên liệu', N'Các món ăn được phân loại dựa theo loại thịt chính làm nên món ăn đó', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'872ec1f0-7e66-4947-a635-4aa2f36170f4', N'Độ khó', N'Độ khó khi nấu các món ăn', 1, CAST(N'2023-02-08T02:56:53.360' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'14465782-6f37-4a8f-8d5e-7a0bea7f3b4c', N'Đặc trưng vùng miền', N'Các món ăn với đặc trưng vùng miền khác nhau', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'63db7259-a32a-4a5e-beb8-985f3db5f63f', N'Lứa tuổi', N'Các món ăn có chế độ dinh dưỡng phù hợp cho các lứa tuổi', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'b3648659-6a2a-4795-80ea-b9f987d88b03', N'Khác', N'Bao gồm các cách phân biệt món ăn khác như độ khó, thời gian làm ngắn hay dài.', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'e5c95ca2-d0fb-4002-8c0f-da4b39312730', N'Bữa ăn', N'Các món ăn phù hợp vói mâm cơm của các bữa ăn trong ngày', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'8033733e-b4d5-4dfb-8316-e66481022cf2', N'Cách thực hiện', N'Các món ăn với cách thực hiện khác nhau', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'3b8ab3d0-08df-47f1-bf9f-ecf895fd0daa', N'Món nước', N'Các món ăn ăn kèm với nước như bún, hủ tiếu,...', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[CronJobTimeConfig] ([cronJobTimeConfigId], [minute], [hour], [day], [month], [createdDate], [updatedDate], [targetObject]) VALUES (N'9314bdfa-5543-48b4-aea9-864ef1013fcd', 5, NULL, NULL, NULL, CAST(N'2023-04-07T21:26:19.480' AS DateTime), CAST(N'2023-04-07T21:26:19.480' AS DateTime), 0)
INSERT [dbo].[CronJobTimeConfig] ([cronJobTimeConfigId], [minute], [hour], [day], [month], [createdDate], [updatedDate], [targetObject]) VALUES (N'075d697c-c33f-478b-bf38-e3c76386a2cc', 5, 2, NULL, NULL, CAST(N'2023-04-07T21:27:26.140' AS DateTime), CAST(N'2023-04-08T10:47:31.500' AS DateTime), 1)
GO
INSERT [dbo].[Customer] ([customerId], [displayname], [username], [firstname], [lastname], [email], [password], [phonenumber], [gender], [avatar], [createdDate], [updatedDate], [isBlocked], [isGoogle]) VALUES (N'e7e1bd28-8979-4b6e-adbc-458408e6ba41', NULL, N'Peter Vo', N'Võ', N'Văn Phương', N'vanphuong0606@gmail.com', N'31m2312', N'0971775169', 1, N'Nolink', CAST(N'2023-01-14T00:10:44.770' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Customer] ([customerId], [displayname], [username], [firstname], [lastname], [email], [password], [phonenumber], [gender], [avatar], [createdDate], [updatedDate], [isBlocked], [isGoogle]) VALUES (N'89fcfb56-be89-442d-9b99-8f498023a5cc', NULL, N'customer3', N'Nhựt', N'Phản', NULL, N'kVU41twDyttUL/SM7IO0vQ==', N'0965421541', 1, NULL, CAST(N'2023-03-22T23:42:49.643' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[Customer] ([customerId], [displayname], [username], [firstname], [lastname], [email], [password], [phonenumber], [gender], [avatar], [createdDate], [updatedDate], [isBlocked], [isGoogle]) VALUES (N'31a1c0df-178d-40aa-96f1-bc932e482d22', N'Nam Nguyen Luong Hoang', NULL, NULL, NULL, N'namnlhse151269@fpt.edu.vn', NULL, NULL, NULL, N'https://lh3.googleusercontent.com/a/AGNmyxbiiDVaHtibVBOeVWlSDOVkDDVloV0FuLWomlOI=s96-c', NULL, NULL, 0, 1)
INSERT [dbo].[Customer] ([customerId], [displayname], [username], [firstname], [lastname], [email], [password], [phonenumber], [gender], [avatar], [createdDate], [updatedDate], [isBlocked], [isGoogle]) VALUES (N'a97b5099-c8ab-4b93-9960-e1d82f234ad5', NULL, N'nhutgdgdg', N'Phan', N'Minh Bruh', NULL, N'kVU41twDyttUL/SM7IO0vQ==', N'0986451241', 1, NULL, CAST(N'2023-02-27T12:47:08.300' AS DateTime), NULL, 0, NULL)
GO
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'dc0a73f3-be27-49ea-bc0b-0022c5404972', N'Mắm tôm', N'mắm tôm', N'a2d09468-11ea-4db0-bb25-11b2b01d89a1', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Ffcc40894-ba3f-4f01-a5b2-0646d9864a61%2Fimage_2023-02-28_171218035.png87015b48-e16d-443e-aaaf-d0e297d43037?alt=media&token=c936e560-82cf-4c9c-8bb4-7e5e213d8d39', 100, CAST(N'2023-02-28T17:12:28.513' AS DateTime), NULL, 1, 20000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Ffcc40894-ba3f-4f01-a5b2-0646d9864a61%2Fimage_2023-02-28_171219251.pnga2a90e35-0904-4bfb-92f3-584df3655d87?alt=media&token=41fb3dcf-3f90-4e57-9739-f65afab510ac', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'fcc40894-ba3f-4f01-a5b2-0646d9864a61')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'Tỏi', N'tỏi', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F73b86529-4552-43f8-8daf-a4c606c5314c%2Fimage_2023-02-27_163127665.pnge45f5c72-a674-460f-be93-4b689d967a2a?alt=media&token=0c485302-ff2e-41de-88cf-f221e750a845', 150, CAST(N'2023-02-27T16:31:48.320' AS DateTime), NULL, 1, 20000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F73b86529-4552-43f8-8daf-a4c606c5314c%2Fimage_2023-02-27_163129047.png595dc530-8589-42d2-ba35-d4a31b273a18?alt=media&token=f770733c-8b61-4cb4-843e-47bd7b291e10', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'73b86529-4552-43f8-8daf-a4c606c5314c')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'ea417c29-dd3b-4ae7-a1e8-14938e3b0c89', N'Cà rốt', N'cà rốt ', N'c6c0d810-5131-431c-9213-5f2dc17402cd', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F6e975bf8-2133-4372-b866-01a460106a71%2Fimage_2023-02-28_111441407.png3e3d5468-2554-4cb7-b39c-2e94e5edec37?alt=media&token=59559d33-260b-41ef-8818-537b3855122b', 40, CAST(N'2023-02-28T11:14:49.327' AS DateTime), NULL, 1, 5000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F6e975bf8-2133-4372-b866-01a460106a71%2Fimage_2023-02-28_111442727.png5315b8e1-8a7f-4e68-b4c9-8d1eb8626890?alt=media&token=3b732f13-abaa-4e69-9000-e8ac0dd85564', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'6e975bf8-2133-4372-b866-01a460106a71')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'010f9779-d4ec-42e5-a8e2-1b33e43c5bb8', N'Rau xà lách', N'rau xà lách', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F023406a1-6ff5-4964-acb6-5b360afcc2d0%2Fimage_2023-02-28_172925980.png819a3ed5-cc11-4124-bbf0-b0dda2e6aaea?alt=media&token=b007e79d-da12-45bf-928f-6078602a2f9a', 14, CAST(N'2023-02-28T17:29:46.887' AS DateTime), NULL, 1, 5000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F023406a1-6ff5-4964-acb6-5b360afcc2d0%2Fimage_2023-02-28_172927204.pngeb159bd1-5811-447d-b0dc-f72530b7eab2?alt=media&token=86cb098c-d9e2-47ca-9651-f32f02a82a3f', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'023406a1-6ff5-4964-acb6-5b360afcc2d0')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'a3749cd1-c1c4-4963-9093-24223925dfbd', N'Bột nếp 200g', N'bột nếp', N'05c16c64-1994-4ac9-bb79-d5be4fcca460', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F5accc7aa-9e44-4027-82bf-8ddc0dbbbcc9%2Fimage_2023-02-28_172127190.pngec51220d-dc02-4dd0-ae6c-1fb14df8767f?alt=media&token=ccb6b127-02cb-4bb2-8744-91377e92ff11', 200, CAST(N'2023-02-28T17:21:52.537' AS DateTime), NULL, 1, 5000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F5accc7aa-9e44-4027-82bf-8ddc0dbbbcc9%2Fimage_2023-02-28_172128572.pngd2480a12-33f6-40a0-8a60-bc52f4f75fb2?alt=media&token=418ace50-9546-4471-910b-32f5b9c7ead2', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'5accc7aa-9e44-4027-82bf-8ddc0dbbbcc9')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'e854b733-7973-4218-8c10-247a5ad37917', N'Rau mầm', N'rau mầm', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fb9dc18e3-32f2-4ff7-a780-3f1c1484f741%2Fimage_2023-03-03_232804909.pnge47058d6-0a80-4caf-80a1-72855d899387?alt=media&token=f9e2cec9-4e60-4821-a415-475214f6d7a5', 12, CAST(N'2023-03-03T23:28:21.880' AS DateTime), NULL, 1, 12000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fb9dc18e3-32f2-4ff7-a780-3f1c1484f741%2Fimage_2023-03-03_232806242.png3a003f2d-a920-4d2d-8aed-4fd7d6e4b7f6?alt=media&token=33d88868-bf5a-4d8c-8b59-648c5d7a9969', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'b9dc18e3-32f2-4ff7-a780-3f1c1484f741')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'6fe9c672-769a-4ac0-beee-24d727cf1dad', N'Tiêu hạt', N'tiêu hạt', N'05c16c64-1994-4ac9-bb79-d5be4fcca460', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fa25c51ec-010e-4188-bf54-0bc556681786%2Fimage_2023-02-27_212906251.pngba4b75d1-8fad-48cb-977a-5921422c45a8?alt=media&token=f19294b3-2f6d-463d-8f51-4dd02c741eb0', 64, CAST(N'2023-02-27T21:30:03.687' AS DateTime), NULL, 1, 11000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fa25c51ec-010e-4188-bf54-0bc556681786%2Fimage_2023-02-27_212907908.pngf1cd0460-7536-4b80-bee1-02c255aecc58?alt=media&token=4dc63e07-15e9-4f70-8b26-2968cf5b3cd2', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'a25c51ec-010e-4188-bf54-0bc556681786')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'bb9059a1-db26-4927-be81-26490badb557', N'Khổ qua', N'khổ qua (mướp đắng)', N'1082dc1a-ac02-452a-b847-25bf5b98630c', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fbac72fcf-0495-4345-aab6-c3943f89a942%2Fimage_2023-02-27_174418558.pnge22211f1-1eea-48bf-8423-6295cd67d96b?alt=media&token=ba50c9b7-d909-4952-98a3-aef33909afe9', 34, CAST(N'2023-02-27T17:45:10.753' AS DateTime), NULL, 1, 6000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fbac72fcf-0495-4345-aab6-c3943f89a942%2Fimage_2023-02-27_174419998.png2ad2594c-0b80-4de0-bcd0-3967c97084f4?alt=media&token=0c428698-f5c5-4761-bce9-acf28d265f3c', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'bac72fcf-0495-4345-aab6-c3943f89a942')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'bad3defe-7312-457c-8d10-280ab40dcb59', N'Đậu phộng rang', N'đậu phộng rang sẵn', N'05c16c64-1994-4ac9-bb79-d5be4fcca460', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F9cd4716c-72cb-4a66-afea-69d87925b180%2Fimage_2023-02-28_120942970.png1536a1b1-27ad-4492-8771-7d3bb8b941fe?alt=media&token=817f0229-2a90-4edf-9341-fd808004e4b1', 600, CAST(N'2023-02-28T12:10:09.103' AS DateTime), NULL, 1, 14000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F9cd4716c-72cb-4a66-afea-69d87925b180%2Fimage_2023-02-28_120944318.png129be954-a4bc-402d-9067-09bd06d8d7a6?alt=media&token=7cc918ac-9d25-44ad-9577-318992c1e9d9', N'773201fd-4d88-40be-825e-cebb5b01c18e', N'9cd4716c-72cb-4a66-afea-69d87925b180')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'40a72e41-1a20-4838-8c82-2cdd71b327a4', N'Bắp cải tím', N'bắp cải tím', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F52f8969a-0c92-46bb-8fd2-dd9f48e8f458%2Fimage_2023-02-28_173334210.pnga824deed-00e0-4143-8133-5d4d739ff9c8?alt=media&token=097cc299-49be-4c52-bd11-db8148d8ded5', 32, CAST(N'2023-02-28T17:33:53.440' AS DateTime), NULL, 1, 20000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F52f8969a-0c92-46bb-8fd2-dd9f48e8f458%2Fimage_2023-02-28_173335498.png639d284f-4d90-4998-a454-5b95cbf17da2?alt=media&token=0aef8118-9720-4446-938e-49b73f2ac98c', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'52f8969a-0c92-46bb-8fd2-dd9f48e8f458')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'58c24dde-229d-494b-9010-31e907871cbf', N'Chanh tươi', N'chanh tươi', N'1082dc1a-ac02-452a-b847-25bf5b98630c', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fcf2d7ece-241e-45a4-beef-809a8209f230%2Fimage_2023-02-27_180613838.png35186860-0bae-4570-8f90-ace77b238b6e?alt=media&token=d10a952b-f066-4dfc-ab1e-e4c2171fa181', 29, CAST(N'2023-02-27T18:06:45.900' AS DateTime), NULL, 1, 5000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fcf2d7ece-241e-45a4-beef-809a8209f230%2Fimage_2023-02-27_180615791.png13ce04ca-12f5-4ea6-b035-7b42f9a8cac7?alt=media&token=4b2983a9-fcb3-4d2f-859a-3b2c98a25fcd', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'cf2d7ece-241e-45a4-beef-809a8209f230')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'ecd765ca-cb2b-4d51-95a5-32f9e6f60267', N'Tôm khô', N'tôm khô dùng để chế biến nấu canh...', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F69731fc6-c9f4-421c-adaa-81664908e348%2Fimage_2023-02-28_104202416.pnga63797e1-a5de-4ebf-9534-e05637930c7c?alt=media&token=a81010d1-fc87-49ef-9d37-9cf352c52efb', 160, CAST(N'2023-02-28T10:42:27.527' AS DateTime), NULL, 1, 5000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F69731fc6-c9f4-421c-adaa-81664908e348%2Fimage_2023-02-28_104204025.pngbca0d15e-89a0-4f3a-bffe-d1b965ddf6f4?alt=media&token=e912b3fe-6081-4a34-b84c-52b7cc68108f', N'773201fd-4d88-40be-825e-cebb5b01c18e', N'69731fc6-c9f4-421c-adaa-81664908e348')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'074ab218-ed61-46ce-87bb-3814308f54ff', N'Mực tươi 400g', N'mực tươi', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F752dca6d-de61-4d0e-9609-40f7d388e405%2Fimage_2023-02-28_170239214.pnge2803614-1a78-4599-9ef5-186a461e18c9?alt=media&token=bf22607a-a41f-4278-a5fe-7324eefae3e6', 120, CAST(N'2023-02-28T17:02:53.823' AS DateTime), NULL, 1, 120000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F752dca6d-de61-4d0e-9609-40f7d388e405%2Fimage_2023-02-28_170240517.pnga4d67535-8aad-4a8a-a9a6-8daf31b5273c?alt=media&token=5a740f17-c3be-4740-9233-51eb3791f680', N'4f0cdf40-0444-4293-96e8-e35839d10037', N'752dca6d-de61-4d0e-9609-40f7d388e405')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'b6bb08b4-eec9-4429-b526-38a4deb84a5e', N'Phomai sợi Mozzarella', N'gói phô mai sợi Mozzarella', N'05c16c64-1994-4ac9-bb79-d5be4fcca460', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F4bfa177e-803d-42e9-bd81-31127c17d433%2Fimage_2023-02-27_215121387.pngc7adeb3f-bb84-4dd4-bb46-b4ce661b93b2?alt=media&token=0ac2f16a-4504-4986-8a2c-298de48d7011', 400, CAST(N'2023-02-27T21:52:01.577' AS DateTime), NULL, 1, 35000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F4bfa177e-803d-42e9-bd81-31127c17d433%2Fimage_2023-02-27_215122879.pngbe7ca4fc-e7d7-487b-8c93-c68cac1f76df?alt=media&token=2c78fbb7-9a7b-457a-a681-33126a4d5b75', N'773201fd-4d88-40be-825e-cebb5b01c18e', N'4bfa177e-803d-42e9-bd81-31127c17d433')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'c26d1c3c-0f0e-4ef4-bd73-3a771c22fb26', N'Bạc hà', N'rau bạc hà', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F0e017727-3819-430a-b41b-ec305ab562b1%2Fimage_2023-02-27_164914826.png8a04f402-1a68-4303-827c-3834c54080aa?alt=media&token=f252bc03-a880-4d49-8105-788cf5f5f7a3', 431, CAST(N'2023-02-27T16:49:29.060' AS DateTime), NULL, 1, 5000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F0e017727-3819-430a-b41b-ec305ab562b1%2Fimage_2023-02-27_164916185.pngc7eaf265-1219-405f-8937-0b0f2fb20536?alt=media&token=a23ad69d-3946-444f-bf77-41b89dc32697', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'0e017727-3819-430a-b41b-ec305ab562b1')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'8edefcac-345b-4527-babb-3deacb0c0ca9', N'Mộc nhĩ', N'mộc nhĩ', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F646fd845-f6c1-4c0e-9760-2c88fa8b9bca%2Fimage_2023-02-28_111315551.pngdafcb583-bd15-4942-afe6-64c2865c18e3?alt=media&token=8aed4177-4c51-426f-a692-6963f490c8d1', 12, CAST(N'2023-02-28T11:13:54.863' AS DateTime), NULL, 1, 3000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F646fd845-f6c1-4c0e-9760-2c88fa8b9bca%2Fimage_2023-02-28_111316881.pngafb5258c-a889-4f78-b63c-e85cedbf5a8b?alt=media&token=5db7f573-61c6-44dc-b357-80a03648b4a0', N'773201fd-4d88-40be-825e-cebb5b01c18e', N'646fd845-f6c1-4c0e-9760-2c88fa8b9bca')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'255fab51-a6e3-4a17-a0b3-3fc53344a18c', N'Tương cà', N'tương cà Cholimex', N'dabc7178-e761-4748-b8f7-1ffc98dfb0c4', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F096e0fa1-21da-441d-8690-22d83557a188%2Fimage_2023-02-27_214945563.pngd6d2647a-ccb7-47d8-9a02-95dd662d4059?alt=media&token=49805503-2f73-4bb7-905f-606a725143dc', 60, CAST(N'2023-02-27T21:50:15.083' AS DateTime), NULL, 1, 15000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F096e0fa1-21da-441d-8690-22d83557a188%2Fimage_2023-02-27_214947178.png164a1571-8e3f-4e54-aa7a-8321dd99d521?alt=media&token=2e7cfeb6-2c53-45c8-b523-d5c8dec5b087', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'096e0fa1-21da-441d-8690-22d83557a188')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'522db896-696b-4260-9968-4aa7f2b02a9b', N'Bột ngọt', N'bột ngọt', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F00397918-c2e9-4db4-b186-d9af60d12952%2Fimage_2023-02-27_162253442.png2b52436b-e51d-47b2-b813-46a68c6fd97c?alt=media&token=43d42889-ad28-4199-8fc9-45f2a0c20b75', 138, CAST(N'2023-02-27T16:23:11.753' AS DateTime), NULL, 1, 8000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F00397918-c2e9-4db4-b186-d9af60d12952%2Fimage_2023-02-27_162255016.png904e10c6-7b4b-4aa2-adf8-8f9a1d29fe55?alt=media&token=c2e73762-956b-42d3-b055-957fa22597bc', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'00397918-c2e9-4db4-b186-d9af60d12952')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'e010c0d8-8742-4fef-81f3-4db3ce029a6c', N'Mì gói', N'mì gói', N'05c16c64-1994-4ac9-bb79-d5be4fcca460', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fdb2e8743-e58e-4066-9844-481db1cdcbe0%2Fimage_2023-02-27_221242086.pngf76aa25c-43b1-4fda-a5b1-012dfbcef36a?alt=media&token=b7fdf131-7366-4564-a7bb-2cfd5e4c5fb1', 300, CAST(N'2023-02-27T22:14:57.007' AS DateTime), NULL, 1, 4000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fdb2e8743-e58e-4066-9844-481db1cdcbe0%2Fimage_2023-02-27_221243447.png7e03879d-f56d-46ec-9d1b-890147a20808?alt=media&token=ecba76d7-e1fc-43c1-9c01-7cc7ad9f7d37', N'773201fd-4d88-40be-825e-cebb5b01c18e', N'db2e8743-e58e-4066-9844-481db1cdcbe0')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'c2816d50-7bb3-43e4-8ae3-508cb23d9c5c', N'Táo', N'táo tây', N'1082dc1a-ac02-452a-b847-25bf5b98630c', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fb3661850-e5ab-414c-adbe-4920af075e4b%2Fimage_2023-02-28_173252773.png83fc3de2-6909-4e6f-b685-e63ec22025ef?alt=media&token=bfe5967e-6668-4347-bb40-f75b7fd94f3d', 60, CAST(N'2023-02-28T17:33:00.303' AS DateTime), NULL, 1, 7000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fb3661850-e5ab-414c-adbe-4920af075e4b%2Fimage_2023-02-28_173253957.png5a68602d-cdf3-4665-af14-c96640e16e35?alt=media&token=36bb55c6-2f99-488e-93c5-295852bf4feb', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'b3661850-e5ab-414c-adbe-4920af075e4b')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'5f241768-8ec9-45f6-a6f0-550704115928', N'Dưa leo', N'dưa leo, dưa chuột', N'1082dc1a-ac02-452a-b847-25bf5b98630c', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F2a773bcc-ceae-42cb-9e1b-5d0ec53578ad%2Fimage_2023-02-28_120857182.png41e09699-dc30-4900-8815-d246bb439ae4?alt=media&token=281bc7ad-001e-4b23-813f-8b2ab8055f45', 8, CAST(N'2023-02-28T12:09:12.387' AS DateTime), NULL, 1, 5000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F2a773bcc-ceae-42cb-9e1b-5d0ec53578ad%2Fimage_2023-02-28_120858813.pngd8f3f8ca-f35a-4ce9-8b30-812061a32dbb?alt=media&token=c92634e4-16e3-4d69-9ad2-6e13fa5c2459', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'2a773bcc-ceae-42cb-9e1b-5d0ec53578ad')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'ad6eece6-1983-46d2-bd70-566885da997c', N'Khoai mỡ trắng 800g', N'khoai mỡ trắng', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F0265e5f6-0b67-4427-b8bb-f275c2818888%2Fimage_2023-02-27_161713077.png18495b50-757c-4a62-999f-943351cc8dc5?alt=media&token=15cfbd7e-bfe1-4366-a685-4b2c9a43c6f3', 117, CAST(N'2023-02-27T16:17:39.937' AS DateTime), NULL, 1, 14000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F0265e5f6-0b67-4427-b8bb-f275c2818888%2Fimage_2023-02-27_161714557.pnge6b3bb80-c170-42a7-bab3-24466b01e768?alt=media&token=5f982001-3a4a-41c0-8ff6-c49d36437dbc', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'0265e5f6-0b67-4427-b8bb-f275c2818888')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'27a236dd-f9a4-4b61-be2c-5e598a51e3dd', N'Thịt ba chỉ 850g', N'thịt ba chỉ', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F54dfc906-b66f-4feb-b8f4-87170491bcb4%2Fimage_2023-02-27_175636250.pngdae06cbe-08c2-4793-86d7-97728bd94950?alt=media&token=71ae6938-86ca-4765-bce3-5b282d611bca', 4400, CAST(N'2023-02-27T17:58:52.030' AS DateTime), NULL, 1, 76000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F54dfc906-b66f-4feb-b8f4-87170491bcb4%2Fimage_2023-02-27_175637609.png06df8dc3-d230-40b6-b3a6-cac3faedffd4?alt=media&token=04ff25a4-0be5-49b9-ac08-7c7788b5ad06', N'4f0cdf40-0444-4293-96e8-e35839d10037', N'54dfc906-b66f-4feb-b8f4-87170491bcb4')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'7556d213-1244-4e5e-8adf-5eb10cf756a6', N'Sả', N'sả', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F6dc208be-6346-47bc-9ae8-621fd39774bd%2Fimage_2023-02-28_105752756.png7f17e8b9-20c3-492c-85dd-ae84fcbc0b06?alt=media&token=ea03e324-6982-4653-a147-0aa47016b419', 99, CAST(N'2023-02-28T10:58:10.333' AS DateTime), NULL, 1, 3000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F6dc208be-6346-47bc-9ae8-621fd39774bd%2Fimage_2023-02-28_105754210.png77379558-9eef-475b-b4ed-e2c095711074?alt=media&token=e0dd5e1d-8fbb-448c-b3de-8fe186239b4a', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'6dc208be-6346-47bc-9ae8-621fd39774bd')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'37c6a1b4-51f2-4b4f-b5e7-6637baeddd93', N'Trứng vịt', N'trứng vịt', N'1082dc1a-ac02-452a-b847-25bf5b98630c', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F4941bfca-b76f-4af9-b8df-21764ecec09e%2Fimage_2023-02-27_175943184.png4c0b4b7f-f879-44ba-bd40-a4cbff00bf72?alt=media&token=ca9f2c2e-dd05-447b-b12d-37ec4abac61d', 130, CAST(N'2023-02-27T17:59:48.377' AS DateTime), NULL, 1, 8000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F4941bfca-b76f-4af9-b8df-21764ecec09e%2Fimage_2023-02-27_175944623.png81dd4e48-f4fa-4465-a4fc-c7d22233ad09?alt=media&token=a33da2d6-6a6a-4ba5-8fc5-43df33674b8b', N'4f0cdf40-0444-4293-96e8-e35839d10037', N'4941bfca-b76f-4af9-b8df-21764ecec09e')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'cf6910e2-0edb-47e9-9ae4-709df36caaca', N'Nước dừa', N'nước cốt dừa đóng hộp', N'a2d09468-11ea-4db0-bb25-11b2b01d89a1', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fa3991ab1-425c-4d81-84e7-ae76ddd0ea4e%2Fimage_2023-02-27_180109343.png0e7179c6-cb4a-4f1e-8826-3cbba20c64d0?alt=media&token=12731a56-c7da-44f9-aa9f-855664d4c3cf', 150, CAST(N'2023-02-27T18:02:19.573' AS DateTime), NULL, 1, 30000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fa3991ab1-425c-4d81-84e7-ae76ddd0ea4e%2Fimage_2023-02-27_180110758.png99b0c32f-993b-4817-906a-2385cdb5c57f?alt=media&token=188eb29b-9639-4245-8451-9f7b52307a83', N'53418c10-fd90-42c8-951a-0e4430008078', N'a3991ab1-425c-4d81-84e7-ae76ddd0ea4e')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'c6bc74cc-c937-4486-b66d-716d177c91c0', N'300g thịt bằm', N'thịt heo bằm', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fd7e1a80e-1c3a-4789-8ec4-6101f57b7327%2Fimage_2023-02-27_214756806.pngeef099cb-41a0-454b-9101-f0d4e596b8e6?alt=media&token=28a09560-ed55-42ec-87df-53a99f6a9b70', 720, CAST(N'2023-02-27T21:49:16.040' AS DateTime), NULL, 1, 30000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fd7e1a80e-1c3a-4789-8ec4-6101f57b7327%2Fimage_2023-02-27_214758214.png2ca47c64-0910-4f50-89e8-c27003fedb1e?alt=media&token=d54d06b3-a005-4e1a-a190-ec42be635cad', N'4f0cdf40-0444-4293-96e8-e35839d10037', N'd7e1a80e-1c3a-4789-8ec4-6101f57b7327')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'a9a8b4c2-6890-4531-85d9-73285d73b13e', N'Hành lá', N'hành lá', N'1082dc1a-ac02-452a-b847-25bf5b98630c', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F8e0696f3-4356-4896-9c29-60af1af2a113%2Fimage_2023-02-27_162019279.png70bbc35e-b812-4e9d-bfe6-5e3a6cfb6ac3?alt=media&token=68833e3b-4b0a-404d-9751-505469b1a345', 30, CAST(N'2023-02-27T16:20:54.260' AS DateTime), NULL, 1, 5000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F8e0696f3-4356-4896-9c29-60af1af2a113%2Fimage_2023-02-27_162020600.png2fb3b1f6-1712-47e4-83bd-627ce6ebab7f?alt=media&token=6f7ba072-2ebe-4746-8ca6-ff0008074de0', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'8e0696f3-4356-4896-9c29-60af1af2a113')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'c3e02a1d-d09c-4fac-a0bd-73e79519ac5a', N'Nước sốt mè rang', N'nước sốt mè rang Kewpie', N'dabc7178-e761-4748-b8f7-1ffc98dfb0c4', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fa1552f78-6d2f-499e-b17d-41c6b4e0f264%2Fimage_2023-02-28_173616722.png8f4137e0-709e-428a-be36-a5bb17a88301?alt=media&token=8fa9bb14-ac40-48d9-963e-834193126046', 600, CAST(N'2023-02-28T17:36:37.097' AS DateTime), NULL, 1, 30000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fa1552f78-6d2f-499e-b17d-41c6b4e0f264%2Fimage_2023-02-28_173618043.pnga88b39b7-2654-4444-ab5d-d53e3b2d7c26?alt=media&token=0031c4de-c4c8-42f8-84e1-e26b34c9a515', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'a1552f78-6d2f-499e-b17d-41c6b4e0f264')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'8395a958-bca5-45a4-95d3-759fc69a7984', N'Sa tế', N'1 hũ sa tế Cholimex', N'a2d09468-11ea-4db0-bb25-11b2b01d89a1', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fbb13e940-8563-47bf-aa82-d0720275c5dd%2Fimage_2023-02-28_170318089.pngedc3e4ea-19a3-4072-812e-b79aa946538d?alt=media&token=0d927133-dd99-43d8-8534-4b3fc92d7551', 20, CAST(N'2023-02-28T17:03:39.590' AS DateTime), NULL, 1, 12000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fbb13e940-8563-47bf-aa82-d0720275c5dd%2Fimage_2023-02-28_170319378.png3d71b759-51c9-4d2f-9738-44ec1280382f?alt=media&token=1676983c-62e7-4d4d-8f2b-de647c4500dd', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'bb13e940-8563-47bf-aa82-d0720275c5dd')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'638e6ba5-9f01-488a-b5bb-7bc6666b9c79', N'Bông cải trắng', N'bông cải trắng', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F650d2ad4-87bb-4cfd-bf4c-4aac0c974948%2Fimage_2023-02-28_170431370.png43ba9531-b8a0-4b2d-b5bd-bdbdbbcd829b?alt=media&token=af3df26d-3f75-435b-b858-90f4619dc149', 25, CAST(N'2023-02-28T17:04:50.687' AS DateTime), NULL, 1, 20000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F650d2ad4-87bb-4cfd-bf4c-4aac0c974948%2Fimage_2023-02-28_170432658.png3876b9ca-af8e-412b-83a1-203bbd5d1204?alt=media&token=3160807d-91ef-4b67-9141-089af2ab297c', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'650d2ad4-87bb-4cfd-bf4c-4aac0c974948')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'3b265262-1da5-421c-b194-7c5ae7b20d58', N'Giá đỗ', N'giá đỗ', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F83b9f8f7-9715-43a2-847f-2b9f23ad568c%2Fimage_2023-02-27_164354341.png3849d849-a6ab-4f70-8be8-cda228da12e9?alt=media&token=af338f9f-4f40-4e34-8717-be1a4e6311cf', 8, CAST(N'2023-02-27T16:44:21.807' AS DateTime), NULL, 1, 10000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F83b9f8f7-9715-43a2-847f-2b9f23ad568c%2Fimage_2023-02-27_164356140.png904e9b97-ebb0-4781-bc5b-b0b64b21104c?alt=media&token=0d79bc87-66b8-4ec3-ac8b-c17513016e97', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'83b9f8f7-9715-43a2-847f-2b9f23ad568c')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'c1f79a50-cbaf-48d7-b9e6-829b4a33856d', N'Tôm tươi 350g', N'Tôm tươi', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Ffa32f657-2055-43a0-ab6c-7690f4175e7d%2Fimage_2023-02-27_161847115.png15d4e06e-f876-4445-aeac-3787ed8bcb82?alt=media&token=a6437576-4594-4fda-a505-87dc0ce403c5', 97, CAST(N'2023-02-27T16:19:31.493' AS DateTime), NULL, 1, 35000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Ffa32f657-2055-43a0-ab6c-7690f4175e7d%2Fimage_2023-02-27_161848484.png7944857c-f42f-488a-bf44-d9f0bb2b0eb9?alt=media&token=86874ae9-b0c6-42f5-ae8f-0283502f8250', N'4f0cdf40-0444-4293-96e8-e35839d10037', N'fa32f657-2055-43a0-ab6c-7690f4175e7d')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'0cd74bc7-cd82-4392-9316-83ce49052467', N'Trứng gà', N'trứng gà ', N'1082dc1a-ac02-452a-b847-25bf5b98630c', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F8957baea-1f1b-454c-affa-1f1cb02e9760%2Fimage_2023-02-27_174626700.pngf66f3598-4984-46e4-98ae-765ecc69f0b4?alt=media&token=b6ac1b37-d61a-4df1-ab42-569901991de2', 115, CAST(N'2023-02-27T17:46:56.287' AS DateTime), NULL, 1, 5000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F8957baea-1f1b-454c-affa-1f1cb02e9760%2Fimage_2023-02-27_174628232.pngc9fcb45a-de44-43f2-aa98-571c52d43be7?alt=media&token=746cf116-72ff-42bf-8d0e-3c1825b649cc', N'4f0cdf40-0444-4293-96e8-e35839d10037', N'8957baea-1f1b-454c-affa-1f1cb02e9760')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'94dfcfa2-9c9c-4d60-a0e5-860c1cd7477c', N'Bột nghệ', N'bột nghệ', N'05c16c64-1994-4ac9-bb79-d5be4fcca460', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F64397e85-7666-40d1-a6df-b3136f3fbe9d%2Fimage_2023-02-28_110242356.pngac87107c-cf2a-42c5-adc5-4c877e641f49?alt=media&token=cd002b62-12a0-493e-94d0-ec5ceace6941', 50, CAST(N'2023-02-28T11:03:07.097' AS DateTime), NULL, 1, 15000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F64397e85-7666-40d1-a6df-b3136f3fbe9d%2Fimage_2023-02-28_110244018.pngb6f6af92-54da-47a9-bb8b-7b48be67a08e?alt=media&token=7c279af2-79d6-492d-ad17-1a8cf3c4a4eb', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'64397e85-7666-40d1-a6df-b3136f3fbe9d')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'809d6874-93ff-4368-9966-894aa3a5fa6f', N'Me ngào', N'me ngào dùng để nấu canh', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F2dde99a2-51b3-4128-bd22-3fd83a61110b%2Fimage_2023-02-27_164746316.png6605421f-7294-4e61-986b-2f8dbed62c94?alt=media&token=4a61e42a-5719-4ed3-8ae1-6d0c07ffd050', 168, CAST(N'2023-02-27T16:48:26.077' AS DateTime), NULL, 1, 6000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F2dde99a2-51b3-4128-bd22-3fd83a61110b%2Fimage_2023-02-27_164747764.pngdf391a5e-6d55-4d0f-a09a-7b6112dea299?alt=media&token=a84a2126-1167-4cb0-bbd1-c76948aa88a9', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'2dde99a2-51b3-4128-bd22-3fd83a61110b')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'4cda4b95-9ff8-4c28-8717-89534a367f46', N'Muối', N'muối iot bịch', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fadf470d0-9e89-4925-b45b-04146dad3210%2Fimage_2023-02-27_170834478.png8cc48870-d213-4469-96c9-56c17483b839?alt=media&token=d8fe74bb-f221-40f5-9697-56d2e0411317', 300, CAST(N'2023-02-27T17:09:07.893' AS DateTime), NULL, 1, 15000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fadf470d0-9e89-4925-b45b-04146dad3210%2Fimage_2023-02-27_170835924.png838b0dc2-41ce-496d-ab70-008a2b4f0ac5?alt=media&token=c768a2e0-e6d7-433d-8622-4adff26b4d6c', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'adf470d0-9e89-4925-b45b-04146dad3210')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'ec96c999-7edb-49d6-9e80-8c3f2781c74c', N'Cà chua bi', N'cà chua bi', N'1082dc1a-ac02-452a-b847-25bf5b98630c', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F5df7a9f3-b61d-466a-8436-a8e2962d5354%2Fimage_2023-02-28_173144553.png09ed716f-b671-44e5-a274-4eeccaf0f737?alt=media&token=88e05df6-710b-4910-980d-3fe9b9ff6340', 31, CAST(N'2023-02-28T17:31:53.770' AS DateTime), NULL, 1, 5000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F5df7a9f3-b61d-466a-8436-a8e2962d5354%2Fimage_2023-02-28_173145817.png6833bee7-f93c-4acd-ad38-a0e248d284be?alt=media&token=6d5965fb-5f2f-4ae1-8f0e-e465ab0c9c9c', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'5df7a9f3-b61d-466a-8436-a8e2962d5354')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'2369a6f3-2502-4751-8d1c-8d35037a29d5', N'Vừng trắng', N'vừng trắng', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F9aef1e71-1b17-4d75-8f56-ca9bf511247f%2Fimage_2023-02-28_172256162.png45ed3cb9-ecd2-4368-9b10-89e4247b8a87?alt=media&token=67afbda6-7340-4884-af52-2dacf75b5bff', 570, CAST(N'2023-02-28T17:23:06.427' AS DateTime), NULL, 1, 12000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F9aef1e71-1b17-4d75-8f56-ca9bf511247f%2Fimage_2023-02-28_172257427.png2be63ed3-76c4-45b3-b986-c12832e35157?alt=media&token=b11d9593-4f9b-4771-85a7-c91f17cf155a', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'9aef1e71-1b17-4d75-8f56-ca9bf511247f')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'ae720851-a428-44b2-9861-97976734fb73', N'Nước mắm', N'nước mắm', N'dabc7178-e761-4748-b8f7-1ffc98dfb0c4', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F14e8e2a9-9cb3-470a-8970-a0909110d458%2Fimage_2023-02-27_162127095.png825a026b-0064-4393-a8ef-d39e445a793a?alt=media&token=45e9a367-e65e-4bdd-bd30-183d5a3798cf', 34, CAST(N'2023-02-27T16:21:47.087' AS DateTime), NULL, 1, 24000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F14e8e2a9-9cb3-470a-8970-a0909110d458%2Fimage_2023-02-27_162128955.png8f682c6d-1d32-4453-9ce8-c99bb59ba14f?alt=media&token=4714220d-c61e-4f4d-8fd3-3ec3aae6cdde', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'14e8e2a9-9cb3-470a-8970-a0909110d458')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'263bca92-3142-4acc-a901-98a3852e9d49', N'Bông bí', N'bông bí', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fbef703bf-4283-4b14-ba0c-ba00414be505%2Fimage_2023-03-03_233323782.pngf9e27476-d13b-46ef-a512-637a7b9c9288?alt=media&token=2787fda3-7346-4975-a45f-a78abb9a9877', 12, CAST(N'2023-03-03T23:33:40.507' AS DateTime), NULL, 1, 3000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fbef703bf-4283-4b14-ba0c-ba00414be505%2Fimage_2023-03-03_233325048.png5b249b93-3368-4bb2-ad8b-e6c68c64c268?alt=media&token=3e3a78c3-934e-40ee-aa61-bbd199047b95', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'bef703bf-4283-4b14-ba0c-ba00414be505')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'ffdc6c8a-0edb-41b7-bc10-a270c61d5fe3', N'Rau húng quế', N'rau húng quế', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F176d40d3-419c-4ff9-8757-f1d869b1e739%2Fimage_2023-02-28_171514171.pnga98b5f88-55d5-4b00-9b56-9caa3cd4902c?alt=media&token=d5251c48-362c-45e3-9a3c-ff78eff74962', 12, CAST(N'2023-02-28T17:15:23.733' AS DateTime), NULL, 1, 12000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F176d40d3-419c-4ff9-8757-f1d869b1e739%2Fimage_2023-02-28_171515427.png114f6bbf-7221-451b-b6a3-011da63c98f9?alt=media&token=75aebee6-304f-425e-b8e8-c429d70fbdad', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'176d40d3-419c-4ff9-8757-f1d869b1e739')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'54d42b35-48c5-4f28-9926-a742af5fd121', N'Cà pháo 200g', N'cà pháo ', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F2628e22b-04f8-4527-ab41-8199dc1ffc0c%2Fimage_2023-02-28_171125856.png3d14052a-24ff-4b9d-9c18-8c63c81459dd?alt=media&token=9e118c14-a0c9-4bcc-a5e5-c7f3a160783c', 35, CAST(N'2023-02-28T17:11:36.483' AS DateTime), NULL, 1, 20000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F2628e22b-04f8-4527-ab41-8199dc1ffc0c%2Fimage_2023-02-28_171127152.pngca8ac7b7-4c6e-415e-b97d-ae49660f22b3?alt=media&token=b037b8ae-a9b7-4cb2-9846-e01fbda30c70', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'2628e22b-04f8-4527-ab41-8199dc1ffc0c')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'3fe75b51-8810-4a5d-9e81-ae4ed9269168', N'Dứa ', N'dứa (thơm)', N'1082dc1a-ac02-452a-b847-25bf5b98630c', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fbf6e3538-0597-426f-a167-beb55ba0a1f8%2Fimage_2023-02-27_164117518.pngb756b58b-7068-4608-a44b-ec3c8a6f4f34?alt=media&token=b6b34f18-f2ae-4ece-bf86-8fb3bd8cb4cd', 50, CAST(N'2023-02-27T16:41:23.717' AS DateTime), NULL, 1, 20000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fbf6e3538-0597-426f-a167-beb55ba0a1f8%2Fimage_2023-02-27_164119004.pngeca4c2e4-0f25-400d-a2a1-643595930f4d?alt=media&token=8e3c4a03-bd36-4eaa-b5fc-bd5cdd515198', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'bf6e3538-0597-426f-a167-beb55ba0a1f8')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'ecd1b683-f42f-4ad6-99ce-aef848ba37f5', N'Đường trắng', N'đường mía, đường trắng', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F2e36bcaf-c789-4333-a729-aa6093fe376d%2Fimage_2023-02-27_164653986.png01e40b4b-36d6-412e-8157-6f0d36b26ae6?alt=media&token=3aa665a4-632f-4064-83ad-acdace7ea772', 386, CAST(N'2023-02-27T16:47:04.800' AS DateTime), NULL, 1, 15000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F2e36bcaf-c789-4333-a729-aa6093fe376d%2Fimage_2023-02-27_164655311.png93f9bcb8-cd8c-4a7a-81c8-2b7a9248091a?alt=media&token=53be32e8-e6dd-4bc5-ae4d-52fb71fa4972', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'2e36bcaf-c789-4333-a729-aa6093fe376d')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'9732d359-0d8d-4898-9908-af0791e57ebf', N'Ớt tươi', N'ớt tươi', N'1082dc1a-ac02-452a-b847-25bf5b98630c', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F17112b84-d84e-4833-b1d5-facd1f16ef29%2Fimage_2023-02-27_164442398.png2a29beb1-899d-4cbb-aa05-d0ee79ab7be0?alt=media&token=7e61ed0d-e585-4d80-abec-202963900bf3', 40, CAST(N'2023-02-27T16:45:09.723' AS DateTime), NULL, 1, 20000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F17112b84-d84e-4833-b1d5-facd1f16ef29%2Fimage_2023-02-27_164443850.png6d5b460c-f813-48c6-8859-2cc69be5da69?alt=media&token=92568977-4326-493e-99e1-28942b56ce84', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'17112b84-d84e-4833-b1d5-facd1f16ef29')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'f75c899e-c992-41b2-8104-b215d3e2b8ff', N'Cá lóc', N'cá lóc', N'8e2c242e-a16e-4d5f-8ce9-27485c2a801a', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Faf026d6d-edd0-4070-96a2-c1879988bb77%2Fimage_2023-02-27_163856691.png8b8855ae-f01c-4d9d-9cd2-eaa4943e96ff?alt=media&token=c9bdf8b1-c4e4-4b15-9471-3cd98865c8c3', 118, CAST(N'2023-02-27T16:39:08.917' AS DateTime), NULL, 1, 100000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Faf026d6d-edd0-4070-96a2-c1879988bb77%2Fimage_2023-02-27_163858120.png717db953-6ea4-4e2d-829b-dac09bb88d51?alt=media&token=c4fb55db-82cf-419b-a51f-bb31a9e44556', N'4f0cdf40-0444-4293-96e8-e35839d10037', N'af026d6d-edd0-4070-96a2-c1879988bb77')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'7fd02c45-4b22-4ddf-9dae-b258f9e88fbb', N'Đậu bắp', N'đậu bắp', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fd2105d14-6227-41c5-bfa3-024e4fb3f100%2Fimage_2023-02-27_164150197.pngb12ae86a-dfae-4bdc-9089-00539d7318bf?alt=media&token=a6992d33-6a97-49ad-9b00-e3a77a8b0075', 33, CAST(N'2023-02-27T16:42:22.500' AS DateTime), NULL, 1, 34998.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fd2105d14-6227-41c5-bfa3-024e4fb3f100%2Fimage_2023-02-27_164151525.png763da764-1c34-4954-a89d-9043950776ea?alt=media&token=24bf9fa7-0905-4955-b3d7-7e6f0d3fb63f', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'd2105d14-6227-41c5-bfa3-024e4fb3f100')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'a1e7eb96-9154-45b5-8ff6-c07fdecc6ad0', N'Gói gia vị chung', N'Gói gia vị chung gồm nước mắm, đường, bột ngọt, muối, tiêu', N'05c16c64-1994-4ac9-bb79-d5be4fcca460', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F7221e6fa-af48-4110-87a9-658350968349%2FHomnayangiSpices.pngcd9448df-21fd-450d-b6dc-1990fab2cc09?alt=media&token=a7e85fcf-cb24-4c44-977f-4a05537aa363', 100, CAST(N'2023-02-28T12:41:51.050' AS DateTime), NULL, 1, 8000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F7221e6fa-af48-4110-87a9-658350968349%2FHomnayangiSpices.png2b7e4925-26eb-4855-80f8-101e7a800833?alt=media&token=ff9fa22d-9c62-4605-b5d7-0d6182c9655f', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'7221e6fa-af48-4110-87a9-658350968349')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'e490edf8-9cbe-4fdc-aec8-c5d82ba3441c', N'Sườn thăn 700g', N'sườn thăn heo', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fce317a54-0963-4aa0-82aa-942aae324894%2Fimage_2023-02-27_170540479.pngbca3834d-a1ba-4132-9f5c-d07510914438?alt=media&token=fc3e370f-491f-4e91-a2ab-ef19e471f138', 2100, CAST(N'2023-02-27T17:06:31.833' AS DateTime), NULL, 1, 67000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fce317a54-0963-4aa0-82aa-942aae324894%2Fimage_2023-02-27_170541958.png5178a855-0f82-49a0-b16a-1ff25ea4d708?alt=media&token=bcce80eb-b4a1-4a2d-ad1d-87f8a72cc5cc', N'4f0cdf40-0444-4293-96e8-e35839d10037', N'ce317a54-0963-4aa0-82aa-942aae324894')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'bd1cbc70-be6b-43e9-a318-c7a31d7d0780', N'Cà chua', N'Cà chua', N'1082dc1a-ac02-452a-b847-25bf5b98630c', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F38cff2cf-4adc-4b70-95a0-1b7466291f3e%2Fimage_2023-02-27_164000409.png76341be3-6060-47c5-b247-3e91adf957bc?alt=media&token=4c6d5678-6653-431d-8c19-2e3f70b268b7', 18, CAST(N'2023-02-27T16:40:04.497' AS DateTime), NULL, 1, 45000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F38cff2cf-4adc-4b70-95a0-1b7466291f3e%2Fimage_2023-02-27_164001753.pngabf9ef25-798d-47c0-b600-d2ba76bd14be?alt=media&token=2cba0885-aef4-42b9-ac5d-64496e4ded05', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'38cff2cf-4adc-4b70-95a0-1b7466291f3e')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'a3e011ef-8e0c-493a-a81a-ccf2590bcda4', N'Hành củ', N'hành củ', N'1082dc1a-ac02-452a-b847-25bf5b98630c', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fa0fc3674-f122-48f9-a0f2-5e2cfa0fc739%2Fimage_2023-02-27_174743162.png6413a266-285d-4b49-a07b-169bf7dc0d9a?alt=media&token=e49c7bc1-e309-49dd-867e-d121c94f07ee', 40, CAST(N'2023-02-27T17:47:52.783' AS DateTime), NULL, 1, 2000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fa0fc3674-f122-48f9-a0f2-5e2cfa0fc739%2Fimage_2023-02-27_174744564.png01505a17-bd8f-4dc1-8171-c26e80c3a18f?alt=media&token=c1c11b0c-1730-4dfd-a28a-84ef0dae679f', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'a0fc3674-f122-48f9-a0f2-5e2cfa0fc739')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'057a5e7f-93d6-4d78-bc96-d23a6e617c9e', N'Miến dong', N'miến dong', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F897ca478-6453-4161-b4e2-078c89d52476%2Fimage_2023-02-28_111225243.pngfd0ee46c-be52-4117-b347-016aeaa916f7?alt=media&token=042e4a69-1958-4112-aeca-0472b78c610b', 12, CAST(N'2023-02-28T11:12:44.827' AS DateTime), NULL, 1, 3000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F897ca478-6453-4161-b4e2-078c89d52476%2Fimage_2023-02-28_111226844.png54992b2c-92ed-41c2-b3b8-1008587f99bc?alt=media&token=4eba1074-b75c-4c7c-83af-1fc9c2153a41', N'773201fd-4d88-40be-825e-cebb5b01c18e', N'897ca478-6453-4161-b4e2-078c89d52476')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'a934d9af-ebe9-4da8-81cf-d28cbc475788', N'Dầu màu điều', N'dầu màu điều', N'dabc7178-e761-4748-b8f7-1ffc98dfb0c4', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F50f08afb-70fd-4eaa-b000-1465bbb17b91%2Fimage_2023-02-27_170749884.png8976f732-5ec5-4e45-98d1-3bef5431e853?alt=media&token=bf658829-776b-4b90-bceb-3b43a6f0a898', 500, CAST(N'2023-02-27T17:08:03.360' AS DateTime), NULL, 1, 50000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F50f08afb-70fd-4eaa-b000-1465bbb17b91%2Fimage_2023-02-27_170751318.pngb1aa1faf-035a-4fad-b164-c116d4361cb4?alt=media&token=dacee9e5-6d3c-4986-b470-7b7fddf9d452', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'50f08afb-70fd-4eaa-b000-1465bbb17b91')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'7bf71a82-0788-44eb-9663-d6e1ccd70362', N'Mì Ý 250g', N'mì ý', N'05c16c64-1994-4ac9-bb79-d5be4fcca460', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F22d7432d-8cd1-4a3c-8177-dc88be6aee03%2Fimage_2023-02-27_214053708.png30d2d15a-246e-4da4-9c3c-0b98722a682a?alt=media&token=4f7a5452-d6cb-4039-a8e9-b2537c7fc692', 260, CAST(N'2023-02-27T21:43:48.387' AS DateTime), NULL, 1, 30000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F22d7432d-8cd1-4a3c-8177-dc88be6aee03%2Fimage_2023-02-27_214055149.png296e0886-0bc9-43b6-9859-831b0c9b3df8?alt=media&token=7218ce0a-1626-4615-8045-48e49ed939ae', N'773201fd-4d88-40be-825e-cebb5b01c18e', N'22d7432d-8cd1-4a3c-8177-dc88be6aee03')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'33c45fd5-1afe-4412-aa0b-d73a50a103f2', N'Mật mía', N'mật mía', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fc07ae065-9551-4ba4-b32d-49f2ef59a1f9%2Fimage_2023-02-28_172340994.png19e79f9d-ca0c-4fdf-9c1c-07e20d4fb962?alt=media&token=a0946ae7-009f-4e81-af65-e24b4191fa4d', 200, CAST(N'2023-02-28T17:23:58.147' AS DateTime), NULL, 1, 5000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fc07ae065-9551-4ba4-b32d-49f2ef59a1f9%2Fimage_2023-02-28_172342195.png837f11c7-03bc-4550-8fc3-43622c78cbee?alt=media&token=ebca1e48-7890-4260-a512-4e445e65ac8a', N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'c07ae065-9551-4ba4-b32d-49f2ef59a1f9')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'267f376f-2799-439d-ab5f-e1f022409f9c', N'Bí đỏ 800g', N'bí đỏ nhỏ', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F206deb0b-8572-48f8-b9eb-ae5f253b94d5%2Fimage_2023-02-27_163003150.png8d0dcb24-0903-4a53-9af4-18ec3eeca1a0?alt=media&token=186731e0-b6ca-4d8e-8c12-763fb0f9ff9f', 25, CAST(N'2023-02-27T16:30:27.707' AS DateTime), NULL, 1, 10000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F206deb0b-8572-48f8-b9eb-ae5f253b94d5%2Fimage_2023-02-27_163004695.png8222b4e1-18d5-44dd-a211-285d920462dc?alt=media&token=8a3ad995-3708-448c-a0fc-7e66ad680c5a', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'206deb0b-8572-48f8-b9eb-ae5f253b94d5')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'b73f8bc9-d36e-4afd-bcff-e29ba7c9c8d8', N'Thịt heo 400g', N'thịt heo', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fcd215762-e8d7-48d8-a3c7-79af56c85e90%2Fimage_2023-02-27_225735719.png6ee3a5f5-b893-46c6-81d2-659247bc60e7?alt=media&token=3894d8d3-68d7-443f-878f-8fee63b25cf2', 800, CAST(N'2023-02-27T22:58:07.367' AS DateTime), NULL, 1, 44000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fcd215762-e8d7-48d8-a3c7-79af56c85e90%2Fimage_2023-02-27_225737102.png1c7f0188-ae68-4ddf-940b-8b05900d716d?alt=media&token=9ca9a26e-ee61-42ea-885b-4b969facf195', N'4f0cdf40-0444-4293-96e8-e35839d10037', N'cd215762-e8d7-48d8-a3c7-79af56c85e90')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'75341320-4a8f-4c75-b32a-e39168f338a7', N'Tai heo 300g', N'tai heo', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F65aabb4d-5e53-4862-a508-7310e6a789d9%2Fimage_2023-02-28_120753824.png6b2aad52-d814-4f7e-b0a6-883a0f809602?alt=media&token=7f903ac7-8940-4871-9771-dbcd51575444', 720, CAST(N'2023-02-28T12:08:21.903' AS DateTime), NULL, 1, 70000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F65aabb4d-5e53-4862-a508-7310e6a789d9%2Fimage_2023-02-28_120755232.png05b0487e-4284-4eeb-a94d-329e13e78db9?alt=media&token=79fdf6a0-2952-4a70-a0f0-4c4d5a05bf6b', N'4f0cdf40-0444-4293-96e8-e35839d10037', N'65aabb4d-5e53-4862-a508-7310e6a789d9')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'3c7e15c7-47dd-40a4-893d-e5a398a91815', N'Rau muống', N'rau muống', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F81bfa16d-f3bf-4286-9510-de63cbf5a050%2Fimage_2023-02-27_173659929.png7e86242a-4e8f-4603-83c0-4deee26daf19?alt=media&token=f5465f8b-c6a3-4a7d-a1b4-5bdedaeb8e5b', 18, CAST(N'2023-02-27T17:37:23.120' AS DateTime), NULL, 1, 25000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F81bfa16d-f3bf-4286-9510-de63cbf5a050%2Fimage_2023-02-27_173701394.png86c693e0-90ed-4500-a510-d2ec8f89c51c?alt=media&token=b1011113-861a-4c47-8551-eaba3db61797', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'81bfa16d-f3bf-4286-9510-de63cbf5a050')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'bc988c66-ee2f-4ba7-9423-e9bacf31b006', N'Mắm ruốc', N'mắm ruốc Trí Hải cao cấp', N'dabc7178-e761-4748-b8f7-1ffc98dfb0c4', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fa2f00ebe-2863-47eb-9dfc-d4527a451d89%2Fimage_2023-02-28_160905488.png872bd9ec-993a-4202-92e1-7060a6553cc9?alt=media&token=9a538543-cb58-47fd-aafb-af3e13326200', 120, CAST(N'2023-02-28T16:10:23.083' AS DateTime), NULL, 1, 38000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2Fa2f00ebe-2863-47eb-9dfc-d4527a451d89%2Fimage_2023-02-28_160908058.pngd3ac0837-2b73-40f2-98e7-fcfaa01e2cf3?alt=media&token=f2a8f264-6b4c-48ad-b067-52a5ac2e2ff2', N'53418c10-fd90-42c8-951a-0e4430008078', N'a2f00ebe-2863-47eb-9dfc-d4527a451d89')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'9bb40236-d65c-40fb-8f1d-ef5162201716', N'Rau diếp cá', N'rau diếp cá', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F8a627227-a29d-4479-947f-57f60dfccb0f%2Fimage_2023-02-28_171245115.png60a52594-9ecc-4623-8605-e81d94fc359f?alt=media&token=1f89ddf4-fd25-47bf-afa1-bd4ebcb9a314', 12, CAST(N'2023-02-28T17:12:57.783' AS DateTime), NULL, 1, 12000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F8a627227-a29d-4479-947f-57f60dfccb0f%2Fimage_2023-02-28_171246315.pnge1930125-23fd-40fc-be1a-64c48891092e?alt=media&token=37ed95f4-d45d-4139-aba6-266896963ccc', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'8a627227-a29d-4479-947f-57f60dfccb0f')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'6dde0f4b-ca9c-4553-b810-f09e65ae2b12', N'Thịt ba chỉ 200g', N'thịt ba chỉ', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F41c56521-cc7e-45b1-acf5-e95ad4bd949d%2Fimage_2023-02-28_104507522.pngf0d92bdb-7fcc-4f5c-b2b1-e0df9ce5688a?alt=media&token=cc246532-8cb0-4235-a046-d9c4ecbf8320', 1000, CAST(N'2023-02-28T10:45:17.680' AS DateTime), NULL, 1, 30000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F41c56521-cc7e-45b1-acf5-e95ad4bd949d%2Fimage_2023-02-28_104508754.pngb16e8db6-227e-4e27-9f07-224dbc0c1f1b?alt=media&token=55a1804b-e46e-4a1e-8ddb-9a4613d1309d', N'4f0cdf40-0444-4293-96e8-e35839d10037', N'41c56521-cc7e-45b1-acf5-e95ad4bd949d')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'df5e2547-57e8-4d0e-9f8a-f3500a138367', N'Rau kinh giới', N'rau kinh giới', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F4f6f4ca4-9310-4dd8-ab90-ebda2815b421%2Fimage_2023-02-28_171436187.png05094bc4-dc09-4a8d-bb7a-ee8b282f79a3?alt=media&token=ba811bdb-31ed-4106-a067-87354267bdfb', 12, CAST(N'2023-02-28T17:14:53.533' AS DateTime), NULL, 1, 12000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F4f6f4ca4-9310-4dd8-ab90-ebda2815b421%2Fimage_2023-02-28_171437380.png800ac1d0-9cea-4859-a6c0-0cfad502a399?alt=media&token=ff7e1189-796b-4f1b-8cfc-b9f35b8cd666', N'1177aed5-130b-4180-b57b-68cd72bafb17', N'4f6f4ca4-9310-4dd8-ab90-ebda2815b421')
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'c2aae894-a18c-4673-95a6-f44cad60de67', N'Thịt đùi gà 500g', N'thịt đùi gà', N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', 0, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F1d44e36a-f3bd-45e0-a6d7-d0c7054e460f%2Fimage_2023-02-28_105325180.png7747bd79-ded4-408e-a06c-cf8e9b0b553b?alt=media&token=d36e5b41-f0f4-4f97-8355-e886acb385c4', 600, CAST(N'2023-02-28T10:53:54.563' AS DateTime), NULL, 1, 40000.0000, N';https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ingredients%2F1d44e36a-f3bd-45e0-a6d7-d0c7054e460f%2Fimage_2023-02-28_105326661.png06c77ca6-d500-4214-ac8a-a3494042f82b?alt=media&token=ec8b6fa2-b33a-4694-9a1e-cf50dfcedfb8', N'4f0cdf40-0444-4293-96e8-e35839d10037', N'1d44e36a-f3bd-45e0-a6d7-d0c7054e460f')
GO
INSERT [dbo].[Order] ([orderId], [orderDate], [shippedDate], [shippedAddress], [totalPrice], [orderStatus], [customerId], [isCooked], [voucherId], [paymentMethod], [paypalUrl]) VALUES (N'35920610-6bef-47f3-a5ab-50c6e2f5be16', CAST(N'2023-04-08T11:00:09.243' AS DateTime), NULL, N'customer3, 0965421541, , địa chỉ nhà trong hẻm, Quận 4, Phường 03, ', 440000.0000, 5, N'89fcfb56-be89-442d-9b99-8f498023a5cc', 0, NULL, 0, N'')
INSERT [dbo].[Order] ([orderId], [orderDate], [shippedDate], [shippedAddress], [totalPrice], [orderStatus], [customerId], [isCooked], [voucherId], [paymentMethod], [paypalUrl]) VALUES (N'dd49d72e-84ba-4b03-9e7f-6af555f50851', CAST(N'2023-04-08T11:02:37.057' AS DateTime), CAST(N'2023-04-12T09:09:28.000' AS DateTime), N'customer3, 0965421541, , địa chỉ nhà trong hẻm, Quận 5, Phường 02, ', 50000.0000, 2, N'89fcfb56-be89-442d-9b99-8f498023a5cc', 1, NULL, 1, N'https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=EC-6256075790593271Y')
GO
INSERT [dbo].[OrderDetail] ([orderDetailId], [orderId], [ingredientId], [quantity], [recipeId], [price]) VALUES (N'50c00318-d840-4f75-801a-72ad57d7e4cb', N'dd49d72e-84ba-4b03-9e7f-6af555f50851', N'00000000-0000-0000-0000-000000000000', 1, N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', 50000.0000)
INSERT [dbo].[OrderDetail] ([orderDetailId], [orderId], [ingredientId], [quantity], [recipeId], [price]) VALUES (N'e4085b05-6520-4af9-96e6-7a30893af471', N'35920610-6bef-47f3-a5ab-50c6e2f5be16', N'd9cf8215-0305-4780-8b85-058dff1c74cc', 2, NULL, 20000.0000)
INSERT [dbo].[OrderDetail] ([orderDetailId], [orderId], [ingredientId], [quantity], [recipeId], [price]) VALUES (N'61edb761-597e-4d04-b7b6-fd8d9c23301b', N'35920610-6bef-47f3-a5ab-50c6e2f5be16', N'00000000-0000-0000-0000-000000000000', 2, N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', 200000.0000)
GO
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', N'Cách làm mì ý sốt bò bằm ngon tại nhà 🥘🥘', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa9531be6-8a7a-43fd-9fe9-13ba99a65dc9%2Fimage_2023-02-27_215304133.pngb7cff8cd-7d1d-47af-b5ff-84a8e59ae23e?alt=media&token=7fdf9aa6-faf6-4f65-99d1-07fd572fec21', 280000.0000, 80000.0000, 1, 2, 260, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'5b579874-46a8-4895-bad2-1f33d4cb006a', N'Cách nấu canh bí đỏ với tôm thanh ngọt, tăng cường sức khỏe 😍😍😍', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5b579874-46a8-4895-bad2-1f33d4cb006a%2Fimage_2023-02-27_163207623.pngc4084603-19db-4d3e-badb-4b9a416130f4?alt=media&token=4c544174-9d5e-4d50-9c08-23b48fc180b5', 100000.0000, 79999.0000, 1, 2, 340, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'ac41da30-8998-4a1f-b2e4-291d0b24dcf4', N'Cách làm bông bí xào tỏi xanh mướt như ngoài hàng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fac41da30-8998-4a1f-b2e4-291d0b24dcf4%2Fimage_2023-03-03_233436189.pngcb39a877-2687-471b-912f-2b49c455fcf0?alt=media&token=00c67bdf-860e-40fe-92a8-50707699f75c', 52000.0000, 20000.0000, 1, 2, 320, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'b5603c80-4003-41f9-a7a5-3076de517599', N'Cách làm mì xào rau muống siêu hấp dẫn, ăn giòn sần sật', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb5603c80-4003-41f9-a7a5-3076de517599%2Fimage_2023-02-27_220834895.pngc579bfe3-a2d6-47fb-b4a2-c00f44d1685e?alt=media&token=f9070137-12ee-40af-ad2a-6916697ff9cd', 90000.0000, 40000.0000, 1, 4, 300, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', N'Cách làm món sườn kho thơm lạ miệng, ngon quên sầu', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F3ef7c4e0-b1ac-4506-9556-3541afd421fe%2Fimage_2023-02-28_155658710.png7554bd97-20ff-4807-accb-39871d10816a?alt=media&token=a15bffbe-5ed3-43b3-a712-674762c19106', 200000.0000, 70000.0000, 1, 4, 430, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', N'Cách làm sườn xào chua ngọt trong 30 phút (cách mới)', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fcc8da8e0-0c9d-4893-8ea1-387c0519a0f3%2Fimage_2023-02-27_170936652.png394c2734-1f4b-4798-83a7-30d46a2928f4?alt=media&token=c632a627-2afc-4941-b33e-215dd558aedb', 195000.0000, 80000.0000, 1, 3, 342, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', N'Cách làm thịt kho mắm ruốc sả ớt thơm ngon đậm đà', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F95b39e92-a35e-4dad-aa9c-4aed3f258b6f%2Fimage_2023-02-28_160422598.png2e06cf2d-c0bd-47a4-b2d7-1e47f6efa89a?alt=media&token=3ffed372-d387-411a-8c26-c70651ed166d', 200000.0000, 70000.0000, 1, 4, 421, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', N'Cách làm salad rau mầm, ăn ngon không đắng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fae2bf83d-2b03-4a44-ac29-4c41246cc355%2Fimage_2023-03-03_232608815.png6ded854b-93cb-4498-a4bb-e01ab43ec198?alt=media&token=1429f1eb-d515-47df-abbd-bba9f0cbefc2', 80000.0000, 30000.0000, 1, 2, 190, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', N'Cách làm nộm tai heo với công thức pha nước trộn nộm chuẩn', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa1515f35-3041-4c6d-9dbb-5dfc002e3894%2Fimage_2023-02-28_124336275.png321d2377-5234-4ef5-917b-0d094eaa1d81?alt=media&token=67e45ddc-88ea-433d-8f45-56908d13ad40', 190000.0000, 70000.0000, 1, 4, 340, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'5de76825-4f4e-469d-b94e-5e457294b045', N'Cách nấu canh khoai mỡ trắng với tôm tươi ngon bổ 😊', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F5de76825-4f4e-469d-b94e-5e457294b045%2Fimage_2023-02-27_162358233.png8d401f60-8477-4961-a67d-5b8174d5f923?alt=media&token=e48424c8-884a-402e-90d3-b24fa32c5d4d', 85000.0000, 90000.0000, 1, 2, 350, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'Cách làm nem rán miền Bắc giòn rụm, vàng đều, không bị ỉu', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9%2Fimage_2023-02-28_111644581.pngf41b9372-3237-49b6-8878-549a094ee6f1?alt=media&token=e094a57a-0fd7-4b75-a5a6-deb81dc81341', 178000.0000, 60000.0000, 2, 2, 560, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'f578682a-d641-4a0b-aa54-6a81eaf7be08', N'Cách làm thịt kho tiêu đơn giản ngon đậm đà', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Ff578682a-d641-4a0b-aa54-6a81eaf7be08%2Fimage_2023-02-27_225457525.png0bcfca79-ffba-4393-a0b0-8dc28cc30593?alt=media&token=ce3d9940-8080-431b-98a8-f3c03417bc4b', 130000.0000, 70000.0000, 1, 2, 450, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'956f6fc0-72e1-4204-852b-73cff12eae37', N'Cách làm bánh ngào mật mía Nghệ An', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F956f6fc0-72e1-4204-852b-73cff12eae37%2Fimage_2023-02-28_172432113.png250851e7-28b1-477f-9663-034e436b290c?alt=media&token=7a42f319-2612-4503-abe9-842bf629c5a3', 30000.0000, 10000.0000, 1, 2, 360, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'b9f39ecc-c689-4910-ac82-816e20994156', N'Cách làm cá lóc kho tiêu, nghệ và lá gừng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fb9f39ecc-c689-4910-ac82-816e20994156%2Fimage_2023-02-27_212437842.png8464529a-8f47-4c32-b489-cf74fc5b99f9?alt=media&token=289ed741-6bef-4cf6-b997-444c0ed1620f', 180000.0000, 69999.0000, 1, 2, 550, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'047935c2-9a60-4b0e-865a-89c5e1a680e5', N'Cách làm món cà pháo mắm tôm ngon, ăn là ghiền', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F047935c2-9a60-4b0e-865a-89c5e1a680e5%2Fimage_2023-02-28_171556392.png51311fd1-cf08-48bf-b7b0-b0857f09b971?alt=media&token=d0179eba-23e9-40e4-8c6a-47078bb79d79', 120000.0000, 50000.0000, 1, 2, 240, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', N'Cách làm mực xào sa tế siêu ngon mà đơn giản', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7%2Fimage_2023-02-28_170532772.png8ff2b7e0-f648-42b1-a587-d25ade0485b1?alt=media&token=16a1a0d3-6ebe-4f49-a06b-04d4aa8c80ea', 200000.0000, 50000.0000, 1, 2, 421, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', N'Cách làm salad rau trộn sốt mè rang cho người giảm cân', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234%2Fimage_2023-02-28_173749843.png52ed5779-daf6-494a-913c-4fe817dc8d0b?alt=media&token=bbaf1262-a0ab-4713-b62a-870a05d13360', 80000.0000, 40000.0000, 1, 2, 312, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', N'Cách làm kho quẹt tôm khô chấm rau củ luộc ngon bá cháy', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f%2Fimage_2023-02-28_104036431.pngf3c9441f-52a5-4540-9dfe-ea8a46fa560e?alt=media&token=b1867ae8-75b7-45c1-8846-a07a060e9398', 120000.0000, 50000.0000, 2, 4, 450, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'a8ab27eb-4401-4b4b-93b9-bda34397621f', N'Cách nấu thịt kho tàu ngon bá cháy 😤', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fa8ab27eb-4401-4b4b-93b9-bda34397621f%2Fimage_2023-02-27_180748893.png22240233-c47a-49ec-a87e-a9a739903619?alt=media&token=58908dea-18e9-4c4b-9ddd-99acc1a19e81', 250000.0000, 99999.0000, 2, 4, 253, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'e3f6c554-29c7-460d-a692-d23d15dae638', N'Cách làm gà xào sả ớt ngon chuẩn vị', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fe3f6c554-29c7-460d-a692-d23d15dae638%2Fimage_2023-02-28_110346984.pngee39ba8b-f8a0-4985-91f9-ab191d3503ee?alt=media&token=275087bf-4963-4178-ac3a-95d8a8216bf5', 120000.0000, 40000.0000, 1, 2, 175, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', N'Cách nấu canh chua cá lóc miền Tây Nam Bộ 🍴', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F0458adf1-68ce-44b1-89b1-f069b44a3ee2%2Fimage_2023-02-27_165412388.png3115e5dd-4342-448a-94d5-200214246041?alt=media&token=0298552b-30c0-4f12-b5b2-052e9faf6009', 200000.0000, 100000.0000, 1, 2, 109, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'346dfff7-d030-408f-b521-f4b15b139bd3', N'Cách làm khổ qua xào trứng không bị đắng 😉', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2F346dfff7-d030-408f-b521-f4b15b139bd3%2Fimage_2023-02-27_174804334.png80da20c5-ab7c-4d7f-8f32-8df02445e04f?alt=media&token=98a34829-03d4-4347-88be-8b66c0c2cf4f', 45000.0000, 30000.0000, 1, 2, 391, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'dc7234fa-21fd-45bc-9612-f4fb5e2f4f8b', N'Cách làm rau muống xào tỏi xanh giòn thơm phức', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/blogs%2Fdc7234fa-21fd-45bc-9612-f4fb5e2f4f8b%2Fimage_2023-02-27_173736886.pngf71ed7f6-3fa0-40ec-8a60-39a5cd816501?alt=media&token=8f44c238-3659-499f-8923-db2e971585f2', 70000.0000, 40000.0000, 1, 2, 260, 1)
GO
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'tỏi, một hoặc nhiều tùy khẩu vị', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', N'b6bb08b4-eec9-4429-b526-38a4deb84a5e', N'phô mai sợi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', N'255fab51-a6e3-4a17-a0b3-3fc53344a18c', N'tương cà', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', N'c6bc74cc-c937-4486-b66d-716d177c91c0', N'300g thịt bò bằm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', N'4cda4b95-9ff8-4c28-8717-89534a367f46', N'muối tinh', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', N'ecd1b683-f42f-4ad6-99ce-aef848ba37f5', N'đường trắng', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', N'bd1cbc70-be6b-43e9-a318-c7a31d7d0780', N'3 trái cà chua', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', N'a3e011ef-8e0c-493a-a81a-ccf2590bcda4', N'vài củ hành', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a9531be6-8a7a-43fd-9fe9-13ba99a65dc9', N'7bf71a82-0788-44eb-9663-d6e1ccd70362', N'250g mì ý', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'5b579874-46a8-4895-bad2-1f33d4cb006a', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'3 củ tỏi tùy sở thích', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'5b579874-46a8-4895-bad2-1f33d4cb006a', N'522db896-696b-4260-9968-4aa7f2b02a9b', N'bột ngọt hoặc bột canh', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'5b579874-46a8-4895-bad2-1f33d4cb006a', N'a9a8b4c2-6890-4531-85d9-73285d73b13e', N'200g hành lá', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'5b579874-46a8-4895-bad2-1f33d4cb006a', N'c1f79a50-cbaf-48d7-b9e6-829b4a33856d', N'300g tôm tươi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'5b579874-46a8-4895-bad2-1f33d4cb006a', N'ae720851-a428-44b2-9861-97976734fb73', N'1 ít nước mắm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'5b579874-46a8-4895-bad2-1f33d4cb006a', N'267f376f-2799-439d-ab5f-e1f022409f9c', N'1 quả bí đỏ nhỏ 700-800g', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'ac41da30-8998-4a1f-b2e4-291d0b24dcf4', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'2 củ tỏi', 2, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'ac41da30-8998-4a1f-b2e4-291d0b24dcf4', N'4cda4b95-9ff8-4c28-8717-89534a367f46', N'một ít muối hạt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'ac41da30-8998-4a1f-b2e4-291d0b24dcf4', N'263bca92-3142-4acc-a901-98a3852e9d49', N'400g bông bí', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'ac41da30-8998-4a1f-b2e4-291d0b24dcf4', N'a1e7eb96-9154-45b5-8ff6-c07fdecc6ad0', N'bột ngọt, tiêu', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'b5603c80-4003-41f9-a7a5-3076de517599', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N' 2 3 tép tỏi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'b5603c80-4003-41f9-a7a5-3076de517599', N'522db896-696b-4260-9968-4aa7f2b02a9b', N'gia vị bột ngọt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'b5603c80-4003-41f9-a7a5-3076de517599', N'e010c0d8-8742-4fef-81f3-4db3ce029a6c', N'2 3 vắt mì gói', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'b5603c80-4003-41f9-a7a5-3076de517599', N'c6bc74cc-c937-4486-b66d-716d177c91c0', N'300g thịt bò bằm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'b5603c80-4003-41f9-a7a5-3076de517599', N'3c7e15c7-47dd-40a4-893d-e5a398a91815', N'1 bó rau muống', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'5 6 tép tỏi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', N'6fe9c672-769a-4ac0-beee-24d727cf1dad', N'tiêu hạt hoặc tiêu xay', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', N'a9a8b4c2-6890-4531-85d9-73285d73b13e', N'vài cọng hành lá', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', N'3fe75b51-8810-4a5d-9e81-ae4ed9269168', N'1 nửa trái dứa', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', N'9732d359-0d8d-4898-9908-af0791e57ebf', N'thêm ớt nếu thích ăn cay', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', N'a1e7eb96-9154-45b5-8ff6-c07fdecc6ad0', N'gia vị khác', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', N'e490edf8-9cbe-4fdc-aec8-c5d82ba3441c', N'sườn non hoặc sườn thăn', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'3ef7c4e0-b1ac-4506-9556-3541afd421fe', N'a3e011ef-8e0c-493a-a81a-ccf2590bcda4', N'2 củ hành khô', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', N'522db896-696b-4260-9968-4aa7f2b02a9b', N'200g bột ngọt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', N'4cda4b95-9ff8-4c28-8717-89534a367f46', N'muối hạt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', N'ae720851-a428-44b2-9861-97976734fb73', N'một ít nước mắm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', N'ecd1b683-f42f-4ad6-99ce-aef848ba37f5', N'200g đường trắng', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', N'9732d359-0d8d-4898-9908-af0791e57ebf', N'1 - 2 quả ớt', 2, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', N'e490edf8-9cbe-4fdc-aec8-c5d82ba3441c', N'700g sườn thăn', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'cc8da8e0-0c9d-4893-8ea1-387c0519a0f3', N'a934d9af-ebe9-4da8-81cf-d28cbc475788', N'50g dầu màu điều', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'3 4 tép tỏi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', N'27a236dd-f9a4-4b61-be2c-5e598a51e3dd', N'thịt ba chỉ hoặc thịt ba rọi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', N'7556d213-1244-4e5e-8adf-5eb10cf756a6', N'5 nhánh sả', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', N'9732d359-0d8d-4898-9908-af0791e57ebf', N'1 2 quả ớt tươi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', N'a1e7eb96-9154-45b5-8ff6-c07fdecc6ad0', N'nước mắm, đường, bột ngọt, tiêu', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', N'a3e011ef-8e0c-493a-a81a-ccf2590bcda4', N'3 4 củ hành', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'95b39e92-a35e-4dad-aa9c-4aed3f258b6f', N'bc988c66-ee2f-4ba7-9423-e9bacf31b006', N'4 muỗng mắm ruốc', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', N'e854b733-7973-4218-8c10-247a5ad37917', N'150g rau mầm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', N'5f241768-8ec9-45f6-a6f0-550704115928', N'2 quả dưa leo', 2, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', N'c3e02a1d-d09c-4fac-a0bd-73e79519ac5a', N'nước sốt mè rang', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'ae2bf83d-2b03-4a44-ac29-4c41246cc355', N'ec96c999-7edb-49d6-9e80-8c3f2781c74c', N'10 trái cà chua bi', 10, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'1 củ tỏi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', N'ea417c29-dd3b-4ae7-a1e8-14938e3b0c89', N'1 củ cà rốt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', N'bad3defe-7312-457c-8d10-280ab40dcb59', N'một ít đậu phộng rang', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', N'58c24dde-229d-494b-9010-31e907871cbf', N'2, 3 quả chanh tươi', 2, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', N'5f241768-8ec9-45f6-a6f0-550704115928', N'3 quả dưa leo', 3, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', N'3b265262-1da5-421c-b194-7c5ae7b20d58', N'50g giá đỗ', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', N'9732d359-0d8d-4898-9908-af0791e57ebf', N'vài quả ớt tươi', 3, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', N'a1e7eb96-9154-45b5-8ff6-c07fdecc6ad0', N'nước mắm, đường, bột ngọt, muối, tiêu', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a1515f35-3041-4c6d-9dbb-5dfc002e3894', N'75341320-4a8f-4c75-b32a-e39168f338a7', N'300g tai heo', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'5de76825-4f4e-469d-b94e-5e457294b045', N'522db896-696b-4260-9968-4aa7f2b02a9b', N'bột ngọt hoặc bột nêm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'5de76825-4f4e-469d-b94e-5e457294b045', N'ad6eece6-1983-46d2-bd70-566885da997c', N'1 củ', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'5de76825-4f4e-469d-b94e-5e457294b045', N'a9a8b4c2-6890-4531-85d9-73285d73b13e', N'100g hành lá', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'5de76825-4f4e-469d-b94e-5e457294b045', N'c1f79a50-cbaf-48d7-b9e6-829b4a33856d', N'350g tôm tươi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'5de76825-4f4e-469d-b94e-5e457294b045', N'ae720851-a428-44b2-9861-97976734fb73', N'một ít nước mắm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'một ít tỏi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'ea417c29-dd3b-4ae7-a1e8-14938e3b0c89', N'1 củ cà rốt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'6fe9c672-769a-4ac0-beee-24d727cf1dad', N'một ít tiêu ', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'8edefcac-345b-4527-babb-3deacb0c0ca9', N'10g mộc nhĩ', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'522db896-696b-4260-9968-4aa7f2b02a9b', N'một ít bột ngọt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'c6bc74cc-c937-4486-b66d-716d177c91c0', N'thịt heo xay', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'a9a8b4c2-6890-4531-85d9-73285d73b13e', N'10g hành lá', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'3b265262-1da5-421c-b194-7c5ae7b20d58', N'20g giá đỗ', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'0cd74bc7-cd82-4392-9316-83ce49052467', N'2 quả trứng gà', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'4cda4b95-9ff8-4c28-8717-89534a367f46', N'một ít muối', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'ae720851-a428-44b2-9861-97976734fb73', N'một ít nước mắm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'9732d359-0d8d-4898-9908-af0791e57ebf', N'vài quả ớt tươi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'6c707aaf-cfcc-4e16-8fe3-6420cf8e3cc9', N'057a5e7f-93d6-4d78-bc96-d23a6e617c9e', N'5g miến dong', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'f578682a-d641-4a0b-aa54-6a81eaf7be08', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'4 tép tỏi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'f578682a-d641-4a0b-aa54-6a81eaf7be08', N'6fe9c672-769a-4ac0-beee-24d727cf1dad', N'hạt tiêu đen', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'f578682a-d641-4a0b-aa54-6a81eaf7be08', N'522db896-696b-4260-9968-4aa7f2b02a9b', N'', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'f578682a-d641-4a0b-aa54-6a81eaf7be08', N'4cda4b95-9ff8-4c28-8717-89534a367f46', N'muối ăn', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'f578682a-d641-4a0b-aa54-6a81eaf7be08', N'ae720851-a428-44b2-9861-97976734fb73', N'nước mắm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'f578682a-d641-4a0b-aa54-6a81eaf7be08', N'ecd1b683-f42f-4ad6-99ce-aef848ba37f5', N'', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'f578682a-d641-4a0b-aa54-6a81eaf7be08', N'a3e011ef-8e0c-493a-a81a-ccf2590bcda4', N'2 củ hành khô', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'f578682a-d641-4a0b-aa54-6a81eaf7be08', N'b73f8bc9-d36e-4afd-bcff-e29ba7c9c8d8', N'400g thịt heo ', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'956f6fc0-72e1-4204-852b-73cff12eae37', N'a3749cd1-c1c4-4963-9093-24223925dfbd', N'200g bột nếp', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'956f6fc0-72e1-4204-852b-73cff12eae37', N'4cda4b95-9ff8-4c28-8717-89534a367f46', N'muối xay hoặc muối hạt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'956f6fc0-72e1-4204-852b-73cff12eae37', N'2369a6f3-2502-4751-8d1c-8d35037a29d5', N'vừng trắng hoặc đen', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'956f6fc0-72e1-4204-852b-73cff12eae37', N'33c45fd5-1afe-4412-aa0b-d73a50a103f2', N'50g mật mía', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'b9f39ecc-c689-4910-ac82-816e20994156', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'2 tép tỏi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'b9f39ecc-c689-4910-ac82-816e20994156', N'6fe9c672-769a-4ac0-beee-24d727cf1dad', N'50g tiêu hạt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'b9f39ecc-c689-4910-ac82-816e20994156', N'522db896-696b-4260-9968-4aa7f2b02a9b', N'bột ngọt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'b9f39ecc-c689-4910-ac82-816e20994156', N'ae720851-a428-44b2-9861-97976734fb73', N'một ít nước mắm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'b9f39ecc-c689-4910-ac82-816e20994156', N'ecd1b683-f42f-4ad6-99ce-aef848ba37f5', N'đường trắng', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'b9f39ecc-c689-4910-ac82-816e20994156', N'9732d359-0d8d-4898-9908-af0791e57ebf', N'1-2 trái ớt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'b9f39ecc-c689-4910-ac82-816e20994156', N'f75c899e-c992-41b2-8104-b215d3e2b8ff', N'700g cá lóc', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'b9f39ecc-c689-4910-ac82-816e20994156', N'a3e011ef-8e0c-493a-a81a-ccf2590bcda4', N'4 củ hành khô', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'047935c2-9a60-4b0e-865a-89c5e1a680e5', N'dc0a73f3-be27-49ea-bc0b-0022c5404972', N'40ml mắm tôm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'047935c2-9a60-4b0e-865a-89c5e1a680e5', N'522db896-696b-4260-9968-4aa7f2b02a9b', N'một ít bột ngọt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'047935c2-9a60-4b0e-865a-89c5e1a680e5', N'5f241768-8ec9-45f6-a6f0-550704115928', N'vài quả dưa leo', 2, NULL)
GO
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'047935c2-9a60-4b0e-865a-89c5e1a680e5', N'ffdc6c8a-0edb-41b7-bc10-a270c61d5fe3', N'rau húng quế', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'047935c2-9a60-4b0e-865a-89c5e1a680e5', N'54d42b35-48c5-4f28-9926-a742af5fd121', N'200g cà pháo', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'047935c2-9a60-4b0e-865a-89c5e1a680e5', N'ecd1b683-f42f-4ad6-99ce-aef848ba37f5', N'một ít đường trắng', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'047935c2-9a60-4b0e-865a-89c5e1a680e5', N'9732d359-0d8d-4898-9908-af0791e57ebf', N'2 3 quả ớt', 3, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'047935c2-9a60-4b0e-865a-89c5e1a680e5', N'9bb40236-d65c-40fb-8f1d-ef5162201716', N'một ít rau diếp cá', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'047935c2-9a60-4b0e-865a-89c5e1a680e5', N'df5e2547-57e8-4d0e-9f8a-f3500a138367', N'một ít rau kinh giới', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'tỏi ', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', N'074ab218-ed61-46ce-87bb-3814308f54ff', N'400g mực tươi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', N'a9a8b4c2-6890-4531-85d9-73285d73b13e', N'vài cọng hành lá', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', N'8395a958-bca5-45a4-95d3-759fc69a7984', N'1 hũ sa tế', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', N'638e6ba5-9f01-488a-b5bb-7bc6666b9c79', N'120g bông cải trắng', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', N'9732d359-0d8d-4898-9908-af0791e57ebf', N'vài quả ớt tươi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'9629aa0d-6e6b-4f84-b9c5-a834e6ff23b7', N'a1e7eb96-9154-45b5-8ff6-c07fdecc6ad0', N'nước mắm, đường, bột ngọt, tiêu', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', N'010f9779-d4ec-42e5-a8e2-1b33e43c5bb8', N'100g rau xà lách', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', N'40a72e41-1a20-4838-8c82-2cdd71b327a4', N'1/2 bắp', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', N'c2816d50-7bb3-43e4-8ae3-508cb23d9c5c', N'1 quả', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', N'5f241768-8ec9-45f6-a6f0-550704115928', N'1 quả', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', N'c3e02a1d-d09c-4fac-a0bd-73e79519ac5a', N'nước sốt mè rang', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'd1dcb721-ae9f-48f0-a0f3-a8c2fe47f234', N'ec96c999-7edb-49d6-9e80-8c3f2781c74c', N'7-8 quả', 8, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'1 củ tỏi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', N'6fe9c672-769a-4ac0-beee-24d727cf1dad', N'', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', N'ecd765ca-cb2b-4d51-95a5-32f9e6f60267', N'50g tôm khô', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', N'a9a8b4c2-6890-4531-85d9-73285d73b13e', N'vài cọng hành lá', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', N'ae720851-a428-44b2-9861-97976734fb73', N'một ít nước mắm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', N'ecd1b683-f42f-4ad6-99ce-aef848ba37f5', N'đường trắng', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', N'9732d359-0d8d-4898-9908-af0791e57ebf', N'5 - 6 quả', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', N'a3e011ef-8e0c-493a-a81a-ccf2590bcda4', N'1 củ hành', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'13cb48ed-52eb-4c2c-aa9e-b07a7c9c930f', N'6dde0f4b-ca9c-4553-b810-f09e65ae2b12', N'200g thịt ba chỉ', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a8ab27eb-4401-4b4b-93b9-bda34397621f', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'2 - 3 tép tỏi', 3, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a8ab27eb-4401-4b4b-93b9-bda34397621f', N'58c24dde-229d-494b-9010-31e907871cbf', N'1/2 quả chanh tươi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a8ab27eb-4401-4b4b-93b9-bda34397621f', N'27a236dd-f9a4-4b61-be2c-5e598a51e3dd', N'850g thịt ba chỉ', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a8ab27eb-4401-4b4b-93b9-bda34397621f', N'37c6a1b4-51f2-4b4f-b5e7-6637baeddd93', N'5 quả trứng vịt', 5, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a8ab27eb-4401-4b4b-93b9-bda34397621f', N'cf6910e2-0edb-47e9-9ae4-709df36caaca', N'900ml nước cốt dừa', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a8ab27eb-4401-4b4b-93b9-bda34397621f', N'4cda4b95-9ff8-4c28-8717-89534a367f46', N'200g muối', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a8ab27eb-4401-4b4b-93b9-bda34397621f', N'ae720851-a428-44b2-9861-97976734fb73', N'một ít nước mắm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a8ab27eb-4401-4b4b-93b9-bda34397621f', N'9732d359-0d8d-4898-9908-af0791e57ebf', N'1 - 2 trái ớt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'a8ab27eb-4401-4b4b-93b9-bda34397621f', N'a3e011ef-8e0c-493a-a81a-ccf2590bcda4', N'4 - 5 củ hành', 5, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'e3f6c554-29c7-460d-a692-d23d15dae638', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'một vài củ tỏi', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'e3f6c554-29c7-460d-a692-d23d15dae638', N'6fe9c672-769a-4ac0-beee-24d727cf1dad', N'tiêu hạt hoặc tiêu xay', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'e3f6c554-29c7-460d-a692-d23d15dae638', N'522db896-696b-4260-9968-4aa7f2b02a9b', N'một ít bột ngọt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'e3f6c554-29c7-460d-a692-d23d15dae638', N'7556d213-1244-4e5e-8adf-5eb10cf756a6', N'200g sả nhánh', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'e3f6c554-29c7-460d-a692-d23d15dae638', N'94dfcfa2-9c9c-4d60-a0e5-860c1cd7477c', N'một chút bột nghệ', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'e3f6c554-29c7-460d-a692-d23d15dae638', N'ae720851-a428-44b2-9861-97976734fb73', N'một ít nước mắm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'e3f6c554-29c7-460d-a692-d23d15dae638', N'a3e011ef-8e0c-493a-a81a-ccf2590bcda4', N'vài củ hành', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'e3f6c554-29c7-460d-a692-d23d15dae638', N'c2aae894-a18c-4673-95a6-f44cad60de67', N'500g thịt đùi gà', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', N'c26d1c3c-0f0e-4ef4-bd73-3a771c22fb26', N'2 nhánh', 2, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', N'809d6874-93ff-4368-9966-894aa3a5fa6f', N'nửa chén nhỏ', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', N'ae720851-a428-44b2-9861-97976734fb73', N'một ít nước mắm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', N'3fe75b51-8810-4a5d-9e81-ae4ed9269168', N'1 quả', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', N'ecd1b683-f42f-4ad6-99ce-aef848ba37f5', N'một ít đường', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', N'9732d359-0d8d-4898-9908-af0791e57ebf', N'2 trái', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', N'f75c899e-c992-41b2-8104-b215d3e2b8ff', N'1 kg cá lóc', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', N'7fd02c45-4b22-4ddf-9dae-b258f9e88fbb', N'100g', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'0458adf1-68ce-44b1-89b1-f069b44a3ee2', N'bd1cbc70-be6b-43e9-a318-c7a31d7d0780', N'2 quả', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'346dfff7-d030-408f-b521-f4b15b139bd3', N'bb9059a1-db26-4927-be81-26490badb557', N'2 quả khổ qua', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'346dfff7-d030-408f-b521-f4b15b139bd3', N'522db896-696b-4260-9968-4aa7f2b02a9b', N'một ít bột ngọt', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'346dfff7-d030-408f-b521-f4b15b139bd3', N'a9a8b4c2-6890-4531-85d9-73285d73b13e', N'vài cành hành lá', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'346dfff7-d030-408f-b521-f4b15b139bd3', N'0cd74bc7-cd82-4392-9316-83ce49052467', N'3 quả trứng gà', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'346dfff7-d030-408f-b521-f4b15b139bd3', N'ae720851-a428-44b2-9861-97976734fb73', N'một ít nước mắm', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'346dfff7-d030-408f-b521-f4b15b139bd3', N'a3e011ef-8e0c-493a-a81a-ccf2590bcda4', N'2 củ hành khô', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'dc7234fa-21fd-45bc-9612-f4fb5e2f4f8b', N'd9cf8215-0305-4780-8b85-058dff1c74cc', N'1 đến 2 củ', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'dc7234fa-21fd-45bc-9612-f4fb5e2f4f8b', N'4cda4b95-9ff8-4c28-8717-89534a367f46', N'một ít muối iot', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'dc7234fa-21fd-45bc-9612-f4fb5e2f4f8b', N'ecd1b683-f42f-4ad6-99ce-aef848ba37f5', N'một ít đường', 1, NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity], [status]) VALUES (N'dc7234fa-21fd-45bc-9612-f4fb5e2f4f8b', N'3c7e15c7-47dd-40a4-893d-e5a398a91815', N'1 bó rau muống', 1, NULL)
GO
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'93a460cc-f092-447f-aaf5-0564cd0ffadc', N'Giảm cân', N'Giảm cân', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'433b682e-3651-4f4b-b688-0eaf344c51bd')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'3bc469f2-df84-491f-a64a-119400614291', N'Bún', N'Bún', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'3b8ab3d0-08df-47f1-bf9f-ecf895fd0daa')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'a7766c2a-8faf-48f9-bdec-1dfaba22ecf3', N'Miền Bắc', N'Miền Bắc', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'14465782-6f37-4a8f-8d5e-7a0bea7f3b4c')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'6e5375f0-b603-49da-ace5-262dcace546f', N'Trẻ em', N'Trẻ em', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'63db7259-a32a-4a5e-beb8-985f3db5f63f')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'85276dc8-058d-4938-b38a-281e7276252c', N'Thịt heo', N'Thịt heo', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'3fa85f64-5717-4562-b3fc-2c963f66afa1')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'76bc7165-d0be-4d32-b819-434dfaeba2ad', N'Món kho', N'Món kho', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'8033733e-b4d5-4dfb-8316-e66481022cf2')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'ba268cf4-c7ce-436d-9184-43ccdf5bbab3', N'Tết', N'tết', CAST(N'2023-03-01T16:12:47.773' AS DateTime), 1, N'13e8bc02-41c4-44ab-bf5f-28fa6c729c41')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'c368e82d-dda4-4b39-bc19-43ef148981e0', N'Miền Trung', N'Miền Trung', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'14465782-6f37-4a8f-8d5e-7a0bea7f3b4c')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'8c6d0b63-74d9-4931-9c2a-4b7c8b54ed0e', N'Bữa sáng', N'Bữa sáng', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'e5c95ca2-d0fb-4002-8c0f-da4b39312730')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'df9bbf2f-b1c4-4c3d-864b-4c96c37a0b39', N'Thực đơn 1', N'Thực đơn 1', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'2d80def2-0135-4373-a4e6-2b15fc0166b6')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'7a13a207-91b5-4e2f-8d6f-4face2528a57', N'Thực đơn 2 ', N'Thực đơn 2', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'2d80def2-0135-4373-a4e6-2b15fc0166b6')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'08833a38-356f-473a-aca3-6e10469e21e2', N'Thịt gà', N'Thịt gà', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'3fa85f64-5717-4562-b3fc-2c963f66afa1')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'507f7e22-d5b4-4755-affe-813bf0280e14', N'Thịt bò', N'Thịt bò', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'3fa85f64-5717-4562-b3fc-2c963f66afa1')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'8519d30b-dfa2-45e0-9541-963e11886015', N'Món xào', N'Món xào', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'8033733e-b4d5-4dfb-8316-e66481022cf2')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'ad4f47e4-9408-49cd-b1d7-964225c1818d', N'Thực đơn 3', N'Thực đơn 3', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'2d80def2-0135-4373-a4e6-2b15fc0166b6')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'8b720ab2-25de-4aae-bef2-a3f780071386', N'Bữa trưa', N'Bữa trưa', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'e5c95ca2-d0fb-4002-8c0f-da4b39312730')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'13e8010c-f583-4a32-9a76-a7f188983c69', N'Miền Nam', N'Miền Nam', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'14465782-6f37-4a8f-8d5e-7a0bea7f3b4c')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'c72749fc-917a-469d-b17e-c5c934c350f4', N'Món chay', N'Món chay', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'433b682e-3651-4f4b-b688-0eaf344c51bd')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'cd704511-e8fa-4083-a847-c667eb8eece2', N'Dễ làm', N'Dễ làm', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'b3648659-6a2a-4795-80ea-b9f987d88b03')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'9747494f-5a51-4056-bef5-c6c03305ee5c', N'Món canh', N'Món canh', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'8033733e-b4d5-4dfb-8316-e66481022cf2')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'715ac265-370a-46a5-b8bd-c8b415b6880f', N'Thịt cá', N'Thịt cá', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'3fa85f64-5717-4562-b3fc-2c963f66afa1')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'e23f1a4a-f82c-41e3-9305-d50f77566808', N'Eat clean', N'Eat clean', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'433b682e-3651-4f4b-b688-0eaf344c51bd')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'f00ab7c8-f31d-422e-b295-e990e2074f50', N'Thịt vịt', N'Thịt vịt', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'3fa85f64-5717-4562-b3fc-2c963f66afa1')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'44d5227c-d10d-465e-a509-f70e469143ea', N'Bữa tối', N'Bữa tối', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'e5c95ca2-d0fb-4002-8c0f-da4b39312730')
GO
INSERT [dbo].[Transaction] ([transactionId], [totalAmount], [createdDate], [transactionStatus], [orderId], [customerId]) VALUES (N'35920610-6bef-47f3-a5ab-50c6e2f5be16', 440000.0000, CAST(N'2023-04-08T11:05:27.280' AS DateTime), 1, NULL, N'89fcfb56-be89-442d-9b99-8f498023a5cc')
INSERT [dbo].[Transaction] ([transactionId], [totalAmount], [createdDate], [transactionStatus], [orderId], [customerId]) VALUES (N'dd49d72e-84ba-4b03-9e7f-6af555f50851', 50000.0000, CAST(N'2023-04-08T11:02:37.057' AS DateTime), 2, NULL, N'89fcfb56-be89-442d-9b99-8f498023a5cc')
GO
INSERT [dbo].[Type] ([typeId], [name], [description], [status]) VALUES (N'53418c10-fd90-42c8-951a-0e4430008078', N'Đồ hộp', N'Đồ hộp', 1)
INSERT [dbo].[Type] ([typeId], [name], [description], [status]) VALUES (N'b7ffc72e-ade7-436c-983e-461fe5b78dfc', N'Gia vị', N'gia vị', 1)
INSERT [dbo].[Type] ([typeId], [name], [description], [status]) VALUES (N'59152ba3-f4b0-47b2-9412-63d368756026', N'adad', N'adad', 0)
INSERT [dbo].[Type] ([typeId], [name], [description], [status]) VALUES (N'1177aed5-130b-4180-b57b-68cd72bafb17', N'Rau củ', N'rau củ', 1)
INSERT [dbo].[Type] ([typeId], [name], [description], [status]) VALUES (N'773201fd-4d88-40be-825e-cebb5b01c18e', N'Đồ khô', N'đồ khô', 1)
INSERT [dbo].[Type] ([typeId], [name], [description], [status]) VALUES (N'4f0cdf40-0444-4293-96e8-e35839d10037', N'Đồ tươi sống', N'đồ tươi sống', 1)
GO
INSERT [dbo].[Unit] ([unitId], [name], [description], [createdDate], [status]) VALUES (N'a2d09468-11ea-4db0-bb25-11b2b01d89a1', N'hộp', N'hộp', NULL, 1)
INSERT [dbo].[Unit] ([unitId], [name], [description], [createdDate], [status]) VALUES (N'dabc7178-e761-4748-b8f7-1ffc98dfb0c4', N'chai', N'string', NULL, 1)
INSERT [dbo].[Unit] ([unitId], [name], [description], [createdDate], [status]) VALUES (N'1082dc1a-ac02-452a-b847-25bf5b98630c', N'trái', N'trái', NULL, 1)
INSERT [dbo].[Unit] ([unitId], [name], [description], [createdDate], [status]) VALUES (N'8e2c242e-a16e-4d5f-8ce9-27485c2a801a', N'kg', N'kí', NULL, 1)
INSERT [dbo].[Unit] ([unitId], [name], [description], [createdDate], [status]) VALUES (N'c6c0d810-5131-431c-9213-5f2dc17402cd', N'củ', N'củ', NULL, 1)
INSERT [dbo].[Unit] ([unitId], [name], [description], [createdDate], [status]) VALUES (N'd4c50d29-9425-42a3-b029-76f9e55214f2', N'con', N'con', NULL, 1)
INSERT [dbo].[Unit] ([unitId], [name], [description], [createdDate], [status]) VALUES (N'05c16c64-1994-4ac9-bb79-d5be4fcca460', N'gói', N'string', NULL, 1)
INSERT [dbo].[Unit] ([unitId], [name], [description], [createdDate], [status]) VALUES (N'f15ae6fb-6be4-46d7-ba6c-f3239350da66', N'gram', N'gram', NULL, 1)
GO
INSERT [dbo].[User] ([userId], [displayname], [username], [firstname], [lastname], [email], [password], [phonenumber], [gender], [avatar], [role], [createdDate], [updatedDate], [isBlocked], [isGoogle]) VALUES (N'211a77bd-7e7a-4fc4-a391-39577a66935d', NULL, N'minhnhut', N'Nhut', N'Nhut', N'minhnhut@gmail.com', N'kVU41twDyttUL/SM7IO0vQ==', N'0985412541', 1, NULL, 1, CAST(N'2022-12-31T23:12:44.720' AS DateTime), CAST(N'2022-12-31T23:12:44.720' AS DateTime), 0, 0)
INSERT [dbo].[User] ([userId], [displayname], [username], [firstname], [lastname], [email], [password], [phonenumber], [gender], [avatar], [role], [createdDate], [updatedDate], [isBlocked], [isGoogle]) VALUES (N'749f5b3b-dea1-49a4-98b8-96da197d123f', NULL, N'vanphuong0606', N'Phương', N'Võ Văn', N'vanphuong0606@gmail.com', N'kVU41twDyttUL/SM7IO0vQ==', N'0971775169', 1, NULL, 2, CAST(N'2022-12-31T23:12:44.720' AS DateTime), CAST(N'2022-12-31T23:12:44.720' AS DateTime), 0, 0)
INSERT [dbo].[User] ([userId], [displayname], [username], [firstname], [lastname], [email], [password], [phonenumber], [gender], [avatar], [role], [createdDate], [updatedDate], [isBlocked], [isGoogle]) VALUES (N'c1bd7421-a3d0-496a-a775-b307737777c1', NULL, N'monkeynam208', N' Nam', N'Nguyễn Lương Hoàng', N'monkeynam208@gmail.com', N'kVU41twDyttUL/SM7IO0vQ==', N'0987603163', 1, NULL, 1, CAST(N'2022-12-31T23:12:44.720' AS DateTime), CAST(N'2022-12-31T23:12:44.720' AS DateTime), 0, 1)
GO
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrderPrice], [maximumOrderPrice], [authorId]) VALUES (N'00000000-0000-0000-0000-000000000000', N'Nạp lần đầu', N'string', 3, CAST(N'2023-01-13T22:46:45.213' AS DateTime), CAST(N'2023-01-14T00:08:19.010' AS DateTime), CAST(N'2023-01-14T00:08:19.010' AS DateTime), 15000.0000, 23000.0000, 150000.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrderPrice], [maximumOrderPrice], [authorId]) VALUES (N'8cf462b9-a323-4eca-bc01-2de9992a5a7d', N'Nạp lần đầu', N'No thing', 0, CAST(N'2023-01-13T22:46:45.213' AS DateTime), CAST(N'2023-01-13T22:46:45.213' AS DateTime), CAST(N'2023-01-13T22:46:45.213' AS DateTime), 15000.0000, 23000.0000, 150000.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrderPrice], [maximumOrderPrice], [authorId]) VALUES (N'dde34ee3-9dc6-46d0-830d-3128105eacad', N'Nạp lần đầu', N'No thing', 1, CAST(N'2023-01-14T00:08:19.180' AS DateTime), CAST(N'2023-01-14T00:08:19.180' AS DateTime), CAST(N'2023-01-14T00:08:19.180' AS DateTime), 15000.0000, 23000.0000, 150000.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrderPrice], [maximumOrderPrice], [authorId]) VALUES (N'9fb22d85-142a-407d-85e8-5034a38b732c', N'Nạp lần đầu', N'No thing', 1, CAST(N'2023-01-14T00:08:19.347' AS DateTime), CAST(N'2023-01-14T00:08:19.347' AS DateTime), CAST(N'2023-01-14T00:08:19.347' AS DateTime), 15000.0000, 23000.0000, 150000.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrderPrice], [maximumOrderPrice], [authorId]) VALUES (N'06a84c50-1960-42b5-ad76-6e3cf06d5269', N'Nạp lần đầu', N'No thing', 0, CAST(N'2023-01-14T00:08:19.010' AS DateTime), CAST(N'2023-01-14T00:08:19.010' AS DateTime), CAST(N'2023-01-14T00:08:19.010' AS DateTime), 15000.0000, 23000.0000, 150000.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrderPrice], [maximumOrderPrice], [authorId]) VALUES (N'c1340754-bbcc-42d1-a73a-a11e1057d1d3', N'string', N'string', 1, CAST(N'2023-01-14T22:22:10.663' AS DateTime), CAST(N'2023-01-14T15:21:56.610' AS DateTime), CAST(N'2023-01-14T15:21:56.610' AS DateTime), 0.0000, 0.0000, 22220.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrderPrice], [maximumOrderPrice], [authorId]) VALUES (N'af0cabc9-7247-4a76-87dd-dba77543fd0c', N'Nạp lần đầu', N'string', 3, CAST(N'2023-01-14T00:27:41.050' AS DateTime), CAST(N'2023-01-14T00:08:19.010' AS DateTime), CAST(N'2023-01-14T00:08:19.010' AS DateTime), 15000.0000, 23000.0000, 150000.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
GO
ALTER TABLE [dbo].[Accomplishment]  WITH CHECK ADD  CONSTRAINT [FK_Accomplishment_Blog] FOREIGN KEY([blogId])
REFERENCES [dbo].[Blog] ([blogId])
GO
ALTER TABLE [dbo].[Accomplishment] CHECK CONSTRAINT [FK_Accomplishment_Blog]
GO
ALTER TABLE [dbo].[Accomplishment]  WITH CHECK ADD  CONSTRAINT [FK_Accomplishment_Customer] FOREIGN KEY([authorId])
REFERENCES [dbo].[Customer] ([customerId])
GO
ALTER TABLE [dbo].[Accomplishment] CHECK CONSTRAINT [FK_Accomplishment_Customer]
GO
ALTER TABLE [dbo].[Accomplishment]  WITH CHECK ADD  CONSTRAINT [FK_Accomplishment_User] FOREIGN KEY([confirmBy])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Accomplishment] CHECK CONSTRAINT [FK_Accomplishment_User]
GO
ALTER TABLE [dbo].[AccomplishmentReaction]  WITH CHECK ADD  CONSTRAINT [FK_AccomplishmentReaction_Accomplishment] FOREIGN KEY([accomplishmentId])
REFERENCES [dbo].[Accomplishment] ([accomplishmentId])
GO
ALTER TABLE [dbo].[AccomplishmentReaction] CHECK CONSTRAINT [FK_AccomplishmentReaction_Accomplishment]
GO
ALTER TABLE [dbo].[AccomplishmentReaction]  WITH CHECK ADD  CONSTRAINT [FK_AccomplishmentReaction_Customer] FOREIGN KEY([customerId])
REFERENCES [dbo].[Customer] ([customerId])
GO
ALTER TABLE [dbo].[AccomplishmentReaction] CHECK CONSTRAINT [FK_AccomplishmentReaction_Customer]
GO
ALTER TABLE [dbo].[Badge]  WITH CHECK ADD  CONSTRAINT [FK_Badge_Voucher] FOREIGN KEY([voucherId])
REFERENCES [dbo].[Voucher] ([voucherId])
GO
ALTER TABLE [dbo].[Badge] CHECK CONSTRAINT [FK_Badge_Voucher]
GO
ALTER TABLE [dbo].[BadgeCondition]  WITH CHECK ADD  CONSTRAINT [FK_BadgeCondition_Badge] FOREIGN KEY([badgeId])
REFERENCES [dbo].[Badge] ([badgeId])
GO
ALTER TABLE [dbo].[BadgeCondition] CHECK CONSTRAINT [FK_BadgeCondition_Badge]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Recipe1] FOREIGN KEY([recipeId])
REFERENCES [dbo].[Recipe] ([recipeId])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_Recipe1]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_User] FOREIGN KEY([authorId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_User]
GO
ALTER TABLE [dbo].[BlogReaction]  WITH CHECK ADD  CONSTRAINT [FK_BlogReaction_Blog] FOREIGN KEY([blogId])
REFERENCES [dbo].[Blog] ([blogId])
GO
ALTER TABLE [dbo].[BlogReaction] CHECK CONSTRAINT [FK_BlogReaction_Blog]
GO
ALTER TABLE [dbo].[BlogReaction]  WITH CHECK ADD  CONSTRAINT [FK_BlogReaction_Customer] FOREIGN KEY([customerId])
REFERENCES [dbo].[Customer] ([customerId])
GO
ALTER TABLE [dbo].[BlogReaction] CHECK CONSTRAINT [FK_BlogReaction_Customer]
GO
ALTER TABLE [dbo].[BlogReference]  WITH CHECK ADD  CONSTRAINT [FK_BlogReference_Blog] FOREIGN KEY([blogId])
REFERENCES [dbo].[Blog] ([blogId])
GO
ALTER TABLE [dbo].[BlogReference] CHECK CONSTRAINT [FK_BlogReference_Blog]
GO
ALTER TABLE [dbo].[BlogSubCate]  WITH CHECK ADD  CONSTRAINT [FK_BlogSubCate_Blog] FOREIGN KEY([blogId])
REFERENCES [dbo].[Blog] ([blogId])
GO
ALTER TABLE [dbo].[BlogSubCate] CHECK CONSTRAINT [FK_BlogSubCate_Blog]
GO
ALTER TABLE [dbo].[BlogSubCate]  WITH CHECK ADD  CONSTRAINT [FK_BlogSubCate_SubCategory] FOREIGN KEY([subCateId])
REFERENCES [dbo].[SubCategory] ([subCategoryId])
GO
ALTER TABLE [dbo].[BlogSubCate] CHECK CONSTRAINT [FK_BlogSubCate_SubCategory]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Blog] FOREIGN KEY([blogId])
REFERENCES [dbo].[Blog] ([blogId])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Blog]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Comment] FOREIGN KEY([parentId])
REFERENCES [dbo].[Comment] ([commentId])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Comment]
GO
ALTER TABLE [dbo].[CustomerBadge]  WITH CHECK ADD  CONSTRAINT [FK_CustomerBadge_Badge] FOREIGN KEY([badgeId])
REFERENCES [dbo].[Badge] ([badgeId])
GO
ALTER TABLE [dbo].[CustomerBadge] CHECK CONSTRAINT [FK_CustomerBadge_Badge]
GO
ALTER TABLE [dbo].[CustomerBadge]  WITH CHECK ADD  CONSTRAINT [FK_CustomerBadge_Customer] FOREIGN KEY([customerId])
REFERENCES [dbo].[Customer] ([customerId])
GO
ALTER TABLE [dbo].[CustomerBadge] CHECK CONSTRAINT [FK_CustomerBadge_Customer]
GO
ALTER TABLE [dbo].[CustomerVoucher]  WITH CHECK ADD  CONSTRAINT [FK_CustomerVoucher_Customer] FOREIGN KEY([customerId])
REFERENCES [dbo].[Customer] ([customerId])
GO
ALTER TABLE [dbo].[CustomerVoucher] CHECK CONSTRAINT [FK_CustomerVoucher_Customer]
GO
ALTER TABLE [dbo].[CustomerVoucher]  WITH CHECK ADD  CONSTRAINT [FK_CustomerVoucher_Voucher] FOREIGN KEY([voucherId])
REFERENCES [dbo].[Voucher] ([voucherId])
GO
ALTER TABLE [dbo].[CustomerVoucher] CHECK CONSTRAINT [FK_CustomerVoucher_Voucher]
GO
ALTER TABLE [dbo].[Ingredient]  WITH CHECK ADD  CONSTRAINT [FK_Ingredient_Type] FOREIGN KEY([typeId])
REFERENCES [dbo].[Type] ([typeId])
GO
ALTER TABLE [dbo].[Ingredient] CHECK CONSTRAINT [FK_Ingredient_Type]
GO
ALTER TABLE [dbo].[Ingredient]  WITH CHECK ADD  CONSTRAINT [FK_Ingredient_Unit] FOREIGN KEY([unitId])
REFERENCES [dbo].[Unit] ([unitId])
GO
ALTER TABLE [dbo].[Ingredient] CHECK CONSTRAINT [FK_Ingredient_Unit]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([customerId])
REFERENCES [dbo].[Customer] ([customerId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([orderId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[RecipeDetail]  WITH CHECK ADD  CONSTRAINT [FK_RecipeDetail_Ingredient] FOREIGN KEY([ingredientId])
REFERENCES [dbo].[Ingredient] ([ingredientId])
GO
ALTER TABLE [dbo].[RecipeDetail] CHECK CONSTRAINT [FK_RecipeDetail_Ingredient]
GO
ALTER TABLE [dbo].[RecipeDetail]  WITH CHECK ADD  CONSTRAINT [FK_RecipeDetail_Recipe] FOREIGN KEY([recipeId])
REFERENCES [dbo].[Recipe] ([recipeId])
GO
ALTER TABLE [dbo].[RecipeDetail] CHECK CONSTRAINT [FK_RecipeDetail_Recipe]
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_Tag_Category] FOREIGN KEY([categoryId])
REFERENCES [dbo].[Category] ([categoryId])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_Tag_Category]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Order] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([orderId])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Order]
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_User] FOREIGN KEY([authorId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_User]
GO
USE [master]
GO
ALTER DATABASE [Homnayangi] SET  READ_WRITE 
GO
