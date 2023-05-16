USE InternetStore

EXEC DropRole;
CREATE TABLE Role(
	id char(1),
	role_name varchar(50) NOT NULL,
	CONSTRAINT [Role.PK_ID] PRIMARY KEY(id),
	UNIQUE(role_name)
)