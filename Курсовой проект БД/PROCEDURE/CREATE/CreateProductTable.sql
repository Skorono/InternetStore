USE InternetStore;

EXEC DropProduct;
CREATE TABLE Product(
	id INT IDENTITY,
	category_id INT,
	product_name VARCHAR(MAX) NOT NULL,
	[count] INT NOT NULL
	CONSTRAINT [Product.PK_ID] PRIMARY KEY(id),
	CONSTRAINT [Product.FK_CATEGORY_ID] FOREIGN KEY(category_id) REFERENCES Category(id) ON DELETE CASCADE
)