using System;

namespace SuperMercado
{
    public class HistorialPrecio
    {
        //Propiedad Automatica
        public float PrecioUnitario { get; set; }
        public DateTime FechaHora { get; set; }

        //Constructor sin parametros
        public HistorialPrecio() { }

        //Constructor que pide un precio
        public HistorialPrecio(float precio)
        {
            PrecioUnitario = precio;
            FechaHora = DateTime.Now;   
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