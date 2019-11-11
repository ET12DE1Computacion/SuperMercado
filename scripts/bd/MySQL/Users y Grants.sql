# Creacion de Usuarios
CREATE USER IF NOT EXISTS 'supermercado'@'localhost' IDENTIFIED BY 'supermercado';
CREATE USER IF NOT EXISTS 'cajero'@'10.120.0.%' IDENTIFIED BY 'passCajero';
CREATE USER IF NOT EXISTS 'gerenteSuper'@'localhost' IDENTIFIED BY 'passGerente';

# Grants gerenteSuper
GRANT SELECT, INSERT on Categoria to 'gerenteSuper'@'localhost';
GRANT SELECT, INSERT, UPDATE(nombre, cantidad, precioUnitario) on Producto to 'gerenteSuper'@'localhost';
GRANT SELECT, INSERT on HistorialPrecio to 'gerenteSuper'@'localhost';
GRANT SELECT(dni, nombre, apellido), INSERT on Cajero TO 'gerenteSuper'@'localhost';