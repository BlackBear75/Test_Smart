using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test_Smart.Base;

namespace Test_Smart.Entity.PlacementContract;

public class PlacementContract : Document
{
    [Required]
    public Guid ProductionFacilityId { get; set; }

    [ForeignKey(nameof(ProductionFacilityId))]
    public ProductionFacility.ProductionFacility ProductionFacility { get; set; }

    [Required]
    public Guid EquipmentTypeId { get; set; }

    [ForeignKey(nameof(EquipmentTypeId))]
    public EquipmentType.EquipmentType EquipmentType { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; } 
}