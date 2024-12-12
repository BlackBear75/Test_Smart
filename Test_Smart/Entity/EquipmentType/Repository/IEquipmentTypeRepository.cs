using Test_Smart.Base;
using Test_Smart.Base.Repository;

namespace Test_Smart.Entity.EquipmentType.Repository;

public interface IEquipmentTypeRepository<TDocument> : IBaseRepository<TDocument> where TDocument : Document
{
   
}