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

USE [InternetStore]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (1, N'Комплектующие для ПК')
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (2, N'Бытовая техника')
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (3, N'Инструмент и стройка')
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (4, N'ПК, ноутбуки, периферия')
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (5, N'Офис и мебель')
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (6, N'Отдых и развлечения')
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (7, N'Инструмент и стройка')
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (8, N'Садовая техника')
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (9, N'Дом, декор и посуда')
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (10, N'Автотовары')
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (11, N'Аксессуары и услуги
')
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (12, N'Книги')
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (13, N'Игры и консоли')
GO
INSERT [dbo].[Category] ([id], [category_name]) VALUES (14, N'Канцелярские товары')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[SubCategories] ON 
GO
INSERT [dbo].[SubCategories] ([id], [category_id], [name], [attributes]) VALUES (1, 1, N'Процессоры ', N'{"Модель": null, "Сокет": null}')
GO
INSERT [dbo].[SubCategories] ([id], [category_id], [name], [attributes]) VALUES (2, 1, N'Видеокарты', N'{}')
GO
INSERT [dbo].[SubCategories] ([id], [category_id], [name], [attributes]) VALUES (3, 1, N'Материнские платы', N'{}')
GO
INSERT [dbo].[SubCategories] ([id], [category_id], [name], [attributes]) VALUES (4, 13, N'Игровые консоли', N'{}')
GO
INSERT [dbo].[SubCategories] ([id], [category_id], [name], [attributes]) VALUES (5, 13, N'Игры для PlayStation 4', N'{}')
GO
INSERT [dbo].[SubCategories] ([id], [category_id], [name], [attributes]) VALUES (6, 13, N'Игры для PlayStation 5', N'{}')
GO
INSERT [dbo].[SubCategories] ([id], [category_id], [name], [attributes]) VALUES (7, 13, N'Консоли', N'{}')
GO
INSERT [dbo].[SubCategories] ([id], [category_id], [name], [attributes]) VALUES (9, 12, N'Развитие ребенка', N'{}')
GO
SET IDENTITY_INSERT [dbo].[SubCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (1, 1, N'Процессор AMD Ryzen 5 4600G, SocketAM4, BOX [100-100000147box]', N'{"Модель": "AMD PRO A6-8570", "Сокет": "AM4", "count": 100, "cost": 10990, "image": null}')
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (2, 1, N'Процессор Intel Core i7 9700, LGA 1151v2, OEM', N'{"count":99, "cost": 27990,  "image": null}')
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (8, 2, N'Видеокарта KFA2 GeForce 210 [21GGF4HI00NK] [PCI-E 2.0 1 ГБ GDDR3, 64 бит, DVI-D, HDMI, VGA (D-Sub), GPU 520 МГц]', N'{"count":250, "cost": 2699,  "image": null}')
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (9, 2, N'Видеокарта KFA2 GeForce GT 710 [71GPF4HI00GK] [PCI-E 2.0 2 ГБ GDDR3, 64 бит, DVI-D, HDMI, VGA (D-Sub), GPU 954 МГц]', N'{"count":350, "cost": 3399, "image": null}')
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (10, 2, N'Видеокарта MSI GeForce 210 [N210-1GD3/LP] [PCI-E 2.0 1 ГБ GDDR3, 64 бит, DVI-I, HDMI, VGA (D-Sub), GPU 460 МГц]', N'{"count":550, "cost": 4199,  "image": null}')
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (11, 2, N'Видеокарта KFA2 GeForce GT 730 [73GQF8HX00HK] [PCI-E 2.0 4 ГБ DDR3, 128 бит, DVI-I, HDMI, VGA (D-Sub), GPU 700 МГц]', N'{"count":10, "cost": 4299,  "image": null}')
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (12, 2, N'Видеокарта GIGABYTE GeForce GT 710 Silent LP [GV-N710D5SL-2GL] [PCI-E 2.0 2 ГБ GDDR5, 64 бит, DVI-I, HDMI, GPU 954 МГц]', N'{"count":199, "cost": 4999, "image": null}')
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (13, 2, N'Видеокарта MSI GeForce GT 730 [N730K-4GD3/OCV1] [PCI-E 2.0 4 ГБ GDDR3, 64 бит, DVI-D, HDMI, VGA (D-Sub), GPU 902 МГц]', N'{"count":99, "cost": 7799, "image": null}')
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (14, 3, N'Материнская плата Esonic G41CPL3 [LGA 775, Intel G41, 2xDDR3-1066 МГц, 1xPCI-Ex16, Micro-ATX]', N'{"count":1500, "cost": 2999, "image": null}')
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (1005, 7, N'Игровая консоль PlayStation 5 Blu-Ray Japan Edition CFI-1100A, белый', N'{"count":54490, "cost": 54490, "image": null}')
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (1006, 7, N'Игровая приставка PlayStation 4 Fat (500gb) Black, ps4, 1 геймпад, reseller', N'{"count":25060, "cost": 25060, "image": null}')
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (1007, 7, N'Шлем виртуальной реальности Sony PlayStation VR 2', N'{"count":52755, "cost": 52755, "image": null}')
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (1008, 7, N'Игровая консоль Microsoft Xbox Series S, белый', N'{"count":1500, "cost": 27813, "image": null}')
GO
INSERT [dbo].[Product] ([id], [subcategory_id], [product_name], [properties]) VALUES (1010, 9, N'100 заданий для малыша. 1+ | Дмитриева Валентина Геннадьевна', N'{"count":0, "cost": 267, "image": null}')
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO

-- -------------------------