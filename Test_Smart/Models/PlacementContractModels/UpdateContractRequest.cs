using System.ComponentModel.DataAnnotations;

namespace Test_Smart.Models.PlacementContractModels;

public class UpdateContractRequest
{
    [Required(ErrorMessage = "ProductionFacilityCode is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "ProductionFacilityCode must be between 3 and 50 characters.")]
    public string ProductionFacilityCode { get; set; }

    [Required(ErrorMessage = "EquipmentTypeCode is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "EquipmentTypeCode must be between 3 and 50 characters.")]
    public string EquipmentTypeCode { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int Quantity { get; set; }
}