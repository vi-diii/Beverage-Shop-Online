USE [master]
GO
/****** Object:  Database [BeverageRetail]    Script Date: 11/10/2023 3:15:07 PM ******/
CREATE DATABASE [BeverageRetail]

USE [BeverageRetail]
GO

CREATE TABLE [dbo].[Cart](
	[CartID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[ProductName] [varchar](250) NULL,
	[Quantity] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[TotalAmount] [float] NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[CartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 11/10/2023 3:15:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](250) NOT NULL,
	[Description] [varchar](255) NULL,
	[Picture] [nvarchar](max) NULL,
 CONSTRAINT [PK__Categori__19093A2B92487D3C] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 11/10/2023 3:15:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[ContactName] [varchar](100) NOT NULL,
	[Address] [varchar](255) NULL,
	[City] [varchar](50) NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 11/10/2023 3:15:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 11/10/2023 3:15:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[UnitPrice] [decimal](10, 2) NULL,
	[Quantity] [int] NULL,
	[Discount] [decimal](5, 2) NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 11/10/2023 3:15:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
 CONSTRAINT [PK__Orders__C3905BAFC53B248B] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/10/2023 3:15:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](250) NOT NULL,
	[CategoryID] [int] NULL,
	[UnitPrice] [decimal](8, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Barcode] [varchar](max) NULL,
	[ProductImage] [nvarchar](max) NULL,
 CONSTRAINT [PK__Products__B40CC6ED7AB9B1C4] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Picture]) VALUES (1, N'Milk', N'Dairy products including various types of milk', N'milk_picture_url')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Description], [Picture]) VALUES (2, N'Filtered Water', N'Bottled and filtered water products', N'water_picture_url')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [ContactName], [Address], [City], [UserName], [Password]) VALUES (1, N'John Doe', N'123 Main St', N'Cityville', N'john.doe', N'customer123')
