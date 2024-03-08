using Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IOrderDetailService
    {
        IDataResult<IQueryable<OrderDetail>> GetAll();
        IDataResult<OrderDetail> GetById(int id);
        IResult Add(OrderDetail orderDetail);
        IResult Update(OrderDetail orderDetail);
    }
}
