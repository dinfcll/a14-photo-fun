USE [tempdb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Table_1_Utilisateurs]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfilUtil]'))
ALTER TABLE [dbo].[ProfilUtil] DROP CONSTRAINT [FK_Table_1_Utilisateurs]
GO

USE [tempdb]
GO

/****** Object:  Table [dbo].[ProfilUtil]    Script Date: 11/07/2014 14:34:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProfilUtil]') AND type in (N'U'))
DROP TABLE [dbo].[ProfilUtil]
GO

USE [tempdb]
GO

/****** Object:  Table [dbo].[ProfilUtil]    Script Date: 11/07/2014 14:34:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ProfilUtil](
	[IdProfil] [int] IDENTITY(1,1) NOT NULL,
	[IDUtilRechercher] [varchar](20) NOT NULL,
	[nbAbonnement] [int] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[IdProfil] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ProfilUtil]  WITH CHECK ADD  CONSTRAINT [FK_Table_1_Utilisateurs] FOREIGN KEY([IDUtilRechercher])
REFERENCES [dbo].[Utilisateurs] ([IDUtil])
GO

ALTER TABLE [dbo].[ProfilUtil] CHECK CONSTRAINT [FK_Table_1_Utilisateurs]
GO


