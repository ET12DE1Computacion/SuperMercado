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

DROP TRIGGER IF EXISTS befInsItem $$
CREATE TRIGGER befInsItem BEFORE INSERT ON Item
FOR EACH ROW
BEGIN
    DECLARE mensaje VARCHAR(128);
    IF  (EXISTS (SELECT *
                FROM    Producto
                WHERE   idProducto = NEW.idProducto
                AND     cantidad < NEW.cantidad)) THEN
        SET mensaje = CONCAT('No alcanza stock para idProducto: ',NEW.idProducto,' con ',NEW.cantidad,' unidades.');
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = mensaje;
    END IF;
END $$

DROP TRIGGER IF EXISTS aftInsItem $$
CREATE TRIGGER aftInsItem AFTER INSERT ON Item
FOR EACH ROW
BEGIN
    UPDATE  Producto
    SET     cantidad = cantidad - NEW.cantidad
    WHERE   idProducto = NEW.idProducto;
END $$