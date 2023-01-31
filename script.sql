USE [master]
GO
/****** Object:  Database [Homnayangi]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[Accomplishment]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[Blog]    Script Date: 1/31/2023 1:55:20 PM ******/
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
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[blogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogReaction]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[BlogSubCate]    Script Date: 1/31/2023 1:55:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogSubCate](
	[blogId] [uniqueidentifier] NOT NULL,
	[subCateId] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NULL,
 CONSTRAINT [PK_BlogSubCate] PRIMARY KEY CLUSTERED 
(
	[blogId] ASC,
	[subCateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 1/31/2023 1:55:20 PM ******/
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
	[isCombo] [bit] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[CustomerReward]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[CustomerVoucher]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[Ingredient]    Script Date: 1/31/2023 1:55:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredient](
	[ingredientId] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](max) NULL,
	[description] [nvarchar](max) NULL,
	[quantitative] [nvarchar](max) NULL,
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
/****** Object:  Table [dbo].[Notification]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[Order]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[OrderCookedDetail]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[OrderIngredientDetail]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[OrderPackageDetail]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[Recipe]    Script Date: 1/31/2023 1:55:20 PM ******/
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
	[size] [int] NULL,
 CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED 
(
	[recipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeDetail]    Script Date: 1/31/2023 1:55:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeDetail](
	[recipeId] [uniqueidentifier] NOT NULL,
	[ingredientId] [uniqueidentifier] NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_RecipeDetail] PRIMARY KEY CLUSTERED 
(
	[recipeId] ASC,
	[ingredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reward]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[SubCategory]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[Transaction]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[Type]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 1/31/2023 1:55:20 PM ******/
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
/****** Object:  Table [dbo].[Voucher]    Script Date: 1/31/2023 1:55:20 PM ******/
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
ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD  CONSTRAINT [FK_Recipe_Blog] FOREIGN KEY([recipeId])
REFERENCES [dbo].[Blog] ([blogId])
GO
ALTER TABLE [dbo].[Recipe] CHECK CONSTRAINT [FK_Recipe_Blog]
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
