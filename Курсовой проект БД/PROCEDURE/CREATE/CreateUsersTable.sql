USE InternetStore

EXEC DropUser;
CREATE TABLE [User](
	id int IDENTITY,
	email varchar(256) NOT NULL,
	password varchar(MAX) NOT NULL,
	role_id char(1),
	
	CONSTRAINT [User.PK_ID] PRIMARY KEY(id),
	CONSTRAINT [User.FK_ROLE_ID] FOREIGN KEY(role_id) 
		REFERENCES Role(id) ON DELETE SET NULL,
	
	UNIQUE(email)
)