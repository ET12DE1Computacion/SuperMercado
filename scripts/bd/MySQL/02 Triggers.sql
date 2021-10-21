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

DROP TRIGGER IF EXISTS aftUpdProducto $$
CREATE TRIGGER aftUpdProducto AFTER UPDATE ON Producto
FOR EACH ROW
BEGIN
    IF (OLD.precioUnitario != NEW.precioUnitario) THEN
        CALL altaHistorialPrecio (NEW.idProducto, NEW.precioUnitario);
    END IF;
    IF (NEW.cantidad > OLD.cantidad) THEN
        CALL altaIngresoStock (NEW.idProducto, NEW.cantidad - OLD.cantidad);
    END IF;
END $$

DROP TRIGGER IF EXISTS befInsCajero $$
CREATE TRIGGER befInsCajero BEFORE INSERT ON Cajero
FOR EACH ROW
BEGIN
    SET NEW.pass = SHA2(NEW.pass, 256);
END $$