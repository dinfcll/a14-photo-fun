USE [tempdb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Photo_Utilisateurs]') AND parent_object_id = OBJECT_ID(N'[dbo].[Photo]'))
ALTER TABLE [dbo].[Photo] DROP CONSTRAINT [FK_Photo_Utilisateurs]
GO

USE [tempdb]
GO

/****** Object:  Table [dbo].[Photo]    Script Date: 10/10/2014 12:46:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Photo]') AND type in (N'U'))
DROP TABLE [dbo].[Photo]
GO

USE [tempdb]
GO

/****** Object:  Table [dbo].[Photo]    Script Date: 10/10/2014 12:46:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Photo](
	[IdPhoto] [int] IDENTITY(1,1) NOT NULL,
	[Categorie] [varchar](20) NOT NULL,
	[Image] [image] NOT NULL,
	[IDUtil] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[IdPhoto] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_Utilisateurs] FOREIGN KEY([IDUtil])
REFERENCES [dbo].[Utilisateurs] ([IDUtil])
GO

ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_Utilisateurs]
GO


