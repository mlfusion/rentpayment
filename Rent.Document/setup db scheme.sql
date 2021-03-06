USE [DB_9BB50B_rent]
GO
/****** Object:  Table [dbo].[LogEmail]    Script Date: 9/21/2017 8:43:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LogEmail](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [int] NULL,
	[Message] [varchar](max) NULL,
	[Email] [varchar](100) NULL,
	[Created] [datetime] NOT NULL,
	[Modifed] [datetime] NULL,
 CONSTRAINT [PK_LogEmail] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LogErrors]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LogErrors](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [int] NULL,
	[Message] [varchar](max) NOT NULL,
	[InnerMessage] [varchar](max) NULL,
	[Created] [datetime] NOT NULL,
	[Modifed] [datetime] NULL,
 CONSTRAINT [PK_LogErrors] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RentPayment]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentPayment](
	[RentPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[Payment] [float] NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modifed] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[Uid] [int] NOT NULL,
 CONSTRAINT [PK_RentPayment] PRIMARY KEY CLUSTERED 
(
	[RentPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RentPaymentNoticeSendLog]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RentPaymentNoticeSendLog](
	[PaymentNoticeSendLogId] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [int] NOT NULL,
	[Message] [varchar](max) NULL,
	[Email] [varchar](50) NULL,
	[Created] [datetime] NOT NULL,
	[Modifed] [datetime] NULL,
	[RentPaymentId] [int] NULL,
 CONSTRAINT [PK_PaymentNoticeSendLog] PRIMARY KEY CLUSTERED 
(
	[PaymentNoticeSendLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modifed] [datetime] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Uid] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NULL,
	[Address] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[ZipCode] [varchar](50) NULL,
	[Created] [datetime] NOT NULL,
	[Modifed] [datetime] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsersAccess]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsersAccess](
	[UserAccessId] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[AccessCode] [varchar](50) NOT NULL,
	[ZipCode] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modifed] [datetime] NULL,
 CONSTRAINT [PK_UsersAccess] PRIMARY KEY CLUSTERED 
(
	[UserAccessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsersLog]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsersLog](
	[UserLogId] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [int] NOT NULL,
	[IpAddress] [varchar](50) NULL,
	[Page] [varchar](50) NULL,
	[Created] [datetime] NOT NULL,
	[Modifed] [datetime] NULL,
 CONSTRAINT [PK_UsersLog] PRIMARY KEY CLUSTERED 
(
	[UserLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsersManagers]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersManagers](
	[Uid] [int] NOT NULL,
	[ManagerId] [int] NOT NULL,
 CONSTRAINT [PK_UsersManagers] PRIMARY KEY CLUSTERED 
(
	[Uid] ASC,
	[ManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersNotification]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersNotification](
	[UserNotificationId] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [int] NOT NULL,
	[Email] [bit] NULL,
	[Phone] [bit] NULL,
	[Created] [datetime] NOT NULL,
	[Modifed] [datetime] NULL,
 CONSTRAINT [PK_UsersNotification] PRIMARY KEY CLUSTERED 
(
	[UserNotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersPassword]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsersPassword](
	[UserPasswordId] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [int] NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modifed] [datetime] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_UsersPassword] PRIMARY KEY CLUSTERED 
(
	[UserPasswordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsersPasswordTranslog]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsersPasswordTranslog](
	[UserPasswordTranslogId] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [int] NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modifed] [datetime] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_UsersPasswordTranslog] PRIMARY KEY CLUSTERED 
(
	[UserPasswordTranslogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsersRole]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRole](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[Uid] [int] NOT NULL,
	[Modifed] [datetime] NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[vwLogEmail]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwLogEmail]
AS
SELECT        dbo.LogEmail.*
FROM            dbo.LogEmail

GO
/****** Object:  View [dbo].[vwLogErrors]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwLogErrors]
AS
SELECT        TOP (100) PERCENT LogId, Uid, Message, InnerMessage, Created, Modifed
FROM            dbo.LogErrors
ORDER BY LogId DESC

GO
/****** Object:  View [dbo].[vwRentPayment]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwRentPayment]
AS
SELECT        dbo.RentPayment.*
FROM            dbo.RentPayment

GO
/****** Object:  View [dbo].[vwUsers]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwUsers]
AS
SELECT        dbo.Users.*
FROM            dbo.Users

GO
/****** Object:  View [dbo].[vwUsersRolesList]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwUsersRolesList]
AS
SELECT        dbo.Users.Uid, dbo.Users.Name, dbo.UsersRole.RoleId, dbo.Roles.Name AS RoleName
FROM            dbo.Users INNER JOIN
                         dbo.UsersRole ON dbo.Users.Uid = dbo.UsersRole.Uid INNER JOIN
                         dbo.Roles ON dbo.UsersRole.RoleId = dbo.Roles.RoleId
WHERE        (dbo.Users.Active = 1)

GO
ALTER TABLE [dbo].[RentPayment]  WITH CHECK ADD  CONSTRAINT [FK_RentPayment_Users] FOREIGN KEY([Uid])
REFERENCES [dbo].[Users] ([Uid])
GO
ALTER TABLE [dbo].[RentPayment] CHECK CONSTRAINT [FK_RentPayment_Users]
GO
ALTER TABLE [dbo].[RentPaymentNoticeSendLog]  WITH CHECK ADD  CONSTRAINT [FK_RentPaymentNoticeSendLog_Users] FOREIGN KEY([Uid])
REFERENCES [dbo].[Users] ([Uid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RentPaymentNoticeSendLog] CHECK CONSTRAINT [FK_RentPaymentNoticeSendLog_Users]
GO
ALTER TABLE [dbo].[UsersAccess]  WITH NOCHECK ADD  CONSTRAINT [FK_UsersAccess_Users] FOREIGN KEY([Uid])
REFERENCES [dbo].[Users] ([Uid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersAccess] CHECK CONSTRAINT [FK_UsersAccess_Users]
GO
ALTER TABLE [dbo].[UsersLog]  WITH CHECK ADD  CONSTRAINT [FK_UsersLog_Users] FOREIGN KEY([Uid])
REFERENCES [dbo].[Users] ([Uid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersLog] CHECK CONSTRAINT [FK_UsersLog_Users]
GO
ALTER TABLE [dbo].[UsersManagers]  WITH CHECK ADD  CONSTRAINT [FK_UsersManagers_Users] FOREIGN KEY([Uid])
REFERENCES [dbo].[Users] ([Uid])
GO
ALTER TABLE [dbo].[UsersManagers] CHECK CONSTRAINT [FK_UsersManagers_Users]
GO
ALTER TABLE [dbo].[UsersNotification]  WITH CHECK ADD  CONSTRAINT [FK_UsersNotification_Users] FOREIGN KEY([Uid])
REFERENCES [dbo].[Users] ([Uid])
GO
ALTER TABLE [dbo].[UsersNotification] CHECK CONSTRAINT [FK_UsersNotification_Users]
GO
ALTER TABLE [dbo].[UsersPassword]  WITH CHECK ADD  CONSTRAINT [FK_UsersPassword_Users] FOREIGN KEY([Uid])
REFERENCES [dbo].[Users] ([Uid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersPassword] CHECK CONSTRAINT [FK_UsersPassword_Users]
GO
ALTER TABLE [dbo].[UsersRole]  WITH CHECK ADD  CONSTRAINT [FK_UsersRole_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersRole] CHECK CONSTRAINT [FK_UsersRole_Roles]
GO
ALTER TABLE [dbo].[UsersRole]  WITH CHECK ADD  CONSTRAINT [FK_UsersRole_Users] FOREIGN KEY([Uid])
REFERENCES [dbo].[Users] ([Uid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersRole] CHECK CONSTRAINT [FK_UsersRole_Users]
GO
/****** Object:  StoredProcedure [dbo].[ApplicationData_Reset]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================


GO
/****** Object:  StoredProcedure [dbo].[LogEmail_Insert]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 8/23/2015
-- Description:	Insert into LogEmail
-- =============================================
CREATE PROCEDURE [dbo].[LogEmail_Insert]
	-- Add the parameters for the stored procedure here
	@Uid int = null,
	@Message varchar(max) = null,
	@Email varchar(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO dbo.LogEmail
		VALUES
			(@Uid, @Message, @Email, GETDATE(), NULL)

	SELECT SCOPE_IDENTITY()
END

GO
/****** Object:  StoredProcedure [dbo].[LogEmail_Select]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 9/18/2015
-- Description:	Select [LogEmail]
-- =============================================
create PROCEDURE [dbo].[LogEmail_Select]
	-- Add the parameters for the stored procedure here
	@ActionID		int					=	1,
	@Uid			int					=	null,
	@CurrentPage	int					=	0,
	@PageSize	    int					=	5,
	@SortColumn		varchar(50)			=	'',
	@SortOrder		varchar(5)			=	'',
	@Search			varchar(50)			=	''

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@ActionID = 1) -- admin
		BEGIN
			SELECT le.*, u.*, COUNT(*) OVER() TotalCount, @CurrentPage CurrentPage FROM dbo.LogEmail le
			LEFT JOIN Users u
			ON u.Uid = le.Uid
			WHERE le.Uid LIKE '%' + @Search 
			ORDER BY le.Created DESC
			OFFSET (@CurrentPage)*@PageSize ROWS
												FETCH NEXT @PageSize ROWS ONLY
		END
	ELSE IF (@ActionID = 2) -- uid
		BEGIN
			SELECT le.*, u.*, COUNT(*) OVER() TotalCount, @CurrentPage CurrentPage FROM dbo.LogEmail le
			LEFT JOIN Users u
			ON u.Uid = le.Uid
			WHERE le.Uid = @Uid AND le.Uid LIKE '%' + @Search 
			ORDER BY le.Created DESC
			OFFSET (@CurrentPage)*@PageSize ROWS
												FETCH NEXT @PageSize ROWS ONLY
		END
END

GO
/****** Object:  StoredProcedure [dbo].[ManagerList]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 6/2/2016
-- Description:	Manager List
-- =============================================
CREATE PROCEDURE [dbo].[ManagerList]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM dbo.vwUsersRolesList v
	where v.RoleId = 3
	order by Name

END

GO
/****** Object:  StoredProcedure [dbo].[RentPayment_Select]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 8/25/2015
-- Description:	Select RentPayment
-- =============================================
CREATE PROCEDURE [dbo].[RentPayment_Select]
	-- Add the parameters for the stored procedure here
	@ActionID		int					=	1,
	@Uid			int					=	null,
	@CurrentPage	int					=	0,
	@PageSize	    int					=	5,
	@SortColumn		varchar(50)			=	'',
	@SortOrder		varchar(5)			=	'',
	@Search			varchar(50)			=	''--,
	--@App		int					=	0 -- 0 = Users UI, 1 = Admin UI

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Role varchar(20)
	
	SET @Role = (SELECT Name FROM dbo.Roles r LEFT JOIN UsersRole u ON r.RoleId=u.RoleId WHERE u.Uid = @Uid)
	SET @ActionID = (CASE WHEN @Role = 'Admin' THEN 1
						  WHEN @Role = 'Manager' THEN 3
						  WHEN @Role = 'User' THEN 2
					 END)

    -- Insert statements for procedure here
	IF (@ActionID = 1) -- admin
		BEGIN
			SELECT r.*, u.*, rp.Created NoticeCreated, COUNT(*) OVER() TotalCount, @CurrentPage CurrentPage FROM dbo.RentPayment r
			LEFT JOIN Users u ON u.Uid = r.Uid
			LEFT JOIN dbo.[RentPaymentNoticeSendLog] rp ON r.RentPaymentId=rp.RentPaymentId
			WHERE (u.Email LIKE '%'+@Search+'%' OR u.Name LIKE '%'+@Search+'%' OR r.Payment LIKE '%'+@Search+'%')
			ORDER BY r.Created
			OFFSET (@CurrentPage)*@PageSize ROWS
												FETCH NEXT @PageSize ROWS ONLY
		END
	ELSE IF (@ActionID = 2) -- uid
		BEGIN
			SELECT r.*, u.*,rp.Created NoticeCreated, COUNT(*) OVER() TotalCount, @CurrentPage CurrentPage FROM dbo.RentPayment r
			LEFT JOIN Users u ON u.Uid = r.Uid
			LEFT JOIN dbo.[RentPaymentNoticeSendLog] rp ON r.RentPaymentId=rp.RentPaymentId
			WHERE r.Uid = @Uid AND (u.Email LIKE '%'+@Search+'%' OR u.Name LIKE '%'+@Search+'%' OR r.Payment LIKE '%'+@Search+'%')
								--AND r.Active = 1
							--AND (CASE WHEN @App = 0 THEN @App ELSE r.Active END) = @App
			ORDER BY r.Created
			OFFSET (@CurrentPage)*@PageSize ROWS
												FETCH NEXT @PageSize ROWS ONLY
		END
	ELSE IF (@ActionID = 3) -- manager
		BEGIN
			SELECT r.*, u.*,rp.Created NoticeCreated, COUNT(*) OVER() TotalCount, @CurrentPage CurrentPage FROM dbo.RentPayment r WITH (NOLOCK)
			LEFT JOIN Users u ON r.Uid = u.Uid -- (SELECT Uid FROM dbo.UsersManagers WHERE ManagerId = @Uid)
			LEFT JOIN dbo.[RentPaymentNoticeSendLog] rp ON r.RentPaymentId=rp.RentPaymentId
			--LEFT JOIN UsersManagers um ON u.Uid = um.ManagerId
			WHERE u.Uid IN (SELECT Uid FROM dbo.UsersManagers WHERE ManagerId = @Uid) AND (u.Email LIKE '%'+@Search+'%' OR u.Name LIKE '%'+@Search+'%' OR r.Payment LIKE '%'+@Search+'%')
			ORDER BY r.Created
			OFFSET (@CurrentPage)*@PageSize ROWS
												FETCH NEXT @PageSize ROWS ONLY
		END
END

GO
/****** Object:  StoredProcedure [dbo].[RentPaymentNoticeSendLog_Update]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 8/18/2015
-- Description:	Update
-- =============================================
CREATE PROCEDURE [dbo].[RentPaymentNoticeSendLog_Update] 
	-- Add the parameters for the stored procedure here
	@Uid int = null,
	@Message varchar(max) = null,
	@Email varchar(100) = null,
	@RentPaymentId int = null
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Id int

	IF EXISTS(SELECT TOP 1 * FROM dbo.RentPaymentNoticeSendLog WHERE Uid = @Uid AND RentPaymentId = @RentPaymentId)
		BEGIN
			UPDATE dbo.RentPaymentNoticeSendLog
			SET Modifed = getdate(), Email = @Email
			WHERE Uid = @Uid AND RentPaymentId = @RentPaymentId
			
			SET @Id = @Uid
		END
	ELSE
		BEGIN
			INSERT INTO dbo.RentPaymentNoticeSendLog
				VALUES
					(@Uid, @Message, @Email, getdate(), null, @RentPaymentId)

			SET @Id = SCOPE_IDENTITY()
		END

    select @Id
END

GO
/****** Object:  StoredProcedure [dbo].[RoleList_Select]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 10.1.2015
-- Description:	Select Users List
-- =============================================
create PROCEDURE [dbo].[RoleList_Select]
	-- Add the parameters for the stored procedure here
	@Uid int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @ActionID int, @Role varchar(20)
	
	SET @Role = (SELECT Name FROM dbo.Roles r LEFT JOIN UsersRole u ON r.RoleId=u.RoleId WHERE u.Uid = @Uid)
	SET @ActionID = (CASE WHEN @Role = 'Admin' THEN 1
						  WHEN @Role = 'Manager' THEN 2
					 END)

	IF (@ActionID = 1) -- Admin
		BEGIN
			SELECT RoleId, Name FROM dbo.Roles r
			--LEFT JOIN dbo.UsersRole ur ON u.Uid=ur.Uid
			--LEFT JOIN dbo.Roles r ON ur.RoleId=r.RoleId
			ORDER BY Name
		END

	ELSE IF (@ActionID = 2) -- Manager
		BEGIN
			SELECT RoleId, Name FROM dbo.Roles r
			--LEFT JOIN dbo.UsersRole ur ON u.Uid=ur.Uid
			--LEFT JOIN dbo.Roles r ON ur.RoleId=r.RoleId
			WHERE Name = 'User'
			ORDER BY Name		
		END

END

GO
/****** Object:  StoredProcedure [dbo].[Users_Select]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 8/25/2015
-- Description:	Select Users
-- =============================================
CREATE PROCEDURE [dbo].[Users_Select]
	-- Add the parameters for the stored procedure here
	--@ActionID		int					=	1,
	@CurrentPage	int					=	0,
	@PageSize	    int					=	5,
	@SortColumn		varchar(50)			=	'',
	@SortOrder		varchar(5)			=	'',
	@Search			varchar(50)			=	'',
	@Uid			int					=	null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ActionID int, @Role varchar(20)
	
	SET @Role = (SELECT Name FROM dbo.Roles r LEFT JOIN UsersRole u ON r.RoleId=u.RoleId WHERE u.Uid = @Uid)
	SET @ActionID = (CASE WHEN @Role = 'Admin' THEN 1
						  WHEN @Role = 'Manager' THEN 2
						  WHEN @Role = 'User' THEN 3
					 END)


    -- Insert statements for procedure here
	IF (@ActionID = 1) -- admin
		BEGIN
			SELECT u.*, ur.*, r.Name RoleName, Payments = (SELECT COUNT(*) FROM dbo.vwRentPayment WHERE Uid = u.Uid AND active = 1), COUNT(*) OVER() TotalCount FROM dbo.Users u
			LEFT JOIN UsersRole ur ON u.UId = ur.UId 
			LEFT JOIN Roles r ON ur.RoleId = r.RoleId
			--LEFT JOIN dbo.vwRentPayment rp ON rp.Uid = u.Uid
			WHERE (Email LIKE '%'+@Search+'%' OR u.Name LIKE '%'+@Search+'%')
			ORDER BY u.Uid 
			OFFSET (@CurrentPage)*@PageSize ROWS
												FETCH NEXT @PageSize ROWS ONLY
		END
	ELSE IF (@ActionID = 2) -- get users by manageid
		BEGIN
			SELECT u.*, ur.*, r.Name RoleName,Payments = (SELECT COUNT(*) FROM dbo.vwRentPayment WHERE Uid = u.Uid AND active = 1), COUNT(*) OVER() TotalCount FROM dbo.Users u
			LEFT JOIN UsersRole ur ON u.UId = ur.UId   
			LEFT JOIN Roles r ON ur.RoleId = r.RoleId
			--LEFT JOIN UsersManagers um ON u.Uid = (SELECT Uid FROM dbo.UsersManagers WHERE ManagerId = @Uid)
			WHERE u.Uid IN (SELECT Uid FROM dbo.UsersManagers WHERE ManagerId = @Uid) AND (Email LIKE '%'+@Search+'%' OR u.Name LIKE '%'+@Search+'%')
			ORDER BY u.Uid 
			OFFSET (@CurrentPage)*@PageSize ROWS
												FETCH NEXT @PageSize ROWS ONLY
		END
	ELSE IF (@ActionID = 3) -- user
		BEGIN
			SELECT u.*, ur.*, 'User' RoleName, Payments = '', COUNT(*) OVER() TotalCount FROM dbo.Users u
			LEFT JOIN UsersRole ur ON u.UId = ur.UId 
			WHERE (Email LIKE '%'+@Search+'%' OR u.Name LIKE '%'+@Search+'%')
					AND u.Uid = @Uid
			ORDER BY u.Uid 
			OFFSET (@CurrentPage)*@PageSize ROWS
												FETCH NEXT @PageSize ROWS ONLY
		END

END

GO
/****** Object:  StoredProcedure [dbo].[UsersAccess_Insert]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 8/31/2015
-- Description:	Set access for user
-- =============================================
CREATE PROCEDURE [dbo].[UsersAccess_Insert]
	-- Add the parameters for the stored procedure here
	@AccessCode varchar(20) = null,
	@Uid int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @zipCode varchar(10)

    -- Insert statements for procedure here
	SELECT @zipCode=ZipCode FROM dbo.Users WHERE Uid = @Uid

	IF NOT EXISTS(SELECT TOP 1 * FROM dbo.UsersAccess WHERE Uid = @Uid)
		BEGIN
			INSERT INTO dbo.UsersAccess 
				VALUES
					(@Uid, 1, @AccessCode, @zipCode, GETDATE(), NULL)
		END
	ELSE
		BEGIN
			SET @Uid = 0
		END

	SELECT @Uid
END

GO
/****** Object:  StoredProcedure [dbo].[UsersAccess_Update]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 8/31/2015
-- Description:	Set access for user
-- =============================================
CREATE PROCEDURE [dbo].[UsersAccess_Update]
	-- Add the parameters for the stored procedure here
	@AccessCode varchar(20) = null,
	@ZipCode varchar(20) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Uid int = null

    -- Insert statements for procedure here
	IF EXISTS(SELECT TOP 1 * FROM dbo.UsersAccess WHERE AccessCode=@AccessCode AND ZipCode=@ZipCode AND Active = 1)
		BEGIN
			SET @Uid = (SELECT Uid FROM dbo.UsersAccess WHERE AccessCode=@AccessCode AND ZipCode=@ZipCode AND Active = 1)

			UPDATE dbo.Users
			SET Active = 1, Modifed=GETDATE()
			WHERE Uid=@Uid
			
			UPDATE dbo.UsersAccess
			SET Active = 0, Modifed=GETDATE()
			WHERE Uid=@Uid
		END
	ELSE
		BEGIN
			SET @Uid = 0
		END

	SELECT @Uid
END

GO
/****** Object:  StoredProcedure [dbo].[UsersList_Select]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 10.1.2015
-- Description:	Select Users List
-- =============================================
CREATE PROCEDURE [dbo].[UsersList_Select]
	-- Add the parameters for the stored procedure here
	@Uid int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @ActionID int, @Role varchar(20)
	
	SET @Role = (SELECT Name FROM dbo.Roles r LEFT JOIN UsersRole u ON r.RoleId=u.RoleId WHERE u.Uid = @Uid)
	SET @ActionID = (CASE WHEN @Role = 'Admin' THEN 1
						  WHEN @Role = 'Manager' THEN 2
						  WHEN @Role = 'User' THEN 3
					 END)

	IF (@ActionID = 1) 
		BEGIN
			SELECT u.Uid, u.Name + ' ('+r.Name+') ' Name FROM dbo.Users u
			LEFT JOIN dbo.UsersRole ur ON u.Uid=ur.Uid
			LEFT JOIN dbo.Roles r ON ur.RoleId=r.RoleId
			WHERE r.Name != @Role
			ORDER BY Name
		END

	ELSE IF (@ActionID = 2)
		BEGIN
			SELECT Uid, Name FROM dbo.Users
			WHERE Uid IN (SELECT Uid FROM dbo.UsersManagers WHERE ManagerId = @Uid) 
			ORDER BY Name			
		END
	ELSE IF (@ActionID = 3)
		BEGIN
			SELECT Uid, Name FROM dbo.Users
			WHERE Uid IN (@Uid) 
			ORDER BY Name			
		END

END

GO
/****** Object:  StoredProcedure [dbo].[UsersLog_Select]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 8/25/2015
-- Description:	Select UserLog
-- =============================================
CREATE PROCEDURE [dbo].[UsersLog_Select]
	-- Add the parameters for the stored procedure here
	@ActionID		int					=	1,
	@Uid			int					=	null,
	@CurrentPage	int					=	0,
	@PageSize	    int					=	5,
	@SortColumn		varchar(50)			=	'',
	@SortOrder		varchar(5)			=	'',
	@Search			varchar(50)			=	''

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@ActionID = 1) -- admin
		BEGIN
			SELECT ul.*, u.*, COUNT(*) OVER() TotalCount, @CurrentPage CurrentPage FROM dbo.UsersLog ul
			LEFT JOIN Users u
			ON u.Uid = ul.Uid
			WHERE ul.Uid LIKE '%' + @Search 
			ORDER BY ul.Created DESC
			OFFSET (@CurrentPage)*@PageSize ROWS
												FETCH NEXT @PageSize ROWS ONLY
		END
	ELSE IF (@ActionID = 2) -- uid
		BEGIN
			SELECT ul.*, u.*, COUNT(*) OVER() TotalCount, @CurrentPage CurrentPage FROM dbo.UsersLog ul
			LEFT JOIN Users u
			ON u.Uid = ul.Uid
			WHERE ul.Uid = @Uid AND ul.Uid LIKE '%' + @Search 
			ORDER BY ul.Created DESC
			OFFSET (@CurrentPage)*@PageSize ROWS
												FETCH NEXT @PageSize ROWS ONLY
		END
END

GO
/****** Object:  StoredProcedure [dbo].[UsersManager_Update]    Script Date: 9/21/2017 8:43:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 6/6/2016
-- Description:	Update User manager status
-- =============================================
CREATE PROCEDURE [dbo].[UsersManager_Update] 
	-- Add the parameters for the stored procedure here
	@Uid int,
	@ManagerId int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF EXISTS(SELECT TOP 1 * 
				FROM dbo.UsersManagers WHERE Uid = @Uid)
		BEGIN
			UPDATE dbo.UsersManagers
			SET ManagerId = @ManagerId,
				Uid = @Uid
			WHERE Uid = @Uid
		END 
	ELSE
		BEGIN
			INSERT INTO dbo.UsersManagers (ManagerId, Uid)
			VALUES (@ManagerId, @Uid)
		END
END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "LogEmail"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 4080
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwLogEmail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwLogEmail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "LogErrors"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwLogErrors'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwLogErrors'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RentPayment"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 192
               Right = 270
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwRentPayment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwRentPayment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwUsers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwUsers'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[34] 4[27] 2[11] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 191
               Right = 214
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UsersRole"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 191
               Right = 652
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Roles"
            Begin Extent = 
               Top = 6
               Left = 690
               Bottom = 135
               Right = 860
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwUsersRolesList'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwUsersRolesList'
GO
