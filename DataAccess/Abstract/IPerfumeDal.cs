using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos.Perfume;

namespace DataAccess.Abstract
{
    public interface IPerfumeDal : IEntityRepository<Perfume>
    {
        IQueryable<PerfumeDetailDto> GetPerfumeDetails();
    }   
}
