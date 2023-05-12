USE InternetStore

EXEC DropRole;
CREATE TABLE Role(
	role_id char(1) PRIMARY KEY,
	role_name varchar(50) NOT NULL,
	UNIQUE(role_name)
)