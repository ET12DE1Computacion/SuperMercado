using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperMercado
{
    [Table("HistorialPrecio")]
    public class HistorialPrecio
    {
        //Propiedad automatica para la PK
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("idHistorial")]
        public int Id { get; set; }
        
        //Propiedad necesario para la inclusion de FK Historial->Producto
        [ForeignKey("idProducto") ,Required]
        public Producto Producto { get; set; }
        
        //Propiedad Automatica
        [Column("precioUnitario") , Required]
        public float PrecioUnitario { get; set; }

        [Column("fechaHora"), Required]
        public DateTime FechaHora { get; set; }

        //Constructor sin parametros
        public HistorialPrecio() { }

        //Constructor que pide un precio
        public HistorialPrecio(float precio)
        {
            PrecioUnitario = precio;
            FechaHora = DateTime.Now;   
        }

        //Nuevo constructor en base a Producto
        //reutilizo el constructor de arriba por medio de ":this()"
        public HistorialPrecio(Producto producto):this(producto.PrecioUnitario)
        {
            Producto = producto;
        }

        /// <summary>
        /// Metodo que indica si la fecha del Historial se encuentra entre las fechas
        /// </summary>
        /// <param name="inicio">Fecha de inicio de la busqueda</param>
        /// <param name="fin">Fecha de fin de la busqueda</param>
        /// <returns>Booleano que indica si al fecha se encuentra entre inicio y fin</returns>
        public bool entre(DateTime inicio, DateTime fin)
        {
            return inicio <= FechaHora && FechaHora <= fin;
        }
    }
}