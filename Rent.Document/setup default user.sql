﻿USE [DB_9BB50B_rent]
GO
/****** Object:  StoredProcedure [dbo].[SetDefaultUser]    Script Date: 9/21/2017 12:41:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SetDefaultUser]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO dbo.Roles 
		VALUES ('Admin', GETDATE(), null, 1), ('User', GETDATE(), null, 1), ('Manager', GETDATE(), null, 1)

	INSERT INTO dbo.Users (Name, Email, Phone, Created, Modifed, Active)
		VALUES ('Admin', 'admin@test.com', '1234567890', getdate(), null, 1)

	INSERT INTO dbo.UsersPassword
		VALUES (1, 'test', getdate(), null, 1)

	INSERT INTO dbo.UsersNotification
		VALUES (1, 1,1,getdate(), null)

	INSERT INTO dbo.UsersRole
		VALUES (1, getdate(), 1, 1, null)
END
