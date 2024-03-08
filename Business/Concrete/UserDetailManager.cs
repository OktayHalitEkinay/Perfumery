using Business.Abstract;
using Entities.Concrete;
using DataAccess.Abstract;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class UserDetailManager : IUserDetailService
    {
        IUserDetailDal _userDetailDal;

        public UserDetailManager(IUserDetailDal userDetailDal)
        {
            _userDetailDal = userDetailDal;
        }

        public IResult Add(UserDetail userDetail)
        {
            throw new NotImplementedException();
        }

        public IDataResult<IQueryable<UserDetail>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<UserDetail> GetById(int id)
        {
            return new SuccessDataResult<UserDetail>(_userDetailDal.Get(user => user.UserDetailId == id));
        }

        public IResult Update(UserDetail userDetail)
        {
            throw new NotImplementedException();
        }
    }
}
