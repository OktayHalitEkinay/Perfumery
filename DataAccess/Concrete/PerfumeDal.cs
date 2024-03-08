using Entities.Concrete;
using DataAccess.Abstract;
using Entities.Dtos.Perfume;
using Core.DataAccess.EntityFramework;

namespace DataAccess.Concrete
{
    public class PerfumeDal : EfEntityRepositoryBase<Perfume>, IPerfumeDal
    {
        public PerfumeryContext context { get; }
        public PerfumeDal(PerfumeryContext perfumeryContext) : base(perfumeryContext)
        {
            context = perfumeryContext;
        }
        public IQueryable<PerfumeDetailDto> GetPerfumeDetails()
        {
            var result = from p in context.Perfumes
                         join b in context.Brands
                         on p.BrandId equals b.BrandId
                         select new PerfumeDetailDto
                         {
                             BrandId = b.BrandId,
                             BrandName = b.BrandName,
                             PerfumeId = p.PerfumeId,
                             PerfumeName = p.PerfumeName,
                             PhotoPath = p.PhotoPath,
                             Price = p.Price
                         };
            return result;
        }
    }
}
