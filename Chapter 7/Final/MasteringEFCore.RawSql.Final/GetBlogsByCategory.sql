USE [MasteringEFCoreRawSqlQueries]
GO

/****** Object:  StoredProcedure [dbo].[GetBlogsByCategory]    Script Date: 12/12/2017 10:59:11 AM ******/
DROP PROCEDURE [dbo].[GetBlogsByCategory]
GO

/****** Object:  StoredProcedure [dbo].[GetBlogsByCategory]    Script Date: 12/12/2017 10:59:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetBlogsByCategory] 
	-- Add the parameters for the stored procedure here
	@categoryId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Blog where CategoryId = @categoryId
END
GO

