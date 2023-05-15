USE InternetStore;

CREATE TABLE OrderDetails(
	order_id int,
	product_id int
	
	CONSTRAINT [Order.FK_ORDER_ID] 
		FOREIGN KEY(order_id) REFERENCES [Order](order_id) ON DELETE CASCADE,
	
	--CONSTRAINT [Order.FK_PRODUCT_ID] 
	--	FOREIGN KEY(product_id) REFERENCES Basket(product_id)
)