using System.ComponentModel.DataAnnotations;

namespace Test_Smart.Models.PlacementContractModels;

public class CreateContractRequest
{
    [Required]
    public string ProductionFacilityCode { get; set; } 

    [Required]
    public string EquipmentTypeCode { get; set; } 

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
