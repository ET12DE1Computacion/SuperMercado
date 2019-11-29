using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperMercado
{
    [Table("Cajero")]
    public class Cajero
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("dni")]
        public int Dni { get; set; }

        [Column("nombre"), StringLength(60), Required]
        public string Nombre { get; set; }

        [Column("apellido"), StringLength(60), Required]
        public string Apellido { get; set; }

        [Column("pass"), StringLength(65), Required]
        public string Password { get; set; }

        [NotMapped]
        public string NombreCompleto => $"{Apellido}, {Nombre}";
    }
}