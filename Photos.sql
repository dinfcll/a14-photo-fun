USE [tempdb]
GO

/****** Object:  Table [dbo].[Photos]    Script Date: 10/24/2014 13:27:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Photos]') AND type in (N'U'))
DROP TABLE [dbo].[Photos]
GO

USE [tempdb]
GO

/****** Object:  Table [dbo].[Photos]    Script Date: 10/24/2014 13:27:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Photos](
	[IdPhoto] [int] IDENTITY(1,1) NOT NULL,
	[Categorie] [varchar](20) NOT NULL,
	[Image] [varchar](150) NOT NULL,
	[IDUtil] [varchar](20) NOT NULL,
	[Commentaire] [varchar](200) NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[IdPhoto] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


