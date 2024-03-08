using Business.Abstract;
using Entities.Concrete;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Dtos.Perfume;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class PerfumeManager : IPerfumeService
    {
        IPerfumeDal _perfumeDal;

        public PerfumeManager(IPerfumeDal perfumeDal)
        {
            _perfumeDal = perfumeDal;
        }

        public IResult Add(Perfume perfume)
        {
            _perfumeDal.Add(perfume);

            return new SuccessResult(Messages.PerfumeAdded);
        }

        public IDataResult<List<Perfume>> GetAll()
        {
            return new SuccessDataResult<List<Perfume>>(_perfumeDal.GetAll(), Messages.PerfumesListed);
        }

        public IDataResult<IQueryable<Perfume>> GetAllAsQueryable()
        {
            return new SuccessDataResult<IQueryable<Perfume>>(_perfumeDal.GetAllAsQueryable(), Messages.PerfumesListed);
        }

        public IDataResult<Perfume> GetById(int id)
        {
            return new SuccessDataResult<Perfume>(_perfumeDal.Get(p => p.PerfumeId == id));
        }

        public IDataResult<IQueryable<PerfumeDetailDto>> GetPerfumeDetails()
        {
            return new SuccessDataResult<IQueryable<PerfumeDetailDto>>(_perfumeDal.GetPerfumeDetails());
        }

        public IResult Update(Perfume perfume)
        {
            throw new NotImplementedException();
        }
    }
}
