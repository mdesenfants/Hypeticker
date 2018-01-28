CREATE PROCEDURE [dbo].[Buy]
	@trader int,
	@word int,
	@quantity int,
	@price int,
	@ticket uniqueidentifier
AS
	begin transaction
		declare @time datetime
		select @time = GETUTCDATE()
		
		-- get cash from buyer
		declare @available int

		select @available =
			case
				when Cash < (@quantity * @price)
					then Cash
				else (@quantity * @price)
			end
		from traders
		where Id = @trader

		-- set buyer's cash aside
		update traders
		set Cash = Cash - @available
		where id = @trader

		-- get matching sell orders
		declare @bought int
		set @bought = 0
		while (@quantity > 0 and @available >= @price)
		begin
			declare @id int
			declare @salePrice int
			declare @seller int
			
			drop table if exists #record
			select top 1 *
			into #record
			from Trades
			where
				Price <= @price and
				Type = 0 and
				WordId = @word
			order by price desc, created desc

			if ((select count(*) from #record) < 1) break;

			select top 1 @id = id, @salePrice = Price, @seller = TraderId
			from #record

			delete from trades where Id = @id

			-- compensate seller
			update Traders
			set cash = cash + @salePrice
			where id = @seller

			update shares
			set Quantity = Quantity + 1
			where traderid = @trader and wordid = @word

			set @available = @available - @salePrice
			set @quantity = @quantity - 1
		end

		-- store remaining buy orders
		while (@available >= @price and @quantity > 0)
		begin
			insert into Trades (TraderId, WordId, Created, Expires, Type, Price)
			values (@trader, @word, @time, DATEADD(HOUR, 1, @time), 1, @price)
			
			set @available = @available - @price
			set @quantity = @quantity - 1
		end

		-- make change
		update traders
		set cash = cash + @available
		where id = @trader
	commit transaction
RETURN 0