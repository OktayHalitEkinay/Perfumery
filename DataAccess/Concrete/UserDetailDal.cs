using Entities.Concrete;
using DataAccess.Abstract;
using Core.DataAccess.EntityFramework;

namespace DataAccess.Concrete
{
    public class UserDetailDal : EfEntityRepositoryBase<UserDetail>, IUserDetailDal
    {
        public UserDetailDal(PerfumeryContext perfumeryContext) : base(perfumeryContext)
        {
        }
    }
}
