USE [master]
GO
/****** Object:  Database [Homnayangi]    Script Date: 2/8/2023 10:13:02 PM ******/
CREATE DATABASE [Homnayangi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Homnayangi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Homnayangi.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Homnayangi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Homnayangi_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
ALTER DATABASE [Homnayangi] SET AUTO_CLOSE ON 
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
ALTER DATABASE [Homnayangi] SET RECOVERY SIMPLE 
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
ALTER DATABASE [Homnayangi] SET QUERY_STORE = OFF
GO
USE [Homnayangi]
GO
/****** Object:  Table [dbo].[Accomplishment]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[Blog]    Script Date: 2/8/2023 10:13:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[blogId] [uniqueidentifier] NOT NULL,
	[title] [nvarchar](max) NULL,
	[description] [nvarchar](max) NULL,
	[preparation] [nvarchar](max) NULL,
	[processing] [nvarchar](max) NULL,
	[finished] [nvarchar](max) NULL,
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
/****** Object:  Table [dbo].[BlogReaction]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[BlogSubCate]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[Comment]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[CustomerReward]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[CustomerVoucher]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[Ingredient]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[Notification]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[Order]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[OrderCookedDetail]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[OrderIngredientDetail]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[OrderPackageDetail]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[PriceNote]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[Recipe]    Script Date: 2/8/2023 10:13:02 PM ******/
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
	[status] [int] NULL,
 CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED 
(
	[recipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeDetail]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[Reward]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[SubCategory]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[Transaction]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[Type]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[Unit]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 2/8/2023 10:13:02 PM ******/
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
/****** Object:  Table [dbo].[Voucher]    Script Date: 2/8/2023 10:13:02 PM ******/
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
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'57448a79-8855-42ad-bd2e-0295d1436037', N'Cách làm bánh ngào mật mía Nghệ An
', N'Cách làm bánh ngào Nghệ An thơm phức mùi nếp, ấm nồng vị gừng và ngọt ngào của mật mía sẽ được chia sẻ chi tiết trong bài viết này. Đây là món bánh không quá cầu kỳ trong khâu thực hiện nhưng thành phẩm thì chắc chắn sẽ chinh phục được bạn ngay từ lần thưởng thức đầu tiên.

', N'200g bột nếp
1 củ gừng tươi ~ 50-70g
Muối hạt
Dầu ăn
Mật mía
Nước lọc
Vừng trắng hoặc vừng đen (nếu thích)', N'Ngoài ra, bánh ngào mật gồm có 2 loại, đó là bánh ngào chay và bánh ngào có nhân. Về phần nhân bánh ngào khá đa dạng, bánh có thể dùng nhân đậu xanh, nhân lạc, nhân gừng hoặc có nơi còn làm nhân thịt quay...

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/banh-ngao-4x5.jpg?alt=media&token=6ed4044c-d6eb-41d6-9adc-0092b28b400b', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 15, 44, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'f5f5f2d8-1ae7-4bb9-b135-6dfad9b39988')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'e8a73c51-7506-4e0a-b801-05175f95b70e', N'Cách nấu vịt om sấu miền Bắc với khoai sọ ngon như ngoài hàng', N'Cách làm vịt om sấu miền Bắc với khoai sọ thơm ngon, chuẩn vị. Khi nấu món ăn này việc khó nhất là làm sao cho nước vịt om sấu được trong, không bị đen và có vị chua dịu thanh mát. ', N'Vịt: 1 con khoảng 1,5kg
Sấu: 10 quả
Khoai sọ: 4-5 củ nhỏ
Sả: 5-6 nhánh
Hành, tỏi, ớt
Rau mùi tàu, hành lá, rau ngổ
Muối hạt
Chanh
Rượu gừng
Dừa: 1 quả
Rau thơm, bún ăn kèm', N'Vịt có thể nhờ người bán cắt tiết, vặt lông và mổ sạch sẽ, sau đó về rửa sạch lại. Xát muối hạt, rượu gừng và chanh tươi để thịt vịt đỡ hôi và sạch sẽ hơn. Hoặc nếu có lá na, bạn có thể vò 1 nắm lá na và xát quanh thân vịt rồi rửa sạch lại, sau đó mang đi chặt thành miếng vừa ăn.', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/vit-om-sau-4x3.jpg?alt=media&token=09204f09-69f6-4827-a7f5-81b42ad401d3', CAST(N'2022-12-31T23:17:20.830' AS DateTime), CAST(N'2022-12-31T23:17:20.830' AS DateTime), 12, 42, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'cfaa3f8b-f7b7-405f-a01d-206a7d87dc32')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'fa1cdf34-652d-4e8e-8baf-19917d31772a', N'Cách nấu canh khoai mỡ trắng với tôm tươi ngon bổ
', N'Canh khoai mỡ có vị ngọt mát, thơm ngậy, kết hợp nấu với tôm tươi là món canh ngon và rất bổ dưỡng cho sức khỏe, đặc biệt tốt cho người có các bệnh liên quan đến tim mạch, lưu thông đường huyết, nhuận tràng hay béo phì...

', N'Khoai mỡ trắng: 1 củ 800g
Tôm tươi: 350g
Hành khô: 3 củ
Hành lá
Mùi tàu
Nước mắm
Bột canh
Bột ngọt hoặc hoặc bột nêm', N'Phần thân tôm băm nhỏ, không nên băm quá nhuyễn, sau đó ướp cùng với 1 ít bột canh. Đảo đều và ướp tôm khoảng 5-10 phút để tôm thấm gia vị.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-khoai-mo-16x9.jpg?alt=media&token=99b5cc05-7df6-42f8-892f-f7c274ad23d1', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 11, 43, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'b8c494c3-bebb-4347-8b99-47c1f02009b3')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'f8c8138f-4e79-4a83-b891-28b2062ad382', N'Spaghetti bún gạo lức ăn kèm cải bó xôi', N'Spaghetti bún gạo lức ăn kèm cải bó xôi', N'Bún gạo lức
Thịt bò hoặc heo bằm
Hành tây
Cà chua bằm
Cải bó xôi', N'Cho dầu olive vào chảo, đợi nóng dầu thì cho hành tây, cà chua vào. Đợi cà chua hơi nhừ thì cho tiếp thịt vào xào đến khi thịt chín. Nếu thấy khô quá có thể cho tí nước vào. Sau đó nêm nếm phần gia vị', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/bun-xao.webp?alt=media&token=5a5a7b30-26fc-4926-b520-82d804fd7596', CAST(N'2022-12-30T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 43, 67, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'9e343cac-2e68-48fc-8d71-87f929e9b8ce')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'404debd8-422f-42e1-9431-2f1f2101777a', N'Cách làm bún chả Hà Nội ngon như ngoài hàng', N'Chia sẻ chi tiết công thức và cách làm bún chả Hà Nội, từ cách ướp thịt nướng bún chả đến cách làm nước chấm bún chả ngon không khác gì các cửa hàng nổi tiếng.

', N'1,5kg bún tươi
300g thịt băm
300g thịt ba chỉ (ba rọi hoặc nạc vai)
30g mỡ phần xay nhuyễn
Muối hạt
Gia vị ướp: Dầu hào, dầu ăn, tiêu xay, nước mắm, nước màu, mật ong, bột ngọt, ớt bột không cay, bột húng lìu (nếu có)
2 củ tỏi
4 củ hành khô
', N'Thịt băm cho vào âu lớn cùng với mỡ phần xay nhuyễn, ướp với 1 thìa canh dầu ăn, 1 thìa canh nước màu, 1/3 thìa canh bột ngọt, 1 thìa canh dầu hào, 1/2 thìa canh mật ong, 1/2 thìa canh tiêu xay, 1/2 thìa canh nước mắm và 1/3 thìa cà phê bột húng lìu (hoặc bột quế). Băm thật nhuyễn 2 củ hành khô, 4-5 tép tỏi, 1 ít sả cho vào trộn đều và để thịt thấm gia vị ít nhất khoảng 30 phút.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bun-cha-ha-noi.jpg?alt=media&token=fa8df759-3b3d-40d2-b7e6-d16ff0c20601', CAST(N'2022-12-31T23:18:14.923' AS DateTime), CAST(N'2022-12-31T23:18:14.923' AS DateTime), 23, 53, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'dfd04bc8-8286-4e9d-8964-b8470ce67ff5')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'899c2bd0-1244-478c-b88b-399886f1436f', N'Cách làm bí xanh xào thịt bò lạ miệng, ăn là ghiền
', N'Bí xanh xào thịt bò có vị ngọt thanh, rất dễ ăn. Món xào ngon này chế biến cũng rất đơn giản, không tốn quá nhiều thời gian.

', N'600-700g bí xanh
250g thịt bò
5 tép tỏi
1 đốt gừng
Muối hạt, rượu gừng
Hành lá
Gia vị: Dầu hào, bột canh, bột ngọt, hạt tiêu', N'Thịt bò bóp qua muối hạt và ít rượu gừng hoặc rượu trắng, rửa sạch để ráo rồi thái mỏng. Sau đó ướp thịt bò với 1/2 thìa cà phê bột canh, 1/2 thìa cà phê bột ngọt, 1 thìa cà phê dầu ăn, 1/2 thìa cà phê dầu hào, trộn đều. Để thịt bò ngấm gia vị ít nhất tầm 15 phút.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/bi-xanh-xao-thit-bo-5x7.jpg?alt=media&token=53f72188-9446-46d5-8607-7cff356b947b', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 6, 19, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'2346415c-0318-4892-9dfb-4ad6ae883d7c')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'da6f26fa-4ea8-4dc8-b42c-4d69dee37c9c', N'Cách nấu cháo thịt bò bí đỏ cho bé và cho cả người lớn
', N'Cháo thịt bò bí đỏ ngọt và thơm dịu, đặc biệt màu sắc và hương vị của nó rất hấp dẫn các bé nhỏ.

', N'300g thịt bò xay
1 quả bí đỏ ~ 400g
150g gạo tẻ
50g gạo nếp
1 đốt gừng
Hành lá
Rau mùi
Gia vị: Nước mắm, bột canh, bột ngọt, bột nêm', N'Thịt bò sau khi được sơ chế sạch sẽ, ướp cùng với 1 thìa cà phê dầu hào, 1 ít dầu ăn, 1/2 thìa cà phê bột nêm, 1/2 thìa cà phê bột canh, trộn đều và để thịt thấm gia vị ít nhất trong 30 phút.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/chao-thit-bo-bi-do-5x7.jpg?alt=media&token=89d4ccef-b375-4f74-a882-964b21a685e2', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 26, 54, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'8669fc4a-731d-404a-a291-4c4048045988')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'95389b16-fc8e-4db3-80d3-5ef9c49d4ede', N'Cách làm thịt kho mắm ruốc sả ớt thơm ngon đậm đà
', N'Để làm thịt kho mắm ruốc ngon, bạn cần mua được đúng mắm ruốc và phải có đường, sả và ớt. Món ăn này ăn với cơm trắng, kèm với khế chua, chuối xanh, rau thơm thì rất hao cơm.

', N'500g thịt ba rọi (thịt ba chỉ)
4 thìa canh mắm ruốc
5 nhánh sả
5-6 tép tỏi
3-4 củ hành khô
1 đốt gừng
1-2 quả ớt hiểm
1 nhánh hành lá', N'Thịt ba rọi bóp với muối hạt, rửa sạch rồi để ráo, thái con chì, có độ dày khoảng 1,5 phân. Để làm món thịt kho mắm ruốc, bạn không nên thái miếng quá to như thịt kho tàu. Chỉ cần thái miếng con chì vừa ăn, dễ thấm gia vị hơn.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-thit-kho-mam-ruoc.jpg?alt=media&token=eb268a7b-d515-4e57-bf49-754b58a8b896', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 13, 55, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'6551fc76-3aaa-4036-9d09-37f5429d5772')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'9e4c748b-01fc-438c-8cd0-61cf4bde58ff', N'Giá xào đậu hũ', N'Đơn giản bằng những thứ có thể tự trồng tại nhà là giá, chúng ta cũng có thể tạo thành 1 món ăn thơm ngon cho bữa cơm gia đình', N' Đậu hũ 1 đến 2 miếng, cà rốt, nấm đùi gà (nấm rơm), giá đỗ, gia vị: muối, xì dầu, hạt nêm chay hoặc bột ngọt, hành ba rô.', N'Sơ chế các nguyên liệu: giá, nấm ngâm với nước muối, rửa sạch để ráo nước. Cà rốt gọt vỏ rửa sạch. Đem nấm và cà rốt thái sợi vừa ăn.', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thanh-pham-1142.jpg?alt=media&token=21452719-5105-4e9f-ba41-7473360bbec1', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 32, 55, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'3f1aa816-496b-4ae0-a90f-d6570d525ec9')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'a6597311-ea7c-4f64-97c0-6b89951d73dc', N'Cách nấu bún cá dọc mùng kiểu miền Bắc ngon chuẩn vị
', N'Món bún cá với nguyên liệu chính là cá (tuỳ loại) thì cần các nguyên liệu chủ đạo như dọc mùng, rau cần, cà chua, nghệ tươi hoặc bột nghệ khô...

', N'600g thịt cá rô phi đã lọc
500g xương cá
1 củ gừng to
10 củ hành khô
1 củ nghệ tươi
5-6 quả cà chua
5-6 nhánh dọc mùng (bạc hà)
1 bó rau cần nước
Dấm bỗng
100g bột chiên giòn
10g bột sư tử (nếu có)
Hành lá, thì là
', N'Cá khi mua nhờ người bán cạo vẩy và lọc thịt sẵn, khi mang về thì bóp cùng muối hạt, 1 ít rượu gừng (nếu có) để làm sạch và khử mùi tanh. Sau đó rửa sạch, để ráo rồi thái lát chéo thành từng miếng có độ dày tầm 1 đốt ngón tay.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bun-ca.jpg?alt=media&token=9276f0ed-8d0a-44bb-a379-ca67922411b1', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 12, 42, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'd279301e-9cab-4333-9d12-2c28fc1b8484')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'b8b93f52-9396-4ee0-8031-6cd6c93fefe9', N'Cách nấu canh chua cá lóc miền Tây Nam Bộ
', N'Canh chua cá lóc là món ăn dân dã nhưng nếu đã một lần được thưởng thức sẽ khiến người ta cảm thấy nhớ mãi hương vị ngọt dịu của cá lóc, chua thanh của cà chua, dứa, trái bắp, chút giòn dai của bạc hà quyện với mùi thơm của rau ngổ, ngò và thêm chút cay nồng của ớt, bùi bùi của tỏi phi', N'Cá lóc: 1kg
Cà chua: 2 quả
Dứa (trái thơm): 1 quả
Bạc hà (dọc mùng): 2 nhánh
Đậu bắp: 4-5 trái
Giá đỗ: 100g
Me ngào: nửa bát con
Tỏi: 2 củ
Sả: 1 nhánh
Rau om (rau ngổ)
Ngò gai (rau mùi tàu)
', N'Đối với món canh chua cá lóc cho 3-4 người ăn, có thể chỉ dùng 3 khúc cá, phần còn lại có thể bảo quản trong tủ lạnh để chế biến cho lần nấu tiếp theo. Cá sau khi rửa sạch, cho vào tô ướp cùng với 1 chút bột canh và 1 ít ớt tươi băm nhỏ.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-chua-ca-loc-5x7.jpg?alt=media&token=f2216489-0040-4e67-825e-4c246cd34fb3', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 22, 45, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'7975d2a0-c6f4-4654-b94f-a217c4ea6ada')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'c41fcd43-ce4d-454b-9c43-6fcccc543632', N'Cách làm mì xào bò đơn giản mà ngon, đủ dinh dưỡng
', N'Mì xào bò là món ăn mà nhiều người yêu thích, nhất là khi muốn thay đổi những bữa cơm hàng ngày. Và vì có sử dụng thịt, kết hợp cùng với rau cải, cà rốt, hành tây và cà chua nên món ăn này rất dễ ăn, có độ tươi mát chứ không gây ngán.

', N'3 gói mì tôm
250g thịt bò
2 quả cà chua
1 bó rau cải
170g hành tây
100g cà rốt
1 củ tỏi ~ 8 tép tỏi
1 đốt gừng nhỏ
Hành lá, rau mùi ta
Gia vị: Nước mắm, xì dầu, bột ngọt, bột nêm, bột canh, dầu hào, đường, tiêu xay
', N'Với 300g thịt bò, mình ướp cùng với 1/2 thìa cà phê bột nêm, 1/2 thìa cà phê bột canh, 1/2 thìa cà phê bột ngọt, 1/2 thìa cà phê tiêu xay, 1 thìa cà phê dầu hào. Và để thịt bò mềm hơn và không bị ra nước khi xào, mình sẽ ướp cùng với 1/2 thìa cà phê đường và 2 thìa cà phê dầu ăn.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-mi-xao-bo-ngon.jpg?alt=media&token=44878e3d-67b5-4ae5-acbb-3c783ec381fc', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 23, 42, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'3456764c-3c95-4b4e-8b46-c564ae9cb9bf')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'7bf92d79-1c3b-4473-820d-7425de673d1c', N'Cách làm món cà pháo mắm tôm ngon, ăn là ghiền
', N'Cà pháo chấm mắm tôm là món ăn dân dã, chứa đựng cả bầu trời ký ức của rất nhiều người, đặc biệt đối với thế hệ 8x trở về trước.

', N'200g cà pháo
40ml mắm tôm
Rau diếp cá
Rau kinh giới
Rau húng quế
Dưa chuột
2-3 quả ớt cay
Đường trắng
Bột ngọt', N'Cà ngâm trong nước muối khoảng 15 phút, sau đó vớt ra, rửa qua. Tiếp tục pha thêm 1 chậu nước muối, rồi lại thả cà vào ngâm thêm 15 phút. Cứ thế ngâm cà và thay nước muối khoảng 3-4 lần.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-ca-phao-mam-tom-2.jpg?alt=media&token=58a58a25-6dba-4610-80de-68ca03d9bc9f', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 12, 33, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'cf04775c-51a9-4664-ae28-493318c3ab8b')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'80e34167-9504-4399-8b1c-755d19d79e5f', N'Cách làm sườn xào chua ngọt trong 30 phút (cách mới)
', N'Không giống những cách làm khác, cách làm sườn xào chua ngọt này không chiên sườn nên chỉ mất 30 phút để hoàn thành, sườn vẫn mềm và róc xương, thích hợp cho người bận rộn.

', N'700g sườn thăn
1-2 quả ớt
2 củ tỏi
Dầu màu điều
Dấm gạo
Nước màu cốt dừa
Muối hạt
Gia vị: Nước mắm, đường, bột ngọt, bột canh, hạt tiêu', N'Sườn chặt miếng vừa ăn, rửa sạch rồi mang đi chần qua. Đun sôi 1 nồi nước với 1 nhúm muối hạt. Khi nước sôi thì cho sườn vào chần khoảng 2-3 phút rồi vớt ra và rửa thật kỹ dưới vòi nước. Bước làm này giúp sườn sạch, khi nấu không bị lẫn xương lợn cợn và tiết ra nhiều bọt bẩn. Ngoài ra, việc rửa sườn dưới vòi nước cũng giúp miếng sườn mềm hơn.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-suon-xao-chua-ngot-mien-bac.jpg?alt=media&token=f31781f0-a238-4171-b321-636844f23e84', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 12, 35, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'cdce7aa7-cfa7-48f4-ba8f-cab4c979b03d')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'a793327a-55e0-453e-af63-75fb1466126b', N'Cách làm xôi xéo Hà Nội ngon chuẩn vị xôi xéo Phú Thượng
', N'Xôi xéo là món xôi ngon, dễ ăn và mang hương vị rất đặc trưng. Đây là món xôi gắn liền với ký ức của rất nhiều thế hệ người Hà Nội. Cách làm xôi xéo cầu kỳ nhưng lại không khó, ai cũng có thể nấu được ngay từ lần đầu tiên. ', N'500g nếp
100g đậu xanh
200g hành khô
700g mỡ gà
Muối hạt
Bột nghệ
2-3 nhánh lá dứa
', N'Nếp để nấu xôi ngon các bạn chọn nếp nhung, nếp cái hoa vàng...
Nếu không có mỡ gà, bạn có thể dùng mỡ lợn.
Cho thêm lá dứa giúp xôi thoảng thơm hơn, nếu không mua được cũng không quá ảnh hưởng đến hương vị của xôi. Tuy nhiên không nên cho quá nhiều lá dứa nếp, dễ làm xôi bị nồng mùi, át đi vị ngọt thơm của nếp.
Ngoài ruốc bông, để ăn cùng xôi xéo bạn có thể chuẩn bị thịt kho tàu, ít dưa góp dưa chuột...
', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/xoi-xeo-16x9.jpg?alt=media&token=d545ee4a-1f79-4d97-a3df-901d4e729cfa', CAST(N'2022-12-31T23:17:36.097' AS DateTime), CAST(N'2022-12-31T23:17:36.097' AS DateTime), 21, 34, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'5bc8d213-4717-4500-adf3-f44a131cb617')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'2b0815b4-a000-4e18-8cce-864ef07886f6', N'Đậu phụ sốt cà chua', N'Một món ăn rất dễ dàng và có cách nấu khá đa dạng từ Bắc vào Nam. Dễ dàng và phù hợp với những người ăn chay', N' Đậu phụ 2 bìa, 2 quả cà chua, nước sôi, xì dầu, hành băm, dầu ăn, gia vị: đường, tiêu, ớt bột, tương cà', N'Cho thêm nước vào chảo và đun đến khi xôi. Nêm ít gia vị cho vừa khẩu vị và đổ đậu phụ đã chiên vào nước sốt. Tiếp tục đun cho đậu phụ thấm gia vị thì tắt bếp.', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-dau-sot-ca-chua-2.jpg?alt=media&token=21be2a2d-a8be-40c5-acf9-a338584d1f39', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 12, 55, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'62642194-a143-480f-9f41-b84505d548bb')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'da109a7c-f359-494d-bc95-8854cefc223a', N'Ức gà nướng mật ong balsamic rất tốt cho người ăn giảm cân!', N'Ức gà nướng mật ong balsamic rất tốt cho người ăn giảm cân!', N'1/4 Chén dầu ô liu
3 muỗng canh mật ong 🍯
3 muỗng canh giấm balsamic
1 muỗng canh băm nhỏ, hương thảo tươi 🥬
1 tép tỏi, băm nhỏ 🧄
4 ức gà không xương, không da🍗
Muối và tiêu 🧂', N'Chuẩn bị lò nướng để nấu, khoảng 120 - 160 độ.
', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ức-ga-nướng-mật-ong-balsamic-rất-tốt-cho-người-an-giảm-can-recipe-main-photo.webp?alt=media&token=9b4c6117-616b-4da5-9bf6-70d19c394b3f', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 34, 67, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'3d33d012-ce32-4645-bc85-d98b7644a87a')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'930fc740-7297-4235-a77d-8e9f0bf34806', N'Ức Gà Cuộn Rong Biển ', N'Ức Gà Cuộn Rong Biển ', N'Ức gà
Rong biển
Nước tương
Tiêu
Kim chi
Gia vị Ướp của Ý', N'Cắt ức ra từng thanh nhỏ vừa ăn, rong biển lá cắt thành 3 phần rồi cuốn lại.
Bạn có thể cuốn nhiều vòng nếu thích ăn nhiều rong biển.
Gà rim thì mềm, ăn kèm với kimchi sẽ không bị ngán', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ga-cuon-rong-bien.webp?alt=media&token=993690e3-eeef-4c70-a6ba-f0894c470012', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 34, 65, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'92ecfaf5-745d-451f-9c6c-c8a95cc8b607')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'9c885816-32f0-455a-bc22-8f5365cd722f', N'Đậu hũ xào rau củ', N'Đậu phụ kết hợp với rau củ quả là sự kết nối tuyệt vời', N' 1 miếng đậu hũ, ớt chuông xanh/ vàng/ đỏ, nấm đông cô, hành tây 1 củ, mè rang, nước tương, dầu mè, dầu ăn, đường, muối.', N'Lấy chảo chiên đậu cho nấm vào xào với lửa lớn sau đó để phần ớt chuông và hành tây vào xào với lửa vừa. Cuối cùng cho đậu phụ vào xào, nêm gia vị muối, đường, nước tương, dầu mè, mè rang là hoàn thành.', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/maxresdefault.jpg?alt=media&token=4afcc7e5-7314-4946-8e26-34dd934a4be8', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 23, 44, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'a6c08997-114b-456f-a815-b68e2a5f9745')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'2dbbd555-d4b9-49dd-bacb-926c10ba8a2a', N'Đậu phụ chiên sả ớt', N'Đậu phụ rất ngon và phù hợp cho người ăn chay', N'3 miếng đậu hũ, 1 củ tỏi, 2 cây sả, gia vị (đường, hạt nêm), 2 trái ớt, đường, xì dầu, dầu ăn', N'Bắt chảo chống dính lên bếp gas, đun nóng dầu ăn để chiên đậu phụ. Sau khi đậu phụ chín vàng hai mặt thì để ra đĩa. Bạn có thể sử dụng giấy thấm dầu để loại bỏ bớt chất béo. Hoặc bạn có thể dụng nồi chiên không dầu để thay thế phương pháp chiên này.', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/MVRQ94ZtT4OzzpfsP8IF_10.duahuchienxa-large%402x.jpg.jpg?alt=media&token=7a4b1c50-f7a5-40df-81e9-6a3d23b6c96b', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 13, 54, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'bffd0e26-85a2-4b71-95f8-b565ec79f956')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'16966619-f7cf-43eb-b758-953e95e81532', N'Cách nấu thịt bò rim miền Trung
', N'Thịt bò rim là món ăn ngon nổi tiếng ở Hà Tĩnh, thường được chế biến trong những ngày lễ Tết và những dịp đặc biệt. Thịt bò mềm không khô thơm lừng mùi hoa hồi, quế, trần bì cùng với vị ngọt nhẹ của mật mía/mật ong dùng ăn với cơm trắng hoặc bánh chưng đều ngon.

