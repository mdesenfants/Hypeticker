


CREATE PROCEDURE [dbo].[Sell]
	@trader int = null,
	@word int,
	@quantity int,
	@price int
AS
	begin transaction
		declare @time datetime
		select @time = GETUTCDATE()
		
		-- get quantity from seller
		declare @available int
		if @trader is not null
			begin
				select @available =
					case when quantity < @quantity
					then quantity else @quantity
					end
				from Shares
				where TraderId = @trader and WordId = @word

				-- set seller's quantity aside
				update shares
				set Quantity = Quantity - @available
				where
					TraderId = @trader
					and WordId = @word
			end
		else set @available = @quantity

		-- get matching buy orders
		select top (@available) id, Price, TraderId
		into #buys
		from Trades
		where
			Price >= @price and
			Type = 1 and
			WordId = @word
		order by price desc, created desc

		-- delete matching buy orders
		delete from trades
		where id in (select id from #buys)

		-- update buyers' quantities
		update shares
		set shares.Quantity = shares.Quantity + buys.total
		from Shares
		inner join (
			select count(*) total, traderid
			from #buys
			group by TraderId
		) buys on buys.TraderId = shares.TraderId
		where shares.WordId = @word

		-- Get number of sales orders completed
		declare @transactions int
		select @transactions = count(*) from #buys
		
		-- update seller's cash
		if @trader is not null
		begin
			update Traders
			set Cash = Cash + (@transactions * @price)
			where id = @trader
		end

		-- create new sales
		set @available = @available - @transactions

		while (@available > 0)
		begin
			insert into Trades (
				TraderId,
				WordId,
				Created,
				Expires,
				Type,
				Price
			)
			values (
				@trader,
				@word,
				@time,
				DATEADD(HOUR, CASE WHEN @trader is null THEN 168 ELSE 1 END, @time),
				0,
				@price
			)
			select @available = @available - 1
		end
	commit transaction
RETURN 0