USE [master]
GO
/****** Object:  Database [Homnayangi]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[Accomplishment]    Script Date: 2/21/2023 8:09:13 PM ******/
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
	[videoURL] [nvarchar](max) NULL,
 CONSTRAINT [PK_Accomplishment] PRIMARY KEY CLUSTERED 
(
	[accomplishmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 2/21/2023 8:09:13 PM ******/
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
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[blogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogReaction]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[BlogReference]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[BlogSubCate]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[Cart]    Script Date: 2/21/2023 8:09:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[cartId] [uniqueidentifier] NOT NULL,
	[customerId] [uniqueidentifier] NULL,
	[quantityOfItem] [int] NULL,
 CONSTRAINT [PK_Cart_2] PRIMARY KEY CLUSTERED 
(
	[cartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartDetail]    Script Date: 2/21/2023 8:09:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetail](
	[cartId] [uniqueidentifier] NOT NULL,
	[itemId] [uniqueidentifier] NOT NULL,
	[isCooked] [bit] NOT NULL,
	[quantity] [int] NULL,
	[unitPrice] [money] NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[cartId] ASC,
	[itemId] ASC,
	[isCooked] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[Comment]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[CustomerReward]    Script Date: 2/21/2023 8:09:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerReward](
	[customerId] [uniqueidentifier] NOT NULL,
	[rewardId] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NULL,
 CONSTRAINT [PK_CustomerReward] PRIMARY KEY CLUSTERED 
(
	[customerId] ASC,
	[rewardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerVoucher]    Script Date: 2/21/2023 8:09:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerVoucher](
	[voucherId] [uniqueidentifier] NOT NULL,
	[customerId] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK_CustomerVoucher] PRIMARY KEY CLUSTERED 
(
	[voucherId] ASC,
	[customerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredient]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[Notification]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[Order]    Script Date: 2/21/2023 8:09:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[orderId] [uniqueidentifier] NOT NULL,
	[orderDate] [datetime] NULL,
	[shippedDate] [datetime] NULL,
	[shippedAddress] [nvarchar](max) NULL,
	[discount] [money] NULL,
	[totalPrice] [money] NULL,
	[orderStatus] [int] NULL,
	[customerId] [uniqueidentifier] NULL,
	[voucherId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderCookedDetail]    Script Date: 2/21/2023 8:09:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderCookedDetail](
	[orderId] [uniqueidentifier] NOT NULL,
	[recipeId] [uniqueidentifier] NOT NULL,
	[quantity] [int] NULL,
	[session] [int] NULL,
	[price] [money] NULL,
 CONSTRAINT [PK_OrderCookedDetail] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC,
	[recipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderIngredientDetail]    Script Date: 2/21/2023 8:09:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderIngredientDetail](
	[orderId] [uniqueidentifier] NOT NULL,
	[ingredientId] [uniqueidentifier] NOT NULL,
	[quantity] [int] NULL,
	[price] [money] NULL,
 CONSTRAINT [PK_OrderIngredientDetail] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC,
	[ingredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderPackageDetail]    Script Date: 2/21/2023 8:09:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderPackageDetail](
	[orderId] [uniqueidentifier] NOT NULL,
	[recipeId] [uniqueidentifier] NOT NULL,
	[quantity] [int] NULL,
	[price] [money] NULL,
 CONSTRAINT [PK_OrderPackageDetail] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC,
	[recipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceNote]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[Recipe]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[RecipeDetail]    Script Date: 2/21/2023 8:09:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeDetail](
	[recipeId] [uniqueidentifier] NOT NULL,
	[ingredientId] [uniqueidentifier] NOT NULL,
	[description] [nvarchar](max) NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK_RecipeDetail] PRIMARY KEY CLUSTERED 
(
	[recipeId] ASC,
	[ingredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reward]    Script Date: 2/21/2023 8:09:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reward](
	[rewardId] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](max) NULL,
	[createDate] [datetime] NULL,
	[imageURL] [nvarchar](max) NULL,
	[status] [bit] NULL,
	[conditionType] [int] NULL,
	[conditionValue] [int] NULL,
 CONSTRAINT [PK_Reward] PRIMARY KEY CLUSTERED 
(
	[rewardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[Transaction]    Script Date: 2/21/2023 8:09:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[transactionId] [uniqueidentifier] NOT NULL,
	[totalAmount] [money] NULL,
	[createdDate] [datetime] NULL,
	[transactionStatus] [int] NULL,
	[customerId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[transactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[Unit]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 2/21/2023 8:09:13 PM ******/
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
/****** Object:  Table [dbo].[Voucher]    Script Date: 2/21/2023 8:09:13 PM ******/
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
	[minimumOrder] [money] NULL,
	[maximumOrder] [money] NULL,
	[authorId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
(
	[voucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'57448a79-8855-42ad-bd2e-0295d1436037', N'Cách làm bánh ngào mật mía Nghệ An
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/banh-ngao-4x5.jpg?alt=media&token=6ed4044c-d6eb-41d6-9adc-0092b28b400b', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 15, 44, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'f5f5f2d8-1ae7-4bb9-b135-6dfad9b39988')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'e8a73c51-7506-4e0a-b801-05175f95b70e', N'Cách nấu vịt om sấu miền Bắc với khoai sọ ngon như ngoài hàng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/vit-om-sau-4x3.jpg?alt=media&token=09204f09-69f6-4827-a7f5-81b42ad401d3', CAST(N'2022-12-31T23:17:20.830' AS DateTime), CAST(N'2022-12-31T23:17:20.830' AS DateTime), 12, 42, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'cfaa3f8b-f7b7-405f-a01d-206a7d87dc32')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'fa1cdf34-652d-4e8e-8baf-19917d31772a', N'Cách nấu canh khoai mỡ trắng với tôm tươi ngon bổ
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-khoai-mo-16x9.jpg?alt=media&token=99b5cc05-7df6-42f8-892f-f7c274ad23d1', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 11, 43, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'b8c494c3-bebb-4347-8b99-47c1f02009b3')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'f8c8138f-4e79-4a83-b891-28b2062ad382', N'Spaghetti bún gạo lức ăn kèm cải bó xôi', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/bun-xao.webp?alt=media&token=5a5a7b30-26fc-4926-b520-82d804fd7596', CAST(N'2022-12-30T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 43, 67, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'9e343cac-2e68-48fc-8d71-87f929e9b8ce')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'404debd8-422f-42e1-9431-2f1f2101777a', N'Cách làm bún chả Hà Nội ngon như ngoài hàng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bun-cha-ha-noi.jpg?alt=media&token=fa8df759-3b3d-40d2-b7e6-d16ff0c20601', CAST(N'2022-12-31T23:18:14.923' AS DateTime), CAST(N'2022-12-31T23:18:14.923' AS DateTime), 23, 53, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'dfd04bc8-8286-4e9d-8964-b8470ce67ff5')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'899c2bd0-1244-478c-b88b-399886f1436f', N'Cách làm bí xanh xào thịt bò lạ miệng, ăn là ghiền
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/bi-xanh-xao-thit-bo-5x7.jpg?alt=media&token=53f72188-9446-46d5-8607-7cff356b947b', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 6, 19, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'2346415c-0318-4892-9dfb-4ad6ae883d7c')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'da6f26fa-4ea8-4dc8-b42c-4d69dee37c9c', N'Cách nấu cháo thịt bò bí đỏ cho bé và cho cả người lớn
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/chao-thit-bo-bi-do-5x7.jpg?alt=media&token=89d4ccef-b375-4f74-a882-964b21a685e2', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 26, 54, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'8669fc4a-731d-404a-a291-4c4048045988')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'95389b16-fc8e-4db3-80d3-5ef9c49d4ede', N'Cách làm thịt kho mắm ruốc sả ớt thơm ngon đậm đà
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-thit-kho-mam-ruoc.jpg?alt=media&token=eb268a7b-d515-4e57-bf49-754b58a8b896', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 13, 55, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'6551fc76-3aaa-4036-9d09-37f5429d5772')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'9e4c748b-01fc-438c-8cd0-61cf4bde58ff', N'Giá xào đậu hũ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thanh-pham-1142.jpg?alt=media&token=21452719-5105-4e9f-ba41-7473360bbec1', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 32, 55, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'3f1aa816-496b-4ae0-a90f-d6570d525ec9')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'a6597311-ea7c-4f64-97c0-6b89951d73dc', N'Cách nấu bún cá dọc mùng kiểu miền Bắc ngon chuẩn vị
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bun-ca.jpg?alt=media&token=9276f0ed-8d0a-44bb-a379-ca67922411b1', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 12, 42, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'd279301e-9cab-4333-9d12-2c28fc1b8484')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'b8b93f52-9396-4ee0-8031-6cd6c93fefe9', N'Cách nấu canh chua cá lóc miền Tây Nam Bộ
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-chua-ca-loc-5x7.jpg?alt=media&token=f2216489-0040-4e67-825e-4c246cd34fb3', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 22, 45, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'7975d2a0-c6f4-4654-b94f-a217c4ea6ada')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'c41fcd43-ce4d-454b-9c43-6fcccc543632', N'Cách làm mì xào bò đơn giản mà ngon, đủ dinh dưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-mi-xao-bo-ngon.jpg?alt=media&token=44878e3d-67b5-4ae5-acbb-3c783ec381fc', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 23, 42, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'3456764c-3c95-4b4e-8b46-c564ae9cb9bf')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'7bf92d79-1c3b-4473-820d-7425de673d1c', N'Cách làm món cà pháo mắm tôm ngon, ăn là ghiền
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-ca-phao-mam-tom-2.jpg?alt=media&token=58a58a25-6dba-4610-80de-68ca03d9bc9f', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 12, 33, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'cf04775c-51a9-4664-ae28-493318c3ab8b')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'80e34167-9504-4399-8b1c-755d19d79e5f', N'Cách làm sườn xào chua ngọt trong 30 phút (cách mới)
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-suon-xao-chua-ngot-mien-bac.jpg?alt=media&token=f31781f0-a238-4171-b321-636844f23e84', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 12, 35, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'cdce7aa7-cfa7-48f4-ba8f-cab4c979b03d')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'a793327a-55e0-453e-af63-75fb1466126b', N'Cách làm xôi xéo Hà Nội ngon chuẩn vị xôi xéo Phú Thượng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/xoi-xeo-16x9.jpg?alt=media&token=d545ee4a-1f79-4d97-a3df-901d4e729cfa', CAST(N'2022-12-31T23:17:36.097' AS DateTime), CAST(N'2022-12-31T23:17:36.097' AS DateTime), 21, 34, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'5bc8d213-4717-4500-adf3-f44a131cb617')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'2b0815b4-a000-4e18-8cce-864ef07886f6', N'Đậu phụ sốt cà chua', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-dau-sot-ca-chua-2.jpg?alt=media&token=21be2a2d-a8be-40c5-acf9-a338584d1f39', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 12, 55, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'62642194-a143-480f-9f41-b84505d548bb')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'da109a7c-f359-494d-bc95-8854cefc223a', N'Ức gà nướng mật ong balsamic rất tốt cho người ăn giảm cân!', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ức-ga-nướng-mật-ong-balsamic-rất-tốt-cho-người-an-giảm-can-recipe-main-photo.webp?alt=media&token=9b4c6117-616b-4da5-9bf6-70d19c394b3f', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 34, 67, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'3d33d012-ce32-4645-bc85-d98b7644a87a')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'930fc740-7297-4235-a77d-8e9f0bf34806', N'Ức Gà Cuộn Rong Biển ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ga-cuon-rong-bien.webp?alt=media&token=993690e3-eeef-4c70-a6ba-f0894c470012', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 34, 65, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'92ecfaf5-745d-451f-9c6c-c8a95cc8b607')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'9c885816-32f0-455a-bc22-8f5365cd722f', N'Đậu hũ xào rau củ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/maxresdefault.jpg?alt=media&token=4afcc7e5-7314-4946-8e26-34dd934a4be8', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 23, 44, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'a6c08997-114b-456f-a815-b68e2a5f9745')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'2dbbd555-d4b9-49dd-bacb-926c10ba8a2a', N'Đậu phụ chiên sả ớt', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/MVRQ94ZtT4OzzpfsP8IF_10.duahuchienxa-large%402x.jpg.jpg?alt=media&token=7a4b1c50-f7a5-40df-81e9-6a3d23b6c96b', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 13, 54, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'bffd0e26-85a2-4b71-95f8-b565ec79f956')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'16966619-f7cf-43eb-b758-953e95e81532', N'Cách nấu thịt bò rim miền Trung
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bo-rim-2.jpg?alt=media&token=97e96f0e-20c6-4ec1-9055-a301d5b03369', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 32, 67, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'f63430aa-d4a8-44dc-a1c4-577eac23d18f')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'ad4f47e4-9408-49cd-b1d7-964225c1818d', N'Cách nấu canh nấm kim châm thơm ngon bổ dưỡng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-nam-kim-cham-4x3.jpg?alt=media&token=ed4b76f7-331e-4bd9-9b13-319277a4cd85', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 34, 55, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'b5abba73-9cd5-4fff-83ba-c84373351551')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'705a19e7-1369-4f33-975c-97f800b66919', N'Cách làm bánh mì nguyên cám bằng nồi chiên không dầu', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thanh-pham-363.jpg?alt=media&token=d31dbf0e-fbcc-439a-9fb2-002ec9004723', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 48, 76, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'1d8e4062-2aee-47a0-9866-10c919f35f05')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'48ca1508-2569-431b-8c39-9ac18939a1ed', N'Cách làm nem rán miền Bắc giòn rụm, vàng đều, không bị ỉu
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-nem-ran-6.jpg?alt=media&token=266a42ed-1ccc-486e-ae37-e6cfb61bc864', CAST(N'2022-12-31T23:17:54.847' AS DateTime), CAST(N'2022-12-31T23:17:54.847' AS DateTime), 23, 45, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'52a78c72-92d6-4d84-8762-ec282b185105')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'f0a76a43-20e9-4009-b368-9de37a4ecdfb', N'Các cách nấu canh súp lơ ngon đủ vị
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-sup-lo-5x7.jpg?alt=media&token=e58929b5-3558-47d1-a353-a548937c2f3f', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 27, 53, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'0101efd3-8a1a-4386-810a-4c008d11616f')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'11a65e68-ca66-4144-b10d-a01cb8f01bab', N'Cách làm bê tái chanh ngon như nhà hàng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/tron-be-tai-chanh.jpg?alt=media&token=1dbcd8c4-76e5-4788-a63d-9852ca0a06d7', CAST(N'2022-12-31T23:17:45.557' AS DateTime), CAST(N'2022-12-31T23:17:45.557' AS DateTime), 21, 34, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'2ec52ea9-65f8-496f-8f35-509c0bee85b8')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'5777c918-0acd-4e19-86dd-a8ebd77a69cb', N'Cách làm mực xào sa tế siêu ngon mà đơn giản
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/muc-xao-sa-te-4x3.jpg?alt=media&token=9b51f4a9-3429-42b0-a100-4da35819fdb3', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 11, 34, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'16c2a414-7a46-48bb-998c-bb5e73e16f4e')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'f3232eb3-1c98-4dbd-8d10-b03f58927a71', N'Cách nấu thịt đông miền Bắc ngon, vị thanh không ngấy
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thit-dong-4x3.jpg?alt=media&token=6ca8fd4d-0b68-4cbc-b29d-170c33c7afe9', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 19, 26, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'9c7a3550-8f2e-44e0-a0bc-82e975870067')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'ff0c4b6d-b5a3-4b83-82dd-b541986f1550', N'Muesli & Greek Yoghurt (Sữa Chua Hy Lạp) Kèm Quả Mọng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/muesli-greek-yoghurt-sữa-chua-hy-lạp-kem-quả-mọng-an-sang-diet-nhanh-gọn-healthy-recipe-main-photo.webp?alt=media&token=0ac971aa-e061-40a1-92bb-61a22a368265', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 45, 75, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'349adefc-32d8-4dd8-be8f-aebb3f32b6cb')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'861f611f-ad13-436b-8d30-b6bfd5f9e6ac', N'Cá hồi nướng chanh', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/Untitled-1-1200x676-10-1024x526.jpg?alt=media&token=73021885-d554-4c40-ae23-559350f087eb', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 45, 78, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'6e755350-6246-4b62-8547-c3164e97c756')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'a9d116cc-6a7b-4022-81ad-bdcd154b416e', N'Cách làm gà nấu xáo ăn bún thơm nức mũi
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/xao-ga-16x9.jpg?alt=media&token=efc288e5-9ec5-4464-8604-0b6924428a4a', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 23, 43, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'07830456-3663-4056-b356-d12074df4d78')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'e33cfe76-d7fe-472e-832a-c517c9f86b65', N'Cách làm món sườn kho thơm lạ miệng, ngon quên sầu
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/suon-kho-thom-16x9.jpg?alt=media&token=29e587e0-55b8-4e31-803e-e752dab2fba5', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 27, 67, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'e9e57429-7510-4fbf-9ed8-d33812552bc7')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'75bc99c6-006a-4eb6-9945-cea4a53fe118', N'Cách làm bánh tráng cuốn thịt heo chấm mắm nêm', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/banh-trang-cuon-thit-heo-16x9.jpg?alt=media&token=beab6c49-b93e-49e7-8d31-9b42b5df59fd', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 11, 55, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'1372940f-1d98-44f5-979d-a016c22243a5')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'7fb631b1-abfc-4acd-b1f7-d0651a30b6b1', N'Cách nấu bao tử hầm tiêu xanh 10 điểm chất lượng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/bao-tu-ham-tieu-4x3.jpg?alt=media&token=0ccf36cf-56ab-4db3-99a0-8f7ab0c5c364', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 22, 66, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'e8e4b8f8-e81d-4049-bfd0-8406deec26b0')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'fa686511-092a-4f50-9683-d4623bc5eb0e', N'Cách làm cút lộn xào me ngon khó cưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/dia-cut-lon-xao-me.jpg?alt=media&token=76243db4-b562-4499-bb2c-6db8bfca2c3a', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 33, 66, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'9dba975b-8f1d-4c42-9305-159259142aec')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'93fe0cbf-0070-491f-9474-d7541387c6d7', N'Cách làm sữa chuối Hàn Quốc thơm ngon, bổ dưỡng đơn giản', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/3-cach-lam-sua-chuoi-thom-ngon-bo-duong-don-gian--14-760x367.jpg?alt=media&token=8885e56b-8937-4097-89c8-e58f8612dd30', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 45, 69, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'c034ba3b-3ce4-45ad-808f-6fb3f78a8767')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'20fc3a6b-5c35-48ff-883b-d7cc0de47bf1', N'Cách làm cá hấp bia ngon, ăn không tanh, ngon khó cưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ca-hap-bia-4x3.jpg?alt=media&token=121a4cf9-9b3f-40cf-8293-93af2fdaef84', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 13, 54, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'b891b591-f775-499e-b759-aefdf47c0a84')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'8f342926-3676-432e-a772-d805c3c1c359', N'Cách nấu canh nấm kim châm thơm ngon bổ dưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-nam-kim-cham-4x3.jpg?alt=media&token=8fdb2ed5-1da8-4bbe-acce-d8dace77d4c7', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 21, 48, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'470187ef-15a4-4559-86f3-e4a7e0f26d93')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'eab413d5-1ea5-4585-8758-dbebe6cffb49', N'Cách nấu thịt kho tàu ngon bá cháy
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thit-kho-tau-4x3.jpg?alt=media&token=c6c02404-328f-40a9-bf13-2112c93d8b33', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 16, 54, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'a494dac7-d09a-4d4a-bec2-af10fd702d87')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'8b169908-ef75-4854-a693-dca0e6cba7e3', N'Inarizushi gạo lứt', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/photo.webp?alt=media&token=eb96dd07-5ddd-4960-a034-d06cfaf4aaf1', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 23, 49, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'1208f873-d1f7-490f-b001-e255d30b8ac9')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'f0831775-a7a9-4e45-aedd-e3ae1590e548', N'Chả giò chay', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/staticFood1.png?alt=media&token=c9f539b2-d25c-4a43-a759-6697dc8056ba', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 32, 59, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'a6cad201-d020-4c3b-889c-4687cba8bf7f')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'45ff1006-52be-49b2-8e58-ef33f0e5111e', N'Thịt Bò Xào Bắp Non', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/recipe12572-635635810083497899.jpg?alt=media&token=144464ff-9d89-4bf3-8ee7-68734f12917a', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 24, 56, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'e4dc6231-ebe7-4e52-aa5a-abe4c2488cc3')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'9d442fa4-ea4d-4cfe-ae30-f20a635d9cd0', N'Cách làm cá nấu măng chua ớt ngon miễn chê
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ca-nau-mang-chua-16x9.jpg?alt=media&token=d55cb81e-1d8a-4ae1-96be-ca8619dadedd', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 26, 35, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'31b2a522-cb8d-4cbf-945a-8f63b9fcec84')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'56347797-102a-4720-a6a8-fa58ae1d464b', N'Cách làm salad ức gà sốt mè rang chua chua ngọt ngọt cực ngon
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/cach-lam-salad-uc-ga-sot-me-rang-chua-chua-ngot-ngot-cuc-ngon-avt-1200x676-1.jpg?alt=media&token=8d56bebf-7682-4b7f-9710-093648727883', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 32, 67, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'e73d912a-2482-42c1-b591-f5bdb3e8653a')
INSERT [dbo].[Blog] ([blogId], [title], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'60487557-92a5-4132-afe2-fb00a5d7e238', N'Cách nấu canh dưa bò thơm ngon, đưa cơm, ăn không bị ngán
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-dua-bo-4x5.jpg?alt=media&token=f22b45ed-4697-49ed-8a26-44fd4aeed345', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 28, 59, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'0f536237-c887-4c19-946a-41eab4c2c0af')
GO
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'57448a79-8855-42ad-bd2e-0295d1436037', N'c368e82d-dda4-4b39-bc19-43ef148981e0', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'e8a73c51-7506-4e0a-b801-05175f95b70e', N'a7766c2a-8faf-48f9-bdec-1dfaba22ecf3', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'fa1cdf34-652d-4e8e-8baf-19917d31772a', N'9747494f-5a51-4056-bef5-c6c03305ee5c', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'f8c8138f-4e79-4a83-b891-28b2062ad382', N'93a460cc-f092-447f-aaf5-0564cd0ffadc', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'404debd8-422f-42e1-9431-2f1f2101777a', N'a7766c2a-8faf-48f9-bdec-1dfaba22ecf3', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'899c2bd0-1244-478c-b88b-399886f1436f', N'df9bbf2f-b1c4-4c3d-864b-4c96c37a0b39', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'899c2bd0-1244-478c-b88b-399886f1436f', N'507f7e22-d5b4-4755-affe-813bf0280e14', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'899c2bd0-1244-478c-b88b-399886f1436f', N'8519d30b-dfa2-45e0-9541-963e11886015', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'95389b16-fc8e-4db3-80d3-5ef9c49d4ede', N'85276dc8-058d-4938-b38a-281e7276252c', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'95389b16-fc8e-4db3-80d3-5ef9c49d4ede', N'76bc7165-d0be-4d32-b819-434dfaeba2ad', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'95389b16-fc8e-4db3-80d3-5ef9c49d4ede', N'8c6d0b63-74d9-4931-9c2a-4b7c8b54ed0e', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'95389b16-fc8e-4db3-80d3-5ef9c49d4ede', N'df9bbf2f-b1c4-4c3d-864b-4c96c37a0b39', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'95389b16-fc8e-4db3-80d3-5ef9c49d4ede', N'13e8010c-f583-4a32-9a76-a7f188983c69', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'9e4c748b-01fc-438c-8cd0-61cf4bde58ff', N'c72749fc-917a-469d-b17e-c5c934c350f4', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'a6597311-ea7c-4f64-97c0-6b89951d73dc', N'715ac265-370a-46a5-b8bd-c8b415b6880f', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'b8b93f52-9396-4ee0-8031-6cd6c93fefe9', N'9747494f-5a51-4056-bef5-c6c03305ee5c', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'7bf92d79-1c3b-4473-820d-7425de673d1c', N'c368e82d-dda4-4b39-bc19-43ef148981e0', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'7bf92d79-1c3b-4473-820d-7425de673d1c', N'df9bbf2f-b1c4-4c3d-864b-4c96c37a0b39', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'7bf92d79-1c3b-4473-820d-7425de673d1c', N'8b720ab2-25de-4aae-bef2-a3f780071386', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'80e34167-9504-4399-8b1c-755d19d79e5f', N'85276dc8-058d-4938-b38a-281e7276252c', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'80e34167-9504-4399-8b1c-755d19d79e5f', N'7a13a207-91b5-4e2f-8d6f-4face2528a57', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'80e34167-9504-4399-8b1c-755d19d79e5f', N'8b720ab2-25de-4aae-bef2-a3f780071386', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'80e34167-9504-4399-8b1c-755d19d79e5f', N'44d5227c-d10d-465e-a509-f70e469143ea', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'a793327a-55e0-453e-af63-75fb1466126b', N'a7766c2a-8faf-48f9-bdec-1dfaba22ecf3', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'2b0815b4-a000-4e18-8cce-864ef07886f6', N'c72749fc-917a-469d-b17e-c5c934c350f4', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'da109a7c-f359-494d-bc95-8854cefc223a', N'93a460cc-f092-447f-aaf5-0564cd0ffadc', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'930fc740-7297-4235-a77d-8e9f0bf34806', N'93a460cc-f092-447f-aaf5-0564cd0ffadc', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'9c885816-32f0-455a-bc22-8f5365cd722f', N'7a13a207-91b5-4e2f-8d6f-4face2528a57', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'9c885816-32f0-455a-bc22-8f5365cd722f', N'8519d30b-dfa2-45e0-9541-963e11886015', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'9c885816-32f0-455a-bc22-8f5365cd722f', N'8b720ab2-25de-4aae-bef2-a3f780071386', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'9c885816-32f0-455a-bc22-8f5365cd722f', N'c72749fc-917a-469d-b17e-c5c934c350f4', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'2dbbd555-d4b9-49dd-bacb-926c10ba8a2a', N'ad4f47e4-9408-49cd-b1d7-964225c1818d', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'2dbbd555-d4b9-49dd-bacb-926c10ba8a2a', N'8b720ab2-25de-4aae-bef2-a3f780071386', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'2dbbd555-d4b9-49dd-bacb-926c10ba8a2a', N'c72749fc-917a-469d-b17e-c5c934c350f4', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'2dbbd555-d4b9-49dd-bacb-926c10ba8a2a', N'44d5227c-d10d-465e-a509-f70e469143ea', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'16966619-f7cf-43eb-b758-953e95e81532', N'c368e82d-dda4-4b39-bc19-43ef148981e0', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'ad4f47e4-9408-49cd-b1d7-964225c1818d', N'df9bbf2f-b1c4-4c3d-864b-4c96c37a0b39', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'705a19e7-1369-4f33-975c-97f800b66919', N'e23f1a4a-f82c-41e3-9305-d50f77566808', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'48ca1508-2569-431b-8c39-9ac18939a1ed', N'a7766c2a-8faf-48f9-bdec-1dfaba22ecf3', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'48ca1508-2569-431b-8c39-9ac18939a1ed', N'7a13a207-91b5-4e2f-8d6f-4face2528a57', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'48ca1508-2569-431b-8c39-9ac18939a1ed', N'44d5227c-d10d-465e-a509-f70e469143ea', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'f0a76a43-20e9-4009-b368-9de37a4ecdfb', N'9747494f-5a51-4056-bef5-c6c03305ee5c', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'11a65e68-ca66-4144-b10d-a01cb8f01bab', N'a7766c2a-8faf-48f9-bdec-1dfaba22ecf3', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'5777c918-0acd-4e19-86dd-a8ebd77a69cb', N'13e8010c-f583-4a32-9a76-a7f188983c69', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'ff0c4b6d-b5a3-4b83-82dd-b541986f1550', N'93a460cc-f092-447f-aaf5-0564cd0ffadc', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'861f611f-ad13-436b-8d30-b6bfd5f9e6ac', N'e23f1a4a-f82c-41e3-9305-d50f77566808', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'a9d116cc-6a7b-4022-81ad-bdcd154b416e', N'c368e82d-dda4-4b39-bc19-43ef148981e0', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'e33cfe76-d7fe-472e-832a-c517c9f86b65', N'13e8010c-f583-4a32-9a76-a7f188983c69', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'75bc99c6-006a-4eb6-9945-cea4a53fe118', N'c368e82d-dda4-4b39-bc19-43ef148981e0', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'7fb631b1-abfc-4acd-b1f7-d0651a30b6b1', N'13e8010c-f583-4a32-9a76-a7f188983c69', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'fa686511-092a-4f50-9683-d4623bc5eb0e', N'8519d30b-dfa2-45e0-9541-963e11886015', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'fa686511-092a-4f50-9683-d4623bc5eb0e', N'ad4f47e4-9408-49cd-b1d7-964225c1818d', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'fa686511-092a-4f50-9683-d4623bc5eb0e', N'13e8010c-f583-4a32-9a76-a7f188983c69', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'fa686511-092a-4f50-9683-d4623bc5eb0e', N'cd704511-e8fa-4083-a847-c667eb8eece2', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'fa686511-092a-4f50-9683-d4623bc5eb0e', N'44d5227c-d10d-465e-a509-f70e469143ea', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'93fe0cbf-0070-491f-9474-d7541387c6d7', N'e23f1a4a-f82c-41e3-9305-d50f77566808', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'20fc3a6b-5c35-48ff-883b-d7cc0de47bf1', N'715ac265-370a-46a5-b8bd-c8b415b6880f', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'8f342926-3676-432e-a772-d805c3c1c359', N'ad4f47e4-9408-49cd-b1d7-964225c1818d', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'8f342926-3676-432e-a772-d805c3c1c359', N'9747494f-5a51-4056-bef5-c6c03305ee5c', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'eab413d5-1ea5-4585-8758-dbebe6cffb49', N'85276dc8-058d-4938-b38a-281e7276252c', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'eab413d5-1ea5-4585-8758-dbebe6cffb49', N'76bc7165-d0be-4d32-b819-434dfaeba2ad', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'eab413d5-1ea5-4585-8758-dbebe6cffb49', N'ad4f47e4-9408-49cd-b1d7-964225c1818d', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'8b169908-ef75-4854-a693-dca0e6cba7e3', N'93a460cc-f092-447f-aaf5-0564cd0ffadc', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'f0831775-a7a9-4e45-aedd-e3ae1590e548', N'c72749fc-917a-469d-b17e-c5c934c350f4', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'45ff1006-52be-49b2-8e58-ef33f0e5111e', N'e23f1a4a-f82c-41e3-9305-d50f77566808', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'9d442fa4-ea4d-4cfe-ae30-f20a635d9cd0', N'715ac265-370a-46a5-b8bd-c8b415b6880f', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'56347797-102a-4720-a6a8-fa58ae1d464b', N'e23f1a4a-f82c-41e3-9305-d50f77566808', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'60487557-92a5-4132-afe2-fb00a5d7e238', N'7a13a207-91b5-4e2f-8d6f-4face2528a57', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
INSERT [dbo].[BlogSubCate] ([blogId], [subCateId], [createdDate], [status]) VALUES (N'60487557-92a5-4132-afe2-fb00a5d7e238', N'9747494f-5a51-4056-bef5-c6c03305ee5c', CAST(N'2023-01-09T00:21:29.803' AS DateTime), NULL)
GO
INSERT [dbo].[Cart] ([cartId], [customerId], [quantityOfItem]) VALUES (N'd46f41be-dd2f-4689-85a8-6c5fa0e76799', N'e7e1bd28-8979-4b6e-adbc-458408e6ba41', 1)
GO
INSERT [dbo].[CartDetail] ([cartId], [itemId], [isCooked], [quantity], [unitPrice]) VALUES (N'd46f41be-dd2f-4689-85a8-6c5fa0e76799', N'57448a79-8855-42ad-bd2e-0295d1436037', 0, 2, 50000.0000)
INSERT [dbo].[CartDetail] ([cartId], [itemId], [isCooked], [quantity], [unitPrice]) VALUES (N'd46f41be-dd2f-4689-85a8-6c5fa0e76799', N'57448a79-8855-42ad-bd2e-0295d1436037', 1, 2, 140000.0000)
GO
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'433b682e-3651-4f4b-b688-0eaf344c51bd', N'Phong cách ăn uống', N'Các món ăn được phân loại theo phong cách ăn uống phù hợp với lựa chọn của người dùng', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'2d80def2-0135-4373-a4e6-2b15fc0166b6', N'Thực đơn hôm nay', N'Các món ăn được chia theo thực đơn nhằm gợi ý cho người dùng thực đơn phù hợp', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'3fa85f64-5717-4562-b3fc-2c963f66afa1', N'Loại nguyên liệu', N'Các món ăn được phân loại dựa theo loại thịt chính làm nên món ăn đó', 0, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'872ec1f0-7e66-4947-a635-4aa2f36170f4', N'Độ khó', N'Độ khó khi nấu các món ăn', 1, CAST(N'2023-02-08T02:56:53.360' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'14465782-6f37-4a8f-8d5e-7a0bea7f3b4c', N'Đặc trưng vùng miền', N'Các món ăn với đặc trưng vùng miền khác nhau', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'63db7259-a32a-4a5e-beb8-985f3db5f63f', N'Lứa tuổi', N'Các món ăn có chế độ dinh dưỡng phù hợp cho các lứa tuổi', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'b3648659-6a2a-4795-80ea-b9f987d88b03', N'Khác', N'Bao gồm các cách phân biệt món ăn khác như độ khó, thời gian làm ngắn hay dài.', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'e5c95ca2-d0fb-4002-8c0f-da4b39312730', N'Bữa ăn', N'Các món ăn phù hợp vói mâm cơm của các bữa ăn trong ngày', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'8033733e-b4d5-4dfb-8316-e66481022cf2', N'Cách thực hiện', N'Các món ăn với cách thực hiện khác nhau', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'3b8ab3d0-08df-47f1-bf9f-ecf895fd0daa', N'Món nước', N'Các món ăn ăn kèm với nước như bún, hủ tiếu,...', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Comment] ([commentId], [authorId], [createdDate], [content], [status], [parentId], [blogId], [byStaff]) VALUES (N'85680a02-a2fd-467b-9f1e-277df4db3172', N'c1bd7421-a3d0-496a-a775-b307737777c1', CAST(N'2023-01-25T09:08:58.353' AS DateTime), N'Ahihi do ngoc2', 1, NULL, N'57448a79-8855-42ad-bd2e-0295d1436037', 0)
INSERT [dbo].[Comment] ([commentId], [authorId], [createdDate], [content], [status], [parentId], [blogId], [byStaff]) VALUES (N'06fb5d96-be0f-47de-9fb9-405b654cbd26', N'c1bd7421-a3d0-496a-a775-b307737777c1', CAST(N'2023-01-25T09:06:08.440' AS DateTime), N'Ahihi do ngoc', 1, NULL, N'57448a79-8855-42ad-bd2e-0295d1436037', 0)
INSERT [dbo].[Comment] ([commentId], [authorId], [createdDate], [content], [status], [parentId], [blogId], [byStaff]) VALUES (N'b741a549-029b-402b-b938-45c6ec126d4d', N'c1bd7421-a3d0-496a-a775-b307737777c1', CAST(N'2023-01-25T09:11:34.650' AS DateTime), N'Ahihi do ngoc4', 1, N'06fb5d96-be0f-47de-9fb9-405b654cbd26', N'57448a79-8855-42ad-bd2e-0295d1436037', 1)
INSERT [dbo].[Comment] ([commentId], [authorId], [createdDate], [content], [status], [parentId], [blogId], [byStaff]) VALUES (N'bdffe659-8279-4ba3-8833-48133eb2f7dc', N'c1bd7421-a3d0-496a-a775-b307737777c1', CAST(N'2023-01-25T09:11:27.200' AS DateTime), N'Ahihi do ngoc5', 1, N'06fb5d96-be0f-47de-9fb9-405b654cbd26', N'57448a79-8855-42ad-bd2e-0295d1436037', 0)
INSERT [dbo].[Comment] ([commentId], [authorId], [createdDate], [content], [status], [parentId], [blogId], [byStaff]) VALUES (N'e969ca1a-6968-4956-916e-698eaaf84519', N'c1bd7421-a3d0-496a-a775-b307737777c1', CAST(N'2023-01-25T09:11:33.983' AS DateTime), N'Ahihi do ngoc6', 1, N'06fb5d96-be0f-47de-9fb9-405b654cbd26', N'57448a79-8855-42ad-bd2e-0295d1436037', 1)
INSERT [dbo].[Comment] ([commentId], [authorId], [createdDate], [content], [status], [parentId], [blogId], [byStaff]) VALUES (N'5dafecfa-f6b9-4ea2-b5d0-c72397468422', N'c1bd7421-a3d0-496a-a775-b307737777c1', CAST(N'2023-01-25T09:09:27.960' AS DateTime), N'Ahihi do ngoc8', 1, NULL, N'57448a79-8855-42ad-bd2e-0295d1436037', 0)
INSERT [dbo].[Comment] ([commentId], [authorId], [createdDate], [content], [status], [parentId], [blogId], [byStaff]) VALUES (N'cacecc26-0cba-4ce6-a78a-e72c2ab82d1f', N'c1bd7421-a3d0-496a-a775-b307737777c1', CAST(N'2023-01-25T09:10:03.270' AS DateTime), N'Ahihi do ngoc9', 1, N'85680a02-a2fd-467b-9f1e-277df4db3172', N'57448a79-8855-42ad-bd2e-0295d1436037', 1)
INSERT [dbo].[Comment] ([commentId], [authorId], [createdDate], [content], [status], [parentId], [blogId], [byStaff]) VALUES (N'045d7193-9703-4383-858b-f264229bc144', N'c1bd7421-a3d0-496a-a775-b307737777c1', CAST(N'2023-01-25T09:10:10.390' AS DateTime), N'Ahihi do ngoc10', 1, N'85680a02-a2fd-467b-9f1e-277df4db3172', N'57448a79-8855-42ad-bd2e-0295d1436037', 1)
GO
INSERT [dbo].[Customer] ([customerId], [displayname], [username], [firstname], [lastname], [email], [password], [phonenumber], [gender], [avatar], [createdDate], [updatedDate], [isBlocked], [isGoogle]) VALUES (N'e7e1bd28-8979-4b6e-adbc-458408e6ba41', NULL, N'Peter Vo', N'Võ', N'Văn Phương', N'vanphuong0606@gmail.com', N'31m2312', N'0971775169', 1, N'Nolink', CAST(N'2023-01-14T00:10:44.770' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[CustomerVoucher] ([voucherId], [customerId], [createdDate], [quantity]) VALUES (N'00000000-0000-0000-0000-000000000000', N'e7e1bd28-8979-4b6e-adbc-458408e6ba41', CAST(N'2023-01-14T23:24:45.790' AS DateTime), 2)
INSERT [dbo].[CustomerVoucher] ([voucherId], [customerId], [createdDate], [quantity]) VALUES (N'8cf462b9-a323-4eca-bc01-2de9992a5a7d', N'e7e1bd28-8979-4b6e-adbc-458408e6ba41', CAST(N'2023-01-14T22:24:34.500' AS DateTime), 50)
INSERT [dbo].[CustomerVoucher] ([voucherId], [customerId], [createdDate], [quantity]) VALUES (N'dde34ee3-9dc6-46d0-830d-3128105eacad', N'e7e1bd28-8979-4b6e-adbc-458408e6ba41', CAST(N'2023-01-14T22:24:34.500' AS DateTime), 2)
INSERT [dbo].[CustomerVoucher] ([voucherId], [customerId], [createdDate], [quantity]) VALUES (N'9fb22d85-142a-407d-85e8-5034a38b732c', N'e7e1bd28-8979-4b6e-adbc-458408e6ba41', CAST(N'2023-01-14T22:24:34.500' AS DateTime), 2)
INSERT [dbo].[CustomerVoucher] ([voucherId], [customerId], [createdDate], [quantity]) VALUES (N'06a84c50-1960-42b5-ad76-6e3cf06d5269', N'e7e1bd28-8979-4b6e-adbc-458408e6ba41', CAST(N'2023-01-14T22:24:34.500' AS DateTime), 2)
INSERT [dbo].[CustomerVoucher] ([voucherId], [customerId], [createdDate], [quantity]) VALUES (N'c1340754-bbcc-42d1-a73a-a11e1057d1d3', N'e7e1bd28-8979-4b6e-adbc-458408e6ba41', CAST(N'2023-01-14T22:24:34.500' AS DateTime), 2)
INSERT [dbo].[CustomerVoucher] ([voucherId], [customerId], [createdDate], [quantity]) VALUES (N'af0cabc9-7247-4a76-87dd-dba77543fd0c', N'e7e1bd28-8979-4b6e-adbc-458408e6ba41', CAST(N'2023-01-14T22:24:34.503' AS DateTime), 2)
GO
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'b5e83e15-3916-4133-8471-392edf0ba205', N'updated!', N'string', NULL, NULL, N'string', NULL, CAST(N'2023-01-11T23:06:14.710' AS DateTime), CAST(N'2023-01-11T23:16:18.003' AS DateTime), 1, 30210.0000, NULL, N'6ffc824d-f539-4abe-a08c-be6ccb124d0a', NULL)
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'aa24a3f3-9086-45b5-9b93-7cd3201d0f89', N'string', N'string', NULL, NULL, N'string', NULL, CAST(N'2023-01-11T23:07:09.293' AS DateTime), NULL, 1, 89000.0000, NULL, N'6ffc824d-f539-4abe-a08c-be6ccb124d0a', NULL)
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'd63ba734-0930-495d-9571-afa6d6f59fe0', N'string', N'string', NULL, NULL, N'string', NULL, CAST(N'2023-01-11T23:06:41.130' AS DateTime), NULL, 1, 0.0000, NULL, N'6ffc824d-f539-4abe-a08c-be6ccb124d0a', NULL)
INSERT [dbo].[Ingredient] ([ingredientId], [name], [description], [unitId], [quantity], [picture], [kcal], [createdDate], [updatedDate], [status], [price], [listImage], [typeId], [listImagePosition]) VALUES (N'467f66ec-354a-4cae-948c-c327bf41a54d', N'string', N'string', NULL, NULL, N'string', NULL, CAST(N'2023-01-11T23:06:38.703' AS DateTime), NULL, 1, 0.0000, NULL, N'6ffc824d-f539-4abe-a08c-be6ccb124d0a', NULL)
GO
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'57448a79-8855-42ad-bd2e-0295d1436037', N'Cách làm bánh ngào mật mía Nghệ An
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/banh-ngao-4x5.jpg?alt=media&token=6ed4044c-d6eb-41d6-9adc-0092b28b400b', 50000.0000, 140000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'e8a73c51-7506-4e0a-b801-05175f95b70e', N'Cách làm vịt om sấu miền Bắc với khoai sọ thơm ngon, chuẩn vị. Khi nấu món ăn này việc khó nhất là làm sao cho nước vịt om sấu được trong, không bị đen và có vị chua dịu thanh mát. ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/vit-om-sau-4x3.jpg?alt=media&token=09204f09-69f6-4827-a7f5-81b42ad401d3', 70000.0000, 100000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'1d8e4062-2aee-47a0-9866-10c919f35f05', N'Cách làm bánh mì nguyên cám bằng nồi chiên không dầu', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thanh-pham-363.jpg?alt=media&token=d31dbf0e-fbcc-439a-9fb2-002ec9004723', 34000.0000, 50000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'9dba975b-8f1d-4c42-9305-159259142aec', N'Cách làm cút lộn xào me ngon khó cưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/dia-cut-lon-xao-me.jpg?alt=media&token=76243db4-b562-4499-bb2c-6db8bfca2c3a', 12000.0000, 25000.0000, 4, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'fa1cdf34-652d-4e8e-8baf-19917d31772a', N'Cách nấu canh khoai mỡ trắng với tôm tươi ngon bổ
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-khoai-mo-16x9%20(1).jpg?alt=media&token=34d7eb06-291b-44aa-8ac7-a4ac5ba76753', 59000.0000, 120000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'cfaa3f8b-f7b7-405f-a01d-206a7d87dc32', N'Cách làm vịt om sấu miền Bắc với khoai sọ thơm ngon, chuẩn vị. Khi nấu món ăn này việc khó nhất là làm sao cho nước vịt om sấu được trong, không bị đen và có vị chua dịu thanh mát. ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/vit-om-sau-4x3.jpg?alt=media&token=09204f09-69f6-4827-a7f5-81b42ad401d3', 70000.0000, 100000.0000, 1, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'f8c8138f-4e79-4a83-b891-28b2062ad382', N'Spaghetti bún gạo lức ăn kèm cải bó xôi', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/photo%20(2).webp?alt=media&token=1296c706-33fa-404d-8155-7455c0aaa810', 45000.0000, 60000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'd279301e-9cab-4333-9d12-2c28fc1b8484', N'Cách nấu bún cá dọc mùng kiểu miền Bắc ngon chuẩn vị
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bun-ca.jpg?alt=media&token=9276f0ed-8d0a-44bb-a379-ca67922411b1', 56000.0000, 70000.0000, 4, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'404debd8-422f-42e1-9431-2f1f2101777a', N'Cách làm bún chả Hà Nội ngon như ngoài hàng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bun-cha-ha-noi.jpg?alt=media&token=fa8df759-3b3d-40d2-b7e6-d16ff0c20601', 67000.0000, 78000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'6551fc76-3aaa-4036-9d09-37f5429d5772', N'Cách làm thịt kho mắm ruốc sả ớt thơm ngon đậm đà
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-thit-kho-mam-ruoc.jpg?alt=media&token=eb268a7b-d515-4e57-bf49-754b58a8b896', 45000.0000, 53000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'899c2bd0-1244-478c-b88b-399886f1436f', N'Cách làm bí xanh xào thịt bò lạ miệng, ăn là ghiền
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/bi-xanh-xao-thit-bo-5x7.jpg?alt=media&token=53f72188-9446-46d5-8607-7cff356b947b', 69000.0000, 309000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'0f536237-c887-4c19-946a-41eab4c2c0af', N'Cách nấu canh dưa bò thơm ngon, đưa cơm, ăn không bị ngán
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-dua-bo-4x5.jpg?alt=media&token=f22b45ed-4697-49ed-8a26-44fd4aeed345', 59000.0000, 67000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'a6cad201-d020-4c3b-889c-4687cba8bf7f', N'Chả giò chay', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/staticFood1.png?alt=media&token=c9f539b2-d25c-4a43-a759-6697dc8056ba', 25000.0000, 35000.0000, 4, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'b8c494c3-bebb-4347-8b99-47c1f02009b3', N'Cách nấu canh khoai mỡ trắng với tôm tươi ngon bổ
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-khoai-mo-16x9%20(1).jpg?alt=media&token=34d7eb06-291b-44aa-8ac7-a4ac5ba76753', 59000.0000, 120000.0000, 4, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'cf04775c-51a9-4664-ae28-493318c3ab8b', N'Cách làm món cà pháo mắm tôm ngon, ăn là ghiền
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-ca-phao-mam-tom-2.jpg?alt=media&token=58a58a25-6dba-4610-80de-68ca03d9bc9f', 67000.0000, 89000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'2346415c-0318-4892-9dfb-4ad6ae883d7c', N'Cách làm bí xanh xào thịt bò lạ miệng, ăn là ghiền
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/bi-xanh-xao-thit-bo-5x7.jpg?alt=media&token=53f72188-9446-46d5-8607-7cff356b947b', 69000.0000, 309000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'0101efd3-8a1a-4386-810a-4c008d11616f', N'Các cách nấu canh súp lơ ngon đủ vị
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-sup-lo-5x7.jpg?alt=media&token=e58929b5-3558-47d1-a353-a548937c2f3f', 59000.0000, 67000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'8669fc4a-731d-404a-a291-4c4048045988', N'Cách nấu cháo thịt bò bí đỏ cho bé và cho cả người lớn
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/chao-thit-bo-bi-do-5x7%20(1).jpg?alt=media&token=ca3cc8bb-7abf-4bea-bc63-9d03e26f635d', 52000.0000, 67000.0000, 1, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'da6f26fa-4ea8-4dc8-b42c-4d69dee37c9c', N'Cách nấu cháo thịt bò bí đỏ cho bé và cho cả người lớn
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/chao-thit-bo-bi-do-5x7%20(1).jpg?alt=media&token=ca3cc8bb-7abf-4bea-bc63-9d03e26f635d', 52000.0000, 67000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'2ec52ea9-65f8-496f-8f35-509c0bee85b8', N'Cách làm bê tái chanh ngon như nhà hàng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/tron-be-tai-chanh.jpg?alt=media&token=1dbcd8c4-76e5-4788-a63d-9852ca0a06d7', 87000.0000, 99000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'f63430aa-d4a8-44dc-a1c4-577eac23d18f', N'Cách nấu thịt bò rim miền Trung
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bo-rim-2.jpg?alt=media&token=97e96f0e-20c6-4ec1-9055-a301d5b03369', 80000.0000, 100000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'95389b16-fc8e-4db3-80d3-5ef9c49d4ede', N'Cách làm thịt kho mắm ruốc sả ớt thơm ngon đậm đà
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-thit-kho-mam-ruoc.jpg?alt=media&token=eb268a7b-d515-4e57-bf49-754b58a8b896', 45000.0000, 53000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'9e4c748b-01fc-438c-8cd0-61cf4bde58ff', N'Giá xào đậu hũ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thanh-pham-1142.jpg?alt=media&token=21452719-5105-4e9f-ba41-7473360bbec1', 23000.0000, 32000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'a6597311-ea7c-4f64-97c0-6b89951d73dc', N'Cách nấu bún cá dọc mùng kiểu miền Bắc ngon chuẩn vị
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bun-ca.jpg?alt=media&token=9276f0ed-8d0a-44bb-a379-ca67922411b1', 56000.0000, 70000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'b8b93f52-9396-4ee0-8031-6cd6c93fefe9', N'Cách nấu canh chua cá lóc miền Tây Nam Bộ
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-chua-ca-loc-5x7.jpg?alt=media&token=f2216489-0040-4e67-825e-4c246cd34fb3', 58000.0000, 68000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'f5f5f2d8-1ae7-4bb9-b135-6dfad9b39988', N'Cách làm bánh ngào mật mía Nghệ An
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/banh-ngao-4x5.jpg?alt=media&token=6ed4044c-d6eb-41d6-9adc-0092b28b400b', 50000.0000, 140000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'c034ba3b-3ce4-45ad-808f-6fb3f78a8767', N'Cách làm sữa chuối Hàn Quốc thơm ngon, bổ dưỡng đơn giản', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/3-cach-lam-sua-chuoi-thom-ngon-bo-duong-don-gian--14-760x367.jpg?alt=media&token=8885e56b-8937-4097-89c8-e58f8612dd30', 26000.0000, 36000.0000, 1, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'c41fcd43-ce4d-454b-9c43-6fcccc543632', N'Cách làm mì xào bò đơn giản mà ngon, đủ dinh dưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-mi-xao-bo-ngon.jpg?alt=media&token=44878e3d-67b5-4ae5-acbb-3c783ec381fc', 34000.0000, 45000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'7bf92d79-1c3b-4473-820d-7425de673d1c', N'Cách làm món cà pháo mắm tôm ngon, ăn là ghiền
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-ca-phao-mam-tom-2.jpg?alt=media&token=58a58a25-6dba-4610-80de-68ca03d9bc9f', 67000.0000, 89000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'80e34167-9504-4399-8b1c-755d19d79e5f', N'Cách làm sườn xào chua ngọt trong 30 phút (cách mới)
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-suon-xao-chua-ngot-mien-bac.jpg?alt=media&token=f31781f0-a238-4171-b321-636844f23e84', 45000.0000, 60000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'a793327a-55e0-453e-af63-75fb1466126b', N'Cách làm xôi xéo Hà Nội ngon chuẩn vị xôi xéo Phú Thượng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/xoi-xeo-16x9.jpg?alt=media&token=d545ee4a-1f79-4d97-a3df-901d4e729cfa', 50000.0000, 64000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'9c7a3550-8f2e-44e0-a0bc-82e975870067', N'Cách nấu thịt đông miền Bắc ngon, vị thanh không ngấy
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thit-dong-4x3.jpg?alt=media&token=6ca8fd4d-0b68-4cbc-b29d-170c33c7afe9', 78000.0000, 93000.0000, 4, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'e8e4b8f8-e81d-4049-bfd0-8406deec26b0', N'Cách nấu bao tử hầm tiêu xanh 10 điểm chất lượng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/bao-tu-ham-tieu-4x3.jpg?alt=media&token=0ccf36cf-56ab-4db3-99a0-8f7ab0c5c364', 45000.0000, 60000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'2b0815b4-a000-4e18-8cce-864ef07886f6', N'Đậu phụ sốt cà chua', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-dau-sot-ca-chua-2.jpg?alt=media&token=21be2a2d-a8be-40c5-acf9-a338584d1f39', 67000.0000, 87000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'9e343cac-2e68-48fc-8d71-87f929e9b8ce', N'Spaghetti bún gạo lức ăn kèm cải bó xôi', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/photo%20(2).webp?alt=media&token=1296c706-33fa-404d-8155-7455c0aaa810', 45000.0000, 60000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'da109a7c-f359-494d-bc95-8854cefc223a', N'Ức gà nướng mật ong balsamic rất tốt cho người ăn giảm cân!', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ức-ga-nướng-mật-ong-balsamic-rất-tốt-cho-người-an-giảm-can-recipe-main-photo.webp?alt=media&token=9b4c6117-616b-4da5-9bf6-70d19c394b3f', 56000.0000, 78000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'930fc740-7297-4235-a77d-8e9f0bf34806', N'Ức Gà Cuộn Rong Biển ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/photo%20(1).webp?alt=media&token=8909fd43-2735-4561-aa95-de44ca929df5', 42000.0000, 55000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'9c885816-32f0-455a-bc22-8f5365cd722f', N'Đậu hũ xào rau củ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/maxresdefault.jpg?alt=media&token=4afcc7e5-7314-4946-8e26-34dd934a4be8', 36000.0000, 55000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'31b2a522-cb8d-4cbf-945a-8f63b9fcec84', N'Cách làm cá nấu măng chua ớt ngon miễn chê
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ca-nau-mang-chua-16x9.jpg?alt=media&token=d55cb81e-1d8a-4ae1-96be-ca8619dadedd', 59000.0000, 67000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'2dbbd555-d4b9-49dd-bacb-926c10ba8a2a', N'Đậu phụ chiên sả ớt', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/MVRQ94ZtT4OzzpfsP8IF_10.duahuchienxa-large%402x.jpg.jpg?alt=media&token=7a4b1c50-f7a5-40df-81e9-6a3d23b6c96b', 26000.0000, 34000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'16966619-f7cf-43eb-b758-953e95e81532', N'Cách nấu thịt bò rim miền Trung
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bo-rim-2.jpg?alt=media&token=97e96f0e-20c6-4ec1-9055-a301d5b03369', 80000.0000, 100000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'ad4f47e4-9408-49cd-b1d7-964225c1818d', N'Cách nấu canh nấm kim châm thơm ngon bổ dưỡng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-nam-kim-cham-4x3%20(1).jpg?alt=media&token=c57e3fd0-98d4-4906-9ac0-9091c63a018a', 42000.0000, 58000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'705a19e7-1369-4f33-975c-97f800b66919', N'Cách làm bánh mì nguyên cám bằng nồi chiên không dầu', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thanh-pham-363.jpg?alt=media&token=d31dbf0e-fbcc-439a-9fb2-002ec9004723', 34000.0000, 50000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'48ca1508-2569-431b-8c39-9ac18939a1ed', N'Cách làm nem rán miền Bắc giòn rụm, vàng đều, không bị ỉu
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-nem-ran-6.jpg?alt=media&token=266a42ed-1ccc-486e-ae37-e6cfb61bc864', 60000.0000, 70000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'f0a76a43-20e9-4009-b368-9de37a4ecdfb', N'Các cách nấu canh súp lơ ngon đủ vị
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-sup-lo-5x7.jpg?alt=media&token=e58929b5-3558-47d1-a353-a548937c2f3f', 59000.0000, 67000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'1372940f-1d98-44f5-979d-a016c22243a5', N'Cách làm bánh tráng cuốn thịt heo chấm mắm nêm', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/banh-trang-cuon-thit-heo-16x9.jpg?alt=media&token=beab6c49-b93e-49e7-8d31-9b42b5df59fd', 37000.0000, 50000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'11a65e68-ca66-4144-b10d-a01cb8f01bab', N'Cách làm bê tái chanh ngon như nhà hàng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/tron-be-tai-chanh.jpg?alt=media&token=1dbcd8c4-76e5-4788-a63d-9852ca0a06d7', 87000.0000, 99000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'7975d2a0-c6f4-4654-b94f-a217c4ea6ada', N'Cách nấu canh chua cá lóc miền Tây Nam Bộ
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-chua-ca-loc-5x7.jpg?alt=media&token=f2216489-0040-4e67-825e-4c246cd34fb3', 58000.0000, 68000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'5777c918-0acd-4e19-86dd-a8ebd77a69cb', N'Cách làm mực xào sa tế siêu ngon mà đơn giản
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/muc-xao-sa-te-4x3.jpg?alt=media&token=9b51f4a9-3429-42b0-a100-4da35819fdb3', 44000.0000, 65000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'e4dc6231-ebe7-4e52-aa5a-abe4c2488cc3', N'Thịt Bò Xào Bắp Non', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/recipe12572-635635810083497899.jpg?alt=media&token=144464ff-9d89-4bf3-8ee7-68734f12917a', 45000.0000, 56000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'349adefc-32d8-4dd8-be8f-aebb3f32b6cb', N'Muesli & Greek Yoghurt (Sữa Chua Hy Lạp) Kèm Quả Mọng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/muesli-greek-yoghurt-sữa-chua-hy-lạp-kem-quả-mọng-an-sang-diet-nhanh-gọn-healthy-recipe-main-photo.webp?alt=media&token=0ac971aa-e061-40a1-92bb-61a22a368265', 32000.0000, 46000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'b891b591-f775-499e-b759-aefdf47c0a84', N'Cách làm cá hấp bia ngon, ăn không tanh, ngon khó cưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ca-hap-bia-4x3.jpg?alt=media&token=121a4cf9-9b3f-40cf-8293-93af2fdaef84', 34000.0000, 50000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'a494dac7-d09a-4d4a-bec2-af10fd702d87', N'Cách nấu thịt kho tàu ngon bá cháy
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thit-kho-tau-4x3.jpg?alt=media&token=c6c02404-328f-40a9-bf13-2112c93d8b33', 58000.0000, 69000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'f3232eb3-1c98-4dbd-8d10-b03f58927a71', N'Cách nấu thịt đông miền Bắc ngon, vị thanh không ngấy
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thit-dong-4x3.jpg?alt=media&token=6ca8fd4d-0b68-4cbc-b29d-170c33c7afe9', 78000.0000, 93000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'ff0c4b6d-b5a3-4b83-82dd-b541986f1550', N'Muesli & Greek Yoghurt (Sữa Chua Hy Lạp) Kèm Quả Mọng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/muesli-greek-yoghurt-sữa-chua-hy-lạp-kem-quả-mọng-an-sang-diet-nhanh-gọn-healthy-recipe-main-photo.webp?alt=media&token=0ac971aa-e061-40a1-92bb-61a22a368265', 32000.0000, 46000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'bffd0e26-85a2-4b71-95f8-b565ec79f956', N'Đậu phụ chiên sả ớt', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/MVRQ94ZtT4OzzpfsP8IF_10.duahuchienxa-large%402x.jpg.jpg?alt=media&token=7a4b1c50-f7a5-40df-81e9-6a3d23b6c96b', 26000.0000, 34000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'a6c08997-114b-456f-a815-b68e2a5f9745', N'Đậu hũ xào rau củ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/maxresdefault.jpg?alt=media&token=4afcc7e5-7314-4946-8e26-34dd934a4be8', 36000.0000, 55000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'861f611f-ad13-436b-8d30-b6bfd5f9e6ac', N'Cá hồi nướng chanh', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/Untitled-1-1200x676-10-1024x526.jpg?alt=media&token=73021885-d554-4c40-ae23-559350f087eb', 63000.0000, 80000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'62642194-a143-480f-9f41-b84505d548bb', N'Đậu phụ sốt cà chua', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-dau-sot-ca-chua-2.jpg?alt=media&token=21be2a2d-a8be-40c5-acf9-a338584d1f39', 67000.0000, 87000.0000, 4, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'dfd04bc8-8286-4e9d-8964-b8470ce67ff5', N'Cách làm bún chả Hà Nội ngon như ngoài hàng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bun-cha-ha-noi.jpg?alt=media&token=fa8df759-3b3d-40d2-b7e6-d16ff0c20601', 67000.0000, 78000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'16c2a414-7a46-48bb-998c-bb5e73e16f4e', N'Cách làm mực xào sa tế siêu ngon mà đơn giản
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/muc-xao-sa-te-4x3.jpg?alt=media&token=9b51f4a9-3429-42b0-a100-4da35819fdb3', 44000.0000, 65000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'a9d116cc-6a7b-4022-81ad-bdcd154b416e', N'Cách làm gà nấu xáo ăn bún thơm nức mũi
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/xao-ga-16x9.jpg?alt=media&token=efc288e5-9ec5-4464-8604-0b6924428a4a', 67000.0000, 83000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'6e755350-6246-4b62-8547-c3164e97c756', N'Cá hồi nướng chanh', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/Untitled-1-1200x676-10-1024x526.jpg?alt=media&token=73021885-d554-4c40-ae23-559350f087eb', 63000.0000, 80000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'e33cfe76-d7fe-472e-832a-c517c9f86b65', N'Cách làm món sườn kho thơm lạ miệng, ngon quên sầu
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/suon-kho-thom-16x9.jpg?alt=media&token=29e587e0-55b8-4e31-803e-e752dab2fba5', 59000.0000, 67000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'3456764c-3c95-4b4e-8b46-c564ae9cb9bf', N'Cách làm mì xào bò đơn giản mà ngon, đủ dinh dưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-mi-xao-bo-ngon.jpg?alt=media&token=44878e3d-67b5-4ae5-acbb-3c783ec381fc', 34000.0000, 45000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'b5abba73-9cd5-4fff-83ba-c84373351551', N'Cách nấu canh nấm kim châm thơm ngon bổ dưỡng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-nam-kim-cham-4x3%20(1).jpg?alt=media&token=c57e3fd0-98d4-4906-9ac0-9091c63a018a', 42000.0000, 58000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'92ecfaf5-745d-451f-9c6c-c8a95cc8b607', N'Ức Gà Cuộn Rong Biển ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/photo%20(1).webp?alt=media&token=8909fd43-2735-4561-aa95-de44ca929df5', 42000.0000, 55000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'cdce7aa7-cfa7-48f4-ba8f-cab4c979b03d', N'Cách làm sườn xào chua ngọt trong 30 phút (cách mới)
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-suon-xao-chua-ngot-mien-bac.jpg?alt=media&token=f31781f0-a238-4171-b321-636844f23e84', 45000.0000, 60000.0000, 4, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'75bc99c6-006a-4eb6-9945-cea4a53fe118', N'Cách làm bánh tráng cuốn thịt heo chấm mắm nêm', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/banh-trang-cuon-thit-heo-16x9.jpg?alt=media&token=beab6c49-b93e-49e7-8d31-9b42b5df59fd', 37000.0000, 50000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'7fb631b1-abfc-4acd-b1f7-d0651a30b6b1', N'Cách nấu bao tử hầm tiêu xanh 10 điểm chất lượng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/bao-tu-ham-tieu-4x3.jpg?alt=media&token=0ccf36cf-56ab-4db3-99a0-8f7ab0c5c364', 45000.0000, 60000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'07830456-3663-4056-b356-d12074df4d78', N'Cách làm gà nấu xáo ăn bún thơm nức mũi
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/xao-ga-16x9.jpg?alt=media&token=efc288e5-9ec5-4464-8604-0b6924428a4a', 67000.0000, 83000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'e9e57429-7510-4fbf-9ed8-d33812552bc7', N'Cách làm món sườn kho thơm lạ miệng, ngon quên sầu
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/suon-kho-thom-16x9.jpg?alt=media&token=29e587e0-55b8-4e31-803e-e752dab2fba5', 59000.0000, 67000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'fa686511-092a-4f50-9683-d4623bc5eb0e', N'Cách làm cút lộn xào me ngon khó cưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/dia-cut-lon-xao-me.jpg?alt=media&token=76243db4-b562-4499-bb2c-6db8bfca2c3a', 12000.0000, 25000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'3f1aa816-496b-4ae0-a90f-d6570d525ec9', N'Giá xào đậu hũ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thanh-pham-1142.jpg?alt=media&token=21452719-5105-4e9f-ba41-7473360bbec1', 23000.0000, 32000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'93fe0cbf-0070-491f-9474-d7541387c6d7', N'Cách làm sữa chuối Hàn Quốc thơm ngon, bổ dưỡng đơn giản', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/3-cach-lam-sua-chuoi-thom-ngon-bo-duong-don-gian--14-760x367.jpg?alt=media&token=8885e56b-8937-4097-89c8-e58f8612dd30', 26000.0000, 36000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'20fc3a6b-5c35-48ff-883b-d7cc0de47bf1', N'Cách làm cá hấp bia ngon, ăn không tanh, ngon khó cưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ca-hap-bia-4x3.jpg?alt=media&token=121a4cf9-9b3f-40cf-8293-93af2fdaef84', 34000.0000, 50000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'8f342926-3676-432e-a772-d805c3c1c359', N'Cách nấu canh nấm kim châm thơm ngon bổ dưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-nam-kim-cham-4x3.jpg?alt=media&token=8fdb2ed5-1da8-4bbe-acce-d8dace77d4c7', 45000.0000, 60000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'3d33d012-ce32-4645-bc85-d98b7644a87a', N'Ức gà nướng mật ong balsamic rất tốt cho người ăn giảm cân!', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ức-ga-nướng-mật-ong-balsamic-rất-tốt-cho-người-an-giảm-can-recipe-main-photo.webp?alt=media&token=9b4c6117-616b-4da5-9bf6-70d19c394b3f', 56000.0000, 78000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'eab413d5-1ea5-4585-8758-dbebe6cffb49', N'Cách nấu thịt kho tàu ngon bá cháy
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thit-kho-tau-4x3.jpg?alt=media&token=c6c02404-328f-40a9-bf13-2112c93d8b33', 58000.0000, 69000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'8b169908-ef75-4854-a693-dca0e6cba7e3', N'Inarizushi gạo lứt', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/photo.webp?alt=media&token=eb96dd07-5ddd-4960-a034-d06cfaf4aaf1', 34000.0000, 50000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'1208f873-d1f7-490f-b001-e255d30b8ac9', N'Inarizushi gạo lứt', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/photo.webp?alt=media&token=eb96dd07-5ddd-4960-a034-d06cfaf4aaf1', 34000.0000, 50000.0000, 2, 2, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'f0831775-a7a9-4e45-aedd-e3ae1590e548', N'Chả giò chay', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/staticFood1.png?alt=media&token=c9f539b2-d25c-4a43-a759-6697dc8056ba', 25000.0000, 35000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'470187ef-15a4-4559-86f3-e4a7e0f26d93', N'Cách nấu canh nấm kim châm thơm ngon bổ dưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-nam-kim-cham-4x3.jpg?alt=media&token=8fdb2ed5-1da8-4bbe-acce-d8dace77d4c7', 45000.0000, 60000.0000, 4, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'52a78c72-92d6-4d84-8762-ec282b185105', N'Cách làm nem rán miền Bắc giòn rụm, vàng đều, không bị ỉu
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-nem-ran-6.jpg?alt=media&token=266a42ed-1ccc-486e-ae37-e6cfb61bc864', 60000.0000, 70000.0000, 4, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'45ff1006-52be-49b2-8e58-ef33f0e5111e', N'Thịt Bò Xào Bắp Non', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/recipe12572-635635810083497899.jpg?alt=media&token=144464ff-9d89-4bf3-8ee7-68734f12917a', 45000.0000, 56000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'9d442fa4-ea4d-4cfe-ae30-f20a635d9cd0', N'Cách làm cá nấu măng chua ớt ngon miễn chê
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ca-nau-mang-chua-16x9.jpg?alt=media&token=d55cb81e-1d8a-4ae1-96be-ca8619dadedd', 59000.0000, 67000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'5bc8d213-4717-4500-adf3-f44a131cb617', N'Cách làm xôi xéo Hà Nội ngon chuẩn vị xôi xéo Phú Thượng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/xoi-xeo-16x9.jpg?alt=media&token=d545ee4a-1f79-4d97-a3df-901d4e729cfa', 50000.0000, 64000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'e73d912a-2482-42c1-b591-f5bdb3e8653a', N'Cách làm salad ức gà sốt mè rang chua chua ngọt ngọt cực ngon
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/cach-lam-salad-uc-ga-sot-me-rang-chua-chua-ngot-ngot-cuc-ngon-avt-1200x676-1.jpg?alt=media&token=8d56bebf-7682-4b7f-9710-093648727883', 38000.0000, 49000.0000, 2, 4, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'56347797-102a-4720-a6a8-fa58ae1d464b', N'Cách làm salad ức gà sốt mè rang chua chua ngọt ngọt cực ngon
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/cach-lam-salad-uc-ga-sot-me-rang-chua-chua-ngot-ngot-cuc-ngon-avt-1200x676-1.jpg?alt=media&token=8d56bebf-7682-4b7f-9710-093648727883', 38000.0000, 49000.0000, NULL, NULL, NULL, 1)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [totalKcal], [status]) VALUES (N'60487557-92a5-4132-afe2-fb00a5d7e238', N'Cách nấu canh dưa bò thơm ngon, đưa cơm, ăn không bị ngán
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-dua-bo-4x5.jpg?alt=media&token=f22b45ed-4697-49ed-8a26-44fd4aeed345', 59000.0000, 67000.0000, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity]) VALUES (N'57448a79-8855-42ad-bd2e-0295d1436037', N'b5e83e15-3916-4133-8471-392edf0ba205', N'Ahihi do ngoc1', NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity]) VALUES (N'57448a79-8855-42ad-bd2e-0295d1436037', N'aa24a3f3-9086-45b5-9b93-7cd3201d0f89', N'Ahihi do ngoc2', NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity]) VALUES (N'57448a79-8855-42ad-bd2e-0295d1436037', N'd63ba734-0930-495d-9571-afa6d6f59fe0', N'Ahihi do ngoc3', NULL)
INSERT [dbo].[RecipeDetail] ([recipeId], [ingredientId], [description], [quantity]) VALUES (N'57448a79-8855-42ad-bd2e-0295d1436037', N'467f66ec-354a-4cae-948c-c327bf41a54d', N'Ahihi do ngoc4', NULL)
GO
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'93a460cc-f092-447f-aaf5-0564cd0ffadc', N'Giảm cân', N'Giảm cân', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'433b682e-3651-4f4b-b688-0eaf344c51bd')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'3bc469f2-df84-491f-a64a-119400614291', N'Bún', N'Bún', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'3b8ab3d0-08df-47f1-bf9f-ecf895fd0daa')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'a7766c2a-8faf-48f9-bdec-1dfaba22ecf3', N'Miền Bắc', N'Miền Bắc', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'14465782-6f37-4a8f-8d5e-7a0bea7f3b4c')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'6e5375f0-b603-49da-ace5-262dcace546f', N'Trẻ em', N'Trẻ em', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'63db7259-a32a-4a5e-beb8-985f3db5f63f')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'85276dc8-058d-4938-b38a-281e7276252c', N'Thịt heo', N'Thịt heo', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 0, N'3fa85f64-5717-4562-b3fc-2c963f66afa1')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'76bc7165-d0be-4d32-b819-434dfaeba2ad', N'Món kho', N'Món kho', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'8033733e-b4d5-4dfb-8316-e66481022cf2')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'c368e82d-dda4-4b39-bc19-43ef148981e0', N'Miền Trung', N'Miền Trung', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'14465782-6f37-4a8f-8d5e-7a0bea7f3b4c')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'8c6d0b63-74d9-4931-9c2a-4b7c8b54ed0e', N'Bữa sáng', N'Bữa sáng', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'e5c95ca2-d0fb-4002-8c0f-da4b39312730')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'df9bbf2f-b1c4-4c3d-864b-4c96c37a0b39', N'Thực đơn 1', N'Thực đơn 1', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'2d80def2-0135-4373-a4e6-2b15fc0166b6')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'7a13a207-91b5-4e2f-8d6f-4face2528a57', N'Thực đơn 2 ', N'Thực đơn 2', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'2d80def2-0135-4373-a4e6-2b15fc0166b6')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'08833a38-356f-473a-aca3-6e10469e21e2', N'Thịt gà', N'Thịt gà', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 0, N'3fa85f64-5717-4562-b3fc-2c963f66afa1')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'507f7e22-d5b4-4755-affe-813bf0280e14', N'Thịt bò', N'Thịt bò', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 0, N'3fa85f64-5717-4562-b3fc-2c963f66afa1')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'8519d30b-dfa2-45e0-9541-963e11886015', N'Món xào', N'Món xào', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'8033733e-b4d5-4dfb-8316-e66481022cf2')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'ad4f47e4-9408-49cd-b1d7-964225c1818d', N'Thực đơn 3', N'Thực đơn 3', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'2d80def2-0135-4373-a4e6-2b15fc0166b6')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'8b720ab2-25de-4aae-bef2-a3f780071386', N'Bữa trưa', N'Bữa trưa', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'e5c95ca2-d0fb-4002-8c0f-da4b39312730')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'13e8010c-f583-4a32-9a76-a7f188983c69', N'Miền Nam', N'Miền Nam', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'14465782-6f37-4a8f-8d5e-7a0bea7f3b4c')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'c72749fc-917a-469d-b17e-c5c934c350f4', N'Món chay', N'Món chay', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'433b682e-3651-4f4b-b688-0eaf344c51bd')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'cd704511-e8fa-4083-a847-c667eb8eece2', N'Dễ làm', N'Dễ làm', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'b3648659-6a2a-4795-80ea-b9f987d88b03')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'9747494f-5a51-4056-bef5-c6c03305ee5c', N'Món canh', N'Món canh', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'8033733e-b4d5-4dfb-8316-e66481022cf2')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'715ac265-370a-46a5-b8bd-c8b415b6880f', N'Thịt cá', N'Thịt cá', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 0, N'3fa85f64-5717-4562-b3fc-2c963f66afa1')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'e23f1a4a-f82c-41e3-9305-d50f77566808', N'Eat clean', N'Eat clean', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'433b682e-3651-4f4b-b688-0eaf344c51bd')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'f00ab7c8-f31d-422e-b295-e990e2074f50', N'Thịt vịt', N'Thịt vịt', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 0, N'3fa85f64-5717-4562-b3fc-2c963f66afa1')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'44d5227c-d10d-465e-a509-f70e469143ea', N'Bữa tối', N'Bữa tối', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'e5c95ca2-d0fb-4002-8c0f-da4b39312730')
GO
INSERT [dbo].[Type] ([typeId], [name], [description], [status]) VALUES (N'e0c1dc5d-e2c3-4535-a4a9-089364825791', N'Ahihi 1', N'dksadkasmdkamskdmaskdmka smkw ', 1)
INSERT [dbo].[Type] ([typeId], [name], [description], [status]) VALUES (N'c9dfbe87-cce1-41af-8ca8-36892ddf4ba9', N'Ahihi 2', N'dksadkasmdkamskdmaskdmka smkw ', 1)
INSERT [dbo].[Type] ([typeId], [name], [description], [status]) VALUES (N'479bd409-874b-4373-aa76-655430732a21', N'Ahihi 3', N'dksadkasmdkamskdmaskdmka smkw ', 1)
INSERT [dbo].[Type] ([typeId], [name], [description], [status]) VALUES (N'4e32c7c9-aec9-40ff-9606-8db795ddbb92', N'Ahihi 4', N'dksadkasmdkamskdmaskdmka smkw ', 1)
INSERT [dbo].[Type] ([typeId], [name], [description], [status]) VALUES (N'd5d7a371-9527-427b-93d9-bc8a20699928', N'Ahihi 5', N'dksadkasmdkamskdmaskdmka smkw ', 1)
INSERT [dbo].[Type] ([typeId], [name], [description], [status]) VALUES (N'6ffc824d-f539-4abe-a08c-be6ccb124d0a', N'Ahihi 6', N'dksadkasmdkamskdmaskdmka smkw ', 1)
INSERT [dbo].[Type] ([typeId], [name], [description], [status]) VALUES (N'58ae6148-db55-4021-8108-ccf31c4dd086', N'Ahihi 7', N'dksadkasmdkamskdmaskdmka smkw ', 1)
GO
INSERT [dbo].[Unit] ([unitId], [name], [description], [createdDate], [status]) VALUES (N'dabc7178-e761-4748-b8f7-1ffc98dfb0c4', N'chai', N'string', NULL, 1)
INSERT [dbo].[Unit] ([unitId], [name], [description], [createdDate], [status]) VALUES (N'05c16c64-1994-4ac9-bb79-d5be4fcca460', N'gói', N'string', NULL, 1)
GO
INSERT [dbo].[User] ([userId], [displayname], [username], [firstname], [lastname], [email], [password], [phonenumber], [gender], [avatar], [role], [createdDate], [updatedDate], [isBlocked], [isGoogle]) VALUES (N'749f5b3b-dea1-49a4-98b8-96da197d123f', NULL, N'vanphuong0606', N'Phương', N'Võ Văn', N'vanphuong0606@gmail.com', N'kVU41twDyttUL/SM7IO0vQ==
', N'0971775169', 1, N'Nolink', 1, CAST(N'2022-12-31T23:12:44.720' AS DateTime), CAST(N'2022-12-31T23:12:44.720' AS DateTime), 0, 0)
INSERT [dbo].[User] ([userId], [displayname], [username], [firstname], [lastname], [email], [password], [phonenumber], [gender], [avatar], [role], [createdDate], [updatedDate], [isBlocked], [isGoogle]) VALUES (N'c1bd7421-a3d0-496a-a775-b307737777c1', NULL, N'monkeynam208', N' Nam', N'Nguyễn Lương Hoàng', N'monkeynam208@gmail.com', N'kVU41twDyttUL/SM7IO0vQ==
', N'0987603163', 1, NULL, 1, CAST(N'2022-12-31T23:12:44.720' AS DateTime), CAST(N'2022-12-31T23:12:44.720' AS DateTime), 0, 1)
GO
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrder], [maximumOrder], [authorId]) VALUES (N'00000000-0000-0000-0000-000000000000', N'Nạp lần đầu', N'string', 3, CAST(N'2023-01-13T22:46:45.213' AS DateTime), CAST(N'2023-01-14T00:08:19.010' AS DateTime), CAST(N'2023-01-14T00:08:19.010' AS DateTime), 15000.0000, 23000.0000, 150000.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrder], [maximumOrder], [authorId]) VALUES (N'8cf462b9-a323-4eca-bc01-2de9992a5a7d', N'Nạp lần đầu', N'No thing', 0, CAST(N'2023-01-13T22:46:45.213' AS DateTime), CAST(N'2023-01-13T22:46:45.213' AS DateTime), CAST(N'2023-01-13T22:46:45.213' AS DateTime), 15000.0000, 23000.0000, 150000.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrder], [maximumOrder], [authorId]) VALUES (N'dde34ee3-9dc6-46d0-830d-3128105eacad', N'Nạp lần đầu', N'No thing', 1, CAST(N'2023-01-14T00:08:19.180' AS DateTime), CAST(N'2023-01-14T00:08:19.180' AS DateTime), CAST(N'2023-01-14T00:08:19.180' AS DateTime), 15000.0000, 23000.0000, 150000.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrder], [maximumOrder], [authorId]) VALUES (N'9fb22d85-142a-407d-85e8-5034a38b732c', N'Nạp lần đầu', N'No thing', 1, CAST(N'2023-01-14T00:08:19.347' AS DateTime), CAST(N'2023-01-14T00:08:19.347' AS DateTime), CAST(N'2023-01-14T00:08:19.347' AS DateTime), 15000.0000, 23000.0000, 150000.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrder], [maximumOrder], [authorId]) VALUES (N'06a84c50-1960-42b5-ad76-6e3cf06d5269', N'Nạp lần đầu', N'No thing', 0, CAST(N'2023-01-14T00:08:19.010' AS DateTime), CAST(N'2023-01-14T00:08:19.010' AS DateTime), CAST(N'2023-01-14T00:08:19.010' AS DateTime), 15000.0000, 23000.0000, 150000.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrder], [maximumOrder], [authorId]) VALUES (N'c1340754-bbcc-42d1-a73a-a11e1057d1d3', N'string', N'string', 1, CAST(N'2023-01-14T22:22:10.663' AS DateTime), CAST(N'2023-01-14T15:21:56.610' AS DateTime), CAST(N'2023-01-14T15:21:56.610' AS DateTime), 0.0000, 0.0000, 22220.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
INSERT [dbo].[Voucher] ([voucherId], [name], [description], [status], [createdDate], [validFrom], [validTo], [discount], [minimumOrder], [maximumOrder], [authorId]) VALUES (N'af0cabc9-7247-4a76-87dd-dba77543fd0c', N'Nạp lần đầu', N'string', 3, CAST(N'2023-01-14T00:27:41.050' AS DateTime), CAST(N'2023-01-14T00:08:19.010' AS DateTime), CAST(N'2023-01-14T00:08:19.010' AS DateTime), 15000.0000, 23000.0000, 150000.0000, N'c1bd7421-a3d0-496a-a775-b307737777c1')
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
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Customer1] FOREIGN KEY([customerId])
REFERENCES [dbo].[Customer] ([customerId])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Customer1]
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD  CONSTRAINT [FK_CartDetail_Cart] FOREIGN KEY([cartId])
REFERENCES [dbo].[Cart] ([cartId])
GO
ALTER TABLE [dbo].[CartDetail] CHECK CONSTRAINT [FK_CartDetail_Cart]
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
ALTER TABLE [dbo].[CustomerReward]  WITH CHECK ADD  CONSTRAINT [FK_CustomerReward_Customer] FOREIGN KEY([customerId])
REFERENCES [dbo].[Customer] ([customerId])
GO
ALTER TABLE [dbo].[CustomerReward] CHECK CONSTRAINT [FK_CustomerReward_Customer]
GO
ALTER TABLE [dbo].[CustomerReward]  WITH CHECK ADD  CONSTRAINT [FK_CustomerReward_Reward] FOREIGN KEY([rewardId])
REFERENCES [dbo].[Reward] ([rewardId])
GO
ALTER TABLE [dbo].[CustomerReward] CHECK CONSTRAINT [FK_CustomerReward_Reward]
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
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Transaction1] FOREIGN KEY([orderId])
REFERENCES [dbo].[Transaction] ([transactionId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Transaction1]
GO
ALTER TABLE [dbo].[OrderCookedDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderCookedDetail_Order] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([orderId])
GO
ALTER TABLE [dbo].[OrderCookedDetail] CHECK CONSTRAINT [FK_OrderCookedDetail_Order]
GO
ALTER TABLE [dbo].[OrderCookedDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderCookedDetail_Recipe] FOREIGN KEY([recipeId])
REFERENCES [dbo].[Recipe] ([recipeId])
GO
ALTER TABLE [dbo].[OrderCookedDetail] CHECK CONSTRAINT [FK_OrderCookedDetail_Recipe]
GO
ALTER TABLE [dbo].[OrderIngredientDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderIngredientDetail_Ingredient] FOREIGN KEY([ingredientId])
REFERENCES [dbo].[Ingredient] ([ingredientId])
GO
ALTER TABLE [dbo].[OrderIngredientDetail] CHECK CONSTRAINT [FK_OrderIngredientDetail_Ingredient]
GO
ALTER TABLE [dbo].[OrderIngredientDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderIngredientDetail_Order] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([orderId])
GO
ALTER TABLE [dbo].[OrderIngredientDetail] CHECK CONSTRAINT [FK_OrderIngredientDetail_Order]
GO
ALTER TABLE [dbo].[OrderPackageDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderPackageDetail_Order] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([orderId])
GO
ALTER TABLE [dbo].[OrderPackageDetail] CHECK CONSTRAINT [FK_OrderPackageDetail_Order]
GO
ALTER TABLE [dbo].[OrderPackageDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderPackageDetail_Recipe] FOREIGN KEY([recipeId])
REFERENCES [dbo].[Recipe] ([recipeId])
GO
ALTER TABLE [dbo].[OrderPackageDetail] CHECK CONSTRAINT [FK_OrderPackageDetail_Recipe]
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
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_User] FOREIGN KEY([authorId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[Voucher] CHECK CONSTRAINT [FK_Voucher_User]
GO
USE [master]
GO
ALTER DATABASE [Homnayangi] SET  READ_WRITE 
GO