', N'1kg bắp bò (loại nhiều gân)
1/2 chén nước mắm nhỏ
1/2 chén mật ong hoặc mật mía
2 đốt gừng
1 củ hành tím
1 củ tỏi
5 hoa hồi
2 nhánh quế
1 miếng trần bì', N'Thịt bò ướp qua ngày hôm sau sẽ ra rất nhiều nước. Lúc này đặt nồi lên bếp để rim mà không cần cho thêm nước. Trong quá trình rim thịt bò cần để lửa ở mức nhỏ nhất có thể đồng thời không mở nắp nồi để nước không cạn quá nhanh khiến thịt bò bị cháy. Thời gian rim khoảng 3h để thịt bò mềm và ngấm gia vị.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bo-rim-2.jpg?alt=media&token=97e96f0e-20c6-4ec1-9055-a301d5b03369', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 32, 67, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'f63430aa-d4a8-44dc-a1c4-577eac23d18f')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'ad4f47e4-9408-49cd-b1d7-964225c1818d', N'Cách nấu canh nấm kim châm thơm ngon bổ dưỡng', N'Chia sẻ cách nấu canh nấm kim châm với cà rốt, giò sống... thanh mát, thơm ngon và bổ dưỡng cho cả nhà cùng thưởng thức để nâng cao sức khỏe.', N'2 túi nấm kim châm ~ 200g
6 tai nấm hương tươi
50g ngô ngọt
1 củ cà rốt
100g giò sống
350g xương gà
3-4 củ hành khô
Muối hạt
Hành lá, mùi ta (ngò rí)
Gia vị: Bột nêm hoặc bột canh, nước mắm, bột ngọt
', N'Nấm hương tươi cũng rửa qua, sau đó đem ngâm cùng nước vo gạo 5 phút để khử mùi gây ngái đặc trưng của loại nấm này. Nếu không có nước vo gạo, bạn ngâm nấm trong nước muối loãng tương tự như nấm kim châm. Sau đó vớt ra, rửa sạch, cắt bỏ cuống rồi thái lát có độ dày tầm 2/3 phân.', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-nam-kim-cham-4x3.jpg?alt=media&token=ed4b76f7-331e-4bd9-9b13-319277a4cd85', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 34, 55, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'b5abba73-9cd5-4fff-83ba-c84373351551')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'705a19e7-1369-4f33-975c-97f800b66919', N'Cách làm bánh mì nguyên cám bằng nồi chiên không dầu', N'Mách bạn công thức làm món bánh mì dai mềm bằng 100% bột mì nguyên cám thơm ngon, bổ dưỡng. Chỉ cần một chiếc nồi chiên không dầu nhỏ gọn là có ngay món bánh để dùng cho bữa sáng hay bữa phụ và vô cùng tốt cho sức khỏe.', N' Bột mì nguyên cám 250 gr
 Men nở instant 3 gr
 Nước 170 ml
 Dầu ăn 10 ml
 Muối 1/2 muỗng cà phê', N'Đầu tiên, bạn gấp bột lại, sau đó dùng mu bàn ấn và miết bột ra xa. Lưu ý là ấn và miết bột ra xa chứ không phải ấn xuống. Kế tiếp xoay khối bột một góc 90 độ rồi lặp lại hai bước trên trong khoảng 15 phút.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thanh-pham-363.jpg?alt=media&token=d31dbf0e-fbcc-439a-9fb2-002ec9004723', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 48, 76, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'1d8e4062-2aee-47a0-9866-10c919f35f05')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'48ca1508-2569-431b-8c39-9ac18939a1ed', N'Cách làm nem rán miền Bắc giòn rụm, vàng đều, không bị ỉu
