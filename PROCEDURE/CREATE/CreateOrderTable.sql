USE InternetStore

EXEC DropOrder;
CREATE TABLE [Order](
	order_id INT IDENTITY,
	[user_id] INT,
	datatime_of_form DATETIME NOT NULL,
	datatime_of_payment DATETIME,
	paid bit NOT NULL 
	CONSTRAINT [Order.PK_ORDER_ID] PRIMARY Key(order_id),
	CONSTRAINT [Order.FK_USER_ID] FOREIGN KEY([user_id]) REFERENCES [User](id)
)