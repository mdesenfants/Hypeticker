-- take backups and clean
drop table if exists #traders
select * into #traders from traders

drop table if exists #shares
select * into #shares from Shares

truncate table trades

-- 
select *
from traders
Join shares on shares.TraderId = traders.id
join words on shares.WordId = words.id

select * from trades

exec dbo.Sell 1, 1, 20, 20

select *
from traders
Join shares on shares.TraderId = traders.id
join words on shares.WordId = words.id

select * from trades

exec dbo.Buy 2, 1, 24, 20

select *
from traders
Join shares on shares.TraderId = traders.id
join words on shares.WordId = words.id

select * from trades

-- reset from backups
update original
	set original.Cash = bak.Cash
from traders original
join #traders bak on bak.Id = original.id

update original
	set original.Quantity = bak.Quantity
from shares original
join #shares bak on bak.Id = original.id