', N'Làm nem rán theo cách dưới đây chắc chắn sẽ khiến bạn tự tin trổ tài trong những dịp nhà có cỗ hoặc làm mâm cơm Tết.', N'500g thịt heo xay
5g miến dong
10g mộc nhĩ, nấm hương
2 thệp bánh đa nem
1 củ cà rốt
2 quả trứng gà
20g giá đỗ
10g hành lá
Rau sống, rau thơm ăn kèm
', N'Miến rửa qua, ngâm trong nước lạnh ít nhất 30 phút. Nhiều người vội nên thường ngâm nước ấm, tuy nhiên sẽ khiến miến nở mềm ra, khi cho vào nhân nem sẽ bị chảy nát, đây là nguyên nhân làm nem rán bị ỉu. Tốt nhất là ngâm nước lạnh, sau đó vớt ra, để thật ráo.

Mộc nhĩ, nấm hương ngâm nước hơi ấm ấm, khi nở mềm thì phải rửa thật sạch, để ráo. 

Giá đỗ ngâm nước muối loãng, rửa sạch rồi cho ra rổ ráo nước.

Hành lá nhặt rửa sạch, xắt nhỏ.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-nem-ran-6.jpg?alt=media&token=266a42ed-1ccc-486e-ae37-e6cfb61bc864', CAST(N'2022-12-31T23:17:54.847' AS DateTime), CAST(N'2022-12-31T23:17:54.847' AS DateTime), 23, 45, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'52a78c72-92d6-4d84-8762-ec282b185105')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'f0a76a43-20e9-4009-b368-9de37a4ecdfb', N'Các cách nấu canh súp lơ ngon đủ vị
', N'Súp lơ nhanh chín nên thường được luộc và xào nhưng nếu muốn bạn cũng có thể nấu canh súp lơ theo những cách phổ biến sau:

