USE [DBPhotoFun]
GO

ALTER TABLE [dbo].[relUtilPhoto] DROP CONSTRAINT [FK_relUtilPhoto_Utilisateurs]
GO

ALTER TABLE [dbo].[relUtilPhoto] DROP CONSTRAINT [FK_relUtilPhoto_Photos]
GO

/****** Object:  Table [dbo].[relUtilPhoto]    Script Date: 2014-11-10 17:25:40 ******/
DROP TABLE [dbo].[relUtilPhoto]
GO

/****** Object:  Table [dbo].[relUtilPhoto]    Script Date: 2014-11-10 17:25:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[relUtilPhoto](
	[IDLiaisonUtilPhoto] [int] IDENTITY(1,1) NOT NULL,
	[IDUtil] [varchar](20) NOT NULL,
	[IdPhoto] [int] NOT NULL,
 CONSTRAINT [PK_relUtilPhoto] PRIMARY KEY CLUSTERED 
(
	[IDLiaisonUtilPhoto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[relUtilPhoto]  WITH CHECK ADD  CONSTRAINT [FK_relUtilPhoto_Photos] FOREIGN KEY([IdPhoto])
REFERENCES [dbo].[Photos] ([IdPhoto])
GO

ALTER TABLE [dbo].[relUtilPhoto] CHECK CONSTRAINT [FK_relUtilPhoto_Photos]
GO

ALTER TABLE [dbo].[relUtilPhoto]  WITH CHECK ADD  CONSTRAINT [FK_relUtilPhoto_Utilisateurs] FOREIGN KEY([IDUtil])
REFERENCES [dbo].[Utilisateurs] ([IDUtil])
GO

ALTER TABLE [dbo].[relUtilPhoto] CHECK CONSTRAINT [FK_relUtilPhoto_Utilisateurs]
GO


