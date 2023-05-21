USE InternetStore;

GO

CREATE PROCEDURE UpdateProductCountInBasket
	@user_id INT,
	@product_id INT,
	@new_count INT
AS
	IF EXISTS(SELECT * FROM Basket WHERE user_id = @user_id AND product_id = @product_id)
	BEGIN
		UPDATE Basket
			SET count = @new_count
		WHERE user_id = @user_id AND product_id = @product_id
	END
GO