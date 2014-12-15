USE [DBPhotoFun]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Photos_NbJaime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Photos] DROP CONSTRAINT [DF_Photos_NbJaime]
END

GO

USE [DBPhotoFun]
GO

/****** Object:  Table [dbo].[Photos]    Script Date: 11/07/2014 13:06:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Photos]') AND type in (N'U'))
DROP TABLE [dbo].[Photos]
GO

USE [DBPhotoFun]
GO

/****** Object:  Table [dbo].[Photos]    Script Date: 11/07/2014 13:06:07 ******/
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
	[NbJaime] [int] NOT NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[IdPhoto] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Photos] ADD  CONSTRAINT [DF_Photos_NbJaime]  DEFAULT ((0)) FOR [NbJaime]
GO


