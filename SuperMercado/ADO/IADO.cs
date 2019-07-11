using System;
using System.Collections.Generic;
using System.Text;

namespace SuperMercado.ADO
{
    public interface IADO
    {
        void agregarCategoria(Categoria categoria);
        List<Categoria> obtenerCategorias();

        void agregarProducto(Producto producto);
        void actualizarProducto(Producto producto);
        List<Producto> obtenerProductos();
       
    }
}