', N'Canh súp lơ với tôm
Canh súp lơ mọc
Canh súp lơ thịt bò hoặc thịt bằm
Canh súp lơ nấu xương
Canh súp lơ chay', N'Tôm tươi lột vỏ, rút chỉ đen, rửa sạch, để ráo. Có thể giữ nguyên phần đuôi tôm để tăng phần thẩm mỹ hoặc giã nhuyễn thịt tôm tùy sở thích. Phần đầu tôm có thể giã rồi lọc lấy nước nấu canh, nước canh sẽ ngọt hơn.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-sup-lo-5x7.jpg?alt=media&token=e58929b5-3558-47d1-a353-a548937c2f3f', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 27, 53, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'0101efd3-8a1a-4386-810a-4c008d11616f')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'11a65e68-ca66-4144-b10d-a01cb8f01bab', N'Cách làm bê tái chanh ngon như nhà hàng
', N'Cách làm bê tái chanh ngon như nhà hàng, thịt bê thơm ngọt, quyện mùi sả, gừng, ăn cùng với khế chua, chuối xanh chấm tương bần cay cay bao phê.

', N'600g thịt bê
7-8 nhánh sả
3 củ gừng to
1-2 quả ớt sừng
2-3 củ giềng bánh tẻ
1 quả chanh
Rượu trắng
Muối hạt', N'Thịt bê có mùi gây ngái đặc trưng nên trước khi chế biến, cần phải ngâm qua rượu trắng, 1 ít gừng giã nhuyễn và chút muối hạt trong khoảng 5-6 phút. Trong lúc ngâm rượu, cần chà khắp bề mặt miếng bê để chúng được sạch đều. Sau đó xả lại nước, để ráo.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/tron-be-tai-chanh.jpg?alt=media&token=1dbcd8c4-76e5-4788-a63d-9852ca0a06d7', CAST(N'2022-12-31T23:17:45.557' AS DateTime), CAST(N'2022-12-31T23:17:45.557' AS DateTime), 21, 34, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'2ec52ea9-65f8-496f-8f35-509c0bee85b8')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'5777c918-0acd-4e19-86dd-a8ebd77a69cb', N'Cách làm mực xào sa tế siêu ngon mà đơn giản
', N'Mực xào sa tế với hành tây, kết hợp cùng với bông cải, ớt chuông các màu và cần tây rất ngon, vị cay nồng của sa tế quyện vào giúp mực thêm thấm vị và không còn mùi tanh. Đặc biệt làm món này nhanh, không quá cầu kỳ, xứng đáng là 1 trong những món ngon từ mực nên chế biến.

', N'400g mực tươi
1 hũ sa tế
1 củ hành tây ~ 150g
1/4 bông cải trắng ~ 120g
Ớt chuông các màu ~ 180-200g
5-6 tép tỏi
1 củ gừng
1 quả ớt sừng
1-2 nhánh cần tây hoặc hành lá, ngò rí (mùi ta)', N'Ớt chuông bỏ ruột, bỏ hạt, cắt miếng vuông nhỏ giống như hành tây. Ớt chuông có độ giòn, thanh mát, sẽ giúp cho món mực xào không bị ngán. Đặc biệt màu sắc rực rỡ của ớt chuông sẽ làm cho món ăn đẹp mắt hơn rất nhiều.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/muc-xao-sa-te-4x3.jpg?alt=media&token=9b51f4a9-3429-42b0-a100-4da35819fdb3', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 11, 34, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'16c2a414-7a46-48bb-998c-bb5e73e16f4e')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'f3232eb3-1c98-4dbd-8d10-b03f58927a71', N'Cách nấu thịt đông miền Bắc ngon, vị thanh không ngấy
', N'Cách nấu thịt đông được chia sẻ dưới đây khá đơn giản, không quá cầu kỳ về nguyên liệu và cách chế biến nhưng thành phẩm chắc chắn làm bạn ưng ý với sự kết hợp của thịt chân giò, tai heo và mộc nhĩ, cà rốt... làm món ăn càng thêm đẹp mắt.', N'500g thịt chân giò
2 cái tai heo: 500-600g
1 ít bì lợn
2 củ hành khô
6-7 tai nấm hương khô
10g mộc nhĩ ~ 1 tai to
1 củ cà rốt
Gia vị: Muối, bột ngọt, hạt tiêu xay, nước mắm', N'Thịt chân giò và tai heo mua về bóp muối hạt, cạo sạch lông, đặc biệt là cạo hết chất bẩn đen đọng lại ở các kẽ nhỏ của tai. Sau đó rửa thật sạch sẽ.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thit-dong-4x3.jpg?alt=media&token=6ca8fd4d-0b68-4cbc-b29d-170c33c7afe9', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 19, 26, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'9c7a3550-8f2e-44e0-a0bc-82e975870067')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'ff0c4b6d-b5a3-4b83-82dd-b541986f1550', N'Muesli & Greek Yoghurt (Sữa Chua Hy Lạp) Kèm Quả Mọng', N'Muesli & Greek Yoghurt (Sữa Chua Hy Lạp) Kèm Quả Mọng', N'Ngũ cốc Muesli
1 hộp sữa chua hy lạp, sữa chua thông thường cũng ngon 👍🏻
Trái mọng
Hạt thông, hạt chia, hạt gì tuỳ thích
Chút mật ong, siro lá phong hoặc đường ăn kiêng', N'Muesli ra đĩa, add sữa chua, thêm trái mọng, rắc các loại hạt tuỳ thích, + siro', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/muesli-greek-yoghurt-sữa-chua-hy-lạp-kem-quả-mọng-an-sang-diet-nhanh-gọn-healthy-recipe-main-photo.webp?alt=media&token=0ac971aa-e061-40a1-92bb-61a22a368265', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 45, 75, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'349adefc-32d8-4dd8-be8f-aebb3f32b6cb')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'861f611f-ad13-436b-8d30-b6bfd5f9e6ac', N'Cá hồi nướng chanh', N'Cá hồi nướng chanh là một món ăn ngon có hương vị đặc biệt mà ai cũng sẽ phải nhớ mãi khi thưởng thức, ngoài ra nó còn là một món ăn rất bổ dưỡng với lượng protein rất phù hợp để eat clean', N'150 gam cá hồi
1 trái chanh vàng
0.5 (1/2)muỗng cà phê hương thảo
0.5 (1/2) muỗng cà phê rau mùi (ngò)
1 muỗng cà phê hành tỏi băm
1 muỗng cà phê dầu olive
0.5 (1/2)muỗng cà phê muối', N'
Pha hỗn hợp ướp: rau ùi, hương thảo, hành tỏi băm nhuyễn, 1 muỗng dầu olive, 1/2 muỗng muối, 1/2 muỗng tiêu, 1 muỗng canh nước cốt chanh,vỏ chanh cà nhuyễn.
Có thể cho thêm ớt bột nếu thích.
', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/Untitled-1-1200x676-10-1024x526.jpg?alt=media&token=73021885-d554-4c40-ae23-559350f087eb', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 45, 78, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'6e755350-6246-4b62-8547-c3164e97c756')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'a9d116cc-6a7b-4022-81ad-bdcd154b416e', N'Cách làm gà nấu xáo ăn bún thơm nức mũi
', N'"Xáo gà" hay "gà nấu xáo" thơm lừng, có chút lớp mỡ vàng mơ sóng sánh bên trên, điểm thêm chút xanh xanh thanh mảnh của lá chanh cùng hành lá. Xáo gà ăn cùng với bún rất ngon, rất đưa miệng, là một món ăn đặc biệt thích hợp cho những ngày mưa hay khi tiết trời se lạnh hoặc vào đông.

