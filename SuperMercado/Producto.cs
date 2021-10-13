using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SuperMercado
{
    public class Producto
    {
        public short Id { get; set; }
        
        //Propiedad Automatica para Categoria del Producto
        [ForeignKey("idCategoria"),Required]
        public Categoria Categoria { get; set; }

        [Column("nombre"), StringLength(60), Required]
        public string Nombre { get; set; }

        [Column("precioUnitario"), Required]
        public float PrecioUnitario { get; set; }

        [Column("cantidad"), Required]
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
            HistorialPrecio historia = new HistorialPrecio(this);
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

        public override string ToString()
            => $"{Nombre} - {Categoria.Nombre} - Cantidad: {Cantidad} - ${PrecioUnitario:0.00}c/u";
    }
}