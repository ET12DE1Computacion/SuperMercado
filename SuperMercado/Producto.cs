using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperMercado
{
    public class Producto
    {
        //Propiedad Automatica para Categoria del Producto
        public Categoria Categoria { get; set; }
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
            cambiarPrecioUnitario(precio);
        }

        public void decrementarCantidad(short unidades)
        {
            Cantidad -= unidades;
        }

        public void cambiarPrecioUnitario(float precio)
        {
            PrecioUnitario = precio;
            HistorialPrecio historia = new HistorialPrecio(precio);
            HistorialPrecios.Add(historia);
            //las 2 lineas de arriba se pueden expresar tambien como
            //HistorialPrecios.Add(new HistorialPrecio(precio));            
        }
        public float precioPromedioEntre(DateTime inicio, DateTime fin)
        {
            //Paso 1: filtro los Historiales que se encuentren entre esas fechas con el FindAll
            //Paso 2: sobre la lista filtrada, aplico el metodo para conocer el promedio
            return  HistorialPrecios.FindAll(h => h.entre(inicio, fin)).
                                     Average(h => h.PrecioUnitario);                    
        }
    }
}