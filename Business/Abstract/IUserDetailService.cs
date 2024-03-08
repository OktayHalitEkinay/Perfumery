using Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserDetailService
    {
        IDataResult<IQueryable<UserDetail>> GetAll();
        IDataResult<UserDetail> GetById(int id);
        IResult Add(UserDetail userDetail);
        IResult Update(UserDetail userDetail);
    }
}
