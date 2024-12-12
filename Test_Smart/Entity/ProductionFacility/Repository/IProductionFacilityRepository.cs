using Test_Smart.Base;
using Test_Smart.Base.Repository;

namespace Test_Smart.Entity.ProductionFacility.Repository;

public interface IProductionFacilityRepository<TDocument> : IBaseRepository<TDocument> where TDocument : Document
{
   
}