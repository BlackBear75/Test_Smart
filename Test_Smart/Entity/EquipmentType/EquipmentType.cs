using System.ComponentModel.DataAnnotations;
using Test_Smart.Base;

namespace Test_Smart.Entity.EquipmentType;

public class EquipmentType : Document
{
    [Required]
    [MaxLength(100)]
    public string Code { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    public double AreaPerUnit { get; set; }
}
