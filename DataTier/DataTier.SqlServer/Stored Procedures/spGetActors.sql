CREATE PROCEDURE [dbo].[spGetActors]
	@pId int = NULL
AS
	SELECT * FROM [dbo].[Actor]
	WHERE Id = @pId
RETURN 0
