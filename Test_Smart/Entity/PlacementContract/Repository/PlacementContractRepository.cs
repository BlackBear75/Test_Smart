using Test_Smart.Base;
using Test_Smart.Base.Repository;
using Test_Smart.Configuration;

namespace Test_Smart.Entity.PlacementContract.Repository;

public class PlacementContractRepositoryRepository<TDocument> : BaseRepository<TDocument>, IPlacementContractRepository<TDocument> where TDocument : Document
{
    public PlacementContractRepositoryRepository(AppDbContext  databaseConfiguration) : base(databaseConfiguration)
    {
    }
    
}