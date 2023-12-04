CREATE DATABASE BRO4FOOD
GO
USE BRO4FOOD
CREATE TABLE KhachHang
(
	ID nvarchar(10) PRIMARY KEY,
	Firstname nvarchar(50) NOT NULL,
	Lastname nvarchar(50) NOT NULL,
	Phone nvarchar(50),
	Email nvarchar(50),
	UserRole nvarchar(50) NOT NULL,
	UserAddress nvarchar(100),
)
GO
CREATE TABLE Category(
	Id varchar(10) NOT NULL,
	Title nvarchar(250) NOT NULL,
	Image nvarchar(250) NULL,
	IsActive bit NOT NULL,
)

GO
CREATE TABLE Product(
	Id int IDENTITY(1,1) NOT NULL,
	Title nvarchar(250) NOT NULL,
	Detail nvarchar(max) NULL,
	Image nvarchar(250) NULL,
	Price decimal(18, 2) NOT NULL,
	PriceSale decimal(18, 2) NULL,
	IsHome bit NOT NULL,
	IsSale bit NOT NULL,
	IsFeature bit NOT NULL,
	CreatedDate datetime NOT NULL,
	ModifiedDate datetime NOT NULL,
	IsActive bit NOT NULL,
	ViewCount int NOT NULL
)
GO
CREATE TABLE ProductCategory(
	CategoryId varchar(10) FOREIGN KEY REFERENCES Category(Id),
	ProductId int FOREIGN KEY REFERENCES Product(Id)
)
