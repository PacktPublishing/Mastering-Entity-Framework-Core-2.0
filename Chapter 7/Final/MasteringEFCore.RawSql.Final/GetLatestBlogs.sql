USE [MasteringEFCoreRawSqlQueries]
GO

/****** Object:  StoredProcedure [dbo].[GetLatestBlogs]    Script Date: 12/12/2017 11:05:20 AM ******/
DROP PROCEDURE [dbo].[GetLatestBlogs]
GO

/****** Object:  StoredProcedure [dbo].[GetLatestBlogs]    Script Date: 12/12/2017 11:05:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetLatestBlogs]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Blog where CreatedAt >= DATEADD(MONTH, -3, GETDATE())  order by CreatedAt desc
END
GO

