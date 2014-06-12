/****** Object:  Table [dbo].[users]    Script Date: 02/09/2014 22:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

--Create Users table and create a new administrator account
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[email] [varchar](250) NOT NULL,
	[user_type] [varchar](50) NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON
INSERT [dbo].[users] ([id], [name], [password], [email], [user_type]) VALUES (1, N'John', N'pass', N'johndoe@hotmail.com', N'administrator')
SET IDENTITY_INSERT [dbo].[users] OFF
/****** Object:  Table [dbo].[coffee]    Script Date: 02/09/2014 22:09:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

--Create Coffee table and fill it with sample coffee data
CREATE TABLE [dbo].[coffee](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[type] [varchar](50) NOT NULL,
	[price] [float] NOT NULL,
	[roast] [varchar](50) NOT NULL,
	[country] [varchar](50) NOT NULL,
	[image] [varchar](255) NULL,
	[review] [text] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[coffee] ON
INSERT [dbo].[coffee] ([id], [name], [type], [price], [roast], [country], [image], [review]) VALUES (1, N'Café au Lait', N'Classic', 2.25, N'Medium', N'France', N'../Images/Coffee/Cafe-Au-Lait.jpg', N'A coffee beverage consisting strong or bold coffee (sometimes espresso) mixed with scalded milk in approximately a 1:1 ratio.')
INSERT [dbo].[coffee] ([id], [name], [type], [price], [roast], [country], [image], [review]) VALUES (2, N'Caffè Americano', N'Espresso', 2.25, N'Medium', N'Italy', N'../Images/coffee/caffe_americano.jpg', N'Similar in strength and taste to American-style brewed coffee, there are subtle differences achieved by pulling a fresh shot of espresso for the beverage base.')
INSERT [dbo].[coffee] ([id], [name], [type], [price], [roast], [country], [image], [review]) VALUES (3, N'Peppermint White Chocolate Mocha', N'Espresso', 3.25, N'Medium', N'Italy', N'../Images/coffee/white-chocolate-peppermint-mocha.jpg', N'Espresso with white chocolate and peppermint flavored syrups.
Topped with sweetened whipped cream and dark chocolate curls.')
INSERT [dbo].[coffee] ([id], [name], [type], [price], [roast], [country], [image], [review]) VALUES (4, N'Irish Coffee', N'Alcoholic', 2.25, N'Dark', N'Ireland', N'../Images/coffee/irish coffee.jpg', N'A cocktail consisting of hot coffee, Irish whiskey, and sugar, stirred, and topped with thick cream. The coffee is drunk through the cream.')
SET IDENTITY_INSERT [dbo].[coffee] OFF
