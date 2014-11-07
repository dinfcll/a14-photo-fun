USE [tempdb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AbonnementUtil_Utilisateurs]') AND parent_object_id = OBJECT_ID(N'[dbo].[AbonnementUtil]'))
ALTER TABLE [dbo].[AbonnementUtil] DROP CONSTRAINT [FK_AbonnementUtil_Utilisateurs]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AbonnementUtil_Utilisateurs1]') AND parent_object_id = OBJECT_ID(N'[dbo].[AbonnementUtil]'))
ALTER TABLE [dbo].[AbonnementUtil] DROP CONSTRAINT [FK_AbonnementUtil_Utilisateurs1]
GO

USE [tempdb]
GO

/****** Object:  Table [dbo].[AbonnementUtil]    Script Date: 11/07/2014 14:04:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AbonnementUtil]') AND type in (N'U'))
DROP TABLE [dbo].[AbonnementUtil]
GO

USE [tempdb]
GO

/****** Object:  Table [dbo].[AbonnementUtil]    Script Date: 11/07/2014 14:04:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AbonnementUtil](
	[NumLiaison] [int] IDENTITY(1,1) NOT NULL,
	[IdUtilConnecter] [varchar](20) NOT NULL,
	[IdUtilAbonner] [varchar](20) NOT NULL,
 CONSTRAINT [PK_AbonnementUtil] PRIMARY KEY CLUSTERED 
(
	[NumLiaison] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[AbonnementUtil]  WITH CHECK ADD  CONSTRAINT [FK_AbonnementUtil_Utilisateurs] FOREIGN KEY([IdUtilConnecter])
REFERENCES [dbo].[Utilisateurs] ([IDUtil])
GO

ALTER TABLE [dbo].[AbonnementUtil] CHECK CONSTRAINT [FK_AbonnementUtil_Utilisateurs]
GO

ALTER TABLE [dbo].[AbonnementUtil]  WITH CHECK ADD  CONSTRAINT [FK_AbonnementUtil_Utilisateurs1] FOREIGN KEY([IdUtilAbonner])
REFERENCES [dbo].[Utilisateurs] ([IDUtil])
GO

ALTER TABLE [dbo].[AbonnementUtil] CHECK CONSTRAINT [FK_AbonnementUtil_Utilisateurs1]
GO


