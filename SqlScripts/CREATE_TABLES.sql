use ski_resort_db;

CREATE TABLE Client(
	id INT PRIMARY KEY IDENTITY,
	LastName NVARCHAR(50) NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	Patronymic NVARCHAR(50) NOT NULL,
	PassportSeries INT,
	PassportNumber INT,
	Birthday DATE,
	Address NVARCHAR(100),
	Email NVARCHAR(100),
	Password NVARCHAR(100),
);