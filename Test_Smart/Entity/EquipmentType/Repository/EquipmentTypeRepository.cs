using Test_Smart.Base;
using Test_Smart.Base.Repository;
using Test_Smart.Configuration;

namespace Test_Smart.Entity.EquipmentType.Repository;

public class EquipmentTypeRepository<TDocument> : BaseRepository<TDocument>, IEquipmentTypeRepository<TDocument> where TDocument : Document
{
    public EquipmentTypeRepository(AppDbContext  databaseConfiguration) : base(databaseConfiguration)
    {
    }
    
}