USE [cpEditorial]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 19-Sep-19 2:03:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Email] [varchar](30) NOT NULL,
	[Message] [text] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
