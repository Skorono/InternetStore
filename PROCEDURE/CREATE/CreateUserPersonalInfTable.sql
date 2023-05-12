USE InternetStore

EXEC DropUsrPersonalInf;
CREATE TABLE UserPersonalInf(
	id int REFERENCES [User](id) ON UPDATE SET DEFAULT 
										   ON DELETE CASCADE,
	name varchar(MAX) NOT NULL,
	surname varchar(MAX),
	middle_name varchar(MAX),
	photo varbinary(MAX),
	phone_number varchar(12) 
	CONSTRAINT PHONE_FORMAT CHECK(phone_number like '8[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);

CREATE UNIQUE NONCLUSTERED INDEX UQ_PhoneNumber
ON dbo.UserPersonalInf(phone_number)
WHERE phone_number IS NOT NULL;