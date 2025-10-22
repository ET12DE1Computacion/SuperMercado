## ¿Como agregamos la dependencias a nuestro proyecto de Biblioteca de Clases?

En la terminal, dentro de nuestro proyecto (`src\cSharp\Super.Dapper\`) abrimos la terminal e ingresamos
```bash
dotnet add package Dapper
dotnet add package MySqlConnector

```

## Diagrama de Clases

```mermaid
classDiagram
    direction TD

    class IRepoCajero {
        <<interface>>
        +AltaCajero(Cajero cajero, string pass) void
        +CajeroPorPass(uint dni, string pass) Cajero?
    }

    class IRepoProducto {
        <<interface>>
        +AltaCategoria(Categoria categoria) void
        +ObtenerCategorias() List~Categoria~
        +AltaProducto(Producto producto) void
        +ObtenerProductos() List~Producto~
        +ObtenerProducto(short id) Producto?
    }

    class IRepoTicket {
        <<interface>>
        +AltaTicket(Ticket ticket) void
        +ObtenerTicket(int id) Ticket?
    }

    class IDbConnection {
        <<interface ext por Dapper>>
        +Execute(string sql, object param) int
        +Query~T~(string sql, object parametros) IEnumerable~T~
        +QueryFirstOrDefault~T~(string sql, object parametros) T
        +BeginTransaction() IDbTransaction
    }

    class IDbTransaction {
        +Commit()
        +Rollback()
    }

    class Repo {
        <<abstract>>
        #IDbConnection _conexion
        +Repo(IDbConnection conexion)
    }

    class RepoCajero {
        -string _queryCajeroPass$
        -string _queryAltaCajero$
        +RepoCajero(IDbConnection conexion)
        +AltaCajero(Cajero cajero, string pass) void
        +CajeroPorPass(uint dni, string pass) Cajero?
    }

    class RepoProducto {
        -string _queryCategorias$
        -string _queryProductos$
        -string _queryProducto$
        +RepoProducto(IDbConnection conexion)
        +ObtenerCategorias() List~Categoria~
        +AltaCategoria(Categoria categoria)
        +ObtenerProductos() List~Producto~
        +AltaProducto(Producto producto)
        +ObtenerProducto(short idProducto) Producto?
    }

    class RepoTicket {
        -string _queryTicket$
        +RepoTicket(IDbConnection conexion)
        +AltaTicket(Ticket ticket) void
        +ObtenerTicket(int idTicket) Ticket?
    }

    class DynamicParameters{
        +Add(string name, object? value, DbType? dbType, ParameterDirection? direction, int? size)
    }

    Repo *-- "1" IDbConnection : _conexion
    Repo ..> DynamicParameters : usa
    IRepoTicket <|.. RepoTicket
    IRepoProducto <|.. RepoProducto
    Repo <|-- RepoCajero
    IRepoCajero <|.. RepoCajero
    Repo <|-- RepoProducto
    Repo <|-- RepoTicket
    RepoTicket ..> IDbTransaction : usa

```

## Algunos aspectos sobre el Diagrama

Van a  ver que los Repositorios concretos, tienen cadenas estáticas (`private static readonly string _queryBla = ...`), el objetivo es definirlo una vez y tenerlo ya asignado en memoria todo el dia, evitando tener que reasignarlo todo el tiempo si lo tuviese como una variable local de cada método.