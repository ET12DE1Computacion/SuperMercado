using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperMercado
{
    [Table("Categoria")]
    public class Categoria
    {
        //Indico que la BD va a generar los valores para el ID
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("idCategoria")]
        //Propiedad agregada para la persistencia
        public byte Id { get; set; }
        //Propiedad automatica, Nombre
        [Column("categoria"), StringLength(45), Required]
        public string Nombre { get; set; }
        //Constructor
        public Categoria() { }

        public Categoria(string nombre)
        {
            Nombre = nombre;
        }
    }
}