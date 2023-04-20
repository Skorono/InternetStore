USE InternetStore

EXEC DropUsrPersonalInf;
CREATE TABLE UserPersonalInf(
	id int REFERENCES [User](id) ON UPDATE SET DEFAULT 
										   ON DELETE CASCADE,
	name varchar(MAX) NOT NULL,
	surname varchar(MAX),
	middle_name varchar(MAX),
	phone_number varchar(12) CONSTRAINT PHONE_FORMAT CHECK(phone_number like '8[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
	UNIQUE(phone_number)
)