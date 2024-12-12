using System.ComponentModel.DataAnnotations;
using Test_Smart.Base;

namespace Test_Smart.Entity.EquipmentType;

public class EquipmentType : Document
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public double AreaPerUnit { get; set; } 
}