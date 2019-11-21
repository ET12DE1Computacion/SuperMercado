# Creacion de Usuarios
CREATE USER IF NOT EXISTS 'supermercado'@'localhost' IDENTIFIED BY 'supermercado';
CREATE USER IF NOT EXISTS 'cajero'@'10.120.0.%' IDENTIFIED BY 'passCajero';

-- Este usuario es para pruebas locales
CREATE USER IF NOT EXISTS 'cajero'@'localhost' IDENTIFIED BY 'passCajero';
CREATE USER IF NOT EXISTS 'gerenteSuper'@'localhost' IDENTIFIED BY 'passGerente';

# Grants gerenteSuper
GRANT SELECT, INSERT on Supermercado.Categoria to 'gerenteSuper'@'localhost';
GRANT SELECT, INSERT, UPDATE(nombre, cantidad, precioUnitario) on Supermercado.Producto to 'gerenteSuper'@'localhost';
GRANT SELECT, INSERT on Supermercado.HistorialPrecio to 'gerenteSuper'@'localhost';
GRANT SELECT, INSERT on Supermercado.Cajero TO 'gerenteSuper'@'localhost';

# Grants cajero
GRANT SELECT on Supermercado.Cajero to 'cajero'@'10.120.0.%'; 

# Grants cajero pruebas locales
GRANT SELECT on Supermercado.Cajero to 'cajero'@'localhost'; 