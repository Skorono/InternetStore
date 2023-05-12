USE InternetStore

EXEC DropUser;
CREATE TABLE [User](
	id int PRIMARY KEY IDENTITY,
	email varchar(256) NOT NULL,
	password varchar(MAX) NOT NULL,
	role_id char(1) REFERENCES Role(role_id) ON DELETE SET NULL,
	UNIQUE(email)
)