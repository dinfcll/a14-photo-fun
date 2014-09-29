USE [tempdb]
GO

/****** Object:  Table [dbo].[Utilisateur]    Script Date: 09/22/2014 13:18:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Utilisateur]') AND type in (N'U'))
DROP TABLE [dbo].[Utilisateur]
GO

USE [tempdb]
GO

/****** Object:  Table [dbo].[Utilisateur]    Script Date: 09/22/2014 13:18:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Utilisateur](
	[NumUtil] [int] NOT NULL,
	[IDUtil] [varchar](20) NOT NULL,
	[MotPasse] [varchar](20) NOT NULL,
	[CourrielUtil] [varchar](30) NOT NULL,
	[PrenomUtil] [varchar](20) NOT NULL,
	[NomUtil] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Utilisateur] PRIMARY KEY CLUSTERED 
(
	[NumUtil] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


