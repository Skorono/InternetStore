USE InternetStore;

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
			WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Role')
BEGIN
	INSERT INTO Role(role_id, role_name) VALUES('U', 'User');
	INSERT INTO Role(role_id, role_name) VALUES('A', 'Administrator');
	INSERT INTO Role(role_id, role_name) VALUES('R', 'Retailer');
END

