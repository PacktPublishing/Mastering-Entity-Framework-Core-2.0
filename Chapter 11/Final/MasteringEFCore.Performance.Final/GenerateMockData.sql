-- person

Declare @counter int
Set @counter = 1

While @counter <= 1000000
Begin 
	Insert Into Person (FirstName, LastName, Biography, NickName, PhoneNumber) 
		values ('User FirstName - ' + CAST(@counter as nvarchar(10)),'User LastName - ' + CAST(@counter as nvarchar(10)),
			'Biography - About User ' + CAST(@counter as nvarchar(10)), 'User' + CAST(@counter as nvarchar(10)),
			RIGHT('1234567890'+cast(cast(9999999999*rand(checksum(newid())) as bigint) as varchar(10)),10))
   Print @counter
   Set @counter = @counter + 1
End

-- user

Declare @counter int
Declare @personId int
declare @datetime datetime
set @datetime = GETDATE()
Set @counter = 1

While @counter <= 10000
Begin 
	SET @personId = (SELECT TOP 1 Id FROM Person	ORDER BY NEWID())
	Insert Into [dbo].[User](Username, DisplayName,Email,PasswordHash,CreatedAt, CreatedBy, ModifiedAt,ModifiedBy) 
		values ('User' + CAST(@counter as nvarchar(10)),'User - ' + CAST(@counter as nvarchar(10)),
			'user' + CAST(@counter as nvarchar(10)) + '@user' + CAST(@counter as nvarchar(10)) +'.com', '4qEWot31RrWWt6I6lPIkHUAiiO0fkfFdzn2j3MCZkN0=',
			@datetime, @personId, @datetime, @personId)
   Print @counter
   Set @counter = @counter + 1
End

 -- post

Declare @counter int
Declare @personId int
Declare @userId int
Declare @blogId int
Declare @categoryId int
declare @datetime datetime
set @datetime = GETDATE()
Set @counter = 1


While @counter <= 10000
Begin 
	SELECT TOP 1 @personId=Person.Id,@userId=[User].Id FROM Person join [User] on Person.Id=[User].PersonId	ORDER BY NEWID()
	SET @blogId = (SELECT TOP 1 Id FROM Blog	ORDER BY NEWID())
	SET @categoryId = (SELECT TOP 1 Id FROM Category	ORDER BY NEWID())
	Insert Into Post (AuthorId, BlogId, CategoryId, Content, PublishedDateTime, Summary, Title, Url, VisitorCount, CreatedAt, CreatedBy, ModifiedAt, ModifiedBy,FileId) 
		values (@userId,@blogId,@categoryId, 'Content - ' + CAST(@counter as nvarchar(10)),@datetime, 'Summary - ' + CAST(@counter as nvarchar(10)),
			'Title - ' + CAST(@counter as nvarchar(10)), 'Url' + CAST(@counter as nvarchar(10)), 0, @datetime, @personId, @datetime, @personId,cast(cast(0 as binary) as uniqueidentifier))
   Print @counter
   Set @counter = @counter + 1
End