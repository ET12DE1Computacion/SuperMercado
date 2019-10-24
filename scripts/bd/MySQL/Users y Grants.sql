CREATE USER IF NOT EXISTS 'supermercado'@'localhost' IDENTIFIED BY 'supermercado';
CREATE USER IF NOT EXISTS 'cajero'@'10.120.0.%' IDENTIFIED BY 'passCajero';
CREATE USER IF NOT EXISTS 'gerenteSuper'@'localhost' IDENTIFIED BY 'passGerente';

GRANT ALL ON Supermercado.* TO 'supermercado'@'localhost';