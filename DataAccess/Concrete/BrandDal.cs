using Entities.Concrete;
using DataAccess.Abstract;
using Core.DataAccess.EntityFramework;

namespace DataAccess.Concrete
{
    public class BrandDal : EfEntityRepositoryBase<Brand>, IBrandDal
    {
        public BrandDal(PerfumeryContext perfumeryContext) : base(perfumeryContext)
        {
        }
    }
}
