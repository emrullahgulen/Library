CREATE TABLE [dbo].[Authors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Writers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 19.02.2024 13:30:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ISBN] [nvarchar](50) NULL,
	[Title] [nvarchar](250) NULL,
	[Year] [int] NULL,
	[PageCount] [int] NULL,
	[AuthorId] [int] NULL,
	[PictureUrl] [nvarchar](500) NULL,
	[BookType] [int] NULL,
	[StatusType] [int] NULL,
	[BookedMemberId] [int] NULL,
	[ReservedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookTransactions]    Script Date: 19.02.2024 13:30:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[BorrowedDate] [datetime] NULL,
	[ReturnDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_BorrowedBooks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 19.02.2024 13:30:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](100) NULL,
	[Surname] [nvarchar](100) NULL,
	[IdentityNumber] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Authors] ADD  CONSTRAINT [DF_Writers_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Authors] ADD  CONSTRAINT [DF_Authors_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Books] ADD  CONSTRAINT [DF_Table_1_Status]  DEFAULT ((1)) FOR [StatusType]
GO
ALTER TABLE [dbo].[Books] ADD  CONSTRAINT [DF_Books_Active]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[BookTransactions] ADD  CONSTRAINT [DF_Table_1_LoanDate]  DEFAULT (getdate()) FOR [BorrowedDate]
GO
ALTER TABLE [dbo].[Members] ADD  CONSTRAINT [DF_Members_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Members] ADD  CONSTRAINT [DF_Members_IsActive]  DEFAULT ((1)) FOR [IsActive]

