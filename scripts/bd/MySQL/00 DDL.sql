DROP DATABASE IF EXISTS Supermercado;
CREATE DATABASE Supermercado;
USE Supermercado;
SELECT 'Creando Tablas' AS 'Estado';

CREATE TABLE Rubro(
	idRubro TINYINT UNSIGNED AUTO_INCREMENT,
	rubro VARCHAR(45) NOT NULL,
	CONSTRAINT PK_Rubro PRIMARY KEY (idRubro ASC),
	CONSTRAINT UQ_Rubro_rubro UNIQUE (rubro ASC)
);

CREATE TABLE Cajero(
	dni INT UNSIGNED,
	nombre VARCHAR(45) NOT NULL,
	apellido VARCHAR(45) NOT NULL,
	pass CHAR(64) NOT NULL,
	CONSTRAINT PK_Cajero PRIMARY KEY (dni ASC)
);

CREATE TABLE Producto(
	idProducto SMALLINT AUTO_INCREMENT,
	idRubro TINYINT UNSIGNED NOT NULL,
	nombre VARCHAR(45) NOT NULL,
	precioUnitario DECIMAL(7,2) NOT NULL,
	cantidad SMALLINT UNSIGNED NOT NULL,
	CONSTRAINT PK_Producto PRIMARY KEY (idProducto ASC),
	CONSTRAINT UQ_Producto_nombre UNIQUE (nombre ASC),
	CONSTRAINT FK_Producto_Rubro FOREIGN KEY (idRubro) REFERENCES Rubro (idRubro) ON DELETE No Action ON UPDATE No Action
);

CREATE TABLE HistorialPrecio(
	idProducto SMALLINT NOT NULL,
	fechaHora DATETIME NOT NULL,
	precioUnitario DECIMAL(7,2) NOT NULL,
	CONSTRAINT PK_HistorialPrecio PRIMARY KEY (idProducto ASC, fechaHora ASC),
	CONSTRAINT FK_HistorialPrecio_Producto FOREIGN KEY (idProducto) REFERENCES Producto (idProducto) ON DELETE No Action ON UPDATE No Action
);

CREATE TABLE IngresoStock(
	idProducto SMALLINT NOT NULL,
	fechaHora DATETIME NOT NULL,
	cantidad SMALLINT UNSIGNED NOT NULL,
	CONSTRAINT PK_IngresoStock PRIMARY KEY (fechaHora ASC, idProducto ASC),
	CONSTRAINT FK_IngresoStock_Producto FOREIGN KEY (idProducto) REFERENCES Producto (idProducto) ON DELETE Restrict ON UPDATE Restrict
);

CREATE TABLE Ticket(
	idTicket INT AUTO_INCREMENT,
	dni INT UNSIGNED NOT NULL,
	fechaHora DATETIME NOT NULL,
	CONSTRAINT PK_Ticket PRIMARY KEY (idTicket ASC),
	CONSTRAINT FK_Ticket_Cajero FOREIGN KEY (dni) REFERENCES Cajero (dni) ON DELETE Restrict ON UPDATE Restrict
);

CREATE TABLE Item(
	idProducto SMALLINT NOT NULL,
	idTicket INT NOT NULL,
	cantidad TINYINT UNSIGNED NOT NULL,
	precioUnitario DECIMAL(7,2) NOT NULL,
	CONSTRAINT PK_Item PRIMARY KEY (idProducto ASC, idTicket ASC),
	CONSTRAINT FK_Item_Producto FOREIGN KEY (idProducto) REFERENCES Producto (idProducto) ON DELETE No Action ON UPDATE No Action,
	CONSTRAINT FK_Item_Ticket FOREIGN KEY (idTicket) REFERENCES Ticket (idTicket) ON DELETE No Action ON UPDATE No Action
);
