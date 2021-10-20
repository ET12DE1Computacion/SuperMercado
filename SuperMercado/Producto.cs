using SuperMercado.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperMercado
{
    public class Producto
    {
        public short Id { get; set; }
        public Rubro Rubro { get; set; }
        public string Nombre { get; set; }
        public float PrecioUnitario { get; set; }
        public short Cantidad { get; set; }

        //Propiedad automatica para la Lista de Historiales de precios
        public List<HistorialPrecio> HistorialPrecios { get; set; }

        //Constructor vacio, inicializa la lista
        public Producto()
        {
            HistorialPrecios = new List<HistorialPrecio>();
        }
        
        public Producto(float precio) : this()
        {
            CambiarPrecioUnitario(precio);
        }

        public void DecrementarCantidad(short unidades)
        {
            Cantidad -= unidades;
        }

        public void CambiarPrecioUnitario(float precio)
        {
            PrecioUnitario = precio;
            HistorialPrecio historia = new HistorialPrecio(this);
            HistorialPrecios.Add(historia);
            //las 2 lineas de arriba se pueden expresar tambien como
            //HistorialPrecios.Add(new HistorialPrecio(precio));            
        }
        public float PrecioPromedioEntre(DateTime inicio, DateTime fin) =>
            //Paso 1: filtro los Historiales que se encuentren entre esas fechas con el FindAll
            //Paso 2: sobre la lista filtrada, aplico el metodo para conocer el promedio
            HistorialPrecios.FindAll(h => h.Entre(inicio, fin)).
                             Average(h => h.PrecioUnitario);

        public override string ToString()
            => $"{Nombre} - {Rubro.Nombre} - Cantidad: {Cantidad} - ${PrecioUnitario:0.00}c/u";
    }
}