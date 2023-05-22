USE master;

DECLARE 
	@DATAPATH char(255) = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\InternetStore.mdf', 
	@LGPATH char(255) = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\InternetStore.ldf';

IF DB_ID('InternetStore') IS NOT NULL
	DROP DATABASE InternetStore;

CREATE DATABASE InternetStore ON
(NAME = Sales_dat,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\InternetStore.mdf',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 5)
LOG ON
(NAME = Sales_log,
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\InternetStore.ldf',
    SIZE = 5 MB,
    MAXSIZE = 25 MB,
    FILEGROWTH = 5 MB);
GO

USE InternetStore

-- Создание хранимых процедур

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
					SELECT id FROM [User] WHERE email=@email
				), 
				@name, @surname, @middle_name, @photo, @phone_number);
GO

GO
CREATE PROCEDURE AddProductToBasket
	@user_id INT,
	@product_id INT,
	@count INT,
	@addDate datetime
AS
	INSERT INTO Basket(user_id, product_id, count, add_date) 
		VALUES(@user_id, @product_id, @count, @addDate);
GO

GO

CREATE PROCEDURE UpdateProductCountInBasket
	@user_id INT,
	@product_id INT,
	@new_count INT
AS
	IF EXISTS(SELECT * FROM Basket WHERE user_id = @user_id AND product_id = @product_id)
	BEGIN
		UPDATE Basket
			SET count = @new_count
		WHERE user_id = @user_id AND product_id = @product_id
	END
GO

GO
CREATE PROCEDURE DropBasket
AS
	IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
			WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Basket')
	BEGIN
		EXEC DROPLINKS @TABLENAME = 'Basket';
		DROP TABLE [Basket];
	END
GO

GO
CREATE PROCEDURE DropCategory
AS
	IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
			WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Category')
	BEGIN
		EXEC DROPLINKS @TABLENAME = 'Category';
		DROP TABLE Category;
	END
GO

GO
CREATE PROCEDURE DropOrder
AS
	IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
				WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Order')
		BEGIN
			EXEC DROPLINKS @TABLENAME = 'Order';
			DROP TABLE [Order];
		END
GO

GO
CREATE PROCEDURE DropProduct
AS
	IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
				WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Product')
		BEGIN
			EXEC DROPLINKS @TABLENAME = 'Product';
			DROP TABLE Product;
		END
GO

GO
CREATE PROCEDURE DropRole
AS
	IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
				WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Role')
		BEGIN
			EXEC DROPLINKS @TABLENAME = 'Role';
			DROP TABLE Role;
		END
GO

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

GO
CREATE PROCEDURE DropUsrPersonalInf
AS
	IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
				WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='UserPersonalInf')
		BEGIN 
			EXEC DROPLINKS @TABLENAME = 'UserPersonalInf';
			DROP TABLE UserPersonalInf;
		END
GO

GO
CREATE PROCEDURE DropUser
AS
	IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
				WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='User')
		BEGIN
			EXEC DROPLINKS @TABLENAME = 'User';
			DROP TABLE [User];
		END
GO

-- --------------------------

-- Создание таблиц

USE InternetStore;

EXEC DropRole;
CREATE TABLE Role(
	id char(1),
	role_name varchar(50) NOT NULL,
	CONSTRAINT [Role.PK_ID] PRIMARY KEY(id),
	UNIQUE(role_name)
)

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

EXEC DropUsrPersonalInf;
CREATE TABLE UserPersonalInf(
	id int,
	name varchar(MAX) NOT NULL,
	surname varchar(MAX),
	middle_name varchar(MAX),
	photo varbinary(MAX),
	phone_number varchar(12)
	
	CONSTRAINT PHONE_FORMAT 
		CHECK(phone_number like '8[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')

	CONSTRAINT [UserPrepsonalInf.FK_ID] FOREIGN KEY(id)
		 REFERENCES [User](id) ON UPDATE SET DEFAULT 
										   ON DELETE CASCADE
);

CREATE UNIQUE NONCLUSTERED INDEX UQ_PhoneNumber
ON dbo.UserPersonalInf(phone_number)
WHERE phone_number IS NOT NULL;

EXEC DropOrder;
CREATE TABLE [Order](
	order_id INT IDENTITY,
	[user_id] INT,
	datetime_of_form DATETIME NOT NULL,
	datetime_of_payment DATETIME,
	paid bit NOT NULL 
	
	CONSTRAINT [Order.PK_ORDER_ID] PRIMARY Key(order_id),
	
	CONSTRAINT [Order.FK_USER_ID] FOREIGN KEY([user_id]) 
		REFERENCES [User](id)
)

CREATE TABLE OrderDetails(
	order_id int,
	product_id int
	
	CONSTRAINT [Order.FK_ORDER_ID] 
		FOREIGN KEY(order_id) REFERENCES [Order](order_id) ON DELETE CASCADE,
	
	--CONSTRAINT [Order.FK_PRODUCT_ID] 
	--	FOREIGN KEY(product_id) REFERENCES Basket(product_id)
)

EXEC DropCategory;
CREATE TABLE Category(
	id INT IDENTITY,
	category_name VARCHAR(MAX) NOT NULL
	CONSTRAINT [Category.PK_ID] PRIMARY KEY(id)
)

EXEC DropSubCategories;
CREATE TABLE SubCategories(
	id int IDENTITY,
	category_id int NOT NULL,
	name varchar(MAX) NOT NULL,
	attributes varchar(MAX),

	CONSTRAINT [SubCategories.PK_SUBCATEGORIES_ID] PRIMARY KEY(id),
	
	CONSTRAINT [SubCategories.FK_CATEGORY_ID] FOREIGN KEY(category_id)
		REFERENCES Category(id)
);

EXEC DropProduct;
CREATE TABLE Product(
	id INT IDENTITY,
	subcategory_id INT,
	product_name VARCHAR(MAX) NOT NULL,
	properties varchar(MAX) NOT NULL,
	
	CONSTRAINT [Product.PK_ID] PRIMARY KEY(id),
	
	CONSTRAINT [Product.FK_SUBCATEGORY_ID] FOREIGN KEY(subcategory_id) 
		REFERENCES SubCategories(id) ON DELETE CASCADE
)

EXEC DropBasket;
CREATE TABLE Basket(
	[user_id] INT NOT NULL,
	product_id INT NOT NULL,
	add_date DATETIME NOT NULL,
	[count] INT NOT NULL CONSTRAINT C_COUNT CHECK([count] > 0),
	
	CONSTRAINT [Basket.FK_USER_ID] FOREIGN KEY([user_id]) 
		REFERENCES dbo.[User](id) ON DELETE CASCADE,
	
	CONSTRAINT [Basket.FK_PRODUCT_ID] FOREIGN KEY([product_id]) 
		REFERENCES dbo.Product(id) ON DELETE CASCADE
)

-- -------------------------

-- Создание представлений

GO
CREATE VIEW UserViewDTO
AS
	SELECT [User].id, email, password, role_id, [name], surname, middle_name, photo, phone_number FROM [User]
	INNER JOIN UserPersonalInf ON [User].id = UserPersonalInf.id
GO

----------------------------

-- Заполнение таблиц данными

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES
			WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='Role')
BEGIN
	INSERT INTO Role(id, role_name) VALUES('U', 'User');
	INSERT INTO Role(id, role_name) VALUES('A', 'Administrator');
	INSERT INTO Role(id, role_name) VALUES('R', 'Retailer');
END

-- -------------------------