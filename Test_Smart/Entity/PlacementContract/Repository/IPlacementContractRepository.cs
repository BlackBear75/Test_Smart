using Test_Smart.Base;
using Test_Smart.Base.Repository;

namespace Test_Smart.Entity.PlacementContract.Repository;

public interface IPlacementContractRepository<TDocument> : IBaseRepository<TDocument> where TDocument : Document
{
   
}