USE InternetStore;

GO
CREATE VIEW UserViewDTO
AS
	SELECT [User].id, email, password, role_id, [name], surname, middle_name, photo, phone_number FROM [User]
	INNER JOIN UserPersonalInf ON [User].id = UserPersonalInf.id
GO
