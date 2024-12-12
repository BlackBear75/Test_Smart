using Test_Smart.Base;
using Test_Smart.Base.Repository;
using Test_Smart.Configuration;

namespace Test_Smart.Entity.ProductionFacility.Repository;

public class ProductionFacilityRepository<TDocument> : BaseRepository<TDocument>, IProductionFacilityRepository<TDocument> where TDocument : Document
{
    public ProductionFacilityRepository(AppDbContext  databaseConfiguration) : base(databaseConfiguration)
    {
    }
    
}