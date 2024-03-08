using Business.Abstract;
using Entities.Concrete;
using DataAccess.Abstract;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class OrderDetailManager : IOrderDetailService
    {
        IOrderDetailDal _orderDetailDal;

        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }

        public IResult Add(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        public IDataResult<IQueryable<OrderDetail>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<OrderDetail> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }
    }
}
