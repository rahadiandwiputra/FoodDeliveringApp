USE [master]
GO
/****** Object:  Database [FoodDelivery]    Script Date: 21/05/2022 13:21:31 ******/
CREATE DATABASE [FoodDelivery]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FoodDelivery', FILENAME = N'C:\Users\dwipu\FoodDelivery.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FoodDelivery_log', FILENAME = N'C:\Users\dwipu\FoodDelivery_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FoodDelivery] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FoodDelivery].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FoodDelivery] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FoodDelivery] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FoodDelivery] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FoodDelivery] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FoodDelivery] SET ARITHABORT OFF 
GO
ALTER DATABASE [FoodDelivery] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [FoodDelivery] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FoodDelivery] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FoodDelivery] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FoodDelivery] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FoodDelivery] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FoodDelivery] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FoodDelivery] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FoodDelivery] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FoodDelivery] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FoodDelivery] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FoodDelivery] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FoodDelivery] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FoodDelivery] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FoodDelivery] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FoodDelivery] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FoodDelivery] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FoodDelivery] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FoodDelivery] SET  MULTI_USER 
GO
ALTER DATABASE [FoodDelivery] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FoodDelivery] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FoodDelivery] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FoodDelivery] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FoodDelivery] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FoodDelivery] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FoodDelivery] SET QUERY_STORE = OFF
GO
USE [FoodDelivery]
GO
/****** Object:  User [tester]    Script Date: 21/05/2022 13:21:31 ******/
CREATE USER [tester] FOR LOGIN [tester] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [tester]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 21/05/2022 13:21:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Caategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courier]    Script Date: 21/05/2022 13:21:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courier](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Courier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 21/05/2022 13:21:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[Latitude] [decimal](18, 7) NULL,
	[Longitude] [decimal](18, 7) NULL,
	[Status] [varchar](50) NOT NULL,
	[CourierId] [int] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 21/05/2022 13:21:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 21/05/2022 13:21:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Stock] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 21/05/2022 13:21:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 21/05/2022 13:21:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 21/05/2022 13:21:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [ntext] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 21/05/2022 13:21:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'MAKANAN')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, N'MINUMAN')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Courier] ON 

INSERT [dbo].[Courier] ([Id], [UserId], [Name]) VALUES (1, 7, N'Courier 1')
SET IDENTITY_INSERT [dbo].[Courier] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([Id], [code], [UserId], [Latitude], [Longitude], [Status], [CourierId]) VALUES (1, N'', 8, NULL, NULL, N'WAITING', NULL)
INSERT [dbo].[Order] ([Id], [code], [UserId], [Latitude], [Longitude], [Status], [CourierId]) VALUES (2, N'A1', 8, NULL, NULL, N'WAITING', NULL)
INSERT [dbo].[Order] ([Id], [code], [UserId], [Latitude], [Longitude], [Status], [CourierId]) VALUES (3, N'A1', 8, NULL, NULL, N'WAITING', NULL)
INSERT [dbo].[Order] ([Id], [code], [UserId], [Latitude], [Longitude], [Status], [CourierId]) VALUES (4, N'A1', 8, CAST(-6.1931250 AS Decimal(18, 7)), CAST(106.8218100 AS Decimal(18, 7)), N'COMPLETED', 1)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] ON 

INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity]) VALUES (3, 4, 1, 1)
INSERT [dbo].[OrderDetail] ([Id], [OrderId], [ProductId], [Quantity]) VALUES (4, 4, 2, 1)
SET IDENTITY_INSERT [dbo].[OrderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [Stock], [Price], [Created], [CategoryId]) VALUES (1, N'Nasi Goreng', 23, 10000, CAST(N'2022-05-19T20:32:58.130' AS DateTime), 1)
INSERT [dbo].[Product] ([Id], [Name], [Stock], [Price], [Created], [CategoryId]) VALUES (2, N'Nasi Uduk', 100, 11000, CAST(N'2022-05-19T20:33:56.477' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Profile] ON 

INSERT [dbo].[Profile] ([Id], [UserId], [Name], [Address], [City], [Phone]) VALUES (4, 6, N'aa', NULL, NULL, NULL)
INSERT [dbo].[Profile] ([Id], [UserId], [Name], [Address], [City], [Phone]) VALUES (5, 1, N'dwi', N'Kp.LBS', N'Bandung Barat', N'09876636357')
INSERT [dbo].[Profile] ([Id], [UserId], [Name], [Address], [City], [Phone]) VALUES (6, 7, N'Courier 1', NULL, NULL, NULL)
INSERT [dbo].[Profile] ([Id], [UserId], [Name], [Address], [City], [Phone]) VALUES (7, 8, N'buyer', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Profile] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name]) VALUES (1, N'ADMIN')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (2, N'BUYER')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (3, N'MANAGER')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (4, N'COURIER')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [FullName], [Email], [Username], [Password]) VALUES (1, N'dwi', N'dwi@gmail.com', N'dwi', N'$2a$11$oIhNL6PgixotPQp9gD8t7erEhTM63676Q7NNnqquJZWzf9CdKgbIC')
INSERT [dbo].[User] ([Id], [FullName], [Email], [Username], [Password]) VALUES (2, N'Rahadian Dwi Putra', N'rahadian@gmail.com', N'rahadian', N'$2a$11$4SEksiPk0e77lv./jqwVOOXD/x8wf637FUCcKDZcgMhizGs2bAoRi')
INSERT [dbo].[User] ([Id], [FullName], [Email], [Username], [Password]) VALUES (3, N'Ade', N'ade@gmail.com', N'ade', N'$2a$11$5c6vAM0Sh2D5tH.0PF6kN.GPI23R9NUookr.RZl09WzOsuALQUq.O')
INSERT [dbo].[User] ([Id], [FullName], [Email], [Username], [Password]) VALUES (6, N'aa', N'aa@gmail.com', N'aa', N'$2a$11$TC98HIqWEniwrvsqHsvZsO0D/3RH86gJYPWDo25lUTH3sbFhFtuCi')
INSERT [dbo].[User] ([Id], [FullName], [Email], [Username], [Password]) VALUES (7, N'Courier 1', N'courier@gmail.com', N'courier', N'$2a$11$c6dwRezd7agzSIj4t4dkieSiE2TRqQLkcuUeM8tYIUX4LHpdoo13q')
INSERT [dbo].[User] ([Id], [FullName], [Email], [Username], [Password]) VALUES (8, N'buyer', N'buyer@gmail.com', N'buyer', N'$2a$11$hgek/mRd3Hb8tKTYKWyHs.4O9URhs3E03ow3KOKaeHP6DfnSp7sFS')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([Id], [UserId], [RoleId]) VALUES (1, 1, 1)
INSERT [dbo].[UserRole] ([Id], [UserId], [RoleId]) VALUES (2, 2, 2)
INSERT [dbo].[UserRole] ([Id], [UserId], [RoleId]) VALUES (3, 3, 3)
INSERT [dbo].[UserRole] ([Id], [UserId], [RoleId]) VALUES (5, 6, 2)
INSERT [dbo].[UserRole] ([Id], [UserId], [RoleId]) VALUES (6, 7, 4)
INSERT [dbo].[UserRole] ([Id], [UserId], [RoleId]) VALUES (7, 8, 2)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
ALTER TABLE [dbo].[Courier]  WITH CHECK ADD  CONSTRAINT [FK_Courier_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Courier] CHECK CONSTRAINT [FK_Courier_User]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Courier] FOREIGN KEY([CourierId])
REFERENCES [dbo].[Courier] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Courier]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_User]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO
USE [master]
GO
ALTER DATABASE [FoodDelivery] SET  READ_WRITE 
GO
