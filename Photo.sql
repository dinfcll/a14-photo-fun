USE [tempdb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Photo_Utilisateur]') AND parent_object_id = OBJECT_ID(N'[dbo].[Photo]'))
ALTER TABLE [dbo].[Photo] DROP CONSTRAINT [FK_Photo_Utilisateur]
GO

USE [tempdb]
GO

/****** Object:  Table [dbo].[Photo]    Script Date: 09/22/2014 13:18:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Photo]') AND type in (N'U'))
DROP TABLE [dbo].[Photo]
GO

USE [tempdb]
GO

/****** Object:  Table [dbo].[Photo]    Script Date: 09/22/2014 13:18:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Photo](
	[IDPhoto] [int] NOT NULL,
	[Categorie] [varchar](20) NOT NULL,
	[IMGPhoto] [image] NOT NULL,
	[NumUtil] [int] NOT NULL,
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[IDPhoto] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_Utilisateur] FOREIGN KEY([NumUtil])
REFERENCES [dbo].[Utilisateur] ([NumUtil])
GO

ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_Utilisateur]
GO


