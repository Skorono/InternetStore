USE InternetStore;

GO
CREATE PROCEDURE AddProductToBasket
	@user_id INT,
	@product_id INT,
	@count INT
AS
	INSERT INTO Basket(user_id, product_id, count) 
		VALUES(@user_id, @product_id, @count);
GO