DELIMITER ;
USE Supermercado;
SELECT 'Preparando para Inserts' AS 'Estado';
SET FOREIGN_KEY_CHECKS=0;
	TRUNCATE TABLE HistorialPrecio;
	TRUNCATE TABLE IngresoStock;
	TRUNCATE TABLE Item;
	TRUNCATE TABLE Ticket;
	TRUNCATE TABLE Cajero;
	TRUNCATE TABLE Producto;
	TRUNCATE TABLE Rubro;
SET FOREIGN_KEY_CHECKS=1;

SELECT 'Rellenando Supermercado' AS 'Estado';

START TRANSACTION;
	CALL altaRubro(@idGaseosa, 'Gaseosa');
	CALL altaRubro(@idLacteo, 'Lacteo');
	CALL altaRubro(@idLimpieza, 'Limpieza');

	SET @precioManaos = 65.15;
	CALL altaProducto(@idManaos, @idGaseosa, 'Manaos Cola 2.25L.', @precioManaos, 1000);
	
	SET @precioCoca = 137.25;
	CALL altaProducto(@idCocaCola, @idGaseosa, 'Coca Cola 2.25L.', @precioCoca, 750);

	SET @dniPepe = 100;
	SET @dniMoni = 90;
	INSERT INTO Cajero	(dni, nombre, apellido, pass)
		VALUES			(@dniPepe, 'Pepe', 'Argentino', 'zapatos'),
						(@dniMoni, 'Moni', 'Argento', 'cafecito');
	
	CALL altaTicket(@idTicket1, @dniPepe, now());
		INSERT INTO Item	(idProducto, idTicket, cantidad, precioUnitario)
				VALUES		(@idManaos, @idTicket1, 2, @precioManaos),
							(@idCocaCola, @idTicket1, 3, @precioCoca);
COMMIT;