', N'1/2 con gà ta ~ 800g
20g hành tăm
1 củ gừng to
4-5 lá chanh nhỏ
Hành lá
Muối hạt, rượu gừng
Gia vị: Nước mắm, bột nêm, bột ngọt, bột canh, tiêu xay, bột nghệ hoặc nghệ tươi
Bún ăn kèm', N'Cho dầu ăn láng nồi, nóng dầu thì phi thơm nốt số hành tăm và gừng. Sau đó cho thịt gà vào. Chỉnh lửa lớn lên 1 chút, đảo liền tay. Đến khi thịt gà săn lại, xem xém thì đổ nước vào để xáo. Lượng nước tùy thuộc vào lượng ăn của gia đình, nhưng cũng nên xâm xấp mặt thịt gà. Ở đây Cookbeo dùng hơn 2 lít một chút.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/xao-ga-16x9.jpg?alt=media&token=efc288e5-9ec5-4464-8604-0b6924428a4a', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 23, 43, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'07830456-3663-4056-b356-d12074df4d78')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'e33cfe76-d7fe-472e-832a-c517c9f86b65', N'Cách làm món sườn kho thơm lạ miệng, ngon quên sầu
', N'Sườn non kho thơm, hay còn có tên gọi khác là sườn non kho khóm hoặc sườn non kho dứa tùy thuộc vào từng vùng miền. Đây là món ngon từ sườn và hấp dẫn không hề thua kém sườn xào chua ngọt quen thuộc.

', N'500g sườn non (hoặc sườn thăn)
1/2 quả thơm (dứa, khóm) ~ 400g
Tiêu hạt, tiêu xay
5-6 tép tỏi
2 củ hành khô
Hành lá
1 quả ớt nếu ăn cay', N'Sườn sau khi chần vớt ra, rửa sạch với nước lạnh, để thật ráo. Ướp sườn cùng với 1 muỗng canh nước mắm, 1/2 thìa canh đường, 1/2 thìa canh bột ngọt, 1 thìa canh dầu màu điều (để tạo màu sắc đẹp mắt cho món kho) và 1 thìa canh nước màu, 1 thìa cà phê tiêu hạt, 1/2 thìa cà phê hạt tiêu xay.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/suon-kho-thom-16x9.jpg?alt=media&token=29e587e0-55b8-4e31-803e-e752dab2fba5', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 27, 67, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'e9e57429-7510-4fbf-9ed8-d33812552bc7')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'75bc99c6-006a-4eb6-9945-cea4a53fe118', N'Cách làm bánh tráng cuốn thịt heo chấm mắm nêm', N'Làm bánh tráng cuốn thịt heo Đà Nẵng tại nhà, đặc biệt là cách pha chế nước mắm nêm siêu ngon, vừa vị.', N'800g thịt heo (phần thịt ba rọi/ba chỉ)
3 quả trứng gà
300g giò tai
1 củ cà rốt
3 quả chuối xanh
4 quả dưa chuột
2 quả dứa
Ớt tươi, tỏi
Diếp xoăn, mùi ta (ngò rí), rau húng, kinh giới, tía tô...
Mắm nêm đóng chai', N'Thịt heo bóp muối hạt, rửa sạch, để nguyên tảng. Nấu 1 nồi nước, khi nước sôi, thả thịt vào chần qua cùng với ít muối hạt. Đến khi nước bùng sôi trở lại thì vớt thịt ra, rửa sạch lại rồi mang đi luộc. Khi luộc, nhớ cho nước ngập mặt thịt. Thả thêm 1 vài củ hành khô để luộc thịt được thơm.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/banh-trang-cuon-thit-heo-16x9.jpg?alt=media&token=beab6c49-b93e-49e7-8d31-9b42b5df59fd', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 11, 55, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'1372940f-1d98-44f5-979d-a016c22243a5')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'7fb631b1-abfc-4acd-b1f7-d0651a30b6b1', N'Cách nấu bao tử hầm tiêu xanh 10 điểm chất lượng
', N'Bao tử hầm tiêu xanh và nước dừa rất ngậy, thơm, bao tử mềm đậm vị và sạch sẽ, không còn mùi hôi. Đặc biệt món này còn có thể làm thành món lẩu bao tử siêu hấp dẫn, ăn là ghiền.

', N'1 cái bao tử ~ 800g (hay còn gọi là dạ dày lợn, dạ dày heo)
8-10 nhánh tiêu xanh
500ml nước dừa tươi
Muối hạt,rượu gừng hoặc rượu trắng
1 củ gừng to
4-5 củ hành khô
2-3 nhánh hành lá
1 quả ớt sừng hoặc ớt hiểm', N'Nấu 1 nồi nước, thả 1 ít gừng đập dập và muối hạt vào, khi nước già sôi thả bao tử vào để chần qua. Khi nước bùng sôi trở lại thì vớt bao tử ngay ra 1 tô nước đá có vắt nước cốt chanh. Nước đá chanh sẽ làm bao tử vừa trắng, vừa giòn lại vừa thơm. Không nên chần bao tử quá lâu sẽ khiến cho mặt trong bao tử bị nát, rất khó để rửa sạch.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/bao-tu-ham-tieu-4x3.jpg?alt=media&token=0ccf36cf-56ab-4db3-99a0-8f7ab0c5c364', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 22, 66, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'e8e4b8f8-e81d-4049-bfd0-8406deec26b0')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'fa686511-092a-4f50-9683-d4623bc5eb0e', N'Cách làm cút lộn xào me ngon khó cưỡng
', N'Cách làm cút lộn xào me thơm nức mũi, trứng cút không bị nát, nước sốt me sánh mịn, vị chua cay mặn ngọt hòa quyện và hấp dẫn người ăn ở độ bùi ngậy siêu hấp dẫn nữa sẽ được chia sẻ trong bài viết. Món ăn này còn được gọi là sốt me, rang me, ngào me... và phải ăn nóng mới ngon, để nguội trứng sẽ bị tanh và cứng.', N'30 quả trứng cút lộn
1 chai nước sốt me
4-5 tép tỏi
1-2 nhánh sả
1 củ gừng
2 củ hành khô
20g đậu phộng (lạc)
1 bó rau răm
1-2 quả ớt
10g cùi dừa
30ml nước cốt dừa', N'Trứng cút lộn rửa qua cho sạch. Nấu 1 nồi nước, nấu sôi thả trứng cút vào để luộc. Khi nước bùng sôi lại, hạ nhỏ lửa để sôi bé và luộc trứng trong khoảng 10-12 phút cùng với 1 ít muối hạt. Trứng chín ủ trong nồi thêm 5 phút rồi vớt ra để nguội. Có thể ngâm trong nước lạnh 1 lúc để dễ bóc vỏ.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/dia-cut-lon-xao-me.jpg?alt=media&token=76243db4-b562-4499-bb2c-6db8bfca2c3a', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 33, 66, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'9dba975b-8f1d-4c42-9305-159259142aec')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'93fe0cbf-0070-491f-9474-d7541387c6d7', N'Cách làm sữa chuối Hàn Quốc thơm ngon, bổ dưỡng đơn giản', N'Sữa chuối là một trong những loại thức uống thơm ngon và vô cùng bổ dưỡng. Với công thức vô cùng đơn giản, chỉ với 5 phút là bạn đã có ngay một ly sữa chuối đủ chất dinh dưỡng cho buổi sáng bận rộn.', N' Chuối 2 quả
 Đường 20 g
 Sữa tươi không đường 200 ml', N'Đầu tiền bạn cho 2 quả chuối và tô hoặc nồi, rồi dùng phới hoặc muỗng nghiền nhuyễn chuối ra. Bạn có thể sử dụng máy xay sinh tố để nghiền chuối nhé.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/3-cach-lam-sua-chuoi-thom-ngon-bo-duong-don-gian--14-760x367.jpg?alt=media&token=8885e56b-8937-4097-89c8-e58f8612dd30', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 45, 69, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'c034ba3b-3ce4-45ad-808f-6fb3f78a8767')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'20fc3a6b-5c35-48ff-883b-d7cc0de47bf1', N'Cách làm cá hấp bia ngon, ăn không tanh, ngon khó cưỡng
', N'Cá hấp bia là món ăn vô cùng hấp dẫn, thơm ngon và ăn không bị ngán. Cá hấp không tanh, thơm mùi bia quyện với các nguyên liệu như sả, gừng, thì là hành lá và đậm đà khi được ướp các gia vị như dầu hào, hạt tiêu...