INSERT [dbo].[Customers] ([CustomerID], [ContactName], [Address], [City], [UserName], [Password]) VALUES (2, N'Jane Smith', N'456 Oak St', N'Towndale', N'jane.smith', N'securepass')
INSERT [dbo].[Customers] ([CustomerID], [ContactName], [Address], [City], [UserName], [Password]) VALUES (3, N'Bob Johnson', N'789 Pine St', N'Villagetown', N'bob.johnson', N'pass456')
INSERT [dbo].[Customers] ([CustomerID], [ContactName], [Address], [City], [UserName], [Password]) VALUES (4, N'Alice Johnson', N'111 Elm St', N'Townsville', N'alice.johnson', N'alicepass')
INSERT [dbo].[Customers] ([CustomerID], [ContactName], [Address], [City], [UserName], [Password]) VALUES (5, N'Mark Davis', N'222 Maple St', N'Cityburg', N'mark.davis', N'mark123')
INSERT [dbo].[Customers] ([CustomerID], [ContactName], [Address], [City], [UserName], [Password]) VALUES (6, N'Sara Miller', N'333 Birch St', N'Villageville', N'sara.miller', N'sara456')
INSERT [dbo].[Customers] ([CustomerID], [ContactName], [Address], [City], [UserName], [Password]) VALUES (7, N'Tran Khanh', NULL, NULL, N'khanh.khanh', N'khanh')
INSERT [dbo].[Customers] ([CustomerID], [ContactName], [Address], [City], [UserName], [Password]) VALUES (8, N'Gia Khanh', NULL, NULL, N'khanh.tran', N'khanh')
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Role], [UserName], [Password]) VALUES (1, N'Vy', N'Vy', N'Sales Representative', N'vy.vy', N'pass123')
INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Role], [UserName], [Password]) VALUES (2, N'Son', N'Vu', N'Store Manager', N'son.vu', N'pass123')
INSERT [dbo].[Employees] ([EmployeeID], [LastName], [FirstName], [Role], [UserName], [Password]) VALUES (3, N'Chau', N'Thao', N'Cashier', N'chau.thao', N'pass123')
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (1, 10, CAST(31.00 AS Decimal(10, 2)), 2, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (1, 11, CAST(16.80 AS Decimal(10, 2)), 24, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (1, 13, CAST(4.80 AS Decimal(10, 2)), 20, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (2, 11, CAST(21.00 AS Decimal(10, 2)), 50, CAST(0.10 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (2, 12, CAST(21.00 AS Decimal(10, 2)), 14, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (3, 1, CAST(18.00 AS Decimal(10, 2)), 35, CAST(0.25 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (3, 3, CAST(10.00 AS Decimal(10, 2)), 6, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (3, 4, CAST(22.00 AS Decimal(10, 2)), 12, CAST(0.10 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (3, 14, CAST(23.25 AS Decimal(10, 2)), 3, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (4, 2, CAST(19.00 AS Decimal(10, 2)), 15, CAST(0.20 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (4, 13, CAST(6.00 AS Decimal(10, 2)), 6, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (4, 14, CAST(23.25 AS Decimal(10, 2)), 16, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (5, 2, CAST(19.00 AS Decimal(10, 2)), 20, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (5, 3, CAST(10.00 AS Decimal(10, 2)), 30, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (5, 7, CAST(19.00 AS Decimal(10, 2)), 21, CAST(0.25 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (6, 6, CAST(25.00 AS Decimal(10, 2)), 16, CAST(0.05 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (6, 10, CAST(31.00 AS Decimal(10, 2)), 20, CAST(0.10 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (7, 11, CAST(21.00 AS Decimal(10, 2)), 2, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (7, 13, CAST(6.00 AS Decimal(10, 2)), 10, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (8, 4, CAST(22.00 AS Decimal(10, 2)), 12, CAST(0.10 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (8, 14, CAST(23.25 AS Decimal(10, 2)), 3, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (9, 1, CAST(18.00 AS Decimal(10, 2)), 35, CAST(0.25 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (9, 3, CAST(10.00 AS Decimal(10, 2)), 6, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (11, 14, CAST(23.25 AS Decimal(10, 2)), 16, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (12, 2, CAST(19.00 AS Decimal(10, 2)), 15, CAST(0.20 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (12, 11, CAST(21.00 AS Decimal(10, 2)), 50, CAST(0.10 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (13, 11, CAST(16.80 AS Decimal(10, 2)), 24, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (13, 13, CAST(4.80 AS Decimal(10, 2)), 20, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (14, 10, CAST(31.00 AS Decimal(10, 2)), 2, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (15, 4, CAST(22.00 AS Decimal(10, 2)), 12, CAST(0.10 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (15, 9, CAST(21.00 AS Decimal(10, 2)), 14, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (15, 11, CAST(21.00 AS Decimal(10, 2)), 50, CAST(0.10 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (16, 1, CAST(18.00 AS Decimal(10, 2)), 35, CAST(0.25 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (16, 14, CAST(23.25 AS Decimal(10, 2)), 3, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (17, 14, CAST(23.25 AS Decimal(10, 2)), 16, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (18, 2, CAST(19.00 AS Decimal(10, 2)), 15, CAST(0.20 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (18, 3, CAST(10.00 AS Decimal(10, 2)), 6, CAST(0.00 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (18, 4, CAST(22.00 AS Decimal(10, 2)), 12, CAST(0.10 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (19, 1, CAST(18.00 AS Decimal(10, 2)), 35, CAST(0.25 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (19, 2, CAST(19.00 AS Decimal(10, 2)), 15, CAST(0.20 AS Decimal(5, 2)))
INSERT [dbo].[OrderDetails] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (19, 3, CAST(10.00 AS Decimal(10, 2)), 6, CAST(0.00 AS Decimal(5, 2)))
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (1, 5, 1)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (2, 4, 3)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (3, 2, 1)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (4, 5, 1)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (5, 5, 1)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (6, 6, 3)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (7, 4, 1)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (8, 1, 3)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (9, 3, 1)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (10, 6, 3)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (11, 2, 1)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (12, 4, 3)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (13, 2, 1)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (14, 3, 1)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (15, 2, 1)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (16, 2, 3)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (17, 5, 1)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (18, 4, 3)
INSERT [dbo].[Orders] ([OrderID], [CustomerID], [EmployeeID]) VALUES (19, 6, 1)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (1, N'Fresh milk Vinamilk', 1, CAST(3.99 AS Decimal(8, 2)), 50, NULL, N'vinamilk-fresh-milk-without-sugar-drink-box-of-1-liter_9dd3.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (2, N'Fresh milk TH True Milk', 1, CAST(4.29 AS Decimal(8, 2)), 40, NULL, N'th-true_fresh_milk.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (3, N'Vinamilk Yogurt', 1, CAST(2.49 AS Decimal(8, 2)), 60, NULL, N'sua_chua_nha_dam_vinamilk__yogurt.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (4, N'Yogurt TH True Milk', 1, CAST(2.79 AS Decimal(8, 2)), 55, NULL, N'th-true-yogurt.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (5, N'Condensed milk Ong Tho', 1, CAST(2.99 AS Decimal(8, 2)), 70, NULL, N'suadac_ongtho.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (6, N'Condensed milk Dutch Lady', 1, CAST(3.49 AS Decimal(8, 2)), 45, NULL, N'dutch_lady_condensedmilk.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (7, N'Milk powder Dielac', 1, CAST(5.99 AS Decimal(8, 2)), 30, NULL, N'dielac_powdermilk.jfif')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (8, N'Mineral water Lavie', 2, CAST(1.99 AS Decimal(8, 2)), 100, NULL, N'lavie_mineral.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (9, N'Mineral water Aquafina', 2, CAST(2.29 AS Decimal(8, 2)), 80, NULL, N've-aquafina_s5875.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (10, N'Mineral water Vinh Hao', 2, CAST(2.49 AS Decimal(8, 2)), 75, NULL, N'nuoc-vinh-hao.png')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (11, N'Mineral water Evian', 2, CAST(3.99 AS Decimal(8, 2)), 40, NULL, N'Evian.jpeg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (12, N'Purified water Aquafina', 2, CAST(1.49 AS Decimal(8, 2)), 120, NULL, N'aquafina__1__.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (13, N'Purified water Dasani', 2, CAST(1.79 AS Decimal(8, 2)), 110, NULL, N'Is-Dasani-Water-Bad-For-You.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (14, N'Purified water LaVie', 2, CAST(1.99 AS Decimal(8, 2)), 100, NULL, N'la_vie.jpg')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (17, N'Fresh milk Dutch lady  ', 1, CAST(3.44 AS Decimal(8, 2)), 20, NULL, N'fresh_milk_DutchLady.jfif')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (18, N'Milk powder Enfamilk', 1, CAST(4.00 AS Decimal(8, 2)), 45, NULL, N'enfamilk.png')
INSERT [dbo].[Products] ([ProductID], [ProductName], [CategoryID], [UnitPrice], [Quantity], [Barcode], [ProductImage]) VALUES (22, N'Dalat Milk ', 1, CAST(4.30 AS Decimal(8, 2)), 50, NULL, N'thumbnail3-447_2_3cc2.jpg')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Employees1] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Employees1]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK__Products__Catego__403A8C7D] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK__Products__Catego__403A8C7D]
GO
USE [master]
GO
ALTER DATABASE [BeverageRetail] SET  READ_WRITE 
GO
