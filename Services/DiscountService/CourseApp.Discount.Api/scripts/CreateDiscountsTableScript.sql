create table Discounts(
	Id serial primary key,
	UserId varchar(100) unique not null,
	Rate smallint not null,
	Code varChar(30) not null,
	CreatedDate timestamp not null default CURRENT_TIMESTAMP
)
