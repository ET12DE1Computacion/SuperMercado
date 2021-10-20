DELIMITER $$
USE Supermercado $$
SELECT 'Preparando para Inserts' AS 'Estado' $$
SET FOREIGN_KEY_CHECKS=0 $$
	TRUNCATE TABLE HistorialPrecio $$
	TRUNCATE TABLE IngresoStock $$
	TRUNCATE TABLE Item $$
	TRUNCATE TABLE Ticket $$
	TRUNCATE TABLE Cajero $$
	TRUNCATE TABLE Producto $$
	TRUNCATE TABLE Rubro $$
SET FOREIGN_KEY_CHECKS=1 $$

SELECT 'Rellenando Supermercado' AS 'Estado' $$

CALL altaRubro(@idGaseosa, 'Gaseosa') $$
CALL altaRubro(@idLacteo, 'Lacteo') $$

CALL altaProducto(@idManaos, @idGaseosa, 'Manaos Cola 2.25L.', 65.15, 1000) $$
CALL altaProducto(@idCocaCola, @idGaseosa, 'Coca Cola 2.25L.', 137.25, 750) $$