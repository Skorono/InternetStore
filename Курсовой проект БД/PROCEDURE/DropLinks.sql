USE InternetStore;

GO
CREATE PROCEDURE DROPLINKS
	@TABLENAME varchar(MAX)
AS
	DECLARE @REQ VARCHAR(MAX);
	SELECT 
		@REQ =
		'ALTER TABLE ' +  OBJECT_SCHEMA_NAME(k.parent_object_id) +
		'.[' + OBJECT_NAME(k.parent_object_id) + 
		'] DROP CONSTRAINT ' + k.name
	FROM sys.foreign_keys k
	WHERE referenced_object_id = object_id(@TABLENAME)
	EXEC(@REQ);
GO