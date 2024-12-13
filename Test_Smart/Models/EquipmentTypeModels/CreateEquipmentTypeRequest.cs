using System.ComponentModel.DataAnnotations;

namespace Test_Smart.Models.EquipmentTypeModels;

public class CreateEquipmentTypeRequest
{
    [Required(ErrorMessage = "Code is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Code must be between 3 and 50 characters.")]
    public string Code { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "AreaPerUnit is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "AreaPerUnit must be greater than 0.")]
    public int AreaPerUnit { get; set; }
}