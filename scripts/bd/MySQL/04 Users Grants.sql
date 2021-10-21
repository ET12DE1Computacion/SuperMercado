DELIMITER ;
SELECT 'Creando Usuarios y Permisos' AS 'Estado';
# Creacion de Usuarios
DROP USER IF EXISTS 'supermercado'@'localhost';
CREATE USER 'supermercado'@'localhost' IDENTIFIED BY 'supermercado';

DROP USER IF EXISTS 'cajero'@'10.120.0.%';
CREATE USER 'cajero'@'10.120.0.%' IDENTIFIED BY 'passCajero';

-- Este usuario es para pruebas locales
DROP USER IF EXISTS 'cajero'@'localhost';
CREATE USER IF NOT EXISTS 'cajero'@'localhost' IDENTIFIED BY 'passCajero';

DROP USER IF EXISTS 'gerenteSuper'@'localhost';
CREATE USER IF NOT EXISTS 'gerenteSuper'@'localhost' IDENTIFIED BY 'passGerente';

# Grants gerenteSuper
GRANT SELECT, INSERT on Supermercado.Rubro to 'gerenteSuper'@'localhost';
GRANT SELECT, INSERT, UPDATE(nombre, cantidad, precioUnitario) on Supermercado.Producto to 'gerenteSuper'@'localhost';
GRANT SELECT, INSERT on Supermercado.HistorialPrecio to 'gerenteSuper'@'localhost';
GRANT SELECT, INSERT on Supermercado.Cajero TO 'gerenteSuper'@'localhost';
GRANT EXECUTE ON PROCEDURE altaRubro TO 'gerenteSuper'@'localhost';
GRANT EXECUTE ON PROCEDURE altaProducto TO 'gerenteSuper'@'localhost';

# Grants cajero
GRANT SELECT on Supermercado.Cajero to 'cajero'@'10.120.0.%';
GRANT SELECT ON Supermercado.Rubro to 'cajero'@'10.120.0.%';
GRANT SELECT on Supermercado.Producto to 'cajero'@'10.120.0.%'; 
GRANT SELECT, INSERT ON Supermercado.Item to 'cajero'@'10.120.0.%'; 
GRANT SELECT, INSERT, UPDATE ON Supermercado.Ticket to 'cajero'@'10.120.0.%';
GRANT EXECUTE ON PROCEDURE altaTicket TO 'cajero'@'10.120.0.%';

# Grants cajero pruebas locales
GRANT SELECT ON Supermercado.Cajero to 'cajero'@'localhost';
GRANT SELECT ON Supermercado.Rubro to 'cajero'@'localhost';
GRANT SELECT on Supermercado.Producto to 'cajero'@'localhost';
GRANT SELECT, INSERT ON Supermercado.Item to 'cajero'@'localhost';
GRANT SELECT, INSERT ON Supermercado.Ticket to 'cajero'@'localhost';
GRANT EXECUTE ON PROCEDURE altaTicket TO 'cajero'@