USE InternetStore;

EXEC DropProduct;
CREATE TABLE Product(
	id INT IDENTITY,
	subcategory_id INT,
	product_name VARCHAR(MAX) NOT NULL,
	[count] INT NOT NULL
	CONSTRAINT [Product.PK_ID] PRIMARY KEY(id),
	CONSTRAINT [Product.FK_SUBCATEGORY_ID] FOREIGN KEY(subcategory_id) REFERENCES SubCategory(id) ON DELETE CASCADE
)