USE [SendGridEvents]
GO
/****** Object:  Index [IX_JOBID]    Script Date: 2014-07-15 07:04:04 ******/
DROP INDEX [IX_JOBID] ON [dbo].[Events]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 2014-07-15 07:04:04 ******/
DROP TABLE [dbo].[Events]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 2014-07-15 07:04:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EventDate] [datetime] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Event] [nvarchar](50) NOT NULL,
	[smtpID] [nvarchar](255) NULL,
	[Category] [nvarchar](max) NULL,
	[UniqueArguments] [nvarchar](max) NULL,
	[Response] [nvarchar](max) NULL,
	[Attempt] [int] NULL,
	[Reason] [nvarchar](max) NULL,
	[UserAgent] [nvarchar](max) NULL,
	[IP] [nvarchar](50) NULL,
	[URL] [nvarchar](max) NULL,
	[Status] [nvarchar](50) NULL,
	[BounceType] [nvarchar](50) NULL,
	[JobID] [int] NULL,
 CONSTRAINT [PK_Events_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Index [IX_JOBID]    Script Date: 2014-07-15 07:04:04 ******/
CREATE NONCLUSTERED INDEX [IX_JOBID] ON [dbo].[Events]
(
	[JobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
