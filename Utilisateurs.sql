USE [DBPhotoFun]
GO

/****** Object:  Table [dbo].[Utilisateurs]    Script Date: 12/01/2014 12:30:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Utilisateurs]') AND type in (N'U'))
DROP TABLE [dbo].[Utilisateurs]
GO

USE [DBPhotoFun]
GO

/****** Object:  Table [dbo].[Utilisateurs]    Script Date: 12/01/2014 12:30:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Utilisateurs](
	[IDUtil] [varchar](20) NOT NULL,
	[CourrielUtil] [varchar](50) NOT NULL,
	[PrenomUtil] [varchar](25) NOT NULL,
	[NomUtil] [varchar](25) NOT NULL,
	[PhotoProfil] [varchar](150) NULL,
 CONSTRAINT [PK_Utilisateurs] PRIMARY KEY CLUSTERED 
(
	[IDUtil] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