', N'1.3kg cá chép
2 lon bia
5-6 nhánh sả
20g gừng
25g riềng
15g nghệ
30g hành lá
30g thì là
3-4 trái ớt cay
Chanh, tỏi làm nước chấm', N'Chuẩn bị 1 chiếc nồi to, đổ 1 lon bia vào. Đặt xửng hấp vào rồi lót sả, hành lá và thì là. Tiếp đến, đặt cá lên trên, rưới luôn phần nước ướp cá tiết ra, đậy kín nắp và bắt đầu hấp ở nhiệt độ vừa.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ca-hap-bia-4x3.jpg?alt=media&token=121a4cf9-9b3f-40cf-8293-93af2fdaef84', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 13, 54, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'b891b591-f775-499e-b759-aefdf47c0a84')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'8f342926-3676-432e-a772-d805c3c1c359', N'Cách nấu canh nấm kim châm thơm ngon bổ dưỡng
', N'Chia sẻ cách nấu canh nấm kim châm với cà rốt, giò sống... thanh mát, thơm ngon và bổ dưỡng cho cả nhà cùng thưởng thức để nâng cao sức khỏe.

', N'2 túi nấm kim châm ~ 200g
6 tai nấm hương tươi
50g ngô ngọt
1 củ cà rốt
100g giò sống
350g xương gà
3-4 củ hành khô
Muối hạt
Hành lá, mùi ta (ngò rí)
', N'Nấm hương tươi cũng rửa qua, sau đó đem ngâm cùng nước vo gạo 5 phút để khử mùi gây ngái đặc trưng của loại nấm này. Nếu không có nước vo gạo, bạn ngâm nấm trong nước muối loãng tương tự như nấm kim châm. Sau đó vớt ra, rửa sạch, cắt bỏ cuống rồi thái lát có độ dày tầm 2/3 phân.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-nam-kim-cham-4x3.jpg?alt=media&token=8fdb2ed5-1da8-4bbe-acce-d8dace77d4c7', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 21, 48, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'470187ef-15a4-4559-86f3-e4a7e0f26d93')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'eab413d5-1ea5-4585-8758-dbebe6cffb49', N'Cách nấu thịt kho tàu ngon bá cháy
', N'Cách nấu thịt kho tàu mà Cookbeo chia sẻ dưới đây chắc chắn sẽ khiến các bạn cảm thấy hài lòng với thành phẩm thu được và đặc biệt tự tin hơn khi nấu món ngon này để chiêu đãi gia đình và bạn bè!

', N'850g thịt ba chỉ
5 quả trứng vịt
900ml nước dừa tươi
4-5 củ hành khô
2-3 tép tỏi
1/2 quả chanh tươi
1-2 trái ớt
Muối hạt
', N'Ngoài ra khi nấu món ăn này, có 3 điểm quan trọng mà Cookbeo cần chia sẻ với các bạn, thứ 1 đó là món thịt này cần có độ nhừ, ngậy, thứ 2 là hương vị mặn ngọt hòa quyện, thơm và thứ 3 là màu sắc của miếng thịt kho.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thit-kho-tau-4x3.jpg?alt=media&token=c6c02404-328f-40a9-bf13-2112c93d8b33', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 16, 54, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'a494dac7-d09a-4d4a-bec2-af10fd702d87')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'8b169908-ef75-4854-a693-dca0e6cba7e3', N'Inarizushi gạo lứt', N'Món cơm bọc vỏ đậu phụ cực kỳ phổ biến ở Nhật và cả trong bữa ăn hàng ngày của người Nhật.', N'Vỏ đậu phụ ngâm dấm Nhật (có bán ở các siêu thị hay cửa hàng đồ Nhật)
Gạo lứt đen
Gạo lứt nâu
Gia vị rắc cơm Nhật (vị lá tía tô và ô mai mơ Nhật)
Lá tía tô Nhật tươi
Quinoa (nếu có/tuỳ thích)', N'Cơm chín để nguội. Trộn chung với gia vị trộn cơm rồi nhét cơm vào vỏ đậu phụ. ', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/photo.webp?alt=media&token=eb96dd07-5ddd-4960-a034-d06cfaf4aaf1', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 23, 49, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'1208f873-d1f7-490f-b001-e255d30b8ac9')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'f0831775-a7a9-4e45-aedd-e3ae1590e548', N'Chả giò chay', N'Một món chay mà lại thơm ngon, giòn rụm, rất phù hợp với những bữa tiệc chay.', N'2 bìa đậu phụ, 2 tấm váng đậu (phù chúc), hành tây, cà rốt, su hào, củ đậu (mỗi loại 1 củ), 1 mớ rau mùi, mộc nhĩ, nấm hương (mỗi loại 10 tai), 30 gram miến khô, bánh đa nem, gia vị, hạt tiêu', N'Hành tây, su hào, cà rốt cạo vỏ, rửa sạch. Hành tây thái hạt lựu. Su hào, cà rốt thái sợi chỉ. Rau mùi nhặt, rửa sạch, thái nhỏ. Nấm hương, mộc nhĩ ngâm nước nóng, rửa sạch và thái nhỏ.', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/staticFood1.png?alt=media&token=c9f539b2-d25c-4a43-a759-6697dc8056ba', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 32, 59, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'a6cad201-d020-4c3b-889c-4687cba8bf7f')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'45ff1006-52be-49b2-8e58-ef33f0e5111e', N'Thịt Bò Xào Bắp Non', N'Món xào đơn giản với thịt bò ngọt, bắp non giòn giòn, thực đơn món xào cho những bà nội trợ bận rộn. Hãy thêm món xào này vào thức đơn nhà bạn nhé!', N'Thịt bò300 GrBắp non200 GrHành tây1/2 CủMuối1/2 Muỗng cà phêDầu hào2 Muỗng cà phêHành lá1/2 CâyTiêu1/2 Muỗng cà phêTỏi băm1/2 Muỗng cà phêDầu ăn2 Muỗng cà phê', N'Thịt bò rửa sạch, dùng cán dao đập sơ để khi xào thịt bò được mềm, thái lát mỏng, ướp vào chén thịt 1 muỗng cà phê dầu ăn, 1/2 muỗng cà phê muối, một ít tiêu.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/recipe12572-635635810083497899.jpg?alt=media&token=144464ff-9d89-4bf3-8ee7-68734f12917a', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 24, 56, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'e4dc6231-ebe7-4e52-aa5a-abe4c2488cc3')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'9d442fa4-ea4d-4cfe-ae30-f20a635d9cd0', N'Cách làm cá nấu măng chua ớt ngon miễn chê
', N'Ngoài cá om dưa, bạn cũng có thể thay đổi 1 chút với cá nấu măng chua. Thực tế, cá nấu măng chua ớt rất ngon, đặc biệt vị chua dịu và cay nồng của măng muối không những đã át đi vị tanh của cá mà còn thấm đẫm vào từng thớ thịt, giúp cho món canh cá thơm và hấp dẫn hơn.

', N'1kg cá (cá trắm, cá chép hay cá lóc đều được)
1 bát tô măng chua ớt
3-4 quả cà chua
2 củ hành khô
2-3 tép tỏi
Bột nghệ (hoặc 1 đốt nghệ tươi)
Muối hạt', N'Cá cắt thành khúc vừa ăn, tuy nhiên lưu ý không nên cắt khúc quá bé, khi nấu cá dễ bị vỡ nát. Bóp muối hạt để làm sạch cá và khử tanh rồi rửa sạch, để ráo nước.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ca-nau-mang-chua-16x9.jpg?alt=media&token=d55cb81e-1d8a-4ae1-96be-ca8619dadedd', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 26, 35, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'31b2a522-cb8d-4cbf-945a-8f63b9fcec84')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'56347797-102a-4720-a6a8-fa58ae1d464b', N'Cách làm salad ức gà sốt mè rang chua chua ngọt ngọt cực ngon
', N'Salad là một trong những món ăn đơn giản nhưng chứa rất nhiều dưỡng chất, vitamin cần thiết cho cơ thể nên được nhiều người ưa chuộng.', N' Thịt ức gà 300 gr
 Rau xà lách 2 cây
 Cà chua 2 quả
( hoăc cà chua bi 6 - 7 quả)
 Dưa leo 1 quả
(dưa chuột)
 Trứng cút 10 quả
 Nước sốt mè rang 4 muỗng canh', N'Rau xà lách mua về bạn rửa sạch nhiều lần với nước, sau đó vớt ra để ráo rồi trình bày sẵn ra đĩa. Cà chua rửa sạch, sau đó cắt thành từng miếng vừa ăn, nếu là cà chua bi thì bạn rửa sạch, sau đó bổ đôi.', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/cach-lam-salad-uc-ga-sot-me-rang-chua-chua-ngot-ngot-cuc-ngon-avt-1200x676-1.jpg?alt=media&token=8d56bebf-7682-4b7f-9710-093648727883', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 32, 67, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'e73d912a-2482-42c1-b591-f5bdb3e8653a')
