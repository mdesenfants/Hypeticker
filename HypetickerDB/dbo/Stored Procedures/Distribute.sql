
CREATE PROCEDURE [dbo].[Distribute]
	@word varchar(50),
	@profit int
AS
	DECLARE @wordId INT

	SELECT TOP 1 @wordId = Id FROM [dbo].[Words] WHERE Word = UPPER(@word)

	IF @wordId IS NULL
		BEGIN
			INSERT INTO [dbo].[Words] (Word) VALUES (UPPER(@word));
		END
	ELSE
		UPDATE tr
		SET Cash = Cash + pr.Dividend
		FROM [dbo].[Traders] tr
		JOIN (
			SELECT
				TraderId,
				CONVERT(INT, (@profit * (Quantity/1000.0))) as Dividend
			FROM [dbo].[Shares] WHERE WordId = @wordId
		) pr ON pr.TraderId = tr.Id
RETURN 0