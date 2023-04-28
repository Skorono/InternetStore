USE InternetStore;

GO
CREATE PROCEDURE AddUser
	@email varchar(256),
	@password varchar(max),
	@name varchar(max),
	@surname varchar(max) = '',
	@middle_name varchar(max) = '',
	@photo varbinary(max) = NULL,
	@phone_number varchar(12) = NULL,
	@role char(1) = 'U'
AS
	INSERT INTO [User](email, [password], role_id) VALUES(@email, @password, @role);
	INSERT INTO UserPersonalInf(id, name, surname, middle_name, photo, phone_number)
		VALUES(
				(
					SELECT TOP 1 
						id 
					FROM [User]
					ORDER BY id DESC
				), 
				@name, @surname, @middle_name, @photo, @phone_number);
GO

EXEC AddUser @email='astat.popov@yandex.ru', @password='233833', @name='������'
EXEC AddUser @email='free-2staff@yandex.ru', @password='233833', @name='�����'

