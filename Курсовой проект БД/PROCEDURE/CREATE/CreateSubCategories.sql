USE InternetStore;

EXEC DropSubCategories;
CREATE TABLE SubCategories(
	id int IDENTITY,
	category_id int NOT NULL,
	name varchar(MAX) NOT NULL,
	attributes varchar(MAX),
	CONSTRAINT [SubCategories.PK_SUBCATEGORIES_ID] PRIMARY KEY(id),
	CONSTRAINT [SubCategories.FK_CATEGORY_ID] FOREIGN KEY(category_id) REFERENCES Category(id)
);