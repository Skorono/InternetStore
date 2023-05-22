USE InternetStore;

GO
CREATE PROCEDURE AddProductToBasket
	@user_id INT,
	@product_id INT,
	@count INT,
	@addDate datetime
AS
	INSERT INTO Basket(user_id, product_id, count, add_date) 
		VALUES(@user_id, @product_id, @count, @addDate);
GO