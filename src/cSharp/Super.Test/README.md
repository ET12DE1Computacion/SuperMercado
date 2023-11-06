### Requisitos

Para poder ejecutar este proyecto, es necesario ejecutar primero el archivo de scripts [/scripts/bd/MySQL/install.sql](/scripts/bd/MySQL/install.sql) (es necesario hacerlo desde una cuenta MySQL con los permisos necesarios).

Este archivo crea la BD, funciones y procedimientos almacenados, triggers y los usuarios y permisos necesarios para poder _testear_ sobre esta BD.

### Despliegue

Desde el directorio `/src/cSharp/` ejecutar en la terminal el comando `dotnet test`.