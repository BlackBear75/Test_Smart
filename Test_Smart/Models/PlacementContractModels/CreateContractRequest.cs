namespace Test_Smart.Models.PlacementContractModels;

public class CreateContractRequest
{
    public Guid ProductionFacilityId { get; set; }
    public Guid EquipmentTypeId { get; set; }
    public int Quantity { get; set; }
}