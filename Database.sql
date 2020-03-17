USE [SaveTheWorld]
GO
/****** Object:  Table [dbo].[CollectivePoints]    Script Date: 17/03/2020 08:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollectivePoints](
	[CollectivePointsId] [int] NOT NULL,
	[PointId] [int] NOT NULL,
	[DriverId] [int] NOT NULL,
 CONSTRAINT [PK_Points] PRIMARY KEY CLUSTERED 
(
	[CollectivePointsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Driver]    Script Date: 17/03/2020 08:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Driver](
	[DriverId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Number] [int] NULL,
	[Address] [nvarchar](50) NULL,
	[PostCode] [nchar](10) NULL,
	[NeedsHelp] [binary](10) NULL,
 CONSTRAINT [PK_Driver] PRIMARY KEY CLUSTERED 
(
	[DriverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Point]    Script Date: 17/03/2020 08:44:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Point](
	[PointId] [int] IDENTITY(1,1) NOT NULL,
	[Longitude] [float] NOT NULL,
	[Latitude] [float] NOT NULL,
 CONSTRAINT [PK_Point] PRIMARY KEY CLUSTERED 
(
	[PointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CollectivePoints]  WITH CHECK ADD  CONSTRAINT [FK_Points_Driver] FOREIGN KEY([DriverId])
REFERENCES [dbo].[Driver] ([DriverId])
GO
ALTER TABLE [dbo].[CollectivePoints] CHECK CONSTRAINT [FK_Points_Driver]
GO
ALTER TABLE [dbo].[CollectivePoints]  WITH CHECK ADD  CONSTRAINT [FK_Points_Point] FOREIGN KEY([PointId])
REFERENCES [dbo].[Point] ([PointId])
GO
ALTER TABLE [dbo].[CollectivePoints] CHECK CONSTRAINT [FK_Points_Point]
GO
