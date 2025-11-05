using System.ComponentModel.DataAnnotations;

namespace Super.Mvc.Models;

public class VMLoguinCajero
{
    [Required(ErrorMessage = "El DNI es obligatorio")]
    [Display(Name = "DNI")]
    public required uint Dni { get; set; } // Usamos tu 'uint'

    [Required(ErrorMessage = "La Contraseña es obligatoria")]
    [DataType(DataType.Password)]
    public required string Pass { get; set; }

    [Display(Name = "Recordarme")]
    public bool Recuerdame { get; set; }
}