INSERT [dbo].[Blog] ([blogId], [title], [description], [preparation], [processing], [finished], [imageURL], [createdDate], [updatedDate], [reaction], [view], [authorId], [blogStatus], [videoURL], [recipeId]) VALUES (N'60487557-92a5-4132-afe2-fb00a5d7e238', N'Cách nấu canh dưa bò thơm ngon, đưa cơm, ăn không bị ngán
', N'Canh dưa bò có vị chua dịu dễ ăn, thơm mùi dưa cải muối chua quyện với độ ngọt mềm của thịt bò, ăn cùng cơm và các món rang mặn rất đưa miệng. Tham khảo nguyên liệu và cách nấu canh dưa bò chi tiết trong bài viết dưới đây.

', N'300g thịt bò
200g dưa cải muối chua
3 quả cà chua
20g hành lá
1 củ hành khô
Gừng
Dầu mè', N'Ướp thịt bò với 1 thìa canh dầu mè hoặc dầu ăn và 1 thìa cà phê tương. Trộn đều lên và để thịt bò ướp trong khoảng ít nhất 20 phút để ngấm gia vị.

', NULL, N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-dua-bo-4x5.jpg?alt=media&token=f22b45ed-4697-49ed-8a26-44fd4aeed345', CAST(N'2022-12-31T23:18:25.910' AS DateTime), CAST(N'2022-12-31T23:18:25.910' AS DateTime), 28, 59, N'c1bd7421-a3d0-496a-a775-b307737777c1', 1, NULL, N'0f536237-c887-4c19-946a-41eab4c2c0af')
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
INSERT [dbo].[Category] ([categoryId], [name], [description], [status], [createdDate]) VALUES (N'433b682e-3651-4f4b-b688-0eaf344c51bd', N'Phong cách ăn uống', N'Các món ăn được phân loại theo phong cách ăn uống phù hợp với lựa chọn của người dùng', 1, CAST(N'2022-12-31T00:00:00.000' AS DateTime))
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
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'1d8e4062-2aee-47a0-9866-10c919f35f05', N'Cách làm bánh mì nguyên cám bằng nồi chiên không dầu', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thanh-pham-363.jpg?alt=media&token=d31dbf0e-fbcc-439a-9fb2-002ec9004723', 34000.0000, 50000.0000, 2, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'9dba975b-8f1d-4c42-9305-159259142aec', N'Cách làm cút lộn xào me ngon khó cưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/dia-cut-lon-xao-me.jpg?alt=media&token=76243db4-b562-4499-bb2c-6db8bfca2c3a', 12000.0000, 25000.0000, 4, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'cfaa3f8b-f7b7-405f-a01d-206a7d87dc32', N'Cách làm vịt om sấu miền Bắc với khoai sọ thơm ngon, chuẩn vị. Khi nấu món ăn này việc khó nhất là làm sao cho nước vịt om sấu được trong, không bị đen và có vị chua dịu thanh mát. ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/vit-om-sau-4x3.jpg?alt=media&token=09204f09-69f6-4827-a7f5-81b42ad401d3', 70000.0000, 100000.0000, 1, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'd279301e-9cab-4333-9d12-2c28fc1b8484', N'Cách nấu bún cá dọc mùng kiểu miền Bắc ngon chuẩn vị
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bun-ca.jpg?alt=media&token=9276f0ed-8d0a-44bb-a379-ca67922411b1', 56000.0000, 70000.0000, 4, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'6551fc76-3aaa-4036-9d09-37f5429d5772', N'Cách làm thịt kho mắm ruốc sả ớt thơm ngon đậm đà
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-thit-kho-mam-ruoc.jpg?alt=media&token=eb268a7b-d515-4e57-bf49-754b58a8b896', 45000.0000, 53000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'0f536237-c887-4c19-946a-41eab4c2c0af', N'Cách nấu canh dưa bò thơm ngon, đưa cơm, ăn không bị ngán
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-dua-bo-4x5.jpg?alt=media&token=f22b45ed-4697-49ed-8a26-44fd4aeed345', 59000.0000, 67000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'a6cad201-d020-4c3b-889c-4687cba8bf7f', N'Chả giò chay', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/staticFood1.png?alt=media&token=c9f539b2-d25c-4a43-a759-6697dc8056ba', 25000.0000, 35000.0000, 4, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'b8c494c3-bebb-4347-8b99-47c1f02009b3', N'Cách nấu canh khoai mỡ trắng với tôm tươi ngon bổ
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-khoai-mo-16x9%20(1).jpg?alt=media&token=34d7eb06-291b-44aa-8ac7-a4ac5ba76753', 59000.0000, 120000.0000, 4, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'cf04775c-51a9-4664-ae28-493318c3ab8b', N'Cách làm món cà pháo mắm tôm ngon, ăn là ghiền
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-ca-phao-mam-tom-2.jpg?alt=media&token=58a58a25-6dba-4610-80de-68ca03d9bc9f', 67000.0000, 89000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'2346415c-0318-4892-9dfb-4ad6ae883d7c', N'Cách làm bí xanh xào thịt bò lạ miệng, ăn là ghiền
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/bi-xanh-xao-thit-bo-5x7.jpg?alt=media&token=53f72188-9446-46d5-8607-7cff356b947b', 69000.0000, 309000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'0101efd3-8a1a-4386-810a-4c008d11616f', N'Các cách nấu canh súp lơ ngon đủ vị
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-sup-lo-5x7.jpg?alt=media&token=e58929b5-3558-47d1-a353-a548937c2f3f', 59000.0000, 67000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'8669fc4a-731d-404a-a291-4c4048045988', N'Cách nấu cháo thịt bò bí đỏ cho bé và cho cả người lớn
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/chao-thit-bo-bi-do-5x7%20(1).jpg?alt=media&token=ca3cc8bb-7abf-4bea-bc63-9d03e26f635d', 52000.0000, 67000.0000, 1, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'2ec52ea9-65f8-496f-8f35-509c0bee85b8', N'Cách làm bê tái chanh ngon như nhà hàng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/tron-be-tai-chanh.jpg?alt=media&token=1dbcd8c4-76e5-4788-a63d-9852ca0a06d7', 87000.0000, 99000.0000, 2, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'f63430aa-d4a8-44dc-a1c4-577eac23d18f', N'Cách nấu thịt bò rim miền Trung
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bo-rim-2.jpg?alt=media&token=97e96f0e-20c6-4ec1-9055-a301d5b03369', 80000.0000, 100000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'f5f5f2d8-1ae7-4bb9-b135-6dfad9b39988', N'Cách làm bánh ngào mật mía Nghệ An
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/banh-ngao-4x5.jpg?alt=media&token=6ed4044c-d6eb-41d6-9adc-0092b28b400b', 50000.0000, 140000.0000, 2, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'c034ba3b-3ce4-45ad-808f-6fb3f78a8767', N'Cách làm sữa chuối Hàn Quốc thơm ngon, bổ dưỡng đơn giản', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/3-cach-lam-sua-chuoi-thom-ngon-bo-duong-don-gian--14-760x367.jpg?alt=media&token=8885e56b-8937-4097-89c8-e58f8612dd30', 26000.0000, 36000.0000, 1, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'9c7a3550-8f2e-44e0-a0bc-82e975870067', N'Cách nấu thịt đông miền Bắc ngon, vị thanh không ngấy
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thit-dong-4x3.jpg?alt=media&token=6ca8fd4d-0b68-4cbc-b29d-170c33c7afe9', 78000.0000, 93000.0000, 4, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'e8e4b8f8-e81d-4049-bfd0-8406deec26b0', N'Cách nấu bao tử hầm tiêu xanh 10 điểm chất lượng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/bao-tu-ham-tieu-4x3.jpg?alt=media&token=0ccf36cf-56ab-4db3-99a0-8f7ab0c5c364', 45000.0000, 60000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'9e343cac-2e68-48fc-8d71-87f929e9b8ce', N'Spaghetti bún gạo lức ăn kèm cải bó xôi', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/photo%20(2).webp?alt=media&token=1296c706-33fa-404d-8155-7455c0aaa810', 45000.0000, 60000.0000, 2, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'31b2a522-cb8d-4cbf-945a-8f63b9fcec84', N'Cách làm cá nấu măng chua ớt ngon miễn chê
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ca-nau-mang-chua-16x9.jpg?alt=media&token=d55cb81e-1d8a-4ae1-96be-ca8619dadedd', 59000.0000, 67000.0000, 2, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'1372940f-1d98-44f5-979d-a016c22243a5', N'Cách làm bánh tráng cuốn thịt heo chấm mắm nêm', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/banh-trang-cuon-thit-heo-16x9.jpg?alt=media&token=beab6c49-b93e-49e7-8d31-9b42b5df59fd', 37000.0000, 50000.0000, 2, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'7975d2a0-c6f4-4654-b94f-a217c4ea6ada', N'Cách nấu canh chua cá lóc miền Tây Nam Bộ
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-chua-ca-loc-5x7.jpg?alt=media&token=f2216489-0040-4e67-825e-4c246cd34fb3', 58000.0000, 68000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'e4dc6231-ebe7-4e52-aa5a-abe4c2488cc3', N'Thịt Bò Xào Bắp Non', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/recipe12572-635635810083497899.jpg?alt=media&token=144464ff-9d89-4bf3-8ee7-68734f12917a', 45000.0000, 56000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'349adefc-32d8-4dd8-be8f-aebb3f32b6cb', N'Muesli & Greek Yoghurt (Sữa Chua Hy Lạp) Kèm Quả Mọng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/muesli-greek-yoghurt-sữa-chua-hy-lạp-kem-quả-mọng-an-sang-diet-nhanh-gọn-healthy-recipe-main-photo.webp?alt=media&token=0ac971aa-e061-40a1-92bb-61a22a368265', 32000.0000, 46000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'b891b591-f775-499e-b759-aefdf47c0a84', N'Cách làm cá hấp bia ngon, ăn không tanh, ngon khó cưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ca-hap-bia-4x3.jpg?alt=media&token=121a4cf9-9b3f-40cf-8293-93af2fdaef84', 34000.0000, 50000.0000, 2, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'a494dac7-d09a-4d4a-bec2-af10fd702d87', N'Cách nấu thịt kho tàu ngon bá cháy
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thit-kho-tau-4x3.jpg?alt=media&token=c6c02404-328f-40a9-bf13-2112c93d8b33', 58000.0000, 69000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'bffd0e26-85a2-4b71-95f8-b565ec79f956', N'Đậu phụ chiên sả ớt', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/MVRQ94ZtT4OzzpfsP8IF_10.duahuchienxa-large%402x.jpg.jpg?alt=media&token=7a4b1c50-f7a5-40df-81e9-6a3d23b6c96b', 26000.0000, 34000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'a6c08997-114b-456f-a815-b68e2a5f9745', N'Đậu hũ xào rau củ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/maxresdefault.jpg?alt=media&token=4afcc7e5-7314-4946-8e26-34dd934a4be8', 36000.0000, 55000.0000, 2, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'62642194-a143-480f-9f41-b84505d548bb', N'Đậu phụ sốt cà chua', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-dau-sot-ca-chua-2.jpg?alt=media&token=21be2a2d-a8be-40c5-acf9-a338584d1f39', 67000.0000, 87000.0000, 4, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'dfd04bc8-8286-4e9d-8964-b8470ce67ff5', N'Cách làm bún chả Hà Nội ngon như ngoài hàng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-bun-cha-ha-noi.jpg?alt=media&token=fa8df759-3b3d-40d2-b7e6-d16ff0c20601', 67000.0000, 78000.0000, 2, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'16c2a414-7a46-48bb-998c-bb5e73e16f4e', N'Cách làm mực xào sa tế siêu ngon mà đơn giản
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/muc-xao-sa-te-4x3.jpg?alt=media&token=9b51f4a9-3429-42b0-a100-4da35819fdb3', 44000.0000, 65000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'6e755350-6246-4b62-8547-c3164e97c756', N'Cá hồi nướng chanh', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/Untitled-1-1200x676-10-1024x526.jpg?alt=media&token=73021885-d554-4c40-ae23-559350f087eb', 63000.0000, 80000.0000, 2, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'3456764c-3c95-4b4e-8b46-c564ae9cb9bf', N'Cách làm mì xào bò đơn giản mà ngon, đủ dinh dưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-mi-xao-bo-ngon.jpg?alt=media&token=44878e3d-67b5-4ae5-acbb-3c783ec381fc', 34000.0000, 45000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'b5abba73-9cd5-4fff-83ba-c84373351551', N'Cách nấu canh nấm kim châm thơm ngon bổ dưỡng', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-nam-kim-cham-4x3%20(1).jpg?alt=media&token=c57e3fd0-98d4-4906-9ac0-9091c63a018a', 42000.0000, 58000.0000, 2, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'92ecfaf5-745d-451f-9c6c-c8a95cc8b607', N'Ức Gà Cuộn Rong Biển ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/photo%20(1).webp?alt=media&token=8909fd43-2735-4561-aa95-de44ca929df5', 42000.0000, 55000.0000, 2, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'cdce7aa7-cfa7-48f4-ba8f-cab4c979b03d', N'Cách làm sườn xào chua ngọt trong 30 phút (cách mới)
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-suon-xao-chua-ngot-mien-bac.jpg?alt=media&token=f31781f0-a238-4171-b321-636844f23e84', 45000.0000, 60000.0000, 4, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'07830456-3663-4056-b356-d12074df4d78', N'Cách làm gà nấu xáo ăn bún thơm nức mũi
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/xao-ga-16x9.jpg?alt=media&token=efc288e5-9ec5-4464-8604-0b6924428a4a', 67000.0000, 83000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'e9e57429-7510-4fbf-9ed8-d33812552bc7', N'Cách làm món sườn kho thơm lạ miệng, ngon quên sầu
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/suon-kho-thom-16x9.jpg?alt=media&token=29e587e0-55b8-4e31-803e-e752dab2fba5', 59000.0000, 67000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'3f1aa816-496b-4ae0-a90f-d6570d525ec9', N'Giá xào đậu hũ', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/thanh-pham-1142.jpg?alt=media&token=21452719-5105-4e9f-ba41-7473360bbec1', 23000.0000, 32000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'3d33d012-ce32-4645-bc85-d98b7644a87a', N'Ức gà nướng mật ong balsamic rất tốt cho người ăn giảm cân!', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/ức-ga-nướng-mật-ong-balsamic-rất-tốt-cho-người-an-giảm-can-recipe-main-photo.webp?alt=media&token=9b4c6117-616b-4da5-9bf6-70d19c394b3f', 56000.0000, 78000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'1208f873-d1f7-490f-b001-e255d30b8ac9', N'Inarizushi gạo lứt', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/photo.webp?alt=media&token=eb96dd07-5ddd-4960-a034-d06cfaf4aaf1', 34000.0000, 50000.0000, 2, 2, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'470187ef-15a4-4559-86f3-e4a7e0f26d93', N'Cách nấu canh nấm kim châm thơm ngon bổ dưỡng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/canh-nam-kim-cham-4x3.jpg?alt=media&token=8fdb2ed5-1da8-4bbe-acce-d8dace77d4c7', 45000.0000, 60000.0000, 4, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'52a78c72-92d6-4d84-8762-ec282b185105', N'Cách làm nem rán miền Bắc giòn rụm, vàng đều, không bị ỉu
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/mon-nem-ran-6.jpg?alt=media&token=266a42ed-1ccc-486e-ae37-e6cfb61bc864', 60000.0000, 70000.0000, 4, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'5bc8d213-4717-4500-adf3-f44a131cb617', N'Cách làm xôi xéo Hà Nội ngon chuẩn vị xôi xéo Phú Thượng
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/xoi-xeo-16x9.jpg?alt=media&token=d545ee4a-1f79-4d97-a3df-901d4e729cfa', 50000.0000, 64000.0000, 2, 4, NULL)
INSERT [dbo].[Recipe] ([recipeId], [title], [imageURL], [packagePrice], [cookedPrice], [minSize], [maxSize], [status]) VALUES (N'e73d912a-2482-42c1-b591-f5bdb3e8653a', N'Cách làm salad ức gà sốt mè rang chua chua ngọt ngọt cực ngon
', N'https://firebasestorage.googleapis.com/v0/b/homnayangi-files.appspot.com/o/cach-lam-salad-uc-ga-sot-me-rang-chua-chua-ngot-ngot-cuc-ngon-avt-1200x676-1.jpg?alt=media&token=8d56bebf-7682-4b7f-9710-093648727883', 38000.0000, 49000.0000, 2, 4, NULL)
GO
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'93a460cc-f092-447f-aaf5-0564cd0ffadc', N'Giảm cân', N'Giảm cân', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'433b682e-3651-4f4b-b688-0eaf344c51bd')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'3bc469f2-df84-491f-a64a-119400614291', N'Bún', N'Bún', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'3b8ab3d0-08df-47f1-bf9f-ecf895fd0daa')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'a7766c2a-8faf-48f9-bdec-1dfaba22ecf3', N'Miền Bắc', N'Miền Bắc', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'14465782-6f37-4a8f-8d5e-7a0bea7f3b4c')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'6e5375f0-b603-49da-ace5-262dcace546f', N'Trẻ em', N'Trẻ em', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'63db7259-a32a-4a5e-beb8-985f3db5f63f')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'85276dc8-058d-4938-b38a-281e7276252c', N'Thịt heo', N'Thịt heo', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'3fa85f64-5717-4562-b3fc-2c963f66afa1')
INSERT [dbo].[SubCategory] ([subCategoryId], [name], [description], [createdDate], [status], [categoryId]) VALUES (N'76bc7165-d0be-4d32-b819-434dfaeba2ad', N'Món kho', N'Món kho', CAST(N'2022-12-31T23:17:20.830' AS DateTime), 1, N'8033733e-b4d5-4dfb-8316-e66481022cf2')
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
INSERT [dbo].[Unit] ([unitId], [name], [description], [createdDate], [status]) VALUES (N'dabc7178-e761-4748-b8f7-1ffc98dfb0c4', N'chai', N'string', NULL, 1)
INSERT [dbo].[Unit] ([unitId], [name], [description], [createdDate], [status]) VALUES (N'05c16c64-1994-4ac9-bb79-d5be4fcca460', N'gói', N'string', NULL, 1)
GO
INSERT [dbo].[User] ([userId], [displayname], [username], [firstname], [lastname], [email], [password], [phonenumber], [gender], [avatar], [role], [createdDate], [updatedDate], [isBlocked], [isGoogle]) VALUES (N'749f5b3b-dea1-49a4-98b8-96da197d123f', NULL, N'vanphuong0606', N'Phương', N'Võ Văn', N'vanphuong0606@gmail.com', N'kVU41twDyttUL/SM7IO0vQ==
', N'0971775169', 1, N'Nolink', 1, CAST(N'2022-12-31T23:12:44.720' AS DateTime), CAST(N'2022-12-31T23:12:44.720' AS DateTime), 0, 0)
INSERT [dbo].[User] ([userId], [displayname], [username], [firstname], [lastname], [email], [password], [phonenumber], [gender], [avatar], [role], [createdDate], [updatedDate], [isBlocked], [isGoogle]) VALUES (N'c1bd7421-a3d0-496a-a775-b307737777c1', NULL, N'monkeynam208', N' Nam', N'Nguyễn Lương Hoàng', N'monkeynam208@gmail.com', N'kVU41twDyttUL/SM7IO0vQ==
', N'0987603163', 1, NULL, 1, CAST(N'2022-12-31T23:12:44.720' AS DateTime), CAST(N'2022-12-31T23:12:44.720' AS DateTime), 0, 1)
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
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Transaction] FOREIGN KEY([orderId])
REFERENCES [dbo].[Transaction] ([transactionId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Transaction]
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
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Customer] FOREIGN KEY([customerId])
REFERENCES [dbo].[Customer] ([customerId])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Customer]
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
