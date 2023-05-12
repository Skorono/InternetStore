USE InternetStore;

GO
CREATE PROCEDURE DropSubCategories
AS
	IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
				WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='SubCategories')
		BEGIN
			EXEC DROPLINKS @TABLENAME = 'SubCategories';
			DROP TABLE SubCategories;
		END
GO