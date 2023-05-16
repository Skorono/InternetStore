Use InternetStore;

EXEC DropBasket;
CREATE TABLE Basket(
	[user_id] INT NOT NULL,
	product_id INT NOT NULL,
	add_date DATETIME NOT NULL,
	[count] INT NOT NULL CONSTRAINT C_COUNT CHECK([count] > 0),
	
	CONSTRAINT [Basket.FK_USER_ID] FOREIGN KEY([user_id]) 
		REFERENCES dbo.[User](id) ON DELETE CASCADE,
	
	CONSTRAINT [Basket.FK_PRODUCT_ID] FOREIGN KEY([product_id]) 
		REFERENCES dbo.Product(id) ON DELETE CASCADE
)
