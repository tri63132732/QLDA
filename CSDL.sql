CREATE DATABASE BRO4FOOD
GO
USE BRO4FOOD
GO
CREATE TABLE Product(
	Id int IDENTITY(1,1) NOT NULL,
	Title nvarchar(250) NOT NULL,
	Description nvarchar(max) NULL,
	Detail nvarchar(max) NULL,
	Image nvarchar(250) NULL,
	Price decimal(18, 2) NOT NULL,
	PriceSale decimal(18, 2) NULL,
	Quantity int NOT NULL,
	IsHome bit NOT NULL,
	IsSale bit NOT NULL,
	IsFeature bit NOT NULL,
	ProductCategoryId int NOT NULL,
	CreatedDate datetime NOT NULL,
	ModifiedDate datetime NOT NULL,
	IsActive bit NOT NULL,
	ViewCount int NOT NULL
)
