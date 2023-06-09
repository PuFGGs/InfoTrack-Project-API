USE [WebScrapper]
GO
/****** Object:  Table [dbo].[Searches]    Script Date: 21.04.2023 23:08:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Searches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](255) NULL,
	[SearchPhrase] [nvarchar](255) NULL,
	[SearchEngineId] [int] NULL,
	[Rank] [nvarchar](255) NULL,
	[Impressions] [int] NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_Search] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
