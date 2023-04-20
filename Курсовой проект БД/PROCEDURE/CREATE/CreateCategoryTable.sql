Use InternetStore;

EXEC DropCategory;
CREATE TABLE Category(
	id INT IDENTITY,
	category_name VARCHAR(MAX) NOT NULL
	CONSTRAINT [Category.PK_ID] PRIMARY KEY(id)
)