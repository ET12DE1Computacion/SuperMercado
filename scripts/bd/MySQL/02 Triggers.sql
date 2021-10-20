DELIMITER $$
USE Supermercado $$
SELECT 'Creando Triggers' AS 'Estado' $$

DROP TRIGGER IF EXISTS aftInsProducto $$
CREATE TRIGGER aftInsProducto AFTER INSERT ON Producto
FOR EACH ROW
BEGIN
    CALL altaHistorialPrecio (NEW.idProducto, NEW.precioUnitario);
    IF (NEW.cantidad > 0) THEN
        CALL altaIngresoStock (NEW.idProducto, NEW.cantidad);
    END IF;
END $$
