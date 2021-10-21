DELIMITER $$
USE Supermercado $$
SELECT 'Creando Procedimientos y Funciones Almacenadas' AS 'Estado' $$

DELIMITER $$
DROP PROCEDURE IF EXISTS altaRubro $$
CREATE PROCEDURE altaRubro (OUT unIdRubro TINYINT UNSIGNED, unRubro VARCHAR(45))
BEGIN
	INSERT INTO rubro (rubro) VALUE (unRubro);
	SET unIdRubro = LAST_INSERT_ID();
END $$

DELIMITER $$
DROP PROCEDURE IF EXISTS altaProducto $$
CREATE PROCEDURE altaProducto (OUT unIdProducto SMALLINT, unIdRubro TINYINT UNSIGNED,
                    unNombre VARCHAR(45), unPrecioUnitario DECIMAL(7,2), unaCantidad SMALLINT UNSIGNED)
BEGIN
	INSERT INTO Producto(idRubro, nombre, precioUnitario, cantidad)
                VALUE   (unIdRubro, unNombre, unPrecioUnitario, unaCantidad);
	SET unIdProducto = LAST_INSERT_ID();
END $$

DELIMITER $$
DROP PROCEDURE IF EXISTS altaHistorialPrecio $$
CREATE PROCEDURE altaHistorialPrecio (unIdProducto SMALLINT, unPrecio FLOAT)
BEGIN
    INSERT INTO HistorialPrecio (idProducto, precioUnitario, fechaHora)
                        VALUE   (unIdProducto, unPrecio, now());
END $$

DELIMITER $$
DROP PROCEDURE IF EXISTS altaIngresoStock $$
CREATE PROCEDURE altaIngresoStock (unIdProducto SMALLINT, ingreso SMALLINT UNSIGNED)
BEGIN
    INSERT INTO IngresoStock (idProducto, cantidad, fechaHora)
                        VALUE   (unIdProducto, ingreso, now());
END $$

DELIMITER $$
DROP PROCEDURE IF EXISTS altaTicket $$
CREATE PROCEDURE altaTicket (OUT undIdTicket INT, unDni INT UNSIGNED, unaFechaHora DATETIME)
BEGIN
    INSERT INTO Ticket  (dni, fechaHora)
                VALUE   (unDni, unaFechaHora);
    SET undIdTicket = LAST_INSERT_ID();
END $$