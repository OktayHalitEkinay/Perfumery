using Entities.Concrete;
using Entities.Dtos.Perfume;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IPerfumeService
    {
        IDataResult<List<Perfume>> GetAll();
        IDataResult<IQueryable<Perfume>> GetAllAsQueryable();
        IDataResult<Perfume> GetById(int id);
        IResult Add(Perfume perfume);
        IResult Update(Perfume perfume);
        IDataResult<IQueryable<PerfumeDetailDto>> GetPerfumeDetails();
    }
}
